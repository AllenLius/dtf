using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

namespace Dta.Core
{
    public class SerializableParameters : Dictionary<string, string>
    {
        private const string RootNodeName = "parameters";

        public SerializableParameters()
        {
        }

        public SerializableParameters(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }        

        public static implicit operator string(SerializableParameters parameters)
        {
            StringBuilder sb = new StringBuilder(); ;
            XmlWriter writer = XmlWriter.Create(sb);
            writer.WriteStartElement(RootNodeName);
            foreach (var kv in parameters)
            {
                writer.WriteElementString(kv.Key, kv.Value);
            }
            writer.WriteEndElement();
            writer.Flush();
            byte[] data = UTF8Encoding.UTF8.GetBytes(sb.ToString());
            string base64Xml = Convert.ToBase64String(data); ;
            return base64Xml;
        }

        public static implicit operator SerializableParameters(string base64Xml)
        {
            SerializableParameters instance = new SerializableParameters();
            if (!string.IsNullOrEmpty(base64Xml))
            {
                byte[] data = Convert.FromBase64String(base64Xml);
                string xml = UTF8Encoding.UTF8.GetString(data, 0, data.Length);
                XmlReader reader = XmlReader.Create(new StringReader(xml));
                reader.ReadStartElement(RootNodeName);
                while (reader.IsStartElement())
                {
                    instance.Add(reader.Name, reader.ReadElementContentAsString());
                }
            }
            return instance;
        }
    }
}
