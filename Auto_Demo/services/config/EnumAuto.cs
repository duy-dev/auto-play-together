using Auto_Demo.services.ml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoItX3Lib;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Auto_Demo.services.config
{

    public class pointXY
    {
        public int x { get; set; }
        public int y { get; set; }

        public pointXY(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
    public class percentXY
    {
        public double x { get; set; }
        public double y { get; set; }

        public percentXY(double _x, double _y)
        {
            x = _x;
            y = _y;
        }
    }
    public class countColor
    {
        public Color color { get; set; }
        public int count { get; set; }

        public countColor(Color _color, int _count)
        {
            color = _color;
            count = _count;
        }
    }

    class EnumAuto
    {
        //-------- Lib --------//

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);
        public static AutoItX3 autoit = new AutoItX3();

        //-------- Point --------//

        // toạ độ nút đóng thông tin cần câu
        public static percentXY point_closeInfoFishingRod = new percentXY(70.52, 16.85);

        // toạ độ balo
        public static percentXY point_Bag = new percentXY(95.66, 53.4);
        // toạ độ tab cần câu
        public static percentXY point_TabRod = new percentXY(79.58, 5.74);
        // toạ độ tab phương tiện
        public static percentXY point_TabVehicle = new percentXY(57.08, 6.11);
        // toạ độ đóng balo
        public static percentXY point_CloseBag = new percentXY(96.56, 6.48);
        // toạ độ nút bảo quản cá
        public static percentXY point_ButtonStore = new percentXY(77.78, 82.1);

        // toạ độ kiểm tra nút space
        public static percentXY point_ButtonSpace1 = new percentXY(87.85, 82.1);
        public static percentXY point_ButtonSpace2 = new percentXY(86.28, 78.09);
        public static percentXY point_ButtonSpace3 = new percentXY(89.76, 78.9);

        // toạ độ nút sửa cần + sửa cần ok
        public static percentXY point_ButtonFixRod = new percentXY(55.55, 75);

        // toạ độ nút quăng câu
        public static percentXY point_ButtonTossSentence = new percentXY(79.69, 60.8);

        // toạ độ nút space
        public static percentXY point_ButtonSpace = new percentXY(87.85, 78.7);

        // toạ độ check mở xong popup thông tin cần câu
        public static percentXY point_PopupInfoRodShow = new percentXY(28.65, 63.89);

        // toạ độ check có hiện ba lô hay không, dùng để check xem có đang câu cá hay ko
        public static percentXY point_IsBagDisplay = new percentXY(94.9, 51.3);

        // R1/R2
        public static double R1 = 5.21;
        public static double R2 = 12;
        public static double minSizeFish = 4;

        // toạ độ check level cá
        public static percentXY point_LevelFish = new percentXY(80.31, 41.48);

        // toạ độ check level cá
        public static percentXY point_checkVM = new percentXY(79.58, 26.11);


        //-------- Area --------//

        // ô cần câu 1
        public static percentXY area_Rod1_TL = new percentXY(63.72, 27.47);
        public static percentXY area_Rod1_BR = new percentXY(65.45, 30.56);
        // ô cần câu 2
        public static percentXY area_Rod2_TL = new percentXY(79.17, 27.47);
        public static percentXY area_Rod2_BR = new percentXY(80.9, 30.56);
        // ô cần câu 3
        public static percentXY area_Rod3_TL = new percentXY(94.73, 27.47);
        public static percentXY area_Rod3_BR = new percentXY(96.40, 30.56);
        // ô cần câu 4
        public static percentXY area_Rod4_TL = new percentXY(63.72, 62.35);
        public static percentXY area_Rod4_BR = new percentXY(65.45, 65.44);
        // ô cần câu 5
        public static percentXY area_Rod5_TL = new percentXY(79.17, 62.35);
        public static percentXY area_Rod5_BR = new percentXY(80.9, 65.44);
        // ô cần câu 6
        public static percentXY area_Rod6_TL = new percentXY(94.73, 62.35);
        public static percentXY area_Rod6_BR = new percentXY(96.40, 65.44);

        // danh sách toạ độ các ô cần câu (1-6)
        public static List<List<percentXY>> list_FishingRod = new List<List<percentXY>>();

        // khu vực tên cần câu
        public static percentXY area_NameRod_TL = new percentXY(32.99, 13.27);
        public static percentXY area_NameRod_BR = new percentXY(65.97, 22.53);
        public static List<percentXY> area_NameFishingRod = new List<percentXY> { area_NameRod_TL, area_NameRod_BR };

        // khu vực kiểm tra ngôn ngữ
        public static percentXY area_LanguageCheck_TL = new percentXY(76.56, 9.26);
        public static percentXY area_LanguageCheck_BR = new percentXY(82.8, 12.5);
        public static List<percentXY> area_LanguageCheck = new List<percentXY> { area_LanguageCheck_TL, area_LanguageCheck_BR };

        // khu vực kiểm tra tên cá
        public static percentXY area_FishName_TL = new percentXY(65.73, 6.67);
        public static percentXY area_FishName_BR = new percentXY(95.1, 17.04);
        public static List<percentXY> area_FishName = new List<percentXY> { area_FishName_TL, area_FishName_BR };

        // khu vực kiểm tra giá cá
        public static percentXY area_FishPrice_TL = new percentXY(79.27, 45);
        public static percentXY area_FishPrice_BR = new percentXY(86.04, 50.77);
        public static List<percentXY> area_FishPrice = new List<percentXY> { area_FishPrice_TL, area_FishPrice_BR };

        //-------- List --------//

        // list màu bỏ qua (ko check)
        public static List<string> list_ColorNoCheckFish = new List<string>();
        public static List<Color> list_ColorColorFish = new List<Color>();
        public static List<Color> list_ColorXoayNuoc = new List<Color>();
        public static List<Color> list_ColorPhao = new List<Color>();
        public static List<Color> list_ColorFishCanCau = new List<Color>();
        public static Color color_NoCheck = ColorTranslator.FromHtml("#F5F5F5");
        public static List<Color> list_ColorFishNgay = new List<Color>();
        public static List<Color> list_ColorFishDem = new List<Color>();
        public static List<Color> list_ColorFishChuyenNgay = new List<Color>();
        public static List<Color> list_ColorFishChuyenDem = new List<Color>();

        // thời tiết
        public static List<percentXY> area_CheckWeather = new List<percentXY>();

        public static void initListFishingRod()
        {

            // List ô cần câu

            List<percentXY> o1 = new List<percentXY>();
            o1.Add(area_Rod1_TL);
            o1.Add(area_Rod1_BR);
            list_FishingRod.Add(o1);
            List<percentXY> o2 = new List<percentXY>();
            o2.Add(area_Rod2_TL);
            o2.Add(area_Rod2_BR);
            list_FishingRod.Add(o2);
            List<percentXY> o3 = new List<percentXY>();
            o3.Add(area_Rod3_TL);
            o3.Add(area_Rod3_BR);
            list_FishingRod.Add(o3);
            List<percentXY> o4 = new List<percentXY>();
            o4.Add(area_Rod4_TL);
            o4.Add(area_Rod4_BR);
            list_FishingRod.Add(o4);
            List<percentXY> o5 = new List<percentXY>();
            o5.Add(area_Rod5_TL);
            o5.Add(area_Rod5_BR);
            list_FishingRod.Add(o5);
            List<percentXY> o6 = new List<percentXY>();
            o6.Add(area_Rod6_TL);
            o6.Add(area_Rod6_BR);
            list_FishingRod.Add(o6);

            // list màu bỏ qua

            //list_ColorNoCheckFish.Add("#00618C");
            //list_ColorNoCheckFish.Add("#0E183B");
            //list_ColorNoCheckFish.Add("#0E2243");
            //list_ColorNoCheckFish.Add("#07466B");
            //list_ColorNoCheckFish.Add("#312F4F");
            //list_ColorNoCheckFish.Add("#2F5B7F");
            //list_ColorNoCheckFish.Add("#69546F");
            //list_ColorNoCheckFish.Add("#0E2646");
            //list_ColorNoCheckFish.Add("#262849");
            //list_ColorNoCheckFish.Add("#C38262");
            //list_ColorNoCheckFish.Add("#2C2D52");
            //list_ColorNoCheckFish.Add("#FFFFFF");
            //list_ColorNoCheckFish.Add("#2F3E5A");
            //list_ColorNoCheckFish.Add("#64A6BE");
            //list_ColorNoCheckFish.Add("#CD959C");
            //list_ColorNoCheckFish.Add("#C48E97");
            //list_ColorNoCheckFish.Add("#5997B0");
            //list_ColorNoCheckFish.Add("#372113");
            //list_ColorNoCheckFish.Add("#5B9FB8");
            //list_ColorNoCheckFish.Add("#C097A0");
            //list_ColorNoCheckFish.Add("#72C9D9");

            // màu helloween

            // ngày
            list_ColorNoCheckFish.Add("#97BBB8");
            list_ColorNoCheckFish.Add("#6A7875");

            // chuyển
            list_ColorNoCheckFish.Add("#A0AAA2");
            list_ColorNoCheckFish.Add("#A3A399");
            list_ColorNoCheckFish.Add("#A89B8E");
            list_ColorNoCheckFish.Add("#B08D7C");
            list_ColorNoCheckFish.Add("#B6806B");
            list_ColorNoCheckFish.Add("#BD755C");
            list_ColorNoCheckFish.Add("#BF7158");

            // chiều
            list_ColorNoCheckFish.Add("#BF6B51");
            list_ColorNoCheckFish.Add("#BF7158");

            // chuyển
            list_ColorNoCheckFish.Add("#B2654E");
            list_ColorNoCheckFish.Add("#8D5245");
            list_ColorNoCheckFish.Add("#75463F");
            list_ColorNoCheckFish.Add("#633C3B");
            list_ColorNoCheckFish.Add("#4B3135");
            list_ColorNoCheckFish.Add("#3F2A32");
            list_ColorNoCheckFish.Add("#382631");

            // tối
            list_ColorNoCheckFish.Add("#2B202E");
            list_ColorNoCheckFish.Add("#2A202D");
            list_ColorNoCheckFish.Add("#181322");

            // chuyển
            list_ColorNoCheckFish.Add("#2B1928");
            list_ColorNoCheckFish.Add("#331C2A");
            list_ColorNoCheckFish.Add("#3D202D");
            list_ColorNoCheckFish.Add("#45232F");
            list_ColorNoCheckFish.Add("#4E2632");
            list_ColorNoCheckFish.Add("#612D38");
            list_ColorNoCheckFish.Add("#77343E");
            list_ColorNoCheckFish.Add("#874850");
            list_ColorNoCheckFish.Add("#8E7B7E");
            list_ColorNoCheckFish.Add("#909091");


            // màu action
            list_ColorNoCheckFish.Add("#CACBD0");

            // bóng mờ cá
            list_ColorNoCheckFish.Add("#292A4F");
            list_ColorNoCheckFish.Add("#2C3454");
            list_ColorNoCheckFish.Add("#5B9FB8");

            // màu nền mờ
            list_ColorNoCheckFish.Add("#FEFEFD");
            list_ColorNoCheckFish.Add("#E9EFDB");

            //list_ColorColorFish.Add(ColorTranslator.FromHtml("#00618C"));
            //list_ColorColorFish.Add(ColorTranslator.FromHtml("#0E2243"));
            //list_ColorColorFish.Add(ColorTranslator.FromHtml("#07466B"));
            //list_ColorColorFish.Add(ColorTranslator.FromHtml("#0E2646"));
            //list_ColorColorFish.Add(ColorTranslator.FromHtml("#0E183B"));
            //list_ColorColorFish.Add(ColorTranslator.FromHtml("#312F4F"));
            //list_ColorColorFish.Add(ColorTranslator.FromHtml("#69546F"));
            //list_ColorColorFish.Add(ColorTranslator.FromHtml("#2F5B7F"));
            //list_ColorColorFish.Add(ColorTranslator.FromHtml("#262849"));

            // ngày
            list_ColorFishNgay.Add(ColorTranslator.FromHtml("#3C7E8C"));
            list_ColorFishNgay.Add(ColorTranslator.FromHtml("#396771"));
            list_ColorFishNgay.Add(ColorTranslator.FromHtml("#313334"));
            list_ColorFishNgay.Add(ColorTranslator.FromHtml("#36545B"));
            list_ColorFishNgay.Add(ColorTranslator.FromHtml("#3B7581"));

            // tối
            list_ColorFishDem.Add(ColorTranslator.FromHtml("#010214"));
            list_ColorFishDem.Add(ColorTranslator.FromHtml("#050615"));

            list_ColorFishDem.Add(ColorTranslator.FromHtml("#0F0F17"));


            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#0B0618"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#4A6F76"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#5B5D5D"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#6C4C44"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#6D3B31"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#423E3D"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#44283B"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#492927"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#383340"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#28171E"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#0A0716"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#3F596A"));

            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#39182A"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#401C2D"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#3D4957"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#30242D"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#190C1D"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#070617"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#211521"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#2F1E27"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#313B43"));

            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#19111E"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#221622"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#2D1C27"));
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#302B34"));

            //mưa
            list_ColorFishChuyenNgay.Add(ColorTranslator.FromHtml("#100F1B"));

            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#47727B"));
            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#5C5D5C"));
            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#11081A"));
            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#1E0D1F"));
            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#2C1225"));
            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#39162A"));
            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#433245"));
            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#405364"));
            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#405364"));
            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#63362E"));
            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#694F48"));

            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#392826"));
            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#405465"));
            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#3D7786"));

            list_ColorFishChuyenDem.Add(ColorTranslator.FromHtml("#3F4344"));

            // ngày
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#3C7E8C"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#396771"));

            // tối
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#010214"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#050615"));

            // chuyển
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#11081A"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#1E0D1F"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#2C1225"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#39162A"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#433245"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#405364"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#405364"));


            list_ColorColorFish.Add(ColorTranslator.FromHtml("#4A6F76"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#5B5D5D"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#6C4C44"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#6D3B31"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#423E3D"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#492927"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#383340"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#28171E"));
            list_ColorColorFish.Add(ColorTranslator.FromHtml("#0A0716"));

            list_ColorColorFish.Add(ColorTranslator.FromHtml("#222224"));


            // xoáy nước
            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#425979"));

            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#73CBE6"));
            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#73D4EE"));
            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#73D3F3"));
            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#72BCDA"));
            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#7CA3D5"));

            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#DBABB2"));
            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#D19EA7"));
            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#DCC7D1"));

            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#35365B"));
            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#464B72"));
            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#92819D"));
            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#34D1FF"));
            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#254688"));


            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#65ACD4"));
            //list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#B0C9E2"));

            // ngày
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#ACD3D1"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#9BC0BD"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#B1F1F2"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#AED8D8"));

            // chuyển
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#BCDEDD"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#B6C1BD"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#B0A99D"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#C5C6BB"));

            // chiều
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#D0BDAE"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#D7AF9D"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#DE9983"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#CD836C"));

            // chuyển
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#9F7875"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#835A57"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#796469"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#573C41"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#6E5B64"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#463B47"));

            // tối
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#312F3F"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#3D4258"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#534957"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#201E2F"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#354158"));

            // chuyển
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#4E495C"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#3F394D"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#392634"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#5C4D63"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#674755"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#774E5D"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#844955"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#B16E7A"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#9C6671"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#9F91A0"));
            list_ColorXoayNuoc.Add(ColorTranslator.FromHtml("#998388"));

            // màu phao câu
            list_ColorPhao.Add(ColorTranslator.FromHtml("#63331E"));
            list_ColorPhao.Add(ColorTranslator.FromHtml("#793E23"));
            list_ColorPhao.Add(ColorTranslator.FromHtml("#29170F"));
            list_ColorPhao.Add(ColorTranslator.FromHtml("#2E180F"));
            list_ColorPhao.Add(ColorTranslator.FromHtml("#331910"));
            list_ColorPhao.Add(ColorTranslator.FromHtml("#3B1E13"));
            list_ColorPhao.Add(ColorTranslator.FromHtml("#472517"));


            // 8h - 18h
            list_ColorFishCanCau.Add(ColorTranslator.FromHtml("#36545B"));

            // 19h - 2xh
            list_ColorFishCanCau.Add(ColorTranslator.FromHtml("#010214"));

            // 2h - 4h
            list_ColorFishCanCau.Add(ColorTranslator.FromHtml("#0F0F17"));

            //miaw
            list_ColorFishCanCau.Add(ColorTranslator.FromHtml("#1B1B1B"));


            // thời tiết
            area_CheckWeather.Add(new percentXY(20, 25));
            area_CheckWeather.Add(new percentXY(50, 25));
            area_CheckWeather.Add(new percentXY(80, 25));

            area_CheckWeather.Add(new percentXY(20, 50));
            area_CheckWeather.Add(new percentXY(80, 50));

            area_CheckWeather.Add(new percentXY(20, 75));
            area_CheckWeather.Add(new percentXY(50, 75));
            area_CheckWeather.Add(new percentXY(80, 75));
        }
    }
}
