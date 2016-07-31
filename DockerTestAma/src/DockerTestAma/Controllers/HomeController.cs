namespace DockerTestAma.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using Models;

    public class HomeController : Controller
    {
        readonly DockerClient dockerClient = new DockerClient();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetContainers()
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.Created;
            return Json(dockerClient.GetContainers());
        }

        [HttpGet]
        public JsonResult GetNodes()
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.Created;
            return Json(dockerClient.GetNodes());
        }

        [HttpPost]
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
                LogWriter.Instance.LogMessage("Action " + actionName + " not executed on container with ID: " + id+ " Error:" + e);
            }

            Response.StatusCode = (int)System.Net.HttpStatusCode.ServiceUnavailable;
            return Json("Action not executed");
        }

        [HttpPost]
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
        /// Start a specific action on a Container by given container ID.
        /// </summary>
        /// <returns>Returns if the action is executed successfully.</returns>
        private bool StartAction(int id, string action)
        {
            return dockerClient.StartAction(id, action);
        }
    }
}
