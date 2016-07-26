namespace DockerTestAma.Controllers
{
    using System;
    using System.IO;
    using System.Net;

    /// <summary>
    /// This class handles all HTTP calls and responses.
    /// </summary>
    public static class HttpUtils
    {
        /// <summary>
        /// Get a JSON object from a given webpage URI.
        /// </summary>
        /// <param name="uri">The location of the HTML page</param>
        /// <returns>The response of the webpage, so the JSON</returns>
        public static string GetJsonFromUri(Uri uri)
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
            catch (WebException webException)
            {
                // TODO: Logger gebruiken.
                Console.WriteLine("Could not get webURL: " + GetWebResponseCode((HttpWebResponse)webException.Response));
            }
            return responseData;
        }

        /// <summary>
        /// Parses the webresponse and returns the StatusCode of the response.
        /// </summary>
        /// <param name="httpWebResponse">Our HttpWebReponse</param>
        /// <returns>Statuscode of the HttpWebReponse</returns>
        private static int GetWebResponseCode(HttpWebResponse httpWebResponse)
        {
            return (int)httpWebResponse.StatusCode;
        }

        /// <summary>
        /// Gets the response code of a given URI
        /// </summary>
        /// <param name="uri">Desired URI location of a webpage</param>
        /// <returns>The webresponse</returns>
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
