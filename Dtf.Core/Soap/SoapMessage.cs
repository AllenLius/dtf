using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Dta.Core
{
    public class SoapMessage
    {
        private string m_envelopeNSPrefix;
        private string m_envelopeNS;
        private List<SoapHeader> m_headers;
        //protected const string SoapNS = "http://www.w3.org/2003/05/soap-envelope";
        //protected const string SoapNSLocalName = "Envelope";
        //protected const string SoapNSPrefix = "s";
        //protected const string SoapHeaderLocalName = "Header";
        //protected const string SoapBodyLocalName = "Body";
        //private List<SoapHeader> m_headers;

        //string m_message;

        protected SoapMessage()
        {
            m_envelopeNS = SoapConstant.EnvelopeNS;
            m_envelopeNSPrefix = SoapConstant.EnvelopeNSPrefix;
            m_headers = new List<SoapHeader>();
        }

        public void AddHeader(SoapHeader header)
        {
            m_headers.Add(header);
        }

        public static string Serialize(SoapMessage message)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            XmlWriter xmlWriter = XmlWriter.Create(sb, settings);
            message.WriteXml(xmlWriter);
            xmlWriter.Flush();
            return sb.ToString();            
        }

        public static T Deserialize<T>(string message) where T : SoapMessage
        {
            StringReader sr = new StringReader(message);
            XmlReader xmlReader = XmlReader.Create(sr);
            T t = (T)typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null).Invoke(new Object[0]);
            t.ReadXml(xmlReader);
            return t;
        }

        public static bool TryDeserialize<T>(string message, out T instance) where T : SoapMessage
        {
            instance = null;
            try
            {
                instance = Deserialize<T>(message);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        protected virtual void ReadXml(XmlReader reader)
        {
            reader.EnsureIsStartElement();
            m_envelopeNS = reader.NamespaceURI;
            if (reader.LocalName != SoapConstant.EnvelopeLocalName)
            {
                throw new ArgumentException("Soap message should start with " + SoapConstant.EnvelopeLocalName + "!");
            }
            //read to Header or body
            reader.ReadStartElement();
            if (reader.LocalName == SoapConstant.SoapHeaderLocalName && reader.NamespaceURI == m_envelopeNS)
            {
                //header
                XmlReader headerReader = reader.ReadSubtree();
                headerReader.ReadStartElement();

                    while (headerReader.IsStartElement())
                    {
                        XmlReader hdrReader = headerReader.ReadSubtree();
                        //string headerXml = reader.readsReadOuterXml();
                        SoapHeader header = SoapHeader.Deserialize(hdrReader);
                        m_headers.Add(header);
                        headerReader.ReadEndElement();
                        //hdrReader = reader.ReadSubtree();
                    }
                    //end header and move to body
                //if header is empty
                    if (reader.IsStartElement())
                    {
                        reader.ReadStartElement();
                    }
                    else
                    {
                        reader.ReadEndElement();
                    }
            }
            if (reader.LocalName != SoapConstant.SoapBodyLocalName || reader.NamespaceURI != m_envelopeNS)
            {
                throw new ArgumentException("Miss body element!", "reader");
            }
            reader.ReadStartElement();
        }

        protected virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(SoapConstant.EnvelopeNSPrefix, SoapConstant.EnvelopeLocalName, SoapConstant.EnvelopeNS);
            writer.WriteStartElement(SoapConstant.SoapHeaderLocalName, SoapConstant.EnvelopeNS);
            foreach (SoapHeader header in m_headers)
            {
                header.Serialize(writer);
            }
            //end header
            writer.WriteEndElement();
            writer.WriteStartElement(SoapConstant.SoapBodyLocalName, EnvelopeNS);
        }

        //protected string GetMessage(SoapMessage soapMessage)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    settings.OmitXmlDeclaration = true;
        //    XmlWriter xmlWriter = XmlWriter.Create(sb, settings);
        //    soapMessage.WriteXml(xmlWriter);
        //    xmlWriter.Flush();
        //    return sb.ToString();
        //}

        public string EnvelopeNS
        {
            get
            {
                return m_envelopeNS;
            }
        }

        public string EnvelopePrefix
        {
            get
            {
                return m_envelopeNSPrefix;
            }
        }

        public SoapHeader[] Headers
        {
            get
            {
                return m_headers.ToArray();
            }
        }
    }
}
