using Inventory.Models;
using System;
using System.Collections.Generic;
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
    }
}