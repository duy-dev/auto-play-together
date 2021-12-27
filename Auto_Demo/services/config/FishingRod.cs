using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Auto_Demo.services.config
{
    public class FishingRodConfig
    {
        public static MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public static ObservableCollection<FishingRodConfig> _ListFishingRodConfig = new ObservableCollection<FishingRodConfig>();

        public string _id { get; set; }
        public string auto_id { get; set; }
        public int fishing_rod_1 { get; set; }
        public int fishing_rod_2 { get; set; }
        public bool change_fishing_rod { get; set; }
        public int step_change { get; set; }
        public bool get_info_rod { get; set; }

        public FishingRodConfig(string id, string _auto_id, int _fishing_rod_1, int _fishing_rod_2, int _change_fishing_rod, int _step_change, int _get_info_rod, bool isNew = false)
        {
            _id = id;
            auto_id = _auto_id;
            fishing_rod_1 = _fishing_rod_1;
            fishing_rod_2 = _fishing_rod_2;
            change_fishing_rod = Convert.ToBoolean(_change_fishing_rod);
            step_change = _step_change;
            get_info_rod = Convert.ToBoolean(_get_info_rod);
            _ListFishingRodConfig.Add(this);

            if(isNew)
            {
                wnd.updateConfigFishingRod(_auto_id);
            }
        }

        public static FishingRodConfig getFishingRodByIdAuto(string id)
        {
            foreach (FishingRodConfig data in _ListFishingRodConfig)
            {
                if (data.auto_id == id)
                {
                    return data;
                }
            }
            return null;
        }

        public static void changeDataFishingRod(string id, int value, string field)
        {
            database db = new database();
            string txtQuery = $"UPDATE FishingRod SET {field}={value} WHERE _id='{id}'";
            try
            {
                db.ExecuteQuery(txtQuery);
                switch(field)
                {
                    case "fishing_rod_1":
                        _ListFishingRodConfig.Where(item => item._id == id).ToList().ForEach(s => s.fishing_rod_1 = value);
                        break;
                    case "fishing_rod_2":
                        _ListFishingRodConfig.Where(item => item._id == id).ToList().ForEach(s => s.fishing_rod_2 = value);
                        break;
                    case "change_fishing_rod":
                        _ListFishingRodConfig.Where(item => item._id == id).ToList().ForEach(s => s.change_fishing_rod = Convert.ToBoolean(value));
                        break;
                    case "step_change":
                        _ListFishingRodConfig.Where(item => item._id == id).ToList().ForEach(s => s.step_change = value);
                        break;
                    case "get_info_rod":
                        _ListFishingRodConfig.Where(item => item._id == id).ToList().ForEach(s => s.get_info_rod = Convert.ToBoolean(value));
                        break;
                }
                
            }
            catch { }
        }

        public static void removeConfigFishingRod(string idAuto)
        {
            database db = new database();
            var itemToRemove = _ListFishingRodConfig.Single(r => r.auto_id == idAuto);
            string txtQuery = $"DELETE FROM FishingRod WHERE _id='{itemToRemove._id}'";
            
            try
            {
                db.ExecuteQuery(txtQuery);
                _ListFishingRodConfig.Remove(itemToRemove);
            } catch { }
        }
    }

    public class FishingRod
    {
        public static ObservableCollection<FishingRod> _ListFishingRod = new ObservableCollection<FishingRod>();

        public string label { get; set; }
        public int value { get; set; }
        public string icon { get; set; }

        public FishingRod(string _label, int _value, string _icon)
        {
            label = _label;
            value = _value;
            icon = _icon;
        }

        public static void CreateListFishingRod()
        {
            _ListFishingRod.Add(new FishingRod("Cần câu nhánh cây", 1, "images/fishing-rod/01.png"));
            _ListFishingRod.Add(new FishingRod("Cần câu mèo", 2, "images/fishing-rod/02.png"));
            _ListFishingRod.Add(new FishingRod("Cần câu đơn giản", 3, "images/fishing-rod/03.png"));
            _ListFishingRod.Add(new FishingRod("Cần câu phao vịt", 4, "images/fishing-rod/04.png"));
            _ListFishingRod.Add(new FishingRod("Cần câu không chuyên", 5, "images/fishing-rod/05.png"));
            _ListFishingRod.Add(new FishingRod("Cần câu chuyên nghiệp", 6, "images/fishing-rod/06.png"));
            _ListFishingRod.Add(new FishingRod("Cần câu xiếc sư tử", 7, "images/fishing-rod/07.png"));
            _ListFishingRod.Add(new FishingRod("Cần câu gậy thần kỳ", 8, "images/fishing-rod/08.png"));
            _ListFishingRod.Add(new FishingRod("Cần câu kiếm huyền thoại", 9, "images/fishing-rod/09.png"));
            _ListFishingRod.Add(new FishingRod("Cần câu gậy phép thuật", 9, "images/fishing-rod/10.png"));
        }
    }
}
