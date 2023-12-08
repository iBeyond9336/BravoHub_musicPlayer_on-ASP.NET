using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using BravoHub.DatabaseModule;
using MySql.Data.MySqlClient;


namespace BravoHub {
    public partial class RegiterPage : Page 
    {
        private const string LOGIN_PAGE_URL = "../LoginModule/LoginPage.aspx";
        protected const string GO_BACK_BTN_TEXT = "Go back to Login Page";
        private const string SIGN_UP_BTN_TEXT = "Sign Up!";

        protected void Page_Load(object sender, EventArgs e) {

           

        }

        protected void Button1_Click(object sender, EventArgs e) {
            bool result = RegisterNewUser();
            if (result) {
                // Register the user in the DB
                ErrorMsg.InnerText = "Registered!";
                MainButton.Text = GO_BACK_BTN_TEXT;
                return;
            }

            // Otherwise, display the error
            ErrorMsg.InnerText = "Oops something happen";
        }

        protected bool RegisterNewUser() {
            if(MainButton.Text == GO_BACK_BTN_TEXT) {
                // go back to Login Page
                Response.Redirect(LOGIN_PAGE_URL);
                return true;
            } else  if(MainButton.Text == SIGN_UP_BTN_TEXT) {
                if (RegisterUsername.Value == string.Empty) {
                    return false;
                } else if (RegisterPassword1.Value == string.Empty) {
                    return false;
                } else if (RegisterPassword1.Value != RegisterPassword2.Value) {
                    return false;
                }

                databaseHelper db = new databaseHelper();

                // Insert the new user into the database
                bool isRegistered = db.InsertNewUser(RegisterUsername.Value, RegisterPassword1.Value);

                if (!isRegistered)
                {
                    ErrorMsg.InnerText = "Registration failed. Please try again.";
                    return false;
                }

                return true; // Registration successful
            }
            return true;
            
        }

       



    }
}