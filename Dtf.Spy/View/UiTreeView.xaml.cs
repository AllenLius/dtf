using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Dta.Spy.View
{
    /// <summary>
    /// Interaction logic for UiTreeView.xaml
    /// </summary>
    public partial class UiTreeView : UserControl
    {
        public static readonly DependencyProperty ExpandedCommandProperty = DependencyProperty.Register("ExpandedCommand", typeof(ICommand), typeof(UiTreeView));

        public UiTreeView()
        {
            InitializeComponent();
            //TreeViewItem.ExpandedEvent.AddOwner(this.GetType());
        }

        [Bindable(true)]
        [Category("Action")]
        [Localizability(LocalizationCategory.NeverLocalize)]
        public ICommand ExpandedCommand
        {
            get
            {
                return (ICommand)this.GetValue(ExpandedCommandProperty);
            }
            set
            {
                this.SetValue(ExpandedCommandProperty, value);
            }
        }

        private void TreeView_Expanded(object sender, RoutedEventArgs e)
        {
            if (ExpandedCommand != null)
            {
                ExpandedCommand.Execute(e);
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}
