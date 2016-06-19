namespace Web.Controllers
{
    using System;
    using System.Net.Mail;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Security;

    using Allors;
    using Allors.Domain;
    using Allors.Meta;
    using Allors.Web.Security;

    using Microsoft.AspNet.Identity;

    using Mvc.Models;

    [Authorize]
    public class AccountController : RealTimeController
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var membershipProvider = new AllorsMembershipProvider();
            if (!membershipProvider.ValidateUser(model.Email, model.Password))
            {
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return this.View(model);
            }

            FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
            return this.RedirectToLocal(returnUrl);
        }

        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var exist = new People(this.AllorsSession).FindBy(M.Person.UserName, model.Email) != null;
                if (exist)
                {
                    this.ModelState.AddModelError("Email", new Exception("User already exists"));
                }
                else
                {
                    var passwordHasher = new PasswordHasher();
                    var passwordHash = passwordHasher.HashPassword(model.Password);

                    var personBuilder = new PersonBuilder(this.AllorsSession);
                    personBuilder.WithUserName(model.Email);
                    personBuilder.WithUserEmail(model.Email);
                    personBuilder.WithUserPasswordHash(passwordHash);

                    var derivationLog = this.AllorsSession.Derive();
                    if (!derivationLog.HasErrors)
                    {
                        this.AllorsSession.Commit();
                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        return this.RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, new Exception(derivationLog.ToString()));
                    }
                }
            }

            return this.View();
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var person = new People(this.AllorsSession).FindBy(M.Person.UserName, model.Email);
                if (person != null)
                {
                    var clearTextPassword = Membership.GeneratePassword(8, 1);

                    var passwordHasher = new PasswordHasher();
                    var passwordHash = passwordHasher.HashPassword(clearTextPassword);
                    person.UserPasswordHash = passwordHash;

                    this.AllorsSession.Derive(true);
                    this.AllorsSession.Commit();

                    var client = new SmtpClient();
                    var mailMessage = new MailMessage
                                          {
                                              From = new MailAddress("support@example.com"),
                                              Subject = "Password reset",
                                              IsBodyHtml = true,
                                              Body = $"<html><body><h1>Password Reset</h1><p>login: {person.UserEmail}<br/>password: {clearTextPassword}</p></body><html>"
                                          };
                    mailMessage.To.Add(person.UserEmail);
                   
                    client.Send(mailMessage);

                    return this.View("ForgotPasswordResult");
                }
                else
                {
                    this.ModelState.AddModelError("Email", new Exception("User doesn't exist"));
                }
            }

            return this.View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}