using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Demo.services.config
{
    public class FishSize
    {
        public string _id { get; set; }
        public int size { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        public bool isChecked { get; set; }
        public string auto_id { get; set; }
        public bool disable { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public FishSize(string id, int _size, int _min, int _max, int _isChecked, string _auto_id, bool _disable = true)
        {
            _id = id;
            size = _size;
            min = _min;
            max = _max;
            isChecked = Convert.ToBoolean(_isChecked);
            auto_id = _auto_id;
            disable = _disable;
            switch (_size)
            {
                case 1:
                    X = 0;
                    Y = 0;
                    break;
                case 2:
                    X = 0;
                    Y = 1;
                    break;
                case 3:
                    X = 0;
                    Y = 2;
                    break;
                case 4:
                    X = 1;
                    Y = 0;
                    break;
                case 5:
                    X = 1;
                    Y = 1;
                    break;
                case 6:
                    X = 1;
                    Y = 2;
                    break;
            }
        }
    }

    public class FishSizeLite
    {
        public int isChecked { get; set; }
        public int min { get; set; }
        public int max { get; set; }

        public FishSizeLite(int _isChecked, int _min, int _max)
        {
            isChecked = _isChecked;
            min = _min;
            max = _max;
        }
    }
}
