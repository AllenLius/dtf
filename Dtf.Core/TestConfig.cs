using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;

namespace Dta.Core
{
    public class TestConfig
    {
        //public const string TestConfigFile = "TestConfig.xml";
        private const string TestConfigNodeName = "testconfig";
        private const string EndPointsNodeName = "endpoints";
        private const string EndPointNodeName = "endpoint";
        private const string EndPointNameNodeName = "name";
        private const string EndPointUriNodeName = "uri";
        private const string ParametersNodeName = "parameters";
        private const string ParameterNodeName = "parameter";
        private const string ParameterKeyNodeName = "key";
        private const string ParameterValueNodeName = "value";
        private const string UIMapsNodeName = "uimaps";
        private const string UIMapName = "uimap";
        private const string UIMapNameNodeName = "name";
        private const string UIMapFileNodeName = "file";


        private Dictionary<string, string> m_configs;
        private static TestConfig _instance;

        protected TestConfig(XmlReader reader)
        {
            EndPoints = new Dictionary<string, string>();
            Parameters = new Dictionary<string, string>();
            UIMaps = new Dictionary<string, string>();
            m_configs = new Dictionary<string, string>();
            XDocument xdoc = XDocument.Load(reader);
            //Fill EndPoints
            XElement xRoot = xdoc.Element(TestConfigNodeName);
            var endpoints = from e in xRoot.Element(EndPointsNodeName).Elements(EndPointNodeName)
                            select new KeyValuePair<string, string>(e.Element(EndPointNameNodeName).Value, e.Element(EndPointUriNodeName).Value);
            EndPoints.AddRange(endpoints);
            //Fill Parameters
            var parameters = from e in xRoot.Element(ParametersNodeName).Elements(ParameterNodeName)
                             select new KeyValuePair<string, string>(e.Element(ParameterKeyNodeName).Value, e.Element(ParameterValueNodeName).Value);
            Parameters.AddRange(parameters);

            var uimaps = from e in xRoot.Element(UIMapsNodeName).Elements(UIMapName)
                         select new KeyValuePair<string, string>(e.Element(UIMapNameNodeName).Value, e.Element(UIMapFileNodeName).Value);
            UIMaps.AddRange(uimaps);
        }

        public Dictionary<string, string> EndPoints { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; }

        public string Parameter(string name)
        {
            return m_configs[name];
        }

        public Dictionary<string, string> UIMaps { get; private set; }

        public static TestConfig Load(XmlReader reader)
        {
            if (_instance == null)
            {
                _instance = new TestConfig(reader);
            }
            return _instance;
        }
    }
}
