/* 
 *  Не используемые библиотеки
 *  
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
*/

using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace SplashScreenDLL
{
    /// <summary>
    /// Логика взаимодействия для LoaderPalitra.xaml
    /// </summary>
    public partial class LoaderPalitra : Window
    {
        LoaderPalitra lsms;            // Объявляем этот класс
        Thread uiThread;               // Объявляем новый поток
        Dispatcher progressDisptacher; // Объявляем новый диспетчер

        /// <summary>
        /// Конструктор класса LoaderPalitra
        /// </summary>
        public LoaderPalitra()
        {
            InitializeComponent();
            this.Title = "Загрузка";
        }

        /// <summary>
        /// Открыть загрузочное окно
        /// </summary>
        public void ShowScreen()
        {
            uiThread = new System.Threading.Thread(() =>
            {
                lsms = new LoaderPalitra();
                lsms.Dispatcher.Invoke((new Action(() => lsms.Topmost = true)));
                lsms.Dispatcher.Invoke((new Action(() => lsms.Show())));
                lsms.Dispatcher.Invoke((new Action(() => lsms.barText.Content = "Загрузка")));
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
        /// Добавить сообщение на загрузочный экран и задать 
        /// значение ProgressBar
        /// </summary>
        /// <param name="message">Ваше сообщение</param>
        /// <param name="value">Значение ProgressBar</param>
        public void SetContent(string message, double value)
        {
            lsms.Dispatcher.Invoke((new Action(() =>
            {
                lsms.barText.Content = message; // Задает сообщение в lable
                lsms.Bar.Value = value;         // Задает значение в ProgressBar
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
