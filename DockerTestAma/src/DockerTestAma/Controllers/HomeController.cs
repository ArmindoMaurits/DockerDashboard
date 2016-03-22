using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using DockerTestAma.Models;

namespace DockerTestAma.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public string postToGetContainers(Container[] containerArray)
        {
            List<Container> containers = getContainers(containerArray);
            return "posted OK";
        }

        private List<Container> getContainers(Container[] containerArray)
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
