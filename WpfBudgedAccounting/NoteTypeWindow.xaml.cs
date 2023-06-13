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

namespace WpfBudgedAccounting
{
    /// <summary>
    /// Логика взаимодействия для NoteTypeWindow.xaml
    /// </summary>
    public partial class NoteTypeWindow : Window
    {
        public string newType;
        public NoteTypeWindow()
        {
            InitializeComponent();
        }

        private void CreateNoteTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            newType = NoteTypeTxtBx.Text;
            DialogResult = true;
        }
    }
}