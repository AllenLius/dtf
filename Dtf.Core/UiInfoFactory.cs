using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dtf.Core
{
    internal class UiInfoFactory
    {
        public const string ResourcesElementNodeName = "Resources";
        public const string UiElementsNodeName = "UiElements";
        public const string ResourceElementNodeName = "Resource";
        public const string ResourceHandlerTypeAttributeName = "HandlerType";
        public const string ResourceKeyAttributeName = "ResourceKey";
        public const string ResourceElementNameAttributeName = "Name";
        public const string UiElementNodeName = "UiElement";
        public const string UiElementNameAttributeName = "Name";
        public const string UiElementPatternsAttributeName = "Patterns";
        public const string UiElementExpressionNodeName = "Expression";
        public const string UiElementChildrenNodeName = "Children";

        public static readonly char[] PatternSeparators = new char[] { ',', '|' };
        public static readonly char[] BindingToken;
        private static readonly Regex BindingRegex;

        Dictionary<string, UiElementInfo> m_uiInfoCaches = new Dictionary<string, UiElementInfo>();
        List<UiElementInfo> m_uiElements = new List<UiElementInfo>();
        List<ResourceInfo> m_resourceInfos = new List<ResourceInfo>();
        static readonly List<Type> PatternTypeList = new List<Type>();

        static UiInfoFactory()
        {
            BindingToken = new char[] { '{', '}' };
            BindingRegex = new Regex("{(?<ref>[^}]+)[}]");

            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach(var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<PatternAttribute>(true) != null)
                {
                    PatternTypeList.Add(type);
                }
            }
        }

        public UiInfoFactory(Stream stream)
        {
            XDocument xdoc = XDocument.Load(stream);
            AddResources(xdoc.Root.Element(ResourcesElementNodeName));
            foreach (var uiElementNode in xdoc.Root.Element(UiElementsNodeName).Elements())
            {
                m_uiElements.Add(AddUiElement(null, uiElementNode));
            }
        }

        public void AddResources(XElement resourcesNode)
        {
            foreach (var resourceNode in resourcesNode.Elements(ResourceElementNodeName))
            {
                string name = resourceNode.Attribute(ResourceElementNameAttributeName).Value;
                string handlerType = resourceNode.Attribute(ResourceHandlerTypeAttributeName).Value;
                string resourceKey = resourceNode.Attribute(ResourceKeyAttributeName).Value;
                ResourceInfo resourceInfo = new ResourceInfo(name, handlerType, resourceKey);
                m_resourceInfos.Add(resourceInfo);
            }
        }

        public UiElementInfo AddUiElement(UiElementInfo parent, XElement uiElementNode)
        {
            string name = uiElementNode.Attribute(UiElementNameAttributeName).Value;
            string patterns = uiElementNode.Attribute(UiElementPatternsAttributeName) == null ? null : uiElementNode.Attribute(UiElementPatternsAttributeName).Value;
            string rawCondition = uiElementNode.Element(UiElementExpressionNodeName).FirstNode.ToString();
            string patternsString = uiElementNode.Attribute(UiElementPatternsAttributeName) == null ? null : uiElementNode.Attribute(UiElementPatternsAttributeName).Value;
            string[] patternNames = new string[0];
            if (patternsString != null)
            {
                patternNames = patternsString.Split(PatternSeparators);
            }
            var patternTypes = PatternTypeList.Where(t => patternNames.Any(n => t.GetCustomAttribute<PatternAttribute>(true).Name.Equals(n))).ToArray();
            UiElementInfo uiElementInfo = new UiElementInfo(name, patternTypes, rawCondition);
            var childrenNode = uiElementNode.Element(UiElementChildrenNodeName);
            if (childrenNode != null)
            {
                foreach (var childNode in childrenNode.Elements())
                {
                    var child = AddUiElement(uiElementInfo, childNode);
                    uiElementInfo.Children.Add(child);
                }
            }
            return uiElementInfo;
        }        

        public static UiInfoFactory Load(Stream stream)
        {
            return new UiInfoFactory(stream);
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
