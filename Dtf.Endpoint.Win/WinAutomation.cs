using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Windows.Automation;
using Dtf.Core;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace Dtf.Endpoint.Win
{
    public class WinAutomation : IWinAutomation
    {
        TimeSpan PatternTimeout = TimeSpan.FromMilliseconds(1500);

        public int Process_Start(string fileName, string arguments, string workingDirectory)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = fileName;
            info.Arguments = arguments;
            info.WorkingDirectory = workingDirectory;
            return Process.Start(info).Id;
        }

        public void Process_Close(int processId)
        {
            Process.GetProcessById(processId).Close();
        }

        public void InvokePattern_Invoke(string ui)
        {
            UiaUiObject target = (UiaUiObject)UiaUiObject.Root.FindFirst(ui);
            if (target == null)
            {
                throw new Exception("UI not found!");
            }
            var pattern = target.Current.GetCurrentPattern(InvokePatternIdentifiers.Pattern) as InvokePattern;
            pattern.Invoke();
        }

        public void TextPattern_SetText(string ui, string text)
        {
            throw new NotImplementedException();
        }

        public void TreeWalker_Set(string filter)
        {
            throw new NotImplementedException();
        }

        public void UiObject_FindFirst(string ui)
        {
            throw new NotImplementedException();
        }

        public string UiObject_GetProperty(string ui, string propertyName)
        {
            var target = UiaUiObject.Root.FindFirst(ui) as UiaUiObject;
            return target[propertyName];
        }

        public string[] UiObject_GetProperties(string ui)
        {
            var target = UiaUiObject.Root.FindFirst(ui) as UiaUiObject;
            return target.Properties.ToArray();
        }

        public string UiObject_GetUi(string ui)
        {
            UiaUiObject target = (UiaUiObject)UiaUiObject.Root.FindFirst(ui);
            return target.GetUI();
        }
        
        public Rect UiObject_GetRect(string ui)
        {
            UiaUiObject target = (UiaUiObject)UiaUiObject.Root.FindFirst(ui);
            var rect = target.BoundingRectangle;
            return rect;
        }

        public bool UiObject_Exists(string ui, TimeSpan timeout)
        {
            return UiaUiObject.Root.Wait(ui, timeout) != null;
        }

        public void MousePattern_Click(MouseButton mouseButton)
        {
            Mouse.Click(mouseButton);
        }

        public void MousePattern_ClickOn(MouseButton mouseButton, string ui)
        {
            UiaUiObject target = (UiaUiObject)UiaUiObject.Root.FindFirst(ui);
            target.Current.SetFocus();
            var rect = target.BoundingRectangle;
            double x = rect.X + rect.Width / 2;
            double y = rect.Y + rect.Height / 2;
            MousePattern_Move((int)x, (int)y);
            Mouse.Click(mouseButton);
        }

        public void MousePattern_Down(MouseButton mouseButton)
        {
            Mouse.Down(mouseButton);
        }

        public void MousePattern_Move(int x, int y)
        {
            Mouse.Move(x, y);
        }

        public void MousePattern_Up(MouseButton mouseButton)
        {
            Mouse.Up(mouseButton);
        }

        #region ResourceManager
        public string ResourceManager_GetObject(string handlerType, string resourceKey)
        {
            return ResourceManager.GetObject(handlerType, resourceKey);
        }
        #endregion
    }
}
