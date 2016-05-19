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
            List<Container> containers = dockerClient.GetContainers();

            return View(containers);
        }

    }
}
