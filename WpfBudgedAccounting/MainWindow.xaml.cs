using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfBudgedAccounting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow win;
        List<String> notesTypes = Serializer.ReadFromNotesTypesFile();
        private static ViewModelBase viewModel = new();
        Regex whitespaceRegex = new Regex(@"^\s*$");
        public MainWindow()
        {
            InitializeComponent();
            win = this;
            DataContext = viewModel;
            TypesCmbBx.ItemsSource = notesTypes;
            Calendar.SelectedDate = DateTime.Now;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NoteTypeWindow window = new NoteTypeWindow();
            bool? dialogResuslt = window.ShowDialog();

            if (dialogResuslt == true && !whitespaceRegex.IsMatch(window.newType))
            {
                notesTypes.Add(window.newType);
                Serializer.ChangeNotesTypesFile(notesTypes);
                RefreshNotesTypes();
            }
            else if (dialogResuslt != false && whitespaceRegex.IsMatch(window.newType))
            {
                MessageBox.Show("Пустой тип записи не будет добавлен");
            }
            else
            {
                MessageBox.Show("Окно закрыто");
            }
        }

        private void RefreshNotesTypes()
        {
            notesTypes = Serializer.ReadFromNotesTypesFile();
            TypesCmbBx.ItemsSource = notesTypes;
        }
    }
}