using System;
using System.IO;
using System.Net;

namespace DockerTestAma.Controllers
{
    public static class Utils
    {
        public static string GetHtmlPage(Uri uri)
        {
            string responseData = String.Empty;

            try
            {
                WebRequest webRequest = WebRequest.Create(uri);
                WebResponse response = webRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseData = streamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseData;
        }
    }
}
