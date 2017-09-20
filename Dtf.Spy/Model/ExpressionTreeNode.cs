using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dtf.Spy.Model
{
    public class QueryCondition
    {
        public int BeginGroupCount { get; set; }
        public int EndGroupCount { get; set; }
        public int GroupCount { get; set; }
        /// <summary>
        /// null if empty; true:And; false:Or
        /// </summary>
        public bool? AndOr { get; set; }
        public string Property { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }
}
