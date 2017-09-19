using System.Xml;

namespace Dta.Core
{
    public abstract class PropertyExpression : Expression
    {
        private const string PropertyAttributeName = "Property";

        protected PropertyExpression()
        {
        }

        public PropertyExpression(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public abstract bool IsMatch(string testValue);

        public override void ReadXml(XmlReader reader)
        {
            string expr = reader.LocalName;
            //bypass expression name node
            Name = reader.GetAttribute(PropertyAttributeName);
            //reader.ReadStartElement();
            //Name = reader.LocalName;
            Value = reader.ReadElementContentAsString();
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(ExpressionName);
            writer.WriteAttributeString(PropertyAttributeName, Name);
            writer.WriteString(Value);
            writer.WriteEndElement();
        }

        public virtual string Name { get; set; }

        public virtual string Value { get; set; }
    }
}
