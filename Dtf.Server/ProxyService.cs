using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Net;
using System.Web;
using System.Reflection;
using Dtf.Core;

namespace Dtf.Server
{
    public class ProxyService : ServiceBase
    {
        HttpListener m_httpListener = new HttpListener();
        private JobProxy m_messageProxy;
        private int port = 80;
        private CancellationTokenSource m_tokenSource;

        public ProxyService()
        {
            this.ServiceName = "DtfJobServer";
            this.CanHandleSessionChangeEvent = false;
            this.CanHandlePowerEvent = false;
            this.CanShutdown = false;
        }

        protected override void OnStart(string[] args)
        {
            Log.Default.AddAppender(new ConsoleLogAppender());
            string portStr = System.Configuration.ConfigurationManager.AppSettings["Port"];
            if (portStr != null)
            {
                port = int.Parse(portStr);
            }
            //string hostName = Dns.GetHostName();
            string url = string.Format("http://+:{0}/", port);
            m_messageProxy = new JobProxy();
            m_httpListener.Prefixes.Add(url);
            StartListener();
        }

        protected override void OnStop()
        {
            StopListener();
        }        

        void StartListener()
        {
            m_httpListener.Start();

            m_tokenSource = new CancellationTokenSource();
            
            Task.Run(new Action(() =>
            {
                while(true)
                {
                    if (m_tokenSource.Token.IsCancellationRequested)
                    {
                        m_httpListener.Stop();
                        m_tokenSource.Token.ThrowIfCancellationRequested();
                    }
                    HttpListenerContext context = m_httpListener.GetContext();
                    Action<object> action = new Action<object>(HandleRequest);
                    Task.Factory.StartNew(action, context);
                }
            }), m_tokenSource.Token);
        }

        void StopListener()
        {
            m_tokenSource.Cancel();   
        }

        void HandleRequest(object context)
        {
            HttpListenerContext httpContext = (HttpListenerContext)context;
            HttpCurrentContext.Current = httpContext;
            httpContext.Response.AddHeader("Cache-Control", "no-cache");
            InvokeResfulMethod();
        }

        void InvokeResfulMethod()
        {
            string httpMethod = HttpCurrentContext.Current.Request.HttpMethod;
            string urlSuffix = HttpCurrentContext.Current.Request.Url.PathAndQuery;
            List<MethodBase> methods = GetRestfulMethods();
            foreach (MethodBase method in methods)
            {
                RestfulAttribute restfulAttr = method.GetCustomAttribute<RestfulAttribute>();
                if (string.Compare(restfulAttr.Method, httpMethod, true) == 0)
                {
                    UriTemplate template = new UriTemplate(restfulAttr.UriTemplate);
                    Uri baseAddress = new Uri("http://temp.org");
                    UriTemplateMatch match = template.Match(baseAddress, new Uri(baseAddress, urlSuffix));
                    if (match != null)
                    {
                        Object[] parameters = GetParameters(method, restfulAttr.ParameterStyle, match);
                        try
                        {
                            method.Invoke(m_messageProxy, parameters);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.GetExceptionText());
                        }
                        return;
                    }
                }
            }
            HttpCurrentContext.CloseAsNotImplemented();
        }

        object[] GetParameters(MethodBase methodBase, ParameterStyle parameterStyle, UriTemplateMatch match)
        {
            List<object> paramValues = new List<object>();
            NameValueCollection boundVariables = new NameValueCollection();
            if (parameterStyle.HasFlag(ParameterStyle.Uri))
            {
                boundVariables = match.BoundVariables;
            }
            if (parameterStyle.HasFlag(ParameterStyle.Content))
            {
                string parameterString = HttpCurrentContext.Body;
                NameValueCollection contentParameters = HttpUtility.ParseQueryString(parameterString);
                boundVariables.Add(contentParameters);
            }
            foreach (ParameterInfo paramInfo in methodBase.GetParameters())
            {
                string paramName = paramInfo.Name;
                string paramValue = boundVariables[paramName];
                paramValues.Add(paramValue);
            }
            return paramValues.ToArray();
        }

        List<MethodBase> GetRestfulMethods()
        {
            List<MethodBase> methods = new List<MethodBase>();
            Type[] interfaces = m_messageProxy.GetType().GetInterfaces();
            foreach (Type t in interfaces)
            {
                List<MethodBase> methodsT = GetRestfulMethods(t);
                methods.AddRange(methodsT);
            }
            return methods;
        }

        List<MethodBase> GetRestfulMethods(Type type)
        {
            List<MethodBase> methods = new List<MethodBase>();
            foreach (MethodInfo method in type.GetMethods())
            {
                if (method.IsDefined(typeof(RestfulAttribute)))
                {
                    methods.Add(method);
                }
            }
            return methods;
        }
    }
}
