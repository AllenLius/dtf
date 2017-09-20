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
using System.Windows.Shapes;
using Dtf.Spy.ViewModel;

namespace Dtf.Spy.View
{
    /// <summary>
    /// Interaction logic for QueryViewTest.xaml
    /// </summary>
    public partial class QueryViewTest : Window
    {
        public QueryViewTest()
        {
            InitializeComponent();
            ViewModelLocator.Instance.Query.Expression =
                "<Or>"+
                "    <And>"+
                "        <Equals Property=\"Name\">abc</Equals>"+
                "        <Equals Property=\"ClassName\">xyz</Equals>"+
                "    </And>"+
                "    <And>"+
                "        <Equals Property=\"a\">abc</Equals>"+
                "        <Equals Property=\"b\">a1</Equals>"+
                "    </And>"+
                "</Or>";
        }
    }
}
