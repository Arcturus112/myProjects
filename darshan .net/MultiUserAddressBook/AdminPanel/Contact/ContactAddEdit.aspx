﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultiUserAddressBook.Master" AutoEventWireup="true" CodeBehind="ContactAddEdit.aspx.cs" Inherits="MultiUserAddressBook.AdminPanel.Contact.ContactAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h2>Contact Add Edit Page</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMassage" runat="server" EnableViewState="False"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="row m-3">
                <div class="col-md-4">
                    Country :
                </div>
                <div class="col-md-8">
                    <asp:DropDownList ID="ddlCountryID" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCountryID_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                </div>
            </div>

            <div class="row m-3">
                <div class="col-md-4">
                    State :
                </div>
                <div class="col-md-8">
                    <asp:DropDownList ID="ddlStateID" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlStateID_SelectedIndexChanged" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                </div>
            </div>

            <div class="row m-3">
                <div class="col-md-4">
                    City :
                </div>
                <div class="col-md-8">
                    <asp:DropDownList ID="ddlCityID" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                </div>
            </div>

            <div class="row m-3">
                <div class="col-md-4">
                    Contact Category :
                </div>
                <div class="col-md-8">
                    <asp:DropDownList ID="ddlContactCategoryID" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>

            <div class="row m-3">
                <div class="col-md-4">
                    Contact Name :
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row m-3">
                <div class="col-md-4">
                    Contact No :
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revContact" runat="server" ControlToValidate="txtContactNo" Display="Dynamic" ErrorMessage="Enter Contact Number" ForeColor="Red" ValidationExpression="^[789]\d{9}$"></asp:RegularExpressionValidator>
                </div>
            </div>

            <div class="row m-3">
                <div class="col-md-4">
                    WhatsApp No :
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="txtWhatsAppNo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row m-3">
                <div class="col-md-4">
                    BirthDate :
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
            </div>

            <div class="row m-3">
                <div class="col-md-4">
                    Email :
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Enter Valid Email Address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </div>
            </div>

            <div class="row m-3">
                <div class="col-md-4">
                    Age :
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="txtAge" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row m-3">
                <div class="col-md-4">
                    Address :
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row m-3">
                <div class="col-md-4">
                    BloodGroup :
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="txtBloodGroup" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row m-3">
                <div class="col-md-4">
                    Facebook ID :
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="txtFacebookID" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="row m-3">
                <div class="col-md-4">
                    LinkedinID :
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="txtLinkedinID" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row m-3">
                <div class="col-md-4">

                </div>
                <div class="col-md-8 mb-5">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-dark m-2" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
