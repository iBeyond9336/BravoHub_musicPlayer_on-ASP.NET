using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BravoHub {
    public partial class RegiterPage : System.Web.UI.Page {
        private const string LOGIN_PAGE_URL = "../LoginPage.aspx";
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void Button1_Click(object sender, EventArgs e) {
            bool result = RegisterNewUser();
            if (result) {
                // Register the user in the DB
                ErrorMsg.InnerText = "Registered!";
                Button1.Text = "Go back to Login Page";
                return;
            }

            // Otherwise, display the error
            ErrorMsg.InnerText = "Oops something happen";
        }

        private bool RegisterNewUser() {
            if(Button1.Text == "Go back to Login Page") {
                // go back to Login Page
                Response.Redirect(LOGIN_PAGE_URL);
                return true;
            }

            if (RegisterUsername.Value == string.Empty) {
                return false;
            } else if (RegisterPassword1.Value == string.Empty) {
                return false;
            } else if (RegisterPassword1.Value != RegisterPassword2.Value) {
                return false;
            }

            return true;
        }
    }
}