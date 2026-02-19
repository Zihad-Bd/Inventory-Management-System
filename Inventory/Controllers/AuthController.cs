using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string txtUserName, string txtPassword)
        {
            BaseMember baseMember = new BaseMember();
            // Validate via datatable is case-insensitive
            //DataTable dt = baseMember.ValidateMemberAsDataTable(txtUserName, txtPassword);
            //if (dt.Rows.Count > 0)
            //{
            //    Session["UserName"] = txtUserName;
            //    return Redirect(Url.Action("About", "Home"));

            //}
            List<BaseMember> memberList = baseMember.ValidateMemberAsList(txtUserName, txtPassword);
            bool statusValid = false;
            foreach (BaseMember member in memberList)
            {
                if (member.Name == txtUserName && member.Password == txtPassword)
                {
                    statusValid = true;
                    break;
                }
            }
            if (statusValid)
            {
                Session["UserName"] = txtUserName;
                //return Redirect(Url.Action("About", "Home"));

            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["UserName"] != null)
            {
                Session.Remove("UserName");
            }
            return Redirect(Url.Action("Login", "Auth"));
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string txtUserName, string txtPassword, string txtRetypePassword)
        {
            BaseMember member = new BaseMember();
            bool userExist = member.UserExist(txtUserName);
            if (userExist)
            {
                if (txtPassword == txtRetypePassword)
                {
                    // Update password in database
                    bool result = member.UpdatePassword(txtUserName, txtPassword);
                    if (result)
                    {
                        TempData["Message"] = "Password updated successfully.";
                        return Redirect(Url.Action("Login", "Auth"));
                    }
                    else
                    {
                        ViewBag.Message = "Failed to update password. Please try again.";
                    }
                }
                else
                {
                    ViewBag.Message = "Passwords do not match.";
                }
            }
            else
            {
                ViewBag.Message = "User does not exist.";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string txtUserName, string txtPassword)
        {
            BaseMember member = new BaseMember();
            bool userExist = member.UserExist(txtUserName);
            if (!userExist)
            {
                // Save new user in database
                bool result = member.SaveUser(txtUserName, txtPassword);
                if (result)
                {
                    TempData["Message"] = "Registration successful. Please log in.";
                    return Redirect(Url.Action("Login", "Auth"));
                }
                else
                {
                    ViewBag.Message = "Failed to register. Please try again.";
                }
            }
            else
            {
                ViewBag.Message = "Username already exists. Please choose a different username.";
            }
            return View();
        }
    }
}