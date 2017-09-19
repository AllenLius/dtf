using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace Dta.Core
{
    public abstract class UiObjectBase
    {
        public static readonly string[] CommonProperties;
        private static readonly string Common_BoundingRectangle = "Common.BoundingRectangle";
        private static readonly string Common_ControlType = "Common.ControlType";
        public static readonly TimeSpan UITryInterval;
        private static readonly ManualResetEvent Waiter = new ManualResetEvent(false);

        static UiObjectBase()
        {
            CommonProperties = new string[] { Common_BoundingRectangle, Common_ControlType };
            UITryInterval = TimeSpan.FromMilliseconds(500);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        public virtual UiObjectBase FindFirst(string ui)
        {
            Queue<Expression> expressionQueue = new Queue<Expression>();
            Expression expression = ui;
            if (expression is MultipleExpression)
            {
                var multipleExpr = expression as MultipleExpression;
                expressionQueue.EnqueueRange(multipleExpr.Expressions);
            }
            else
            {
                expressionQueue.Enqueue(expression);
            }
            return FindFirst(this, expressionQueue);
        }

        protected virtual UiObjectBase FindFirst(UiObjectBase uiParent, Queue<Expression> expressionQueue)
        {
            Log.Default.Debug(string.Format("<<<expr queue count:{0}", expressionQueue.Count));
            Log.Default.Trace("UIObjectBase/FindFirst");
            Queue<UiObjectBase> searchQueue = new Queue<UiObjectBase>();
            searchQueue.EnqueueRange(uiParent.Children);

            while (expressionQueue.Count > 0)
            {
                Expression expression = expressionQueue.Dequeue();
                while (searchQueue.Count > 0)
                {
                    UiObjectBase uiChild = searchQueue.Dequeue();
                    bool isMatch = Expression.IsMatch(expression, (s) => uiChild.Properties.Contains(s), (s) => uiChild[s]);
                    if (isMatch)
                    {
                        if (expressionQueue.Count == 0)
                        {
                            return uiChild;
                        }
                        else
                        {
                            //var result = FindFirst(uiChild, expressionQueue);
                            //if (result != null)
                            //{
                            //    return result;
                            //}
                            return FindFirst(uiChild, expressionQueue);
                        }
                    }
                    else
                    {
                        searchQueue.EnqueueRange(uiChild.Children);
                    }
                }
            }
            return null;
        }

        public string GetUI()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true
            };
            XmlWriter writer = XmlWriter.Create(sb, settings);
            WriteUI(writer);
            writer.Flush();
            return sb.ToString();
        }

        public virtual UiObjectBase Wait(string ui, TimeSpan timeout)
        {
            DateTime start = DateTime.Now;
            UiObjectBase t = null;
            while (DateTime.Now - start < timeout)
            {
                t = FindFirst(ui);
                if (t != null)
                {
                    return t;
                }
                Waiter.WaitOne(UITryInterval);
            }
            return null;
        }

        public void WriteUI(XmlWriter writer)
        {
            writer.WriteStartElement(ControlType);
            //writer.WriteAttributeString("ID", uixObj.Current.AutomationHandle.ToString());
            foreach (string propName in Properties)
            {
                try
                {
                    string value = this[propName];
                    writer.WriteAttributeString(propName, value);
                }
                catch (Exception ex)
                {
                    Log.Default.LogException(ex);
                }
            }
            //Console.WriteLine("{0} Children count:{1}", uixObj.Current.AutomationHandle, uixObj.Children.Count());
            foreach (UiObjectBase child in Children)
            {
                child.WriteUI(writer);
            }
            writer.WriteEndElement();
        }

        public abstract Rect BoundingRectangle { get; }

        public abstract IEnumerable<UiObjectBase> Children { get; }

        public abstract string ControlType { get; }

        public IDictionary<string, string> GetMetadata()
        {
            Dictionary<string, string> metadata = new Dictionary<string, string>();
            foreach (string propName in Properties)
            {
                try
                {
                    metadata.Add(propName, this[propName]);
                }
                catch (Exception)
                {
                    //Log.Error(ex.GetExceptionText());
                }
            }
            return metadata;
        }

        public abstract UiObjectBase NextSibling { get; }

        public abstract UiObjectBase Parent { get; }

        public abstract UiObjectBase PreviousSibling { get; }

        public virtual IEnumerable<string> Properties
        {
            get
            {
                return CommonProperties;
            }
        }

        public virtual string this[string propertyName]
        {
            get
            {
                if (propertyName == Common_BoundingRectangle)
                {
                    Rect rect = BoundingRectangle;
                    return string.Format("X={0},Y={1},Width={2},Height={3}", rect.X, rect.Y, rect.Width, rect.Height);
                }
                else if (propertyName == Common_ControlType)
                {
                    return ControlType;
                }
                return null;
            }
        }
    }
}