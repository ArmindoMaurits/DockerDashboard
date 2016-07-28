namespace DockerTestAma.Controllers
{
    using System.Collections.Generic;
    using Models;
    using Newtonsoft.Json;

    public static class JsonParser
    {
        /// <summary>
        /// Parse a JSON response into a list of Docker containers
        /// </summary>
        /// <param name="responseData">JSON response</param>
        /// <returns>A list of Docker containers</returns>
        public static List<DockerContainer> ParseContainers(string responseData)
        {
            List<DockerContainer> containers = new List<DockerContainer>();
            try
            {
                containers = JsonConvert.DeserializeObject<List<DockerContainer>>(responseData);
            }
            catch (JsonSerializationException e)
            {
                LogWriter.Instance.LogMessage("Cannot deserialize JSON object: " + e);
            }

            return containers;
        }

        /// <summary>
        /// Parse all the nodes from a web response data.
        /// </summary>
        /// <param name="responseData">The response data of a JSON response</param>
        /// <returns>A list of IP-addresses of the Nodes</returns>
        public static List<string> ParseNodes(string responseData)
        {
            List<string> nodes = new List<string>();

            try
            {
                dynamic jsonObjectArray = JsonConvert.DeserializeObject(responseData);

                foreach (var jsonObject in jsonObjectArray)
                {
                    nodes.Add( (string)jsonObject.name);
                }
            }
            catch (System.Exception e)
            {
                LogWriter.Instance.LogMessage("Cannot get node IP adresses from response: " + e);
            }

            return nodes;
        }
    }
}
