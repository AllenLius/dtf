using System.Collections.Generic;
using System.Xml;

namespace Dta.Core
{
    public abstract class GroupExpression : Expression
    {
        public override void ReadXml(XmlReader reader)
        {
            List<Expression> exprs = new List<Expression>();
            reader.ReadStartElement();
            while (reader.IsStartElement())
            {
                Expression expr = Expression.Create(reader);
                exprs.Add(expr);
            }
            reader.ReadEndElement();
            Expressions = exprs.ToArray();
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(ExpressionName);
            foreach (Expression expr in Expressions)
            {
                expr.WriteXml(writer);
            }
            writer.WriteEndElement();
        }

        public Expression[] Expressions { get; protected set; }
    }
}
