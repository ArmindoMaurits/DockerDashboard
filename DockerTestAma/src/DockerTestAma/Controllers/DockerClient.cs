using System;

using System.Collections.Generic;
using DockerTestAma.Models;
using System.Collections;

namespace DockerTestAma.Controllers
{
    public class DockerClient
    {
        public List<Container> Containers { get; set; }
        public string BaseUrl = "http://145.24.222.227:8080/ictlab/api";

        public DockerClient()
        {
            Containers = InitContainers();
        }

        public List<Container> InitContainers()
        {
            List<Container> containerList;
            string url = BaseUrl + "/containers/";
            Uri uri = new Uri(url);

            string response = Utils.GetHtmlPage(uri);
            containerList = JsonParser.ParseContainers(response);

            return containerList;
        }

        public string StartContainer(int id)
        {
            int result;

            string url = BaseUrl + "/containers/" + id + "/start";
            Uri apiUri = new Uri(url);
            result = Utils.GetHttpWebResponseCode(apiUri);

            if (result >= 200 && result < 300)
            {
                return "Started container with code:" + result;
            }
            else
            {
                return "Could not start container";
            }

        }
    }
}
