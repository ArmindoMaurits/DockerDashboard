using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using DockerTestAma.Models;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Data;

namespace DockerTestAma.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            List<Container> containers = GetContainers();
            List<Image> imageList = GetImages();

            return View(containers);
        }

        private List<Container> GetContainers() {
            List<Container> containerList;
            Uri uri = new Uri(@"http://amaurits.nl/get/containers.json");

            string response = GetHtmlPage(uri);

            containerList = ParseContainersJson(response);

            return containerList;
        }

        private List<Image> GetImages() {
            List<Image> imageList = new List<Image>();
            Uri uri = new Uri(@"http://amaurits.nl/get/images.json");

            string response = GetHtmlPage(uri);
            return imageList;
        }

        static string GetHtmlPage(Uri uri) {
            WebRequest webRequest = WebRequest.Create(uri);
            WebResponse response = webRequest.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string responseData = streamReader.ReadToEnd();
            streamReader.Close();

            return responseData;
        }

        static List<Container> ParseContainersJson(string responseData) {
            List<Container> containers;

            containers = JsonConvert.DeserializeObject<List<Container>>(responseData);

            return containers;
        }

    }
}
