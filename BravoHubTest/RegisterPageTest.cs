using NUnit.Framework;
using BravoHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NUnit.Framework.Legacy;

namespace BravoHubTest {
    [TestFixture]
    internal class RegisterPageTest {

        [Test]
        public void RegisterNewUser_NoEmptyUsernameAndPassword1AndPassword2_Registered() { 
            RegisteredPageStub registerPage = new RegisteredPageStub();
            registerPage.SetUsername("edwin0614");
            registerPage.SetPassword1("12345678");
            registerPage.SetPassword2("12345678");
            registerPage.SetButtonText("Sign Up!");

            int result = registerPage.RegisterNewUser();

            ClassicAssert.IsTrue(result == 0, "The result must be true, but it was false -> the user was not registered");
        }

        [Test]
        public void RegisterNewUser_EmptyUsername_NotRegistered() {
            RegisteredPageStub registerPage = new RegisteredPageStub();
            registerPage.SetPassword1("12345678");
            registerPage.SetPassword2("12345678");
            registerPage.SetButtonText("Sign Up!");

            int result = registerPage.RegisterNewUser();

            ClassicAssert.IsFalse(result != 0, "The result must be false, but it was true -> the user was registered");
        }

        [Test]
        public void RegisterNewUser_EmptyPassword1_NotRegistered() {
            RegisteredPageStub registerPage = new RegisteredPageStub();
            registerPage.SetUsername("edwin0614");
            registerPage.SetPassword2("12345678");
            registerPage.SetButtonText("Sign Up!");

            int result = registerPage.RegisterNewUser();

            ClassicAssert.IsFalse(result != 0, "The result must be false, but it was true -> the user was registered");
        }

        [Test]
        public void RegisterNewUser_EmptyPassword2_NotRegistered() {
            RegisteredPageStub registerPage = new RegisteredPageStub();
            registerPage.SetUsername("edwin0614");
            registerPage.SetPassword1("12345678");
            registerPage.SetButtonText("Sign Up!");

            int result = registerPage.RegisterNewUser();

            ClassicAssert.IsFalse(result != 0, "The result must be false, but it was true -> the user was registered");
        }

        [Test]
        public void RegisterNewUser_Password1AndPassword2DoNotMatch_NotRegistered() {
            RegisteredPageStub registerPage = new RegisteredPageStub();
            registerPage.SetUsername("edwin0614");
            registerPage.SetPassword1("12345678");
            registerPage.SetPassword2("12345679");
            registerPage.SetButtonText("Sign Up!");

            int result = registerPage.RegisterNewUser();

            ClassicAssert.IsFalse(result != 0, "The result must be false, but it was true -> the user was registered");
        }

        private class RegisteredPageStub : RegiterPage {
            public const string GO_BACK_TEXT_TEST = "Go back to Login Page";

            public RegisteredPageStub() {
                RegisterUsername = new HtmlInputText();
                RegisterPassword1 = new HtmlInputPassword();
                RegisterPassword2 = new HtmlInputPassword();
                MainButton = new Button();
            }

            public void SetUsername(string username) {
                RegisterUsername.Value = username;
            }

            public void SetPassword1(string password) {
                RegisterPassword1.Value = password;
            }

            public void SetPassword2(string password) {
                RegisterPassword2.Value = password;
            }

            public void SetButtonText(string text) {
                MainButton.Text = text;
            }

            public int RegisterNewUser() {
                return base.RegisterNewUser();
            }
        }
    }
}
