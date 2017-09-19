using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Dta.Core;
using Dta.Spy.Model;
using Dta.Endpoint.Win;

namespace Dta.Spy.ViewModel
{
    public class UiTreeViewModel : ViewModelBase
    {
        private ObservableCollection<UiTreeNode> m_uiTreeNodeRoot;

        public UiTreeViewModel()
        {
            ExpandedCommand = new RelayCommand<RoutedEventArgs>((e) => PopulateTreeItems(e));
        }

        public void Start()
        {
            var uiRoot = UiaUiObject.Root.Children.First();
            var c = new ObservableCollection<UiTreeNode>();
            c.Add(new UiTreeNode(uiRoot));
            UiTreeNodeRoot = c;
        }

        private void PopulateTreeItems(RoutedEventArgs e)
        {
            TreeViewItem tvi = e.OriginalSource as TreeViewItem;
            if (tvi != null)
            {
                if (tvi.Items.Count == 1 && tvi.Items[0] is TreeViewItem)
                {
                    tvi.Items.Clear();
                    UpdateTreeItems(tvi);
                }
            }
        }

        private void UpdateTreeItems(TreeViewItem parent)
        {
            parent.Items.Clear();
            parent.Items.Add(new CheckBox());
        }

        public RelayCommand<RoutedEventArgs> ExpandedCommand { get; set; }

        public ObservableCollection<UiTreeNode> UiTreeNodeRoot
        {
            get
            {
                return m_uiTreeNodeRoot;
            }
            private set
            {
                m_uiTreeNodeRoot = value;
                RaisePropertyChanged();
            }
        }
    }
}
