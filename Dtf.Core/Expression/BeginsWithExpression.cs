using System.Xml.Serialization;

namespace Dta.Core
{
    [XmlRoot(ElementName = "BeginsWith")]
    public class BeginsWithExpression : PropertyExpression
    {
        public BeginsWithExpression()
        {
        }

        public BeginsWithExpression(string name, string value)
            : base(name, value)
        {
        }

        public override bool IsMatch(string testValue)
        {
            return testValue == null ? false : testValue.StartsWith(Value);
        }

        protected override Expression Instance
        {
            get { return this; }
        }
    }
}
