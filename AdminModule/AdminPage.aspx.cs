using System;
using BravoHub.FileLoggerModule;
using BravoHub.DatabaseModule;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Protobuf.WellKnownTypes;
using BravoHub.Models;
using BravoHub.AdminModule.Controller;
using System.Web.UI.HtmlControls;
using System.IO;

namespace BravoHub.AdminModule
{
    public enum Tabs
    {
        USERS,
        MEDIAS,
        LOGS,
    }

    public partial class AdminPage : System.Web.UI.Page {
        private readonly string LOGIN_PAGE = "../LoginModule/LoginPage.aspx";

        private readonly string UsersTabTitle = "Users Tab";
        private readonly string MediasTabTitle = "Medias Tab";
        private readonly string UsersTabDescription1 = "Section 1: You can search for any existing user's info(name)";
        private readonly string UsersTabDescription2 = "Section 2: You can delete any existing user's info";
        private readonly string MediasTabDescription1 = "Section 1: You can search for any existing media file info";
        private readonly string MediasTabDescription2 = "Section 2: You can delete any existing media file";
        private readonly string selectedTabKey = "selectedTab";
        private readonly string USER_SEARCHED = "AdminPage - User searched";
        private readonly string USER_NOT_EXIST = "AdminPage - User does NOT exist";
        private readonly string USER_DELETED = "AdminPage - User deleted";
        private readonly string USER_NOT_DELETED = "AdminPage - User not deleted";
        private readonly string MEDIAS_SEARCHED = "AdminPage - Media searched";
        private readonly string MEDIAS_DELETED = "AdminPage - Media deleted";
        private readonly string TEXTBOX_EMPTY = "Sorry, the input cannot be empty";

        private Tabs SelectedTab;
        private AdminController controller;

        public AdminPage() {
            controller = new AdminController();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // admin account : username: "AdminTest", passwor: "123"
            string username = "AdminTest";
            UserGreeting.InnerText = $"Welcome {username}";
            if (IsPostBack)
            {
                SelectedTab = (Tabs)ViewState[selectedTabKey];
            }
            else
            {
                ViewState[selectedTabKey] = Tabs.USERS;
                TabTitle.InnerText = UsersTabTitle;
                TabDescription1.InnerText = UsersTabDescription1;
                TabDescription2.InnerText = UsersTabDescription2;

                UsersBtn.CssClass = "tab tab-selected";
                MediasBtn.CssClass = "tab";
            }


        }

        protected void LogOutBtn_Click(object sender, EventArgs e) {
            Response.Redirect(LOGIN_PAGE);
        }

        protected void UsersBtn_Click(object sender, EventArgs e)
        {
            // update the fields of the HTML page
            // update the CSS rules to make tab as selected
            if (SelectedTab != Tabs.USERS) {
                ViewState[selectedTabKey] = Tabs.USERS;
                UsersBtn.CssClass = "tab tab-selected";
                MediasBtn.CssClass = "tab";
                LogsBtn.CssClass = "tab";

                // update the sections
                LogSection.Attributes["class"] = "hide-section";
                UserAndMediaSection.Attributes["class"] = "show";

                TabTitle.InnerText = UsersTabTitle;
                TabDescription1.InnerText = UsersTabDescription1;
                TabDescription2.InnerText = UsersTabDescription2;
            }
        }

        protected void MediasBtn_Click(object sender, EventArgs e)
        {
            // update the fields of the HTML page
            // update the CSS rules to make tab as selected
            if (SelectedTab != Tabs.MEDIAS) {
                ViewState[selectedTabKey] = Tabs.MEDIAS;
                UsersBtn.CssClass = "tab";
                MediasBtn.CssClass = "tab tab-selected";
                LogsBtn.CssClass = "tab";

                // update the sections
                LogSection.Attributes["class"] = "hide-section";
                UserAndMediaSection.Attributes["class"] = "show";

                TabTitle.InnerText = MediasTabTitle;
                TabDescription1.InnerText = MediasTabDescription1;
                TabDescription2.InnerText = MediasTabDescription2;
            }
        }

        protected void LogsBtn_Click(object sender, EventArgs e) {
            if(SelectedTab != Tabs.LOGS) {
                // update the tabs
                ViewState[selectedTabKey] = Tabs.LOGS;
                UsersBtn.CssClass = "tab";
                MediasBtn.CssClass = "tab";
                LogsBtn.CssClass = "tab tab-selected";

                // update the sections
                LogSection.Attributes["class"] = "show";
                UserAndMediaSection.Attributes["class"] = "hide-section";

                // load logFileNames
                LogFileList.Items.Clear(); // necessary to avoid duplications when we change tabs
                List<string> logFileList = controller.GetLogFileNames();
                foreach(string logFile in logFileList) {
                    LogFileList.Items.Add(new ListItem(logFile, logFile));
                }

                // Display the logFileContent
                DisplayFileContent(logFileList[0]);
            }
        }

        protected void btnSearchClicked(object sender, EventArgs e)
        {
            string input = SectionInputElement.Text;

            if (string.IsNullOrEmpty(SectionInputElement.Text))
            {
                PromotMsg.Text = TEXTBOX_EMPTY;
                return;
            }

            // if tab is "Users"
            if (TabTitle.InnerText == UsersTabTitle)
            {
                UserModel userInfo = controller.GetUserByUsername(SectionInputElement.Text);

                if (userInfo == null)
                {
                    PromotMsg.Text = USER_NOT_EXIST;
                    FileLogger.GetInstance().LogMessage(USER_NOT_EXIST, MessageType.ERROR);
                }
                else
                {
                    PromotMsg.Text = $"User [{userInfo.Username}] is found:<br/>" +
                                     $"Password: [{userInfo.Password}]<br/>" +
                                     $"Email: [{userInfo.Email}]<br/>" +
                                     $"Role: [{userInfo.Role}]";
                    FileLogger.GetInstance().LogMessage(USER_SEARCHED, MessageType.INFO);
                }

            }
            else// tab is "Media"
            {
                // to search media
                string rootPath = HttpContext.Current.Server.MapPath("~");
                List<string> allFileFound = controller.SearchFiles(rootPath, input);
                string userFeedback = null;
                foreach (string item in allFileFound)
                {
                    userFeedback += item + "<br/>";
                }

                PromotMsg.Text = userFeedback;
                FileLogger.GetInstance().LogMessage(MEDIAS_SEARCHED, MessageType.INFO);
            }

        }

        protected void btnDeleteClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DeleteUserInputElement.Text))
            {
                PromotMsg.Text = TEXTBOX_EMPTY;
                return;
            }

            // if tab is "Users"
            if (TabTitle.InnerText == UsersTabTitle)
            {
                // to delete user in DB
                UserModel userInfo = controller.GetUserByUsername(DeleteUserInputElement.Text);

                if (userInfo == null)
                {
                    PromotMsg.Text = USER_NOT_EXIST;
                    FileLogger.GetInstance().LogMessage(USER_NOT_EXIST, MessageType.ERROR);
                }
                else if (controller.DeleteUser(userInfo)) {
                    PromotMsg.Text = $"User [{userInfo.Username}] deleted";
                    FileLogger.GetInstance().LogMessage(USER_DELETED);
                } else {
                    PromotMsg.Text = $"User [{userInfo.Username}]could not be deleted";
                    FileLogger.GetInstance().LogMessage(USER_NOT_DELETED, MessageType.ERROR);
                }

            }
            else// tab is "Media"
            {
                // to delete media in DB
                PromotMsg.Text = MEDIAS_DELETED;
                FileLogger.GetInstance().LogMessage(MEDIAS_DELETED, MessageType.INFO);
            }
        }

        protected void LogFileList_TextChanged(object sender, EventArgs e) {
            // Display the logFileContent
            DisplayFileContent(LogFileList.Text);
        }

        private void DisplayFileContent(string fileName) {
            TextArea.InnerText = "";
            TextArea.InnerText = controller.GetFileContent(fileName);
        }
    }
}
