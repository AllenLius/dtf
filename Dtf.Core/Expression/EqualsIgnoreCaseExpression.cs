using System;
using System.Xml.Serialization;

namespace Dta.Core
{
    [XmlRoot(ElementName = "EqualsIgnoreCase")]
    public class EqualsIgnoreCaseExpression : PropertyExpression
    {
        public EqualsIgnoreCaseExpression()
        {
        }

        public EqualsIgnoreCaseExpression(string name, string value)
            : base(name, value)
        {
        }

        public override bool IsMatch(string testValue)
        {
            return string.Compare(Value, testValue, StringComparison.OrdinalIgnoreCase) == 0;
        }

        protected override Expression Instance
        {
            get { return this; }
        }
    }
}
