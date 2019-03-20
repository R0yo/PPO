using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_project
{
    /// This class is responsible for 
    /// saving and reading config from disk.
    /// Config contains persistent settings.
    class ConfigManager
    {
        /// -------- VARS --------

        public string dog_icons_path;

        /// -------- FUNCTIONS --------

        public ConfigManager(string _dog_icons_path)
        {
            dog_icons_path = _dog_icons_path;
        }

        public void save_config() { }
        public void restore_config() { }
    }
}
