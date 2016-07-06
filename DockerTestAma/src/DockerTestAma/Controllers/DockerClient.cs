namespace DockerTestAma.Controllers
{
    using System;
    using System.Collections.Generic;
    using Models;

    public class DockerClient
    {
        private List<DockerContainer> containers { get; set; }
        private readonly string baseUrl = "http://145.24.222.227:8080/ictlab/api";

        public DockerClient()
        {
            containers = InitContainers();
        }

        public List<DockerContainer> GetContainers()
        {
            return this.containers;
        }

        private List<DockerContainer> InitContainers()
        {
            List<DockerContainer> containerList;
            string url = baseUrl + "/containers/";
            Uri uri = new Uri(url);

            string response = HttpUtils.GetHtmlPage(uri);
            containerList = JsonParser.ParseContainers(response);

            return containerList;
        }

        public string StartContainer(int id)
        {
            string url = baseUrl + "/containers/" + id + "/start";
            Uri apiUri = new Uri(url);
            int result = HttpUtils.GetHttpWebResponseCode(apiUri);

            if (result >= 200 && result < 300)
            {
                return "Started container with code:" + result;
            }
            else
            {
                return "Could not start container";
            }

        }
    }
}
