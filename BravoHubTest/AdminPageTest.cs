using BravoHub.AdminModule;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace BravoHubTest {
    [TestFixture]
    internal class AdminPageTest {

        [Test]
        public void OnLoad_TabTitle_UsersTab() {
            AdminPageStub adminPage = new AdminPageStub();
            adminPage.OnLoadPage();
            HtmlGenericControl TabTitle = adminPage.GetTabTitleHtmlControl();
            
            Assert.That(TabTitle.InnerText.Equals("Users Tab"));
        }

        private class AdminPageStub : AdminPage {

            public void OnLoadPage() {
                base.Page_Load(new object(), new EventArgs());
            }
            public HtmlGenericControl GetTabTitleHtmlControl() {
                return TabTitle;
            }
        }
    }
}
