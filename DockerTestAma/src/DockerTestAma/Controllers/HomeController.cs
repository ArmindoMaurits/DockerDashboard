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

            return View(containers);
        }

        private List<Container> GetContainers() {
            List<Container> containerList;
            Uri uri = new Uri(@"http://amaurits.nl/get/containers.json");
            //Uri uri = new Uri(@"http://thomasmaurer.nl/docker/containers/get");

            string response = GetHtmlPage(uri);
            containerList = ParseContainersJson(response);

            return containerList;
        }

        private List<Image> GetImages() {
            List<Image> imageList;
            Uri uri = new Uri(@"http://amaurits.nl/get/images.json");

            string response = GetHtmlPage(uri);

            imageList = ParseImagesJson(response);
            return imageList;
        }

        private List<Image> ParseImagesJson(string response) {
            List<Image> images;
            try {
                images = JsonConvert.DeserializeObject<List<Image>>(response);
            } catch (Exception) {
                throw;
            }
            
            return images;
        }

        static string GetHtmlPage(Uri uri) {
            string responseData = "";

            try {
                WebRequest webRequest = WebRequest.Create(uri);
                WebResponse response = webRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(response.GetResponseStream())) {
                    responseData = streamReader.ReadToEnd();
                }

            } catch (Exception) {
                throw;
            }


            return responseData;
        }

        static List<Container> ParseContainersJson(string responseData) {
            List<Container> containers;

            try {
                containers = JsonConvert.DeserializeObject<List<Container>>(responseData);
            } catch (Exception) {
                throw;
            }
            
            return containers;
        }

    }
}
