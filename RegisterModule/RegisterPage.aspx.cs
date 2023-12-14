using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using BravoHub.DatabaseModule;
using BravoHub.FileLoggerModule;
using MySql.Data.MySqlClient;


namespace BravoHub {
    public partial class RegiterPage : Page 
    {
        private const string LOGIN_PAGE_URL = "../LoginModule/LoginPage.aspx";
        protected const string GO_BACK_BTN_TEXT = "Go back to Login Page";
        private const string SIGN_UP_BTN_TEXT = "Sign Up!";

        private const int SUCCESS = 0;
        private const int ERROR_EMPTY_PARAM = 1;
        private const int ERROR_DONT_MATCH = 2;
        private const int ERROR_REGISTRATION_FAIL = 3;
        private const int ERROR_USERNAME_EXISTS = 4;
        private const int SUCCESS_REDIRECTION = 5;
        private const int GENERAL_ERROR = -1;

        private FileLogger fileLogger;

        protected void Page_Load(object sender, EventArgs e) {
            fileLogger = FileLogger.GetInstance();
        }

        protected void Button1_Click(object sender, EventArgs e) {
            int result = RegisterNewUser();

            switch(result) {
                case SUCCESS:
                    ErrorMsg.InnerText = "Registered!";
                    MainButton.Text = GO_BACK_BTN_TEXT;
                    fileLogger.LogMessage("RegisterPage - User registered successfully");
                    break;
                case ERROR_EMPTY_PARAM:
                    ErrorMsg.InnerText = "There is an empty field, please enter username and password";
                    fileLogger.LogMessage("RegisterPage - Empty username or password", MessageType.WARNING);
                    break;
                case ERROR_DONT_MATCH:
                    ErrorMsg.InnerText = "Passwords do not match, please enter the same password";
                    fileLogger.LogMessage("RegisterPage - confirmation password and password fields do not match", MessageType.WARNING);
                    break;
                case ERROR_REGISTRATION_FAIL:
                    ErrorMsg.InnerText = "something bad happened during registration. Please try again later";
                    fileLogger.LogMessage("RegisterPage - database registration failed", MessageType.ERROR);
                    break;
                case ERROR_USERNAME_EXISTS:
                    ErrorMsg.InnerText = "Username already exist please try another one";
                    fileLogger.LogMessage("RegisterPage - username already exists", MessageType.WARNING);
                    break;
                case SUCCESS_REDIRECTION:
                    // go back to Login Page
                    Response.Redirect(LOGIN_PAGE_URL);
                    FileLogger.GetInstance().LogMessage("RegisterPage - Redirecting to LoginPage.aspx");
                    break;
                default:
                    ErrorMsg.InnerText = "Oops! something unexpected happen. tray again later";
                    fileLogger.LogMessage("RegisterPage - something unexpected happen", MessageType.ERROR);
                    break;
            }
        }

        protected int RegisterNewUser() {
            if(MainButton.Text == GO_BACK_BTN_TEXT) {
                return SUCCESS_REDIRECTION;
            } else  if(MainButton.Text == SIGN_UP_BTN_TEXT) {
                if (RegisterUsername.Value == string.Empty) {
                    return ERROR_EMPTY_PARAM;
                } else if (RegisterPassword1.Value == string.Empty) {
                    return ERROR_EMPTY_PARAM;
                } else if (RegisterPassword1.Value != RegisterPassword2.Value) {
                    return ERROR_DONT_MATCH;
                }

                DatabaseManager db = new DatabaseManager();
                if(db.GetUserByUsername(RegisterUsername.Value) != null ) {
                    // username already exist, so inform the user to change it
                    return ERROR_USERNAME_EXISTS;
                }

                // Insert the new user into the database
                bool isRegistered = db.InsertNewUser(RegisterUsername.Value, RegisterPassword1.Value);
                if (!isRegistered) {
                    return ERROR_REGISTRATION_FAIL;
                }

                return SUCCESS; // Registration successful
            }

            return GENERAL_ERROR;
        }
    }
}