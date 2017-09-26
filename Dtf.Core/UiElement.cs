using System.Collections.Generic;

namespace Dtf.Core
{
    /// <summary>
    /// UI element
    /// </summary>
    public class UiElement
    {
        static readonly string MultipleExpressionName = Dtf.Core.Expression.GetExpressionName(typeof(Dtf.Core.MultipleExpression));
        private Dtf.Core.Expression m_expression;
        private Dtf.Core.Expression m_fullExpression;
        private UiElement m_parent;

        public UiElement(Dtf.Core.Expression expression, UiElement parent)
        {
            m_expression = expression;
            m_parent = parent;
        }

        /// <summary>
        /// Get full path (cascaded) of current UI expression
        /// </summary>
        /// <returns></returns>
        protected Dtf.Core.Expression GetFullExpression()
        {
            Stack<string> path = new Stack<string>();
            var uiElement = this;
            while(uiElement != null)
            {
                path.Push(uiElement.m_expression.ToString());
                uiElement = uiElement.Parent;
            }
            var pathStr = string.Join(string.Empty, path.ToArray());
            return string.Format("<{0}>{1}</{0}>", MultipleExpressionName, pathStr);
        }

        /// <summary>
        /// Expression of current UI element
        /// </summary>
        public Dtf.Core.Expression Expression
        {
            get
            {
                return m_expression;
            }
        }

        public Dtf.Core.Expression FullExpression
        {
            get
            {
                //if (m_fullExpression == null)
                {
                    m_fullExpression = GetFullExpression();
                }
                return m_fullExpression;
            }
        }

        /// <summary>
        /// Parent UI element
        /// </summary>
        public UiElement Parent
        {
            get { return m_parent; }
        }
    }
}
