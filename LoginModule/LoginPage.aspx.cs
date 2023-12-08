using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BravoHub {
    public partial class LoginPage : Page {
        protected LoginPage() { }
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void login_button_Click(object sender, EventArgs e) {
            bool result = ValidateUserCredentials();
            if (result) {
                // TODO: redirect to the media player page
                
            }

            // TODO: otherwise we should display an error message to the user
        }

        protected bool ValidateUserCredentials() {
            if (LoginUsername.Value == string.Empty) {
                return false;
            } else if (LoginPassword.Value == string.Empty) {
                return false;
            }


            return true;
        }
    }
}