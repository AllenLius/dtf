using System;
using System.Xml;

namespace Dta.Core
{
    public enum SoapCode
    {
        VersionMismatch,
        MustUnderstand,
        Client,
        Server
    }

    public class SoapFaultMessage : SoapMessage
    {
        /// <summary>
        /// for Deserialize
        /// </summary>
        protected SoapFaultMessage()
        {
        }

        public SoapFaultMessage(SoapCode faultCode, string reason, string detail = null)
        {
            FaultCode = faultCode;
            Reason = reason;
            Detail = detail;
        }

        public SoapFaultMessage(SoapCode faultCode, Exception ex)
        {
            FaultCode = faultCode;
            Reason = ex.GetExceptionText();
        }

        protected override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            reader.EnsureIsStartElement();
            //fault
            //if (reader.LocalName != SoapConstant.SoapFaultLocalName && String.Compare(reader.NamespaceURI, EnvelopeNS) != 0)
            //{
            //    throw new ArgumentException("Miss Fault element!", "reader");
            //}
            //to Code
            reader.ReadStartElement(SoapConstant.SoapFaultLocalName, EnvelopeNS);
            //to Code Value
            reader.ReadStartElement(SoapConstant.SoapCodeLocalName, EnvelopeNS);
            //to Code Value Content
            reader.ReadStartElement(SoapConstant.SoapCodeValueLocalName, EnvelopeNS);
            string valueString = reader.ReadContentAsString();
            FaultCode = GetCodeValue(valueString);
            reader.ReadEndElement();
            reader.ReadEndElement();
            //reason
            reader.ReadStartElement(SoapConstant.SoapReasonLocalName, EnvelopeNS);
            reader.ReadStartElement(SoapConstant.SoapReasonTextLocalName, EnvelopeNS);
            Reason = reader.ReadContentAsString();
            reader.ReadEndElement();
            reader.ReadEndElement();
            //detail
            reader.ReadStartElement(SoapConstant.SoapDetailLocalName, EnvelopeNS);
            Detail = reader.ReadInnerXml();
            reader.ReadEndElement();
            //end fault
            reader.ReadEndElement();
            //end body
            reader.ReadEndElement();
            //end envelope
            reader.ReadEndElement();
        }

        protected override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            string codeValue = GetCodeString(FaultCode);
            //fault
            writer.WriteStartElement(SoapConstant.SoapFaultLocalName, EnvelopeNS);
            //code
            writer.WriteStartElement(SoapConstant.SoapCodeLocalName, EnvelopeNS);
            //value
            writer.WriteStartElement(SoapConstant.SoapCodeValueLocalName, EnvelopeNS);
            writer.WriteValue(codeValue);
            //end value
            writer.WriteEndElement();
            //end code
            writer.WriteEndElement();
            //reason
            writer.WriteStartElement(SoapConstant.SoapReasonLocalName, EnvelopeNS);
            writer.WriteStartElement(SoapConstant.SoapReasonTextLocalName, EnvelopeNS);
            writer.WriteAttributeString("xml", "lang", "", "en");
            writer.WriteValue(Reason);
            writer.WriteEndElement();
            //end reason
            writer.WriteEndElement();
            //detail
            if (Detail != null)
            {
                writer.WriteStartElement(SoapConstant.SoapDetailLocalName, EnvelopeNS);
                try
                {
                    //, null, 
                    SoapSerializeHelper.WriteDataContract(writer, Detail, "string", "http://schemas.datacontract.org/2004/07/WcfService3");
                }
                catch (Exception ex)
                {
                    Log.Default.LogException(ex);
                }
                writer.WriteEndElement();
            }
            //end fault
            writer.WriteEndElement();
            //end body
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        private SoapCode GetCodeValue(string codeValue)
        {
            string value = codeValue;
            if (value.StartsWith(base.EnvelopePrefix))
            {
                value = value.Substring(base.EnvelopePrefix.Length + 1);
            }
            SoapCode code;
            if (!Enum.TryParse<SoapCode>(value, true, out code))
            {
                throw new ArgumentException(String.Format("{0} is not a valid fault value!", codeValue));
            }
            return code;
        }

        private string GetCodeString(SoapCode faultCode)
        {
            Log.Default.Trace("FaultCode:{0}", Enum.GetName(faultCode.GetType(), faultCode));
            Log.Default.Trace("FaultCode:{0}", faultCode.ToString());
            return String.Format("{0}:{1}", base.EnvelopePrefix, Enum.GetName(faultCode.GetType(), faultCode));
        }

        public SoapCode FaultCode { get; set; }

        public string Reason { get; set; }

        public string Detail { get; set; }
    }   
}
