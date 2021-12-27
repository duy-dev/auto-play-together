using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Auto_Demo.services.config
{
    public class ConfigAuto
    {
        public static MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public static ObservableCollection<ConfigAuto> _ListConfigAuto = new ObservableCollection<ConfigAuto>();

        public string _id { get; set; }
        public string auto_id { get; set; }
        public int delay_fishing_test { get; set; }
        public int language { get; set; }
        public int speed { get; set; }
        public int save_exclamation { get; set; }
        public int delay_check { get; set; }

        public ConfigAuto(string id, string _auto_id, int _delay_fishing_test, int _language, int _speed, int _save_exclamation, int _delay_check, bool isNew = false)
        {
            _id = id;
            auto_id = _auto_id;
            delay_fishing_test = _delay_fishing_test;
            language = _language;
            speed = _speed;
            save_exclamation = _save_exclamation;
            delay_check = _delay_check;

            _ListConfigAuto.Add(this);
            wnd.updateConfigAuto(_auto_id);
        }

        public static void changeLanguage(int idLanguage, string autoId, bool isUpdateUI = false)
        {
            _ListConfigAuto.Where(item => item.auto_id == autoId).ToList().ForEach(s => s.language = idLanguage);
            
            if (isUpdateUI)
            {
                bool isActive = emulator._EmulatorList.Single(r => r._id == autoId).isActive;
                if (isActive)
                {
                    wnd.updateLanguage(idLanguage);
                }
            }
        }

        public static void changeDataConfigAuto(string id, int value, string field)
        {
            database db = new database();
            string txtQuery = $"UPDATE Config SET {field}={value} WHERE _id='{id}'";
            try
            {
                db.ExecuteQuery(txtQuery);
                switch (field)
                {
                    case "delay_fishing_test":
                        _ListConfigAuto.Where(item => item._id == id).ToList().ForEach(s => s.delay_fishing_test = value);
                        break;
                    case "save_exclamation":
                        _ListConfigAuto.Where(item => item._id == id).ToList().ForEach(s => s.save_exclamation = value);
                        break;
                    case "speed":
                        _ListConfigAuto.Where(item => item._id == id).ToList().ForEach(s => s.speed = value);
                        break;
                    case "delay_check":
                        _ListConfigAuto.Where(item => item._id == id).ToList().ForEach(s => s.delay_check = value);
                        break;
                }

            }
            catch { }
        }

        public static void removeConfigAutoOneEmu(string idAuto)
        {
            foreach (ConfigAuto data in ConfigAuto._ListConfigAuto)
            {
                if (data.auto_id == idAuto)
                {
                    database db = new database();
                    try
                    {
                        string txtQuery = $"DELETE FROM Config WHERE _id='{data._id}'";
                        db.ExecuteQuery(txtQuery);
                    }
                    catch { }
                }
            }

        }
    }
}
