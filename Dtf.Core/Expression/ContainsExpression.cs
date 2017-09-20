using System.Xml.Serialization;

namespace Dtf.Core
{
    [XmlRoot(ElementName = "Contains")]
    public class ContainsExpression : PropertyExpression
    {
        public ContainsExpression()
        {
        }

        public ContainsExpression(string name, string value)
            : base(name, value)
        {            
        }

        public override bool IsMatch(string testValue)
        {
            return testValue == null ? false : testValue.Contains(Value);
        }

        protected override Expression Instance
        {
            get { return this; }
        }
    }
}
