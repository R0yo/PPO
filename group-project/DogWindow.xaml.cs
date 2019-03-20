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

namespace group_project
{

    /// The most important window in application
    /// Responsible for displaying dog and
    /// work with NoteStorage.
    public partial class DogWindow : Window
    {
        /// -------- VARS --------
        NoteStorage note_storage;
        ConfigManager config_manager;

        /// -------- EVENT HANDLERS --------

        // when you click on context menu exit button
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        // when you release mouse button
        void imageOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            image.Source = new BitmapImage(new Uri(
                "pack://application:,,," + config_manager.dog_icons_path + "/idle.ico"));
            ((Image)sender).ReleaseMouseCapture();
        }

        // when you move mouse
        void imageOnMouseMove(object sender, MouseEventArgs e)
        {

            if (((Image)sender).IsMouseCaptured)
            {
                Point mouseCurrent = e.GetPosition(null);
                image.Margin = new System.Windows.Thickness(
                    mouseCurrent.X, mouseCurrent.Y, 0, 0);
            }

        }

        // when you press mouse button on dog image
        void imageOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            image.Source = new BitmapImage(new Uri(
                "pack://application:,,," + config_manager.dog_icons_path + "/fly.ico"));
            ((Image)sender).CaptureMouse();

        }

        /// -------- FUNCTIONS --------

        // constructor
        public DogWindow()
        {
            InitializeComponent();
            image.PreviewMouseDown += new MouseButtonEventHandler(imageOnMouseDown);
            image.PreviewMouseMove += new MouseEventHandler(imageOnMouseMove);
            image.PreviewMouseUp += new MouseButtonEventHandler(imageOnMouseUp);

            config_manager = new ConfigManager("/icons/default_dog");

            note_storage = new NoteStorage(this, "storage");
            note_storage.restore_from_disk();
        }

        // functions to delegate to NoteStorage
        public void save_note(NoteWindow n)      { note_storage.save_note(n); }
        public void new_note()                   { note_storage.new_note(); }
        public void delete_note(string filename) { note_storage.delete_note(filename); }

    }
}
