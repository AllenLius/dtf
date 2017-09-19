using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Dta.Spy.Model;

namespace Dta.Spy.ViewModel
{
    public class QueryViewViewModel : ViewModelBase
    {
        public QueryViewViewModel()
        {
            Conditions = new ObservableCollection<QueryCondition>();
            var c = new QueryCondition();
            c.AndOr = true;
            c.GroupCount = 10;
            c.Operator = "Equals";
            c.Property = "Name";
            c.Value = "123";
            Conditions.Add(c);
            Conditions.Add(c);
            Ls = new List<string>();
            Ls.Add("haha");
        }

        public string Expression
        {
            get;
            set;
        }

        public QueryCondition Condition { get; set; }

        public ObservableCollection<QueryCondition> Conditions { get; set; }
        public List<string> Ls { get; set; }
    }
}
