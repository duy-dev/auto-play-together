using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Application = System.Windows.Application;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace Auto_Demo.components
{
    /// <summary>
    /// Interaction logic for CMessageBox.xaml
    /// </summary>
    public partial class CMessageBox : Window
    {
        public CMessageBox()
        {
            InitializeComponent();
            setStartingPosition();
        }

        private int heightPopup = 200;
        private int widthPopup = 380;

        static CMessageBox cMessageBox;
        static DialogResult result = System.Windows.Forms.DialogResult.No;
        public string messageTitle { get; set; }
        public string messageContent { get; set; }
        public string messageButtonCancel { get; set; }
        public string messageButtonOK { get; set; }

        private void Window_MouseMove(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void setStartingPosition()
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.widthPopup) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.heightPopup) / 2;
        }

        private void hover_Close(object sender, MouseEventArgs e)
        {
            icon_ClosePrimary1.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#EF4444");
            icon_ClosePrimary2.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#EF4444");
            icon_CloseSecondary.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#EF4444");

            icon_ClosePrimaryGroup.Opacity = 1;
            icon_CloseSecondary.Opacity = 0.5;
        }

        private void hover_CloseOut(object sender, MouseEventArgs e)
        {
            icon_ClosePrimary1.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#EF4444");
            icon_ClosePrimary2.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#EF4444");
            icon_CloseSecondary.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFFFF");

            icon_ClosePrimaryGroup.Opacity = 0.7;
            icon_CloseSecondary.Opacity = 1;
        }

        public static DialogResult Show(string title, string content, string okButton, string cancelButton)
        {
            cMessageBox = new CMessageBox();
            cMessageBox.tb_Title.Text = title;
            cMessageBox.tb_Content.Text = content;
            cMessageBox.tb_CancelButton.Text = cancelButton;
            cMessageBox.tb_OKButton.Text = okButton;

            cMessageBox.ShowDialog();
            return result;
        }

        private void click_cancelButton(object sender, MouseButtonEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            Thread.Sleep(200);
            cMessageBox.Close();
        }

        private void click_okButton(object sender, MouseButtonEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.Yes;
            Thread.Sleep(200);
            cMessageBox.Close();
        }
    }
}
