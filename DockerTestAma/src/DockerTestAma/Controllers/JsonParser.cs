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

            try
            {
                containers = JsonConvert.DeserializeObject<List<Container>>(responseData);
            }
            catch (Exception)
            {
                throw;
            }

            return containers;
        }

        public static List<Image> ParseImages(string responseData)
        {
            List<Image> images;
            try
            {
                images = JsonConvert.DeserializeObject<List<Image>>(responseData);
            }
            catch (Exception)
            {
                throw;
            }

            return images;
        }

        public static Image ParseImage(string responseData)
        {
            Image image;

            try
            {
                image = JsonConvert.DeserializeObject<Image>(responseData);
            }
            catch (Exception)
            {
                throw;
            }

            return image;
        }

        public static Container ParseContainer(string responseData)
        {
            Container container;

            try
            {
                container = JsonConvert.DeserializeObject<Container>(responseData);
            }
            catch (Exception)
            {
                throw;
            }

            return container;
        }
    }
}
