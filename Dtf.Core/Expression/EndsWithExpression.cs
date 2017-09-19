using System.Xml.Serialization;

namespace Dta.Core
{
    [XmlRoot(ElementName = "EndsWith")]
    public class EndsWithExpression : PropertyExpression
    {
        public const string OP = "$=";

        public EndsWithExpression()
        {
        }

        public EndsWithExpression(string name, string value)
            : base(name, value)
        {
        }

        public override bool IsMatch(string testValue)
        {
            return testValue == null ? false : testValue.EndsWith(Value);
        }

        protected override Expression Instance
        {
            get { return this; }
        }
    }
}
