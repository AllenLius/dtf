namespace CalcTest
{
    using System.IO;
    using Dta.Core;
    
    
    public partial class CalcUi
    {
        
        private Dta.Core.Endpoint m_endpoint;
        
        public CalcUi(Dta.Core.Endpoint endpoint)
        {
            m_endpoint = endpoint;
        }
        
        public Dta.Core.Endpoint Endpoint
        {
            get
            {
                return m_endpoint;
            }
        }
        
        public object CalcProcessId
        {
            get
            {
                return Dta.Core.ResourceManager.GetObject("ProcessIdResourceHandler", "Calculator");
            }
        }
        
        public object Var
        {
            get
            {
                return Dta.Core.ResourceManager.GetObject("CallbackResourceHandler", "Var");
            }
        }
        
        public UiElements.Calculator Calculator
        {
            get
            {
                return new UiElements.Calculator(this);
            }
        }
        
        public UiElements.Result Result
        {
            get
            {
                return new UiElements.Result(this);
            }
        }
        
        public UiElements.Zero Zero
        {
            get
            {
                return new UiElements.Zero(this);
            }
        }
        
        public UiElements.One One
        {
            get
            {
                return new UiElements.One(this);
            }
        }
        
        public UiElements.Two Two
        {
            get
            {
                return new UiElements.Two(this);
            }
        }
        
        public UiElements.Three Three
        {
            get
            {
                return new UiElements.Three(this);
            }
        }
        
        public UiElements.Four Four
        {
            get
            {
                return new UiElements.Four(this);
            }
        }
        
        public UiElements.Five Five
        {
            get
            {
                return new UiElements.Five(this);
            }
        }
        
        public UiElements.Six Six
        {
            get
            {
                return new UiElements.Six(this);
            }
        }
        
        public UiElements.Seven Seven
        {
            get
            {
                return new UiElements.Seven(this);
            }
        }
        
        public UiElements.Eight Eight
        {
            get
            {
                return new UiElements.Eight(this);
            }
        }
        
        public UiElements.Nine Nine
        {
            get
            {
                return new UiElements.Nine(this);
            }
        }
        
        public UiElements.Ten Ten
        {
            get
            {
                return new UiElements.Ten(this);
            }
        }
        
        public UiElements.Plus Plus
        {
            get
            {
                return new UiElements.Plus(this);
            }
        }
        
        public UiElements.Minus Minus
        {
            get
            {
                return new UiElements.Minus(this);
            }
        }
        
        public UiElements.Equals Equals
        {
            get
            {
                return new UiElements.Equals(this);
            }
        }
        
        public class UiElements
        {
            
            public class Calculator : Dta.Core.IUiInspector, Dta.Core.IMousePattern
            {
                
                private CalcUi m_calcUi;
                
                public Calculator(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
            }
            
            public class Result : Dta.Core.IUiInspector, Dta.Core.IMousePattern
            {
                
                private CalcUi m_calcUi;
                
                public Result(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"AutomationId\">CalculatorResults</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"AutomationId\">CalculatorResults</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
            }
            
            public class Zero : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public Zero(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">Zero</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">Zero</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
            
            public class One : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public One(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">One</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">One</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
            
            public class Two : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public Two(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">Two</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">Two</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
            
            public class Three : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public Three(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">Three</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">Three</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
            
            public class Four : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public Four(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">Four</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">Four</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
            
            public class Five : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public Five(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">Five</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">Five</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
            
            public class Six : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public Six(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">Six</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">Six</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
            
            public class Seven : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public Seven(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">Seven</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">Seven</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
            
            public class Eight : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public Eight(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">Eight</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">Eight</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
            
            public class Nine : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public Nine(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">Nine</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">Nine</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
            
            public class Ten : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public Ten(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">Ten</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">Ten</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
            
            public class Plus : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public Plus(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">Plus</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">Plus</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
            
            public class Minus : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public Minus(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">Minus</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">Minus</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
            
            public class Equals : Dta.Core.IUiInspector, Dta.Core.IMousePattern, Dta.Core.IInvokePattern
            {
                
                private CalcUi m_calcUi;
                
                public Equals(CalcUi calcUi)
                {
                    m_calcUi = calcUi;
                }
                
                public Dta.Core.Expression Ui
                {
                    get
                    {
                        return "<And><Equals Property=\"Name\">Equals</Equals><Equals Property=\"ControlType\">Button</Equals></And>";
                    }
                }
                
                public Dta.Core.Expression UiFull
                {
                    get
                    {
                        return "<Multiple><And><Equals Property=\"ProcessId\">" + m_calcUi.CalcProcessId.ToString() + "</Equals><Equals Property=\"Common.ControlType\">Window</Equals></And><And><Equals Property=\"Name\">Equals</Equals><Equals Property=\"ControlType\">Button</Equals></And></Multiple>";
                    }
                }
                
                public bool Exists()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists();
                }
                
                public bool Exists(System.TimeSpan timeout)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Exists(timeout);
                }
                
                public byte[] Capture()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Capture();
                }
                
                public string GetProperty(string name)
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperty(name);
                }
                
                public string[] GetProperties()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetProperties();
                }
                
                public string GetUi()
                {
                    return m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).GetUi();
                }
                
                public void Wait(System.TimeSpan timeout)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IUiInspectorFactory>().Create(UiFull).Wait(timeout);
                }
                
                public void Click()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click();
                }
                
                public void Click(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Click(button);
                }
                
                public void MoveTo()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).MoveTo();
                }
                
                public void Down(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Down(button);
                }
                
                public void Up(Dta.Core.MouseButton button)
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IMousePattern>(UiFull).Up(button);
                }
                
                public void Invoke()
                {
                    m_calcUi.Endpoint.QueryInterface<Dta.Core.IPatternFactory>().Create<Dta.Core.IInvokePattern>(UiFull).Invoke();
                }
            }
        }
    }
}
