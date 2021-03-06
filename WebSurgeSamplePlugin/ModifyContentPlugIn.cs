﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSurge;

namespace WebSurgeSamplePlugin
{
    public class ModifyContentPlugIn : IWebSurgeExtensibility
    {
        public bool OnBeforeRequestSent(HttpRequestData data)
        {
            var header = new HttpRequestHeader
            {
                Name = "x-request-time",
                Value = DateTime.UtcNow.ToString("o")
            };
            data.Headers.Add(header);


            if (!string.IsNullOrEmpty(data.RequestContent))
            {
                // do something here to get your unique value
                var userId = new Random().Next().ToString();

                // then embed it into the content
                data.RequestContent = data.RequestContent.Replace("##UserId##", "USER_" + userId);
            }

            return true;
        }

        public void OnAfterRequestSent(HttpRequestData data)
        {
        }

        public bool OnLoadTestStarted(IList<HttpRequestData> requests)
        {
            return true;
        }

        public void OnLoadTestCompleted(IList<HttpRequestData> results, int timeTakenForTestMs)
        {
        }
    }
}
