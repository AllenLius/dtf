using System.Xml.Serialization;

namespace Dta.Core
{
    [XmlRoot(ElementName="Equals")]
    public class EqualsExpression : PropertyExpression
    {
        public EqualsExpression()
        {
        }

        public EqualsExpression(string name, string value)
            : base(name, value)
        {
        }

        public override bool IsMatch(string testValue)
        {
            return testValue == Value;
        }

        protected override Expression Instance
        {
            get { return this; }
        }
    }
}
