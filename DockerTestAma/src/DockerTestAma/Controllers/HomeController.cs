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
        public DockerClient dockerClient;

        public IActionResult Index()
        {
            dockerClient = new DockerClient();
            List<Container> containers = dockerClient.containers;

            return View(containers);
        }

        [HttpPost]
        public JsonResult StartContainer()
        {
            int id = 0;
            try
            {
                id = int.Parse(Request.Form["id"][0]);
                dockerClient.StartContainer(id);
            }
            catch (Exception)
            {
                throw;
            }
            
            return Json("Started container " + id);
        }

    }
}
