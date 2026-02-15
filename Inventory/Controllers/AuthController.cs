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
            foreach(BaseMember member in memberList)
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
    }
}