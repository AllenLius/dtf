using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using Dtf.Core;
using System.Diagnostics;

namespace Dtf.Endpoint.Win
{
    public class UiaUiObject : UiObjectBase
    {
        //public static readonly string[] BypassProcesses = new string[] { "devenv", "OUTLOOK", "lync" }; // TODO: read list from config file
        bool m_isVirtualRoot = false;
        AutomationElement m_automationElement;
        private const string SupportedPatternsName = "SupportedPatterns";
        private static string[] uiaProperties = new string[] { "AutomationId", "Name", "ProcessId", "ClassName", "ControlType", "FrameworkId", "IsContentElement", "IsControlElement", "IsPassword", "LocalizedControlType" };

        private UiaUiObject()
        {
            m_isVirtualRoot = true;
        }

        public UiaUiObject(AutomationElement automationElement)
        {
            if (automationElement == null)
            {
                throw new ArgumentNullException("automationElement");
            }
            m_automationElement = automationElement;
        }

        public override string ToString()
        {
            string str;
            if (m_isVirtualRoot)
            {
                str = "Desktop";
            }
            else
            {
                str = string.Format("{0} - {1}", this["Name"], this["Common.ControlType"]);
            }
            return str;
        }

        private void AssertVirtualRoot()
        {
            if (m_isVirtualRoot)
            {
                throw new NotSupportedException();
            }
        }

        public override Rect BoundingRectangle
        {
            get
            {
                AutomationElement el = m_isVirtualRoot ? AutomationElement.RootElement : Current;
                var rect = el.Current.BoundingRectangle;
                return new Rect(rect.X, rect.Y, rect.Width, rect.Height);
            }
        }

        public override IEnumerable<UiObjectBase> Children
        {
            get
            {
                if (m_isVirtualRoot)
                {
                    yield return new UiaUiObject(AutomationElement.RootElement);
                }
                else
                {
                    // TODO: GetFirstChild return self?
                    AutomationElement automationElement = null;
                    try
                    {
                        automationElement = WalkerContext.TreeWalker.GetFirstChild(Current);
                    }
                    catch
                    {
                    }
                    while (automationElement != null)
                    {
                        var uiObject = new UiaUiObject(automationElement);
                        bool bypass = false;
                        //try
                        //{
                        //    if ((this.Parent as UiaUiObject).m_isVirtualRoot)
                        //    {
                        //        if (BypassProcesses.Contains(uiObject.ProcessName))
                        //        {
                        //            bypass = true;
                        //        }
                        //    }
                        //}
                        //catch
                        //{
                        //}
                        if (!bypass)
                        {
                            yield return new UiaUiObject(automationElement);
                        }
                        try
                        {
                            automationElement = WalkerContext.TreeWalker.GetNextSibling(automationElement);
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
            }
        }

        public override string ControlType
        {
            get
            {
                return m_isVirtualRoot ? "UiRoot" : this["ControlType"];
            }
        }

        public AutomationElement Current
        {
            get { return m_automationElement; }
        }

        public override UiObjectBase Parent
        {
            get
            {
                if (Current == AutomationElement.RootElement)
                {
                    return UiaUiObject.Root;
                }
                var parentElement = WalkerContext.TreeWalker.GetParent(Current);
                return new UiaUiObject(parentElement);
            }
        }

        public override UiObjectBase NextSibling
        {
            get
            {
                var sibling = WalkerContext.TreeWalker.GetNextSibling(Current);
                return sibling == null ? null : new UiaUiObject(sibling);
            }
        }

        public override IEnumerable<string> Properties
        {
            get
            {
                foreach (string comProp in base.Properties)
                {
                    yield return comProp;
                }
                if (!m_isVirtualRoot)
                {
                    yield return SupportedPatternsName;
                    foreach (var p in uiaProperties)
                    {
                        yield return p;
                    }
                }                
            }
        }

        public override UiObjectBase PreviousSibling
        {
            get
            {
                var sibling = WalkerContext.TreeWalker.GetPreviousSibling(Current);
                return sibling == null ? null : new UiaUiObject(sibling);
            }
        }

        public static UiaUiObject Root
        {
            get { return new UiaUiObject(); }
        }        

        public virtual string SupportedPatterns
        {
            get
            {
                AssertVirtualRoot();
                List<string> patternList = new List<string>();
                var patterns = Current.GetSupportedPatterns();
                patternList.AddRange(patterns.Select((p) => Automation.PatternName(p)));
                return string.Join(" ", patternList);
            }
        }

        static int n = 1;
        public override string ProcessName
        {
            get
            {
                Log.Default.Trace("ProcessName:{0}", n++);
                var name = Process.GetProcessById(m_automationElement.Current.ProcessId).ProcessName;
                return name;
            }
        }

        public override string this[string propertyName]
        {
            get
            {
                try
                {
                    if (CommonProperties.Contains(propertyName))
                    {
                        return base[propertyName];
                    }
                    if (propertyName == SupportedPatternsName)
                    {
                        return SupportedPatterns;
                    }
                    if (CommonProperties.Contains(propertyName))
                    {
                        return base[propertyName];
                    }
                    if (propertyName.Equals("AutomationId"))
                    {
                        return Current.Current.AutomationId;
                    }
                    if (propertyName.Equals("Name"))
                    {
                        return Current.Current.Name;
                    }
                    if (propertyName.Equals("ProcessId"))
                    {
                        return Current.Current.ProcessId.ToString();
                    }
                    if (propertyName.Equals("ClassName"))
                    {
                        return Current.Current.ClassName;
                    }
                    if (propertyName.Equals("ControlType"))
                    {
                        string controlType = Current.Current.ControlType.ProgrammaticName;
                        int dotPos = controlType.IndexOf('.');
                        return controlType.Substring(dotPos + 1);
                    }
                    if (propertyName.Equals("FrameworkId"))
                    {
                        return Current.Current.FrameworkId;
                    }
                    if (propertyName.Equals("IsContentElement"))
                    {
                        return Current.Current.IsContentElement.ToString();
                    }
                    if (propertyName.Equals("IsControlElement"))
                    {
                        return Current.Current.IsControlElement.ToString();
                    }
                    if (propertyName.Equals("IsPassword"))
                    {
                        return Current.Current.IsPassword.ToString();
                    }
                    if (propertyName.Equals("LocalizedControlType"))
                    {
                        return Current.Current.LocalizedControlType;
                    }
                    throw new ArgumentException();
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
