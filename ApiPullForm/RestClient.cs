using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ApiPullForm
{
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    class RestClient
    {
        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }

        public RestClient()
        {
            endPoint = string.Empty;
            httpMethod = httpVerb.GET;
        }

        public string makeRequest()
        {
            string strResponse = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = httpMethod.ToString();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("error code:" + response.StatusCode.ToString());

                }
                // Process the response stream (could be JSON, XML, or HTML, etc)
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponse = reader.ReadToEnd();
                        }// end of StreamReader
                    }
                }// end of using responseStream

            }// end of using response
            return strResponse;
        }
    }
}
