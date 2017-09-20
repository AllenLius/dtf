using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Xml;
using System.Xml.Serialization;

namespace Dtf.Core
{
    public class SoapCallProxy
    {
        public const string DefaultServiceContractNS = "http://tempuri.org";
        //string ErrorMessage_XmlIncorrect = "Incorrect xml call meesage!";
        //string ErrorMessage_NoOperationTypeAttritube = "OperationTypeAttribute absent!";
        //string ErrorMessage_NoOperationInTypeAttritube = "Operation type should has at least one operationable method!";
        //List<Type> m_operationTypes = new List<Type>();
        List<MethodInfo> m_operations = new List<MethodInfo>();
        Dictionary<string, MethodInfo> m_actions = new Dictionary<string, MethodInfo>();
        //List<MethodBase> m_methodList = new List<MethodBase>();

        //private string m_serviceAddress;
        private object m_instance;

        //public SoapCallProxy(Type type, string serviceAddress)
        //{
        //    m_instance = Activator.CreateInstance(type);
        //    m_serviceAddress = serviceAddress;
        //    if (!m_serviceAddress.EndsWith("/"))
        //    {
        //        m_serviceAddress += "/";
        //    }
        //}

        public SoapCallProxy(object instance)
        {
            m_instance = instance;
            //m_serviceAddress = baseAddress.TrimEnd('/')+"/"+instance.GetType().Name;
            Init();
        }

        //public void AddType<T>(string serviceAddress)
        //{
        //    AddType(typeof(T), serviceAddress);
        //}

        private void Init()
        {
            string errMsg = null;
            Type type = m_instance.GetType();
            bool hasOperation = false;
            List<string> methodNames = new List<string>();
            foreach (Type iType in type.GetInterfaces())
            {
                ServiceContractAttribute serviceContractAttr = iType.GetCustomAttribute<ServiceContractAttribute>(true);
                if (serviceContractAttr != null)
                {
                    InterfaceMapping map = type.GetInterfaceMap(iType);
                    for (int i = 0; i < map.InterfaceMethods.Length; i++)
                    {
                        OperationContractAttribute operationContractAttr = map.InterfaceMethods[i].GetCustomAttribute<OperationContractAttribute>(true);
                        if (operationContractAttr != null)
                        {
                            hasOperation = true;
                            string action = GetAction(serviceContractAttr, iType.Name, operationContractAttr, map.InterfaceMethods[i].Name);
                            MethodInfo targetMethod = map.TargetMethods[i];
                            AddOperation(action, targetMethod);
                        }
                    }
                }
            }
            {
                ServiceContractAttribute serviceContractAttr = type.GetCustomAttribute<ServiceContractAttribute>();
                if (hasOperation)
                {
                    if (serviceContractAttr != null)
                    {
                        errMsg = String.Format("{0} cannot in both interface and class!", typeof(ServiceContractAttribute).Name);
                        throw new InvalidOperationException(errMsg);
                    }
                }
                else if (serviceContractAttr==null)
                {
                    errMsg = String.Format("No {0} define in type {1}!", typeof(ServiceContractAttribute).Name, type.Name);
                    throw new InvalidOperationException(errMsg);
                }
                else
                {
                    MethodInfo[] methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    foreach (MethodInfo method in methods)
                    {
                        OperationContractAttribute operationContractAttr = method.GetCustomAttribute<OperationContractAttribute>(true);
                        if (operationContractAttr != null)
                        {
                            hasOperation = true;
                            string action = GetAction(serviceContractAttr, type.Name, operationContractAttr, method.Name);
                            AddOperation(action, method);
                        }
                    }
                }
                if (!hasOperation)
                {
                    errMsg = String.Format("No method mark as {0}!", typeof(OperationContractAttribute).Name);
                    throw new InvalidOperationException(errMsg);
                }
            }
        }

        private string GetAction(ServiceContractAttribute serviceContractAttr, string typeName, OperationContractAttribute operationContractAttr, string methodName)
        {
            string action = operationContractAttr.Action;
            if (String.IsNullOrEmpty(action))
            {
                string ns = serviceContractAttr.Namespace;
                string serviceContractName = serviceContractAttr.Name;
                string opName = operationContractAttr.Name;
                if (String.IsNullOrEmpty(ns))
                {
                    ns = DefaultServiceContractNS;
                }
                if (String.IsNullOrEmpty(serviceContractName))
                {
                    serviceContractName = typeName;
                }
                if (String.IsNullOrEmpty(opName))
                {
                    opName = methodName;
                }
                action = String.Format("{0}/{1}/{2}", ns, serviceContractName, opName);
            }
            return action;
        }
        //public void AddInstance(object instance, string serviceAddress)
        //{
        //    AddType(instance.GetType(), serviceAddress);
        //    m_instance.Add(instance);
        //}

        //public Dictionary<string, List<string>> GetMetadata()
        //{
        //    Dictionary<string, List<string>> metaData = new Dictionary<string, List<string>>();
        //    foreach (var m in m_methodList)
        //    {
        //        string type = m.DeclaringType.Name;
        //        string method = m.Name;
        //        List<string> methods;
        //        if (!metaData.TryGetValue(type, out methods))
        //        {
        //            methods = new List<string>();
        //            metaData.Add(type, methods);
        //        }
        //        methods.Add(method);
        //    }
        //    return metaData;
        //}

        private void AddOperation(string action, MethodInfo method)
        {
            if (m_actions.ContainsKey(action))
            {
                string errMsg = String.Format("The action {0} already exists!", action);
                throw new InvalidOperationException(errMsg);
            }
            string methodName = method.Name;
            if (method.ReturnType != typeof(void))
            {
                if (!IsSerializable(method.ReturnType))
                {
                    throw new InvalidOperationException("The method " + methodName + " has return type " + method.ReturnType.Name + " can not be serialize!");
                }
            }
            foreach (ParameterInfo p in method.GetParameters())
            {
                Type paramType = p.ParameterType;
                if (!IsSerializable(paramType))
                {
                    throw new InvalidOperationException("The method " + methodName + " has parameter type " + paramType.Name + " can not be serialize!");
                }
            }
            m_actions.Add(action, method);
        }

        private bool IsSerializable(Type type)
        {
            //TODO fix
            //if (!type.IsSerializable)
            //{
            //    return false;
            //}
            //FieldInfo[] fields = type.GetFields(BindingFlags.Public);
            //PropertyInfo[] propertie = type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public);

            //foreach (FieldInfo field in fields)
            //{
            //    if (!IsSerializable(field.FieldType))
            //    {
            //        return false;
            //    }
            //}
            //foreach (PropertyInfo property in propertie)
            //{
            //    if (!IsSerializable(property.PropertyType))
            //    {
            //        return false;
            //    }
            //}
            return true;
        }

        public virtual string Execute(string message)
        {
            Log.Default.Trace("Execute.");
            SoapMessage returnMessage = null;
            SoapCallMessage soapMessage = SoapMessage.Deserialize<SoapCallMessage>(message);
            
            try
            {
                string action = soapMessage.Action;
                Log.Default.Trace(string.Format("Action:{0}", action));
                MethodInfo method = null;
                if (!m_actions.TryGetValue(action, out method))
                {
                    string errMsg = string.Format("The action {0}  doesn't exists!", action);
                    throw new InvalidOperationException(errMsg);
                }
                Log.Default.Trace("Create Name:{0}", method.Name);
                object[] args = soapMessage.GetInArgs(method);
                Log.Default.Trace("Invoke method:{0}", method.Name);
                object result = method.Invoke(m_instance, args);
                //Log.Default.Trace("result:{0}", result);
                Log.Default.Trace("Invoke method:{0} done", method.Name);
                Dictionary<string, object> outArgs = GetOutArgs(method, args);
                returnMessage = new SoapReturnMessage(method, result, outArgs);
            }
            catch (Exception ex)
            {
                Log.Default.LogException(ex);
                returnMessage = new SoapFaultMessage(SoapCode.Server, "Error:"+ex.Message+ "\n\rText:"+ex.GetExceptionText());
            }
            string response = SoapMessage.Serialize(returnMessage);
            return response;
        }

        public bool HasAction(string action)
        {
            return m_actions.ContainsKey(action);
        }

        private string GetActionAddressing(MethodInfo methodInfo)
        {
            return ServiceAddress + "/" + methodInfo.Name;
        }

        //private MethodInfo GetMethod(string actionAddress)
        //{
        //    if (actionAddress == null)
        //    {
        //        throw new ArgumentNullException("actionAddress");
        //    }
        //    string methodName = null;
        //    int index = actionAddress.IndexOf(ServiceAddress);
        //    if (index == 0)
        //    {
        //        methodName = actionAddress.Substring(ServiceAddress.Length);
        //        methodName = methodName.Trim('/');
        //        foreach (var m in m_operations)
        //        {
        //            if (String.Compare(m.Name, methodName, StringComparison.OrdinalIgnoreCase) == 0)
        //            {
        //                return m;
        //            }
        //        }
        //    }
        //    return null;
        //}

        private Dictionary<string, object> GetOutArgs(MethodBase method, object[] args)
        {
            Dictionary<string, object> argsMap = new Dictionary<string, object>();
            ParameterInfo[] pInfos = method.GetParameters();
            for (int i = 0; i < pInfos.Length ;i++)
            {
                ParameterInfo pInfo = pInfos[i];
                if (pInfo.IsOut) // || pInfo.ParameterType.HasElementType)
                {
                    Log.Default.Trace(">>>OutArgs:{0}", pInfo.Name);
                    argsMap.Add(pInfo.Name, args[i]);
                }
            }
            return argsMap;
        }

        private object[] DeserializeParameters(MethodBase method, XmlReader xmlReader)
        {
            List<object> parameters = new List<object>();
            ParameterInfo[] paramInfos = method.GetParameters();

            xmlReader.ReadStartElement();
            foreach (ParameterInfo paramInfo in paramInfos)
            {
                if (!xmlReader.IsStartElement() || xmlReader.NodeType != XmlNodeType.Element)
                {
                    throw new InvalidOperationException("Mismatch parameter!");
                }
                if (paramInfo.IsOut)
                {
                    parameters.Add(new object());
                    //object Activator.CreateInstance(paramInfo.ParameterType);
                }
                string parameterName = xmlReader.Name;
                Type parameterType = paramInfo.ParameterType;
                XmlSerializer xmlSer = new XmlSerializer(parameterType, new XmlRootAttribute(parameterName));
                try
                {
                    object obj = xmlSer.Deserialize(xmlReader);
                    parameters.Add(obj);
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("The argument format is incorrect!");
                }
                //xmlReader.ReadEndElement();
            }
            if (parameters.Any())
            {
                return parameters.ToArray();
            }
            return null;
        }



        public string ServiceAddress
        {
            get
            {
                return "m_serviceAddress";
            }
        }

        //protected virtual void LogMessage(string message)
        //{
        //    if (Log != null)
        //    {
        //        Log(message);
        //    }
        //}

        //protected virtual void LogException(Exception ex)
        //{
        //}
    }
}