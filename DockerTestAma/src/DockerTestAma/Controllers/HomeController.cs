namespace DockerTestAma.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using Models;

    public class HomeController : Controller
    {
        private readonly DockerClient dockerClient = new DockerClient();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        /// <summary>
        /// Gets a JSON Array of DockerContainers.
        /// </summary>
        /// <returns>A list of DockerContainers.</returns>
        public JsonResult GetContainers()
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.Created;

            return Json(dockerClient.GetContainers());
        }

        [HttpGet]
        /// <summary>
        /// Gets a JSON Array of Node ip-addresses as strings.
        /// </summary>
        /// <returns>A list of strings (Docker node IP-address).</returns>
        public JsonResult GetNodes()
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.Created;

            return Json(dockerClient.GetNodes());
        }

        [HttpPost]
        /// <summary>
        /// Post an action to a specific container, by given action and target container ID.
        /// </summary>
        /// <param name="actionName">Name of the action, like "start", "stop", "restart", "delete".</param>
        /// <param name="id">ID of the target container where the action needs be performed on.</param>
        /// <returns>JSON return result and 201 or 503 HTTPStatusCode</returns>
        /// <example>PostAction("start", 60)</example>
        public JsonResult PostAction(string actionName, int id)
        {
            bool startedAction = false;

            try
            {
                startedAction = StartAction(id, actionName);

                if (startedAction)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Created;
                    return Json("Action excecuted.");
                }
            }
            catch (NullReferenceException e)
            {
                LogWriter.Instance.LogMessage("Action " + actionName + " not executed on container with ID: " + id + " Error:" + e);
            }

            Response.StatusCode = (int)System.Net.HttpStatusCode.ServiceUnavailable;
            return Json("Action not executed");
        }

        [HttpPost]
        /// <summary>
        /// Posts a create container request to the docker client. Returns true or false if the container has been created.
        /// </summary>
        /// <param name="containerName">Name of the container</param>
        /// <param name="node">IP-address of the disired node</param>
        /// <param name="baseImage">Name of the base image</param>
        /// <param name="hostPort">Port of the new container on the host</param>
        /// <param name="containerPort">Port of the new container inside the container</param>
        /// <returns>Returns true or false if the container has been created.</returns>
        public JsonResult PostCreateContainer(string containerName, string node, string baseImage, string hostPort, string containerPort)
        {
            bool created = dockerClient.CreateNewContainer(containerName, node, baseImage, hostPort, containerPort);

            if (created)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Created;
                return Json("Container created.");
            }

            Response.StatusCode = (int)System.Net.HttpStatusCode.ServiceUnavailable;
            return Json("Container could not be created.");
        }

        /// <summary>
        /// Starts a specific action on a Container by given container ID.
        /// </summary>
        /// <returns>Returns if the action is executed successfully.</returns>
        private bool StartAction(int id, string action)
        {
            return dockerClient.StartAction(id, action);
        }
    }
}
