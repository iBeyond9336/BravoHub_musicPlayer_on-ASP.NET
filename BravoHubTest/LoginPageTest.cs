using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BravoHub;
using System.Web.UI.HtmlControls;

namespace BravoHubTest {
    [TestFixture]
    public class LoginPageTest {

        [Test]
        public void LoginButtonClick_ValidUserCredentials_UserLogsIn() {
            LoginPageStub loginPage = new LoginPageStub();
            loginPage.SetUsername("edwin0614");
            loginPage.SetPassword("12345678");

            bool result = loginPage.ValidateUserCredentials();


            Assert.That(result, "Test Fail");
        }

        private class LoginPageStub : LoginPage {

            public LoginPageStub() {
                LoginUsername = new HtmlInputText();
                LoginPassword = new HtmlInputPassword();
            }

            public void SetUsername(string username) {
                LoginUsername.Value = username;
            }

            public void SetPassword(string password) {
                LoginPassword.Value = password;
            }

            public void LoginButtonClick(object o, EventArgs e) {
                base.login_button_Click(o, e);
            }

            public bool ValidateUserCredentials() {
                return base.ValidateUserCredentials();
            }
        }
    }
}
