using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dta.CodeGen
{
    using Microsoft.CSharp;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using Dta.Core;
    using System.Reflection;

    class Program
    {
        private const string ArgFile = "file";
        private const string ArgNameSpace = "namespace";
        private const string ArgClass = "class";
        private const string ArgOut = "out";
        private const string ArgProvider = "provider";

        /// <summary>
        /// [Product]App.App
        /// </summary>
        private const string UiElements = "UiElements";
        private const string UiPropertyName = "Ui";
        private const string UiFullPropertyName = "UiFull";
        private static readonly string MultipleExpressionName = Expression.GetExpressionName(typeof(MultipleExpression));
        private const string EndpointPropertyName = "Endpoint";
        private const string EndpointMethodQueryInterfaceName = "QueryInterface";
        private const string IAppFactoryMethodCreateName = "Create";
        private const string IAppFactoryMethodCreateParamPathName = "Create";
        private const string PatternFactoryCreateMethodName = "Create";
        private const string IAppFactoryMethodCreateParamNamedParametersName = "namedParameters";
        private const string CtorParamEndpointName = "endpoint";
        //private const string UiFieldName = "m_ui";
        private const string EndpointFieldName = "m_endpoint";
        private static CodeParameterDeclarationExpression appFieldParameter;
        private static CodeMemberField appField;

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

            string className;
            if (namedArgs.ContainsKey("class"))
            {
                className = namedArgs["class"].FirstOrDefault();
            }
            else
            {
                className = Path.GetFileNameWithoutExtension(uiomFile);
            }

            if (namedArgs.ContainsKey("provider"))
            {
                providers = namedArgs["provider"];
            }

            string name = string.Format("{0}{1}", char.ToLower(className[0]), className.Length > 1 ? className.Substring(1) : string.Empty);
            appField = new CodeMemberField(
                    new CodeTypeReference(className),
                    string.Format("m_{0}", name));

            appFieldParameter = new CodeParameterDeclarationExpression(className, name);

            CodeNamespace codeNs = new CodeNamespace(nameSpace);
            CodeTypeDeclaration codeTypeUi = new CodeTypeDeclaration(className); // [Product]App
            codeTypeUi.IsPartial = true;
            CodeTypeDeclaration codeTypeUiElements = new CodeTypeDeclaration(UiElements);
            CodeMemberField codeFieldUiEndpoint = new CodeMemberField(typeof(Endpoint), EndpointFieldName); // private Endpoint m_endpoint
            CodeConstructor codeTypeUiCtor = new CodeConstructor(); // private [Product]App(Endpoint endpoint)

            codeNs.Types.Add(codeTypeUi);
            codeTypeUi.Members.Add(codeFieldUiEndpoint);
            codeTypeUi.Members.Add(codeTypeUiCtor);
            codeTypeUi.Members.Add(codeTypeUiElements);
            
            var codeCtorParamEndpoint = new CodeParameterDeclarationExpression(
                new CodeTypeReference(typeof(Endpoint)),
                CtorParamEndpointName
            );
            codeTypeUiCtor.Attributes = MemberAttributes.Public;
            codeTypeUiCtor.Parameters.Add(codeCtorParamEndpoint);
            codeTypeUiCtor.Statements.Add(
                new CodeAssignStatement(
                    new CodeVariableReferenceExpression(EndpointFieldName),
                    new CodeVariableReferenceExpression(CtorParamEndpointName)
                )
            );

            CodeMemberProperty endpointProperty = new CodeMemberProperty();
            endpointProperty.Name = EndpointPropertyName;
            endpointProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            endpointProperty.Type = new CodeTypeReference(typeof(Endpoint));
            endpointProperty.HasGet = true;
            endpointProperty.GetStatements.Add(
                new CodeMethodReturnStatement(new CodeVariableReferenceExpression(EndpointFieldName)));
            codeTypeUi.Members.Add(endpointProperty);            

            var invokeQueryInterface = new CodeMethodInvokeExpression(
                new CodeMethodReferenceExpression(
                    new CodeVariableReferenceExpression(EndpointPropertyName),
                    EndpointMethodQueryInterfaceName
                )
            );

            var invokeQueryInterfaceCreate = new CodeMethodInvokeExpression(
                new CodeMethodReferenceExpression(
                    invokeQueryInterface,
                    IAppFactoryMethodCreateName
                ),
                new CodeParameterDeclarationExpression(
                    new CodeTypeReference(typeof(string)),
                    IAppFactoryMethodCreateParamNamedParametersName
                )
            );

            UiInfoFactory factory = new UiInfoFactory(File.OpenRead(uiomFile));

            // add resource
            foreach(var res in factory.ResourceInfos)
            {
                CodeMemberProperty codePropertyRes = new CodeMemberProperty();
                codePropertyRes.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                codePropertyRes.Name = res.Name;
                codePropertyRes.Type = new CodeTypeReference(typeof(object));
                codePropertyRes.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(
                                new CodeTypeReferenceExpression(typeof(ResourceManager)), "GetObject"
                            ),
                            new CodePrimitiveExpression(res.HandlerType), new CodePrimitiveExpression(res.ResourceKey)
                        )
                    )
                );
                codeTypeUi.Members.Add(codePropertyRes);
            }

            // add UI class and property
            foreach (var uiInfo in factory.UiElementInfos)
            {
                CodeTypeDeclaration codeTypeUiElement = new CodeTypeDeclaration()
                {
                    Name = uiInfo.Name
                };
                CodeConstructor codeTypeUiElementCtor = new CodeConstructor()
                {
                    Attributes = MemberAttributes.Public
                };
                codeTypeUiElementCtor.Parameters.Add(appFieldParameter);
                codeTypeUiElementCtor.Statements.Add(
                    new CodeAssignStatement(
                        new CodeVariableReferenceExpression(appField.Name),
                        new CodeVariableReferenceExpression(appFieldParameter.Name)
                    )
                );
                CodeMemberProperty codePropertyUiElement = new CodeMemberProperty();

                codeTypeUiElements.Members.Add(codeTypeUiElement); // add UI type
                codeTypeUiElement.Members.Add(appField);
                codeTypeUiElement.Members.Add(codeTypeUiElementCtor);
                codeTypeUi.Members.Add(codePropertyUiElement); // add UI property

                codePropertyUiElement.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                codePropertyUiElement.Type = new CodeTypeReference(codeTypeUiElements.Name + "." + uiInfo.Name);
                codePropertyUiElement.Name = uiInfo.Name;
                codePropertyUiElement.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeObjectCreateExpression(codePropertyUiElement.Type, new CodeThisReferenceExpression())
                    )
                );

                // add and implement IUiInspector
                ImplementInterface(codeTypeUiElement, typeof(IUiInspectorFactory), typeof(IUiInspector), null);

                // add click
                ImplementInterface(codeTypeUiElement, typeof(IPatternFactory), typeof(IMousePattern), typeof(IMousePattern));

                foreach (var type in uiInfo.Patterns)
                {
                    ImplementInterface(codeTypeUiElement, typeof(IPatternFactory), type, type);
                }

                // add Ui property
                CodeMemberProperty uiProperty = new CodeMemberProperty();
                codeTypeUiElement.Members.Add(uiProperty);
                uiProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                uiProperty.Type = new CodeTypeReference(typeof(Expression));
                uiProperty.Name = UiPropertyName;
                uiProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeSnippetExpression(ParserValue(uiInfo.Condition.ToString()))));
                
                // add UiFull property
                CodeMemberProperty uiFullProperty = new CodeMemberProperty();
                codeTypeUiElement.Members.Add(uiFullProperty);
                uiFullProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                uiFullProperty.Type = new CodeTypeReference(typeof(Expression));
                uiFullProperty.Name = UiFullPropertyName;
                string uiFull = string.Empty;
                var uiInfoT = uiInfo;
                do
                {
                    uiFull = uiInfoT.Condition.ToString() + uiFull;
                    uiInfoT = uiInfoT.Parent;
                }
                while (uiInfoT != null);
                uiFull = "<" + MultipleExpressionName + ">" + uiFull + "</" + MultipleExpressionName + ">";
                uiFull = ParserValue(uiFull);
                uiFullProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeSnippetExpression(uiFull)));
            }

            //UiInfoFactory.

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            codeNs.Imports.Add(new CodeNamespaceImport("System.IO"));
            codeNs.Imports.Add(new CodeNamespaceImport("Dta.Core"));
            
            CSharpCodeProvider cs = new CSharpCodeProvider();
            StringWriter sw = new StringWriter();
                        
            CodeGeneratorOptions codeGenOptions = new CodeGeneratorOptions() { BracingStyle = "C" };
            cs.GenerateCodeFromNamespace(codeNs, sw, codeGenOptions);
            sw.Flush();
            string code = sw.ToString();
            File.WriteAllText(outFile, code);
        }

        /// <summary>
        /// Add interface implement of type
        /// </summary>
        /// <param name="codeTypeUi">Type to implement</param>
        /// <param name="queryType">Template type of QueryInterface</param>
        /// <param name="implementType">Interface to implement</param>
        /// <param name="createType">Template type of Create</param>
        static void ImplementInterface(CodeTypeDeclaration codeTypeUi, Type queryType, Type implementType, Type createType)
        {
            codeTypeUi.BaseTypes.Add(implementType);
            foreach (var memberInfo in implementType.GetMembers())
            {
                if (memberInfo is MethodInfo)
                {
                    var methodInfo = memberInfo as MethodInfo;
                    var methodName = methodInfo.Name;
                    CodeMemberMethod codeMethod = new CodeMemberMethod()
                    {
                        Name = methodName,
                        Attributes = MemberAttributes.Public | MemberAttributes.Final,
                        ReturnType = new CodeTypeReference(methodInfo.ReturnType)
                    };
                    codeTypeUi.Members.Add(codeMethod);
                    foreach (var p in methodInfo.GetParameters())
                    {
                        CodeParameterDeclarationExpression parameter = new CodeParameterDeclarationExpression(new CodeTypeReference(p.ParameterType), p.Name);
                        codeMethod.Parameters.Add(parameter);
                    }

                    CodePropertyReferenceExpression endpointField = new CodePropertyReferenceExpression(new CodeVariableReferenceExpression(appField.Name), EndpointPropertyName);
                    CodeMethodInvokeExpression queryMethod = new CodeMethodInvokeExpression(
                        new CodeMethodReferenceExpression(
                            endpointField,
                            EndpointMethodQueryInterfaceName,
                            new CodeTypeReference(queryType)));

                    CodeMethodReturnStatement queryReturnStatment = new CodeMethodReturnStatement(queryMethod);
                    CodeMethodReferenceExpression codeMethodCreateRef = new CodeMethodReferenceExpression(queryReturnStatment.Expression,
                            PatternFactoryCreateMethodName);
                    if (createType != null)
                    {
                        codeMethodCreateRef.TypeArguments.Add(new CodeTypeReference(createType));
                    }
                    CodeMethodInvokeExpression createMethod = new CodeMethodInvokeExpression(codeMethodCreateRef,
                        new CodeVariableReferenceExpression(UiFullPropertyName));


                    List<CodeVariableReferenceExpression> varList = new List<CodeVariableReferenceExpression>();
                    foreach (CodeParameterDeclarationExpression p in codeMethod.Parameters)
                    {
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
        static string ParserValue(string value)
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
                        result.Append(string.Format("\" + {0}.{1}.ToString() + \"", appField.Name, refId.ToString()));
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
