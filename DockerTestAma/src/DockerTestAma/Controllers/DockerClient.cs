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
        /// Start a given action on Container by given ID
        /// </summary>
        /// <param name="id">ID of the Container that needs to get the action used on</param>
        /// <param name="action">The action that needs to be invoked on an container.</param>
        /// <returns>Excecuted action, true of false</returns>
        public bool StartAction(int id, string action)
        {
            try
            {
                string url = baseUrl + "/containers/" + id + "/" + action;
                Uri apiUri = new Uri(url);
                int result = HttpUtils.GetHttpWebResponseCode(apiUri);

                if (result >= 200 && result < 300)
                {
                    return true;
                }
            }
            catch (NullReferenceException)
            {
                LogWriter.Instance.LogMessage("Could not "+ action +" container: " + id);
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
