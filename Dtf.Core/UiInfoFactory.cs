using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dta.Core
{
    internal class UiInfoFactory
    {
        public const string UiElementsNodeName = "UiElements";
        public const string ResourcesElementNodeName = "Resources";
        public const string ResourceElementNodeName = "Resource";
        public const string ResourceHandlerTypeAttributeName = "HandlerType";
        public const string ResourceKeyAttributeName = "ResourceKey";
        public const string ResourceElementNameAttributeName = "Name";
        public const string UiElementNodeName = "UiElement";
        public const string UiElementNameAttributeName = "Name";
        public const string UiElementParentAttributeName = "Parent";
        public const string UiElementPatternsAttributeName = "Patterns";
        public static readonly char[] PatternSeparators = new char[] { ',', '|' };
        public static readonly char[] BindingToken;
        private static readonly Regex _BindingRegex;

        Dictionary<string, UiElementInfo> m_uiInfoCaches = new Dictionary<string, UiElementInfo>();
        List<UiElementInfo> m_uiElements = new List<UiElementInfo>();
        List<ResourceInfo> m_resourceInfos = new List<ResourceInfo>();

        static UiInfoFactory()
        {
            BindingToken = new char[] { '{', '}' };
            _BindingRegex = new Regex("{(?<ref>[^}]+)[}]");
        }

        public UiInfoFactory(Stream stream)
        {
            XDocument xdoc = XDocument.Load(stream);
            InitResources(xdoc);
            InitUiElements(xdoc);
        }

        public void InitResources(XDocument xdoc)
        {
            foreach (var element in xdoc.Descendants().Where(e => e.Name.LocalName == ResourceElementNodeName))
            {
                string name = element.Attribute(ResourceElementNameAttributeName).Value;
                string handlerType = element.Attribute(ResourceHandlerTypeAttributeName).Value;
                string resourceKey = element.Attribute(ResourceKeyAttributeName).Value;
                ResourceInfo resourceInfo = new ResourceInfo(name, handlerType, resourceKey);
                m_resourceInfos.Add(resourceInfo);
            }
        }

        public void InitUiElements(XDocument xdoc)
        {
            var elements = from e in xdoc.Descendants()
                           where
                               e.Name.LocalName == UiElementNodeName  && e.Attribute(UiElementParentAttributeName) == null
                           select e;

            Queue<XElement> elementQueue = new Queue<XElement>(elements);
            while (elementQueue.Count > 0)
            {
                XElement element = elementQueue.Dequeue();
                string name = element.Attribute(UiElementNameAttributeName).Value;
                string parentName = element.Attribute(UiElementParentAttributeName) == null ? null : element.Attribute(UiElementParentAttributeName).Value;
                string patterns = element.Attribute(UiElementPatternsAttributeName) == null ? null : element.Attribute(UiElementPatternsAttributeName).Value;
                string rawCondition = element.FirstNode.ToString();
                string patternsString = element.Attribute(UiElementPatternsAttributeName) == null ? null : element.Attribute(UiElementPatternsAttributeName).Value;
                string[] patternNames = new string[0];
                if (patternsString != null)
                {
                    patternNames = patternsString.Split(PatternSeparators);
                }
                List<Type> patternTypeList = new List<Type>();
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type[] patternTypes = (from t in assembly.GetTypes()
                                    where
                                       t.GetCustomAttribute<PatternAttribute>(true) != null
                                    select t).ToArray();
                foreach (var patternName in patternNames)
                {
                    Type patternType = patternTypes.FirstOrDefault(t => t.GetCustomAttribute<PatternAttribute>(true).Name == patternName);
                    if (patternType != null)
                    {
                        patternTypeList.Add(patternType);
                    }
                }
                UiElementInfo parent = m_uiElements.FirstOrDefault(e => e.Name == parentName);
                UiElementInfo uiElement = new UiElementInfo(parent, name, patternTypeList.ToArray(), rawCondition);
                m_uiElements.Add(uiElement);
                elementQueue.EnqueueRange(xdoc.Descendants().Where(e => e.Attribute("Parent") != null && e.Attribute("Parent").Value == name));
            }
        }

        public ResourceInfo[] ResourceInfos
        {
            get
            {
                return m_resourceInfos.ToArray();
            }
        }

        public UiElementInfo[] UiElementInfos
        {
            get
            {
                return m_uiElements.ToArray();
            }
        }
    }
}
