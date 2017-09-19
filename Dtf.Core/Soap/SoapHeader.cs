using System;
using System.Xml;

namespace Dta.Core
{
    public class SoapHeader
    {
        public string HeaderNamespace;
        public bool MustUnderstand;
        public string Name;
        public object Value;

        private SoapHeader()
        {
        }

        public SoapHeader(string name, object value, bool mustUnderstand, string headerNamespace)
        {
            this.Name = name;
            this.Value = value;
            this.MustUnderstand = mustUnderstand;
            this.HeaderNamespace = headerNamespace;
        }

        //public SoapHeader(XmlReader reader)
        //{
        //    ReadXml(reader);
        //}

        public static SoapHeader Deserialize(XmlReader reader)
        {
            //StringReader stringReader = new StringReader(xml);
            //XmlReader reader = XmlReader.Create(stringReader);
            SoapHeader instance = new SoapHeader();
            instance.ReadXml(reader);
            return instance;
        }

        public void Serialize(XmlWriter writer)
        {
            WriteXml(writer);
        }

        protected virtual void ReadXml(XmlReader reader)
        {
            reader.EnsureIsStartElement();
            Name = reader.LocalName;
            HeaderNamespace = reader.NamespaceURI;
            string mustUnderstand = reader.GetAttribute(SoapConstant.SoapHeaderMustUnderstandLocalName, SoapConstant.EnvelopeNS);
            if (!String.IsNullOrEmpty(mustUnderstand))
            {
                MustUnderstand = mustUnderstand == "1" ? true : false;
            }
            Value = reader.ReadInnerXml();

            //reader.ReadStartElement();
        }

        protected virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(Name, HeaderNamespace);
            int mustUnderstand = MustUnderstand == true ? 1 : 0;
            writer.WriteAttributeString(SoapConstant.SoapHeaderMustUnderstandLocalName, SoapConstant.EnvelopeNS, mustUnderstand.ToString());
            writer.WriteRaw(Value.ToString());
            writer.WriteEndElement();
        }
    }
}
