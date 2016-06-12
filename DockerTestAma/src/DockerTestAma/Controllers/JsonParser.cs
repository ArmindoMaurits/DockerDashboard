using System;
using System.Collections.Generic;
using DockerTestAma.Models;
using Newtonsoft.Json;

namespace DockerTestAma.Controllers
{
    public static class JsonParser
    {

        public static List<Container> ParseContainers(string responseData)
        {
            List<Container> containers;
            containers = JsonConvert.DeserializeObject<List<Container>>(responseData);

            return containers;
        }
    }
}
