using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Dtf.Core
{
    [XmlRoot(ElementName = "Multiple")]
    public class MultipleExpression : GroupExpression
    {
        protected override Expression Instance
        {
            get { return this; }
        }
    }
}
