using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dtf.Core
{
    public abstract class Expression : IXmlSerializable
    {
        public Expression()
        {
        }

        public static implicit operator Expression(string xml)
        {
            StringReader sr = new StringReader(xml);
            XmlReader reader = XmlReader.Create(sr);
            reader.Read();
            return Create(reader);
        }

        public static implicit operator string(Expression expression)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true
            };
            XmlWriter writer = XmlWriter.Create(sb, settings);
            Type type = expression.GetType();
            expression.WriteXml(writer);
            writer.Flush();
            return sb.ToString();
        }

        public static Expression Create(XmlReader reader)
        {
            Type exprType = typeof(Expression);
            string exprName = reader.LocalName;
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type rootExprType = (from t in assembly.GetTypes()
                                 where
                                    exprType.IsAssignableFrom(t) && GetExpressionName(t) == exprName
                                 select t).FirstOrDefault();
            if (rootExprType == null)
            {
                throw new NotSupportedException(string.Format("Expression {0} is not support!", exprName));
            }
            Expression instance = CreateInstance(rootExprType);
            instance.ReadXml(reader);
            return instance;
        }

        //public static string Clean(string xml)
        //{
        //    //xml = "<And xmlns=\"http://schemas.datacontract.org/2004/07/Dtf.Core\" />";
        //    XElement root = XElement.Parse(xml);
        //    XElement result = Clean(root);
        //    return result.ToString();
        //}

        public static string GetExpressionName(Type type)
        {
            XmlRootAttribute attr = type.GetCustomAttributes(typeof(XmlRootAttribute), false).FirstOrDefault() as XmlRootAttribute;
            return attr == null ? null : attr.ElementName;
        }

        public static bool IsMatch(Expression expression, Func<string, bool> propertyExistsCallback, Func<string, string> propertyFetchCallbalk)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            if (propertyExistsCallback == null)
            {
                throw new ArgumentNullException("propertyExistsCallback");
            }
            if (propertyFetchCallbalk == null)
            {
                throw new ArgumentNullException("propertyFetchCallbalk");
            }
            if (expression is PropertyExpression)
            {
                PropertyExpression propExpr = (PropertyExpression)expression;
                if (!propertyExistsCallback(propExpr.Name))
                {
                    return false;
                }
                try
                {
                    string actualValue = propertyFetchCallbalk(propExpr.Name);
                    return propExpr.IsMatch(actualValue);
                }
                catch (Exception)
                {
                }
                return false;
            }
            else if (expression is NotExpression)
            {
                NotExpression notExpr = (NotExpression)expression;
                return !IsMatch(notExpr.Expression, propertyExistsCallback, propertyFetchCallbalk);
            }
            else if (expression is AndExpression)
            {
                AndExpression andExpr = (AndExpression)expression;
                foreach (Expression expr in andExpr.Expressions)
                {
                    if (!IsMatch(expr, propertyExistsCallback, propertyFetchCallbalk))
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (expression is OrExpression)
            {
                OrExpression orExpr = (OrExpression)expression;
                foreach (Expression expr in orExpr.Expressions)
                {
                    if (IsMatch(expr, propertyExistsCallback, propertyFetchCallbalk))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        //private static XElement Clean(XElement element)
        //{
        //    if (!element.HasElements)
        //    {
        //        XElement xElement = new XElement(element.Name.LocalName);
        //        xElement.Value = element.Value;
        //        return xElement;
        //    }
        //    var content = element.Elements().Select(el => Clean(el));
        //    return new XElement(element.Name.LocalName, content);
        //}

        protected static Expression CreateInstance(Type type)
        {
            ConstructorInfo ctor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault();
            if (ctor == null)
            {
                throw new Exception("No paramerterless constructor defined!");
            }
            return (Expression)ctor.Invoke(new object[] { });
        }
        //public override string ToString()
        //{
        //    return Instance;
        //}

        protected abstract Expression Instance { get; }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public abstract void ReadXml(XmlReader reader);

        public abstract void WriteXml(XmlWriter writer);

        public override string ToString()
        {
            return Instance;
        }

        protected string ExpressionName
        {
            get
            {
                return GetExpressionName(Instance.GetType());
            }
        }

    }
}
