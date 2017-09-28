namespace CalcTest
{
    
    
    public partial class CalcUi
    {
        
        private Dtf.Core.IEndpoint m_endpoint;
        
        private Resources m_resources;
        
        private UiElements m_ui;
        
        public CalcUi(Dtf.Core.IEndpoint endpoint)
        {
            m_endpoint = endpoint;
            m_resources = new Resources(this);
            m_ui = new UiElements(this);
        }
        
        public Dtf.Core.IEndpoint Endpoint
        {
            get
            {
                return m_endpoint;
            }
        }
        
        public Resources Resources
        {
            get
            {
                return m_resources;
            }
        }
        
        public UiElements Ui
        {
            get
            {
                return m_ui;
            }
        }
    }
    
    public class Resources
    {
        
        private CalcUi m_calcUi;
        
        public Resources(CalcUi calcUi)
        {
            m_calcUi = calcUi;
        }
        
        public object CalcProcessName
        {
            get
            {
                return Dtf.Core.ResourceManager.GetObject("ConstantResourceHandler", "Calculator");
            }
        }
        
        public object CalcProcessId
        {
            get
            {
                return Dtf.Core.ResourceManager.GetObject("ProcessIdResourceHandler", "Calculator");
            }
        }
        
        public object Var
        {
            get
            {
                return Dtf.Core.ResourceManager.GetObject("CallbackResourceHandler", "Var");
            }
        }
    }
    
    public class UiElements
    {
        
        private CalcUi m_calcUi;
        
        public UiElements(CalcUi calcUi)
        {
            m_calcUi = calcUi;
        }
        
        public Calculator Calculator
        {
            get
            {
                return new Calculator(null, m_calcUi);
            }
        }
        
        public Test Test
        {
            get
            {
                return new Test(null, m_calcUi);
            }
        }
    }
    
    public class Calculator : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Calculator(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"ProcessId\">" + calcUi.Resources.CalcProcessId + "</Equals><Equals Property=\"Name\">Calculator</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
        
        public Zero Zero
        {
            get
            {
                return new Zero(this, m_calcUi);
            }
        }
        
        public Result Result
        {
            get
            {
                return new Result(this, m_calcUi);
            }
        }
        
        public One One
        {
            get
            {
                return new One(this, m_calcUi);
            }
        }
        
        public Two Two
        {
            get
            {
                return new Two(this, m_calcUi);
            }
        }
        
        public Three Three
        {
            get
            {
                return new Three(this, m_calcUi);
            }
        }
        
        public Four Four
        {
            get
            {
                return new Four(this, m_calcUi);
            }
        }
        
        public Five Five
        {
            get
            {
                return new Five(this, m_calcUi);
            }
        }
        
        public Six Six
        {
            get
            {
                return new Six(this, m_calcUi);
            }
        }
        
        public Seven Seven
        {
            get
            {
                return new Seven(this, m_calcUi);
            }
        }
        
        public Eight Eight
        {
            get
            {
                return new Eight(this, m_calcUi);
            }
        }
        
        public Nine Nine
        {
            get
            {
                return new Nine(this, m_calcUi);
            }
        }
        
        public Ten Ten
        {
            get
            {
                return new Ten(this, m_calcUi);
            }
        }
        
        public Plus Plus
        {
            get
            {
                return new Plus(this, m_calcUi);
            }
        }
        
        public Minus Minus
        {
            get
            {
                return new Minus(this, m_calcUi);
            }
        }
        
        public Equals Equals
        {
            get
            {
                return new Equals(this, m_calcUi);
            }
        }
    }
    
    public static class CalculatorEx
    {
        
        public static bool Exists(this Calculator calculator)
        {
            return calculator.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(calculator.FullExpression).Exists();
        }
        
        public static bool Exists(this Calculator calculator, System.TimeSpan timeout)
        {
            return calculator.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(calculator.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Calculator calculator)
        {
            return calculator.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(calculator.FullExpression).Capture();
        }
        
        public static string GetProperty(this Calculator calculator, string name)
        {
            return calculator.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(calculator.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Calculator calculator)
        {
            return calculator.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(calculator.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Calculator calculator)
        {
            return calculator.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(calculator.FullExpression).GetUi();
        }
        
        public static void Wait(this Calculator calculator, System.TimeSpan timeout)
        {
            calculator.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(calculator.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Calculator calculator)
        {
            calculator.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(calculator.FullExpression).Click();
        }
        
        public static void Click(this Calculator calculator, Dtf.Core.MouseButton button)
        {
            calculator.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(calculator.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Calculator calculator)
        {
            calculator.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(calculator.FullExpression).MoveTo();
        }
        
        public static void Down(this Calculator calculator, Dtf.Core.MouseButton button)
        {
            calculator.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(calculator.FullExpression).Down(button);
        }
        
        public static void Up(this Calculator calculator, Dtf.Core.MouseButton button)
        {
            calculator.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(calculator.FullExpression).Up(button);
        }
    }
    
    public class Zero : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Zero(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Zero</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class ZeroEx
    {
        
        public static bool Exists(this Zero zero)
        {
            return zero.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero.FullExpression).Exists();
        }
        
        public static bool Exists(this Zero zero, System.TimeSpan timeout)
        {
            return zero.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Zero zero)
        {
            return zero.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero.FullExpression).Capture();
        }
        
        public static string GetProperty(this Zero zero, string name)
        {
            return zero.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Zero zero)
        {
            return zero.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Zero zero)
        {
            return zero.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero.FullExpression).GetUi();
        }
        
        public static void Wait(this Zero zero, System.TimeSpan timeout)
        {
            zero.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Zero zero)
        {
            zero.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(zero.FullExpression).Click();
        }
        
        public static void Click(this Zero zero, Dtf.Core.MouseButton button)
        {
            zero.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(zero.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Zero zero)
        {
            zero.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(zero.FullExpression).MoveTo();
        }
        
        public static void Down(this Zero zero, Dtf.Core.MouseButton button)
        {
            zero.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(zero.FullExpression).Down(button);
        }
        
        public static void Up(this Zero zero, Dtf.Core.MouseButton button)
        {
            zero.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(zero.FullExpression).Up(button);
        }
        
        public static void Invoke(this Zero zero)
        {
            zero.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(zero.FullExpression).Invoke();
        }
    }
    
    public class Result : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Result(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"AutomationId\">CalculatorResults</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class ResultEx
    {
        
        public static bool Exists(this Result result)
        {
            return result.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(result.FullExpression).Exists();
        }
        
        public static bool Exists(this Result result, System.TimeSpan timeout)
        {
            return result.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(result.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Result result)
        {
            return result.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(result.FullExpression).Capture();
        }
        
        public static string GetProperty(this Result result, string name)
        {
            return result.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(result.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Result result)
        {
            return result.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(result.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Result result)
        {
            return result.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(result.FullExpression).GetUi();
        }
        
        public static void Wait(this Result result, System.TimeSpan timeout)
        {
            result.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(result.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Result result)
        {
            result.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(result.FullExpression).Click();
        }
        
        public static void Click(this Result result, Dtf.Core.MouseButton button)
        {
            result.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(result.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Result result)
        {
            result.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(result.FullExpression).MoveTo();
        }
        
        public static void Down(this Result result, Dtf.Core.MouseButton button)
        {
            result.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(result.FullExpression).Down(button);
        }
        
        public static void Up(this Result result, Dtf.Core.MouseButton button)
        {
            result.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(result.FullExpression).Up(button);
        }
    }
    
    public class One : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public One(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">One</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class OneEx
    {
        
        public static bool Exists(this One one)
        {
            return one.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(one.FullExpression).Exists();
        }
        
        public static bool Exists(this One one, System.TimeSpan timeout)
        {
            return one.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(one.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this One one)
        {
            return one.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(one.FullExpression).Capture();
        }
        
        public static string GetProperty(this One one, string name)
        {
            return one.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(one.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this One one)
        {
            return one.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(one.FullExpression).GetProperties();
        }
        
        public static string GetUi(this One one)
        {
            return one.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(one.FullExpression).GetUi();
        }
        
        public static void Wait(this One one, System.TimeSpan timeout)
        {
            one.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(one.FullExpression).Wait(timeout);
        }
        
        public static void Click(this One one)
        {
            one.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(one.FullExpression).Click();
        }
        
        public static void Click(this One one, Dtf.Core.MouseButton button)
        {
            one.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(one.FullExpression).Click(button);
        }
        
        public static void MoveTo(this One one)
        {
            one.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(one.FullExpression).MoveTo();
        }
        
        public static void Down(this One one, Dtf.Core.MouseButton button)
        {
            one.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(one.FullExpression).Down(button);
        }
        
        public static void Up(this One one, Dtf.Core.MouseButton button)
        {
            one.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(one.FullExpression).Up(button);
        }
        
        public static void Invoke(this One one)
        {
            one.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(one.FullExpression).Invoke();
        }
    }
    
    public class Two : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Two(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Two</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class TwoEx
    {
        
        public static bool Exists(this Two two)
        {
            return two.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(two.FullExpression).Exists();
        }
        
        public static bool Exists(this Two two, System.TimeSpan timeout)
        {
            return two.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(two.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Two two)
        {
            return two.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(two.FullExpression).Capture();
        }
        
        public static string GetProperty(this Two two, string name)
        {
            return two.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(two.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Two two)
        {
            return two.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(two.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Two two)
        {
            return two.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(two.FullExpression).GetUi();
        }
        
        public static void Wait(this Two two, System.TimeSpan timeout)
        {
            two.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(two.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Two two)
        {
            two.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(two.FullExpression).Click();
        }
        
        public static void Click(this Two two, Dtf.Core.MouseButton button)
        {
            two.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(two.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Two two)
        {
            two.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(two.FullExpression).MoveTo();
        }
        
        public static void Down(this Two two, Dtf.Core.MouseButton button)
        {
            two.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(two.FullExpression).Down(button);
        }
        
        public static void Up(this Two two, Dtf.Core.MouseButton button)
        {
            two.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(two.FullExpression).Up(button);
        }
        
        public static void Invoke(this Two two)
        {
            two.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(two.FullExpression).Invoke();
        }
    }
    
    public class Three : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Three(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Three</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class ThreeEx
    {
        
        public static bool Exists(this Three three)
        {
            return three.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(three.FullExpression).Exists();
        }
        
        public static bool Exists(this Three three, System.TimeSpan timeout)
        {
            return three.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(three.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Three three)
        {
            return three.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(three.FullExpression).Capture();
        }
        
        public static string GetProperty(this Three three, string name)
        {
            return three.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(three.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Three three)
        {
            return three.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(three.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Three three)
        {
            return three.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(three.FullExpression).GetUi();
        }
        
        public static void Wait(this Three three, System.TimeSpan timeout)
        {
            three.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(three.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Three three)
        {
            three.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(three.FullExpression).Click();
        }
        
        public static void Click(this Three three, Dtf.Core.MouseButton button)
        {
            three.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(three.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Three three)
        {
            three.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(three.FullExpression).MoveTo();
        }
        
        public static void Down(this Three three, Dtf.Core.MouseButton button)
        {
            three.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(three.FullExpression).Down(button);
        }
        
        public static void Up(this Three three, Dtf.Core.MouseButton button)
        {
            three.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(three.FullExpression).Up(button);
        }
        
        public static void Invoke(this Three three)
        {
            three.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(three.FullExpression).Invoke();
        }
    }
    
    public class Four : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Four(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Four</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class FourEx
    {
        
        public static bool Exists(this Four four)
        {
            return four.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(four.FullExpression).Exists();
        }
        
        public static bool Exists(this Four four, System.TimeSpan timeout)
        {
            return four.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(four.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Four four)
        {
            return four.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(four.FullExpression).Capture();
        }
        
        public static string GetProperty(this Four four, string name)
        {
            return four.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(four.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Four four)
        {
            return four.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(four.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Four four)
        {
            return four.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(four.FullExpression).GetUi();
        }
        
        public static void Wait(this Four four, System.TimeSpan timeout)
        {
            four.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(four.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Four four)
        {
            four.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(four.FullExpression).Click();
        }
        
        public static void Click(this Four four, Dtf.Core.MouseButton button)
        {
            four.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(four.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Four four)
        {
            four.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(four.FullExpression).MoveTo();
        }
        
        public static void Down(this Four four, Dtf.Core.MouseButton button)
        {
            four.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(four.FullExpression).Down(button);
        }
        
        public static void Up(this Four four, Dtf.Core.MouseButton button)
        {
            four.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(four.FullExpression).Up(button);
        }
        
        public static void Invoke(this Four four)
        {
            four.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(four.FullExpression).Invoke();
        }
    }
    
    public class Five : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Five(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Five</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class FiveEx
    {
        
        public static bool Exists(this Five five)
        {
            return five.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(five.FullExpression).Exists();
        }
        
        public static bool Exists(this Five five, System.TimeSpan timeout)
        {
            return five.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(five.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Five five)
        {
            return five.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(five.FullExpression).Capture();
        }
        
        public static string GetProperty(this Five five, string name)
        {
            return five.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(five.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Five five)
        {
            return five.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(five.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Five five)
        {
            return five.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(five.FullExpression).GetUi();
        }
        
        public static void Wait(this Five five, System.TimeSpan timeout)
        {
            five.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(five.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Five five)
        {
            five.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(five.FullExpression).Click();
        }
        
        public static void Click(this Five five, Dtf.Core.MouseButton button)
        {
            five.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(five.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Five five)
        {
            five.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(five.FullExpression).MoveTo();
        }
        
        public static void Down(this Five five, Dtf.Core.MouseButton button)
        {
            five.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(five.FullExpression).Down(button);
        }
        
        public static void Up(this Five five, Dtf.Core.MouseButton button)
        {
            five.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(five.FullExpression).Up(button);
        }
        
        public static void Invoke(this Five five)
        {
            five.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(five.FullExpression).Invoke();
        }
    }
    
    public class Six : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Six(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Six</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class SixEx
    {
        
        public static bool Exists(this Six six)
        {
            return six.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(six.FullExpression).Exists();
        }
        
        public static bool Exists(this Six six, System.TimeSpan timeout)
        {
            return six.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(six.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Six six)
        {
            return six.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(six.FullExpression).Capture();
        }
        
        public static string GetProperty(this Six six, string name)
        {
            return six.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(six.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Six six)
        {
            return six.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(six.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Six six)
        {
            return six.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(six.FullExpression).GetUi();
        }
        
        public static void Wait(this Six six, System.TimeSpan timeout)
        {
            six.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(six.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Six six)
        {
            six.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(six.FullExpression).Click();
        }
        
        public static void Click(this Six six, Dtf.Core.MouseButton button)
        {
            six.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(six.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Six six)
        {
            six.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(six.FullExpression).MoveTo();
        }
        
        public static void Down(this Six six, Dtf.Core.MouseButton button)
        {
            six.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(six.FullExpression).Down(button);
        }
        
        public static void Up(this Six six, Dtf.Core.MouseButton button)
        {
            six.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(six.FullExpression).Up(button);
        }
        
        public static void Invoke(this Six six)
        {
            six.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(six.FullExpression).Invoke();
        }
    }
    
    public class Seven : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Seven(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Seven</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class SevenEx
    {
        
        public static bool Exists(this Seven seven)
        {
            return seven.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(seven.FullExpression).Exists();
        }
        
        public static bool Exists(this Seven seven, System.TimeSpan timeout)
        {
            return seven.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(seven.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Seven seven)
        {
            return seven.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(seven.FullExpression).Capture();
        }
        
        public static string GetProperty(this Seven seven, string name)
        {
            return seven.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(seven.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Seven seven)
        {
            return seven.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(seven.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Seven seven)
        {
            return seven.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(seven.FullExpression).GetUi();
        }
        
        public static void Wait(this Seven seven, System.TimeSpan timeout)
        {
            seven.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(seven.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Seven seven)
        {
            seven.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(seven.FullExpression).Click();
        }
        
        public static void Click(this Seven seven, Dtf.Core.MouseButton button)
        {
            seven.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(seven.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Seven seven)
        {
            seven.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(seven.FullExpression).MoveTo();
        }
        
        public static void Down(this Seven seven, Dtf.Core.MouseButton button)
        {
            seven.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(seven.FullExpression).Down(button);
        }
        
        public static void Up(this Seven seven, Dtf.Core.MouseButton button)
        {
            seven.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(seven.FullExpression).Up(button);
        }
        
        public static void Invoke(this Seven seven)
        {
            seven.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(seven.FullExpression).Invoke();
        }
    }
    
    public class Eight : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Eight(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Eight</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class EightEx
    {
        
        public static bool Exists(this Eight eight)
        {
            return eight.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(eight.FullExpression).Exists();
        }
        
        public static bool Exists(this Eight eight, System.TimeSpan timeout)
        {
            return eight.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(eight.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Eight eight)
        {
            return eight.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(eight.FullExpression).Capture();
        }
        
        public static string GetProperty(this Eight eight, string name)
        {
            return eight.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(eight.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Eight eight)
        {
            return eight.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(eight.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Eight eight)
        {
            return eight.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(eight.FullExpression).GetUi();
        }
        
        public static void Wait(this Eight eight, System.TimeSpan timeout)
        {
            eight.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(eight.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Eight eight)
        {
            eight.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(eight.FullExpression).Click();
        }
        
        public static void Click(this Eight eight, Dtf.Core.MouseButton button)
        {
            eight.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(eight.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Eight eight)
        {
            eight.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(eight.FullExpression).MoveTo();
        }
        
        public static void Down(this Eight eight, Dtf.Core.MouseButton button)
        {
            eight.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(eight.FullExpression).Down(button);
        }
        
        public static void Up(this Eight eight, Dtf.Core.MouseButton button)
        {
            eight.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(eight.FullExpression).Up(button);
        }
        
        public static void Invoke(this Eight eight)
        {
            eight.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(eight.FullExpression).Invoke();
        }
    }
    
    public class Nine : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Nine(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Nine</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class NineEx
    {
        
        public static bool Exists(this Nine nine)
        {
            return nine.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(nine.FullExpression).Exists();
        }
        
        public static bool Exists(this Nine nine, System.TimeSpan timeout)
        {
            return nine.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(nine.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Nine nine)
        {
            return nine.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(nine.FullExpression).Capture();
        }
        
        public static string GetProperty(this Nine nine, string name)
        {
            return nine.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(nine.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Nine nine)
        {
            return nine.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(nine.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Nine nine)
        {
            return nine.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(nine.FullExpression).GetUi();
        }
        
        public static void Wait(this Nine nine, System.TimeSpan timeout)
        {
            nine.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(nine.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Nine nine)
        {
            nine.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(nine.FullExpression).Click();
        }
        
        public static void Click(this Nine nine, Dtf.Core.MouseButton button)
        {
            nine.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(nine.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Nine nine)
        {
            nine.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(nine.FullExpression).MoveTo();
        }
        
        public static void Down(this Nine nine, Dtf.Core.MouseButton button)
        {
            nine.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(nine.FullExpression).Down(button);
        }
        
        public static void Up(this Nine nine, Dtf.Core.MouseButton button)
        {
            nine.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(nine.FullExpression).Up(button);
        }
        
        public static void Invoke(this Nine nine)
        {
            nine.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(nine.FullExpression).Invoke();
        }
    }
    
    public class Ten : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Ten(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Ten</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class TenEx
    {
        
        public static bool Exists(this Ten ten)
        {
            return ten.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(ten.FullExpression).Exists();
        }
        
        public static bool Exists(this Ten ten, System.TimeSpan timeout)
        {
            return ten.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(ten.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Ten ten)
        {
            return ten.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(ten.FullExpression).Capture();
        }
        
        public static string GetProperty(this Ten ten, string name)
        {
            return ten.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(ten.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Ten ten)
        {
            return ten.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(ten.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Ten ten)
        {
            return ten.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(ten.FullExpression).GetUi();
        }
        
        public static void Wait(this Ten ten, System.TimeSpan timeout)
        {
            ten.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(ten.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Ten ten)
        {
            ten.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(ten.FullExpression).Click();
        }
        
        public static void Click(this Ten ten, Dtf.Core.MouseButton button)
        {
            ten.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(ten.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Ten ten)
        {
            ten.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(ten.FullExpression).MoveTo();
        }
        
        public static void Down(this Ten ten, Dtf.Core.MouseButton button)
        {
            ten.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(ten.FullExpression).Down(button);
        }
        
        public static void Up(this Ten ten, Dtf.Core.MouseButton button)
        {
            ten.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(ten.FullExpression).Up(button);
        }
        
        public static void Invoke(this Ten ten)
        {
            ten.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(ten.FullExpression).Invoke();
        }
    }
    
    public class Plus : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Plus(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Plus</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class PlusEx
    {
        
        public static bool Exists(this Plus plus)
        {
            return plus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(plus.FullExpression).Exists();
        }
        
        public static bool Exists(this Plus plus, System.TimeSpan timeout)
        {
            return plus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(plus.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Plus plus)
        {
            return plus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(plus.FullExpression).Capture();
        }
        
        public static string GetProperty(this Plus plus, string name)
        {
            return plus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(plus.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Plus plus)
        {
            return plus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(plus.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Plus plus)
        {
            return plus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(plus.FullExpression).GetUi();
        }
        
        public static void Wait(this Plus plus, System.TimeSpan timeout)
        {
            plus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(plus.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Plus plus)
        {
            plus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(plus.FullExpression).Click();
        }
        
        public static void Click(this Plus plus, Dtf.Core.MouseButton button)
        {
            plus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(plus.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Plus plus)
        {
            plus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(plus.FullExpression).MoveTo();
        }
        
        public static void Down(this Plus plus, Dtf.Core.MouseButton button)
        {
            plus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(plus.FullExpression).Down(button);
        }
        
        public static void Up(this Plus plus, Dtf.Core.MouseButton button)
        {
            plus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(plus.FullExpression).Up(button);
        }
        
        public static void Invoke(this Plus plus)
        {
            plus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(plus.FullExpression).Invoke();
        }
    }
    
    public class Minus : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Minus(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Minus</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class MinusEx
    {
        
        public static bool Exists(this Minus minus)
        {
            return minus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(minus.FullExpression).Exists();
        }
        
        public static bool Exists(this Minus minus, System.TimeSpan timeout)
        {
            return minus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(minus.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Minus minus)
        {
            return minus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(minus.FullExpression).Capture();
        }
        
        public static string GetProperty(this Minus minus, string name)
        {
            return minus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(minus.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Minus minus)
        {
            return minus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(minus.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Minus minus)
        {
            return minus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(minus.FullExpression).GetUi();
        }
        
        public static void Wait(this Minus minus, System.TimeSpan timeout)
        {
            minus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(minus.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Minus minus)
        {
            minus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(minus.FullExpression).Click();
        }
        
        public static void Click(this Minus minus, Dtf.Core.MouseButton button)
        {
            minus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(minus.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Minus minus)
        {
            minus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(minus.FullExpression).MoveTo();
        }
        
        public static void Down(this Minus minus, Dtf.Core.MouseButton button)
        {
            minus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(minus.FullExpression).Down(button);
        }
        
        public static void Up(this Minus minus, Dtf.Core.MouseButton button)
        {
            minus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(minus.FullExpression).Up(button);
        }
        
        public static void Invoke(this Minus minus)
        {
            minus.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(minus.FullExpression).Invoke();
        }
    }
    
    public class Equals : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Equals(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Equals</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class EqualsEx
    {
        
        public static bool Exists(this Equals equals)
        {
            return equals.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(equals.FullExpression).Exists();
        }
        
        public static bool Exists(this Equals equals, System.TimeSpan timeout)
        {
            return equals.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(equals.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Equals equals)
        {
            return equals.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(equals.FullExpression).Capture();
        }
        
        public static string GetProperty(this Equals equals, string name)
        {
            return equals.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(equals.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Equals equals)
        {
            return equals.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(equals.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Equals equals)
        {
            return equals.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(equals.FullExpression).GetUi();
        }
        
        public static void Wait(this Equals equals, System.TimeSpan timeout)
        {
            equals.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(equals.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Equals equals)
        {
            equals.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(equals.FullExpression).Click();
        }
        
        public static void Click(this Equals equals, Dtf.Core.MouseButton button)
        {
            equals.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(equals.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Equals equals)
        {
            equals.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(equals.FullExpression).MoveTo();
        }
        
        public static void Down(this Equals equals, Dtf.Core.MouseButton button)
        {
            equals.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(equals.FullExpression).Down(button);
        }
        
        public static void Up(this Equals equals, Dtf.Core.MouseButton button)
        {
            equals.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(equals.FullExpression).Up(button);
        }
        
        public static void Invoke(this Equals equals)
        {
            equals.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(equals.FullExpression).Invoke();
        }
    }
    
    public class Test : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Test(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
        
        public Zero2 Zero
        {
            get
            {
                return new Zero2(this, m_calcUi);
            }
        }
    }
    
    public static class TestEx
    {
        
        public static bool Exists(this Test test)
        {
            return test.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(test.FullExpression).Exists();
        }
        
        public static bool Exists(this Test test, System.TimeSpan timeout)
        {
            return test.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(test.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Test test)
        {
            return test.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(test.FullExpression).Capture();
        }
        
        public static string GetProperty(this Test test, string name)
        {
            return test.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(test.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Test test)
        {
            return test.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(test.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Test test)
        {
            return test.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(test.FullExpression).GetUi();
        }
        
        public static void Wait(this Test test, System.TimeSpan timeout)
        {
            test.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(test.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Test test)
        {
            test.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(test.FullExpression).Click();
        }
        
        public static void Click(this Test test, Dtf.Core.MouseButton button)
        {
            test.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(test.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Test test)
        {
            test.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(test.FullExpression).MoveTo();
        }
        
        public static void Down(this Test test, Dtf.Core.MouseButton button)
        {
            test.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(test.FullExpression).Down(button);
        }
        
        public static void Up(this Test test, Dtf.Core.MouseButton button)
        {
            test.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(test.FullExpression).Up(button);
        }
    }
    
    public class Zero2 : Dtf.Core.UiElement
    {
        
        private CalcUi m_calcUi;
        
        public Zero2(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"ControlType\">a</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public CalcUi CalcUi
        {
            get
            {
                return this.m_calcUi;
            }
        }
    }
    
    public static class Zero2Ex
    {
        
        public static bool Exists(this Zero2 zero2)
        {
            return zero2.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero2.FullExpression).Exists();
        }
        
        public static bool Exists(this Zero2 zero2, System.TimeSpan timeout)
        {
            return zero2.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero2.FullExpression).Exists(timeout);
        }
        
        public static byte[] Capture(this Zero2 zero2)
        {
            return zero2.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero2.FullExpression).Capture();
        }
        
        public static string GetProperty(this Zero2 zero2, string name)
        {
            return zero2.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero2.FullExpression).GetProperty(name);
        }
        
        public static string[] GetProperties(this Zero2 zero2)
        {
            return zero2.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero2.FullExpression).GetProperties();
        }
        
        public static string GetUi(this Zero2 zero2)
        {
            return zero2.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero2.FullExpression).GetUi();
        }
        
        public static void Wait(this Zero2 zero2, System.TimeSpan timeout)
        {
            zero2.CalcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(zero2.FullExpression).Wait(timeout);
        }
        
        public static void Click(this Zero2 zero2)
        {
            zero2.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(zero2.FullExpression).Click();
        }
        
        public static void Click(this Zero2 zero2, Dtf.Core.MouseButton button)
        {
            zero2.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(zero2.FullExpression).Click(button);
        }
        
        public static void MoveTo(this Zero2 zero2)
        {
            zero2.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(zero2.FullExpression).MoveTo();
        }
        
        public static void Down(this Zero2 zero2, Dtf.Core.MouseButton button)
        {
            zero2.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(zero2.FullExpression).Down(button);
        }
        
        public static void Up(this Zero2 zero2, Dtf.Core.MouseButton button)
        {
            zero2.CalcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(zero2.FullExpression).Up(button);
        }
    }
}
