using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Dtf.Core
{
    public class SoapCallMessage : SoapMessage
    {
        private string m_argsXml;
        //private string m_message;
        private const string SoapActionLocalName = "Action";
        private const string SoapAddressingNS = "http://www.w3.org/2005/08/addressing";
        private string m_actionAddress;
        private string m_toAddress;
        private string m_operation;
        private string m_operationNS;
        //private string m_actionNS;
        private object[] m_args;

        /// <summary>
        /// for Deserialize
        /// </summary>
        protected SoapCallMessage()
        {
        }

        //public SoapCallMessage(XmlReader reader)
        //{
        //    ReadXml(reader);
        //}

        public object[] GetInArgs(MethodInfo method)
        {
            if (m_args == null)
            {
                Log.Default.Trace(string.Format(">>Create:{0}", method.Name));
                StringReader stringReader = new StringReader(m_argsXml);
                XmlReader argsReader = XmlReader.Create(stringReader);
                argsReader.EnsureIsStartElement();
                ParameterInfo[] paramsInfo = method.GetParameters();
                object[] args = new object[paramsInfo.Length];
                argsReader.ReadStartElement();
                while (argsReader.IsStartElement())
                {
                    string argName = argsReader.LocalName;
                    Log.Default.Trace(string.Format(">>argName:{0}", argName));
                    int i = 0;
                    ParameterInfo pInfo = null;
                    for (; i < paramsInfo.Length; i++)
                    {
                        if (paramsInfo[i].Name == argName)
                        {
                            pInfo = paramsInfo[i];
                            break;
                        }
                    }
                    if (pInfo == null)
                    {
                        throw new Exception("pInfo is null!");                        
                    }
                    if (!pInfo.IsOut)
                    {
                        Type type = pInfo.ParameterType;
                        Log.Default.Trace(">>type:{0}", type.Name);
                        if (type.HasElementType)
                        {
                            Log.Default.Trace(">>HasElementType");
                            //type = type.GetElementType();
                            Log.Default.Trace(">>el type:{0}", type.Name);
                        }
                        args[i] = SoapSerializeHelper.ReadDataContract(argsReader, type);
                    }
                    else
                    {
                        argsReader.ReadStartElement();
                    }
                }
                m_args = args;
            }            
            return m_args;            
        }

        protected override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            reader.EnsureIsStartElement();
            //read to method node
            //reader.ReadStartElement();
            m_operation = reader.LocalName;
            m_operationNS = reader.NamespaceURI;
            m_argsXml = reader.ReadOuterXml();
            foreach (SoapHeader h in Headers)
            {
                if (String.Compare(h.HeaderNamespace, SoapConstant.SoapAddressingNS, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (h.Name == SoapConstant.SoapHeaderActionLocalName)
                    {
                        m_actionAddress = h.Value.ToString();
                    }
                    else if (h.Name == SoapConstant.SoapHeaderToLocalName)
                    {
                        m_toAddress = h.Value.ToString();
                    }
                }                
            }
            if (String.IsNullOrEmpty(m_actionAddress))
            {
                throw new ArgumentException("Miss Action address", "reader");
            }
            if (String.IsNullOrEmpty(m_toAddress))
            {
                throw new ArgumentException("Miss To address", "reader");
            }
        }

        protected override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteRaw(m_argsXml);
            //end body
            writer.WriteEndElement();
            //end envelope
            writer.WriteEndElement();
            //base.WriteXml(writer);
            //writer.write
            ////write to method node
            //writer.WriteStartElement(Name, OperationNS);
            //XmlReader xmlReader = XmlReader.Create(new StringReader(m_argsXml));
            //xmlReader.EnsureIsStartElement();
            //string argsXml = xmlReader.ReadInnerXml();
            //writer.WriteRaw(argsXml);
            //writer.WriteEndElement();
            //writer.WriteEndElement();
        }               

        public string Action
        {
            get
            {
                return m_actionAddress;
            }
        }

        public string To
        {
            get
            {
                return m_toAddress;
            }
        }

        /// <summary>
        /// Name of OperationContract
        /// </summary>
        public string Name
        {
            get
            {
                return m_operation;
            }
        }

        /// <summary>
        /// Namespace of OperationContract
        /// </summary>
        public string OperationNS
        {
            get
            {
                return m_operationNS;
            }
        }        
    }
}
