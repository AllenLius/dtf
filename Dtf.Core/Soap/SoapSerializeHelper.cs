using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace Dta.Core
{
    public static class SoapSerializeHelper
    {
        public static object ReadDataContract(XmlReader reader, Type type)
        {
            //DataContractSerializerSettings dcss = new DataContractSerializerSettings();
            //dcss.RootName = new XmlDictionary().Add("GetDataResult");
            DataContractSerializer dcs = new DataContractSerializer(type);
            return dcs.ReadObject(reader, false);
            //dcs.WriteStartObject(xmlWriter1, myData);
            //dcs.WriteObject(xmlWriter1, myData);
            //dcs = new DataContractSerializer(typeof(string), dcss);
            //dcs.WriteObject(xmlWriter1, "aabb");
        }

        public static void WriteDataContract(XmlWriter writer, object obj, string rootName = null, string ns = null, string prefix = null)
        {
            DataContractSerializerSettings dcss = new DataContractSerializerSettings();
            if (String.IsNullOrEmpty(rootName))
            {
                Type type = obj.GetType();
                if (type.IsGenericType)
                {
                    StringBuilder name = new StringBuilder();
                    name.Append(type.Name.Substring(0, type.Name.IndexOf('`')));
                    name.Append("Of");
                    Type[] argsType = type.GetGenericArguments();
                    foreach (Type argType in argsType)
                    {
                        name.Append(argType.Name);
                    }
                    rootName = name.ToString();
                }
                rootName = obj.GetType().Name;
            }
            if (!String.IsNullOrEmpty(ns))
            {
                dcss.RootNamespace = new XmlDictionary().Add(ns);
                //ns = SoapConstant.ContractDefaultNS;
            }
            dcss.RootName = new XmlDictionary().Add(rootName);
            //dcss.RootNamespace = new XmlDictionary().Add(ns);
            DataContractSerializer dcs = new DataContractSerializer(obj.GetType(), dcss);
            dcs.WriteObject(writer, obj);
        }

        #region custom impl
        private static Type[] SupportTypes = new Type[] { typeof(byte), typeof(short), typeof(int), typeof(long), typeof(float), typeof(double), typeof(char), typeof(string), typeof(bool) };
        
        public static object ReadDataContract2(XmlReader reader, Type type, string rootName=null)
        {
            string name = rootName;
            if (rootName==null)
            {
                name = type.Name;
            }

            while (reader.IsStartElement())
            {
                string localName = reader.LocalName;
                if (name != type.Name)
                {
                    throw new SerializationException();
                }
                if (reader.HasValue)
                {
                    if (SupportTypes.Contains(type))
                    {
                        return type.ToString();
                    }
                }
                reader.ReadStartElement();
            }
            
            if (type.IsArray)
            {

                Type t = type.GetElementType();
                object value = ReadDataContract2(reader, t, null);

            }
            return null;
        }

        /// <summary>
        /// Soap DataContract
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="xmlWriter"></param>
        /// <param name="prefix"></param>
        /// <param name="rootName"></param>
        /// <param name="ns"></param>
        public static void WriteDataContract2(XmlWriter xmlWriter, object obj, string rootName = null, string ns = null, string prefix = null)
        {
            //found out if has nil value object
            bool needXsi = false;
            if (rootName == null)
            {
                Type objType = obj.GetType();
                rootName = objType.IsArray ? String.Format("ArrayOf{0}", objType.GetElementType().Name) : objType.Name;
            }
            Queue<KeyValuePair<string, object>> serObjs = new Queue<KeyValuePair<string, object>>();
            Queue<KeyValuePair<string, object>> serObjs2 = new Queue<KeyValuePair<string, object>>();
            serObjs2.Enqueue(new KeyValuePair<string, object>(rootName, obj));
            while (serObjs2.Count > 0)
            {
                KeyValuePair<string, object> serObj = serObjs2.Dequeue();
                serObjs.Enqueue(serObj);
                //skip seplite
                if (String.IsNullOrEmpty(serObj.Key))
                {
                    continue;
                }
                if (serObj.Value == null)
                {
                    needXsi = true;
                    continue;
                }
                Type type = serObj.Value.GetType();
                if (SupportTypes.Contains(type))
                {
                    continue;
                }
                
                IEnumerable iter = serObj.Value as IEnumerable;
                if (iter != null)
                {
                    serObjs.Enqueue(new KeyValuePair<string, object>(rootName, null));
                    foreach (object o in iter)
                    {
                        serObjs2.Enqueue(new KeyValuePair<string, object>(o.GetType().Name, o));
                    }
                    serObjs2.Enqueue(new KeyValuePair<string, object>());
                    continue;
                }
                
                
                
                foreach (MemberInfo member in type.GetMembers())
                {
                    string name = null;
                    object value = null;
                    DataMemberAttribute dataMemberAttr = member.GetCustomAttribute<DataMemberAttribute>();
                    if (dataMemberAttr == null)
                    {
                        continue;
                    }
                    if (member.MemberType == MemberTypes.Field)
                    {
                        name = ((FieldInfo)member).Name;
                        value = ((FieldInfo)member).GetValue(serObj.Value);
                    }
                    else if (member.MemberType == MemberTypes.Property)
                    {
                        name = ((PropertyInfo)member).Name;
                        value = ((PropertyInfo)member).GetValue(serObj.Value);
                    }
                    else
                    {
                        continue;
                    }
                    if (!String.IsNullOrEmpty(dataMemberAttr.Name))
                    {
                        name = dataMemberAttr.Name;
                    }
                    serObjs2.Enqueue(new KeyValuePair<string, object>(name, value));
                }
                //make seplit
                serObjs2.Enqueue(new KeyValuePair<string, object>());
            }
            while (serObjs.Count > 0)
            {
                KeyValuePair<string, object> serObj = serObjs.Dequeue();
                string name = serObj.Key;
                object value = serObj.Value;
                if (String.IsNullOrEmpty(name))
                {
                    xmlWriter.WriteEndElement();
                    continue;
                }
                if (ns == null)
                {
                    xmlWriter.WriteStartElement(name);
                }
                else if (prefix == null)
                {
                    xmlWriter.WriteStartElement(name, ns);
                }
                else
                {
                    xmlWriter.WriteStartElement(prefix, name, ns);
                }

                if (needXsi)
                {
                    xmlWriter.WriteAttributeString("xmlns", "i", null, "http://www.w3.org/2001/XMLSchema-instance");
                    needXsi = false;
                }
                
                if (SupportTypes.Contains(value.GetType()))
                {
                    xmlWriter.WriteValue(value.ToString());
                    xmlWriter.WriteEndElement();
                }
                else if (value == null)
                {
                    xmlWriter.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
                    xmlWriter.WriteEndElement();
                }
            }
        #endregion

        }
    }
}
