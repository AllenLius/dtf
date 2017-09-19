using System.Xml.Serialization;

namespace Dta.Core
{
    [XmlRoot(ElementName = "And")]
    public class AndExpression : GroupExpression
    {
        public AndExpression()
        {
        }

        public AndExpression(params Expression[] conditions)
        {
            Expressions = conditions;
        }

        protected override Expression Instance
        {
            get { return this; }
        }
    }
}
