using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicManagementSystem.Models;
using System.Web.Mvc;
using ClinicManagementSystemBL;
using ClinicManagementSystemDOL.Models;
using ClinicManagementSystemDOL.Enums;
using ClinicManagementSystemDOL.Exceptions;
using AutoMapper;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Net.Configuration;

namespace ClinicManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Returns the login-form view.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the login-form UI.
        /// </remarks>
        /// <returns>The login view</returns>
        // GET: Account/Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Post action to validate the use credentials and redirect to the index page.
        /// </summary>
        /// <remarks>
        /// Once the user details are validated a session is created and the user is redirected to the home page
        /// </remarks>
        /// <param name="Email">The email of the user is required for login</param>
        /// <param name="Password">The password of that particular email is required to validate the user</param>
        /// <returns>If the user credentials are correct, he is redirected to the index page else a error message is displayed</returns>
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (TryValidateModel(model))
            {
                Accounts account = new Accounts();
                if (account.ValidateUserLogin(model.Email, model.Password))
                {
                    Users user = new Users();
                    user.Email = model.Email;
                    var sessionInfo = account.SessionUserInfo(user);
                    Session["userId"] = sessionInfo.Id;
                    Session["roleId"] = sessionInfo.RoleId;
                    Session["firstName"] = sessionInfo.FirstName;
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect Email Id or Password...");
                }
            }
            return View(model);
        }

        /// <summary>
        /// Redirects a user to the index page.
        /// </summary>
        /// <remarks>
        /// <param name="returnUrl">This is the path that the action redirects to, if specified</param>
        /// This is the controller action that redirects user to a specific page or the index page.
        /// </remarks>
        /// <returns>The redirection action</returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Returns the registration-form view.
        /// </summary>
        /// <remarks>
        /// This is the controller action that returns the registration-form UI.
        /// </remarks>
        /// <returns>The registration view</returns>
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Post action to register the user and redirect to the login page.
        /// </summary>
        /// <remarks>
        /// The email ID and the phone number that the user provides should be unique.
        /// <param name="First Name">The first name of the user</param>
        /// <param name="Last Name">The last name of the user</param>
        /// <param name="Mobile">The mobile number of the user</param>
        /// <param name="Address">The address that the user resides in</param>
        /// <param name="Email">The email of the user is required for registration</param>
        /// <param name="Gender">The gender of the user</param>
        /// <param name="Date of Birth">The Date of birth of the user</param>
        /// <param name="Password">The password field is used to secure a user's account</param>
        /// <param name="Confirm Password">This password field is used to make sure that the user entered the correct password</param>
        /// </remarks
        /// <returns>Once all the fields are entered and submitted, the user is registered and can login using these credentials</returns>
        [HttpPost]
        public ActionResult Register(RegisterViewModel model, string returnUrl)
        {
            if (TryValidateModel(model))
            {
                Users user = new Users();
                user.RoleId = Convert.ToInt32(Roles.Patient);
                AutoMapper.Mapper.Map(model, user);
                Patients patient = new Patients();
                AutoMapper.Mapper.Map(model, patient);
                try
                {
                    if (new Accounts().AddUser(user, patient)) //RoleId 4 is for Patients
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        return RedirectToAction("Register", "Account");
                    }
                }
                catch (EmailAlreadyExistsEx ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
                catch (PhoneAlreadyExistsEx ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to register. Kindly contact the Clinic");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        
        /// <summary>
        /// Logs the user out and redirects the login-form view.
        /// </summary>
        /// <remarks>
        /// The user is logged out of his account once this action is called and all sessions are deleted.
        /// </remarks>
        /// <returns>The login view</returns>
        // GET: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Returns the Update Password form view.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpdatePasswd(string id)
        {
            var model = new PasswdViewModel()
            {
                Id = id
            };
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult UpdatePasswd(PasswdViewModel model)
        {
            Accounts account = new Accounts();
            // Link Validity
            if (account.ResetLinkValid(model.Id))
            {
                var id = account.GetIdFrmLink(model.Id);
                if(account.UpdatePassword(id, model.Password))
                {
                    ModelState.AddModelError("", "Password Reset Successfully!");
                    //return RedirectToAction("Login","Account");
                }
                else
                    ModelState.AddModelError("", "Failed To Reset Password!");
            }
            else
            {
                ModelState.AddModelError("", "Reset Link Expired!");
            }
            return View();
        }

        /// <summary>
        /// This is non action takes receivers email and activation code for sending mail with unique link
        /// </summary>
        /// <param name="emailID"></param>
        /// <param name="activationCode"></param>
        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var verifyUrl = "/Account/UpdatePasswd/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress(smtpSection.From, smtpSection.Network.UserName);
            var toEmail = new MailAddress(emailID);
            MailMessage mailMessage = new MailMessage(fromEmail, toEmail);

            mailMessage.Subject = "Reset Password";
            mailMessage.Body = "Hi,<br/><br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient {
                Host = smtpSection.Network.Host,
                Port = smtpSection.Network.Port,
                EnableSsl = smtpSection.Network.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential (smtpSection.From,smtpSection.Network.Password)
            };
            
            smtpClient.Send(mailMessage);
        }

        /// <summary>
        /// Returns to the Reset Password View in case of forget password selection.
        /// </summary>
        /// <returns></returns>
        /// GET: Account/ResetPasswd
        public ActionResult ResetPasswd()
        {
            return View();
        }
        /// <summary>
        /// This post action access non action SendVerificationLinkEmail and sends mail.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Message based on status of mail</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPasswd(ResetPasswdViewModel model)
        {
            if (new Accounts().EmailExist(model.Email))
            {
                string resetCode = Guid.NewGuid().ToString();
                try
                {
                    SendVerificationLinkEmail(model.Email, resetCode);
                    Accounts account = new Accounts();
                    Users user = new Users();
                    user.Email = model.Email;
                    var sessionInfo = account.SessionUserInfo(user);
                    ResetPasswd info = new ResetPasswd() { Id = resetCode, UserId = sessionInfo.Id, ResetRequestDateTime = DateTime.Now };
                    if (account.AddResetPasswordLinkRecord(info))
                    {
                        ModelState.AddModelError("", "Reset password link has been sent to your email id.");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Oops! Some Error Occured.\nPlease Try Again Later.");
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Account Doesn't Exist!");
            }
            return View();
        }
    }
}