﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Dta.Core;
using Dta.Spy.Model;

namespace Dta.Spy.ViewModel
{
    public class DepotViewModel : ViewModelBase
    {
        private ObservableCollection<UiTreeNode> m_depotTreeNodeRoot;
        private string m_fileName;

        public DepotViewModel()
        {
            ExpandedCommand = new RelayCommand<RoutedEventArgs>((e) => PopulateTreeItems(e));
        }

        public void Load(string file)
        {
            XmlReader xmlReader = XmlReader.Create(file);
            var uiRoot = DepotUiObject.Create(xmlReader);
            var nodes = new ObservableCollection<UiTreeNode>();
            foreach (var child in uiRoot.Children)
            {
                nodes.Add(new UiTreeNode(child));
            }
            FileName = file;
            DepotTreeNodeRoot = nodes;
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

        public string FileName
        {
            get
            {
                return m_fileName;
            }
            private set
            {
                m_fileName = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<UiTreeNode> DepotTreeNodeRoot
        {
            get
            {
                return m_depotTreeNodeRoot;
            }
            private set
            {
                m_depotTreeNodeRoot = value;
                RaisePropertyChanged();
            }
        }
    }
}