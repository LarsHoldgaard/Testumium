﻿using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testumium.Domain.Data;
using Testumium.Domain.Models.Domain.Notifications;
using Testumium.Domain.Models.Domain.Tests;
using Testumium.Domain.Services;
using Testumium.Helpers;
using Testumium.Models;

namespace Testumium.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(HomeController));
        private ITestService testService;
        private INotificationService notificationService;
        public HomeController()
        {
            IEmailSender sender = new SystemNetMailSender(Configurations.SMTPServer, Configurations.SMTPPort, 
                Configurations.SMTPUsername, Configurations.SMTPPassword, Configurations.SMTPEnableSsl);
            this.notificationService = new NotificationService(sender);
            this.testService = new TestService(new TestumiumEntities(), this.notificationService);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateTest(string query = "")
        {
            ViewBag.Url = query;
            return View();
        }

        public ActionResult Prices()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View(new ContactFormViewModel());
        }

        [HttpPost]
        public ActionResult Contact(ContactFormViewModel model)
        {
            try
            {
                this.notificationService.GetHelpEmail(model.ContactName, model.Email, model.Phone, model.Company, model.Comments);
                TempData.Add("SuccessMessage", "Your question is sent");
            }
            catch (Exception ex)
            {
                TempData.Add("ErrorMessage", ex.Message);
                logger.Error(ex);
            }

            return RedirectToAction("Contact");
        }

        [HttpGet]
        public ActionResult CreateOwnTest(string domain)
        {
            ViewBag.Domain = domain;
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreateOwnTest(string domain, TestType testType, string websiteUrl, string testList)
        {
            Session.Add("TestList", testList);
            return RedirectToAction("TellUsAboutYou", new { domain = domain, testType = testType, websiteUrl = websiteUrl });
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult TellUsAboutYou(string domain, TestType testType, string websiteUrl)
        {
            CreateTestViewModel model = new CreateTestViewModel();
            model.Domain = domain;
            model.TestType = testType;
            model.TestList = (string)Session["TestList"];
            model.WebsiteUrl = websiteUrl;
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TellUsAboutYou(CreateTestViewModel model)
        {
            try
            {
                DbTest test = new DbTest();
                test.Name = string.Empty;
                test.ContactName = model.ContactName;
                test.Domain = string.IsNullOrEmpty(model.Domain) ? string.Empty : model.Domain;
                test.Email = model.Email;
                test.Phone = model.Phone;
                test.Company = model.Company;
                test.WebsiteUrl = model.WebsiteUrl;
                test.Comments = model.Comments;
                test.TestType = model.TestType;
                test.CallTime = model.CallTime;
                if (!string.IsNullOrEmpty(model.TestList))
                {
                    TestItemViewModel[] testItems = JsonConvert.DeserializeObject<TestItemViewModel[]>(model.TestList);
                    foreach (var item in testItems)
                    {
                        test.DbTestItems.Add(new DbTestItem() { Description = item.Description });
                    }
                }
                this.testService.CreateTest(test);
                return RedirectToAction("CreateTestCompleted", new { type = model.TestType.ToString() });
            }
            catch (Exception ex)
            {
                TempData.Add("ErrorMessage", ex.Message);
                logger.Error(ex);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateTestCompleted(TestType type)
        {
            CreateTestViewModel model = new CreateTestViewModel();
            model.TestType = type;
            return View(model);
        }

        public JsonResult CheckWebsite(string url)
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    url = url.ToLower();
                    if (!url.StartsWith("http://")) url = "http://" + url;
                    string data = client.DownloadString(url);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
    }
}