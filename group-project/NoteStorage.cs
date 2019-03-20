using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace group_project
{
    /// This class is responsible
    /// for storing notes, saving
    /// them on disk and restoring
    public class NoteStorage
    {
        /// -------- VARS --------
        String directory_path;
        List<NoteWindow> notes;
        DogWindow dog;
        static int next_note_id = 0;

        /// -------- FUNCTIONS --------

        public NoteStorage(DogWindow _dog, String _dir_path)
        {
            dog = _dog;
            directory_path = _dir_path;
            notes = new List<NoteWindow>();
        }


        public int save_note(NoteWindow note)
        {
            NoteWindow.Memento settings = note.create_memento();

            if (!Directory.Exists(directory_path))
                Directory.CreateDirectory(directory_path);


            Stream stream = null;

            if (File.Exists(settings.Filename))
            {
                stream = File.Open(settings.Filename, FileMode.Create);
            }
            else
            {
                string filename = "";
                while (true)
                {
                    if (File.Exists(directory_path + "/note" + ++next_note_id + ".osl"))
                        continue;

                    filename = directory_path + "/note" + next_note_id + ".osl";
                    break;
                }

                Console.WriteLine("choosed filename: " + filename);

                stream = File.Open(filename, FileMode.Create);

                note.set_filename(filename);
                settings.Filename = filename;
                
            }
            BinaryFormatter bformatter = new BinaryFormatter();

            bformatter.Serialize(stream, settings);
            stream.Close();

            return 0;
        }

        

        public int restore_from_disk()
        {
            if (!Directory.Exists(directory_path))
                Directory.CreateDirectory(directory_path);

            string[] all_files = Directory.GetFiles(directory_path);

            if (all_files.Length < 1)
            {
                NoteWindow a = new NoteWindow(dog);
                a.Show();
                save_note(a);
                return 1;
            }

            next_note_id = 0;

            foreach (string filename in all_files)
            {
                Stream stream2 = new FileStream(filename, FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();
                NoteWindow.Memento obj = (NoteWindow.Memento)bformatter.Deserialize(stream2);
                stream2.Close();

                NoteWindow b = new NoteWindow(dog);
                b.restore_memento(obj);

                b.Style = (System.Windows.Style) b.FindResource(obj.Style_name);

                b.Show();
                notes.Add(b);
            }

            return 0;
        }

        public void delete_note(string filename)
        {
            File.Delete(filename);
        }

        public void new_note()
        {
            NoteWindow a = new NoteWindow(dog);
            //NoteWindow a = new NoteWindow(dog, "RetrowaveWindowStyle");
            //a.Style = (System.Windows.Style) a.FindResource("RetrowaveWindowStyle");

            a.Show();
            notes.Add(a);
        }


    }




}
