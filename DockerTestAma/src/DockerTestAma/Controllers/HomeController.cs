namespace DockerTestAma.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using Models;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetContainers()
        {
            DockerClient dockerClient = new DockerClient();
            List<DockerContainer> containers = dockerClient.GetContainers();

            Response.StatusCode = (int)System.Net.HttpStatusCode.Created;
            return Json(containers);
        }

        [HttpPost]
        public JsonResult StartAction([FromFormAttribute]int id, [FromFormAttribute]string action)
        {
            try
            {
                //JObject kan werken.
                LogWriter.Instance.LogMessage("Start action: " + action + " on containerID: " + id);
                //Do something.
            }
            catch (NullReferenceException e)
            {
                // TODO: Logger gebruiken.
                Response.StatusCode = (int)System.Net.HttpStatusCode.ServiceUnavailable;
                return Json("Action not executed: " + e);
            }

            Response.StatusCode = (int)System.Net.HttpStatusCode.Created;
            return Json("Action excecuted.");
        }

        /// <summary>
        /// Start a Container by given container ID.
        /// </summary>
        /// <returns>The response of the start request. Also returns HTTP status code 201 or 501 depending on the result.</returns>
        [HttpPost]
        public JsonResult StartContainer(int id)
        {
            bool started;
            try
            {
                //int id = int.Parse(Request.Form["id"][0]);
                DockerClient dockerClient = new DockerClient();
                started = dockerClient.StartContainer(id);
            }
            catch (NullReferenceException e)
            {
                // TODO: Logger gebruiken.
                Response.StatusCode = (int)System.Net.HttpStatusCode.ServiceUnavailable;
                return Json("Could not start container: " + e);
            }

            Response.StatusCode = (int)System.Net.HttpStatusCode.Created;
            return Json(started);
        }
    }
}
