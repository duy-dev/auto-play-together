using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Auto_Demo.services
{
    public class emulator
    {
        public static MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public string _id { get; set; }
        public string name { get; set; }
        public string emulator_id { get; set; }
        public string emulator_name { get; set; }
        public string emulator_type { get; set; }
        public string className { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public bool isActive { get; set; }

        // Config

        public bool isAuto { get; set; }
        public int tabSelect { get; set; }

        public static ObservableCollection<emulator> _EmulatorList = new ObservableCollection<emulator>();

        public emulator(string _id, string name, string emulator_id, string emulator_name, string emulator_type, bool isNew = false)
        {
            this._id = _id;
            this.name = name;
            this.emulator_id = emulator_id;
            this.emulator_name = emulator_name;
            this.emulator_type = emulator_type;
            //this.className = _className;
            //this.text = _text;
            this.isActive = false;
            this.isAuto = false;
            this.tabSelect = 0;
            switch (emulator_type)
            {
                case "custom":
                    this.icon = "images/android.png";
                    break;
                case "bluestacks":
                    this.icon = "images/bluestacks.png";
                    break;
                case "ldplayer":
                    this.icon = "images/ldplayer.png";
                    break;
                case "nox":
                    this.icon = "images/nox.png";
                    break;
                case "memu":
                    this.icon = "images/memu.png";
                    break;
            }
            emulator._EmulatorList.Add(this);
            if (isNew)
            {
                wnd.updateListEmulator(_id);
            } else
            {
                wnd.updateListEmulator();
            }
            

        }

        public static void changeActive(string idActive)
        {
            foreach(emulator emu in emulator._EmulatorList)
            {
                if (emu.isActive)
                {
                    emu.isActive = false;
                } else if (emu._id == idActive)
                {
                    emu.isActive = true;
                }
            }
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                wnd.updateListEmulator();
            });
            
        }

        public static void removeAllActive()
        {
            foreach (emulator emu in emulator._EmulatorList)
            {
                if (emu.isActive)
                {
                    emu.isActive = false;
                }
            }
            wnd.updateListEmulator();
        }

        public static emulator getEmulatorById(string id)
        {
            foreach (emulator emu in emulator._EmulatorList)
            {
                if (emu._id == id)
                {
                    return emu;
                }
            }
            return null;
        }

        public static void removeEmulatorById(string id)
        {
            foreach (emulator emu in emulator._EmulatorList)
            {
                if (emu._id == id)
                {
                    _EmulatorList.Remove(emu);
                    wnd.updateListEmulator();
                    break;
                }
            }
        }

        public static string getEmulatorIdActive()
        {
            foreach (emulator emu in emulator._EmulatorList)
            {
                if (emu.isActive)
                {
                    return emu._id;
                }
            }
            return "";
        }
    }
}
