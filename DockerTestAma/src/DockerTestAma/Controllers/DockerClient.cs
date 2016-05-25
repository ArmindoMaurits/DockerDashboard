using System;

using System.Collections.Generic;
using DockerTestAma.Models;

namespace DockerTestAma.Controllers
{
    public class DockerClient
    {
        public List<Container> containers;
        public List<Image> images;

        public DockerClient()
        {
            containers = GetContainers();
            images = GetImages();
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

        public void StartContainer(int id)
        {
            ////http://thomasmaurer.nl/docker/containers/id/start
            ////Env var: ApiAddress
            ////Env value: http://thomasmaurer.nl/

            string baseUrl = System.Environment.GetEnvironmentVariable("ApiAddress");
            string apiUrl = baseUrl + "docker/containers/" + id + "/start";
            ////WebRequest naar docker/containers/id/start
            ////returned responseOK of NoContent.
        }
    }
}
