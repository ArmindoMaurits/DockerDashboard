namespace DockerTestAma.Controllers
{
    using System;
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Client which handles Docker tasks and acts as a business logic level between a controller and a Docker Dashboard API.
    /// </summary>
    public class DockerClient
    {
        /// <summary>
        /// Environment variable used so that the Docker dashboard API address can be got. Docker containers can use this, so we can use different addresses.
        /// </summary>
        private readonly string baseUrl = Environment.GetEnvironmentVariable("DockerDashboardApiAddress");

        /// <summary>
        /// The total list of Docker containers
        /// </summary>
        private List<DockerContainer> containers;

        /// <summary>
        /// The total list of Docker node IP-addresses
        /// </summary>
        private List<string> nodes;

        /// <summary>
        /// Initializes a new instance of the DockerClient, also gets all containers
        /// </summary>
        public DockerClient()
        {
            SetContainers(InitContainers());
            SetNodes(InitNodes());
        }

        /// <summary>
        /// Gets a list of Docker Containers
        /// </summary>
        /// <returns>A list of Docker Containers</returns>
        public List<DockerContainer> GetContainers()
        {
            return containers;
        }

        /// <summary>
        /// Gets a list of Docker Node IP-adresses.
        /// </summary>
        /// <returns>A list of Docker Node IP-adresses</returns>
        public List<string> GetNodes()
        {
            return nodes;
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
                LogWriter.Instance.LogMessage("Could not " + action + " container: " + id);
            }

            return false;
        }

        /// <summary>
        /// Start a JSON POST request to create a new container
        /// </summary>
        /// <returns>Created, true of false</returns>
        public bool CreateNewContainer(string containerName, string node, string baseImage, string hostPort, string containerPort)
        {
            object jsonObject = new { baseImage, containerName, containerPort, hostPort, node };

            if (jsonObject.Equals(null))
            {
                return false;
            }

            return HttpUtils.PostJsonObjectAtUri(new Uri("http://145.24.222.227:8080/ictlab/api/containers"), jsonObject);
        }

        /// <summary>
        /// Initialize our List of Docker Containers from our Front-end API.
        /// </summary>
        /// <returns>Returns a list of Docker Containers</returns>
        private List<DockerContainer> InitContainers()
        {
            List<DockerContainer> containerList;
            string url = baseUrl + "/containers/";
            Uri uri = new Uri(url);

            string response = HttpUtils.GetJsonFromUri(uri);
            containerList = JsonParser.ParseContainers(response);

            return containerList;
        }

        /// <summary>
        /// Initializes a List of strings (docker node IP-addresses) from our Front-end API.
        /// </summary>
        /// <returns>A list of string IP-addresses</returns>
        private List<string> InitNodes()
        {
            List<string> nodesList;
            string url = baseUrl + "/nodes/";
            Uri uri = new Uri(url);

            string response = HttpUtils.GetJsonFromUri(uri);
            nodesList = JsonParser.ParseNodes(response);

            return nodesList;
        }

        /// <summary>
        /// Sets our Containers variable with given list of DockerContainers
        /// </summary>
        /// <param name="containers">A list of Docker Containers</param>
        private void SetContainers(List<DockerContainer> containers)
        {
            this.containers = containers;
        }

        /// <summary>
        /// Sets our Nodes variable with given list of strings (IP-addresses).
        /// </summary>
        /// <param name="nodes">A list of strings (IP-addresses)</param>
        private void SetNodes(List<string> nodes)
        {
            this.nodes = nodes;
        }
    }
}
