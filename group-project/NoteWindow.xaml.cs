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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Threading;
using System.ComponentModel;

namespace group_project
{
    /// This class is responsible to show note
    /// window
    public partial class NoteWindow : Window
    {
        // nested class (memento pattern)
        [Serializable]
        public class Memento
        {

            public String Text;
            public string Filename;
            public string Style_name;

            public Memento(NoteWindow _obj)
            {
                Text = _obj.textBox.Text;
                Filename = _obj.filename;
                Style_name = _obj.style_name;
            }



        }

        /// -------- VARS --------
        DogWindow dog;
        string filename;
        string style_name = "DefaultWindowStyle";
        DispatcherTimer timer = null;

        /// -------- EVENT HANDLERS --------
        void TimerTask(Object source, EventArgs e)
        {
            dog.save_note(this);
            Console.WriteLine("saved: " + filename);
            timer.Stop();
            timer = null;
        }

        void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Console.WriteLine("deleting: " + filename);
            dog.delete_note(filename);
        }

        private void KeyEvents(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int cursor_pos = textBox.SelectionStart;
                textBox.SelectedText = "\r\n";
                textBox.SelectionLength = 0;
                textBox.SelectionStart = cursor_pos + 1;
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (timer != null)
                return;

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TimerTask);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        /// -------- FUNCTIONS --------

        public void set_filename(string _filename) { filename = _filename; }
        public DogWindow get_dog()                 { return dog; }


        public void restore_memento(Memento mem)
        {
            textBox.Text = mem.Text;
            filename = mem.Filename;
            style_name = mem.Style_name;
        }

        public Memento create_memento()
        {
            return new Memento(this);
        }

        public NoteWindow(DogWindow _dog)
        {
            dog = _dog;
            InitializeComponent();
            Title = "Note";
            Closing += OnWindowClosing;
        }

        public NoteWindow(DogWindow _dog, string _style_name)
        {
            dog = _dog;
            InitializeComponent();
            Title = "Note";
            style_name = _style_name;
            Closing += OnWindowClosing;
        }

    }
}
