using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskWinForm.Services
{
    public class APIHelper
    {
        const string main_url = "http://localhost:50965/api/v1/";

        #region Send POST Request
        string POSTRequest(string json, string route)
        {
            string result = "";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(main_url + route);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("Authorization", Token.token);

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                
                return result;
            }
            catch (WebException ex)
            {
                using (StreamReader r = new StreamReader(((HttpWebResponse)ex.Response).GetResponseStream()))
                {
                    result = r.ReadToEnd();
                }
                return result + "\n" + ex.Message;
            }
            catch (Exception ex)
            {
                return result + "\n" + ex.Message;
            }
        }
        #endregion

        #region Send GET Request
        string GETRequest(string prms, string route)
        {
            string result = "";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(main_url + route + prms);
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers.Add("Authorization", Token.token);

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                return result;
            }
            catch (WebException ex)
            {
                using (StreamReader r = new StreamReader(((HttpWebResponse)ex.Response).GetResponseStream()))
                {
                    result = r.ReadToEnd();
                }
                return result + "\n" + ex.Message;
            }
            catch (Exception ex)
            {
                return result + "\n" + ex.Message;
            }
        }
        #endregion
    }
}
