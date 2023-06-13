using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfBudgedAccounting
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };


        public ViewModelBase()
        {
            Notes = Serializer.Deserialize();
            Date = DateTime.Today;
            UpCounter();
        }


        private DateTime _date = DateTime.Today;
        private string title = "";
        private string type = "";
        private decimal money = 0;
        private bool isIncome;
        private ObservableCollection<Note> notesDays = new();

        public ObservableCollection<Note> NotesDays
        {
            get => notesDays;
            set => Set(ref notesDays, value);
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                Set(ref _date, value);
                UpdateNotes();
            }
        }

        private void UpdateNotes()
        {
            NotesDays.Clear();
            foreach (var item in Notes)
            {
                if (item.date == Date)
                {
                    NotesDays.Add(item);
                }
            }
        }

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public string Type
        {
            get => type; set => Set(ref type, value);
        }

        public decimal Money
        {
            get => money; set
            {
                money = value;
                Set(ref money, value);
                if (money  > 0) IsIncome = true;
                else IsIncome = false;
            }
        }

        public bool IsIncome
        {
            get => isIncome;
            set => Set(ref isIncome, value);
        }

        protected void Set<T>(ref T field, T value, [CallerMemberName] string propname = "")
        {
            if (field != null)
            {
                if (!field.Equals(value))
                {
                    field = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(propname));
                }
            }
        }


        private ObservableCollection<Note> notes = new ObservableCollection<Note>();
        public ObservableCollection<Note> Notes
        {
            get => notes;
            set => Set(ref  notes, value, "Notes");
        }


        private Note? selectedNote;
        public Note? SelectedNote
        {
            get => selectedNote;
            set
            {
                selectedNote = value;
                Set(ref selectedNote, value);
                if (SelectedNote == null) return;
                OnSelected();
            }
        }

        public void AddNote(Note note)
        {
            Notes.Add(note);
            UpCounter();
            Serializer.Serialize(Notes);
        }
        
        public void RemoveNote(Note note)
        {
            Notes.Remove(note);
            Serializer.Serialize(Notes);
            UpdateNotes();
        }

        private void OnSelected()
        {
            if (SelectedNote == null) return;
            if (SelectedNote != null)
            {
                Title = SelectedNote.title; 
                Date = SelectedNote.date; 
                Type = SelectedNote.type; 
                Money = SelectedNote.money;
            }
        }


        private RelayCommand commandDelete;
        public RelayCommand CommandDelete
        {
            get
            {
                return commandDelete ??= new RelayCommand(obj =>
                {
                    if (SelectedNote == null) return;
                    RemoveNote(SelectedNote);
                    Serializer.Serialize(Notes);
                    UpCounter();

                });
            }
        }

        private string amount = "";
        public string Amount
        {
            get => amount;
            set
            {
                amount = $"Итого : {value}";
                Set(ref amount, value);
            }
        }

        private void UpCounter()
        {
            Amount = "";
            decimal a = 0;
            foreach (var item in Notes)
            {
                a += item.money;
            }
            Amount = a.ToString();
        }

        private RelayCommand commandSave;
        public RelayCommand CommandSave
        {
            get
            {
                return commandSave ??= new RelayCommand(obj =>
                {
                    SaveNote();

                });
            }
        }

        private void SaveNote()
        {
            if (SelectedNote != null)
            {
                SelectedNote.date = Date;
                SelectedNote.title = Title;
                SelectedNote.money = Money;
                SelectedNote.type = Type;
                SelectedNote.isIncome = IsIncome;
                Serializer.Serialize(Notes);
                UpCounter();
                
            }
            else
            {
                AddNote(new Note(Date, Title, Type, Money));
            }
            Title = "";
            Type = "";
            Money = 0;
            SelectedNote = null;
            UpdateNotes();
        }




    }

    public class PositiveNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return Math.Abs(intValue);
            }
            else if (value is double doubleValue)
            {
                return Math.Abs(doubleValue);
            }
            else if (value is decimal decimalValue)
            {
                return Math.Abs(decimalValue);
            }
            else
            {
                throw new ArgumentException("Value must be a number.");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
