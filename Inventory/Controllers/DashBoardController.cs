using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Index()
        {
            BaseEquipment baseEquipment = new BaseEquipment();
            List<BaseEquipment> equipmentList = baseEquipment.ListEquipment();
            ViewBag.EquipmentList = equipmentList;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string btnSubmit, FormCollection formCollection)
        {
            BaseEquipment baseEquipment = new BaseEquipment();
            int resultStatus = 0;
            if (btnSubmit == "Save" || btnSubmit == "Update")
            {
                baseEquipment.EquipmentName = formCollection["txtEquipmentName"].ToString();
                baseEquipment.Quantity = Convert.ToInt32(formCollection["quantity"].ToString());
                baseEquipment.ReceiveDate = Convert.ToDateTime(formCollection["receiveDate"].ToString());
            }
            if (btnSubmit == "Save")
            {
                baseEquipment.EntryDate = Convert.ToDateTime(formCollection["entryDate"].ToString());
                resultStatus = baseEquipment.saveEquipment();
                if (resultStatus > 0)
                {
                    TempData["OutMessage"] = "Equipment saved successfully.";
                }
                else
                {
                    TempData["OutMessage"] = "Equipment can't be saved. Please try again.";
                }
            }
            else if (btnSubmit == "Update")
            {
                baseEquipment.EquipmentId = Convert.ToInt32(formCollection["hdnEquipmentId"].ToString());
                resultStatus = baseEquipment.updateEquipment();
                if (resultStatus > 0)
                {
                    TempData["OutMessage"] = "Equipment updated successfully.";
                }
                else
                {
                    TempData["OutMessage"] = "Equipment can't be updated. Please try again.";
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult NewEquipmentAssignment()
        {
            BaseEquipment baseEquipment = new BaseEquipment();
            List<(int, string)> customerList = baseEquipment.getCustomerList();
            ViewBag.CustomerList = customerList;
            ViewBag.EquipmentList = baseEquipment.ListEquipment();
            return View(); 
        }
        [HttpPost]
        public ActionResult NewEquipmentAssignment(FormCollection formCollection)
        {
            int CustomerId = Convert.ToInt32(formCollection["CustomerName"].ToString());
            int EquipmentId = Convert.ToInt32(formCollection["EquipmentName"].ToString());
            int Quantity = Convert.ToInt32(formCollection["Quantity"].ToString());
            BaseEquipment baseEquipment = new BaseEquipment();
            int result = baseEquipment.SaveEquipmentAssignment(CustomerId, EquipmentId, Quantity);
            if (result > 0)
            {
                TempData["OutMessage"] = "Equipment assigned successfully";
                return Redirect(Url.Action("Index", "DashBoard"));
            }
            else
            {
                TempData["OutMessage"] = "Equipment assignment failed";
                return View();
            }
        }

        [HttpGet]
        public ActionResult UpdateEquipmentAssignment(int id)
        {
            BaseEquipment baseEquipment = new BaseEquipment();
            List<(int, string)> customerList = baseEquipment.getCustomerList();
            ViewBag.CustomerList = customerList;
            List<BaseEquipment> equipmentList = baseEquipment.ListEquipment();
            ViewBag.EquipmentList = equipmentList;
            DataTable dt = baseEquipment.ListAssignedEquipment();
            DataRow row = dt.AsEnumerable().FirstOrDefault(r => r.Field<int>("AssignmentId") == id);
            ViewBag.CustomerId = row.Field<int>("CustomerId");
            ViewBag.EquipmentId = row.Field<int>("EquipmentId");
            ViewBag.Quantity = row.Field<int>("EquiCount");
            ViewBag.AssignmentId = id;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateEquipmentAssignment(FormCollection formCollection)
        {
            int CustomerId = Convert.ToInt32(formCollection["CustomerName"].ToString());
            int EquipmentId = Convert.ToInt32(formCollection["EquipmentName"].ToString());
            int Quantity = Convert.ToInt32(formCollection["Quantity"].ToString());
            int AssignmentId = Convert.ToInt32(formCollection["hdnAssignmentId"].ToString());
            BaseEquipment baseEquipment = new BaseEquipment();
            int result = baseEquipment.UpdateEquipmentAssignment(CustomerId, EquipmentId, Quantity, AssignmentId);
            if (result > 0)
            {
                TempData["OutMessage"] = "Assignment updated successfully";
                return Redirect(Url.Action("Index", "DashBoard"));
            }
            else
            {
                TempData["OutMessage"] = "Assignment update failed";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAssignment(int id)
        {
            BaseEquipment baseEquipment = new BaseEquipment();
            int result = baseEquipment.DeleteEquipmentAssignment(id);
            if (result > 0) {
                TempData["OutMessage"] = "Assignment deleted successfully";
            }
            else
            {
                TempData["OutMessage"] = "Assignment deletion failed";
            }
            return Redirect(Url.Action("Index", "DashBoard"));
        }
    }
}