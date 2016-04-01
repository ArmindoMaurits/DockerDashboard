using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using DockerTestAma.Models;
using Newtonsoft.Json;

namespace DockerTestAma.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public JsonResult postToGetContainers([FromBody]Object containers)
        {
            //List<Container> containerList = getContainers(containers);

            return Json("OK");
        }

        private List<Container> getContainers(string[] containerArray)
        {
            List<Container> containerList = new List<Container>();
            String url = "http://amaurits.nl/get/containers.json";

            return containerList;
        }

        private List<Image> getImages()
        {
            List<Image> imageList = new List<Image>();
            String url = "http://amaurits.nl/get/images.json";

            return imageList;
        }
    }
}
