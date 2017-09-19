namespace Dta.Endpoint.Win
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dta.Core;

    internal class WinMousePattern : IMousePattern
    {
        private IWinAutomation m_winAutomation;
        private Expression m_ui;

        public WinMousePattern(IWinAutomation winAutomation, Expression ui)
        {
            m_winAutomation = winAutomation;
            m_ui = ui;
        }

        public void Click()
        {
            m_winAutomation.MousePattern_Click(MouseButton.Left, m_ui);
        }

        public void Click(MouseButton button)
        {
            m_winAutomation.MousePattern_Click(button);
        }

        public void MoveTo()
        {
            var rect = m_winAutomation.UiObject_GetRect(m_ui);
            double x = rect.X + rect.Width / 2;
            double y = rect.Y + rect.Height / 2;
            m_winAutomation.MousePattern_Move((int)x, (int)y);
        }

        public void Down(MouseButton button)
        {
            m_winAutomation.MousePattern_Down(button);
        }

        public void Up(MouseButton button)
        {
            m_winAutomation.MousePattern_Up(button);
        }
    }
}
