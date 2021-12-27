using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using AutoItX3Lib;

using System.Runtime.InteropServices;
using System.Drawing;
using KAutoHelper;
using System.Diagnostics;
using Tesseract;
using Auto_Demo.services.ml;
using Auto_Demo.services.config;
using System.Drawing.Imaging;
using Auto_Demo.draw_window;
using Point = System.Drawing.Point;
using System.IO;

namespace Auto_Demo.services
{

    public class Auto
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

        public static AutoItX3 autoit = new AutoItX3();
        public static MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public static ObservableCollection<Auto> _ListAuto = new ObservableCollection<Auto>();

        public string auto_id { get; set; }
        public string emulator_type { get; set; }
        public bool isAuto { get; set; }
        public string nameAuto { get; set; }


        public int acceptError { get; set; }
        public bool isGetAcceptError { get; set; }
        public int maxPercentAcceptError { get; set; }
        public int totalAcceptError { get; set; }


        public bool check_BuoysAreaDone { get; set; }
        public bool skipFish { get; set; }
        public int shadowSize { get; set; }
        public bool isPullRod { get; set; }

        public int currentSizeShadowFish { get; set; }

        public string messageNotiStep { get; set; }
        public int typeNotiStep { get; set; }
        public string nameFishNotiStep { get; set; }
        public string colorLevelFishNotiStep { get; set; }
        public int priceFishNotiStep { get; set; }
        public int sizeFishNotiStep { get; set; }
        public bool isVMFishNotiStep { get; set; }


        //public string classHandle { get; set; }
        //public string textHandle { get; set; }

        public int handleWidth { get; set; }
        public int handleHeight { get; set; }
        public int idRodUsing { get; set; }

        public List<pointXY> detect_ExclamationArea { get; set; }
        public List<pointXY> detect_SensorArea1 { get; set; }
        public List<pointXY> detect_SensorArea2 { get; set; }
        public List<pointXY> detect_NameArea { get; set; }
        public List<pointXY> detect_NameNoFishingArea { get; set; }
        public List<pointXY> detect_BuoysArea { get; set; }
        public List<pointXY> detect_CheckSizeArea { get; set; }
        public List<pointXY> detect_KVBanDau { get; set; }
       // public List<pointXY> detect_trucDoc { get; set; }

        public List<string> colorDefault_ExclamationArea { get; set; }
        public List<string> colorDefault_SensorArea1 { get; set; }
        public List<string> colorDefault_SensorArea2 { get; set; }
        public bool useSensor { get; set; }

        public List<FishingRodDetect> siteMapFishingRod { get; set; }
        public FishingRodDetect currentFishingRod { get; set; }
        public int totalFishing { get; set; }

        public Bitmap lastImgCheck { get; set; }
        public Bitmap OKImgCheck { get; set; }
        public double firstDifference { get; set; }

        public Image img_checkPhao { get; set; }
        public Bitmap img_getAreaBongCa { get; set; }
        public int countCheckDutDay { get; set; }

        public SummaryTotal summaryTotal { get; set; }
        public SummaryTotalTime totalTime { get; set; }
        public List<SummaryFish> summaryFish { get; set; }
        public SummaryFishSort summaryFishSort { get; set; }
        public int totalFailed { get; set; }

        public int hashCheck { get; set; }
        public int hashCheckDoc { get; set; }
        public int maxCheck { get; set; }


        public int nguong_ChenhLechAll { get; set; }
        public int nguong_ChenhLechDoc { get; set; }
        public List<Color> list_ColorCheckAfter { get; set; }
        public int soLanCheckChamThan { get; set; }
        public int countMauKhongDoi { get; set; }

        public List<pointXY> kv_BoQuaCheckBong { get; set; }

        //thời tiết
        public string weather { get; set; }
        public List<pointXY> list_PointWeather { get; set; }

        //lấy toạ độ
        public int statusGetPosition { get; set; }
        public bool isGetPosition { get; set; }
        public List<SquareBorder> _ListDraw { get; set; }
        public List<RoundBorder> _ListRounded { get; set; }
        public List<FishSizeBorder> _ListDrawSize { get; set; }
        public int totalShowDraw { get; set; }




        //public string title = "(MEmu)";
        public IntPtr handle { get; set; }

        public Auto(string _auto_id, string _emulator_type, string _nameAuto, bool isNew = false)
        {
            auto_id = _auto_id;
            emulator_type = _emulator_type;
            isAuto = false;
            nameAuto = _nameAuto;
            messageNotiStep = "Chưa auto...";
            typeNotiStep = 0;
            summaryTotal = new SummaryTotal(0, 0, 0, 0, 0, 0, 0, 0);
            summaryFishSort = new SummaryFishSort();

            //classHandle = _classHandle;
            //textHandle = _textHandle;

            _ListAuto.Add(this);
            initSummaryTotalDefault();
            initSummaryFish();

            if (isNew)
            {
                wnd.updateAuto(auto_id);
            }
        }

        public IntPtr gethWnd(string name)
        {
            IntPtr hWnd = IntPtr.Zero;
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle == name)
                {
                    hWnd = pList.MainWindowHandle;
                }
            }
            return hWnd;
        }

        public void initSummaryTotalDefault()
        {
            totalTime = new SummaryTotalTime();
            summaryTotal = new SummaryTotal(0, 0, 0, 0, 0, 0, 0, 0);
            App.Current.Dispatcher.Invoke(() =>
            {
                
            });
        }

        public void initSummaryFish()
        {
            summaryFish = new List<SummaryFish>();
        }

        public void initHandle(bool isGetPositionCheck = false)
        {
            ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
            isGetPosition = isGetPositionCheck;
            isAuto = true;
            Debug.WriteLine("save_exclamation: " + configAuto.save_exclamation);
            if (configAuto.save_exclamation != 1 || detect_ExclamationArea == null)
            {
                detect_ExclamationArea = new List<pointXY>();
                detect_SensorArea1 = new List<pointXY>();
                detect_SensorArea2 = new List<pointXY>();
                useSensor = true;
                list_ColorCheckAfter = new List<Color>();
                colorDefault_ExclamationArea = new List<string>();
                colorDefault_SensorArea1 = new List<string>();
                colorDefault_SensorArea1 = new List<string>();
                OKImgCheck = null;
            }
            if (configAuto.save_exclamation != 1 || detect_CheckSizeArea == null)
            {
                detect_CheckSizeArea = new List<pointXY>();
                detect_BuoysArea = new List<pointXY>();
                detect_KVBanDau = new List<pointXY>();
                check_BuoysAreaDone = false;
                kv_BoQuaCheckBong = new List<pointXY> { new pointXY(0, 0), new pointXY(0, 0) };
            }

            detect_NameArea = new List<pointXY>();
            detect_NameNoFishingArea = new List<pointXY>();
            totalShowDraw = 0;

            if(list_PointWeather == null)
            {
                list_PointWeather = new List<pointXY>();
            }

            if (siteMapFishingRod == null)
            {
                siteMapFishingRod = new List<FishingRodDetect>();
            }

            if (_ListDraw == null)
            {
                _ListDraw = new List<SquareBorder>();
            }

            if (_ListRounded == null)
            {
                _ListRounded = new List<RoundBorder>();
            }

            if(_ListDrawSize == null)
            {
                _ListDrawSize = new List<FishSizeBorder>();
            }

            currentFishingRod = null;
            weather = "default";

            if (emulator_type == "memu" || emulator_type == "nox" || emulator_type == "ldplayer")
            {
                IntPtr win = AutoControl.FindWindowHandle(null, nameAuto);
                Debug.WriteLine("nameAuto" + win);
                handle = AutoControl.FindHandle(gethWnd(nameAuto), "subWin", null);
                Debug.WriteLine("handle" + handle);
                Image handleGetSize = CaptureHelper.CaptureWindow(handle);
                //handleGetSize.Save($"data/a.png");
                handleWidth = handleGetSize.Width;
                handleHeight = handleGetSize.Height;
            } else if (emulator_type == "bluestacks")
            {
                IntPtr win = AutoControl.FindWindowHandle(null, nameAuto);
                handle = AutoControl.FindHandle(win, "BlueStacksApp", null);
                Image handleGetSize = CaptureHelper.CaptureWindow(handle);
                handleWidth = handleGetSize.Width;
                handleHeight = handleGetSize.Height;
            }

            if (!isGetPosition)
            {
                loadTime();
                totalFishing = 0;
                shadowSize = 0;
                currentSizeShadowFish = 0;

                isGetAcceptError = false;
                maxPercentAcceptError = 0;
                totalAcceptError = 0;
                acceptError = 82;
                totalFailed = 0;
            }
        }

        public Task newTaskAuto()
        {
            Task auto = new Task(() =>
            {
                Delay(200);
                App.Current.Dispatcher.Invoke(() =>
                {
                    messageNotiStep = "Bắt đầu auto...";
                    wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                });



                Debug.WriteLine("aaaa");
                checkNotFishing();
                initHandle();
                //Delay(1000);

                openBagRod();
                FishingRodConfig configRod = FishingRodConfig._ListFishingRodConfig.SingleOrDefault(r => r.auto_id == auto_id);
                if (siteMapFishingRod.Count == 0 || configRod.get_info_rod)
                {
                    siteMapFishingRod = new List<FishingRodDetect>();
                    getSiteMapFishingRod();
                }
                else
                {
                    selectFishingRod(getIdRod());
                }
                while (!checkDisplayBag()) {
                    Delay(200);
                    if (!isAuto) { break; }
                }

                
                while (true)
                {
                    skipFish = false;
                    isPullRod = false;
                    if (!isAuto) { return; }
                    //App.Current.Dispatcher.Invoke(() =>
                    //{});
                    

                    // Lấy toạ độ tên khi không câu
                    getAreaNameNoFishing();
                    while (!checkDisplayBag()) {
                        Delay(50);
                        if (!isAuto) { break; }
                    }
                    ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
                    Delay((100 - configAuto.speed) * 40);
                    controlClickPercentXY(EnumAuto.point_ButtonTossSentence);
                    Delay(3000);
                    //getPhaoCau();
                    if (!isAuto) { return; }
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        if (detect_NameArea.Count != 2 && detect_ExclamationArea.Count == 0)
                        {
                            messageNotiStep = "Quăng câu... chờ lấy toạ độ dấu chấm than, phao câu";
                        }
                        else
                        {
                            messageNotiStep = "Quăng câu... chờ bóng cá";
                        }
                        wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                    });
                    // Lấy toạ độ tên khi đang câu
                    getAreaName();
                    if (!isAuto) { return; }

                    // Kiểm tra sửa cần
                    if (checkDetectAreaWithColor(detect_NameNoFishingArea, "#FFFFFF", 0.5))
                    {
                        fixFishing();
                    } else
                    {
                        ConfigFishSize configSize = ConfigFishSize._ListConfigFishSize.SingleOrDefault(r => r.auto_id == auto_id);
                        int count = configSize.listSize.Where(x => x.isChecked == true).ToList().Count;
                        //Debug.WriteLine("bỏ bóng: " + count + configSize.mode_setting);
                        if (configSize.mode_setting == "auto" || count > 0)
                        {
                            filterShadowFish();
                        }
                        
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            wnd.setSummaryTotal(auto_id, ++summaryTotal.quangCan, 1);
                        });
                        fishing();
                        Debug.WriteLine("out");
                        if (!skipFish)
                        {
                            checkCanCau();
                        }
                        Debug.WriteLine("out 2");
                    }
                }
            });
            return auto;
        }

        public void getAreaNameNoFishing()
        {
            if (detect_NameNoFishingArea.Count != 2)
            {
                List<pointXY> areaRes = new List<pointXY>();
                do
                {
                    Bitmap img_handleCheck = (Bitmap)CaptureHelper.CaptureWindow(handle);
                    areaRes = getColorRelativeArea(img_handleCheck, "#00FF00", 15);
                    //drawArea(areaRes, nameAuto);
                    Delay(400);
                    if (!isAuto) { break; }

                } while (areaRes.Count != 2);
                double maxSizeArea = (double)(handleHeight * handleWidth) / (double)100 * (double)5;
                if ((double)getTotalPixelArea(areaRes) < maxSizeArea)
                {
                    detect_NameNoFishingArea = areaRes;
                }
            }
        }

        public void getAreaName()
        {
            if (detect_NameArea.Count != 2 && detect_NameNoFishingArea.Count > 0)
            {
                List<pointXY> areaRes = new List<pointXY>();
                int countGetArea = 0;
                int widthNameNoFish = detect_NameNoFishingArea[1].x - detect_NameNoFishingArea[0].x;
                int widthName = 0;
                do
                {
                    Bitmap img_handleCheck = (Bitmap)CaptureHelper.CaptureWindow(handle);
                    areaRes = getColorRelativeArea(img_handleCheck, "#00FF00", 15);
                    if (areaRes.Count > 0)
                    {
                       
                        widthName = areaRes[1].x - areaRes[0].x;
                    } else
                    {
                        stopAuto();
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            new Notification() { id = Guid.NewGuid().ToString(), title = "Lỗi cấu hình toạ độ cảm biến", content = $"Hệ thống không thể lấy được toạ độ, vui lòng cấu hình thủ công hoặc chạy lại auto", type = "error", timeStrat = DateTime.Now };
                        });
                    }
                    countGetArea++;
                    Delay(400);
                    if (!isAuto) { break; }
                } while ((areaRes.Count != 2 || countGetArea > 20) && widthName < widthNameNoFish);

                // nếu quá 20 mà ko lấy đc khu vực tên thì sẽ dừng auto và báo lỗi
                if (countGetArea > 20)
                {
                    stopAuto();
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        new Notification() { id = Guid.NewGuid().ToString(), title = "Lỗi cấu hình toạ độ cảm biến", content = $"Hệ thống không thể lấy được toạ độ, vui lòng cấu hình thủ công hoặc chạy lại auto", type = "error", timeStrat = DateTime.Now };
                    });
                }
                else
                {
                    double maxSizeAreaName = (double)(handleHeight * handleWidth) / (double)100 * (double)5;
                    if ((double)getTotalPixelArea(areaRes) < maxSizeAreaName)
                    {
                        //drawArea(areaRes, nameAuto);
                        detect_NameArea = areaRes;
                    }
                }
            }
        }

        public void fishing()
        {
            if (!isAuto) { return; }
            Debug.WriteLine("1: " + useSensor);
            if (useSensor)
            {
                if(detect_SensorArea1.Count == 0 && detect_SensorArea2.Count == 0)
                {
                    setDetectSensorArea(detect_NameArea[0].x, detect_NameArea[0].y, detect_NameArea[1].x, detect_NameArea[1].y);
                }
                //drawArea(detect_SensorArea1, nameAuto);
                //drawArea(detect_SensorArea2, nameAuto);
                drawArea(detect_KVBanDau, nameAuto, "#FFFFFF");
                //drawArea(detect_trucDoc, nameAuto, "#FFFFFF");
                //colorDefault_SensorArea1 = getAllColorArea(detect_SensorArea1);
                //colorDefault_SensorArea2 = getAllColorArea(detect_SensorArea2);
                bool check = false;

                // Lấy ảnh check phao câu
                img_checkPhao = (CaptureHelper.CaptureWindow(handle));
                Delay(8000);
                hashCheck = 0;
                hashCheckDoc = 0;
                maxCheck = 0;
                soLanCheckChamThan = 0;
                countMauKhongDoi = 0;
                //ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
                while (true)
                {
                    //checkDetectSensorArea()
                    if (!isAuto || skipFish) { break; }
                    check = testND();
                    if (check || checkDisplayBag())
                    {
                        controlClickPercentXY(EnumAuto.point_ButtonSpace);
                        isPullRod = true;
                        checkDutDay();
                        break;
                    }
                    //GC.Collect();
                    Thread.Sleep(50);
                }
            }
            else
            {
                //checkWeather();
                lastImgCheck = getImageByListPixelXY(detect_ExclamationArea);
                //firstDifference = 0;
                colorDefault_ExclamationArea = getAllColorArea(detect_ExclamationArea);
                //drawArea(detect_ExclamationArea, nameAuto);
                bool check = false;
                ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
                Delay((configAuto.delay_fishing_test - 3) * 1000);
                Debug.WriteLine("CHECK...");
                if (!isAuto) { return; }

                while (true)
                {
                    if (!isAuto || skipFish) { return; }
                    check = checkD();
                    if (check || checkDisplayBag())
                    {
                        if (check)
                        {
                            controlClickPercentXY(EnumAuto.point_ButtonSpace);
                            isPullRod = true;
                            checkDutDay();
                            //getCoordinatesBuoys();
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                messageNotiStep = "Dựt cần...";
                                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                            });
                        }
                        break;
                    }
                    Thread.Sleep(configAuto.delay_check);
                }
            }
        }

        public void checkCanCau()
        {
            if (!isAuto) { return; }
            while (!checkDoneFishing() && !checkDisplayBag()) {
                Delay(200);
                if (!isAuto) { return; }
            }
            if (checkDoneFishing())
            {
                actionAfterFishing();
                updateSlotCurrentRod();
            } else
            {
                if (countCheckDutDay >= 3)
                {
                    //updateSlotCurrentRod();
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        wnd.setSummaryTotal(auto_id, ++summaryTotal.dutDay, 3);
                    });
                } else
                {
                    totalFailed++;
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        wnd.setSummaryTotal(auto_id, ++summaryTotal.cauLoi, 4);
                    });
                    if(totalFailed > 2)
                    {
                        isAuto = false;
                        newTaskAuto().Start();
                    }
                }
                Delay(1000);
            }
            if (!isAuto) { return; }

            if (getSlotCurrentRod() == 0)
            {
                fixFishing();
            }
        }

        public void getCoordinatesBuoys(Bitmap img_Check)
        {
            Task get = new Task(() =>
            {
                Debug.WriteLine("check_BuoysAreaDone: " + check_BuoysAreaDone);
                if (!check_BuoysAreaDone)
                {
                    //Bitmap img_handleCheck = (Bitmap)CaptureHelper.CaptureWindow(handle);
                    List<pointXY> detect_BuoysAreaTemp = getKVPhaoCau(img_Check);
                    Debug.WriteLine("3: " + detect_BuoysAreaTemp.Count);
                    if (checkPercentBitmapCompareScreen(detect_BuoysAreaTemp, 2))
                    {
                        Debug.WriteLine("5a: ");

                        detect_BuoysArea = detect_BuoysAreaTemp;
                        //img_checkPhao.Save($"data/img_cap/area-phao/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                        //drawArea(detect_BuoysArea, nameAuto, "#FFFFFF");
                        getLocationBuoys(img_checkPhao);
                    }
                }
            });
            get.Start();
        }

        public void stopAuto()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                messageNotiStep = "Dừng auto...";
                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
            });
            isAuto = false;
            Thread.Sleep(100);
            if (getColorPercentXYToHex(EnumAuto.point_CloseBag) == "#FFFFFF")
            {
                controlClickPercentXY(EnumAuto.point_CloseBag);
            }
            Thread.Sleep(100);
            if (getColorPercentXYToHex(EnumAuto.point_closeInfoFishingRod) == "#AD8E87")
            {
                controlClickPercentXY(EnumAuto.point_closeInfoFishingRod);
            }
            Thread.Sleep(100);
            if (getColorPercentXYToHex(EnumAuto.point_IsBagDisplay) != "#E44142")
            {
                controlClickPercentXY(EnumAuto.point_ButtonSpace);
            }
            Thread.Sleep(1000);
            if (getColorPercentXYToHex(EnumAuto.point_IsBagDisplay) != "#E44142")
            {
                controlClickPercentXY(EnumAuto.point_ButtonSpace);
            }

        }

        // ------- Func -------- //

        // ------- Func AUTO -------- //

        // Sau khi cá cắn câu
        public void actionAfterFishing()
        {
            getInfoFish();
            ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
            Delay((100 - configAuto.speed) * 40);
            controlClickPercentXY(EnumAuto.point_ButtonStore);
            totalFishing++;
            if(getStepChangeRod() != -1 && totalFishing % getStepChangeRod() == 0)
            {
                while (!checkDisplayBag()) { Delay(200); }
                changeFishingRod();
            }
            while (!checkDisplayBag()) { Delay(200); }
        }

        // mở ba balô
        public void openBagRod()
        {
            Debug.WriteLine("aaaa");
            Delay(1000);
            ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
            Delay((100 - configAuto.speed) * 40);
            controlClickPercentXY(EnumAuto.point_Bag);
            while (!checkDisplayTabVehicle())
            {
                if (!isAuto) { break; }
            }
            Delay((100 - configAuto.speed) * 40);
            controlClickPercentXY(EnumAuto.point_TabRod);
            while (!checkDisplayTabRod())
            {
                if (!isAuto) { break; }
            }
        }

        // sửa cần câu
        public void fixFishing()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                messageNotiStep = "Đang sửa cần câu...";
                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
            });
            openBagRod();
            //Delay(1000);
            //if (!isAuto) { return; }
            FishingRodDetect rod1 = siteMapFishingRod.Single(r => r.id == currentFishingRod.id);
            double new_Y = rod1.point.y + 18.2;
            percentXY checkPoint = new percentXY(rod1.point.x, new_Y);
            if (getColorPercentXYToHex(checkPoint) == "#F15E4E")
            {
                int countTimeFix = 0;

                ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
                Delay((100 - configAuto.speed) * 40);
                controlClickPercentXY(checkPoint);
                while (!checkDisplayButtonOKFixRod()) {
                    Delay(200);
                    countTimeFix++;
                    if (!isAuto || countTimeFix > 30) { break; }
                }

                Delay((100 - configAuto.speed) * 40);
                controlClickPercentXY(EnumAuto.point_ButtonFixRod);
                Delay(100);
                while (!checkDisplayButtonOKFixRod()) {
                    Delay(200);
                    countTimeFix++;
                    if (!isAuto || countTimeFix > 60) { break; }
                }

                if (countTimeFix > 60)
                {
                    reFixFishing();
                    return;
                } else
                {
                    Delay((100 - configAuto.speed) * 40);
                    controlClickPercentXY(EnumAuto.point_ButtonFixRod);
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        summaryTotal.tienSuaCan += rod1.repairCost;
                        wnd.setSummaryTotal(auto_id, summaryTotal.tienSuaCan, 8);
                    });
                    Delay(1000);
                    if (!isAuto) { return; }

                    Delay((100 - configAuto.speed) * 40);
                    controlClickPercentXY(EnumAuto.point_CloseBag);
                    siteMapFishingRod.Where(rod => rod.id == currentFishingRod.id).ToList().ForEach(s => s.remaining = s.slot);
                    Delay(2000);
                }
            }
        }

        public void reFixFishing()
        {
            Thread.Sleep(100);
            if (getColorPercentXYToHex(EnumAuto.point_closeInfoFishingRod) == "#AD8E87")
            {
                ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
                Delay((100 - configAuto.speed) * 40);
                controlClickPercentXY(EnumAuto.point_closeInfoFishingRod);
            }
            Thread.Sleep(100);
            if (getColorPercentXYToHex(EnumAuto.point_CloseBag) == "#FFFFFF")
            {
                ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
                Delay((100 - configAuto.speed) * 40);
                controlClickPercentXY(EnumAuto.point_CloseBag);
            }
        }

        // sửa cần câu trước khi chọn cần câu
        public void fixFishingFast(percentXY point_FixRod)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                messageNotiStep = "Đang sửa cần câu...";
                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
            });

            ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
            Delay((100 - configAuto.speed) * 40);
            controlClickPercentXY(point_FixRod);
            while (!checkDisplayButtonOKFixRod()) {
                Delay(100);
                if (!isAuto) { return; }
            }

            Delay((100 - configAuto.speed) * 40);
            controlClickPercentXY(EnumAuto.point_ButtonFixRod);
            while (!checkDisplayButtonOKFixRod()) {
                Delay(100);
                if (!isAuto) { return; }
            }

            Delay((100 - configAuto.speed) * 40);
            controlClickPercentXY(EnumAuto.point_ButtonFixRod);
            siteMapFishingRod.Where(rod => rod.id == currentFishingRod.id).ToList().ForEach(s => s.remaining = s.slot);
            Delay(2000);
        }

        // Đổi cần câu
        public void changeFishingRod()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                messageNotiStep = "Chuẩn bị đổi cần câu...";
                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
            });
            openBagRod();
            Delay(1000);
            selectFishingRod(getIdRod());
        }

        // ------- Func Set -------- //

        // Chọn cần câu
        public void selectFishingRod(int idRod)
        {
            if (!isAuto) { return; }

            try
            {
                currentFishingRod = siteMapFishingRod.Single(r => r.id == idRod);
                percentXY point_CheckFixRod = new percentXY(currentFishingRod.point.x, currentFishingRod.point.y);

                point_CheckFixRod.y += 18.2;
                if (getColorPercentXYToHex(point_CheckFixRod) == "#F15E4E")
                {
                    fixFishingFast(point_CheckFixRod);
                }


                App.Current.Dispatcher.Invoke(() =>
                {
                    messageNotiStep = "Chọn cần câu...";
                    wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                });
                Delay(300);
                percentXY point_Check = new percentXY(currentFishingRod.point.x, currentFishingRod.point.y);
                point_Check.x -= 5.73;
                string checkColor = getColorPercentXYToHex(point_Check);
                if (checkColor != "#82FB28")
                {
                    // Select cần câu
                    ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
                    Delay((100 - configAuto.speed) * 40);
                    controlClickPercentXY(point_Check);
                }
                else
                {
                    // Cần câu đã được chọn
                    ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
                    Delay((100 - configAuto.speed) * 40);
                    controlClickPercentXY(EnumAuto.point_CloseBag);
                }
            } catch {
                App.Current.Dispatcher.Invoke(() =>
                {
                    new Notification() { id = Guid.NewGuid().ToString(), title = "Không tìm thấy cần câu đúng với cài đặt", content = $"Đảm bảo {FishingRod._ListFishingRod[idRod - 1].label} đang nằm trong 6 ô đầu tiên của tab công cụ trong ba lô", type = "error", timeStrat = DateTime.Now };
                });
                isAuto = false;
            }
        }

        // Update độ bền cần câu sau khi câu thành công
        public void updateSlotCurrentRod()
        {
            siteMapFishingRod.Where(rod => rod.id == currentFishingRod.id).ToList().ForEach(s => s.remaining = s.remaining - 1);
        }

        // Set toạ độ cho 2 ô cảm biến ban đầu
        public void setDetectSensorArea(int xMin, int yMin, int xMax, int yMax)
        {
            int widthNameArea = xMax - xMin;
            double widthPercentSensor1 = 1.74;
            double widthPercentSensor2 = 2.43;
            double saiSo = 1;


            int widthDetectSensorArea1 = percentToPixelWidth(widthPercentSensor1);
            int widthDetectSensorArea2 = percentToPixelWidth(widthPercentSensor2);

            // Tính toán sai số cảm biến
            int middleAxis = handleWidth / 2;
            int xCenter = xMin + (widthNameArea / 2);
            int doLech = Math.Abs(middleAxis - xCenter);
            int percent25 = percentToPixelWidth(25);
            if (doLech <= percent25)
            {
                saiSo = (double)(doLech) / (double)(percent25);
            }
            int saiSoDetect = percentToPixelWidth(saiSo);
            saiSoDetect = xCenter < middleAxis ? saiSoDetect : -saiSoDetect;

            int detectSensor1_xMin = xMin + (int)((double)widthNameArea / (double)2) - (int)((double)widthDetectSensorArea1 / (double)2) - saiSoDetect;
            int detectSensor1_yMin = yMin - percentToPixelHeight(6.17);
            int detectSensor1_xMax = detectSensor1_xMin + widthDetectSensorArea1;
            int detectSensor1_yMax = detectSensor1_yMin + 3;
            detect_SensorArea1.Clear();
            detect_SensorArea1.Add(new pointXY(detectSensor1_xMin, detectSensor1_yMin));
            detect_SensorArea1.Add(new pointXY(detectSensor1_xMax, detectSensor1_yMax));

            int detectSensor2_xMin = xMin + (int)((double)widthNameArea / (double)2) - (int)((double)widthDetectSensorArea2 / (double)2) - saiSoDetect;
            //int detectSensor2_yMin = yMin - percentToPixelHeight(12.34);
            double phanTramName = (double)(detect_NameArea[1].y - detect_NameArea[0].y) / (double)handleHeight * (double)100;
            double phanTramHeightCheck = phanTramName * 7;
            int detectSensor2_yMin = yMin - percentToPixelHeight(phanTramHeightCheck);
            int detectSensor2_xMax = detectSensor2_xMin + widthDetectSensorArea2;
            int detectSensor2_yMax = detectSensor2_yMin + 3;
            detect_SensorArea2.Clear();
            detect_SensorArea2.Add(new pointXY(detectSensor2_xMin, detectSensor2_yMin));
            detect_SensorArea2.Add(new pointXY(detectSensor2_xMax, detectSensor2_yMax));

            detect_KVBanDau.Clear();
            double tiLe = (double)10 / (double)540;
            int boKCTen = (int)(tiLe * (double)handleHeight);
            Debug.WriteLine("boKCTen: " + boKCTen);
            detect_KVBanDau.Add(new pointXY(detectSensor1_xMin + 1, detectSensor2_yMin));
            detect_KVBanDau.Add(new pointXY(detectSensor1_xMax + 1, yMin - boKCTen));

            int saiSoNguong = 38;
            if (wnd.totalMinuteGame >= 480 && wnd.totalMinuteGame < 1080)
            {
                saiSoNguong = 15;
            }
            nguong_ChenhLechAll = (detect_KVBanDau[1].x - detect_KVBanDau[0].x) * (detect_KVBanDau[1].y - detect_KVBanDau[0].y) * saiSoNguong;
            nguong_ChenhLechDoc = (detect_KVBanDau[1].x - detect_KVBanDau[0].x) * (detect_KVBanDau[1].y - detect_KVBanDau[0].y) * 14;
            Debug.WriteLine("nguong_ChenhLechAll: " + nguong_ChenhLechAll);
            Debug.WriteLine("name: " + (detect_NameArea[1].y - detect_NameArea[0].y));

            //detect_trucDoc.Clear();
            //int center_TrucDoc = (int)((detectSensor1_xMax - detectSensor1_xMin) / 2) + detectSensor1_xMin + 1;
            //detect_trucDoc.Add(new pointXY(center_TrucDoc, detectSensor2_yMin));
            //detect_trucDoc.Add(new pointXY(center_TrucDoc, yMin - boKCTen));
        }

        public List<pointXY> setDetectBuoysArea(Bitmap img_checkArea)
        {
            //img_checkArea.Save($"data/img_cap/area-phao/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
            int xMax = 0;
            int xMin = 99999;
            int yMax = 0;
            int yMin = 99999;
            List<pointXY> res = new List<pointXY>();
            int x, y;
            for (x = 0; x < img_checkArea.Width; x++)
            {
                for (y = 0; y < img_checkArea.Height; y++)
                {
                    Color pixelColor = img_checkArea.GetPixel(x, y);
                    // DUY123
                    if (colorComparisonList(pixelColor, EnumAuto.list_ColorColorFish, 1))
                    {
                        xMax = x > xMax ? x : xMax;
                        xMin = x < xMin ? x : xMin;
                        yMax = y > yMax ? y : yMax;
                        yMin = y < yMin ? y : yMin;
                    }
                }
            }
            if (xMax > 0)
            {
                res.Add(new pointXY(xMin, yMin));
                res.Add(new pointXY(xMax, yMax));
            }
            try
            {
                res[0].x -= 20;
                res[0].y -= 40;
                res[1].x += 20;
                //res[1].y += 5;
                Debug.WriteLine("x1: " + res[0].x);
                Debug.WriteLine("y1: " + res[0].y);
                Debug.WriteLine("x2: " + res[1].x);
                Debug.WriteLine("y2: " + res[1].y);
                //drawArea(res, auto_id);
            } catch { }
            return res;
        }

        public bool filterColorFish(Color color)
        {
            bool check = color.GetBrightness() < 0.5;
            //Debug.WriteLine("check: " + check);
            //int gray = (int)((double)(0.2126 * color.R) + (double)(0.7152 * color.G) + (double)(0.0722 * color.R));
            //int avg = (int)((double)(color.R + color.G + color.R) / (double)3);
            //bool check_1 = color.R < 70 && color.G < 130 && color.B < 140 && color.R >= 0 && color.G >= 0 && color.B >= 10;
            //bool check_2 = gray < 100;
            //bool check_3 = Math.Abs(color.R - avg) < 25 && Math.Abs(color.G - avg) < 25 && Math.Abs(color.B - avg) < 25;
            return check;
        }

        public bool filterColorFishRain(Color color)
        {
            bool check1 = color.R < 60 && color.G < 60 && color.B < 60;
            bool check2 = Math.Abs(color.R - color.G) < 6 && Math.Abs(color.G - color.B) < 6 && Math.Abs(color.R - color.B) < 6;
            return check1 && check2;
        }

        public bool filterColorFishRain2(Color color)
        {
            bool check1 = color.R < 30 && color.G < 30 && color.B < 30;
            bool check2 = Math.Abs(color.R - color.G) < 3 && color.G < color.B;
            return check1 && check2;
        }

        public List<pointXY> getKVPhaoCau(Bitmap bitmap_Check)
        {
            int xMax = 0;
            int yMax = 0;
            int xMin = 0;
            int yMin = 0;

            List<List<pointXY>> listGomCum = new List<List<pointXY>>();
            List<Color> colorNoCheck = new List<Color>();
            int maxBoQuaColor = 0;
            pointXY startPoint = new pointXY(0, 0);
            pointXY endPoint = new pointXY(0, 0);
            int too = 0;
            for (int x = 75; x < bitmap_Check.Width - 75; x++)
            {
                for (int y = 75; y < bitmap_Check.Height - 75; y++)
                {
                    Color colorPixel = bitmap_Check.GetPixel(x, y);
                    
                    bool noCheck = colorComparisonList(colorPixel, colorNoCheck, 2);
                    bool check = false;
                    if (wnd.totalMinuteGame >= 480 && wnd.totalMinuteGame < 1080)
                    {
                        check = colorComparisonList(colorPixel, EnumAuto.list_ColorFishNgay, 5);
                        //if(!check && wnd.totalMinuteGame >= 1020 && wnd.totalMinuteGame < 1080)
                        //{
                        //    check = filterColorFishRain(colorPixel) || filterColorFishRain2(colorPixel);
                        //}
                    }
                    else if (wnd.totalMinuteGame >= 1200 || wnd.totalMinuteGame < 240)
                    {
                        check = colorComparisonList(colorPixel, EnumAuto.list_ColorFishDem, 5);
                        //if (!check && wnd.totalMinuteGame >= 1200 && wnd.totalMinuteGame < 120)
                        //{
                        //    check = filterColorFishRain(colorPixel) || filterColorFishRain2(colorPixel);
                        //}
                    }
                    else if (wnd.totalMinuteGame >= 240 && wnd.totalMinuteGame < 480)
                    {
                        check = colorComparisonList(colorPixel, EnumAuto.list_ColorFishChuyenNgay, 20) || filterColorFishRain(colorPixel) || filterColorFishRain2(colorPixel);
                    }
                    else if (wnd.totalMinuteGame >= 1080 && wnd.totalMinuteGame < 1200)
                    {
                        check = colorComparisonList(colorPixel, EnumAuto.list_ColorFishChuyenDem, 20) || filterColorFishRain(colorPixel) || filterColorFishRain2(colorPixel);
                    }
                    if (check && !noCheck)
                    {
                        //Debug.WriteLine("cx: " + HexConverter(colorPixel));
                        Color c1 = bitmap_Check.GetPixel(x, y - 60);
                        Color c2 = bitmap_Check.GetPixel(x, y + 60);
                        Color c3 = bitmap_Check.GetPixel(x - 60, y);
                        Color c4 = bitmap_Check.GetPixel(x + 60, y);
                        int cx1 = c1.R + c1.G + c1.B;
                        int cx2 = c2.R + c2.G + c2.B;
                        int cx3 = c3.R + c3.G + c3.B;
                        int cx4 = c4.R + c4.G + c4.B;
                        //Debug.WriteLine("4g: " + cx1 + " -- " + cx2 + " -- " + cx3 + " -- " + cx4 + " -- ");
                        int colorPixelNumber = colorPixel.R + colorPixel.G + colorPixel.B;

                        if (colorPixelNumber != cx1 && colorComparison(c1, c2, 10) && colorComparison(c2, c3, 10) && colorComparison(c3, c4, 10))
                        {
                            //Debug.WriteLine("cx: " + HexConverter(colorPixel));
                            //Debug.WriteLine("xy: " + x + " -- " + y);
                            if (KhoangCach(new pointXY(xMax, yMax), new pointXY(x, y)) > 80)
                            {
                                //if(listGomCum.Count > 0)
                                //{
                                //    double kcc = KhoangCach(listGomCum[listGomCum.Count - 1][0], new pointXY(listGomCum[listGomCum.Count - 1][1].x, listGomCum[listGomCum.Count - 1][0].y));
                                //    if(kcc > 3)
                                //    {
                                //        drawArea(listGomCum[listGomCum.Count - 1], nameAuto);
                                //    }
                                //}
                                
                                List<pointXY> newCum = new List<pointXY> { new pointXY(x, y), new pointXY(x, y) };
                                too = 0;
                                listGomCum.Add(newCum);
                                xMin = x;
                                yMin = y;
                                xMax = x;
                                yMax = y;
                                //Debug.WriteLine("TACHS1: " + listGomCum.Count + " -- " + xMax + " - " + yMax);
                            }
                            else
                            {
                                too++;
                                xMax = x > xMax ? x : xMax;
                                yMax = y > yMax ? y : yMax;
                                listGomCum[listGomCum.Count - 1][1].x = xMax;
                                listGomCum[listGomCum.Count - 1][1].y = yMax;
                            }
                        }
                        else
                        {
                            if (maxBoQuaColor > 20)
                            {
                                colorNoCheck.Add(colorPixel);
                                //Debug.WriteLine("colorPixel No: " + HexConverter(colorPixel));
                                maxBoQuaColor = 0;
                            }
                            else
                            {
                                maxBoQuaColor++;
                            }
                        }
                    }
                }
            }
            List<pointXY> listDraw = new List<pointXY>();
            Debug.WriteLine("TACHS: " + listGomCum.Count);
            List<pointXY> areaPhao = new List<pointXY> { new pointXY(0, 0), new pointXY(0, 0) };
            double kcCum = 0;
            foreach (List<pointXY> area in listGomCum)
            {
                double currentKCCum = KhoangCach(area[0], area[1]);
                if (currentKCCum > kcCum)
                {
                    areaPhao[0].x = area[0].x;
                    areaPhao[0].y = area[0].y;
                    areaPhao[1].x = area[1].x;
                    areaPhao[1].y = area[1].y;
                    kcCum = currentKCCum;
                }
            }
            try
            {
                areaPhao[0].x -= 20;
                areaPhao[0].y -= 40;
                areaPhao[1].x += 20;
            }
            catch { }
            //drawArea(areaPhao, nameAuto);
            return areaPhao;
        }

        //public void getLocationBuoys(Image img_check)
        //{
        //    int widthCheck = detect_BuoysArea[1].x - detect_BuoysArea[0].x;
        //    int heightCheck = detect_BuoysArea[1].y - detect_BuoysArea[0].y;
        //    Bitmap bitmap_check = MakeGrayscale3(CropImage(img_check, detect_BuoysArea[0].x, detect_BuoysArea[0].y, widthCheck, heightCheck));
        //    Color colorMax = getColorMaxInBitmap(bitmap_check);
        //    //string colorMaxHex = HexConverter(colorMax);
        //    Bitmap black_white_Img = new Bitmap(bitmap_check.Width, bitmap_check.Height);
        //    for (int x = 0; x < bitmap_check.Width; x++)
        //    {
        //        for (int y = 0; y < bitmap_check.Height; y++)
        //        {
        //            Color colorPixel = bitmap_check.GetPixel(x, y);
        //            if(colorComparison(colorPixel, colorMax, 23))
        //            {
        //                black_white_Img.SetPixel(x, y, Color.FromArgb(255, 255, 255));
        //            } else
        //            {
        //                black_white_Img.SetPixel(x, y, Color.FromArgb(0, 0, 0));
        //            }
        //        }
        //    }
        //    black_white_Img.Save($"data/img_cap/area-phao/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
        //}

        public void getLocationBuoys(Image img_check)
        {
            //(detect_BuoysArea, nameAuto);
            int widthCheck = detect_BuoysArea[1].x - detect_BuoysArea[0].x;
            int heightCheck = detect_BuoysArea[1].y - detect_BuoysArea[0].y;
            Bitmap bitmap_check = CropImage(img_check, detect_BuoysArea[0].x, detect_BuoysArea[0].y, widthCheck, heightCheck);
            List<Color> colorMax = getListColorMaxInBitmap(bitmap_check, 60);
            Debug.WriteLine("D1: " + colorMax.Count);
            Bitmap black_white_Img = new Bitmap(bitmap_check.Width, bitmap_check.Height);
            for (int x = 0; x < bitmap_check.Width; x++)
            {
                for (int y = 0; y < bitmap_check.Height; y++)
                {
                    Color colorPixel = bitmap_check.GetPixel(x, y);
                    if (colorComparisonList(colorPixel, colorMax, 2) || colorComparisonList(colorPixel, EnumAuto.list_ColorXoayNuoc, 15))
                    {
                        black_white_Img.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        black_white_Img.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                }
            }
            //bitmap_check.Save($"data/img_cap/area-phao/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
            List<List<pointXY>> gomCum = new List<List<pointXY>>();
            for (int x = 0; x < black_white_Img.Width; x++)
            {
                for (int y = 0; y < black_white_Img.Height; y++)
                {
                    Color colorPixel = black_white_Img.GetPixel(x, y);
                    if(colorPixel.R == 0)
                    {
                        if(gomCum.Count == 0)
                        {
                            List<pointXY> newCum = new List<pointXY>();
                            newCum.Add(new pointXY(x, y));
                            gomCum.Add(newCum);
                        } else
                        {
                            bool isAdd = false;
                            foreach (List<pointXY> cum in gomCum)
                            {
                                if (KhoangCach(cum[cum.Count - 1], new pointXY(x, y)) < 25)
                                {
                                    cum.Add(new pointXY(x, y));
                                    isAdd = true;
                                    break;
                                }
                            }
                            if(!isAdd)
                            {
                                List<pointXY> newCum = new List<pointXY>();
                                newCum.Add(new pointXY(x, y));
                                gomCum.Add(newCum);
                            }
                        }
                    }
                    
                }
            }
            List<pointXY> res = new List<pointXY>();
            int saiLech = 10000;
            Debug.WriteLine("gom: " + gomCum.Count);
            foreach (List<pointXY> cum in gomCum)
            {
                int saiLechCurrent = Math.Abs(100 - cum.Count);
                if (saiLechCurrent < saiLech)
                {
                    saiLech = saiLechCurrent;
                    res = cum;
                }
            }
            Debug.WriteLine(res.Count);
            if(res.Count != 0)
            {
                Debug.WriteLine("4: ");
                res = getAroundArea(res);
                // lấy tâm phao
                int buoysCenter_x = detect_BuoysArea[0].x + res[0].x + (int)((res[1].x - res[0].x) / 2);
                int buoysCenter_y = detect_BuoysArea[0].y + res[0].y + (int)((res[1].y - res[0].y) / 2);
                pointXY buoysCenter = new pointXY(buoysCenter_x, buoysCenter_y);
                check_BuoysAreaDone = true;

                // Set khu vực kiểm tra kích thước cá
                int R1 = (int)((double)handleWidth / (double)100 * (double)EnumAuto.R1);
                int R2 = (int)((double)handleWidth / (double)100 * (double)EnumAuto.R2);
                int checkSize_TL_x = buoysCenter_x - R2 < 0 ? 0 : buoysCenter_x - R2;
                int checkSize_TL_y = buoysCenter_y - R2 < 0 ? 0 : buoysCenter_y - R2;
                pointXY checkSize_TL = new pointXY(checkSize_TL_x, checkSize_TL_y);
                int checkSize_BR_x = buoysCenter_x + R2 >= handleWidth ? handleWidth : buoysCenter_x + R2;
                int checkSize_BR_y = buoysCenter_y + R2 >= handleHeight ? handleHeight : buoysCenter_y + R2;
                pointXY checkSize_BR = new pointXY(checkSize_BR_x, checkSize_BR_y);
                detect_CheckSizeArea = new List<pointXY> { checkSize_TL, checkSize_BR };
                App.Current.Dispatcher.Invoke(() =>
                {
                    messageNotiStep = "Nhận diện toạ độ phao câu thành công";
                    wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                });

                // vẽ tâm phao lên màn hình
                pointXY drawBuoys_TL = new pointXY(buoysCenter_x - 3, buoysCenter_y - 3);
                pointXY drawBuoys_BR = new pointXY(buoysCenter_x + 3, buoysCenter_y + 3);
                List<pointXY> drawBuoys = new List<pointXY> { drawBuoys_TL, drawBuoys_BR };
                drawArea(drawBuoys, nameAuto, "#FFFFFF");

                int R1Doi = R1 / 2;

                kv_BoQuaCheckBong[0].x = buoysCenter_x - R1Doi;
                kv_BoQuaCheckBong[1].x = buoysCenter_x + R1Doi;

                Debug.WriteLine("R1: " + (int)(R1 / 2));
                kv_BoQuaCheckBong[0].y = buoysCenter_y - R1Doi;
                kv_BoQuaCheckBong[1].y = buoysCenter_y + R1Doi;

                if (kv_BoQuaCheckBong.Count > 0)
                {
                    pointXY point1 = new pointXY(kv_BoQuaCheckBong[0].x - 20, kv_BoQuaCheckBong[0].y - 20);
                    pointXY point2 = new pointXY(kv_BoQuaCheckBong[1].x + 20, kv_BoQuaCheckBong[0].y - 20);
                    pointXY point3 = new pointXY(kv_BoQuaCheckBong[0].x - 20, kv_BoQuaCheckBong[1].y + 20);
                    pointXY point4 = new pointXY(kv_BoQuaCheckBong[1].x + 20, kv_BoQuaCheckBong[1].y + 20);
                    list_PointWeather = new List<pointXY> { point1, point2, point3, point4 };
                }
                //drawArea(kv_BoQuaCheckBong, nameAuto);
                //drawRoundBorder(buoysCenter, R1, R2, nameAuto);
            }
            try
            {
                using (Graphics g = Graphics.FromImage(black_white_Img))
                {
                    int width = res[1].x - res[0].x;
                    int height = res[1].y - res[0].y;
                    g.DrawRectangle(new Pen(Brushes.Red, 1), new Rectangle(res[0].x, res[0].y, width, height));
                }
            }
            catch { }
            //black_white_Img.Save($"data/img_cap/area-phao/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
            //img_check.Save($"data/img_cap/area-phao/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
        }

        public bool checkColorInFishSize(Bitmap img_Check, List<pointXY> area_Check)
        {
            bool check = false;
            for (int x = area_Check[0].x; x < area_Check[1].x; x++)
            {
                if (!isAuto || check) { break; }
                for (int y = area_Check[0].y; y < area_Check[1].y; y++)
                {
                    Color color = img_Check.GetPixel(x, y);
                    if (HexConverter(color) == "#00FF00" || HexConverter(color) == "#FFFFFF")
                    {
                        check = true;
                        break;
                    }
                }
            }
            Debug.WriteLine("check: " + check);
            return check;
        }

        public int getSizeFish()
        {
            int res = 0;
            int countCheck = 0;
            //int widthArea = detect_CheckSizeArea[1].x - detect_CheckSizeArea[0].x;
            //int heightArea = detect_CheckSizeArea[1].y - detect_CheckSizeArea[0].y;
            //int totalPixel = widthArea * heightArea;
            List<double> listColorAfter = new List<double>();
            int min = percentToPixelWidth(EnumAuto.minSizeFish);
            for (int i = 0; countCheck < 2; i++)
            {
                if (!isAuto) { return 0; }
                if (isPullRod)
                {
                    res = 0;
                    break;
                } else
                {
                    int count = 0;
                    Bitmap img_handleCheck = (Bitmap)CaptureHelper.CaptureWindow(handle);
                    Color colorWater = img_handleCheck.GetPixel(kv_BoQuaCheckBong[0].x, kv_BoQuaCheckBong[0].y);
                    //Debug.WriteLine("colorWater: " + HexConverter(colorWater));
                    //List<pointXY> areaSize = new List<pointXY> { new pointXY(0, 0), new pointXY(0, 0) };
                    //int max_X = 0;
                    //int max_Y = 0;
                    //int min_X = 99999;
                    //int min_Y = 99999;

                    List<List<pointXY>> gomCum = new List<List<pointXY>>();
                    List<int> gomCumCount = new List<int>();
                    List<Color> gomCumColor = new List<Color>();
                    for (int x = detect_CheckSizeArea[0].x; x < detect_CheckSizeArea[1].x; x++)
                    {
                        if (!isAuto) { break; }
                        for (int y = detect_CheckSizeArea[0].y; y < detect_CheckSizeArea[1].y; y++)
                        {
                            if (!isAuto) { break; }
                            
                            Color colorpixel = img_handleCheck.GetPixel(x, y);
                            bool noCheckCenter = (x < kv_BoQuaCheckBong[0].x || x > kv_BoQuaCheckBong[1].x) || (y < kv_BoQuaCheckBong[0].y || y > kv_BoQuaCheckBong[1].y);
                            if (!colorComparison(colorWater, colorpixel, 5) && noCheckCenter && colorpixel.GetBrightness() < colorWater.GetBrightness())
                            {
                                Color c1 = img_handleCheck.GetPixel(x, y - 60 >= 0 ? y - 60 : 0);
                                Color c2 = img_handleCheck.GetPixel(x, y + 60 < img_handleCheck.Height ? y + 60 : img_handleCheck.Height - 1);
                                Color c3 = img_handleCheck.GetPixel(x - 60 >= 0 ? x - 60 : 0, y);
                                Color c4 = img_handleCheck.GetPixel(x + 60 < img_handleCheck.Width ? x + 60 : img_handleCheck.Width - 1, y);
                                int cx1 = c1.R + c1.G + c1.B;
                                int cx2 = c2.R + c2.G + c2.B;
                                int cx3 = c3.R + c3.G + c3.B;
                                int cx4 = c4.R + c4.G + c4.B;
                                //bool checkAround = (cx1 == cx2) || (cx1 == cx3) || (cx1 == cx4) || (cx2 == cx3) || (cx2 == cx4) || (cx3 == cx4);
                                //bool checkAround2 = (cx1 == cx2) && (cx2 == cx3) && (cx3 == cx4);
                                bool checkAround3 = (colorComparison(c1, c2, 10)) && (colorComparison(c2, c3, 10)) && (colorComparison(c3, c4, 10));
                                bool checkAround4 = ((colorComparison(c1, c2, 10)) && (colorComparison(c2, c3, 10))) || ((colorComparison(c2, c3, 10)) && (colorComparison(c3, c4, 10))) || ((colorComparison(c3, c4, 10)) && (colorComparison(c4, c1, 10)));
                                if (wnd.totalMinuteGame >= 480 && wnd.totalMinuteGame < 1080)
                                {
                                    bool check = (colorComparisonList(colorpixel, EnumAuto.list_ColorFishNgay, 5) || checkColorFishNgay(colorpixel)) && checkAround4;
                                    if (check)
                                    {
                                        if (gomCum.Count == 0)
                                        {
                                            Debug.WriteLine("aX: " + x);
                                            Debug.WriteLine("aY: " + y);
                                            List<pointXY> newCum = new List<pointXY>();
                                            newCum.Add(new pointXY(x, y));
                                            newCum.Add(new pointXY(x, y));
                                            gomCum.Add(newCum);
                                            gomCumCount.Add(1);
                                            gomCumColor.Add(colorpixel);
                                        }
                                        else
                                        {
                                            bool isAdd = false;
                                            int index = 0;
                                            foreach (List<pointXY> cum in gomCum)
                                            {
                                                if (Math.Abs(cum[1].x - x) < 10)
                                                {
                                                    cum[0].x = x < cum[0].x ? x : cum[0].x;
                                                    cum[0].y = y < cum[0].y ? y : cum[0].y;

                                                    cum[1].x = x > cum[1].x ? x : cum[1].x;
                                                    cum[1].y = y > cum[1].y ? y : cum[1].y;

                                                    gomCumCount[index] = gomCumCount[index] + 1;
                                                    isAdd = true;
                                                    break;
                                                }
                                                index++;
                                            }
                                            if (!isAdd)
                                            {
                                                List<pointXY> newCum = new List<pointXY>();
                                                newCum.Add(new pointXY(x, y));
                                                newCum.Add(new pointXY(x, y));
                                                gomCum.Add(newCum);
                                                gomCumCount.Add(1);
                                                gomCumColor.Add(colorpixel);
                                            }
                                        }
                                        count++;
                                    }
                                } else if (wnd.totalMinuteGame >= 1200 || wnd.totalMinuteGame < 240)
                                {
                                    bool check = colorComparisonList(colorpixel, EnumAuto.list_ColorFishDem, 5) && checkAround4;
                                    if (check)
                                    {
                                        if (gomCum.Count == 0)
                                        {
                                            Debug.WriteLine("aX: " + x);
                                            Debug.WriteLine("aY: " + y);
                                            List<pointXY> newCum = new List<pointXY>();
                                            newCum.Add(new pointXY(x, y));
                                            newCum.Add(new pointXY(x, y));
                                            gomCum.Add(newCum);
                                            gomCumCount.Add(1);
                                            gomCumColor.Add(colorpixel);
                                        }
                                        else
                                        {
                                            bool isAdd = false;
                                            int index = 0;
                                            foreach (List<pointXY> cum in gomCum)
                                            {
                                                if (Math.Abs(cum[1].x - x) < 10)
                                                {
                                                    cum[0].x = x < cum[0].x ? x : cum[0].x;
                                                    cum[0].y = y < cum[0].y ? y : cum[0].y;

                                                    cum[1].x = x > cum[1].x ? x : cum[1].x;
                                                    cum[1].y = y > cum[1].y ? y : cum[1].y;

                                                    gomCumCount[index] = gomCumCount[index] + 1;
                                                    isAdd = true;
                                                    break;
                                                }
                                                index++;
                                            }
                                            if (!isAdd)
                                            {
                                                List<pointXY> newCum = new List<pointXY>();
                                                newCum.Add(new pointXY(x, y));
                                                newCum.Add(new pointXY(x, y));
                                                gomCum.Add(newCum);
                                                gomCumCount.Add(1);
                                                gomCumColor.Add(colorpixel);
                                            }
                                        }
                                        count++;
                                    }
                                } else if (wnd.totalMinuteGame >= 240 && wnd.totalMinuteGame < 480)
                                {
                                    if ((colorComparisonList(colorpixel, EnumAuto.list_ColorFishChuyenNgay, 20) || filterColorFishRain(colorpixel) || filterColorFishRain2(colorpixel)) && checkAround3)
                                    {
                                        if (gomCum.Count == 0)
                                        {
                                            Debug.WriteLine("aX: " + x);
                                            Debug.WriteLine("aY: " + y);
                                            List<pointXY> newCum = new List<pointXY>();
                                            newCum.Add(new pointXY(x, y));
                                            newCum.Add(new pointXY(x, y));
                                            gomCum.Add(newCum);
                                            gomCumCount.Add(1);
                                            gomCumColor.Add(colorpixel);
                                        }
                                        else
                                        {
                                            bool isAdd = false;
                                            int index = 0;
                                            foreach (List<pointXY> cum in gomCum)
                                            {
                                                if (Math.Abs(cum[1].x - x) < 10)
                                                {
                                                    cum[0].x = x < cum[0].x ? x : cum[0].x;
                                                    cum[0].y = y < cum[0].y ? y : cum[0].y;

                                                    cum[1].x = x > cum[1].x ? x : cum[1].x;
                                                    cum[1].y = y > cum[1].y ? y : cum[1].y;

                                                    gomCumCount[index] = gomCumCount[index] + 1;
                                                    isAdd = true;
                                                    break;
                                                }
                                                index++;
                                            }
                                            if (!isAdd)
                                            {
                                                List<pointXY> newCum = new List<pointXY>();
                                                newCum.Add(new pointXY(x, y));
                                                newCum.Add(new pointXY(x, y));
                                                gomCum.Add(newCum);
                                                gomCumCount.Add(1);
                                                gomCumColor.Add(colorpixel);
                                            }
                                        }
                                        count++;
                                    }
                                } else if (wnd.totalMinuteGame >= 1080 && wnd.totalMinuteGame < 1200)
                                {
                                    if ((colorComparisonList(colorpixel, EnumAuto.list_ColorFishChuyenDem, 20) || filterColorFishRain(colorpixel) || filterColorFishRain2(colorpixel)) && checkAround3)
                                    {
                                        if (gomCum.Count == 0)
                                        {
                                            Debug.WriteLine("aX: " + x);
                                            Debug.WriteLine("aY: " + y);
                                            List<pointXY> newCum = new List<pointXY>();
                                            newCum.Add(new pointXY(x, y));
                                            newCum.Add(new pointXY(x, y));
                                            gomCum.Add(newCum);
                                            gomCumCount.Add(1);
                                            gomCumColor.Add(colorpixel);
                                        }
                                        else
                                        {
                                            bool isAdd = false;
                                            int index = 0;
                                            foreach (List<pointXY> cum in gomCum)
                                            {
                                                if (Math.Abs(cum[1].x - x) < 10)
                                                {
                                                    cum[0].x = x < cum[0].x ? x : cum[0].x;
                                                    cum[0].y = y < cum[0].y ? y : cum[0].y;

                                                    cum[1].x = x > cum[1].x ? x : cum[1].x;
                                                    cum[1].y = y > cum[1].y ? y : cum[1].y;

                                                    gomCumCount[index] = gomCumCount[index] + 1;
                                                    isAdd = true;
                                                    break;
                                                }
                                                index++;
                                            }
                                            if (!isAdd)
                                            {
                                                List<pointXY> newCum = new List<pointXY>();
                                                newCum.Add(new pointXY(x, y));
                                                newCum.Add(new pointXY(x, y));
                                                gomCum.Add(newCum);
                                                gomCumCount.Add(1);
                                                gomCumColor.Add(colorpixel);
                                            }
                                        }
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                    if (count > min)
                    {
                        int index = 0;
                        bool findSize = false;
                        foreach (List<pointXY> area in gomCum)
                        {
                            int next = 30;
                            int nextSize_TL_X = area[0].x - next >= 0 ? area[0].x - next : 0;
                            int nextSize_TL_Y = area[0].y - next >= 0 ? area[0].y - next : 0;
                            pointXY nextSize_TL = new pointXY(nextSize_TL_X, nextSize_TL_Y);
                            int nextSize_BR_X = area[1].x + next < img_handleCheck.Width ? area[1].x + next : img_handleCheck.Width - 1;
                            int nextSize_BR_Y = area[1].y + next < img_handleCheck.Height ? area[1].y + next : img_handleCheck.Height - 1;
                            pointXY nextSize_BR = new pointXY(nextSize_BR_X, nextSize_BR_Y);
                            List<pointXY> checkNextSite = new List<pointXY> { nextSize_TL, nextSize_BR };

                            int countPixel = 0;
                            for (int x = checkNextSite[0].x; x < checkNextSite[1].x; x++)
                            {
                                if (!isAuto) { break; }
                                for (int y = checkNextSite[0].y; y < checkNextSite[1].y; y++)
                                {
                                    Color color = img_handleCheck.GetPixel(x, y);
                                    if (colorComparison(color, gomCumColor[index], 5))
                                    {
                                        countPixel++;
                                    }
                                }

                            }
                            double kc = pixelToPercentWidth((int)KhoangCach(area[0], area[1]));
                            if (!checkColorInFishSize(img_handleCheck, area) && gomCumCount[index] > min && kc > 1.4 && kc < 8)
                            {
                                res = gomCumCount[index] > res ? gomCumCount[index] : res;
                                res = countPixel > res ? countPixel : res;
                                findSize = true;
                                if (countCheck == 1)
                                {
                                    drawAreaSize(area, nameAuto, res);
                                    removeDrawSize();
                                }
                            }
                            index++;
                        }

                        if (findSize)
                        {
                            //drawArea(areaSize, nameAuto);
                            countCheck++;
                        }
                        
                    } else {

                        Debug.WriteLine("minCount: " + count);
                    }
                    Delay(100);
                }
            }
            
            return res;
        }

        public bool checkColorFishNgay(Color color)
        {
            float hue = color.GetHue();
            float brightness = color.GetBrightness();
            if (hue > 188 && hue < 193 && brightness < 42)
            {
                return true;
            } else
            {
                return false;
            }
        }

        double KhoangCach(pointXY d1, pointXY d2)
        {
            double kc;
            kc = Math.Sqrt((d1.x - d2.x) * (d1.x - d2.x) + (d1.y - d2.y) * (d1.y - d2.y));
            return kc;
        }

        public void getColor()
        {
            initHandle();
            Task get = new Task(() =>
            {
                using (StreamWriter sw = new StreamWriter("../../text/color.txt"))
                {
                    // doc va hien thi cac dong trong file cho toi
                    // khi tien toi cuoi file. 
                    while (isAuto)
                    {
                        Bitmap img_handleCheck = (Bitmap)CaptureHelper.CaptureWindow(handle);
                        Color co = img_handleCheck.GetPixel(284, 284);
                        //Debug.WriteLine($"{HexConverter(co)} ---- {co.R}-{co.G}-{co.B}");
                        sw.WriteLine($"{HexConverter(co)} --- {co.R}-{co.G}-{co.B} --- {co.R + co.G + co.B} --- {co.ToArgb()}");
                        Delay(1000);
                    }
                    sw.Close();
                }

            });
            get.Start();
        }

        // ------- Func Check -------- //

        // Kiểm tra xem đã hiện nút bảo quản hay chưa
        public bool checkDoneFishing()
        {
            string check_Color = getColorPercentXYToHex(EnumAuto.point_ButtonStore);
            return check_Color == "#41C5F3" ? true : false;
        }

        // Kiểm tra xem có đang hiện nút space (không câu ca) hay không
        public bool checkDisplayBag()
        {
            //Thread.Sleep(500);
            if (getColorPercentXYToHex(EnumAuto.point_IsBagDisplay) == "#E44142")
            {
                return true;
            } else
            {
                return false;
            }
        }

        // Kiểm tra xem có đang hiện nút sửa chửa ok không
        public bool checkDisplayButtonOKFixRod()
        {
            Delay(500);
            if (getColorPercentXYToHex(EnumAuto.point_ButtonFixRod) == "#41C5F3")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Kiểm tra xem đã hiện tab cần câu hay chưa
        public bool checkDisplayTabRod()
        {
            Delay(200);
            if (getColorPercentXYToHex(EnumAuto.point_TabRod) == "#FFFFFF")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Kiểm tra xem đã hiện tab phương tiện hay chưa
        public bool checkDisplayTabVehicle()
        {
            Delay(200);
            if (getColorPercentXYToHex(EnumAuto.point_TabVehicle) == "#FFFFFF")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Kiểm tra xem đã hiện button close Balo hay chưa?
        public bool checkDisplayButtonCloseBag()
        {
            Delay(200);
            if (getColorPercentXYToHex(EnumAuto.point_CloseBag) == "#FFFFFF")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void checkNotFishing()
        {
            Task get = new Task(() =>
            {
                int count = 0;
                int countBalo = 0;
                int countButtonFixRod = 0;
                while (true)
                {
                    if (!isAuto) { break; }
                    if (getColorPercentXYToHex(EnumAuto.point_IsBagDisplay) == "#E44142")
                    {
                        count++;
                        if(count > 10)
                        {
                            break;
                        }
                    } else
                    {
                        count = 0;
                    }
                    if (getColorPercentXYToHex(EnumAuto.point_CloseBag) == "#FFFFFF")
                    {
                        countBalo++;
                        if (countBalo > 20)
                        {
                            break;
                        }
                    }
                    else
                    {
                        countBalo = 0;
                    }
                    if (getColorPercentXYToHex(EnumAuto.point_ButtonFixRod) == "#41C5F3")
                    {
                        countButtonFixRod++;
                        if (countButtonFixRod > 5)
                        {
                            break;
                        }
                    }
                    else
                    {
                        countButtonFixRod = 0;
                    }
                    Delay(1000);
                }
                if(count > 10 && isAuto && !isGetPosition)
                {
                    // Gọi lại hàm chạy auto
                    stopAuto();
                    newTaskAuto().Start();
                } else if (countBalo > 20 && !isGetPosition)
                {
                    controlClickPercentXY(EnumAuto.point_CloseBag);
                    stopAuto();
                    newTaskAuto().Start();
                }
                else if (countButtonFixRod > 5 && !isGetPosition)
                {
                    controlClickPercentXY(EnumAuto.point_ButtonFixRod);
                    while (!checkDisplayButtonCloseBag())
                    {
                        if (!isAuto) { break; }
                    }
                    controlClickPercentXY(EnumAuto.point_CloseBag);
                    stopAuto();
                    newTaskAuto().Start();
                }
            });
            get.Start();
        }

        public bool testND()
        {
            Image img = CaptureHelper.CaptureWindow(handle);
            Bitmap img_Check = getImageByListPixelXY(detect_KVBanDau, img);

            Bitmap img_CheckGray = MakeGrayscale3(img_Check);
            int current_HashCheck = 0;
            int center_TrucDoc = (int)(img_Check.Width / 2);
            List<Color> list_ColorCheck = new List<Color>();
            int i = 0;
            int khac = 0;
            for (int x = 0; x < img_Check.Width; x++)
            {
                for (int y = 0; y < img_Check.Height; y++)
                {
                    
                    Color pixelColorGray = img_CheckGray.GetPixel(x, y);
                    if (center_TrucDoc == x)
                    {
                        Color pixelColor = img_Check.GetPixel(x, y);
                        //list_ColorCheck.Add(pixelColor);
                        //Color pixelColorDoc = img_Check.GetPixel(center_TrucDoc, y);
                        if (list_ColorCheckAfter.Count > i)
                        {
                            //Debug.WriteLine("list_Color: " + (HexConverter(list_ColorCheckAfter[i])));
                            //Debug.WriteLine("pixelColor: " + (HexConverter(pixelColor)));
                            //Debug.WriteLine("pixelColor: " + (list_ColorCheckAfter[i].ToArgb() != pixelColor.ToArgb()));
                            if (!colorComparison(list_ColorCheckAfter[i], pixelColor, 10) && !colorComparison(pixelColor, Color.FromArgb(254, 254, 254), 10))
                            {
                                khac++;
                            }
                            list_ColorCheckAfter[i] = pixelColor;
                        }
                        else
                        {
                            list_ColorCheckAfter.Add(pixelColor);
                        }
                        i++;
                    }
                    current_HashCheck += (pixelColorGray.R + pixelColorGray.G + pixelColorGray.B);
                }
            }
            int chenhLech = Math.Abs(current_HashCheck - hashCheck);
            if(chenhLech < 1000 && nguong_ChenhLechAll > 5000)
            {
                countMauKhongDoi++;
                if(countMauKhongDoi > 30)
                {
                    nguong_ChenhLechAll = 5000;
                    Debug.WriteLine("Đổi ngưỡng");
                }
            } else
            {
                countMauKhongDoi = 0;
            }
            
            //Color beforeColor = Color.FromArgb(0, 0, 0);
            //int doDaiDaiMau = 0;
            //bool done = false;
            
            //int chenhLechDoc = Math.Abs(current_HashTrucDoc - hashCheckDoc);
            if (hashCheck == 0) {
                hashCheck = current_HashCheck;
                return false;
            } else
            {
                hashCheck = current_HashCheck;
                //int gioiHan = (int)((double)img_Check.Height - 5);

                //Debug.WriteLine("khac: " + khac);
                //Debug.WriteLine("gioiHan: " + (img_Check.Height - 5));
                //Debug.WriteLine("gioiHan: " + gioiHan);
                //img_Check.Save($"data/img_cap/2020/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}---{chenhLech}.png");
                if (chenhLech > nguong_ChenhLechAll && khac < (img_Check.Height - 1) && khac != 0)
                {
                    //Debug.WriteLine("gioiHan: " + (img_Check.Height - 1));
                    Debug.WriteLine("LÊN: " + khac);
                    Debug.WriteLine("chenhLechAAA: " + chenhLech);
                    //Debug.WriteLine("list_ColorCheckKhac: " + list_ColorCheckKhac.Count);
                    //Debug.WriteLine("all_ColorCheckKhac: " + all_ColorCheckKhac.Count);
                    //Debug.WriteLine("gioiHan: " + (img_Check.Height - 1));
                    getDetectExclamationArea((Bitmap)img, img_Check);
                    //Debug.WriteLine(HexConverter(colorMax));
                    return true;

                } else
                {
                    //Debug.WriteLine("gioiHan: " + (khac < (img_Check.Height - 1)));
                    Debug.WriteLine("chenhLech: " + chenhLech);
                    if (chenhLech > 3000)
                    {
                        Debug.WriteLine("chenhLech: " + chenhLech);
                        Debug.WriteLine("khac: " + khac);
                    }
                    //Debug.WriteLine("Height: " + img_Check.Height);
                    return false;
                }
            }
        }


        // Kiểm tra cá cắn câu bằng 2 ô cảm biến
        public bool checkDetectSensorArea(Bitmap img_handleCheck = null, bool isUpdateExclamationArea = false)
        {
            Debug.WriteLine("Zooo");
            int colorMax1 = 0;
            int colorMax2 = 0;
            string hexColorMax1 = "";
            string hexColorMax2 = "";
            if(img_handleCheck == null)
            {
                img_handleCheck = (Bitmap)(CaptureHelper.CaptureWindow(handle));
            }
            List<string> allColorPixel1 = new List<string>();
            List<string> allColorPixel2 = new List<string>();
            List<string> colorPixelUnique1 = new List<string>();
            List<string> colorPixelUnique2 = new List<string>();

            // Kiểm tra cắn câu ô nhận diện 1 (ô phía dưới)
            for (int x = detect_SensorArea1[0].x; x <= detect_SensorArea1[1].x; x++)
            {
                for (int y = detect_SensorArea1[0].y; y <= detect_SensorArea1[1].y; y++)
                {
                    Color pixelColor = img_handleCheck.GetPixel(x, y);
                    string hexColor = HexConverter(pixelColor);
                    allColorPixel1.Add(hexColor);
                    if (!colorPixelUnique1.Contains(hexColor))
                    {
                        colorPixelUnique1.Add(hexColor);
                    }
                }
            }
            foreach (string color in colorPixelUnique1)
            {
                int count = allColorPixel1.Where(x => x.Equals(color)).Count();
                if (count > colorMax1)
                {
                    colorMax1 = count;
                    hexColorMax1 = color;
                }
            }
            


            // Kiểm tra cắn câu ô nhận diện 2 (ô phía trên)
            for (int x = detect_SensorArea2[0].x; x <= detect_SensorArea2[1].x; x++)
            {
                for (int y = detect_SensorArea2[0].y; y <= detect_SensorArea2[1].y; y++)
                {
                    Color pixelColor = img_handleCheck.GetPixel(x, y);
                    string hexColor = HexConverter(pixelColor);
                    allColorPixel2.Add(hexColor);
                    if (!colorPixelUnique2.Contains(hexColor))
                    {
                        colorPixelUnique2.Add(hexColor);
                    }
                }
            }

            foreach (string color in colorPixelUnique2)
            {
                int count = allColorPixel2.Where(x => x.Equals(color)).Count();
                if (count > colorMax2)
                {
                    colorMax2 = count;
                    hexColorMax2 = color;
                }
            }

            double phanTram1 = (double)colorMax1 / (double)allColorPixel1.Count;
            double phanTram2 = (double)colorMax2 / (double)allColorPixel2.Count;
            Debug.WriteLine("phanTram1: " + phanTram1);
            Debug.WriteLine("phanTram2: " + phanTram2);

            bool fishingCheck1 = false;
            if (phanTram1 > 0.3 && phanTram1 <= 1)
            {
                Color color_1 = ColorTranslator.FromHtml(hexColorMax1);
                if (!colorComparison(color_1, EnumAuto.color_NoCheck, 30))
                {
                    bool notDefault1 = !colorDefault_SensorArea1.Contains(hexColorMax1);
                    fishingCheck1 = notDefault1 && !EnumAuto.list_ColorNoCheckFish.Contains(hexColorMax1);
                }
            }

            bool fishingCheck2 = false;
            if(phanTram2 > 0.3 && phanTram2 <= 1)
            {
                Color color_2 = ColorTranslator.FromHtml(hexColorMax2);
                if (!colorComparison(color_2, EnumAuto.color_NoCheck, 30))
                {
                    bool notDefault2 = !colorDefault_SensorArea2.Contains(hexColorMax2);
                    fishingCheck2 = notDefault2 && !EnumAuto.list_ColorNoCheckFish.Contains(hexColorMax2);
                }
            }
            //if (fishingCheck1)
            //{
            //    getDetectExclamationArea(img_handleCheck, hexColorMax1);
            //}
            //else if (fishingCheck2)
            //{
            //    getDetectExclamationArea(img_handleCheck, hexColorMax2);
            //}
            return fishingCheck1 || fishingCheck2;
        }

        public bool checkD()
        {
            Bitmap currenImgCheck = getImageByListPixelXY(detect_ExclamationArea);

            // kiểm tra trùng màu cá
            int center_x = currenImgCheck.Width / 2;
            int center_y = currenImgCheck.Height - 1;
            Color pixelCenter = currenImgCheck.GetPixel(center_x, center_y);
            if(colorComparisonList(pixelCenter, EnumAuto.list_ColorColorFish, 1))
            {
                return false;
            } else
            {
                int phanTram = checkSame(OKImgCheck, MakeGrayW(currenImgCheck, pixelCenter));
                if (phanTram >= acceptError - 2)
                {
                    //currenImgCheck.Save($"data/img_cap/pro/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                    //MakeGrayW(currenImgCheck, pixelCenter).Save($"data/img_cap/pro/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                    acceptError = acceptError < phanTram || acceptError <= 78 ? acceptError : phanTram;
                    Image imgFull = CaptureHelper.CaptureWindow(handle);
                    //imgFull.Save($"data/img_cap/phao-pro/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                    if (!check_BuoysAreaDone)
                    {
                        
                        //imgFull.Save($"data/img_cap/phao-pro/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                        getCoordinatesBuoys((Bitmap)imgFull);
                    }
                    currenImgCheck.Dispose();
                    return true;
                }
                else
                {
                    currenImgCheck.Dispose();
                    return false;
                }
            }
        }

        public int checkSame(Bitmap img1, Bitmap img2)
        {
            int count = 0;
            for(int x = 0; x < img1.Width; x++)
            {
                for (int y = 0; y < img1.Height; y++)
                {
                    if(img1.GetPixel(x, y).R == img2.GetPixel(x, y).R)
                    {
                        count++;
                    }
                }
            }
            double size = (double)(img1.Width * img1.Height);

            int phanTram = (int)((double)((double)count / size) * (double)100);
            return phanTram;
        }

        public Bitmap MakeGrayW(Bitmap original, Color pixelCenter)
        {
            Bitmap imgGray = new Bitmap(original.Width, original.Height);
            byte centerR = pixelCenter.R;
            byte centerG = pixelCenter.G;
            byte centerB = pixelCenter.B;

            byte centerGray = (byte)(0.2126 * centerR + 0.7152 * centerG + 0.0722 * centerB);


            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixel = original.GetPixel(x, y);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    byte gray = (byte)(0.2126 * R + 0.7152 * G + 0.0722 * B);
                    if(gray > centerGray - 5 && gray < centerGray + 5)
                    {
                        imgGray.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    } else
                    {
                        imgGray.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                }
            }
            return imgGray;
        }

        // ảnh nhị phân
        public Bitmap MakeWhiteBlack(Bitmap original, int threshold = 180)
        {
            Bitmap imgGray = new Bitmap(original.Width, original.Height);


            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixel = original.GetPixel(x, y);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    byte gray = (byte)(0.2126 * R + 0.7152 * G + 0.0722 * B);
                    if (gray > threshold)
                    {
                        imgGray.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        imgGray.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                }
            }
            return imgGray;
        }

        // chuyển ảnh sang thang độ xám
        public static Bitmap MakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            using (Graphics g = Graphics.FromImage(newBitmap))
            {

                //create the grayscale ColorMatrix
                ColorMatrix colorMatrix = new ColorMatrix(
                   new float[][]
                   {
                         new float[] {.3f, .3f, .3f, 0, 0},
                         new float[] {.59f, .59f, .59f, 0, 0},
                         new float[] {.11f, .11f, .11f, 0, 0},
                         new float[] {0, 0, 0, 1, 0},
                         new float[] {0, 0, 0, 0, 1}
                   });

                //create some image attributes
                using (ImageAttributes attributes = new ImageAttributes())
                {

                    //set the color matrix attribute
                    attributes.SetColorMatrix(colorMatrix);

                    //draw the original image on the new image
                    //using the grayscale color matrix
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                                0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return newBitmap;
        }

        // Kiểm tra xem một khu vực nào đó có chứa quá threshold % 1 màu sắc nào đó (dùng để kiểm tra sửa cần, đứt dây)
        public bool checkDetectAreaWithColor(List<pointXY> checkArea, string color, double threshold)
        {
            if (checkArea.Count == 0)
            {
                return false;
            }
            else
            {
                int count = 0;
                Bitmap img_handleCheck = (Bitmap)(CaptureHelper.CaptureWindow(handle));

                // Kiểm tra màu
                for (int x = checkArea[0].x; x <= checkArea[1].x; x++)
                {
                    for (int y = checkArea[0].y; y <= checkArea[1].y; y++)
                    {
                        Color pixelColor = img_handleCheck.GetPixel(x, y);
                        string hexColor = HexConverter(pixelColor);
                        if (hexColor == color)
                        {
                            count++;
                        }
                    }
                }
                double thresholdColor = (double)count / (double)getTotalPixelArea(checkArea);
                return thresholdColor > threshold ? true : false;
            }
        }

        // Kiểm tra xem một khu vực nào đó có chứa quá threshold px 1 màu sắc nào đó (dùng để kiểm toạ độ nút xem thông tin cần câu)
        public bool checkDetectAreaWithColorPixel(List<percentXY> checkArea, string color, int threshold)
        {
            int count = 0;
            Bitmap img_handle = (Bitmap)(CaptureHelper.CaptureWindow(handle));
            pointXY point_TL = percentToPixel(checkArea[0]);
            pointXY point_BR = percentToPixel(checkArea[1]);

            for (int x = point_TL.x; x <= point_BR.x; x++)
            {
                for (int y = point_TL.y; y <= point_BR.y; y++)
                {
                    Color pixelColor = img_handle.GetPixel(x, y);
                    string hexColor = HexConverter(pixelColor);
                    if (hexColor == color)
                    {
                        count++;
                    }
                }
            }
            return count > threshold ? true : false;
        }

        // ------- Func Get -------- //

        // Lấy toạ độ 1 khu vực bằng màu sắc
        public static List<pointXY> getColorArea(Bitmap img_handleCheck, string hexColor)
        {
            int xMax = 0;
            int xMin = 99999;
            int yMax = 0;
            int yMin = 99999;
            List<pointXY> res = new List<pointXY>();
            int x, y;
            for (x = 0; x < img_handleCheck.Width; x++)
            {
                for (y = 0; y < img_handleCheck.Height; y++)
                {
                    Color pixelColor = img_handleCheck.GetPixel(x, y);
                    if (HexConverter(pixelColor) == hexColor)
                    {
                        xMax = x > xMax ? x : xMax;
                        xMin = x < xMin ? x : xMin;
                        yMax = y > yMax ? y : yMax;
                        yMin = y < yMin ? y : yMin;
                    }
                }
            }
            if (xMax > 0)
            {
                res.Add(new pointXY(xMin, yMin));
                res.Add(new pointXY(xMax, yMax));
            }
            return res;
        }

        // Lấy toạ độ bao quanh 1 khu vực hình
        public static List<pointXY> getAroundArea(List<pointXY> area)
        {
            int xMax = 0;
            int xMin = 99999;
            int yMax = 0;
            int yMin = 99999;
            List<pointXY> res = new List<pointXY>();
            int x, y;
            foreach(pointXY point in area)
            {
                xMax = point.x > xMax ? point.x : xMax;
                xMin = point.x < xMin ? point.x : xMin;
                yMax = point.y > yMax ? point.y : yMax;
                yMin = point.y < yMin ? point.y : yMin;
            }
            if (xMax > 0)
            {
                res.Add(new pointXY(xMin, yMin));
                res.Add(new pointXY(xMax, yMax));
            }
            return res;
        }

        // Lấy toạ độ 1 khu vực bằng màu sắc tương đối
        public List<pointXY> getColorRelativeArea(Bitmap img_handleCheck, string hexColor, int difference)
        {
            int xMax = 0;
            int xMin = 99999;
            int yMax = 0;
            int yMin = 99999;
            List<pointXY> res = new List<pointXY>();
            int x, y;
            Color color_Check = ColorTranslator.FromHtml(hexColor);
            for (x = 0; x < img_handleCheck.Width; x++)
            {
                for (y = 0; y < img_handleCheck.Height; y++)
                {
                    Color pixelColor = img_handleCheck.GetPixel(x, y);
                    if (colorComparison(pixelColor, color_Check, difference))
                    {
                        xMax = x > xMax ? x : xMax;
                        xMin = x < xMin ? x : xMin;
                        yMax = y > yMax ? y : yMax;
                        yMin = y < yMin ? y : yMin;
                    }
                }
            }
            if (xMax > 0)
            {
                res.Add(new pointXY(xMin, yMin));
                res.Add(new pointXY(xMax, yMax));
            }
            return res;
        }

        //Lọc bóng cá
        public void filterShadowFish()
        {
            Debug.WriteLine("check_BuoysAreaDone: " + check_BuoysAreaDone);
            if (check_BuoysAreaDone)
            {
                Task get = new Task(() =>
                {
                    ConfigFishSize configSize = ConfigFishSize._ListConfigFishSize.SingleOrDefault(r => r.auto_id == auto_id);
                    Delay(6000);
                    int sizeFish = getSizeFish();
                    Debug.WriteLine("sizeFish: " + sizeFish);
                    shadowSize = sizeFish;
                    if (configSize.mode_auto_count_check == -1 || ConfigFishSize.checkDisableAll(configSize.auto_id))
                    {
                        currentSizeShadowFish = sizeFish;
                        if (sizeFish <= configSize.listSize[0].max)
                        {
                            if (configSize.listSize[0].isChecked)
                            {
                                controlClickPercentXY(EnumAuto.point_ButtonSpace);
                                skipFish = true;
                                Debug.WriteLine("bỏ qua");
                            }
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                messageNotiStep = $"Phát hiện bóng cá 1 (kích thước: {sizeFish})" + (configSize.listSize[0].isChecked ? "  →  bỏ qua" : "");
                                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                                if (configSize.listSize[0].isChecked) { 
                                    wnd.setSummaryTotal(auto_id, ++summaryTotal.boQuaCa, 2);
                                }
                            });
                        }
                        else if (sizeFish <= configSize.listSize[1].max)
                        {
                            if (configSize.listSize[1].isChecked)
                            {
                                controlClickPercentXY(EnumAuto.point_ButtonSpace);
                                skipFish = true;
                            }
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                messageNotiStep = $"Phát hiện bóng cá 2 (kích thước: {sizeFish})" + (configSize.listSize[1].isChecked ? "  →  bỏ qua" : "");
                                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                                if (configSize.listSize[1].isChecked) {
                                    wnd.setSummaryTotal(auto_id, ++summaryTotal.boQuaCa, 2);
                                }
                            });
                        }
                        else if (sizeFish <= configSize.listSize[2].max)
                        {
                            if (configSize.listSize[2].isChecked)
                            {
                                controlClickPercentXY(EnumAuto.point_ButtonSpace);
                                skipFish = true;
                            }
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                messageNotiStep = $"Phát hiện bóng cá 3 (kích thước: {sizeFish})" + (configSize.listSize[2].isChecked ? "  →  bỏ qua" : "");
                                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                                if (configSize.listSize[2].isChecked)
                                {
                                    wnd.setSummaryTotal(auto_id, ++summaryTotal.boQuaCa, 2);
                                }
                            });
                        }
                        else if (sizeFish <= configSize.listSize[3].max)
                        {
                            if (configSize.listSize[3].isChecked)
                            {
                                controlClickPercentXY(EnumAuto.point_ButtonSpace);
                                skipFish = true;
                            }
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                messageNotiStep = $"Phát hiện bóng cá 4 (kích thước: {sizeFish})" + (configSize.listSize[3].isChecked ? "  →  bỏ qua" : "");
                                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                                if (configSize.listSize[3].isChecked)
                                {
                                    wnd.setSummaryTotal(auto_id, ++summaryTotal.boQuaCa, 2);
                                }
                            });
                        }
                        else if (sizeFish <= configSize.listSize[4].max)
                        {
                            if (configSize.listSize[4].isChecked)
                            {
                                controlClickPercentXY(EnumAuto.point_ButtonSpace);
                                skipFish = true;
                            }
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                messageNotiStep = $"Phát hiện bóng cá 5 (kích thước: {sizeFish})" + (configSize.listSize[4].isChecked ? "  →  bỏ qua" : "");
                                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                                if (configSize.listSize[4].isChecked)
                                {
                                    wnd.setSummaryTotal(auto_id, ++summaryTotal.boQuaCa, 2);
                                }
                            });
                        } else
                        {
                            messageNotiStep = $"Phát hiện bóng cá (kích thước: {sizeFish})";
                            wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                        }
                    } else
                    {
                        currentSizeShadowFish = sizeFish;
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            messageNotiStep = $"Phát hiện bóng cá (kích thước: {sizeFish})";
                            wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                        });
                    }
                });
                get.Start();
            } else
            {
                shadowSize = 0;
            }
        }

        // Lấy toạ độ dấu chấm than (chạy nền khi câu xong lần đầu)
        public void getDetectExclamationArea(Bitmap img_handleCheck, Bitmap bitmapChamThan)
        {
            Task get = new Task(() =>
            {
                if (detect_ExclamationArea.Count != 2)
                {
                    //img_handleCheck.Save($"data/img_cap/phao-pro/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                    //bitmapChamThan.Save($"data/img_cap/phao-pro/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                    int center = (int)(bitmapChamThan.Width / 2);
                    List<string> colorPixelUnique = new List<string>();
                    List<string> colorPixelAll = new List<string>();

                    string colorMax = "";
                    int colorMaxCount = 0;
                    for (int y = 0; y < bitmapChamThan.Height - 1; y++)
                    {
                        Color color = bitmapChamThan.GetPixel(center, y);
                        string hexColor = HexConverter(color);
                        if (!colorPixelUnique.Contains(hexColor))
                        {
                            colorPixelUnique.Add(hexColor);
                        }
                        colorPixelAll.Add(hexColor);
                    }
                    foreach(string color in colorPixelUnique)
                    {
                        int count = 0;
                        foreach (string colorAll in colorPixelAll)
                        {
                            if (colorAll == color)
                            {
                                count++;
                            }
                        }
                        //Debug.WriteLine("Color Unique: " + color);
                        if (count > colorMaxCount)
                        {
                            //Debug.WriteLine("count: " + count);
                            colorMaxCount = count;
                            colorMax = color;
                            //Debug.WriteLine("maxxColor: " + colorMax);
                        }
                    }
                    //foreach (Color color in list_ColorCheckKhac)
                    //{
                    //    Debug.WriteLine(HexConverter(color));
                    //    int count = 0;
                    //    foreach(Color colorK in all_ColorCheckKhac)
                    //    {
                    //        if(colorComparison(color, colorK, 15))
                    //        {
                    //            count++;
                    //        }
                    //    }
                    //    if (count > colorMaxCount)
                    //    {
                    //        Debug.WriteLine("count: " + count);
                    //        colorMaxCount = count;
                    //        colorMax = color;
                    //        Debug.WriteLine("maxxColor: " + HexConverter(colorMax));
                    //    }
                    //}
                    
                    List<pointXY> areaRes = getColorRelativeArea(img_handleCheck, colorMax, 10);

                    // Công thêm để tránh 100%
                    int congThem = 4;
                    areaRes[0].x -= 6;
                    areaRes[0].y -= congThem + 6;
                    areaRes[1].x += 6;
                    areaRes[1].y += congThem + 8;

                    double maxSizeAreaName = (double)(handleHeight * handleWidth) / (double)100 * (double)10;
                    //Debug.WriteLine("maxSizeAreaName: " + maxSizeAreaName);
                    //Debug.WriteLine("areaRes: " + getTotalPixelArea(areaRes));
                    if ((double)getTotalPixelArea(areaRes) < maxSizeAreaName)
                    {
                        areaRes[1].y = (int)((double)areaRes[0].y + (double)(areaRes[1].y - areaRes[0].y) / (double)2);
                        detect_ExclamationArea = areaRes;

                        Image imageCheckOK = (Image)img_handleCheck;
                        Bitmap img_handleCheck_Clone = CropImage(img_handleCheck, 0, 0, img_handleCheck.Width, img_handleCheck.Height);
                        getCoordinatesBuoys(img_handleCheck_Clone);
                        int cropWidth = areaRes[1].x - areaRes[0].x;
                        int cropHeight = areaRes[1].y - areaRes[0].y;

                        Bitmap cropOKCheck = CropImage(img_handleCheck, areaRes[0].x, areaRes[0].y, cropWidth, cropHeight);
                        // kiểm tra trùng màu cá
                        int center_x = cropOKCheck.Width / 2;
                        int center_y = cropOKCheck.Height - 1;
                        Color pixelCenter = cropOKCheck.GetPixel(center_x, center_y);
                        OKImgCheck = MakeGrayW(cropOKCheck, pixelCenter);
                        //cropOKCheck.Save($"data/img_cap/pro/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                        //img_handleCheck.Save($"data/img_cap/pro/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");

                        int x = detect_ExclamationArea[0].x;
                        int y = detect_ExclamationArea[0].y;
                        int width = detect_ExclamationArea[1].x - x;
                        int height = detect_ExclamationArea[1].y - y;
                        useSensor = false;
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            messageNotiStep = "Nhận diện toạ độ dấu chấm than thành công";
                            wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                        });
                        removeDraw();
                        drawArea(detect_ExclamationArea, nameAuto, "#86EFAC");
                    }
                }
            });
            get.Start();
        }

        // Lấy danh sách màu của một khu vực (dùng để làm bộ màu mặc định)
        public List<string> getAllColorArea(List<pointXY> checkArea)
        {
            Bitmap img_handle = (Bitmap)(CaptureHelper.CaptureWindow(handle));
            int x1 = checkArea[0].x > 0 ? checkArea[0].x : 0;
            int y1 = checkArea[0].y > 0 ? checkArea[0].y : 0;
            int x2 = checkArea[1].x < img_handle.Width - 1 ? checkArea[1].x : img_handle.Width - 1;
            int y2 = checkArea[1].y < img_handle.Height - 1 ? checkArea[1].y : img_handle.Height - 1;
            List<string> res = new List<string>();
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    Color pixelColor = img_handle.GetPixel(x, y);
                    string hexColor = HexConverter(pixelColor);
                    if (!res.Contains(hexColor))
                    {
                        res.Add(hexColor);
                    }
                }
            }
            return res;
        }

        // Lấy id cần câu
        public int getIdRod()
        {
            FishingRodConfig configRod = FishingRodConfig._ListFishingRodConfig.SingleOrDefault(r => r.auto_id == auto_id);
            if(currentFishingRod == null)
            {
                return configRod.fishing_rod_1;
            } else if(currentFishingRod.id == configRod.fishing_rod_2)
            {
                return configRod.fishing_rod_1;
            } else
            {
                return configRod.fishing_rod_2;
            }
        }

        // Lấy thông tin step đổi cần câu
        public int getStepChangeRod()
        {
            FishingRodConfig configRod = FishingRodConfig._ListFishingRodConfig.SingleOrDefault(r => r.auto_id == auto_id);
            if (configRod.change_fishing_rod)
            {
                return configRod.step_change;
            } else
            {
                return -1;
            }
        }

        // Lấy thông tin ngôn ngữ
        //public void getLanguage()
        //{
        //    Bitmap img_RAW = getImageByListPercentXY(EnumAuto.area_LanguageCheck);
        //    List<pointXY> areaCheck = getColorRelativeArea(img_RAW, "#FFFFFF", 0);
        //    percentXY TL = pixelToPercent(areaCheck[0]);
        //    percentXY BR = pixelToPercent(areaCheck[1]);
        //    percentXY img_Language_TL = new percentXY(EnumAuto.area_LanguageCheck[0].x + TL.x, EnumAuto.area_LanguageCheck[0].y + TL.y);
        //    percentXY img_Language_BR = new percentXY(EnumAuto.area_LanguageCheck[1].x - BR.x, EnumAuto.area_LanguageCheck[1].y - BR.y);
        //    List<percentXY> area = new List<percentXY> { img_Language_TL, img_Language_BR };
        //    Bitmap img_Language = ResizeBitmapReadText(getImageByListPercentXY(area));

        //    MakeWhiteBlack(img_Language).Save($"data/img_cap/language/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
        //}
        public void getLanguage()
        {
            ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
            if(configAuto.language == -1)
            {
                Bitmap img_Language = ResizeBitmapReadText(getImageByListPercentXY(EnumAuto.area_LanguageCheck));
                Bitmap img_Check = MakeWhiteBlack(img_Language);
                //img_Check.Save($"data/img_cap/language/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                string name = OCR(img_Check);
                int id = ImageToText.getIdLanguage(name);
                App.Current.Dispatcher.Invoke(() =>
                {
                    messageNotiStep = "Nhận diện ngôn ngữ trong game: " + (id == 0 ? "Tiếng Việt" : "English");
                    wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                    ConfigAuto.changeLanguage(id, configAuto.auto_id, true);
                });
            }
        }

        // Check thời tiết
        public void checkWeather()
        {
            Task get = new Task(() =>
            {
                int countCheck = 0;
                for(int i = 0; i < 5; i++)
                {
                    int timer = 0;
                    while (true && timer <= 10)
                    {
                        Delay(100);
                        Bitmap currentImage = (Bitmap)(CaptureHelper.CaptureWindow(handle));
                        if (getTotalColorArea(currentImage))
                        {
                            countCheck++;
                            break;
                        }
                        timer++;
                    }
                    Debug.WriteLine("---------------");
                }
                Debug.WriteLine("countCheck: " + countCheck);
                if(countCheck >= 4)
                {
                    Debug.WriteLine("weather: mưa");
                } else
                {
                    Debug.WriteLine("weather: bình thường");
                }
            });
            get.Start();
        }

        public bool getTotalColorArea(Bitmap currentImage)
        {
            foreach(pointXY position in list_PointWeather)
            {
                int totalColor = 0;
                Color color = Color.FromArgb(0, 0, 0);
                pointXY point_TL = position;
                pointXY point_BR = new pointXY(point_TL.x + 20, point_TL.y + 10);
                List<pointXY> area = new List<pointXY> { point_TL, point_BR };
                //int totalColorPrve = 0;
                //int totalColorCurrent = 0;
                for (int x = point_TL.x; x < point_BR.x; x++)
                {
                    for (int y = point_TL.y; y < point_BR.y; y++)
                    {
                        Color colorCurrent = currentImage.GetPixel(x, y);
                        //totalColorCurrent += colorCurrent.ToArgb();
                        if(color.ToArgb() != colorCurrent.ToArgb())
                        {
                            totalColor++;
                            color = colorCurrent;
                        }
                    }
                }
                Debug.WriteLine("totalColor: " + totalColor);
                //int chenhLech = Math.Abs(totalColorPrve - totalColorCurrent);
                if (totalColor > 3)
                {
                    return true;
                }
            }
            return false;
        }

        // Lấy danh sách siteMap túi cành câu
        public void getSiteMapFishingRod(bool checkStopGetPoition = false)
        {
            if (!isAuto) { return; }

            getLanguage();
            siteMapFishingRod = new List<FishingRodDetect>();
            int index = 0;
            Delay(400);

            App.Current.Dispatcher.Invoke(() =>
            {
                messageNotiStep = "Nhận diện thông tin cần câu...";
                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
            });
            if (!isAuto) { return; }

            foreach (List<percentXY> point in EnumAuto.list_FishingRod)
            {
                if(checkStopGetPoition && !isGetPosition) {
                    siteMapFishingRod = new List<FishingRodDetect>();
                    isAuto = false;
                    isGetPosition = false;
                    controlClickPercentXY(EnumAuto.point_closeInfoFishingRod);
                    break;
                }
                if (!isAuto) { break; }
                index++;
                if (checkDetectAreaWithColorPixel(point, "#82BDCD", 10) || checkDetectAreaWithColorPixel(point, "#3B465C", 10))
                {
                    ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
                    Delay((100 - configAuto.speed) * 40);
                    controlClickPercentXY(point[0]);
                    while (getColorPercentXYToHex(EnumAuto.point_PopupInfoRodShow) != "#E7DACB")
                    {
                        Delay(100);
                        if (!isAuto) { break; }
                    }
                    Bitmap img_Name = ResizeBitmapReadText(getImageByListPercentXY(EnumAuto.area_NameFishingRod));
                    Bitmap img_Check = MakeWhiteBlack(img_Name);
                    //img_Check.Save($"data/img_cap/name_rod/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                    string name = OCRRod(img_Check);

                    Debug.WriteLine(name);
                    bool isLanguageVI = languageConfig.getLanguageByAutoId(auto_id) == 0;
                    FishingRodDetect infoFishingRod = isLanguageVI ? ImageToText.getFishingName(name) : ImageToText.getFishingNameEN(name);
                    infoFishingRod.index = index;
                    infoFishingRod.point = point[0];
                    siteMapFishingRod.Add(infoFishingRod);

                    Delay((100 - configAuto.speed) * 40);
                    controlClickPercentXY(EnumAuto.point_closeInfoFishingRod);
                    Delay(600);
                }
            }
            //siteMapFishingRod = siteMapFishingRod.Distinct().ToList();
            if (checkStopGetPoition && !isGetPosition) {
                siteMapFishingRod = new List<FishingRodDetect>();
                isAuto = false;
                isGetPosition = false;
                return;
            }
            siteMapFishingRod = siteMapFishingRod.GroupBy(t => t.id).Select(g => g.First()).ToList();
            selectFishingRod(getIdRod());
        }

        public void getInfoFish()
        {
            Bitmap img = (Bitmap)CaptureHelper.CaptureWindow(handle);
            Task get = new Task(() =>
            {
                ConfigAuto configAuto = ConfigAuto._ListConfigAuto.SingleOrDefault(r => r.auto_id == auto_id);
                string colorLevel = getColorPercentXYToHex(EnumAuto.point_LevelFish, img);
                int level = 0;
                colorLevelFishNotiStep = "#E4E0C5";
                switch (colorLevel)
                {
                    case "#E4E0C5":
                        level = 1;
                        colorLevelFishNotiStep = "#E4E0C5";
                        break;
                    case "#A3E467":
                        level = 2;
                        colorLevelFishNotiStep = "#A3E467";
                        break;
                    case "#59C6D9":
                        level = 3;
                        colorLevelFishNotiStep = "#59C6D9";
                        break;
                    case "#E793E8":
                        level = 4;
                        colorLevelFishNotiStep = "#E793E8";
                        break;
                    case "#DDEDEE":
                        level = 0;
                        colorLevelFishNotiStep = "#DDEDEE";
                        break;
                    default:
                        level = 0;
                        colorLevelFishNotiStep = "#DDEDEE";
                        break;
                }
                bool isVM = getColorPercentXYToHex(EnumAuto.point_checkVM, img) == "#F8D638";
                Bitmap img_Name = ResizeBitmapReadText(getImageByListPercentXY(EnumAuto.area_FishName, (Image)img));
                Bitmap img_Check = MakeWhiteBlack(img_Name, 150);
                //img_Check.Save($"data/img_cap/name_fish/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                string name = OCR(img_Check);
                Fish fish = Fish.getFishName(name, level, isVM, configAuto.language);
                ConfigFishSize configSize = ConfigFishSize._ListConfigFishSize.SingleOrDefault(r => r.auto_id == auto_id);
                if (configSize.mode_setting == "auto" && currentSizeShadowFish > 10 && currentSizeShadowFish < 8000 && fish.size > 0)
                {
                    ConfigFishSize.changeMinMaxAuto(auto_id, fish.size, currentSizeShadowFish);
                }
                typeNotiStep = 1;
                nameFishNotiStep = fish.name_vi;
                isVMFishNotiStep = isVM;
                sizeFishNotiStep = fish.size;

                //Bitmap imgCheckMoney = (Bitmap)CaptureHelper.CaptureWindow(handle);
                List<pointXY> areaMoney = getColorRelativeArea(img, "#9BF676", 10);
                if (areaMoney.Count != 0)
                {
                    double price_TL_x = pixelToPercentWidth(areaMoney[1].x) + 0.625;
                    percentXY price_TL = new percentXY(price_TL_x, EnumAuto.area_FishPrice[0].y);
                    percentXY price_BR = new percentXY(EnumAuto.area_FishPrice[1].x, EnumAuto.area_FishPrice[1].y);
                    List<percentXY> areaCheckPrice = new List<percentXY> { price_TL, price_BR };
                    //drawArea(areaCheckPrice, nameAuto);
                    Bitmap img_Price = ResizeBitmapReadText(getImageByListPercentXY(areaCheckPrice, img));
                    Bitmap img_Price_Gray = MakeWhiteBlack(img_Price, 200);

                    Point pt = new Point(1, 1);
                    Bitmap img_FloodFill = FloodFill(img_Price_Gray, pt, ColorTranslator.FromHtml("#FFFFFF"), ColorTranslator.FromHtml("#000000"));
                    Bitmap img_LocNhieu = LocNhieu(img_FloodFill);
                    //img_LocNhieu.Save($"data/img_cap/price-nhieu/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                    try
                    {
                        priceFishNotiStep = OCRNumber(img_LocNhieu);
                    }
                    catch { }
                    Debug.WriteLine(priceFishNotiStep);

                    // Câu cá thành công

                    totalFailed = 0;
                    summaryFish.Add(new SummaryFish(summaryFish.Count + 1, fish.no, priceFishNotiStep, fish.size, fish.level, shadowSize, isVM));
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        summaryTotal.tienBanCa += priceFishNotiStep;
                        wnd.setSummaryTotal(auto_id, summaryTotal.tienBanCa, 7);
                        wnd.sortChange(auto_id, summaryFishSort.typeSort, summaryFishSort.typeSelect);
                        wnd.updateTotalSummaryFish(fish.level, auto_id);
                    });
                    //img_Price_Gray.Save($"data/img_cap/price2/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                    
                } else
                {
                    priceFishNotiStep = 1;
                }
                App.Current.Dispatcher.Invoke(() =>
                {
                    wnd.setMassageNotiStep(auto_id, 1);
                    if (sizeFishNotiStep > 0)
                    {
                        if (detect_CheckSizeArea.Count > 0 && detect_ExclamationArea.Count > 0)
                        {
                            totalShowDraw++;
                            if(totalShowDraw > 2)
                            {
                                removeDraw();
                            }
                        }
                        wnd.setSummaryTotal(auto_id, ++summaryTotal.soCa, 5);
                    } else
                    {
                        wnd.setSummaryTotal(auto_id, ++summaryTotal.doTaiChe, 6);
                        summaryFish.Add(new SummaryFish(summaryFish.Count + 1, fish.no, 1, fish.size, fish.level, shadowSize));
                        wnd.sortChange(auto_id, summaryFishSort.typeSort, summaryFishSort.typeSelect);
                    }
                });
                Delay(6000);
                App.Current.Dispatcher.Invoke(() =>
                {
                    if (detect_NameArea.Count != 2 && detect_ExclamationArea.Count == 0)
                    {
                        messageNotiStep = "Quăng câu... chờ lấy toạ độ dấu chấm than, phao câu";
                    }
                    else
                    {
                        messageNotiStep = "Quăng câu... chờ bóng cá";
                    }
                    wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                });
            });
            get.Start();
            
        }

        public void loadTime()
        {
            Task get = new Task(() =>
            {
                while (isAuto)
                {
                    totalTime.second++;
                    if(totalTime.second >= 60)
                    {
                        totalTime.minute++;
                        totalTime.second = 0;

                        if(totalTime.minute >= 60)
                        {
                            totalTime.hour++;
                            totalTime.minute = 0;
                        }
                    }
                    updateTime();
                    Delay(1000);
                }
                 
            });
            get.Start();

        }

        public void updateTime()
        {
            Task get = new Task(() =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    wnd.setTime(auto_id);
                });
            });
            get.Start();

        }

        public void checkDutDay()
        {
            Task check = new Task(() =>
            {
                countCheckDutDay = 0;
                while (!checkDisplayBag())
                {
                    countCheckDutDay++;
                    if(countCheckDutDay > 8)
                    {
                        countCheckDutDay = 0;
                        break;
                    }
                    Delay(1000);
                }
            });
            check.Start();
        }

        // Lấy số lần (slot) còn lại của cần câu đang dùng
        public int getSlotCurrentRod()
        {
            FishingRodDetect currentRod = siteMapFishingRod.Single(r => r.id == currentFishingRod.id);
            return currentRod.remaining;
        }

        // Lấy mã màu hex từ 1 toạ độ (phần trăm)
        public string getColorPercentXYToHex(percentXY point, Bitmap img = null)
        {
            pointXY point_Pixel = percentToPixel(point);
            if(img == null)
            {
                img = (Bitmap)CaptureHelper.CaptureWindow(handle);
            }
            Color color = img.GetPixel(point_Pixel.x, point_Pixel.y);
            return HexConverter(color);
        }

        // Lấy Color từ 1 toạ độ (phần trăm)
        public Color getColorPercentXYToColor(percentXY point)
        {
            pointXY point_Pixel = percentToPixel(point);
            Bitmap img = (Bitmap)CaptureHelper.CaptureWindow(handle);
            Color color = img.GetPixel(point_Pixel.x, point_Pixel.y);
            return color;
        }

        // Lấy kích thước một khu vực (pixel)
        public int getTotalPixelArea(List<pointXY> area)
        {
            if(area.Count > 0)
            {
                int width = area[1].x - area[0].x;
                int height = area[1].y - area[0].y;
                return width * height;
            } else {
                return 0;
            }
        }

        // ------- Func Sử lý ảnh -------- //

        // V
        private Bitmap FloodFill(Bitmap bmp, Point pt, Color targetColor, Color replacementColor)
        {
            Stack<Point> pixels = new Stack<Point>();
            targetColor = bmp.GetPixel(pt.X, pt.Y);
            pixels.Push(pt);

            while (pixels.Count > 0)
            {
                if (!isAuto) { break; }
                Point a = pixels.Pop();
                if (a.X < bmp.Width && a.X > 0 &&
                        a.Y < bmp.Height && a.Y > 0)//make sure we stay within bounds
                {

                    if (bmp.GetPixel(a.X, a.Y) == targetColor)
                    {
                        bmp.SetPixel(a.X, a.Y, replacementColor);
                        pixels.Push(new Point(a.X - 1, a.Y));
                        pixels.Push(new Point(a.X + 1, a.Y));
                        pixels.Push(new Point(a.X, a.Y - 1));
                        pixels.Push(new Point(a.X, a.Y + 1));
                    }
                }
            }
            Bitmap res = new Bitmap(bmp.Width - 2, bmp.Height - 2);
            for (int x = 0; x < res.Width; x++)
            {
                for (int y = 0; y < res.Height; y++)
                {
                    res.SetPixel(x, y, bmp.GetPixel(x + 1, y + 1));
                }
            }
            return res;
        }

        // Lọc nhiễu số
        public Bitmap LocNhieu(Bitmap img)
        {
            try
            {
                Color white = Color.FromArgb(255, 255, 255);
                Color white2 = Color.FromArgb(255, 255, 254);
                Color black = Color.FromArgb(0, 0, 0);
                for (int x = 0; x < img.Width; x++)
                {
                    if (img.GetPixel(x, 12) == white)
                    {
                        Point pt = new Point(x, 12);
                        img = FloodFill(img, pt, white, white2);
                    }
                }
                Bitmap res = new Bitmap(img.Width, img.Height);

                for (int x = 0; x < img.Width; x++)
                {
                    for (int y = 0; y < img.Height; y++)
                    {
                        if (img.GetPixel(x, y) == white2)
                        {
                            res.SetPixel(x, y, white2);
                        }
                        else
                        {
                            res.SetPixel(x, y, black);
                        }
                    }
                }
                return res;
            } catch
            {
                return new Bitmap(img.Width, img.Height);
            }
        }

        // Lấy Bitmap 1 ảnh từ toạ độ 1 khu vực
        private Bitmap getImageByListPercentXY(List<percentXY> checkArea, Image img = null)
        {
            pointXY point_TL = percentToPixel(checkArea[0]);
            pointXY point_BR = percentToPixel(checkArea[1]);
            if(img == null)
            {
                img = CaptureHelper.CaptureWindow(handle);
            }
            try
            {
                Bitmap img_Crop = CaptureHelper.CropImage(img, new Rectangle(point_TL.x, point_TL.y, point_BR.x - point_TL.x, point_BR.y - point_TL.y));
                return img_Crop;
            } catch { return null; }
        }

        // Lấy Bitmap 1 ảnh từ toạ độ 1 khu vực pixel
        private Bitmap getImageByListPixelXY(List<pointXY> checkArea, Image img = null)
        {
            pointXY point_TL = checkArea[0];
            pointXY point_BR = checkArea[1];
            if(img == null)
            {
                img = CaptureHelper.CaptureWindow(handle);
            }
            Bitmap img_Crop = CaptureHelper.CropImage(img, new Rectangle(point_TL.x, point_TL.y, point_BR.x - point_TL.x, point_BR.y - point_TL.y));
            return img_Crop;
        }

        // Image to text
        private string OCR(Bitmap b)
        {
            string res = "";
            using (var engine = new TesseractEngine(@"tessdata", "vie", EngineMode.TesseractOnly))
            {
                using (var page = engine.Process(b, PageSegMode.SingleBlock))
                    res = page.GetText();
            }
            return res;
        }

        private string OCRRod(Bitmap b)
        {
            string res = "";
            using (var engine = new TesseractEngine(@"tessdata", "vie", EngineMode.TesseractOnly))
            {
                using (var page = engine.Process(b, PageSegMode.SingleLine))
                    res = page.GetText();
            }
            return res;
        }

        private int OCRNumber(Bitmap b)
        {
            string res = "0";
            using (var engine = new TesseractEngine(@"tessdata", "number", EngineMode.TesseractOnly))
            {
                using (var page = engine.Process(b, PageSegMode.SingleWord))
                    res = page.GetText();
            }
            return Int32.Parse(res);
        }

        // Thay đổi kích thước ảnh trước khi OCR
        public Bitmap ResizeBitmapReadText(Bitmap bmp)
        {
            double zoomRatio = (double)1280 / (double)handleWidth;
            int width = (int)((double)bmp.Width * zoomRatio);
            int height = (int)((double)bmp.Height * zoomRatio);
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }
            return result;
        }

        public static Bitmap CropImage(Image source, int x, int y, int width, int height)
        {
            Rectangle crop = new Rectangle(x, y, width, height);

            var bmp = new Bitmap(crop.Width, crop.Height);
            using (var gr = Graphics.FromImage(bmp))
            {
                gr.DrawImage(source, new Rectangle(0, 0, bmp.Width, bmp.Height), crop, GraphicsUnit.Pixel);
            }
            return bmp;
        }

        // ------- Func Tools -------- //

        public void Delay(int delay)
        {
            while(delay > 0)
            {
                Thread.Sleep(100);
                delay = delay - 100;
                if (!isAuto)
                {
                    break;
                }
            }
        }

        private static String HexConverter(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        // Vẽ viền màu cho 1 khu vực kích thước cá
        public void drawAreaSize(List<pointXY> pointList, string name, int size, string color = "#FFFFFF")
        {
            try
            {

                Application.Current.Dispatcher.Invoke((Action)delegate {
                    try
                    {
                        int width = pointList[1].x - pointList[0].x;
                        int height = pointList[1].y - pointList[0].y;
                        FishSizeBorder draw = new FishSizeBorder(pointList[0].x, pointList[0].y, width, height, name, color, size, auto_id);
                        draw.Show();
                        draw.Activate();
                    }
                    catch { }
                });
            }
            catch { }
        }

        public void removeDrawSize()
        {
            Task remove = new Task(() =>
            {
                Delay(10000);
                Application.Current.Dispatcher.Invoke((Action)delegate {
                    foreach (FishSizeBorder item in _ListDrawSize)
                    {
                        item.Close();
                    }
                });
            });
            remove.Start();
        }

        // Vẽ viền màu cho 1 khu vực đầu vào pixel
        public void drawArea(List<pointXY> pointList, string name, string color = "#FF0000")
        {
            try
            {

                Application.Current.Dispatcher.Invoke((Action)delegate {
                    try
                    {
                        int width = pointList[1].x - pointList[0].x;
                        int height = pointList[1].y - pointList[0].y;
                        SquareBorder a = new SquareBorder(pointList[0].x, pointList[0].y, width, height, name, color, auto_id);
                        a.Show();
                        a.Activate();
                    }
                    catch { }
                });
            }
            catch { }

        }

        // Vẽ viền màu cho 1 khu vực đầu vào %
        public void drawAreaPercent(List<percentXY> pointList, string name, string color = "#FF0000")
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                pointXY point0 = percentToPixel(pointList[0]);
                pointXY point1 = percentToPixel(pointList[1]);
                int width = point1.x - point0.x;
                int height = point1.y - point0.y;
                SquareBorder a = new SquareBorder(point0.x, point0.y, width, height, name, color, auto_id);
                a.Show();
            });
        }

        // Vẽ 2 viền tròn lồng nhau
        public void drawRoundBorder(pointXY pointList, int r1, int r2, string name)
        {
            try
            {
                Application.Current.Dispatcher.Invoke((Action)delegate {
                    RoundBorder a = new RoundBorder(pointList, r1, r2, name, auto_id);
                    a.Show();
                    a.Activate();
                });
            }
            catch { }

        }

        // Xoá toàn bộ đường viễn đã vẽ
        public void removeDraw()
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                foreach (SquareBorder item in _ListDraw)
                {
                    item.Close();
                }
                //foreach (RoundBorder item in _ListRounded)
                //{
                //    item.Close();
                //}
            });
        }

        // Chuyển đổ percent <--> pixel
        public percentXY pixelToPercent(pointXY pixel)
        {
            double x = (double)(pixel.x / handleWidth) * (double)100;
            double y = (double)(pixel.y / handleHeight) * (double)100;
            return new percentXY(x, y);
        }

        public double pixelToPercentWidth(int pixel)
        {
            double x = (double)pixel / (double)handleWidth * 100;
            return x;
        }

        public pointXY percentToPixel(percentXY percent)
        {
            int x = (int)((double)handleWidth / 100 * percent.x);
            int y = (int)((double)handleHeight / 100 * percent.y);
            return new pointXY(x, y);
        }

        public int percentToPixelWidth(double percent)
        {
            int pixel = (int)((double)handleWidth / 100 * percent);
            return pixel;
        }

        public int percentToPixelHeight(double percent)
        {
            int pixel = (int)((double)handleHeight / 100 * percent);
            return pixel;
        }

        // Kiểm tra 2 hình ảnh giống nhau
        public double checkImage(Bitmap img1, Bitmap img2)
        {
            float diff = 0;

            for (int y = 0; y < img1.Height; y++)
            {
                for (int x = 0; x < img1.Width; x++)
                {
                    Color pixel1 = img1.GetPixel(x, y);
                    Color pixel2 = img2.GetPixel(x, y);

                    diff += Math.Abs(pixel1.R - pixel2.R);
                    diff += Math.Abs(pixel1.G - pixel2.G);
                    diff += Math.Abs(pixel1.B - pixel2.B);
                }
            }
            double res = 100 * (diff / 255) / (img1.Width * img1.Height * 3);
            return res;
        }

        // Click vào toạ độ phần trăm X:Y
        public void controlClickPercentXY(percentXY point)
        {
            pointXY pointPixel = percentToPixel(point);
            Debug.WriteLine("emulator_type " + emulator_type);
            if(emulator_type == "memu")
            {
                IntPtr hWnd = IntPtr.Zero;
                hWnd = AutoControl.FindWindowHandle("Qt5QWindowIcon", nameAuto);
                IntPtr childhWnd = IntPtr.Zero;
                childhWnd = AutoControl.FindWindowExFromParent(hWnd, "MainWindowWindow", "Qt5QWindowIcon");
                AutoControl.SendClickOnPosition(childhWnd, pointPixel.x, pointPixel.y + 31);
            } else if(emulator_type == "ldplayer")
            {
                IntPtr hWnd = IntPtr.Zero;
                hWnd = AutoControl.FindWindowHandle("LDPlayerMainFrame", nameAuto);
                IntPtr childhWnd = IntPtr.Zero;
                childhWnd = AutoControl.FindWindowExFromParent(hWnd, "TheRender", "RenderWindow");
                AutoControl.SendClickOnPosition(childhWnd, pointPixel.x, pointPixel.y);
            } else if (emulator_type == "nox")
            {
                
                Debug.WriteLine(nameAuto);
                IntPtr hWnd = IntPtr.Zero;
                hWnd = gethWnd(nameAuto);
                Debug.WriteLine(hWnd);
                IntPtr childhWnd = IntPtr.Zero;
                List<IntPtr> listChildhWnd = AutoControl.GetChildHandle(hWnd);
                //List<IntPtr> listChildhWnd2 = AutoControl.GetChildHandle(listChildhWnd[2]);
                //Debug.WriteLine("Count: " + listChildhWnd2.Count);
                //childhWnd = AutoControl.FindWindowExFromParent(hWnd, "Nox", "Qt5QWindowIcon");
                
                //Debug.WriteLine("childhWnd" + childhWnd);
                AutoControl.SendClickOnPosition(listChildhWnd[2], pointPixel.x, pointPixel.y + 31);
            } else if(emulator_type == "bluestacks")
            {
                IntPtr hWnd = IntPtr.Zero;
                hWnd = AutoControl.FindWindowHandle("Qt5154QWindowIcon", nameAuto);
                IntPtr childhWnd = IntPtr.Zero;
                childhWnd = AutoControl.FindWindowExFromParent(hWnd, "plrNativeInputWindow", "plrNativeInputWindowClass");
                AutoControl.SendClickOnPosition(childhWnd, pointPixel.x, pointPixel.y);
            }
        }

        // Click vào toạ độ pixel X:Y
        public void controlClickPercentXY(pointXY point)
        {
            autoit.ControlClick(nameAuto, "", "", "LEFT", 1, point.x, point.y + 31);
        }

        public void controlGetPosition()
        {
            statusGetPosition = 0;
            App.Current.Dispatcher.Invoke(() =>
            {
                messageNotiStep = "Chuẩn bị lấy toạ độ thủ công...";
                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
            });
            initHandle(true);
            while (!checkDisplayBag())
            {
                Delay(50);
                if (!isAuto || !isGetPosition) { break; }
            }
            openBagRod();
            if (!isGetPosition)
            {
                return;
            }
            FishingRodConfig configRod = FishingRodConfig._ListFishingRodConfig.SingleOrDefault(r => r.auto_id == auto_id);
            if (siteMapFishingRod.Count == 0 || configRod.get_info_rod)
            {
                siteMapFishingRod = new List<FishingRodDetect>();
                getSiteMapFishingRod(true);
            }
            else
            {
                selectFishingRod(getIdRod());
            }
            while (!checkDisplayBag())
            {
                Delay(50);
                if (!isAuto || !isGetPosition) { break; }
            }
            if (!isGetPosition)
            {
                controlClickPercentXY(EnumAuto.point_CloseBag);
                return;
            }
            controlClickPercentXY(EnumAuto.point_ButtonTossSentence);
            Delay(2000);
            App.Current.Dispatcher.Invoke(() =>
            {
                messageNotiStep = "Click chuột vào phao câu để lấy toạ độ";
                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
            });
            getPositionPhao();
        }

        public void getPositionPhao()
        {
            Task setTime = new Task(() =>
            {
                while (true)
                {
                    if(!isGetPosition)
                    {
                        isAuto = false;
                        isGetPosition = false;
                        controlClickPercentXY(EnumAuto.point_ButtonSpace);
                        break;
                    }
                    Thread.Sleep(50);
                    if ((System.Windows.Forms.Control.MouseButtons & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
                    {
                        int x1 = autoit.WinGetPosX(nameAuto);
                        int y1 = autoit.WinGetPosY(nameAuto) + 31;

                        int x2 = x1 + handleWidth;
                        int y2 = y1 + handleHeight;
                        Debug.WriteLine("x1: " + x1 + " - y1: " + y1);
                        Debug.WriteLine("x2: " + x2 + " - y2: " + y2);
                        System.Drawing.Point p = System.Windows.Forms.Cursor.Position;
                        if (p.X > x1 && p.X < x2 && p.Y > y1 && p.Y < y2)
                        {
                            // lấy tâm phao
                            int buoysCenter_x = p.X - x1;
                            int buoysCenter_y = p.Y - y1;
                            pointXY buoysCenter = new pointXY(buoysCenter_x, buoysCenter_y);
                            check_BuoysAreaDone = true;

                            // Set khu vực kiểm tra kích thước cá
                            int R1 = (int)((double)handleWidth / (double)100 * (double)EnumAuto.R1);
                            int R2 = (int)((double)handleWidth / (double)100 * (double)EnumAuto.R2);
                            int checkSize_TL_x = buoysCenter_x - R2 < 0 ? 0 : buoysCenter_x - R2;
                            int checkSize_TL_y = buoysCenter_y - R2 < 0 ? 0 : buoysCenter_y - R2;
                            pointXY checkSize_TL = new pointXY(checkSize_TL_x, checkSize_TL_y);
                            int checkSize_BR_x = buoysCenter_x + R2 >= handleWidth ? handleWidth : buoysCenter_x + R2;
                            int checkSize_BR_y = buoysCenter_y + R2 >= handleHeight ? handleHeight : buoysCenter_y + R2;
                            pointXY checkSize_BR = new pointXY(checkSize_BR_x, checkSize_BR_y);
                            detect_CheckSizeArea = new List<pointXY> { checkSize_TL, checkSize_BR };
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                messageNotiStep = "Lấy toạ độ phao câu thành công";
                                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                            });

                            // vẽ tâm phao lên màn hình
                            pointXY drawBuoys_TL = new pointXY(buoysCenter_x - 4, buoysCenter_y - 4);
                            pointXY drawBuoys_BR = new pointXY(buoysCenter_x + 4, buoysCenter_y + 4);
                            List<pointXY> drawBuoys = new List<pointXY> { drawBuoys_TL, drawBuoys_BR };

                            int R1Doi = R1 / 2;
                            kv_BoQuaCheckBong[0].x = buoysCenter_x - R1Doi;
                            kv_BoQuaCheckBong[1].x = buoysCenter_x + R1Doi;
                            kv_BoQuaCheckBong[0].y = buoysCenter_y - R1Doi;
                            kv_BoQuaCheckBong[1].y = buoysCenter_y + R1Doi;

                            if (kv_BoQuaCheckBong.Count > 0)
                            {
                                pointXY point1 = new pointXY(kv_BoQuaCheckBong[0].x - 20, kv_BoQuaCheckBong[0].y - 20);
                                pointXY point2 = new pointXY(kv_BoQuaCheckBong[1].x + 20, kv_BoQuaCheckBong[0].y - 20);
                                pointXY point3 = new pointXY(kv_BoQuaCheckBong[0].x - 20, kv_BoQuaCheckBong[1].y + 20);
                                pointXY point4 = new pointXY(kv_BoQuaCheckBong[1].x + 20, kv_BoQuaCheckBong[1].y + 20);
                                list_PointWeather = new List<pointXY> { point1, point2, point3, point4 };
                            }
                            drawArea(drawBuoys, nameAuto, "#FFFFFF");
                            //drawRoundBorder(buoysCenter, R1, R2, nameAuto);
                            break;
                        }
                    }
                }
                Delay(2000);
                getPositionChamThan();
            });
            //Thread.Sleep(1000);
            setTime.Start();
            
            //List<pointXY> areaCheck = new List<pointXY> { new pointXY(x1Win, y1Win), new pointXY(x2, y2) };
            //drawArea(areaCheck, nameAuto, "#FFFFFF");
        }

        public void getPositionChamThan()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (!isGetPosition)
                {
                    messageNotiStep = "Đã dừng lấy toạ độ thủ công";
                } else
                {
                    messageNotiStep = "Click vào dấu chấm than khi cá cắn câu để lấy toạ độ";
                }
                wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
            });
            Task getPosition = new Task(() =>
            {
                while (true)
                {
                    if (!isGetPosition)
                    {
                        isAuto = false;
                        isGetPosition = false;
                        controlClickPercentXY(EnumAuto.point_ButtonSpace);
                        break;
                    }
                    Thread.Sleep(50);
                    if ((System.Windows.Forms.Control.MouseButtons & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
                    {
                        int x1 = autoit.WinGetPosX(nameAuto);
                        int y1 = autoit.WinGetPosY(nameAuto) + 31;

                        int x2 = x1 + handleWidth;
                        int y2 = y1 + handleHeight;
                        System.Drawing.Point p = System.Windows.Forms.Cursor.Position;
                        if (p.X > x1 && p.X < x2 && p.Y > y1 && p.Y < y2)
                        {
                            
                            Bitmap img_Check = (Bitmap)CaptureHelper.CaptureWindow(handle);
                            // dut can
                            //Delay(100);
                            //controlClickPercentXY(EnumAuto.point_ButtonSpace);
                            //img_Check.Save($"data/img_cap/check-cham-than/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                            Color colorChamThan = img_Check.GetPixel(p.X - x1, p.Y - y1);
                            Debug.WriteLine(HexConverter(colorChamThan));

                            List<pointXY> areaRes = getColorRelativeArea(img_Check, HexConverter(colorChamThan), 10);

                            // Công thêm để tránh 100%
                            int congThem = 4;
                            areaRes[0].x -= 6;
                            areaRes[0].y -= congThem + 6;
                            areaRes[1].x += 6;
                            areaRes[1].y += congThem + 8;

                            double maxSizeAreaName = (double)(handleHeight * handleWidth) / (double)100 * (double)10;
                            Debug.WriteLine("maxSizeAreaName: " + maxSizeAreaName);
                            Debug.WriteLine("areaRes: " + getTotalPixelArea(areaRes));
                            if ((double)getTotalPixelArea(areaRes) < maxSizeAreaName)
                            {
                                areaRes[1].y = (int)((double)areaRes[0].y + (double)(areaRes[1].y - areaRes[0].y) / (double)2);
                                detect_ExclamationArea = areaRes;

                                int cropWidth = areaRes[1].x - areaRes[0].x;
                                int cropHeight = areaRes[1].y - areaRes[0].y;

                                Bitmap cropOKCheck = CropImage(img_Check, areaRes[0].x, areaRes[0].y, cropWidth, cropHeight);
                                // kiểm tra trùng màu cá
                                int center_x = cropOKCheck.Width / 2;
                                int center_y = cropOKCheck.Height - 1;
                                Color pixelCenter = cropOKCheck.GetPixel(center_x, center_y);
                                OKImgCheck = MakeGrayW(cropOKCheck, pixelCenter);
                                //OKImgCheck.Save($"data/img_cap/cham-than/{DateTime.Now.Subtract(DateTime.MinValue.AddYears(2021)).TotalMilliseconds}.png");
                                useSensor = false;
                                isAuto = false;
                                statusGetPosition = 1;
                                isGetPosition = false;

                                App.Current.Dispatcher.Invoke(() =>
                                {
                                    foreach (SquareBorder item in _ListDraw)
                                    {
                                        item.Activate();
                                    }
                                    messageNotiStep = "Nhận diện toạ độ dấu chấm than thành công";
                                    wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                                    wnd.updateStatusGetPosition(1);
                                });
                                drawArea(detect_ExclamationArea, nameAuto, "#86EFAC");
                            } else
                            {
                                isAuto = false;
                                statusGetPosition = 2;
                                isGetPosition = false;
                                App.Current.Dispatcher.Invoke(() =>
                                {
                                    messageNotiStep = "Nhận diện toạ độ dấu chấm than thất bại";
                                    removeDraw();
                                    wnd.setMassageNotiStep(auto_id, 0, messageNotiStep);
                                    wnd.updateStatusGetPosition(2);
                                });
                            }
                            storeCheckPosition();
                            break;
                        }
                    }
                }
            });
            //Thread.Sleep(1000);
            getPosition.Start();
        }

        public void storeCheckPosition()
        {
            Task storeFish = new Task(() => {
                int timer = 0;
                while (true && timer < 50)
                {
                    if (checkDoneFishing())
                    {
                        controlClickPercentXY(EnumAuto.point_ButtonStore);
                        break;
                    }
                    timer++;
                    Delay(100);
                }
            });
            storeFish.Start();
        }

        // So sánh 2 màu cho phép 1 độ lệch (difference) nhất định
        public bool colorComparison(Color color1, Color color2, int difference = 5)
        {
            int difference_R = Math.Abs(color1.R - color2.R);
            int difference_G = Math.Abs(color1.G - color2.G);
            int difference_B = Math.Abs(color1.B - color2.B);
            if(difference_R <= difference && difference_G <= difference && difference_B <= difference)
            {
                return true;
            } else
            {
                return false;
            }
        }

        // So sánh 1 màu với 1 list màu cho phép 1 độ lệch (difference) nhất định
        public bool colorComparisonList(Color color1, List<Color> listColor, int difference = 5)
        {
            foreach(Color color in listColor)
            {
                if(colorComparison(color1, color, difference))
                {
                    return true;
                }
            }
            return false;
        }

        // So sánh 1 màu với 1 list màu cho phép 1 độ lệch (difference) nhất định
        public bool colorComparisonColorFish(List<Color> listColor)
        {
            foreach (Color color in listColor)
            {
                
                int a = color.ToArgb();
                Debug.WriteLine(HexConverter(color) + ": " + a);
                //if (colorComparison(color1, color, difference))
                //{
                //    return true;
                //}
            }
            return false;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Lấy màu chiếm nhiều nhất trong 1 bitmap
        public Color getColorMaxInBitmap(Bitmap img_Check)
        {
            int colorMax = 0;
            string hexColorMax = "";
            List<string> allColorPixel1 = new List<string>();
            List<string> colorPixelUnique1 = new List<string>();
            for (int x = 0; x < img_Check.Width; x++)
            {
                for (int y = 0; y < img_Check.Height; y++)
                {
                    Color pixelColor = img_Check.GetPixel(x, y);
                    string hexColor = HexConverter(pixelColor);
                    allColorPixel1.Add(hexColor);
                    if (!colorPixelUnique1.Contains(hexColor))
                    {
                        colorPixelUnique1.Add(hexColor);
                    }
                }
            }
            foreach (string color in colorPixelUnique1)
            {
                int count = allColorPixel1.Where(x => x.Equals(color)).Count();
                if (count > colorMax)
                {
                    colorMax = count;
                    hexColorMax = color;
                }
            }
            Color res = ColorTranslator.FromHtml(hexColorMax);
            return res;
        }

        // Lấy danh sách màu chiếm nhiều hơn n pixel trong 1 bitmap
        public List<Color> getListColorMaxInBitmap(Bitmap img_Check, int bigger)
        {
            List<string> allColorPixel1 = new List<string>();
            List<string> colorPixelUnique1 = new List<string>();
            List<Color> listRes = new List<Color>();
            for (int x = 0; x < img_Check.Width; x++)
            {
                for (int y = 0; y < img_Check.Height; y++)
                {
                    Color pixelColor = img_Check.GetPixel(x, y);
                    string hexColor = HexConverter(pixelColor);
                    allColorPixel1.Add(hexColor);
                    if (!colorPixelUnique1.Contains(hexColor))
                    {
                        colorPixelUnique1.Add(hexColor);
                    }
                }
            }
            foreach (string color in colorPixelUnique1)
            {
                int count = allColorPixel1.Where(x => x.Equals(color)).Count();
                if (count > bigger)
                {
                    listRes.Add(ColorTranslator.FromHtml(color));
                }
            }
            return listRes;
        }

        // Kiểm tra xem 1 bitmap có lớn hơn n % màn hình    double maxSizeAreaName = (double)(handleHeight * handleWidth) / (double)100 * (double)10;
        public bool checkPercentBitmapCompareScreen(List<pointXY> area, int percentCheck)
        {
            if(area.Count != 0)
            {
                int areaWidth = area[1].x - area[0].x;
                int areaHeight = area[1].y - area[0].y;
                double areaScreen = (double)(handleHeight * handleWidth);
                double areaBitmap = (double)(areaWidth * areaHeight);
                double percent = areaBitmap / areaScreen * (double)100;
                return (double)percentCheck > percent;
            } else
            {
                return false;
            }
        }
    }
}
