using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Dtf.Core
{
    public class DepotUiObject : UiObjectBase
    {
        //private bool m_isVirtualRoot;
        private DepotUiObject m_parent; // null if Root
        private UiElementInfo m_uiElementInfo;
        private DepotUiObject[] m_children;

        private DepotUiObject(DepotUiObject parent, UiElementInfo uiElementInfo)
        {
            m_parent = parent;
            m_uiElementInfo = uiElementInfo;
        }

        public static DepotUiObject Load(Stream stream)
        {
            //if (m_isVirtualRoot)
            //{
            //    return "Root";
            //}
            //return m_xDoc.Root.Attribute(UiInfoFactory.UiElementNameAttributeName).Value;
            var uiInfoFactory = UiInfoFactory.Load(stream);
            DepotUiObject root = new DepotUiObject(null, null);
            Queue<UiElementInfo> uiElementInfoQueue = new Queue<UiElementInfo>();
            uiElementInfoQueue.EnqueueRange(uiInfoFactory.UiElementInfos);
            return root;
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }

        public override Rect BoundingRectangle
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override IEnumerable<UiObjectBase> Children
        {
            get
            {
                if (m_children == null)
                {
                    m_children = m_uiElementInfo.Children.Select(ui => new DepotUiObject(this, ui)).ToArray();
                }
                return m_children;
            }
        }

        public string Name { get; private set; }

        public override string ControlType
        {
            get { throw new NotImplementedException(); }
        }

        public override UiObjectBase NextSibling
        {
            get
            {
                int index = Array.IndexOf(m_children, this) + 1;
                if (index < m_children.Length)
                {
                    return m_children[index];
                }
                return null;
            }
        }

        public override UiObjectBase Parent
        {
            get
            {
                return m_parent;
            }
        }

        public override UiObjectBase PreviousSibling
        {
            get
            {
                int index = Array.IndexOf(m_children, this) - 1;
                if (index > 0)
                {
                    return m_children[index];
                }
                return null;
            }
        }

        public override string ProcessName => throw new NotImplementedException();
    }
}