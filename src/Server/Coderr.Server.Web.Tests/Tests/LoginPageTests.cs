﻿using codeRR.Server.Web.Tests.Pages;
using Xunit;

namespace codeRR.Server.Web.Tests.Tests
{
    [Trait("Category", "Integration")]
    public class LoginPageTests : LoggedInTest
    {
        [Fact]
        public void Should_not_be_able_to_login_with_empty_username()
        {
            UITest(() =>
            {
                var sut = new LoginPage(WebDriver)
                    .LoginWithNoUserNameSpecified();

                Assert.IsType<LoginPage>(sut);
                sut.VerifyIsCurrentPage();
            });
        }

        [Fact]
        public void Should_not_be_able_to_login_with_empty_password()
        {
            UITest(() =>
            {
                var sut = new LoginPage(WebDriver)
                    .LoginWithNoPasswordSpecified();

                Assert.IsType<LoginPage>(sut);
                sut.VerifyIsCurrentPage();
            });
        }

        [Fact]
        public void Should_not_be_able_to_login_with_wrong_password()
        {
            UITest(() =>
            {
                var sut = new LoginPage(WebDriver)
                    .LoginWithWrongPasswordSpecified();

                Assert.IsType<LoginPage>(sut);
                sut.VerifyIsCurrentPage();
            });
        }

        [Fact]
        public void Should_be_able_to_login_with_valid_credentials()
        {
            UITest(() =>
            {
                var sut = new LoginPage(WebDriver)
                    .LoginWithValidCredentials();

                Assert.IsType<HomePage>(sut);
                sut.VerifyIsCurrentPage();

                Logout();
            });
        }
    }
}