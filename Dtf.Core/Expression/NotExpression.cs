namespace Dta.Core
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "Not")]
    public class NotExpression : Expression
    {
        public NotExpression()
        {
        }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            reader.ReadStartElement();
            reader.EnsureIsStartElement();
            Expression = Expression.Create(reader);            
            reader.ReadEndElement();
        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement(ExpressionName);
            Expression.WriteXml(writer);
            writer.WriteEndElement();
        }

        public Expression Expression { get; private set; }

        protected override Expression Instance
        {
            get { return this; }
        }
    }
}
