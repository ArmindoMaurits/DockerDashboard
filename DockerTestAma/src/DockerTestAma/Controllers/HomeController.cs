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

        public JsonResult GetContainers()
        {
            List<DockerContainer> containers = dockerClient.GetContainers();

            Response.StatusCode = (int)System.Net.HttpStatusCode.Created;
            return Json(containers);
        }

        [HttpPost]
        public IActionResult PostAction(string actionName, int id)
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
