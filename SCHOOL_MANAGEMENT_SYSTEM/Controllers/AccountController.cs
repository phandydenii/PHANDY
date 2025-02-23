﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Net.Mail;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //if (model.ConfirmPassword == model.Password)
                //{
                    var user = await UserManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var validCredentials = await UserManager.FindAsync(model.Email, model.Password);

                        // When a user is lockedout, this check is done to ensure that even if the credentials are valid
                        // the user can not login until the lockout duration has passed
                        if (await UserManager.IsLockedOutAsync(user.Id))
                        {
                            var timeLeft = TimeZone.CurrentTimeZone.ToLocalTime(user.LockoutEndDateUtc.Value) - DateTime.Now;

                            ModelState.AddModelError("", string.Format("Your account has been locked out for " + timeLeft.Minutes + "." + timeLeft.Seconds + " minutes due to multiple failed login attempts."));
                        }
                        // if user is subject to lockouts and the credentials are invalid
                        // record the failure and check if user is lockedout and display message, otherwise, 
                        // display the number of attempts remaining before lockout
                        else if (await UserManager.GetLockoutEnabledAsync(user.Id) && validCredentials == null)
                        {
                            // Record the failure which also may cause the user to be locked out
                            await UserManager.AccessFailedAsync(user.Id);

                            string message;

                            if (await UserManager.IsLockedOutAsync(user.Id))
                            {
                                message = string.Format("Your account has been locked out for 5 minutes due to multiple failed login attempts.");
                            }
                            else
                            {
                                int accessFailedCount = await UserManager.GetAccessFailedCountAsync(user.Id);

                                int attemptsLeft = 5 - accessFailedCount;

                                message = string.Format("Invalid credentials. You have {0} more attempt(s) before your account gets locked out.", attemptsLeft);
                            }

                            ModelState.AddModelError("", message);
                        }
                        else if (validCredentials == null)
                        {
                            ModelState.AddModelError("", "Invalid credentials. Please try again.");
                        }
                        else
                        {
                            // When token is verified correctly, clear the access failed count used for lockout
                            await UserManager.ResetAccessFailedCountAsync(user.Id);

                            await SignInAsync(user, model.RememberMe);

                            return RedirectToLocal(returnUrl);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                    }

                //}
                //else
                //{
                //    ModelState.AddModelError("", "Username or Password invalid!");
                //    return this.View(model);
                //}

            }

            return View(model);
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != null) // This will check if the user is existed in database (Update)
                {
                    var user = UserManager.FindById(model.Id);

                    user.Email = model.Email;
                    user.BrandId = user.BrandId;
                    user.FullName = model.FullName;
                    user.Sex = model.Sex;
                    UserManager.Update(user); // using UserManager to update any entity in AspNetUser

                    return RedirectToAction("Index", "UserRoles");
                }
                else // New user need to (create)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email, BrandId = 1, Sex = model.Sex, FullName = model.FullName };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        // This code is used to create user roles (Used when we want to create new role)
                        var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                        var roleManager = new RoleManager<IdentityRole>(roleStore);
                        await roleManager.CreateAsync(new IdentityRole("Manage Staff"));
                        await roleManager.CreateAsync(new IdentityRole("Manage Guest"));
                        await roleManager.CreateAsync(new IdentityRole("Manage Register"));
                        await roleManager.CreateAsync(new IdentityRole("Manage Invoice"));
                        await roleManager.CreateAsync(new IdentityRole("Manage User"));
                        await roleManager.CreateAsync(new IdentityRole("Manage Report"));
                        await roleManager.CreateAsync(new IdentityRole("Manage Room"));
                        await roleManager.CreateAsync(new IdentityRole("Manage Utility"));
                        await roleManager.CreateAsync(new IdentityRole("Manage Expense"));
                        await roleManager.CreateAsync(new IdentityRole("Manage Balance"));
                        await roleManager.CreateAsync(new IdentityRole("Manage Process"));
                        await roleManager.CreateAsync(new IdentityRole("Manage Setup"));
                        await roleManager.CreateAsync(new IdentityRole("Manage Booking"));
                        await roleManager.CreateAsync(new IdentityRole("Manage Other Expense"));
                        await roleManager.CreateAsync(new IdentityRole("Manage Payment"));

                        //==========================================================================

                        //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "UserRoles");
                    }else
                    {
                        //AddErrors(result);
                        //Your password not secure please try another!


                    }
                    
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                //if (user != null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                if (user != null)
                    {
                    string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    string to = model.Email;
                    string sub = "Welcome to ROSELANY APARTMENT";
                    string bod = "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>";
                    MailMessage mm = new MailMessage();
                    mm.To.Add(to);
                    mm.Subject = sub;
                    mm.Body = bod;
                    mm.From = new MailAddress("phandy010@gmail.com");
                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("phandy010@gmail.com", "vpvsvqigrcingkbk");
                    smtp.Send(mm);
                    ViewBag.data = "The mail has been sent to " + model.Email + " successfuly";
                    ViewBag.sms = "Please check your email to reset your password.";
                    ViewBag.status = "Success";
                    return View("ForgotPasswordConfirmation");
                }
                else
                {
                    ViewBag.status = "Invalid";
                    ViewBag.data = "Server Respond";
                    ViewBag.sms = "Invalid gmail address!";
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //create captcha
        [AllowAnonymous]
        public ActionResult Captcha()
        {
            Bitmap objBMP = new System.Drawing.Bitmap(60, 30);
            Graphics objGraphics = System.Drawing.Graphics.FromImage(objBMP);
            objGraphics.Clear(Color.DimGray);
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            //' Configure font to use for text
            Font objFont = new Font("Calibri", 14, FontStyle.Bold);
            string randomStr = "";
            int[] myIntArray = new int[5];
            int x;
            //That is to create the random # and add it to our string
            Random autoRand = new Random();
            for (x = 0; x < 5; x++)
            {
                myIntArray[x] = System.Convert.ToInt32(autoRand.Next(0, 9));
                randomStr += (myIntArray[x].ToString());
            }
            //This is to add the string to session cookie, to be compared later
            Session.Add("randomStr", randomStr);
            //' Write out the text
            objGraphics.DrawString(randomStr, objFont, Brushes.White, 4, 4);
            //' Set the content type and return the image
            Response.ContentType = "image/GIF";
            objBMP.Save(Response.OutputStream, ImageFormat.Gif);

            objFont.Dispose();
            objGraphics.Dispose();
            objBMP.Dispose();

            return new EmptyResult();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Your password not secure please try another!", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion


        ///////Sent Mail

    }
}