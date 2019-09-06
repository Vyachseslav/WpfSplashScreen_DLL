/*
 *  Platform: .NET Framework 4
 *  System Requirements: Windows XP/7/8/10
 *  
 *  Company: Palitra System
 *  Author: Ermilov V., Selyakov V.
 *  
 *  PALITRA SYSTEM © 2017
 * 
 */

/*
// Not using library
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
*/
using System;
using System.Windows;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;

namespace SplashScreenDLL
{
   /// <summary>
   /// Класс загрузочного окна LoaderMetroStyleTXT_Simple.xaml
   /// </summary>
   public partial class LoaderMetroStyleTXT_Simple : Window, INotifyPropertyChanged
    {
        LoaderMetroStyleTXT_Simple lsms;            // Объявляем этот класс
        Thread uiThread;                            // Объявляем новый поток
        Dispatcher progressDisptacher;              // Объявляем новый диспетчер
        private string _message = String.Empty;     // Объявляем переменную для сообщений на форму
        private string _softVersion = String.Empty; // Объявляем переменную для версии программы
        private string _programName = String.Empty; // Объявляем переменную для названия запускаемой программы

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

        /// <summary>
        /// Версия программы
        /// </summary>
        public string SoftVersion
        {
            get
            {
                return _softVersion;
            }
            set
            {
                _softVersion = value;
                //NotifyPropertyChanged();
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("SoftVersion");
            }
        }

        /// <summary>
        /// Название программы
        /// </summary>
        public string ProgramName
        {
            get
            {
                return _programName;
            }
            set
            {
                _programName = value;
                //NotifyPropertyChanged();
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("ProgramName");
            }
        }

        /// <summary>
        /// Событие PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged; // Declare the event

        // Create the OnPropertyChanged method to raise the event
        private void OnPropertyChanged(string name)
        {
            //PropertyChangedEventHandler handler = PropertyChanged;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Конструктор класса LoaderMetroStyleTXT_Simple (обычный)
        /// </summary>
        public LoaderMetroStyleTXT_Simple()
        {
            InitializeComponent();
            //this.txt_SoftVersion.Text = "6.5"; // Версия программы

            this.SoftVersion = "6.5";     // Версия программы
            this.ProgramName = "SAP MS";  // Название запскаемой программы
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="softVersion">Версия программы</param>
        /// <param name="programName">Название запускаемой программы</param>
        public LoaderMetroStyleTXT_Simple(string softVersion, string programName)
        {
            InitializeComponent();
            //this.txt_SoftVersion.Text = softVersion;        // Версия программы
            //this.txt_ProgramName.Text = programName;        // Название программы  

            this.SoftVersion = softVersion;     // Версия программы
            this.ProgramName = programName;     // Название запскаемой программы
        }

        /// <summary>
        /// Открыть загрузочное окно
        /// </summary>
        public void ShowScreen()
        {
            uiThread = new System.Threading.Thread(() =>
            {
                lsms = new LoaderMetroStyleTXT_Simple();
                lsms.Dispatcher.Invoke((new Action(() => lsms.Topmost = true)));
                lsms.Dispatcher.Invoke((new Action(() => lsms.Show())));
                lsms.Dispatcher.Invoke((new Action(() => lsms.txt_Status.Text = "Загрузка")));
                lsms.Dispatcher.Invoke((new Action(() => lsms.txt_SoftVersion.Text = SoftVersion)));
                lsms.Dispatcher.Invoke((new Action(() => lsms.txt_ProgramName.Text = ProgramName)));
                progressDisptacher = lsms.Dispatcher;

                // allowing the main UI thread to proceed 
                System.Windows.Threading.Dispatcher.Run();
            });
            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.IsBackground = true;
            uiThread.Priority = ThreadPriority.Highest;
            uiThread.Start();
        }

        /// <summary>
        /// Задать текст сообщения при загрузке
        /// </summary>
        /// <param name="message">Сообщение при загрузке</param>
        public void SetContent(string message)
        {
            this.DataContext = this;
            lsms.Dispatcher.Invoke((new Action(() =>
            {
                lsms.txt_Status.Text = message; // Задает сообщение в lable
            })));
        }

        /// <summary>
        /// Закрыть загрузочное окно
        /// </summary>
        public void CloseScreen()
        {
            this.Dispatcher.Invoke((new Action(() => this.Close())));
            uiThread.IsBackground = false;
            progressDisptacher.InvokeShutdown();
        }
    }
}
