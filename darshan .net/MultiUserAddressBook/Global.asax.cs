﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace MultiUserAddressBook
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        public static void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");

            routes.MapPageRoute("AdminPanelCountryList", "AdminPanel/Country/List", "~/AdminPanel/Country/CountryList.aspx");
            routes.MapPageRoute("Login", "Login", "~/AdminPanel/Login/Login.aspx");
            routes.MapPageRoute("AdminPanelCityList", "AdminPanel/City/List", "~/AdminPanel/City/CityList.aspx");
            routes.MapPageRoute("AdminPanelContactList", "AdminPanel/Contact/List", "~/AdminPanel/Contact/ContactList.aspx");

        }
    }
}