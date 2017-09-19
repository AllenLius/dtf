using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Dta.Core
{
    public static class XmlReaderExtension
    {
        public static void EnsureIsStartElement(this XmlReader reader)
        {
            if (!reader.IsStartElement())
            {
                throw new FormatException("Reader is not in start element!");
            }
        }

        public static void EnsureElementName(this XmlReader reader, string name, string ns = null)
        {
            if (reader.LocalName == name)
            {
                throw new ArgumentException(String.Format("Element {0} not present!", name));
            }
            if (ns != null && String.Compare(reader.NamespaceURI, ns, StringComparison.OrdinalIgnoreCase) != 0)
            {
                throw new ArgumentException(String.Format("Namespace {0} doesn't match!", ns));
            }
        }
    }
}
