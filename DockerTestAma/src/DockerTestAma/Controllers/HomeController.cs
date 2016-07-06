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
            string status = string.Empty;
            try
            {
                int id = int.Parse(Request.Form["id"][0]);
                status = DockerClient.StartContainer(id);
            }
            catch (NullReferenceException e)
            {
                // TODO: Logger gebruiken.
                // TODO: Status code returnen
                // Response.StatusCode = (int)System.Net.HttpStatusCode.Created
                // Response.StatusCode = (int)System.Net.HttpStatusCode.ServiceUnavailable
                return Json("Could not start container: " + e);
            }

            // TODO: Status code returnen
            return Json(status);
        }

    }
}
