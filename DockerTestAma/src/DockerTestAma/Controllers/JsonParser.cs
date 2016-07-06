namespace DockerTestAma.Controllers
{
    using System.Collections.Generic;
    using Models;
    using Newtonsoft.Json;

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
                // TODO: Create singleton class, for logging
                // TODO: Don't throw the exception here, but log it!
                throw new JsonSerializationException("Cannot deserialize JSON object: ", e);
            }
            return containers;

        }
    }
}
