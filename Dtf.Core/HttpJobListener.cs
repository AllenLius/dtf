using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Net;
using System.Web;
using Dta.Core;

namespace Dta.Core
{
    public class HttpJobListener
    {
        /// <summary>
        /// <para>parameter2: Job xml</para>
        /// </summary>
        public event Action<string> JobReceived;
        private const string ServiceContractAddressHeader = "Service-Contract-Address";
        public const int BufferSize = 512;
        private Uri m_uri;
        volatile bool m_stop;
        HttpWebRequest m_request;

        public HttpJobListener(Uri uri)
        {
            m_uri = uri;
            Log.Default.Trace("Listen job on :{0}", m_uri.ToString());
        }

        public void Start()
        {
            m_stop = false;
            Task.Run(new Action(() =>
            {
                string job;
                while (!m_stop)
                {
                    if (GetJob(out job))
                    {
                        //Log.Default.Trace("Get job:{0}", job);
                        if (JobReceived != null)
                        {
                            JobReceived(job);
                        }
                    }
                }
            }));
            Log.Default.Trace("Started");
        }

        public void Stop()
        {
            m_stop = true;
            if (m_request != null)
            {
                m_request.Abort();
            }
            Log.Default.Trace("Stopped");
        }

        public void SendResult(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                string errMsg = "SendResult: message is null or empty!";
                Log.Default.Error(errMsg);
                throw new ArgumentNullException("message");
            }
            int length = Encoding.UTF8.GetByteCount(message);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(m_uri);
            request.Method = "PUT";
            request.ContentLength = length;
            try
            {
                Stream contentStream = request.GetRequestStream();
                contentStream.WriteUTF8String(message);
                contentStream.Close();
                request.GetResponse().Close();
            }
            catch (WebException ex)
            {
                Log.Default.Error(string.Format("error code:{0}, Description:{1}", ex.Status.ToString(), ex.Message));
            }
            Log.Default.Trace("Send done");
        }

        private bool GetJob(out string job)
        {
            job = null;
            try
            {
                Log.Default.Debug("Getting job...");
                m_request = (HttpWebRequest)WebRequest.Create(m_uri);
                //#if TEST
                //                request.Proxy = new WebProxy("iss-guoliu-1", 8888);
                //#endif
                m_request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)m_request.GetResponse())
                {
                    //serviceAddress = response.GetResponseHeader(ServiceContractAddressHeader);
                    //if (String.IsNullOrEmpty(serviceAddress))
                    //{
                    //    Log.Default.Error("Invalid http headers: no {0} present!", ServiceContractAddressHeader);
                    //    return false;
                    //}
                    Stream contentStream = response.GetResponseStream();
                    int length = (int)response.ContentLength;
                    job = contentStream.ReadUTF8String(length);
                }
                //Log.Default.Trace("Got job:{0}", job);
            }
            catch (WebException ex)
            {
                if (ex.Status != WebExceptionStatus.Timeout)
                {
                    Log.Default.Trace(string.Format("HttpCode:{0}", ex.Status));
                    Thread.Sleep(1000);
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Default.LogException(ex);
                Thread.Sleep(1000);
                return false;
            }
            return true;
        }

        //private bool HttpSend(string method, string uriSuffix, string content, out string responseMessage)
        //{
        //    responseMessage = null;
        //    Log.Default.Trace("HttpRequest: Create {0}, uriSuffix {1}, content {2}", method, uriSuffix, content);
        //    responseMessage = null;
        //    method = method.ToUpper();
        //    m_request = (HttpWebRequest)WebRequest.Create(m_uri + uriSuffix);
        //    m_request.Timeout = int.MaxValue;
        //    m_request.Create = method;
        //    if (method == "POST" && !string.IsNullOrEmpty(content))
        //    {
        //        try
        //        {
        //            m_request.ContentLength = content.Length;
        //            Stream requestStream = m_request.GetRequestStream();
        //            StreamWriter requestWriter = new StreamWriter(requestStream);
        //            requestWriter.Write(content);
        //            requestWriter.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Default.LogException(ex);
        //        }
        //    }
        //    try
        //    {
        //        HttpWebResponse response = (HttpWebResponse)m_request.GetResponse();
        //        Stream responseStream = response.GetResponseStream();
        //        // null if server no response data
        //        if (response != null)
        //        {
        //            StreamReader sr = new StreamReader(responseStream);
        //            responseMessage = sr.ReadToEnd();
        //            sr.Close();
        //            return true;
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
        //        // bypass timeout
        //        if (errorResponse != null && errorResponse.StatusCode != HttpStatusCode.RequestTimeout)
        //        {
        //            Log.Default.LogException(ex);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Default.Error("TODO need fix here!");
        //        Log.Default.LogException(ex);
        //    }
        //    return false;
        //}
    }
}