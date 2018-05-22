﻿using BetEventScanner.DogonWeb.Models;
using BetEventScanner.DogonWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using BetEventScanner.DataAccess;

namespace BetEventScanner.DogonWeb.Controllers
{
    public class HomeController : Controller
    {
        private ITennisService _tennisService;
        private ICalculateService _calculateService;
        public HomeController()
        {
            _tennisService = new TennisBetService(new UnitOfWork());
            _calculateService = new CalculateService(new UnitOfWork());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Tennis()
        {
            ViewBag.Message = "Tennis page.";

            return View();
        }

        [HttpPost]
        public JsonResult SendData(BetInfoDto betInfo)
        {

            var result = _tennisService.ProcessBetLine(betInfo);
            return Json(new { isr = "ds" });
        }

        public ActionResult List()
        {

            return View();
        }

        public ActionResult ReadBetList([DataSourceRequest]DataSourceRequest request)
        {
            var betsList = _tennisService.GetBetsList();
            return Json(betsList.ToDataSourceResult(request));
        }
    }
}