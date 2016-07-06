namespace DockerTestAma.Controllers
{
    using System;
    using System.IO;
    using System.Net;

    public static class HttpUtils
    {
        public static string GetHtmlPage(Uri uri)
        {
            string responseData = string.Empty;

            try
            {
                WebRequest webRequest = WebRequest.Create(uri);
                WebResponse response = webRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseData = streamReader.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                // TODO: Logger gebruiken.
                Console.WriteLine("Could not get webURL: " + GetWebResponseCode((HttpWebResponse)we.Response));
            }
            return responseData;
        }

        private static int GetWebResponseCode(HttpWebResponse response)
        {
            return (int)response.StatusCode;
        }

        public static int GetHttpWebResponseCode(Uri uri)
        {
            int responseCode = 0;
            try
            {
                WebRequest webRequest = WebRequest.Create(uri);
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                responseCode = GetWebResponseCode(response);
            }
            catch (WebException we)
            {
                // TODO: Logger gebruiken.
                Console.WriteLine("Could not get webURL: " + GetWebResponseCode((HttpWebResponse)we.Response));
            }
            return responseCode;
        }

    }
}
