using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Auto_Demo.services.config
{
    class fastConfigFishSize
    {
        public static MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public static List<fastConfigFishSize> _FastConfigFishSizeList = new List<fastConfigFishSize>();
        public string value { get; set; }
        public string label { get; set; }

        public fastConfigFishSize(string _value, string _label)
        {
            value = _value;
            label = _label;
            fastConfigFishSize._FastConfigFishSizeList.Add(this);
            wnd.updateListFastFishSize();
        }

        public static int getConfigMode(string value)
        {
            for (int i = 0; i < _FastConfigFishSizeList.Count; i++)
            {
                if (value == _FastConfigFishSizeList[i].value)
                {
                    return i;
                }
            }
            return 0;
        }
    }

    class languageConfig
    {
        public static MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public static List<languageConfig> _LanguageList = new List<languageConfig>();
        public int value { get; set; }
        public string label { get; set; }

        public languageConfig(int _value, string _label)
        {
            value = _value;
            label = _label;
            _LanguageList.Add(this);
            wnd.updateListLanguage(-1);
        }

        public static void initLanguageList()
        {
            if(_LanguageList.Count == 0)
            {
                new languageConfig(0, "Tiếng Việt");
                new languageConfig(1, "English");
            }
        }

        public static int getLanguageByAutoId(string autoId)
        {
            ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == autoId);
            return configAuto.language;
        }
    }

    public class SummaryTotal
    {
        public int quangCan { get; set; }
        public int boQuaCa { get; set; }
        public int dutDay { get; set; }
        public int cauLoi { get; set; }
        public int soCa { get; set; }
        public int doTaiChe { get; set; }
        public int tienBanCa { get; set; }
        public int tienSuaCan { get; set; }

        public SummaryTotal(int _quangCan, int _boQuaCa, int _dutDay, int _cauLoi, int _soCa, int _doTaiChe, int _tienBanCa, int _tienSuaCan)
        {
            quangCan = _quangCan;
            boQuaCa = _boQuaCa;
            dutDay = _dutDay;
            cauLoi = _cauLoi;
            soCa = _soCa;
            doTaiChe = _doTaiChe;
            tienBanCa = _tienBanCa;
            tienSuaCan = _tienSuaCan;
        }
    }

    public class SummaryTotalTime
    {
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }

        public SummaryTotalTime()
        {
            hour = 0;
            minute = 0;
            second = 0;
        }
    }

    public class SummaryFish
    {
        public int stt { get; set; }
        public int id { get; set; }
        public int money { get; set; }
        public int size { get; set; }
        public int level { get; set; }
        public int shadow { get; set; }
        public string image { get; set; }
        public string colorBackground { get; set; }
        public string colorBorder { get; set; }
        public string iconMoney { get; set; }
        public string sizeText { get; set; }
        public Visibility isVM { get; set; }

        public SummaryFish(int _stt, int _id, int _money, int _size, int _level, int _shadow, bool _isVM = false)
        {
            stt = _stt;
            id = _id;
            money = _money;
            size = _size;
            level = _level;
            shadow = _shadow;
            image = $"images/fish/{_id}.png";
            isVM = _isVM ? Visibility.Visible : Visibility.Hidden;
            switch (_level)
            {
                case 1:
                    colorBackground = "#19E4E0C5";
                    colorBorder = "#FFE4E0C5";
                    iconMoney = "images/icon/icon_money_1.png";
                    break;
                case 2:
                    colorBackground = "#19A3E467";
                    colorBorder = "#FFA3E467";
                    iconMoney = "images/icon/icon_money_1.png";
                    break;
                case 3:
                    colorBackground = "#1959C6D9";
                    colorBorder = "#FF59C6D9";
                    iconMoney = "images/icon/icon_money_1.png";
                    break;
                case 4:
                    colorBackground = "#19E793E8";
                    colorBorder = "#FFE793E8";
                    iconMoney = "images/icon/icon_money_1.png";
                    break;
                case 0:
                    colorBackground = "#19DDEDEE";
                    colorBorder = "#FFDDEDEE";
                    iconMoney = "images/icon/icon_recycle_1.png";
                    break;
                default:
                    colorBackground = "#19DDEDEE";
                    colorBorder = "#FFDDEDEE";
                    iconMoney = "images/icon/icon_recycle_1.png";
                    break;
            }
            switch (_size)
            {
                case 1:
                    sizeText = "bóng 1";
                    break;
                case 2:
                    sizeText = "bóng 2";
                    break;
                case 3:
                    sizeText = "bóng 3";
                    break;
                case 4:
                    sizeText = "bóng 4";
                    break;
                case 5:
                    sizeText = "bóng 5";
                    break;
                case 0:
                    sizeText = "";
                    break;
                default:
                    sizeText = "";
                    break;
            }
        }
    }

    public class SummaryFishSort
    {
        public int sortSTT { get; set; }
        public int sortLevel { get; set; }
        public int sortMoney { get; set; }
        public int sortSize { get; set; }
        public int sortShadow { get; set; }
        public List<SummaryFish> listSort { get; set; }
        public int typeSort { get; set; }
        public int typeSelect { get; set; }
        public int page { get; set; }
        public int Totalpage { get; set; }

        public SummaryFishSort()
        {
            sortSTT = 0;
            sortLevel = -1;
            sortMoney = 0;
            sortSize = 0;
            sortShadow = 0;
            listSort = new List<SummaryFish>();
            typeSort = 1;
            typeSelect = 2;
            page = 1;
            Totalpage = 1;
        }
    }

    class itemSelectLevel
    {
        public static MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public static List<itemSelectLevel> _LevelList = new List<itemSelectLevel>();
        public int value { get; set; }
        public string label { get; set; }
        public string color { get; set; }

        public itemSelectLevel(int _value, string _label, string _color)
        {
            value = _value;
            label = _label;
            color = _color;
            _LevelList.Add(this);
            wnd.updateListLevel(0);
        }

        public static void initLevelList()
        {
            if (_LevelList.Count == 0)
            {
                new itemSelectLevel(-1, "Tất cả", "#FF7D7D85");
                new itemSelectLevel(1, "Basic", "#FFE4E0C5");
                new itemSelectLevel(2, "Trendy", "#FFA3E467");
                new itemSelectLevel(3, "Luxury", "#FF59C6D9");
                new itemSelectLevel(4, "VIP", "#FFE793E8");
                new itemSelectLevel(0, "Trash", "#FFDDEDEE");
            }
        }
    }

    class itemSelectSize
    {
        public static MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public static List<itemSelectSize> _SizeList = new List<itemSelectSize>();
        public int value { get; set; }
        public string label { get; set; }

        public itemSelectSize(int _value, string _label)
        {
            value = _value;
            label = _label;
            _SizeList.Add(this);
            wnd.updateListSize(0);
        }

        public static void initSizeList()
        {
            if (_SizeList.Count == 0)
            {
                new itemSelectSize(0, "Tất cả");
                new itemSelectSize(1, "Bóng 1");
                new itemSelectSize(2, "Bóng 2");
                new itemSelectSize(3, "Bóng 3");
                new itemSelectSize(4, "Bóng 4");
                new itemSelectSize(5, "Bóng 5");
            }
        }
    }
}
