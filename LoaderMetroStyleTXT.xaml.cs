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
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace SplashScreenDLL
{
    /// <summary>
    /// Логика взаимодействия для LoaderMetroStyleTXT.xaml
    /// </summary>
    public partial class LoaderMetroStyleTXT : Window, INotifyPropertyChanged
    {
        LoaderMetroStyleTXT lsms;                          // Объявляем этот класс
        Thread uiThread;                                   // Объявляем новый поток
        Dispatcher progressDisptacher;                     // Объявляем новый диспетчер

        private string _message = String.Empty;            // Объявляем переменную для сообщений на форму
        private string _softVersion = String.Empty;        // Объвление переменной для версии программы
        private string _companyDescription = String.Empty; // Объвление переменной для описание компании
        private string _companyName = String.Empty;        // Объвление переменной для названия программы

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
        /// Описание компании
        /// </summary>
        public string CompanyDescription
        {
            get
            {
                return _companyDescription;
            }
            set
            {
                _companyDescription = value;
                //NotifyPropertyChanged();
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("CompanyDescription");
            }
        }

        /// <summary>
        /// Название компании
        /// </summary>
        public string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
                //NotifyPropertyChanged();
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("CompanyName");
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
        /// Конструктор LoaderMetroStyleTXT
        /// </summary>
        public LoaderMetroStyleTXT()
        {
            InitializeComponent();
            //this.txt_SoftVersion.Text = "6.5";                                 // Версия программы
            //this.txt_Company.Text = "ПАЛИТРА СИСТЕМ";                          // Название компании
            //this.txt_Description.Text = "Автоматизация метрологических служб"; // Описание компании

            // Присваивание свойствам значений из по умолчанию
            this.SoftVersion = "6.5";                                         // Версия программы
            this.CompanyDescription = "Автоматизация метрологических служб";  // Описание программы
            this.CompanyName = "ПАЛИТРА СИСТЕМ";                              // Название компании
        }

        /// <summary>
        /// Конструктор LoaderMetroStyleTXT
        /// </summary>
        /// <param name="softVersion">Версия программы</param>
        /// <param name="companyName">Название компании</param>
        /// <param name="companyDescription">Описание компании</param>
        public LoaderMetroStyleTXT(string softVersion, string companyName, string companyDescription)
        {
            InitializeComponent();
            //this.txt_SoftVersion.Text = softVersion;        // Версия программы
            //this.txt_Company.Text = companyName;            // Название компании
            //this.txt_Description.Text = companyDescription; // Описание компании

            // Присваивание свойствам значений из конструктора
            this.SoftVersion = softVersion;               // Версия программы
            this.CompanyName = companyName;               // Название компании
            this.CompanyDescription = companyDescription; // Описание компании
        }

        /// <summary>
        /// Открыть загрузочное окно
        /// </summary>
        public void ShowScreen()
        {
            uiThread = new System.Threading.Thread(() =>
            {
                lsms = new LoaderMetroStyleTXT();
                lsms.Dispatcher.Invoke((new Action(() => lsms.Topmost = true)));
                lsms.Dispatcher.Invoke((new Action(() => lsms.Show())));
                lsms.Dispatcher.Invoke((new Action(() => lsms.txt_Status.Text = "Загрузка")));
                lsms.Dispatcher.Invoke((new Action(() => lsms.txt_SoftVersion.Text = SoftVersion)));
                lsms.Dispatcher.Invoke((new Action(() => lsms.txt_Company.Text = CompanyName)));
                lsms.Dispatcher.Invoke((new Action(() => lsms.txt_Description.Text = CompanyDescription)));
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
                Message = message;
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
