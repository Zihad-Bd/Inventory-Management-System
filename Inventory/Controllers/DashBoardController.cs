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
            if (btnSubmit == "Save")
            {
                baseEquipment.EquipmentName = formCollection["txtEquipmentName"].ToString();
                baseEquipment.Quantity = Convert.ToInt32(formCollection["quantity"].ToString());
                baseEquipment.EntryDate = Convert.ToDateTime(formCollection["entryDate"].ToString());
                baseEquipment.ReceiveDate = Convert.ToDateTime(formCollection["receiveDate"].ToString());
                resultStatus = baseEquipment.saveEquipment();
            }
            List<BaseEquipment> equipmentList = baseEquipment.ListEquipment();
            ViewBag.EquipmentList = equipmentList;
            if (resultStatus > 0)
            {
                TempData["OutMessage"] = "Equipment saved successfully.";
            }
            return RedirectToAction("Index");
        }
    }
}