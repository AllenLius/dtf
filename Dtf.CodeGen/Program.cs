namespace Dtf.CodeGen
{
    using Microsoft.CSharp;
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using Dtf.Core;

    class Program
    {
        private const string ArgFile = "file";
        private const string ArgNameSpace = "namespace";
        private const string ArgClass = "class";
        private const string ArgOut = "out";
        private const string ArgProvider = "provider";

        private const string TypeNameResources = "Resources";
        private const string TypeNameUiElements = "UiElements";
        private const string VarNameEndpoint = "endpoint";
        private const string FieldNameEndpoint = "m_" + VarNameEndpoint;
        private const string FieldNameResources = "m_resources";
        private const string FieldNameUi = "m_ui";
        private static readonly string CtorParamNameEndpoint= VarNameEndpoint;
        private const string PropertyNameEndpoint = "Endpoint";
        private static readonly string PropertyNameResources = "Resources";
        private static readonly string PropertyNameUi = "Ui";
        private static readonly string VarNameParent = "parent";
        private const string PatternFactoryMethodNameCreate = "Create";
        private const string UiPropertyName = "Ui";
        private static string PropertyNameFullExpression = nameof(UiElement.FullExpression);
        /// <summary>
        /// [Product]App.App
        /// </summary>
        private static readonly string MultipleExpressionName = Expression.GetExpressionName(typeof(MultipleExpression));
        private const string EndpointMethodQueryInterfaceName = "QueryInterface";
        private const string IAppFactoryMethodCreateName = "Create";
        private const string IAppFactoryMethodCreateParamPathName = "Create";
        private const string IAppFactoryMethodCreateParamNamedParametersName = "namedParameters";
        //private const string UiFieldName = "m_ui";

        static string typeNameApp;
        static CodeNamespace codeNs;
        static CodeTypeDeclaration codeTypeApp;
        static CodeTypeDeclaration codeTypeUiElements;
        static string varNameApp;
        static string fieldNameApp;
        static string PropertyNameApp;
        static HashSet<string> typeNames = new HashSet<string>();
        static HashSet<string> extensionTypeNames = new HashSet<string>(); // Make static class ("public class OkEx" -> "public static class OkEx")

        static void Main(string[] args)
        {
            var namedArgs = GetNamedArgList();
            string uiomFile = null;
            string nameSpace = null;
            string outFile = null;
            string[] providers = null;

            if (!namedArgs.ContainsKey(ArgFile) || !namedArgs.ContainsKey(ArgNameSpace) || !namedArgs.ContainsKey(ArgOut))
            {
                string procName = Process.GetCurrentProcess().ProcessName;
                Console.WriteLine(procName + " /" + ArgFile + ":[UIMapFile] /" + ArgNameSpace + ":[Namespace] /" + ArgClass + ":[ClasName] /" + ArgProvider + ":[Dll List] /" + ArgOut + ":[Destination]");
                Console.WriteLine(procName + " /" + ArgFile + ":[UIMapFile] /" + ArgNameSpace + ":[Namespace] /" + ArgClass + ":[ClasName] /" + ArgProvider + ":\"MyCustomPatterns.dll\" /" + ArgProvider + ":\"MyCustomPatterns.dll\" /" + ArgOut + ":[Destination]");
                Environment.Exit(-1);
            }

            uiomFile = namedArgs["file"].FirstOrDefault();
            nameSpace = namedArgs["namespace"].FirstOrDefault();
            outFile = namedArgs["out"].FirstOrDefault();

            if (namedArgs.ContainsKey("class"))
            {
                typeNameApp = namedArgs["class"].FirstOrDefault();
            }
            else
            {
                typeNameApp = Path.GetFileNameWithoutExtension(uiomFile);
            }

            if (namedArgs.ContainsKey("provider"))
            {
                providers = namedArgs["provider"];
            }

            // add reverse names
            typeNames.Add(typeNameApp);
            typeNames.Add(TypeNameResources);
            typeNames.Add(TypeNameUiElements);

            PropertyNameApp = typeNameApp;

            UiInfoFactory factory = new UiInfoFactory(File.OpenRead(uiomFile)); // uiom information
            
            // namespace [Namespace]
            codeNs = new CodeNamespace(nameSpace);
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            // class in codeNs
            codeTypeApp = new CodeTypeDeclaration(typeNameApp) { Attributes = MemberAttributes.Final | MemberAttributes.Public, IsPartial = true }; // class [ClassName]
            CodeTypeDeclaration codeTypeResources = new CodeTypeDeclaration(TypeNameResources) { Attributes = MemberAttributes.Public }; // class Resources
            codeTypeUiElements = new CodeTypeDeclaration(TypeNameUiElements) { Attributes = MemberAttributes.Final | MemberAttributes.Public }; // class UiElements

            // add types to codeNs
            codeNs.Types.Add(codeTypeApp);
            codeNs.Types.Add(codeTypeResources);
            codeNs.Types.Add(codeTypeUiElements);

            #region Init codeTypeApp 
            CodeMemberField codeFieldEndpoint = new CodeMemberField(typeof(IEndpoint), FieldNameEndpoint); // private Endpoint m_endpoint
            CodeMemberField codeFieldResource = new CodeMemberField(new CodeTypeReference(TypeNameResources), FieldNameResources);
            CodeMemberField codeFieldUi = new CodeMemberField(new CodeTypeReference(TypeNameUiElements), FieldNameUi);
            CodeConstructor codeTypeCtorApp = new CodeConstructor() { Attributes = MemberAttributes.Final | MemberAttributes.Public }; // private [Product]App(Endpoint endpoint)
            CodeMemberProperty codePropertyEndpoint = new CodeMemberProperty() { Name = PropertyNameEndpoint, Attributes = MemberAttributes.Final | MemberAttributes.Public, Type = new CodeTypeReference(typeof(IEndpoint)), HasGet = true };
            CodeMemberProperty codePropertyResources = new CodeMemberProperty() { Name = PropertyNameResources, Attributes = MemberAttributes.Final | MemberAttributes.Public, Type = new CodeTypeReference(TypeNameResources), HasGet = true };
            CodeMemberProperty codePropertyUi = new CodeMemberProperty() { Name = PropertyNameUi, Attributes = MemberAttributes.Final | MemberAttributes.Public, Type = new CodeTypeReference(TypeNameUiElements), HasGet = true };
            codeTypeApp.Members.Add(codeFieldEndpoint); // add m_endpoint field
            codeTypeApp.Members.Add(codeFieldResource);
            codeTypeApp.Members.Add(codeFieldUi);
            codeTypeApp.Members.Add(codeTypeCtorApp); // add ctor
            codeTypeApp.Members.Add(codePropertyEndpoint); // add Endpoint property
            codeTypeApp.Members.Add(codePropertyResources); // add Resources property
            codeTypeApp.Members.Add(codePropertyUi); // add Ui property

            // ctor
            codeTypeCtorApp.Parameters.Add( // add parameter to ctor
                new CodeParameterDeclarationExpression(typeof(IEndpoint), CtorParamNameEndpoint));
            codeTypeCtorApp.Statements.Add( // m_endpoint = endpoint
                new CodeAssignStatement(
                    new CodeVariableReferenceExpression(FieldNameEndpoint),
                    new CodeVariableReferenceExpression(CtorParamNameEndpoint)));
            codeTypeCtorApp.Statements.Add( // m_resources = new Resources(this);
                new CodeAssignStatement(
                    new CodeVariableReferenceExpression(FieldNameResources),
                    new CodeObjectCreateExpression(TypeNameResources, new CodeThisReferenceExpression())));
            codeTypeCtorApp.Statements.Add( // m_ui = new UiElements(this);
                new CodeAssignStatement(
                    new CodeVariableReferenceExpression(FieldNameUi),
                    new CodeObjectCreateExpression(TypeNameUiElements, new CodeThisReferenceExpression())));

            // codePropertyEndpoint
            codePropertyEndpoint.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeVariableReferenceExpression(FieldNameEndpoint)));

            // codePropertyResources
            codePropertyResources.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeVariableReferenceExpression(FieldNameResources)));

            // codePropertyUi
            codePropertyUi.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeVariableReferenceExpression(FieldNameUi)));
            #endregion

            // define field of [appClassName]
            varNameApp = string.Format("{0}{1}", char.ToLower(typeNameApp[0]), typeNameApp.Length > 1 ? typeNameApp.Substring(1) : string.Empty);
            fieldNameApp = "m_" + varNameApp;

            #region Init codeTypeResources
            // field
            codeTypeResources.Members.Add(new CodeMemberField(typeNameApp, fieldNameApp));

            // ctor
            CodeConstructor codeCtorResources = new CodeConstructor() { Attributes = MemberAttributes.Final | MemberAttributes.Public };
            codeTypeResources.Members.Add(codeCtorResources);
            codeCtorResources.Parameters.Add(new CodeParameterDeclarationExpression(typeNameApp, varNameApp));
            codeCtorResources.Statements.Add(
                new CodeAssignStatement(
                    new CodeVariableReferenceExpression(fieldNameApp),
                    new CodeVariableReferenceExpression(varNameApp)));
            //codeCtorResources.Statements.Add( // m_resourceManager = [fieldNameApp].Endpoint.QueryInterface<IResourceManagerFactory>().Create();
            //    new CodeAssignStatement(
            //        new CodeVariableReferenceExpression(FieldNameResourceManager), // m_resourceManager
            //        new CodeMethodInvokeExpression( // Create()
            //            new CodeMethodReferenceExpression( // QueryInterface<IResourceManagerFactory>()
            //                new CodeMethodInvokeExpression(
            //                    new CodeMethodReferenceExpression(
            //                        new CodeFieldReferenceExpression( // Endpoint
            //                            new CodeVariableReferenceExpression(fieldNameApp), // fieldNameApp
            //                            PropertyNameEndpoint), // [fieldNameApp]
            //                        EndpointMethodQueryInterfaceName, // QueryInterface
            //                        new CodeTypeReference(typeof(IResourceManagerFactory)))), // generic type parameter
            //            PatternFactoryMethodNameCreate))));

            // add resource
            foreach (var res in factory.ResourceInfos)
            {
                CodeMemberProperty codePropertyResource = new CodeMemberProperty() { Attributes = MemberAttributes.Final | MemberAttributes.Public, Name = res.Name, Type = new CodeTypeReference(typeof(object)) };
                codePropertyResource.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(
                                new CodeTypeReferenceExpression(typeof(ResourceManager)),
                                nameof(ResourceManager.GetObject)),
                            new CodePrimitiveExpression(res.HandlerType), new CodePrimitiveExpression(res.ResourceKey))));
                codeTypeResources.Members.Add(codePropertyResource); // add to Resources type
            }
            #endregion

            #region Init codeTypeUiElements
            codeTypeUiElements.Members.Add(new CodeMemberField(typeNameApp, fieldNameApp));
            CodeConstructor codeTypeCtorUiElements = new CodeConstructor() { Attributes = MemberAttributes.Final | MemberAttributes.Public };
            codeTypeUiElements.Members.Add(codeTypeCtorUiElements);
            codeTypeCtorUiElements.Parameters.Add(new CodeParameterDeclarationExpression(typeNameApp, varNameApp));
            codeTypeCtorUiElements.Statements.Add(
                new CodeAssignStatement(
                    new CodeVariableReferenceExpression(fieldNameApp),
                    new CodeVariableReferenceExpression(varNameApp)));
            #endregion

            // add UI class and property
            foreach (var uiInfo in factory.UiElementInfos)
            {
                AddUiElement(codeTypeUiElements, uiInfo);
            }

            CSharpCodeProvider cs = new CSharpCodeProvider();
            StringWriter sw = new StringWriter();
                        
            CodeGeneratorOptions codeGenOptions = new CodeGeneratorOptions() { BracingStyle = "C" };
            cs.GenerateCodeFromNamespace(codeNs, sw, codeGenOptions);
            sw.Flush();
            string code = sw.ToString();

            // make static class
            foreach(var typeName in extensionTypeNames)
            {
                code = code.Replace("\r\n    public class " + typeName, "\r\n    public static class " + typeName);
            }

            File.WriteAllText(outFile, code);
        }

        /// <summary>
        /// Add UiElement class and instance
        /// </summary>
        /// <param name="uiInfo"></param>
        private static void AddUiElement(CodeTypeDeclaration targetType, UiElementInfo uiInfo)
        {
            string uiName = uiInfo.Name;
            int n = 2;
            while (typeNames.Contains(uiName))
            {
                uiName = uiInfo.Name + n.ToString();
                n++;
            }
            typeNames.Add(uiName);

            // add UiElement type
            CodeTypeDeclaration codeTypeUiElement = new CodeTypeDeclaration() { Name = uiName };
            codeNs.Types.Add(codeTypeUiElement); // add UI to namespace
            CodeConstructor codeCtorUiElement = new CodeConstructor() { Attributes = MemberAttributes.Final | MemberAttributes.Public };
            codeTypeUiElement.Members.Add(new CodeMemberField(typeNameApp, fieldNameApp));
            codeTypeUiElement.Members.Add(codeCtorUiElement);
            codeCtorUiElement.Parameters.Add(new CodeParameterDeclarationExpression(typeof(UiElement), VarNameParent));
            codeCtorUiElement.Parameters.Add(new CodeParameterDeclarationExpression(typeNameApp, varNameApp));
            codeCtorUiElement.BaseConstructorArgs.Add(new CodeSnippetExpression(GetFormattedExpressionString(uiInfo.Condition.ToString())));
            codeCtorUiElement.BaseConstructorArgs.Add(new CodeVariableReferenceExpression(VarNameParent));
            codeCtorUiElement.Statements.Add(
                new CodeAssignStatement(
                    new CodeVariableReferenceExpression(fieldNameApp),
                    new CodeVariableReferenceExpression(varNameApp)));

            // extension class
            CodeTypeDeclaration codeTypeEx = new CodeTypeDeclaration(uiName + "Ex");
            typeNames.Add(codeTypeEx.Name);
            extensionTypeNames.Add(codeTypeEx.Name);
            codeNs.Types.Add(codeTypeEx);

            // inherit UiElement
            codeTypeUiElement.BaseTypes.Add(new CodeTypeReference(typeof(UiElement)));

            // add and implement IUiInspector
            ImplementInterface(uiName, codeTypeEx, typeof(IUiInspectorFactory), typeof(IUiInspector), false);

            // add click
            ImplementInterface(uiName, codeTypeEx, typeof(IPatternFactory), typeof(IMousePattern), true);

            // implement patterns
            foreach (var type in uiInfo.Patterns)
            {
                ImplementInterface(uiName, codeTypeEx, typeof(IPatternFactory), type, true);
            }

            // add app property
            CodeMemberProperty codePropertyApp = new CodeMemberProperty() { Attributes = MemberAttributes.Public | MemberAttributes.Final, Name = PropertyNameApp, Type = new CodeTypeReference(typeNameApp) };
            codeTypeUiElement.Members.Add(codePropertyApp);
            codePropertyApp.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                    fieldNameApp)));

            // add UI propety to UiElements
            CodeMemberProperty codePropertyUiElement = new CodeMemberProperty() { Attributes = MemberAttributes.Final | MemberAttributes.Public, Name = uiInfo.Name, Type = new CodeTypeReference(uiName) };
            targetType.Members.Add(codePropertyUiElement);
            CodeExpression uiElementParent = targetType.Name.Equals(TypeNameUiElements) ? new CodePrimitiveExpression(null) as CodeExpression : new CodeThisReferenceExpression() as CodeExpression;
            codePropertyUiElement.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeObjectCreateExpression(codePropertyUiElement.Type, uiElementParent, new CodeVariableReferenceExpression(fieldNameApp))
                )
            );

            // add children
            foreach(var child in uiInfo.Children)
            {
                AddUiElement(codeTypeUiElement, child);
            }
        }

        /// <summary>
        /// Implement interface method by using extension method.
        /// </summary>
        /// <param name="typeName">UI type</param>
        /// <param name="codeTypeEx">Extension type</param>
        /// <param name="factoryType">Factory</param>
        /// <param name="implementType">Interface to implement</param>
        /// <param name="genericCreate">If Create method is generic</param>
        static void ImplementInterface(string typeName, CodeTypeDeclaration codeTypeEx, Type factoryType, Type implementType, bool genericCreate)
        {
            foreach (var memberInfo in implementType.GetMembers())
            {
                if (memberInfo is MethodInfo)
                {
                    var methodInfo = memberInfo as MethodInfo;
                    var methodName = methodInfo.Name;
                    CodeMemberMethod codeMethod = new CodeMemberMethod()
                    {
                        Name = methodName,
                        Attributes = MemberAttributes.Final | MemberAttributes.Public | MemberAttributes.Static,
                        ReturnType = new CodeTypeReference(methodInfo.ReturnType)
                    };
                    string varTypeName = char.ToLower(typeName[0]).ToString() + string.Join(string.Empty, typeName.Skip(1).ToArray());
                    codeTypeEx.Members.Add(codeMethod);
                    codeMethod.Parameters.Add(new CodeParameterDeclarationExpression("this " + typeName, varTypeName));
                    foreach (var p in methodInfo.GetParameters())
                    {
                        CodeParameterDeclarationExpression parameter = new CodeParameterDeclarationExpression(new CodeTypeReference(p.ParameterType), p.Name);
                        codeMethod.Parameters.Add(parameter);
                    }

                    CodePropertyReferenceExpression endpointField = new CodePropertyReferenceExpression(new CodeVariableReferenceExpression(fieldNameApp), PropertyNameEndpoint);
                    CodeMethodInvokeExpression queryMethod = new CodeMethodInvokeExpression(
                        new CodeMethodReferenceExpression(
                            new CodePropertyReferenceExpression(
                                new CodePropertyReferenceExpression(
                                    new CodeVariableReferenceExpression(varTypeName),
                                    PropertyNameApp),
                                PropertyNameEndpoint),
                            EndpointMethodQueryInterfaceName,
                            new CodeTypeReference(factoryType)));

                    CodeMethodReturnStatement queryReturnStatment = new CodeMethodReturnStatement(queryMethod);
                    CodeMethodReferenceExpression codeMethodCreateRef = new CodeMethodReferenceExpression(queryReturnStatment.Expression,
                            PatternFactoryMethodNameCreate);
                    if (genericCreate)
                    {
                        codeMethodCreateRef.TypeArguments.Add(new CodeTypeReference(implementType));
                    }
                    CodeMethodInvokeExpression createMethod = new CodeMethodInvokeExpression(
                        codeMethodCreateRef,
                        new CodePropertyReferenceExpression(
                            new CodeVariableReferenceExpression(varTypeName),
                            PropertyNameFullExpression));
                            


                    List<CodeVariableReferenceExpression> varList = new List<CodeVariableReferenceExpression>();
                    for (int i = 1; i < codeMethod.Parameters.Count; i++)
                    {
                        CodeParameterDeclarationExpression p = codeMethod.Parameters[i];
                        CodeVariableReferenceExpression varExpr = new CodeVariableReferenceExpression(p.Name);
                        varList.Add(varExpr);
                    } 

                    CodeMethodInvokeExpression callMethod = new CodeMethodInvokeExpression(
                        createMethod,
                        methodName,
                        varList.ToArray());

                    if (methodInfo.ReturnType == typeof(void))
                    {
                        codeMethod.Statements.Add(callMethod);
                    }
                    else
                    {
                        CodeMethodReturnStatement createReturnStatment = new CodeMethodReturnStatement(callMethod);
                        codeMethod.Statements.Add(createReturnStatment);
                    }
                }
            }
        }

        /// <summary>
        /// Fill resource
        /// </summary>
        /// <param name="value">{{=>{; Hi {Res}=>"Hi " + Res; Hi {{res}=> Hi {res}; Hi {{{res}=> "Hi {" +str</param>
        /// <returns>Formated string</returns>
        static string GetFormattedExpressionString(string value)
        {
            value = value.Replace("\"", "\\\"");
            StringBuilder result = new StringBuilder();
            result.Append("\"");
            StringBuilder refId = new StringBuilder();
            bool hasStartToken = false;
            foreach (char c in value)
            {
                if (hasStartToken)
                {
                    if (c == '{')
                    {
                        result.Append(c);
                        hasStartToken = false;
                        continue;
                    }
                    if (c == '}')
                    {
                        //GET refId & assest refId is not empty
                        if (refId.ToString() == string.Empty)
                        {
                            throw new FormatException();
                        }
                        //Append formatted str
                        result.Append(string.Format("\" + {0}.{1}.{2} + \"", varNameApp, PropertyNameResources, refId.ToString()));
                        refId.Clear();
                        hasStartToken = false;
                        continue;
                    }
                    else
                    {
                        refId.Append(c);
                    }
                }
                else if (c == '{')
                {
                    hasStartToken = true;
                    continue;
                }
                else
                {
                    result.Append(c);
                }
            }
            if (hasStartToken)
            {
                throw new FormatException();
            }
            result.Append("\"");
            return result.ToString();
        }

        /// <summary>
        /// Get the command args like /output:abc"
        /// </summary>
        /// <returns></returns>
        static Dictionary<string, string[]> GetNamedArgList()
        {
            Dictionary<string, string[]> namedArgs = new Dictionary<string, string[]>();
            string[] args = Environment.GetCommandLineArgs();

            foreach (string arg in args)
            {
                Regex regex = new Regex("[/{1}](?<key>[^:]+):(?<value>[^\"]*)");
                System.Text.RegularExpressions.Match match = regex.Match(arg);
                if (match.Success)
                {
                    string strKey = match.Result("${key}").ToLower();
                    string strValue = match.Result("${value}");
                    strKey = strKey.Trim('"');	//Remove ?
                    string[] values;
                    if (!namedArgs.TryGetValue(strKey, out values))
                    {
                        values = new string[0];
                        namedArgs.Add(strKey, values);
                    }
                    string[] newValues = new string[values.Length + 1];
                    values.CopyTo(newValues, 0);
                    newValues[values.Length] = strValue;
                    namedArgs[strKey] = newValues;
                }
            }
            return namedArgs;
        }
    }
}
