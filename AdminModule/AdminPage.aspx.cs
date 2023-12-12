using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BravoHub.AdminModule {
    public enum Tabs {
        USERS,
        MEDIAS,
    }

    public partial class AdminPage : System.Web.UI.Page {
        private readonly string UsersTabTitle = "Users Tab";
        private readonly string MediasTabTitle = "Medias Tab";
        private readonly string UsersTabDescription1 = "Section 1: You can search for any existing user's info";
        private readonly string UsersTabDescription2 = "Section 2: You can delete any existing user's info";
        private readonly string MediasTabDescription1 = "Section 1: You can search for any existing media file info";
        private readonly string MediasTabDescription2 = "Section 2: You can delete any existing media file";
        private readonly string selectedTabKey = "selectedTab";

        private Tabs SelectedTab;

        protected void Page_Load(object sender, EventArgs e) {
            string username = "Edwin";
            UserGreeting.InnerText = $"Welcome {username}";
            if(IsPostBack) {
                SelectedTab = (Tabs)ViewState[selectedTabKey];
            } else {
                ViewState[selectedTabKey] = Tabs.USERS;
                TabTitle.InnerText = UsersTabTitle;
                TabDescription1.InnerText = UsersTabDescription1;
                TabDescription2.InnerText = UsersTabDescription2;

                ViewState[selectedTabKey] = Tabs.USERS;
                UsersBtn.CssClass = "tab tab-selected";
                MediasBtn.CssClass = "tab";
            }


        }

        protected void LogOutBtn_Click(object sender, EventArgs e) {

        }

        protected void UsersBtn_Click(object sender, EventArgs e) {
            // update the fields of the HTML page
            // update the CSS rules to make tab as selected
            if (SelectedTab != Tabs.USERS) {
                TabTitle.InnerText = UsersTabTitle;
                TabDescription1.InnerText = UsersTabDescription1;
                TabDescription2.InnerText = UsersTabDescription2;

                ViewState[selectedTabKey] = Tabs.USERS;
                UsersBtn.CssClass = "tab tab-selected";
                MediasBtn.CssClass = "tab";
            }
        }

        protected void MediasBtn_Click(object sender, EventArgs e) {
            // update the fields of the HTML page
            // update the CSS rules to make tab as selected
            if (SelectedTab != Tabs.MEDIAS) {
                TabTitle.InnerText = MediasTabTitle;
                TabDescription1.InnerText = MediasTabDescription1;
                TabDescription2.InnerText = MediasTabDescription2;

                ViewState[selectedTabKey] = Tabs.MEDIAS;
                UsersBtn.CssClass = "tab";
                MediasBtn.CssClass = "tab tab-selected";
            }
        }
    }
}