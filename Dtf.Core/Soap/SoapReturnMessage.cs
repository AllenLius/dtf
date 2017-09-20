using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.Xml;

namespace Dtf.Core
{
    public class SoapReturnMessage : SoapMessage
    {
        private object m_result;
        private string m_operationName;
        private string m_actionResponseName;
        private string m_serviceNS;
        private Dictionary<string, object> m_outArgs;

        protected SoapReturnMessage()
        {
        }

        //public SoapReturnMessage(XmlReader reader)
        //{
        //    ReadXml(reader);
        //}

        //public SoapReturnMessage(string serviceNS, string replyAction, object result = null, Dictionary<string, object> outArgs = null, SoapHeader[] headers = null)
        //{
        //    if (headers != null)
        //    {
        //        foreach (SoapHeader header in headers)
        //        {
        //            AddHeader(header);
        //        }
        //    }
        //    m_serviceNS = serviceNS;
        //    m_replyAction = replyAction;
        //    m_result = result;
        //    m_outArgs = outArgs;
        //}

        public SoapReturnMessage(MethodBase method, object result = null, Dictionary<string, object> outArgs = null, SoapHeader[] headers = null)
        {
            if (headers != null)
            {
                foreach (SoapHeader header in headers)
                {
                    AddHeader(header);
                }
            }
            m_result = result;
            m_outArgs = outArgs;
            ServiceContractAttribute serviceAttr;
            OperationContractAttribute operationAttr;
            GetContract(method, out serviceAttr, out operationAttr);
            if (!String.IsNullOrEmpty(serviceAttr.Namespace))
            {
                m_serviceNS = serviceAttr.Namespace;
            }
            else
            {
                m_serviceNS = SoapConstant.ContractDefaultNS;
            }
            if (!String.IsNullOrEmpty(operationAttr.Name))
            {
                m_operationName = operationAttr.Name;
            }
            else
            {
                m_operationName = method.Name;
            }
            if (!String.IsNullOrEmpty(operationAttr.ReplyAction))
            {
                m_actionResponseName = operationAttr.ReplyAction;
            }
            else
            {
                m_actionResponseName = method.Name + SoapConstant.ActionResponseNameSuffix;
            }
        }

        private void GetContract(MethodBase method, out ServiceContractAttribute serviceAttr, out OperationContractAttribute operationAttr)
        {
            serviceAttr = null;
            operationAttr = null;
            operationAttr = method.GetCustomAttribute<OperationContractAttribute>();
            //OperationContract mark in class
            if (operationAttr != null)
            {
                Type type = method.DeclaringType;
                serviceAttr = type.GetCustomAttribute<ServiceContractAttribute>();
            }
            else
            {            
                Type type = method.DeclaringType;
                Type[] iTypes = type.GetInterfaces();
                bool found = false;
                foreach (Type it in iTypes)
                {
                    InterfaceMapping map = type.GetInterfaceMap(it);
                    for (int i = 0; i < map.TargetMethods.Length; i++)
                    {
                        if (method == map.TargetMethods[i])
                        {
                            serviceAttr = it.GetCustomAttribute<ServiceContractAttribute>();
                            operationAttr = map.InterfaceMethods[i].GetCustomAttribute<OperationContractAttribute>();
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }
            }
            Debug.Assert(serviceAttr != null);
            Debug.Assert(operationAttr != null);
        }
        //public static SoapReturnMessage Create(string actionName, object result, Dictionary<string, object> outArgs, SoapHeader[] headers=null)
        //{
        //    SoapReturnMessage message = new SoapReturnMessage();
        //    if (headers != null)
        //    {
        //        foreach (SoapHeader header in headers)
        //        {
        //            message.AddHeader(header);
        //        }
        //    }
        //    message.m_action = actionName;
        //    message.m_result = result;
        //    message.m_outArgs = outArgs;
        //    return message;
        //}

        //public object ReadResult(MethodInfo method)
        //{
        //    // read to [Action]Result node
        //    XmlReader resultReader = m_responseReader.ReadSubtree();
        //    resultReader.ReadStartElement();
        //    if (!resultReader.IsStartElement())
        //    {
        //        throw new SerializationException("Message invalid!");
        //    }
        //    Type returnType = method.ReturnType;
        //    //TODO: check void
        //    return SoapSerializeHelper.ReadDataContract(resultReader, returnType);
        //}

        //public IReadOnlyDictionary<string, object> GetOutArgs(MethodInfo methodInfo)
        //{
        //    if (m_outArgs != null)
        //    {
        //        return new ReadOnlyDictionary<string, object>(m_outArgs);
        //    }
        //    return null;
        //}

        //public object ActionName
        //{
        //    get
        //    {
        //        return m_action;
        //    }
        //}       

        protected override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            reader.EnsureIsStartElement();
            m_actionResponseName = reader.LocalName;
            m_serviceNS = reader.NamespaceURI;
            reader.ReadStartElement();
            if (reader.LocalName.EndsWith(SoapConstant.ActionResponseResultSuffix))
            {
                //m_result = SoapSerializeHelper.ReadDataContract(reader.ReadSubtree(), typeof(
            }
            string responseName = m_operationName + SoapConstant.ActionResponseNameSuffix;
            string resultName = m_operationName + SoapConstant.ActionResponseResultSuffix;
            
            //if (!reader.ReadToDescendant(SoapConstrant.SoapBodyLocalName, SoapConstrant.SoapNS))
            //{
            //    throw new SerializationException("reader");
            //}
            ////read to method node
            //reader.ReadStartElement();
            //m_action = reader.LocalName;
            //if (m_action.EndsWith(SoapConstrant.ActionResponseSuffix))
            //{
            //    m_action = m_action.Substring(0, m_action.Length - SoapConstrant.ActionResponseSuffix.Length);
            //}
            //m_responseReader = reader.ReadSubtree();
        }

        protected override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            //writer Response
            writer.WriteStartElement(m_actionResponseName, m_serviceNS);
            if (m_result != null)
            {
                SoapSerializeHelper.WriteDataContract(writer, m_result, m_operationName + SoapConstant.ActionResponseResultSuffix, this.m_serviceNS);
            }
            if (m_outArgs != null)
            {
                foreach (KeyValuePair<string, object> outArg in m_outArgs)
                {
                    SoapSerializeHelper.WriteDataContract(writer, outArg.Value, outArg.Key);
                }
            }
            //write end Response
            writer.WriteEndElement();
            //write end body
            writer.WriteEndElement();
            //write end Envelop
            writer.WriteEndElement();
        }
    }
}
