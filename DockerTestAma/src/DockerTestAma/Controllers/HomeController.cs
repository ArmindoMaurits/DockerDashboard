using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using DockerTestAma.Models;
using System.IO;
using System.Net;

namespace DockerTestAma.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Container> containers = getContainers();
            
            return View();
        }

        private List<Container> getContainers()
        {
            List<Container> containerList = new List<Container>();
            String url = "http://amaurits.nl/get/containers.json";
            Console.WriteLine("url: " + url);
            string response = GetHtmlPage(url);
            Console.WriteLine("response: " + response);

            //TODO: Parse json
            return containerList;
        }

        private List<Image> getImages()
        {
            List<Image> imageList = new List<Image>();
            String url = "http://amaurits.nl/get/images.json";

            string response = GetHtmlPage(url);
            return imageList;
        }

        static string GetHtmlPage(string strURL)
        {
            String strResult;
            WebResponse objResponse;

            WebRequest objRequest = HttpWebRequest.Create(strURL);
            objResponse = objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                strResult = sr.ReadToEnd();
                sr.Close();
            }
            return strResult;
        }

        static string parseJson(string data)
        {
            string json = "";

            return json;
        }
    }
}
