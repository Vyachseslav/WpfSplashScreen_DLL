using PSLocalization;
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

namespace SplashScreenDLL
{
    /// <summary>
    /// Логика взаимодействия для SplashMetroTexted.xaml
    /// </summary>
    public partial class SplashMetroTexted : Window
    {
        SplashMetroTexted lsms;          // Объявляем этот класс
        System.Threading.Thread uiThread;                // Объявляем новый поток
        System.Windows.Threading.Dispatcher progressDisptacher;  // Объявляем новый диспетчер
        private DependencyObject parent;

        // Свойства окна-родителя, вызывающего загрузочное окно.
        double HIn { get; set; }        // Высота окна.
        double WIn { get; set; }        // Ширина окна.
        double LIn { get; set; }        // this.Left.
        double TIn { get; set; }        // this.Top.

        // Текст и цвет формы.
        string TextMessage { get; set; } // Сообщение пользователю.
        string TextLoad { get; set; }    // Сообщение при загрузке.
        string BgColor { get; set; }     // Цвет окна.

        static Window Parrent { get; set; }     // Окно, вызывающее этот SplashScreen.

        private double HeigthScreen { get; set; } // Высота экрана.
        double WidthScreen { get; set; } // Ширина экрана.
        double SplashHeigth { get; set; } // Высота загрузочного окна.
        double SplashWidth { get; set; }  // Ширина загрузочного окна.

        /// <summary>
        /// Запуск окна по центру экрана (true).
        /// </summary>
        bool IsCenterScreen { get; set; } // Запук по центру экрана или нет.

        /// <summary>
        /// Запуск загрузочного окна по умолчанию. Текст из локализации.
        /// </summary>
        bool IsDefault { get; set; }

        /// <summary>
        /// Загрузка параметров по умолчанию.
        /// </summary>
        private void DefaultParameters()
        {
            IsDefault = true;
            
            // Запуск по центру экрана.
            IsCenterScreen = true;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Цвет фона.
            BgColor = "#FFFFFF";
            // Устанавливаем цвет формы.
            BrushConverter bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(BgColor);

            // Текст из локазизации.
            LoadTextFromXML();
            txtTextUp.Text = TextMessage;
            txtTextDown.Text = TextLoad;
        }

        /// <summary>
        /// Загрузить тексты из XML-файла локализации.
        /// </summary>
        private void LoadTextFromXML()
        {
            MainXML xml = new MainXML();
            string loc = LanguageFromBase.GetLanguage();
            xml = xml.DeserializeFromXml("XML\\PSLocalization_" + loc + ".xml");
            PSForm psFormMain = xml.Programmers["ErmilovVA"].Applications["MainStrings"].Forms["MainStrings"];

            string PleaseWait = MainXML.GetString(psFormMain, "PleaseWait");
            string LoadingProcess = MainXML.GetString(psFormMain, "LoadingProcess") + " ...";

            TextMessage = PleaseWait;
            TextLoad = LoadingProcess;
        }


        /// <summary>
        /// Загрущочное окно SplashMetroTexted (по умолчанию)
        /// </summary>
        public SplashMetroTexted()
        {
            InitializeComponent();

            DefaultParameters(); // Загрузка параметров по умолчанию из локализации.
        }

        /// <summary>
        /// Окно загрузки в стиле Metro по центру родительского окна.
        /// </summary>
        /// <param name="parrentWindow">Родительское окно.</param>
        /// <param name="textMessage">Сообщение пользователю.</param>
        /// <param name="textLoad">Текст, обозначающий загрузку.</param>
        /// <param name="bgColor">Цвет формы (по умолчанию - #E0FFFF).</param>
        public SplashMetroTexted(Window parrentWindow, string textMessage, string textLoad, string bgColor = "#E0FFFF")
        {
            InitializeComponent();

            IsCenterScreen = false;
            // Окно - родитель.
            Parrent = parrentWindow;
            HIn = parrentWindow.Height;
            WIn = parrentWindow.Width;
            LIn = parrentWindow.Left;
            TIn = parrentWindow.Top;

            // Сообщения и цвет. Задаем свойства.
            TextMessage = textMessage;
            TextLoad = textLoad;
            BgColor = bgColor;

            // Вычисляем положение центра родительской формы.
            this.Left = LIn + (WIn / 2) - (this.Width / 2);
            this.Top = TIn + (HIn / 2) - (this.Height / 2);

            // Задаем текст сообщений.
            txtTextUp.Text = TextMessage;
            txtTextDown.Text = TextLoad;

            // Устанавливаем цвет формы.
            BrushConverter bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(bgColor);
        }

        /// <summary>
        /// Конструктор для запуска по центру экрана c Вашим текстом.
        /// </summary>
        /// <param name="textMessage">Ваше сообщение.</param>
        /// <param name="textLoad">Ваше текст.</param>
        /// <param name="bgColor">Цвет формы (по умолчанию - #E0FFFF).</param>
        public SplashMetroTexted(string textMessage, string textLoad, string bgColor = "#E0FFFF")
        {
            InitializeComponent();

            IsCenterScreen = true;
            // Сообщения и цвет.
            TextMessage = textMessage;
            TextLoad = textLoad;
            BgColor = bgColor;
            // Запуск по центру экрана.
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // Задаем текст сообщений.
            txtTextUp.Text = TextMessage;
            txtTextDown.Text = TextLoad;
            // Устанавливаем цвет формы.
            BrushConverter bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(bgColor);
        }

        private SplashMetroTexted(double height, double width, double left, double top, string textMessage, string textLoad, string bgColor = "#E0FFFF")
        {
            InitializeComponent();

            // Размеры и положение родителя.
            HIn = height;
            WIn = width;
            LIn = left;
            TIn = top;

            TextMessage = textMessage;
            TextLoad = textLoad;
            BgColor = bgColor;

            // Вычисляем положение центра родительской формы.
            this.Top = TIn + (HIn / 2) - (this.Height / 2);
            this.Left = LIn + (WIn / 2) - (this.Width / 2);

            Parrent.LocationChanged += Parrent_LocationChanged; // Событие при изменении положения родительского окна.
            Parrent.SizeChanged += Parrent_SizeChanged; // Событие, при изменении размера родительского окна.

            Parrent.Deactivated += Parrent_Deactivated; // Событие, происходящее при деактивации окна.
            Parrent.Activated += Parrent_Activated; // Событие, происходящее при активации окна.

            // Задаем текст сообщений.
            txtTextUp.Text = TextMessage;
            txtTextDown.Text = TextLoad;

            // Устанавливаем цвет формы.
            BrushConverter bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFromString(BgColor);
        }

        void Parrent_Activated(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(()=>this.Visibility = System.Windows.Visibility.Visible));
            this.Dispatcher.Invoke(new Action(() => this.Activate()));
            this.Dispatcher.Invoke(new Action(() => this.Topmost = true));    
        }

        void Parrent_Deactivated(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() => this.Visibility = System.Windows.Visibility.Visible));
            this.Dispatcher.Invoke(new Action(() => this.Topmost = false));
        }

        /// <summary>
        /// Изменить размер родительского окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Parrent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Чтобы загрузочное окно передвигалось с главным.
            if (Parrent.WindowState == System.Windows.WindowState.Maximized)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);
                    this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
                }));
            }
            else
            {
                Parrent.Dispatcher.Invoke(new Action(() => HIn = Parrent.Height));
                Parrent.Dispatcher.Invoke(new Action(() => WIn = Parrent.Width));
                Parrent.Dispatcher.Invoke(new Action(() => LIn = Parrent.Left));
                Parrent.Dispatcher.Invoke(new Action(() => TIn = Parrent.Top));

                // Вычисляем положение центра родительской формы.
                this.Dispatcher.Invoke(new Action(() =>
                {
                    this.Top = TIn + (HIn / 2) - (this.Height / 2);
                    this.Left = LIn + (WIn / 2) - (this.Width / 2);
                }));
            }
        }

        /// <summary>
        /// Изменить положение родительского окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Parrent_LocationChanged(object sender, EventArgs e)
        {
            // Чтобы загрузочное окно передвигалось с главным.
            Parrent.Dispatcher.Invoke(new Action(() => HIn = Parrent.Height));
            Parrent.Dispatcher.Invoke(new Action(() => WIn = Parrent.Width));
            Parrent.Dispatcher.Invoke(new Action(() => LIn = Parrent.Left));
            Parrent.Dispatcher.Invoke(new Action(() => TIn = Parrent.Top));
            this.Dispatcher.Invoke(new Action(() => this.Left = LIn + (WIn / 2) - (this.Width / 2)));
            this.Dispatcher.Invoke(new Action(() => this.Top = TIn + (HIn / 2) - (this.Height / 2)));
        }

        /// <summary>
        /// Открытие окна загрузки
        /// </summary>
        public void ShowScreen()
        {
            uiThread = new System.Threading.Thread(() =>
            {
                if(IsDefault == true)
                {
                    lsms = new SplashMetroTexted();
                }
                else
                {
                    switch (IsCenterScreen)
                    {
                        case true:
                            lsms = new SplashMetroTexted(TextMessage, TextLoad, BgColor);
                            break;
                        case false:
                            lsms = new SplashMetroTexted(HIn, WIn, LIn, TIn, TextMessage, TextLoad, BgColor);
                            break;
                            /*default:
                                lsms = new SplashMetroTexted();
                                break;
                             * */
                    }
                }
                
                lsms.Dispatcher.Invoke((new Action(() => lsms.Show())));
                progressDisptacher = lsms.Dispatcher;
                System.Windows.Threading.Dispatcher.Run();
            });
            uiThread.SetApartmentState(System.Threading.ApartmentState.STA);
            uiThread.IsBackground = true;
            uiThread.Priority = System.Threading.ThreadPriority.Highest;
            uiThread.Start();
        }

        /// <summary>
        /// Закрытие окна загрузки
        /// </summary>
        public void CloseScreen()
        {
            this.Dispatcher.Invoke(new Action(() => this.Close()));
            uiThread.IsBackground = false;
            if (Parrent != null)
                Parrent.Dispatcher.Invoke(new Action(() => Parrent.Activate()));
            while (progressDisptacher == null)
                continue;
            progressDisptacher.InvokeShutdown();
        }
    }
}
