using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using AutoItX3Lib;

namespace Auto_Demo.test
{
    /// <summary>
    /// Interaction logic for draw.xaml
    /// </summary>
    public partial class draw : Window
    {
        public static ObservableCollection<draw> _ListDraw = new ObservableCollection<draw>();
        public static AutoItX3 autoit = new AutoItX3();
        public draw(int x1, int y1, int x2, int y2, string name, string color)
        {
            InitializeComponent();

            int x1Win = autoit.WinGetPosX(name);
            int y1Win = autoit.WinGetPosY(name) + 31;

            win_Draw.Left = x1Win + x1 - 1;
            win_Draw.Top = y1Win + y1;
            drawLine.Width = x2 + 2;
            drawLine.Height = y2 + 2;
            drawLine.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(color);
            _ListDraw.Add(this);
        }
    }
}
