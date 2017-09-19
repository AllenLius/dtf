using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Dta.Core;

namespace Dta.Spy.Model
{
    public class UiTreeNode : ObservableObject
    {
        ObservableCollection<UiTreeNode> m_children = new ObservableCollection<UiTreeNode>();

        public UiTreeNode(UiObjectBase uiObj)
        {
            UiObject = uiObj;
            RaisePropertyChanged(() => Children);
        }

        public ObservableCollection<UiTreeNode> Children
        {
            get
            {
                if (!m_children.Any())
                {
                    foreach (var child in UiObject.Children)
                    {
                        m_children.Add(new UiTreeNode(child));
                    }
                }
                return m_children;
            }
        }

        public string DisplayName
        {
            get
            {
                return UiObject.ToString();
            }
        }

        internal UiObjectBase UiObject { get; private set; }
    }
}
