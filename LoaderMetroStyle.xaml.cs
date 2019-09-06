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
using System.Windows.Threading;
using System.Windows;


namespace SplashScreenDLL
{
    /// <summary>
    /// Логика взаимодействия для LoaderMetroStyle.xaml
    /// </summary>
    public partial class LoaderMetroStyle : Window
    {
        LoaderMetroStyle lsms;          // Объявляем этот класс
        Thread uiThread;                // Объявляем новый поток
        Dispatcher progressDisptacher;  // Объявляем новый диспетчер

        // Свойства окна-родителя, вызывающего загрузочное окно.
        double hIn { get; set; }        // Высота окна.
        double wIn { get; set; }        // Ширина окна.
        double lIn { get; set; }        // this.Left.
        double tIn { get; set; }        // this.Top.

        /// <summary>
        /// Окно загрузки в стиле Metro.
        /// </summary>
        public LoaderMetroStyle()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen; // Запуск по центру экрана.
        }

        /// <summary>
        /// Окно загрузки в стиле Metro.
        /// </summary>
        /// <param name="parrentWindow">Родительское окно.</param>
        public LoaderMetroStyle(Window parrentWindow)
        {
            InitializeComponent();

            // Размеры и положение родителя.
            hIn = parrentWindow.Height;
            wIn = parrentWindow.Width;
            lIn = parrentWindow.Left;
            tIn = parrentWindow.Top;

            // Вычисляем положение центра родительской формы.
            this.Top = tIn + (hIn / 2) - (this.Height / 2);
            this.Left = lIn + (wIn / 2) - (this.Width / 2);
        }

        /// <summary>
        /// Окно загрузки в стиле Metro.
        /// </summary>
        /// <param name="height">Высота родительского окна.</param>
        /// <param name="width">Ширина родительского окна.</param>
        /// <param name="left">Положение левого угла родительской формы.</param>
        /// <param name="top">Положение верхнего угла родительской формы.</param>
        public LoaderMetroStyle(double height, double width, double left, double top)
        {
            InitializeComponent();

            // Размеры и положение родителя.
            hIn = height;
            wIn = width;
            lIn = left;
            tIn = top;

            // Вычисляем положение центра родительской формы.
            this.Top = tIn + (hIn / 2) - (this.Height / 2);
            this.Left = lIn + (wIn / 2) - (this.Width / 2);
        }


        /// <summary>
        /// Открытие окна загрузки
        /// </summary>
        [STAThread]
        public void ShowScreen()
        {
            uiThread = new System.Threading.Thread(() =>
            {
                lsms = new LoaderMetroStyle(hIn, wIn, lIn, tIn);
                //lsms.Dispatcher.Invoke((new Action(() => lsms.Topmost = true)));
                lsms.Dispatcher.Invoke((new Action(() => lsms.Show())));
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
        /// Закрытие окна загрузки
        /// </summary>
        public void CloseScreen()
        {
            this.Dispatcher.Invoke((new Action(() => this.Close())));
            uiThread.IsBackground = false;
            progressDisptacher.InvokeShutdown();
        }
    }
}
