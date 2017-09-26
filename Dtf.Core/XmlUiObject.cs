using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace Dtf.Core
{
    public class XmlUiObject : UiObjectBase
    {
        public static XmlUiObject Create(System.Xml.XmlReader xmlReader)
        {
            return null;
        }

        public override UiObjectBase Wait(string ui, TimeSpan timeout)
        {
            throw new NotSupportedException();
        }

        public override Rect BoundingRectangle
        {
            get
            {
                return new Rect(0, 0, 0, 0);
            }
        }

        public override IEnumerable<UiObjectBase> Children
        {
            get
            {
                return null;
            }
        }

        public override string ControlType
        {
            get
            {
                return null;
            }
        }

        public override UiObjectBase NextSibling
        {
            get
            {
                return null;
            }
        }

        public override UiObjectBase Parent
        {
            get
            {
                return null;
            }
        }

        public override UiObjectBase PreviousSibling
        {
            get
            {
                return null;
            }
        }

        public override IEnumerable<string> Properties
        {
            get
            {
                return CommonProperties;
            }
        }

        public override string ProcessName => throw new NotImplementedException();

        public override string this[string propertyName]
        {
            get
            {
                if (CommonProperties.Contains(propertyName))
                {
                    return base[propertyName];
                }
                return null;
            }
        }
    }
}