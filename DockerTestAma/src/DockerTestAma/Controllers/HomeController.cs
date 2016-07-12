namespace DockerTestAma.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using Models;

    public class HomeController : Controller
    {
        public DockerClient DockerClient;

        public IActionResult Index()
        {
            DockerClient = new DockerClient();
            List<DockerContainer> containers = DockerClient.GetContainers();

            return View(containers);
        }

        [HttpPost]
        public JsonResult StartContainer()
        {
            bool status;
            try
            {
                int id = int.Parse(Request.Form["id"][0]);
                status = DockerClient.StartContainer(id);
            }
            catch (NullReferenceException e)
            {
                // TODO: Logger gebruiken.
                Response.StatusCode = (int)System.Net.HttpStatusCode.ServiceUnavailable;
                return Json("Could not start container: " + e);
            }

            Response.StatusCode = (int)System.Net.HttpStatusCode.Created;
            return Json(status);
        }

    }
}
