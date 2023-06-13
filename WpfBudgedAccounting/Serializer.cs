using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfBudgedAccounting
{
    class Serializer
    {
        //Методы для работы с файлом, содержащим записи
        public static void Serialize(ObservableCollection<Note> notes)
        {
            string json = JsonConvert.SerializeObject(notes);
            File.WriteAllText(".\\notes.json", json);
        }
        public static ObservableCollection<Note> Deserialize()
        {
            if (File.Exists(".\\notes.json"))
            {
                string json = File.ReadAllText(".\\notes.json");
                ObservableCollection<Note> notes = JsonConvert.DeserializeObject<ObservableCollection<Note>>(json);
                return notes;
            }
            else
            {
                File.Create(".\\notes.json");
                return new ObservableCollection<Note>();
            }
        }
        //Методы для работы с файлом, содержащим типы записей
        public static void ChangeNotesTypesFile(List<string> notesTypes)
        {
            string json = JsonConvert.SerializeObject(notesTypes);
            File.WriteAllText(".\\Types.json", json);
        }
        public static List<string> ReadFromNotesTypesFile()
        {
            if (File.Exists(".\\Types.json"))
            {
                string json = File.ReadAllText(".\\Types.json");
                List<string> notesTypes = JsonConvert.DeserializeObject<List<string>>(json);
                return notesTypes;
            }
            else
            {
                File.Create(".\\Types.json");
                return new List<string>();
            }
        }
    }
}