using BravoHub.AdminModule;
using BravoHub.DatabaseModule;
using BravoHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace BravoHub.LoginModule.Controller {
    public class LoginController {
        private DatabaseManager databaseManager;
        private Page LoginPage;

        public LoginController(Page LoginPage) {
            databaseManager = new DatabaseManager();
            this.LoginPage = LoginPage;
        }

        public int CheckCredentials(string username, string password) {
            if (username == string.Empty) {
                return 0;
            } else if (password == string.Empty) {
                return 0;
            }

            // if user is Admin
            UserModel user = new UserModel();
            user.Username = username;
            user.Password = password;

            bool result = databaseManager.CheckUserCredentials(user.Username, user.Password);
            return result ? 1 : 2;
        }

        internal void GetNextPagePath(string username) {
            UserModel user = databaseManager.GetUserByUsername(username);

            if(user.Role.Equals("admin")) {
                ((LoginPage)LoginPage).ShowAdminPage();
            }
            else { 
                ((LoginPage)LoginPage).ShowMediaPlayerPage(); 
            }
        }
    }
}