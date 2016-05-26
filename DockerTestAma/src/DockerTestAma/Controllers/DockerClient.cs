using System;

using System.Collections.Generic;
using DockerTestAma.Models;
using System.Collections;

namespace DockerTestAma.Controllers
{
    public class DockerClient
    {
        public List<Container> containers;
        public List<Image> images;
        public string baseUrl;

        public DockerClient()
        {
            containers = GetContainers();
            images = GetImages();
            baseUrl = System.Environment.GetEnvironmentVariable("ApiAddress");
        }

        public List<Container> GetContainers()
        {
            List<Container> containerList;
            Uri uri = new Uri(@"http://amaurits.nl/get/containers.json");
            ////Uri uri = new Uri(@"http://thomasmaurer.nl/docker/containers/get");

            string response = Utils.GetHtmlPage(uri);
            containerList = JsonParser.ParseContainers(response);

            return containerList;
        }

        public Container GetContainer(int id)
        {
            Container container;
            Uri uri = new Uri(@"http://thomasmaurer.nl/docker/containers/" + id);
            string response = Utils.GetHtmlPage(uri);

            container = JsonParser.ParseContainer(response);

            return container;
        }

        public List<Image> GetImages()
        {
            List<Image> imageList;
            Uri uri = new Uri(@"http://amaurits.nl/get/images.json");

            string response = Utils.GetHtmlPage(uri);

            imageList = JsonParser.ParseImages(response);
            return imageList;
        }

        public Image GetImage(string id)
        {
            Image image;
            Uri uri = new Uri(@"http://thomasmaurer.nl/docker/images/" + id);
            string response = Utils.GetHtmlPage(uri);

            image = JsonParser.ParseImage(response);

            return image;
        }

        public string StartContainer(int id)
        {
            ////http://thomasmaurer.nl/docker/containers/id/start
            ////Env var: ApiAddress
            ////Env value: http://145.24.222.227:8080/
            ////Env value: http://145.24.222.227:8080/ictlab/resources/

            int result;

            string apiUrl = baseUrl + "docker/containers/" + id + "/start";
            Uri apiUri = new Uri(apiUrl);
            result = Utils.GetHttpWebResponseCode(apiUri);

            if (result >= 200 && result < 300)
            {
                return "Started container with code:" + result;
            }else
            {
                return "Could not start container";
            }

        }
    }
}
