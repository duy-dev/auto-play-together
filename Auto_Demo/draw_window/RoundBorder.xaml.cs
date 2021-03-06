using Auto_Demo.services.config;
using AutoItX3Lib;
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
using Auto_Demo.services;

namespace Auto_Demo.draw_window
{
    /// <summary>
    /// Interaction logic for RoundBorder.xaml
    /// </summary>
    public partial class RoundBorder : Window
    {
        public static AutoItX3 autoit = new AutoItX3();
        public RoundBorder(pointXY center, int r1, int r2, string name, string auto_id)
        {
            InitializeComponent();
            int x1Win = autoit.WinGetPosX(name);
            int y1Win = autoit.WinGetPosY(name) + 31;

            win_Draw.Width = r2 * 2 + 10;
            win_Draw.Height = r2 * 2 + 10;
            win_Draw.Left = x1Win + center.x - r2 - 1;
            win_Draw.Top = y1Win + center.y - r2;

            drawRound.Margin = new Thickness(r2 - r1, r2 - r1, 0, 0);
            drawRound.Width = r1 * 2;
            drawRound.Height = r1 * 2;

            drawRound2.Width = r2 * 2;
            drawRound2.Height = r2 * 2;

            Auto AutoValue = Auto._ListAuto.Single(r => r.auto_id == auto_id);
            AutoValue._ListRounded.Add(this);
        }
    }
}
