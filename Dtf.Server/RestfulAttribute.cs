using System;

namespace Dtf.Server
{
    [Flags]
    public enum ParameterStyle
    {
        Uri = 1,
        Content = 2
    }

    public class RestfulAttribute : Attribute
    {
        private string m_method;
        private string m_uriTemplate;
        private ParameterStyle m_parameterStyle = ParameterStyle.Content;

        public RestfulAttribute(string method, string uriTemplate, ParameterStyle parameterStyle=ParameterStyle.Uri)
        {
            m_method = method.ToUpper();
            m_uriTemplate = uriTemplate;
            if (m_method == "GET" && parameterStyle == ParameterStyle.Content)
            {
                throw new NotSupportedException("Http GET method does not support ParameterStyle.Content!");
            }
            m_parameterStyle = parameterStyle;
        }

        public string Method
        {
            get
            {
                return m_method;
            }
        }

        public string UriTemplate
        {
            get
            {
                return m_uriTemplate;
            }
        }

        public ParameterStyle ParameterStyle
        {
            get
            {
                return m_parameterStyle;
            }
        }
    }
}
