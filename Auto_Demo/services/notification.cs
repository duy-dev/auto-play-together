using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using Thread = System.Threading.Thread;
using System.Windows.Threading;

namespace Auto_Demo.services
{
    public class Notification
    {
        MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public string id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string type { get; set; }
        public DateTime timeStrat { get; set; }

        public static ObservableCollection<Notification> _NotiList = new ObservableCollection<Notification>();

        public Notification()
        {
            Notification._NotiList.Add(this);
            wnd.updateNoti();
            this.HiddenNoti();
        }

        public void HiddenNoti()
        {
            Task taskHidden = new Task(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    TimeSpan timeShow = DateTime.Now - this.timeStrat;
                    if (timeShow.TotalSeconds > 6)
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            _NotiList.Remove(this);
                        });
                        return;
                    }
                }
            });
            taskHidden.Start();
        }
    }
}
