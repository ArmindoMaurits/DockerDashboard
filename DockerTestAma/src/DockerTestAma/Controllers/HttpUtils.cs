namespace DockerTestAma.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using Models;

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
                LogWriter.Instance.LogMessage("Could not get JSON from uri: " + uri + " : " + webException);
            }

            return responseData;
        }

        /// <summary>
        /// Posts a JSON Object to a given URI
        /// </summary>
        /// <param name="uri">URI of the target API page</param>
        /// <param name="jsonObject">JSON object, as key:value pairs</param>
        /// <returns>True or false, if the object has been posted succesfully.</returns>
        public static bool PostJsonObjectAtUri(Uri uri, object jsonObject)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    var dataString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject);

                    webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    webClient.UploadString(uri, "POST", dataString);
                }
                return true;
            }
            catch (WebException e)
            {
                LogWriter.Instance.LogMessage("Could not post JSON object:" + jsonObject.ToString() + " to URI: " + uri + " error: " + e);
                return false;
            }
        }

        /// <summary>
        /// Gets the response code of a given URI.
        /// </summary>
        /// <param name="uri">Desired URI location of a webpage.</param>
        /// <returns>The webResponseCode.</returns>
        public static int GetHttpWebResponseCode(Uri uri)
        {
            int responseCode = 0;
            try
            {
                WebRequest webRequest = WebRequest.Create(uri);
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                responseCode = GetWebResponseCode(response);
            }
            catch (WebException webException)
            {
                LogWriter.Instance.LogMessage("Could not get response from: " + uri + " : " + webException);
            }
            return responseCode;
        }

        /// <summary>
        /// Parses the webresponse and returns the StatusCode of the response.
        /// </summary>
        /// <param name="httpWebResponse">Our HttpWebResponse</param>
        /// <returns>Statuscode of the HttpWebResponse</returns>
        private static int GetWebResponseCode(HttpWebResponse httpWebResponse)
        {
            return (int)httpWebResponse.StatusCode;
        }
    }
}
