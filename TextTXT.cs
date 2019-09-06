using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SplashScreenDLL
{
    class TextTXT : INotifyPropertyChanged
    {
        private string _message = String.Empty;

        /// <summary>
        /// Сообщение при загрузке
        /// </summary>
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                //NotifyPropertyChanged();
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("Message");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged; // Declare the event

        // Create the OnPropertyChanged method to raise the event
        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        /*protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }*/

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        /*private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }*/
    }
}
