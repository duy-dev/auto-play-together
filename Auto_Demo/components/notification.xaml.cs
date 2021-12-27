using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Auto_Demo.components
{
    /// <summary>
    /// Interaction logic for notification.xaml
    /// </summary>
    public partial class notification : UserControl
    {
        public string color2 { get; set; }
        public string colorMessage1 { get; set; }
        public string colorMessage2 { get; set; }
        public string colorMessage3 { get; set; }
        public string colorMessage4 { get; set; }

        public string iconSuccess { get; set; }
        public string iconError { get; set; }
        public string iconInfo { get; set; }
        public string iconWarning { get; set; }

        public string colorTitle { get; set; }


        List<string> colorSuccess = new List<string> { "#FF062C21", "#FF032118", "#FF021710", "#FF0A060E" };
        List<string> colorError = new List<string> { "#FF360C0C", "#FF2C0707", "#FF1F0404", "#FF0A060E" };
        List<string> colorInfo = new List<string> { "#FF091830", "#FF061227", "#FF040F21", "#FF0A060E" };
        List<string> colorWarning = new List<string> { "#FF362F0B", "#FF2B2507", "#FF1D1903", "#FF0A060E" };
        //public string color = "#FF062C21";
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(notification), new PropertyMetadata(string.Empty));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty ContentsProperty =
            DependencyProperty.Register("Contents", typeof(string), typeof(notification), new PropertyMetadata(string.Empty));

        public string Contents
        {
            get { return (string)GetValue(ContentsProperty); }
            set { SetValue(ContentsProperty, value); }
        }

        public static readonly DependencyProperty TypeMessageProperty =
            DependencyProperty.Register("TypeMessage", typeof(string), typeof(notification), new FrameworkPropertyMetadata(
                 new PropertyChangedCallback(ChangeText)));

        public string TypeMessage
        {
            get { return (string)GetValue(TypeMessageProperty); }
            set { SetValue(TypeMessageProperty, value); }
        }

        private static void ChangeText(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as notification).initColor(e.NewValue.ToString());
        }


        public notification()
        {
            InitializeComponent();
        }

        void initColor(string aa)
        {
            iconSuccess = "Hidden";
            iconError = "Hidden";
            iconInfo = "Hidden";
            iconWarning = "Hidden";
            switch (aa)
            {
                case "success":
                    colorMessage1 = colorSuccess[0];
                    colorMessage2 = colorSuccess[1];
                    colorMessage3 = colorSuccess[2];
                    colorMessage4 = colorSuccess[3];
                    iconSuccess = "Visible";
                    colorTitle = "#FF10B981";
                    break;
                case "error":
                    colorMessage1 = colorError[0];
                    colorMessage2 = colorError[1];
                    colorMessage3 = colorError[2];
                    colorMessage4 = colorError[3];
                    iconError = "Visible";
                    colorTitle = "#FFEF4444";
                    break;
                case "info":
                    colorMessage1 = colorInfo[0];
                    colorMessage2 = colorInfo[1];
                    colorMessage3 = colorInfo[2];
                    colorMessage4 = colorInfo[3];
                    iconInfo = "Visible";
                    colorTitle = "#FF3B82F6";
                    break;
                case "warning":
                    colorMessage1 = colorWarning[0];
                    colorMessage2 = colorWarning[1];
                    colorMessage3 = colorWarning[2];
                    colorMessage4 = colorWarning[3];
                    iconWarning = "Visible";
                    colorTitle = "#FFFDE047";
                    break;
            }
        }
    }
}
