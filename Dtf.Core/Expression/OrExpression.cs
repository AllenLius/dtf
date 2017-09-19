using System.Xml.Serialization;

namespace Dta.Core
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
