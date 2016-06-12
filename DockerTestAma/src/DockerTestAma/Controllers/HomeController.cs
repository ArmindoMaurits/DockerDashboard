using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using DockerTestAma.Models;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace DockerTestAma.Controllers
{
    public class HomeController : Controller
    {
        public DockerClient DockerClient;

        public IActionResult Index()
        {
            DockerClient = new DockerClient();
            List<Container> containers = DockerClient.Containers;

            return View(containers);
        }

        [HttpPost]
        public JsonResult StartContainer()
        {
            int id = 0;
            string status = string.Empty;
            try
            {
                id = int.Parse(Request.Form["id"][0]);
                status = DockerClient.StartContainer(id);
            }
            catch (NullReferenceException e)
            {
                return Json("Could not start container: " + e);
            }
            
            return Json(status);
        }

    }
}
