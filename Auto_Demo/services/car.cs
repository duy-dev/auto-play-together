using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Auto_Demo.services
{
    class car
    {
        MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public string color {get; set; }
        public int price { get; set; }
        public static ObservableCollection<car> _CarList = new ObservableCollection<car>();

        public car()
        {
            car._CarList.Add(this);
        }
    }
}
