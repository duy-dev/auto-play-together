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
using AutoItX3Lib;
using Auto_Demo.services;

namespace Auto_Demo.draw_window
{
    /// <summary>
    /// Interaction logic for FishSizeBorder.xaml
    /// </summary>
    public partial class FishSizeBorder : Window
    {
        public static AutoItX3 autoit = new AutoItX3();
        public FishSizeBorder(int x1, int y1, int x2, int y2, string name, string color, int size, string auto_id)
        {
            InitializeComponent();

            int x1Win = autoit.WinGetPosX(name);
            int y1Win = autoit.WinGetPosY(name) + 31;

            win_Draw.Left = x1Win + x1 - 1;
            win_Draw.Top = y1Win + y1;
            drawLine.Width = x2 + 2;
            drawLine.Height = y2 + 2;
            drawLine.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(color);
            tb_Size.Text = size.ToString();
            if(x2 < y2 && x2 < 20)
            {
                tb_Size.LayoutTransform = new RotateTransform(-90);
                if(x2 * 0.85 > 16)
                {
                    tb_Size.FontSize = 16;
                } else
                {
                    tb_Size.FontSize = x2 * 0.85;
                }
            } else
            {
                tb_Size.LayoutTransform = new RotateTransform(0);
                if (y2 * 0.85 > 16)
                {
                    tb_Size.FontSize = 16;
                }
                else
                {
                    tb_Size.FontSize = y2 * 0.85;
                }
            }
            Auto AutoValue = Auto._ListAuto.Single(r => r.auto_id == auto_id);
            AutoValue._ListDrawSize.Add(this);
        }
    }
}
