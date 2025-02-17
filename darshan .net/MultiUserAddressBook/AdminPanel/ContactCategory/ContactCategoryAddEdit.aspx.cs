﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MultiUserAddressBook.AdminPanel.ContactCategory
{
    public partial class ContactCategoryAddEdit : System.Web.UI.Page
    {
        #region Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ContactCategoryID"] != null)
                {
                    lblMassage.Text = "Edit Mode";
                    FillControls(Convert.ToInt32(Request.QueryString["ContactCategoryID"]));
                }
                else
                {
                    lblMassage.Text = "Add Mode";
                }
            }
        }
        #endregion Load Event

        #region Button : Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region Local Variables
            SqlString strContactCategoryName = SqlString.Null;

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiAddressBookConnectionString"].ConnectionString);
            #endregion Local Variables
            try
            {
                #region Server Side Validation
                string strErrorMassage = "";

                if (txtContactCategoryName.Text.Trim() == "")
                {
                    strErrorMassage += "- Enter Contact Category Name - <br/>";
                }
                if (strErrorMassage.Trim() != "")
                {
                    lblMassage.Text = strErrorMassage;
                    return;
                }
                #endregion Server Side Validation

                objConn.Open();
                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;

                #region Gather Information & Command Object
                if (txtContactCategoryName.Text.Trim() != "")
                {
                    strContactCategoryName = txtContactCategoryName.Text.Trim();
                    objCmd.Parameters.AddWithValue("@ContactCategoryName", strContactCategoryName);
                }
                objCmd.Parameters.AddWithValue("UserID", Session["UserID"]);
                #endregion Gather Information & Command Object

                if (Request.QueryString["ContactCategoryID"] != null)
                {
                    #region Edit Record
                    objCmd.Parameters.AddWithValue("@ContactCategoryID", Request.QueryString["ContactCategoryID"].ToString().Trim());
                    objCmd.CommandText = "PR_ContactCategory_UpdatePK";
                    objCmd.ExecuteNonQuery();
                    Response.Redirect("~/AdminPanel/ContactCategory/ContactCategoryList.aspx", true);
                    #endregion Edit Record
                }
                else
                {
                    #region Add Record
                    objCmd.CommandText = "PR_ContactCategory_Insert";
                    objCmd.ExecuteNonQuery();
                    lblMassage.ForeColor = System.Drawing.Color.Green;
                    lblMassage.Text = "Data Inserted Successfully";
                    txtContactCategoryName.Text = "";
                    txtContactCategoryName.Focus();
                    #endregion Add Record
                }

                objConn.Close();

            }
            catch (Exception ex)
            {
                lblMassage.Text = ex.Message;
            }
            finally
            {
                objConn.Close();
            }
        }
        #endregion Button : Save

        #region FillControls
        private void FillControls(SqlInt32 ContactCategoryID)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiAddressBookConnectionString"].ConnectionString);
            try
            {
                #region Set Connection & Command Object
                sqlConn.Open();

                SqlCommand objCmd = sqlConn.CreateCommand();

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_ContactCategory_SelectByUserIDContactCategoryID";
                objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID.ToString().Trim());
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

                SqlDataReader objSDR = objCmd.ExecuteReader();
                #endregion Set Connection & Command Object
                #region Read the Value And Set The Controls
                if (objSDR.HasRows == true)
                {
                    while (objSDR.Read())
                    {
                        if (!objSDR["ContactCategoryName"].Equals(DBNull.Value))
                        {
                            txtContactCategoryName.Text = objSDR["ContactCategoryName"].ToString().Trim();
                        }
                        break;
                    }
                }
                else
                {
                    lblMassage.Text = "No Data Available For The StateID = " + ContactCategoryID.ToString().Trim();
                }
                #endregion Read the Value And Set The Controls
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                lblMassage.Text = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }
        }
        #endregion FillControls

        #region Button : Cancel
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminPanel/ContactCategory/ContactCategoryList.aspx", true);
        }
        #endregion Button : Cancel
    }
}