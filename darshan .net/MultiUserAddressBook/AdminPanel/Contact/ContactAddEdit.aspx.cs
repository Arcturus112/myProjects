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

namespace MultiUserAddressBook.AdminPanel.Contact
{
    public partial class ContactAddEdit : System.Web.UI.Page
    {
        #region Load Event
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                FillCountryDropDownList();
                FillContactCategoryDropDownList();



                if (Request.QueryString["ContactID"] != null)
                {
                    lblMassage.Text = "Edit Mode | ContactID = " + Request.QueryString["ContactID"].ToString();
                    FillControls(Convert.ToInt32(Request.QueryString["ContactID"]));
                    FillStateDropDownList();
                    FillCityDropDownList();
                }
                else
                {
                    lblMassage.Text = "Add Mode";
                }


            }
        }
        #endregion Load Event

        #region SelectedIndexChanged : Country
        protected void ddlCountryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStateDropDownList();
        }
        #endregion SelectedIndexChanged : Country

        #region SelectedIndexChanged : State
        protected void ddlStateID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCityDropDownList();
        }
        #endregion SelectedIndexChanged : State

        #region FillDropDown
        #region Fill Country DropDownList
        private void FillCountryDropDownList()
        {
            App_Code.CommonDropDownFillMethods.FillDropDownListCountryByUserID(ddlCountryID, Session["UserID"]);
        }
        #endregion Fill Country DropDownList

        #region Fill State DropDownList
        private void FillStateDropDownList()
        {
            App_Code.CommonDropDownFillMethods.FillDropDownListStateyByUserID(ddlStateID, ddlCountryID, Session["UserID"]);
        }
        #endregion Fill State DropDownList

        #region Fill City DropDownList
        private void FillCityDropDownList()
        {
            App_Code.CommonDropDownFillMethods.FillDropDownListCityByUserID(ddlCityID, ddlStateID, Session["UserID"]);
        }
        #endregion Fill City DropDownList

        #region Fill ContactCategory DropDownList
        private void FillContactCategoryDropDownList()
        {
            App_Code.CommonDropDownFillMethods.FillDropDownListContactCategoryByUserID(ddlContactCategoryID, Session["UserID"]);
        }
        #endregion Fill ContactCategory DropDownList
        #endregion FillDropDown

        #region FillControls
        private void FillControls(SqlInt32 ContactID)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiAddressBookConnectionString"].ConnectionString);
            try
            {
                #region Set Connection & Command Object
                sqlConn.Open();

                SqlCommand objCmd = sqlConn.CreateCommand();

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_Contact_SelectByUserIDContactID";
                objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

                SqlDataReader objSDR = objCmd.ExecuteReader();
                #endregion Set Connection & Command Object
                #region Read the Value And Set The Controls
                if (objSDR.HasRows == true)
                {
                    while (objSDR.Read())
                    {
                        if (!objSDR["CountryID"].Equals(DBNull.Value))
                        {
                            ddlCountryID.SelectedValue = objSDR["CountryID"].ToString().Trim();
                        }
                        if (!objSDR["StateID"].Equals(DBNull.Value))
                        {
                            ddlStateID.SelectedValue = objSDR["StateID"].ToString().Trim();
                        }
                        if (!objSDR["CityID"].Equals(DBNull.Value))
                        {
                            ddlCityID.SelectedValue = objSDR["CityID"].ToString().Trim();
                        }
                        if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                        {
                            ddlContactCategoryID.SelectedValue = objSDR["ContactCategoryID"].ToString().Trim();
                        }
                        if (!objSDR["ContactName"].Equals(DBNull.Value))
                        {
                            txtContactName.Text = objSDR["ContactName"].ToString().Trim();
                        }
                        if (!objSDR["ContactNo"].Equals(DBNull.Value))
                        {
                            txtContactNo.Text = objSDR["ContactNo"].ToString().Trim();
                        }
                        if (!objSDR["WhatsAppNo"].Equals(DBNull.Value))
                        {
                            txtWhatsAppNo.Text = objSDR["WhatsAppNo"].ToString().Trim();
                        }
                        if (!objSDR["BirthDate"].Equals(DBNull.Value))
                        {
                            //txtBirthDate.TextMode = TextBoxMode.SingleLine;
                            //txtBirthDate.Text = objSDR["BirthDate"].ToString().Trim();
                            //txtBirthDate.Text = ((DateTime)objSDR["BirthDate"]).ToString("dd-MM-yyyy");
                            txtBirthDate.Text = Convert.ToDateTime(objSDR["BirthDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                        }
                        if (!objSDR["Email"].Equals(DBNull.Value))
                        {
                            txtEmail.Text = objSDR["Email"].ToString().Trim();
                        }
                        if (!objSDR["Age"].Equals(DBNull.Value))
                        {
                            txtAge.Text = objSDR["Age"].ToString().Trim();
                        }
                        if (!objSDR["Address"].Equals(DBNull.Value))
                        {
                            txtAddress.Text = objSDR["Address"].ToString().Trim();
                        }
                        if (!objSDR["BloodGroup"].Equals(DBNull.Value))
                        {
                            txtBloodGroup.Text = objSDR["BloodGroup"].ToString().Trim();
                        }
                        if (!objSDR["FacebookID"].Equals(DBNull.Value))
                        {
                            txtFacebookID.Text = objSDR["FacebookID"].ToString().Trim();
                        }
                        if (!objSDR["LinkedINID"].Equals(DBNull.Value))
                        {
                            txtLinkedinID.Text = objSDR["LinkedINID"].ToString().Trim();
                        }

                        break;
                    }
                }
                else
                {
                    lblMassage.Text = "No Data Available For The ContactID = " + ContactID.ToString().Trim();
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

        #region Button : Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region Local Variables
            SqlInt32 strCountryID = SqlInt32.Null;
            SqlInt32 strStateID = SqlInt32.Null;
            SqlInt32 strCityID = SqlInt32.Null;
            SqlInt32 strContactCategoryID = SqlInt32.Null;
            SqlString strContactName = SqlString.Null;
            SqlString strContactNo = SqlString.Null;
            SqlString strWhatsAppNo = SqlString.Null;
            SqlString strBirthDate = SqlString.Null;
            SqlString strEmail = SqlString.Null;
            SqlString strAge = SqlString.Null;
            SqlString strAddress = SqlString.Null;
            SqlString strBloodGroup = SqlString.Null;
            SqlString strFacebookID = SqlString.Null;
            SqlString strLinkedINID = SqlString.Null;
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultiAddressBookConnectionString"].ConnectionString);
            #endregion Local Variables

            try
            {
                #region Server Side Validation
                string strErrorMassage = "";

                if (ddlCountryID.SelectedIndex == 0)
                {
                    strErrorMassage += "- Select Country - <br/>";
                }
                if (ddlStateID.SelectedIndex == 0)
                {
                    strErrorMassage += "- Select State - <br/>";
                }
                if (ddlCityID.SelectedIndex == 0)
                {
                    strErrorMassage += "- Select City - <br/>";
                }
                if (ddlContactCategoryID.SelectedIndex == 0)
                {
                    strErrorMassage += "- Select Contact Category - <br/>";
                }
                if (txtContactName.Text.Trim() == "")
                {
                    strErrorMassage += "- Enter Contact Name - <br/>";
                }
                if (txtContactNo.Text.Trim() == "")
                {
                    strErrorMassage += "- Enter Contact No - <br/>";
                }
                if (txtEmail.Text.Trim() == "")
                {
                    strErrorMassage += "- Enter Email - <br/>";
                }
                if (txtAddress.Text.Trim() == "")
                {
                    strErrorMassage += "- Enter Address - <br/>";
                }
                
                if (strErrorMassage.Trim() != "")
                {
                    lblMassage.Text = strErrorMassage;
                    return;
                }
                #endregion Server Side Validation

                #region Gather Information
                if (ddlCountryID.SelectedIndex > 0)
                {
                    strCountryID = Convert.ToInt32(ddlCountryID.SelectedValue);
                }
                if (ddlStateID.SelectedIndex > 0)
                {
                    strStateID = Convert.ToInt32(ddlStateID.SelectedValue);
                }
                if (ddlCityID.SelectedIndex > 0)
                {
                    strCityID = Convert.ToInt32(ddlCityID.SelectedValue);
                }
                if (ddlContactCategoryID.SelectedIndex > 0)
                {
                    strContactCategoryID = Convert.ToInt32(ddlContactCategoryID.SelectedValue);
                }
                if (txtContactName.Text.Trim() != "")
                {
                    strContactName = txtContactName.Text.Trim();
                }
                if (txtContactNo.Text.Trim() != "")
                {
                    strContactNo = txtContactNo.Text.Trim();
                }
                if (txtWhatsAppNo.Text.Trim() != "")
                {
                    strWhatsAppNo = txtWhatsAppNo.Text.Trim();
                }
                if (txtBirthDate.Text.Trim() != "")
                {
                    strBirthDate = txtBirthDate.Text.Trim();
                    //strBirthDate = Convert.ToDateTime(txtBirthDate.Text.Trim()).ToString("MM-dd-yyyy");

                }
                if (txtEmail.Text.Trim() != "")
                {
                    strEmail = txtEmail.Text.Trim();
                }
                if (txtAge.Text.Trim() != "")
                {
                    strAge = txtAge.Text.Trim();
                }
                if (txtAddress.Text.Trim() != "")
                {
                    strAddress = txtAddress.Text.Trim();
                }
                if (txtBloodGroup.Text.Trim() != "")
                {
                    strBloodGroup = txtBloodGroup.Text.Trim();
                }
                if (txtFacebookID.Text.Trim() != "")
                {
                    strFacebookID = txtFacebookID.Text.Trim();
                }
                if (txtLinkedinID.Text.Trim() != "")
                {
                    strLinkedINID = txtLinkedinID.Text.Trim();
                }
                #endregion Gather Information

                #region Set Connection & Command Object
                sqlConn.Open();

                SqlCommand objCmd = sqlConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;



                objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
                objCmd.Parameters.AddWithValue("@StateID", strStateID);
                objCmd.Parameters.AddWithValue("@CityID", strCityID);
                objCmd.Parameters.AddWithValue("@ContactCategoryID", strContactCategoryID);
                objCmd.Parameters.AddWithValue("@ContactName", strContactName);
                objCmd.Parameters.AddWithValue("@ContactNo", strContactNo);
                objCmd.Parameters.AddWithValue("@WhatsAppNo", strWhatsAppNo);
                objCmd.Parameters.AddWithValue("@BirthDate", strBirthDate);
                objCmd.Parameters.AddWithValue("@Email", strEmail);
                objCmd.Parameters.AddWithValue("@Age", strAge);
                objCmd.Parameters.AddWithValue("@Address", strAddress);
                objCmd.Parameters.AddWithValue("@BloodGroup", strBloodGroup);
                objCmd.Parameters.AddWithValue("@FacebookID", strFacebookID);
                objCmd.Parameters.AddWithValue("@LinkedINID", strLinkedINID);
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                #endregion Set Connection & Command Object

                if (Request.QueryString["ContactID"] != null)
                {
                    #region Edit Record
                    objCmd.Parameters.AddWithValue("@ContactID", Request.QueryString["ContactID"].ToString().Trim());
                    objCmd.CommandText = "PR_Contact_UpdatePK";
                    objCmd.ExecuteNonQuery();
                    Response.Redirect(GetRouteUrl("AdminPanelContactList"));
                    #endregion Edit Record
                }
                else
                {
                    #region Add Record
                    objCmd.CommandText = "PR_Contact_Insert";
                    objCmd.ExecuteNonQuery();
                    ddlCountryID.SelectedIndex = 0;
                    ddlStateID.SelectedIndex = 0;
                    ddlCityID.SelectedIndex = 0;
                    ddlContactCategoryID.SelectedIndex = 0;
                    txtContactName.Text = "";
                    txtContactNo.Text = "";
                    txtWhatsAppNo.Text = "";
                    txtBirthDate.Text = "";
                    txtEmail.Text = "";
                    txtAge.Text = "";
                    txtAddress.Text = "";
                    txtBloodGroup.Text = "";
                    txtFacebookID.Text = "";
                    txtLinkedinID.Text = "";
                    ddlCountryID.Focus();
                    lblMassage.ForeColor = System.Drawing.Color.Green;
                    lblMassage.Text = "Data Inserted Successfully";
                    Response.Redirect("~/AdminPanel/Contact/ContactList.aspx", true);
                    #endregion Add Record
                }

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
        #endregion Button : Save

        #region Button : Cancel

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminPanel/Contact/ContactList.aspx", true);
        }

        #endregion Button : Cancel
    }
}