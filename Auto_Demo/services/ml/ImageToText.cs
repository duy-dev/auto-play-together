using Auto_Demo.services.config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Auto_Demo.services.ml
{
    public class FishingRodDetect {
        public int id { get; set; }
        public string name { get; set; }
        public string nameDetect { get; set; }
        public string[] nameArr { get; set; }
        public int slot { get; set; }
        public int remaining { get; set; }
        public int repairCost { get; set; }
        public int index { get; set; }
        public percentXY point { get; set; }

        public FishingRodDetect()
        {
            id = 0;
            name = "";
            nameDetect = "";
            nameArr = name.Split(new[] { ' ' });
            slot = 0;
            remaining = 0;
            repairCost = 0;
        }

        public FishingRodDetect(int _id, string _name, string _nameDetect, int _slot, int _repairCost)
        {
            id = _id;
            name = _name;
            nameDetect = _nameDetect;
            nameArr = name.Split(new[] {' '});
            slot = _slot;
            remaining = _slot;
            repairCost = _repairCost;
        }

        public void clone(FishingRodDetect item)
        {
            this.id = item.id;
            this.name = item.name;
            this.nameDetect = item.nameDetect;
            this.nameArr = item.nameArr;
            this.slot = item.slot;
            this.remaining = item.remaining;
            this.index = item.index;
            this.point = item.point;
            this.repairCost = item.repairCost;
        }
    }

    public class ImageToText
    {

        public static List<FishingRodDetect> _listFishingRod = new List<FishingRodDetect>();
        public static List<FishingRodDetect> _listFishingRodEN = new List<FishingRodDetect>();
        public static FishingRodDetect getFishingName(string nameRaw)
        {
            Debug.WriteLine(nameRaw);
            string[] nameClearArr = RemoveSpecialCharacters(RemoveUnicode(nameRaw)).Split(new[] { ' ' });
            var nameClearArrCut = nameClearArr.Skip(1).ToArray().Skip(1).ToArray();
            //string name = string.Join(" ", Client);
            int countMax = 0;
            FishingRodDetect res = new FishingRodDetect();

            foreach (FishingRodDetect rod in _listFishingRod)
            {
                int count = 0;
                for(int i = 0; i < rod.nameArr.Length; i++)
                {
                    for(int j = 0; j < rod.nameArr[i].Length; j++)
                    {
                        try
                        {
                            if (rod.nameArr[i][j] == nameClearArrCut[i][j])
                            {
                                count++;
                            }
                        } catch
                        {

                        }
                    }
                }
                if (count > countMax)
                {
                    countMax = count;
                    res.clone(rod);
                }
            }
            return res;
        }

        public static FishingRodDetect getFishingNameEN(string nameRaw)
        {
            string[] nameClearArr = RemoveSpecialCharacters(RemoveUnicode(nameRaw)).Split(new[] { ' ' });
            var nameClearArrCut = nameClearArr.Take(nameClearArr.Count() - 1).ToArray();
            //string name = string.Join(" ", Client);
            int countMax = 0;
            FishingRodDetect res = new FishingRodDetect();

            foreach (FishingRodDetect rod in _listFishingRodEN)
            {
                int count = 0;
                for (int i = 0; i < rod.nameArr.Length; i++)
                {
                    for (int j = 0; j < rod.nameArr[i].Length; j++)
                    {
                        try
                        {
                            if (rod.nameArr[i][j] == nameClearArrCut[i][j])
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
                    res.clone(rod);
                }
            }
            return res;
        }

        public static void initNameFishing()
        {
            if (_listFishingRod.Count == 0)
            {
                _listFishingRod.Add(new FishingRodDetect(1, "nhánh cây", "nhanh cay", 15, 100));
                _listFishingRod.Add(new FishingRodDetect(2, "mèo", "meo", 20, 180));
                _listFishingRod.Add(new FishingRodDetect(3, "đơn giản", "don gian", 35, 500));
                _listFishingRod.Add(new FishingRodDetect(4, "phao vịt", "phao vit", 35, 500));
                _listFishingRod.Add(new FishingRodDetect(5, "không chuyên", "khong chuyen", 40, 750));
                _listFishingRod.Add(new FishingRodDetect(6, "chuyên nghiệp", "chuyen nghiep", 80, 1500));
                _listFishingRod.Add(new FishingRodDetect(7, "xiếc sư tử", "xiec su tu", 80, 750));
                _listFishingRod.Add(new FishingRodDetect(8, "gậy thần kỳ", "gay than ky", 80, 750));
                _listFishingRod.Add(new FishingRodDetect(9, "kiếm huyền thoại", "kiem huyen thoai", 80, 750));
                _listFishingRod.Add(new FishingRodDetect(10, "gậy phép thuật", "gay phep thuat", 80, 2000));
            }

            if (_listFishingRodEN.Count == 0)
            {
                _listFishingRodEN.Add(new FishingRodDetect(1, "Wooden", "Wooden", 15, 100));
                _listFishingRodEN.Add(new FishingRodDetect(2, "Feline", "Feline", 20, 180));
                _listFishingRodEN.Add(new FishingRodDetect(3, "Pocket", "Pocket", 35, 500));
                _listFishingRodEN.Add(new FishingRodDetect(4, "Duck", "Duck", 35, 500));
                _listFishingRodEN.Add(new FishingRodDetect(5, "Amateur", "Amateur", 40, 750));
                _listFishingRodEN.Add(new FishingRodDetect(6, "Pro", "Pro", 80, 1500));
                _listFishingRodEN.Add(new FishingRodDetect(7, "Lion Circus", "Lion Circus", 80, 750));
                _listFishingRodEN.Add(new FishingRodDetect(8, "Magical", "Magical", 80, 750));
                _listFishingRodEN.Add(new FishingRodDetect(9, "Legendary Sword Fishing", "Legendary Sword Fishing", 80, 750));
                _listFishingRodEN.Add(new FishingRodDetect(10, "Magic Staff", "Magic Staff", 80, 2000));
            }
        }

        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }

        public static int getIdLanguage(string textRaw)
        {
            //string[] nameClearArr = RemoveUnicode(textRaw).Split(new[] { ' ' });
            //foreach(string a in nameClearArr)
            //{
            //    Debug.WriteLine(a);
            //}
            bool c1 = textRaw.Contains("ool");
            bool c2 = textRaw.Contains("Tuol");
            //int pos = Array.IndexOf(nameClearArr, "Tool");
            //int pos2 = Array.IndexOf(nameClearArr, "Tuol");
            //int pos3 = Array.IndexOf(nameClearArr, "Tooll");
            if (c1 || c2)
            {
                return 1;
            } else
            {
                return 0;
            }
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
