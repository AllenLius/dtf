using System;
using System.IO;
using System.Net;
using System.Text;
using Dtf.Core;

namespace Dtf.Server
{
    public class HttpCurrentContext
    {
        private const string SoapContentType = "application/soap+xml";
        private const int BufferSize = 512;

        [ThreadStatic]
        private static HttpListenerContext _context;

        [ThreadStatic]
        private static string _body;

        public static bool Close()
        {
            try
            {
                Response.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public static bool WriteClose(string message)
        {
            try
            {
                if (message != null)
                {
                    Response.ContentType = SoapContentType;
                    byte[] buf = Encoding.UTF8.GetBytes(message);
                    Response.ContentLength64 = buf.Length;
                    Response.OutputStream.Write(buf, 0, buf.Length);
                }
                Response.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void CloseAsTimeout()
        {
            Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
            try
            {
                Response.Close();
            }
            catch (Exception)
            {
            }
        }

        public static void CloseAsError(string error)
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            Response.StatusDescription = error;
            try
            {
                Response.Close();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message">StatusDescription if code other than OK, otherwise the output stream</param>
        public static void Close(HttpStatusCode code = HttpStatusCode.OK, string message = null)
        {
            Response.StatusCode = (int)code;
            if (message != null)
            {
                if (code == HttpStatusCode.OK)
                {
                    Response.ContentType = SoapContentType;
                    int count = UTF8Encoding.UTF8.GetByteCount(message);
                    Response.ContentLength64 = count;
                    Response.OutputStream.WriteUTF8String(message);                    
                }
                else
                {
                    Response.StatusDescription = message;
                }
            }
            Response.Close();
        }

        public static void CloseAsNotImplemented()
        {
            Response.StatusCode = (int)HttpStatusCode.NotImplemented;
            try
            {
                Response.Close();
            }
            catch (Exception)
            {
            }
        }

        public static string Body
        {
            get
            {
                if (_body == null)
                {
                    int length = (int)Request.ContentLength64;
                    _body = Request.InputStream.ReadUTF8String(length);
                }
                return _body;
            }
        }

        public static HttpListenerRequest Request
        {
            get
            {
                return _context.Request;
            }
        }

        public static HttpListenerResponse Response
        {
            get
            {
                return _context.Response;
            }
        }

        public static HttpListenerContext Current
        {
            get
            {
                return _context;
            }
            set
            {
                _body = null;
                _context = value;               
            }
        }
    }
}
