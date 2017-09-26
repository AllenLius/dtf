namespace CalcTest
{
    using System.IO;
    using Dtf.Core;
    
    
    public partial class CalcUi
    {
        
        private Dtf.Core.Endpoint m_endpoint;
        
        private Resources m_resources;
        
        private UiElements m_ui;
        
        public CalcUi(Dtf.Core.Endpoint endpoint)
        {
            m_endpoint = endpoint;
            m_resources = new Resources(this);
            m_ui = new UiElements(this);
        }
        
        public Dtf.Core.Endpoint Endpoint
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
    
    public class Calculator : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern
    {
        
        private CalcUi m_calcUi;
        
        public Calculator(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Calculator</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And>", parent)
        {
            m_calcUi = calcUi;
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
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
    }
    
    public class Zero : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public Zero(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Zero</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Result : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern
    {
        
        private CalcUi m_calcUi;
        
        public Result(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"AutomationId\">CalculatorResults</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
    }
    
    public class One : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public One(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">One</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Two : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public Two(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Two</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Three : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public Three(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Three</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Four : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public Four(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Four</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Five : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public Five(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Five</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Six : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public Six(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Six</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Seven : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public Seven(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Seven</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Eight : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public Eight(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Eight</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Nine : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public Nine(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Nine</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Ten : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public Ten(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Ten</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Plus : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public Plus(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Plus</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Minus : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public Minus(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Minus</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Equals : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern, Dtf.Core.IInvokePattern
    {
        
        private CalcUi m_calcUi;
        
        public Equals(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"Name\">Equals</Equals><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
        
        public void Invoke()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IInvokePattern>(FullExpression).Invoke();
        }
    }
    
    public class Test : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern
    {
        
        private CalcUi m_calcUi;
        
        public Test(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"ControlType\">Button</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public Zero2 Zero
        {
            get
            {
                return new Zero2(this, m_calcUi);
            }
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
    }
    
    public class Zero2 : Dtf.Core.UiElement, Dtf.Core.IUiInspector, Dtf.Core.IMousePattern
    {
        
        private CalcUi m_calcUi;
        
        public Zero2(Dtf.Core.UiElement parent, CalcUi calcUi) : 
                base("<And><Equals Property=\"ControlType\">a</Equals></And>", parent)
        {
            m_calcUi = calcUi;
        }
        
        public bool Exists()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists();
        }
        
        public bool Exists(System.TimeSpan timeout)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Exists(timeout);
        }
        
        public byte[] Capture()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Capture();
        }
        
        public string GetProperty(string name)
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperty(name);
        }
        
        public string[] GetProperties()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetProperties();
        }
        
        public string GetUi()
        {
            return m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).GetUi();
        }
        
        public void Wait(System.TimeSpan timeout)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IUiInspectorFactory>().Create(FullExpression).Wait(timeout);
        }
        
        public void Click()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click();
        }
        
        public void Click(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Click(button);
        }
        
        public void MoveTo()
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).MoveTo();
        }
        
        public void Down(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Down(button);
        }
        
        public void Up(Dtf.Core.MouseButton button)
        {
            m_calcUi.Endpoint.QueryInterface<Dtf.Core.IPatternFactory>().Create<Dtf.Core.IMousePattern>(FullExpression).Up(button);
        }
    }
}
