﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testumium.Domain.Data;
using Testumium.Domain.Models.Domain.Notifications;
using Testumium.Domain.Models.Domain.Tests;

namespace Testumium.Domain.Services
{
    public class TestService : ITestService
    {
        private TestumiumEntities repository;
        private INotificationService notificationService;
        public TestService(TestumiumEntities repository, INotificationService notificationService)
        {
            this.repository = repository;
            this.notificationService = notificationService;
        }

        public void CreateTest(Data.DbTest test)
        {
            test.DateCreated = DateTime.Now;
            test.DateUpdated = DateTime.Now;

            this.repository.DbTests.Add(test);
            this.repository.SaveChanges();

            if (test.DbTestItems != null && test.DbTestItems.Count > 0)
            {
                foreach (var item in test.DbTestItems)
                {
                    item.TestId = test.Id;
                    this.repository.DbTestItems.Add(item);
                }
                this.repository.SaveChanges();
            }

            if (test.TestType == TestType.Manual)
            {
                notificationService.SendEmailToUserRequestManualTest(test);
            }
        }
    }
}