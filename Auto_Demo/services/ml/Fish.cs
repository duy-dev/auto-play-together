using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Auto_Demo.services.ml
{
    class Fish
    {
        
        public static List<Fish> _FishList = new List<Fish>();

        public int no { get; set; }
        public string name_vi { get; set; }
        public string name_check_vi { get; set; }
        public string[] name_arr_vi { get; set; }
        public int words_vi { get; set; }
        public string name_en { get; set; }
        public string name_check_en { get; set; }
        public string[] name_arr_en { get; set; }
        public int words_en { get; set; }
        public int size { get; set; }
        public int level { get; set; }

        public Fish(int _no, string _name_vi, string _name_check_vi, int _words_vi, string _name_en, string _name_check_en, int _words_en, int _size, int _level)
        {
            no = _no;
            name_vi = _name_vi;
            name_check_vi = _name_check_vi;
            name_arr_vi = name_check_vi.Split(new[] { ' ' });
            words_vi = _words_vi;
            name_en = _name_en;
            name_check_en = _name_check_en;
            name_arr_en = name_check_en.Split(new[] { ' ' });
            words_en = _words_en;
            size = _size;
            level = _level;

            _FishList.Add(this);
        }

        public Fish()
        {
            no = 0;
            name_vi = "";
            name_check_vi = "";
            name_arr_vi = name_check_vi.Split(new[] { ' ' });
            words_vi = 0;
            name_en = "";
            name_check_en = "";
            name_arr_en = name_check_en.Split(new[] { ' ' });
            words_en = 0;
            size = 0;
            level = 0;

            _FishList.Add(this);
        }

        public void clone(Fish item)
        {
            this.no = item.no;
            this.name_vi = item.name_vi;
            this.name_check_vi = item.name_check_vi;
            this.name_arr_vi = item.name_arr_vi;
            this.words_vi = item.words_vi;
            this.name_en = item.name_en;
            this.name_check_en = item.name_check_en;
            this.name_arr_en = item.name_arr_en;
            this.words_en = item.words_en;
            this.size = item.size;
            this.level = item.level;
        }

        public static void initListFish()
        {
            database db = new database();
            var data = db.getDataTable("Fish");

            foreach (DataRow rowFish in data.Rows)
            {
                int no = int.Parse(rowFish["no"].ToString());
                string name_vi = rowFish["name_vi"].ToString();
                string name_check_vi = rowFish["name_check_vi"].ToString();
                int words_vi = int.Parse(rowFish["words_vi"].ToString());
                string name_en = rowFish["name_en"].ToString();
                string name_check_en = rowFish["name_check_en"].ToString();
                int words_en = int.Parse(rowFish["words_en"].ToString());
                int size = int.Parse(rowFish["size"].ToString());
                int level = int.Parse(rowFish["level"].ToString());
                new Fish(no, name_vi, name_check_vi, words_vi, name_en, name_check_en, words_en, size, level);
            }

            List<Fish> _listFishCheck = _FishList.Where(x => x.level == 1 && x.words_vi == 2).ToList();
            //Debug.WriteLine(_FishList);
        }

        public static Fish getFishName(string nameRaw, int level, bool isVM, int language)
        {
            nameRaw = Regex.Replace(nameRaw, @"\r\n?|\n", " ").Trim();
            string nameOK = RemoveSpecialCharacters(RemoveUnicode(nameRaw));
            Debug.WriteLine(nameOK);
            string[] nameClearArr = nameOK.Split(new[] { ' ' });
            var nameClearArrCut = nameClearArr;
            if (isVM)
            {
                if(language == 0) {
                    nameClearArrCut = nameClearArrCut.Take(nameClearArrCut.Count() - 1).ToArray();
                    nameClearArrCut = nameClearArrCut.Take(nameClearArrCut.Count() - 1).ToArray();
                } else
                {
                    nameClearArrCut = nameClearArrCut.Skip(1).ToArray();
                }
            }
            Debug.WriteLine(nameClearArrCut.Length);
            if(nameClearArrCut.Length == 3 && nameClearArrCut[1] == "xe")
            {
                Debug.WriteLine("");
            }
            foreach(string text in nameClearArrCut)
            {
                Debug.WriteLine(text);
            }
            if (language == 0)
            {
                List<Fish> _listFishCheck = _FishList.Where(x => x.level == level && x.words_vi == nameClearArrCut.Length).ToList();
                int countMax = 0;
                Fish res = new Fish();

                foreach (Fish fish in _listFishCheck)
                {
                    int count = 0;
                    for (int i = 0; i < fish.name_arr_vi.Length; i++)
                    {
                        for (int j = 0; j < fish.name_arr_vi[i].Length; j++)
                        {
                            try
                            {
                                if (fish.name_arr_vi[i][j] == nameClearArrCut[i][j])
                                {
                                    count++;
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                    if (count > countMax)
                    {
                        countMax = count;
                        res.clone(fish);
                    }
                }
                return res;
            } else
            {
                List<Fish> _listFishCheck = _FishList.Where(x => x.level == level && x.words_en == nameClearArrCut.Length).ToList();
                int countMax = 0;
                Fish res = new Fish();

                foreach (Fish fish in _listFishCheck)
                {
                    int count = 0;
                    for (int i = 0; i < fish.name_arr_en.Length; i++)
                    {
                        for (int j = 0; j < fish.name_arr_en[i].Length; j++)
                        {
                            try
                            {
                                if (fish.name_arr_en[i][j] == nameClearArrCut[i][j])
                                {
                                    count++;
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                    if (count > countMax)
                    {
                        countMax = count;
                        res.clone(fish);
                    }
                }
                return res;
            }
        }

        public static string RemoveSpecialCharacters(string input)
        {
            input = input.TrimEnd(new char[] { '\r', '\n' });
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }

        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
    }
}
