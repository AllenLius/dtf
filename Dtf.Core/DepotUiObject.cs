using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Dta.Core
{
    public class DepotUiObject : UiObjectBase
    {
        //private XDocument m_xDoc;
        //private bool m_isVirtualRoot;
        private DepotUiObject m_parent;
        private List<UiObjectBase> m_children = new List<UiObjectBase>();

        private DepotUiObject(DepotUiObject parent, string name)
        {
            m_parent = parent;
            Name = name;
        }

        public static DepotUiObject Create(XmlReader xmlReader)
        {

            //if (m_isVirtualRoot)
            //{
            //    return "Root";
            //}
            //return m_xDoc.Root.Attribute(UiInfoFactory.UiElementNameAttributeName).Value;
            XDocument xDoc = XDocument.Load(xmlReader);
            DepotUiObject root = new DepotUiObject(null, "Root");
            CreateChildren(root, xDoc);
            return root;
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }

        private static void CreateChildren(DepotUiObject parent, XDocument xDoc)
        {
            var children = xDoc.Element(UiInfoFactory.UiElementsNodeName).Elements(UiInfoFactory.UiElementNodeName);
            if (parent.Parent==null) // root
            {
                children = from e in children
                           where
                               e.Attribute(UiInfoFactory.UiElementParentAttributeName) == null
                           select e;
            }
            else
            {
                children = from e in children
                           where
                               e.Attribute(UiInfoFactory.UiElementParentAttributeName) != null &&
                               e.Attribute(UiInfoFactory.UiElementParentAttributeName).Value.Equals(parent.Name)
                           select e;
            }
                               
            foreach(var child in children)
            {
                var uiObj = new DepotUiObject(parent, child.Attribute(UiInfoFactory.UiElementNameAttributeName).Value);
                parent.m_children.Add(uiObj);
                CreateChildren(uiObj, xDoc);
            }
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
            get { throw new NotImplementedException(); }
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
            get { throw new NotImplementedException(); }
        }
    }
}