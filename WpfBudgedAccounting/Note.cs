using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace WpfBudgedAccounting
{
    public class Note
    {
        public DateTime date { get; set; }
        public string title { get; set; }
        public string type { get; set; }

        private decimal Money;
        public decimal money
        {
            get => Money;
            set
            {
                Money = value;
                if (money < 0)
                {
                    isIncome = false;
                }
                else
                {
                    isIncome = true;
                }
            }
        }
        public bool isIncome { get; set; }

        public Note (DateTime date, string title, string type, decimal money)
        {
            this.date = date;
            this.title = title;
            this.type = type;
            this.money = money;
            if (money < 0) isIncome = false;
            else isIncome = true;
        }




        public Note()
        {

        }
    }
}
