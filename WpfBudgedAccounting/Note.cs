using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBudgedAccounting
{
    internal class Note
    {
        private string Title;
        private string Type;
        private double Money;

        public DateTime date { get; set; }

        public string title
        {
            get { return Title; }
            set { Title = value; }
        }

        public double money
        {
            get { return Money; }
            set { Money = value; }
        }

        public string type
        {
            get { return Type; }
            set { Type = value; }
        }
    }
}
