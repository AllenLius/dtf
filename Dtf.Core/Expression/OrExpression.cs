using System.Xml.Serialization;

namespace Dtf.Core
{
    [XmlRoot(ElementName = "Or")]
    public class OrExpression : GroupExpression
    {
        public OrExpression()
        {
        }
        
        protected override Expression Instance
        {
            get { return this; }
        }
    }
}
