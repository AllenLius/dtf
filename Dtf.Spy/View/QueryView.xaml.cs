using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Dta.Spy.Model;


namespace Dta.Spy.View
{
    /// <summary>
    /// Interaction logic for QueryView.xaml
    /// </summary>
    public partial class QueryView : UserControl
    {
        public static readonly DependencyProperty ExpressionProperty = DependencyProperty.Register("Expression", typeof(string), typeof(QueryView));
        private DataGridCell m_cell;

        public QueryView()
        {
            InitializeComponent();
        }

        private void Cell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m_cell = ((DataGridCell)sender);
        }

        private void Cell_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (m_cell == ((DataGridCell)sender) && !m_cell.IsReadOnly)
            {
                m_cell.IsEditing = true;
            }
        }
        
        public string Expression
        {
            get
            {
                return (string)GetValue(ExpressionProperty);
            }
            set
            {
                object oldValue = Expression;
                SetValue(ExpressionProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(ExpressionProperty, oldValue, value));
            }
        }
    }
}
