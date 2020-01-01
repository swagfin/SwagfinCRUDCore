using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SwagfinRESTServices
{
    public static class API
    {
        #region GetResponceViaGETAsync
        public static Task<string> GetResponceViaGETAsync(string UrlPath, List<UrlParameter> Parameters = null, List<UrlParameter> Headers = null)
        {
            return Task.Run(() =>
            {
                try
                {
                    // Check Parameters
                    if (Parameters != null)
                    {
                        UrlPath += "?";
                        foreach (UrlParameter param in Parameters)
                        {
                            if (UrlPath.Length > 0)
                                UrlPath += "&";
                            UrlPath += HttpUtility.UrlEncode(param.Key, Encoding.UTF8);
                            UrlPath += "=" + HttpUtility.UrlEncode(param.Value, Encoding.UTF8);
                        }
                    }
                    HttpWebRequest webRequest = (HttpWebRequest)System.Net.WebRequest.Create(UrlPath);
                    webRequest.Method = "GET";
                    webRequest.Accept = "application/json";

                    //Add Headers here
                    if (Headers != null)
                    {
                        foreach (UrlParameter param in Headers)
                        {
                            webRequest.Headers.Add(param.Key, param.Value);
                        }
                    }

                    HttpWebResponse httpResponse = (HttpWebResponse)webRequest.GetResponse();
                    StreamReader webpageReader = new StreamReader(httpResponse.GetResponseStream());
                    return webpageReader.ReadToEnd();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            });

        }

        #endregion

        #region GetResponceViaPOSTAsync
        public static Task<string> GetResponceViaPOSTAsync(string UrlPath, List<UrlParameter> Parameters = null, List<UrlParameter> Headers = null)
        {
            return Task.Run(() =>
            {
                try
                {
                    // Check Parameters
                    string ParamsDataContents = "";
                    if (Parameters != null)
                    {
                        foreach (UrlParameter param in Parameters)
                        {
                            if (ParamsDataContents.Length > 0)
                                ParamsDataContents += "&";
                            ParamsDataContents += HttpUtility.UrlEncode(param.Key, Encoding.UTF8);
                            ParamsDataContents += "=" + HttpUtility.UrlEncode(param.Value, Encoding.UTF8);
                        }
                    }
                    //#Get All Bytes Data
                    byte[] byteArray = Encoding.UTF8.GetBytes(ParamsDataContents);
                    HttpWebRequest webRequest = (HttpWebRequest)System.Net.WebRequest.Create(UrlPath);
                    webRequest.Method = "POST";
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                    webRequest.ContentLength = byteArray.Length;
                    webRequest.Accept = "application/json";

                    //Add Headers here
                    if (Headers != null)
                    {
                        foreach (UrlParameter param in Headers)
                        {
                            webRequest.Headers.Add(param.Key, param.Value);
                        }
                    }

                    Stream webpageStream = webRequest.GetRequestStream();
                    webpageStream.Write(byteArray, 0, byteArray.Length);
                    webpageStream.Close();
                    HttpWebResponse httpResponse = (HttpWebResponse)webRequest.GetResponse();
                    StreamReader webpageReader = new StreamReader(httpResponse.GetResponseStream());
                    return webpageReader.ReadToEnd();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            });      
        }

        #endregion

    }



}
