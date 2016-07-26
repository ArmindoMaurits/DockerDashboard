namespace DockerTestAma.Controllers
{
    using System;
    using System.Collections.Generic;
    using Models;

    public class DockerClient
    {
        private List<DockerContainer> Containers { get; set; }
        private readonly string baseUrl = "http://145.24.222.227:8080/ictlab/api";

        /// <summary>
        /// Initialze a new DockerClient, also gets all containers
        /// </summary>
        public DockerClient()
        {
            SetContainers(InitContainers());
        }

        /// <summary>
        /// Gets a list of Docker Containers
        /// </summary>
        /// <returns></returns>
        public List<DockerContainer> GetContainers()
        {
            return Containers;
        }

        /// <summary>
        /// Start a given Contaienr by given ID
        /// </summary>
        /// <param name="id">ID of the Container that needs to get started</param>
        /// <returns>Started, true of false</returns>
        public bool StartContainer(int id)
        {
            string url = baseUrl + "/containers/" + id + "/start";
            Uri apiUri = new Uri(url);
            int result = HttpUtils.GetHttpWebResponseCode(apiUri);

            if (result >= 200 && result < 300)
            {
                return true;
            }

            return false;
        }

        private List<DockerContainer> InitContainers()
        {
            List<DockerContainer> containerList;
            string url = baseUrl + "/containers/";
            Uri uri = new Uri(url);

            string response = HttpUtils.GetJsonFromUri(uri);
            containerList = JsonParser.ParseContainers(response);

            return containerList;
        }

        private void SetContainers(List<DockerContainer> containers)
        {
            Containers = containers;
        }
    }
}
