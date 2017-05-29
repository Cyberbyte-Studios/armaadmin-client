using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberByte.ArmaAdmin.Launcher.Models
{
    class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public string FullName
        {
            get
            {
                if (FullName == null)
                {
                    return string.Format("{0} {1}", FirstName, LastName);
                }
                return FullName;
            }
            // todo possibly allow setting
        }
    }
}
