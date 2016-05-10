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
            List<Container> containers = getContainers();

            return View();
        }

        private List<Container> getContainers() {
            List<Container> containerList = new List<Container>();
            Uri uri = new Uri(@"http://amaurits.nl/get/containers.json");

            string response = getHtmlPage(uri);

            containerList = parseContainersJson(response);

            return containerList;
        }

        private List<Image> getImages() {
            List<Image> imageList = new List<Image>();
            Uri uri = new Uri(@"http://amaurits.nl/get/images.json");

            string response = getHtmlPage(uri);
            return imageList;
        }

        static string getHtmlPage(Uri uri) {
            WebRequest webRequest = WebRequest.Create(uri);
            WebResponse response = webRequest.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string responseData = streamReader.ReadToEnd();
            streamReader.Close();

            return responseData;
        }

        static List<Container> parseContainersJson(string responseData) {
            List<Container> containers = new List<Container>();

            containers = JsonConvert.DeserializeObject<List<Container>>(responseData);

            return containers;
        }

    }
}
