using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Dta.Core
{
    [XmlRoot(ElementName="FormatedContains")]
    public class FormattedContainsExpression : PropertyExpression
    {
        //{0} {123} {0123} {102}
        //\{0}\ Send\ a\ IM\ to\ \{1}\.
        public const string OP = "*=";
        public const string FormatPlaceHolderPattern = @"\\{(0|([1-9]+[0-9]*))\}";
        private Regex m_regex;

        public FormattedContainsExpression()
        {
        }

        public FormattedContainsExpression(string name, string value)
            :base(name, value)
        {
        }

        public override bool IsMatch(string testValue)
        {
            return testValue == null ? false : m_regex.Match(testValue).Success;
        }

        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }

        public override string Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                string pattern = Regex.Escape(value);
                pattern = Regex.Replace(pattern, FormatPlaceHolderPattern, ".+");
                m_regex = new Regex(pattern);
                base.Value = value;
            }
        }

        protected override Expression Instance
        {
            get { return this; }
        }
    }
}
