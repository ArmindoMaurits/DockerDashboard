namespace DockerTestAma.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using Models;

    public class HomeController : Controller
    {
        readonly DockerClient dockerClient = new DockerClient();

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
            try
            {
                StartAction(id, actionName);
            }
            catch (NullReferenceException e)
            {
                LogWriter.Instance.LogMessage("Action " + actionName + " not executed: " + e);

                Response.StatusCode = (int)System.Net.HttpStatusCode.ServiceUnavailable;
                return Json("Action not executed: " + e);
            }

            Response.StatusCode = (int)System.Net.HttpStatusCode.Created;
            return Json("Action excecuted.");
        }

        [HttpPost]
        public JsonResult PostCreateContainer(string containerName, string node, string baseImage, string hostPort, string containerPort)
        {
            bool created = dockerClient.CreateNewContainer(containerName, node, baseImage, hostPort, containerPort);

            Response.StatusCode = (int)System.Net.HttpStatusCode.Created;
            return Json("Container created.");
        }


        /// <summary>
        /// Start a specific action on a Container by given container ID.
        /// </summary>
        /// <returns>Returns if the action is executed successfully.</returns>
        public bool StartAction(int id, string action)
        {
            return dockerClient.StartAction(id, action);
        }
    }
}
