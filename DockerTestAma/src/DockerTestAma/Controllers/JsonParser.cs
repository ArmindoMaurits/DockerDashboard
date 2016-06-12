using System;
using System.Collections.Generic;
using DockerTestAma.Models;
using Newtonsoft.Json;

namespace DockerTestAma.Controllers
{
    public static class JsonParser
    {
        public static List<DockerContainer> ParseContainers(string responseData)
        {
            List<DockerContainer> containers = new List<DockerContainer>();
            try
            {
                containers = JsonConvert.DeserializeObject<List<DockerContainer>>(responseData);
            }
            catch (JsonSerializationException e)
            {
                throw new JsonSerializationException("Cannot deserialize JSON object: ", e);
            }
            return containers;

        }
    }
}
