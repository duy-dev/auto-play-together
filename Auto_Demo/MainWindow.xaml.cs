using Auto_Demo.popups;
using Auto_Demo.test;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Thread = System.Threading.Thread;
using KAutoHelper;
using System.Collections.ObjectModel;
using Auto_Demo.services;
using System.Data;
using System.ComponentModel;
using Auto_Demo.components;
using System.Windows.Forms;


using Label = System.Windows.Controls.Label;
using TextBox = System.Windows.Controls.TextBox;
using Cursors = System.Windows.Input.Cursors;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Auto_Demo.services.config;
using CheckBox = System.Windows.Controls.CheckBox;
using System.Text.RegularExpressions;
using Auto_Demo.services.ml;
using System.Runtime.InteropServices;

namespace Auto_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 



    

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        database db = new database();

        private bool isDashboard;
        public bool IsDashboard
        {   
            get { return isDashboard; }
            set
            {
                isDashboard = value;
                OnPropertyChanged("IsDashboard");
            }
        }

        private bool isEditNameEmu;
        public bool IsEditNameEmu
        {
            get { return isEditNameEmu; }
            set
            {
                isEditNameEmu = value;
                OnPropertyChanged("IsEditNameEmu");
            }
        }

        private emulator config;
        public emulator Config
        {
            get { return config; }
            set
            {
                config = value;
                OnPropertyChanged("Config");
            }
        }

        private FishingRodConfig configFishingRod;
        public FishingRodConfig ConfigFishingRod
        {
            get { return configFishingRod; }
            set
            {
                configFishingRod = value;
                OnPropertyChanged("ConfigFishingRod");
            }
        }

        List<fastConfigFishSize> listFastFishSize = new List<fastConfigFishSize>();
        List<languageConfig> listLanguage = new List<languageConfig>();
        List<itemSelectLevel> listLevel = new List<itemSelectLevel>();
        List<itemSelectSize> listSize = new List<itemSelectSize>();


        private ConfigFishSize configFish;
        public ConfigFishSize ConfigFish
        {
            get { return configFish; }
            set
            {
                configFish = value;
                OnPropertyChanged("ConfigFish");
            }
        }

        private ConfigAuto configAutoValue;
        public ConfigAuto ConfigAutoValue
        {
            get { return configAutoValue; }
            set
            {
                configAutoValue = value;
                OnPropertyChanged("ConfigAutoValue");
            }
        }

        private Auto autoValue;
        public Auto AutoValue
        {
            get { return autoValue; }
            set
            {
                autoValue = value;
                OnPropertyChanged("AutoValue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

        public int minuteGame { get; set; }
        public int hourGame { get; set; }
        public string sessionGame { get; set; }
        public int totalMinuteGame { get; set; }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(ref Point lpPoint);

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            img_logo.Source = new BitmapImage(new Uri("pack://application:,,,/images/logo.png"));

            languageConfig.initLanguageList();
            itemSelectLevel.initLevelList();
            itemSelectSize.initSizeList();
            loadListAuto();
            loadConfigAuto();
            EnumAuto.initListFishingRod();
            Fish.initListFish();
            getTimeGame();
            clearCache();
        }

        public void clearCache()
        {
            Task clear = new Task(() =>
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    GC.Collect();
                }
            });
            clear.Start();
        }

        private void Window_MouseMove(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                try
                {
                    this.DragMove();
                } catch { };
            }
        }

        private void hover_Close(object sender, MouseEventArgs e)
        {
            icon_ClosePrimary1.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#EF4444");
            icon_ClosePrimary2.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#EF4444");
            icon_CloseSecondary.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#EF4444");

            icon_ClosePrimaryGroup.Opacity = 1;
            icon_CloseSecondary.Opacity = 0.5;
        }

        private void hover_CloseOut(object sender, MouseEventArgs e)
        {
            icon_ClosePrimary1.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#EF4444");
            icon_ClosePrimary2.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#EF4444");
            icon_CloseSecondary.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFFFF");

            icon_ClosePrimaryGroup.Opacity = 0.7;
            icon_CloseSecondary.Opacity = 1;
        }

        private void close_App(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(200);
            System.Windows.Application.Current.Shutdown();
            Close();
        }

        private void hover_Minimize(object sender, MouseEventArgs e)
        {
            icon_MinimizePrimary.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#FCD34D");
            icon_MinimizeSecondary.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#FCD34D");

            icon_MinimizePrimary.Opacity = 1;
            icon_MinimizeSecondary.Opacity = 0.5;
        }

        private void hover_MinimizeOut(object sender, MouseEventArgs e)
        {
            icon_MinimizePrimary.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#FCD34D");
            icon_MinimizeSecondary.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFFFF");

            icon_MinimizePrimary.Opacity = 0.7;
            icon_MinimizeSecondary.Opacity = 1;
        }
        private void minimize_App(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(200);
            this.WindowState = WindowState.Minimized;
        }

        // Valid

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Add Device

        private void open_PopupAddDevice(object sender, RoutedEventArgs e)
        {
            IsEditNameEmu = false;
            add_device info = new add_device();
            info.Show();
        }

        private void addNoti(object sender, RoutedEventArgs e)
        {
            new Notification() { id = Guid.NewGuid().ToString(), title = "Không tìm thấy thiết bị", content = "vui lòng mở giả lập hoặc kết nối với điện thoai", type = "success", timeStrat = DateTime.Now };
        }

        public void updateNoti()
        {
            ic_Notification.ItemsSource = Notification._NotiList;
        }

        private void loadListAuto()
        {
            new fastConfigFishSize("custom", "Tuỳ chỉnh");
            new fastConfigFishSize("default", "Mặc định");
            new fastConfigFishSize("auto", "Tự động");

            FishingRod.CreateListFishingRod();
            cb_ListFishingRod.ItemsSource = FishingRod._ListFishingRod;
            cb_ListFishingRod2.ItemsSource = FishingRod._ListFishingRod;

            ImageToText.initNameFishing();

            // Load thông tin thiết bị
            var data = db.getDataTable("Auto");
            foreach (DataRow row in data.Rows)
            {
                string _id = row["_id"].ToString();
                string name = row["name"].ToString();
                string emulator_id = row["emulator_id"].ToString();
                string emulator_name = row["emulator_name"].ToString();
                string emulator_type = row["emulator_type"].ToString();
                //string className = row["class"].ToString();
                //string text = row["text"].ToString();
                new emulator(_id, name, emulator_id, emulator_name, emulator_type);
                new fastConfigFishSize(_id, name);
            }

            // Load cài đặt bỏ bóng cá cho thiết bị
            foreach (emulator emu in emulator._EmulatorList)
            {
                List<FishSize> listSize = new List<FishSize>();
                var dataFishSize = db.getDataFishSize(emu._id);
                //string modeSetting = "custom";
                string modeSetting = dataFishSize.Rows[0]["setting"].ToString();

                foreach (DataRow rowFishSize in dataFishSize.Rows)
                {
                    modeSetting = rowFishSize["setting"].ToString();
                    string _id_FishSize = rowFishSize["_id"].ToString();
                    int size_FishSize = int.Parse(rowFishSize["size"].ToString());
                    int min_FishSize = int.Parse(rowFishSize["min"].ToString());
                    int max_FishSize = int.Parse(rowFishSize["max"].ToString());
                    int status_FishSize = int.Parse(rowFishSize["status"].ToString());
                    string auto_id_FishSize = rowFishSize["auto_id"].ToString();
                    bool disable_FishSize = true;
                    if (modeSetting == "auto")
                    {
                        disable_FishSize = false;
                    }
                    FishSize fishSize = new FishSize(_id_FishSize, size_FishSize, min_FishSize, max_FishSize, status_FishSize, auto_id_FishSize, disable_FishSize);
                    listSize.Add(fishSize);
                }
                if (modeSetting == "auto")
                {
                    new ConfigFishSize(listSize, emu._id, modeSetting, 0);
                } else
                {
                    new ConfigFishSize(listSize, emu._id, modeSetting, -1);
                }
                    
                if (modeSetting.Length > 10)
                {
                    ConfigFishSize.changeModeFS(emu._id, modeSetting);
                }
            }

            // Load cài đặt cần câu cho thiết bị
            foreach (emulator emu in emulator._EmulatorList)
            {
                var dataFishingRod = db.getDataFishingRodByIdAuto(emu._id);
                foreach (DataRow rowFishingRod in dataFishingRod.Rows)
                {
                    string _id_FishingRod = rowFishingRod["_id"].ToString();
                    string auto_id_FishingRod = rowFishingRod["auto_id"].ToString();
                    int fishing_rod_1_FishingRod = int.Parse(rowFishingRod["fishing_rod_1"].ToString());
                    int fishing_rod_2_FishingRod = int.Parse(rowFishingRod["fishing_rod_2"].ToString());
                    int change_fishing_rod_FishingRod = int.Parse(rowFishingRod["change_fishing_rod"].ToString());
                    int step_change_FishingRod = int.Parse(rowFishingRod["step_change"].ToString());
                    int get_info_rod_FishingRod = int.Parse(rowFishingRod["get_info_rod"].ToString());
                    new FishingRodConfig(_id_FishingRod, auto_id_FishingRod, fishing_rod_1_FishingRod, fishing_rod_2_FishingRod, change_fishing_rod_FishingRod, step_change_FishingRod, get_info_rod_FishingRod);
                }
            }

            // Load cài đặt auto cho thiết bị
            foreach (emulator emu in emulator._EmulatorList)
            {
                var dataConfigAuto = db.getDataConfigAutoByIdAuto(emu._id);
                foreach (DataRow rowConfigAuto in dataConfigAuto.Rows)
                {
                    string _id_ConfigAuto = rowConfigAuto["_id"].ToString();
                    string auto_id_ConfigAuto = rowConfigAuto["auto_id"].ToString();
                    int delay_fishing_test_ConfigAuto = int.Parse(rowConfigAuto["delay_fishing_test"].ToString());
                    int speed_ConfigAuto = int.Parse(rowConfigAuto["speed"].ToString());
                    int save_exclamation_ConfigAuto = int.Parse(rowConfigAuto["save_exclamation"].ToString());
                    int delay_check_ConfigAuto = int.Parse(rowConfigAuto["delay_check"].ToString());
                    new ConfigAuto(_id_ConfigAuto, auto_id_ConfigAuto, delay_fishing_test_ConfigAuto, -1, speed_ConfigAuto, save_exclamation_ConfigAuto, delay_check_ConfigAuto);
                }
            }

            // Load cài đặt task auto
            foreach (emulator emu in emulator._EmulatorList)
            {
                new Auto(emu._id, emu.emulator_type, emu.emulator_name);
            }
        }
        
        // Cập nhật UI khi tạo mới
        public void updateListEmulator(string idNewEmu = "")
        {
            ic_Auto.ItemsSource = emulator._EmulatorList;
            ic_Auto.Items.Refresh();
            if (idNewEmu.Length > 0)
            {
                Config = emulator.getEmulatorById(idNewEmu);
            }
        }

        public void updateListFishSize(string idAuto)
        {
            if(Config._id == idAuto)
            {
                ConfigFish = ConfigFishSize.getFishSizeByIdAuto(idAuto);
                ic_FishSize.ItemsSource = ConfigFish.listSize;
                ic_FishSize.Items.Refresh();
            }
        }

        public void updateListFastFishSize()
        {
            listFastFishSize = fastConfigFishSize._FastConfigFishSizeList;
            cb_ListFastFS.ItemsSource = listFastFishSize;
            cb_ListFastFS.Items.Refresh();
            cb_ListFastFS.SelectedIndex = 0;
        }

        public void updateConfigFishingRod(string idAuto)
        {
            ConfigFishingRod = FishingRodConfig.getFishingRodByIdAuto(idAuto);
            cb_ListFishingRod.SelectedIndex = ConfigFishingRod.fishing_rod_1 - 1;
            cb_ListFishingRod2.SelectedIndex = ConfigFishingRod.fishing_rod_2 - 1;
            cb_isCheckedChangeFishingRod.IsChecked = ConfigFishingRod.change_fishing_rod;
            tb_stepChangeFishingRod.Text = ConfigFishingRod.step_change.ToString();
            cb_isReGetInfoRod.IsChecked = ConfigFishingRod.get_info_rod;
        }

        public void updateConfigAuto(string idAuto)
        {
            ConfigAutoValue = ConfigAuto._ListConfigAuto.Single(r => r.auto_id == idAuto);
            tb_DelayFishingTest.Text = ConfigAutoValue.delay_fishing_test.ToString();
            cb_isSaveExclamation.IsChecked = ConfigAutoValue.save_exclamation == 1 ? true : false;
            tb_ValueSpeedName.Text = ConfigAutoValue.speed.ToString();
            slider_SpeedAuto.Value = ConfigAutoValue.speed;
            tb_DelayCheck.Text = ConfigAutoValue.delay_check.ToString();
        }

        public void updateAuto(string idAuto)
        {
            AutoValue = Auto._ListAuto.Single(r => r.auto_id == idAuto);
        }

        // Init danh sách ngôn ngữ
        public void updateListLanguage(int index)
        {
            listLanguage = languageConfig._LanguageList;
            cb_ListLanguage.ItemsSource = listLanguage;
            cb_ListLanguage.Items.Refresh();
            cb_ListLanguage.SelectedIndex = index;
        }

        // Thay đổi ngôn ngữ (tự nhận diện)
        public void updateLanguage(int index)
        {
            cb_ListLanguage.SelectedIndex = index;
        }

        // Hover thiết bị trong danh sách thiết bị
        private void hover_Emulator(object sender, MouseEventArgs e)
        {
            var dataHover = (sender as FrameworkElement).DataContext;
            Border bor = sender as Border;
            if (dataHover.ToString() != "{DisconnectedItem}")
            {
                var property_IsActive = dataHover.GetType().GetProperty("isActive");
                bool isActive = (bool)property_IsActive.GetValue(dataHover, null);
                if (!isActive)
                {
                    bor.Cursor = Cursors.Hand;
                }
            }

            bor.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF1F1E25");

            Grid grid = bor.Child as Grid;
            grid.Children[0].Visibility = Visibility.Visible;
            grid.Children[1].Visibility = Visibility.Visible;
            grid.Children[4].Visibility = Visibility.Visible;
            grid.Children[5].Visibility = Visibility.Visible;

            TextBlock name = grid.Children[3] as TextBlock;
            name.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFFFF");
        }

        private void hover_EmulatorOut(object sender, MouseEventArgs e)
        {
            var dataHover = (sender as FrameworkElement).DataContext;
            
            if (dataHover.ToString() != "{DisconnectedItem}")
            {
                var property_IsActive = dataHover.GetType().GetProperty("isActive");
                bool isActive = (bool)property_IsActive.GetValue(dataHover, null);
                if (!isActive)
                {
                    var property_Id = dataHover.GetType().GetProperty("_id");
                    var value = property_Id.GetValue(dataHover, null);

                    Border bor = sender as Border;
                    bor.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF0A060E");
                    bor.Cursor = Cursors.Arrow;

                    Grid grid = bor.Child as Grid;
                    grid.Children[0].Visibility = Visibility.Hidden;
                    grid.Children[1].Visibility = Visibility.Hidden;
                    grid.Children[4].Visibility = Visibility.Hidden;
                    grid.Children[5].Visibility = Visibility.Hidden;

                    TextBlock name = grid.Children[3] as TextBlock;
                    name.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF7D7D85");
                }
            }
        }

        // Chọn một thiết bị
        private void selectAuto(object sender, MouseButtonEventArgs e)
        {
            var dataHover = (sender as FrameworkElement).DataContext;
            var property_IsActive = dataHover.GetType().GetProperty("isActive");
            bool isActive = (bool)property_IsActive.GetValue(dataHover, null);
            if (!isActive && !TextboxIsBeingChanged)
            {
                // Active UI cho thiết bị được chọn
                var property_Id = dataHover.GetType().GetProperty("_id");
                string _id = property_Id.GetValue(dataHover, null).ToString();
                emulator.changeActive(_id);
                Config = emulator.getEmulatorById(_id);

                // Load UI cài đặt bỏ bóng cá
                ConfigFish = ConfigFishSize.getFishSizeByIdAuto(_id);
                cb_ListFastFS.SelectedIndex = fastConfigFishSize.getConfigMode(ConfigFish.mode_setting);
                ic_FishSize.ItemsSource = ConfigFish.listSize;
                ic_FishSize.Items.Refresh();

                // Load UI cài đặt cần câu
                ConfigFishingRod = FishingRodConfig.getFishingRodByIdAuto(_id);
                cb_ListFishingRod.SelectedIndex = ConfigFishingRod.fishing_rod_1 - 1;
                cb_ListFishingRod2.SelectedIndex = ConfigFishingRod.fishing_rod_2 - 1;
                cb_isCheckedChangeFishingRod.IsChecked = ConfigFishingRod.change_fishing_rod;
                tb_stepChangeFishingRod.Text = ConfigFishingRod.step_change.ToString();
                cb_isReGetInfoRod.IsChecked = ConfigFishingRod.get_info_rod;

                // Load UI cài đặt Auto
                ConfigAutoValue = ConfigAuto._ListConfigAuto.Single(r => r.auto_id == _id);
                tb_DelayFishingTest.Text = ConfigAutoValue.delay_fishing_test.ToString();
                cb_isSaveExclamation.IsChecked = ConfigAutoValue.save_exclamation == 1 ? true : false;
                tb_ValueSpeedName.Text = ConfigAutoValue.speed.ToString();
                slider_SpeedAuto.Value = ConfigAutoValue.speed;
                tb_DelayCheck.Text = ConfigAutoValue.delay_check.ToString();

                // Load UI Auto
                AutoValue = Auto._ListAuto.Single(r => r.auto_id == _id);
                ic_Play.Visibility = AutoValue.isAuto ? Visibility.Hidden : Visibility.Visible;
                ic_Pause.Visibility = AutoValue.isAuto ? Visibility.Visible : Visibility.Hidden;
                setMassageNotiStep(Config._id, 0, AutoValue.messageNotiStep);
                setSummaryTotalCurent(Config._id);
                setTime(Config._id);
                ic_SummaryFish.ItemsSource = AutoValue.summaryFish;
                ic_SummaryFish.Items.Refresh();
                sortChange(Config._id, AutoValue.summaryFishSort.typeSort, AutoValue.summaryFishSort.typeSelect);

                // Load UI ngôn ngữ trong game
                cb_ListLanguage.SelectedIndex = ConfigAutoValue.language;

                IsEditNameEmu = false;
                IsDashboard = false;

                Debug.WriteLine("statusGetPosition :" + AutoValue.statusGetPosition);
                updateStatusGetPosition(AutoValue.statusGetPosition);

                // load tổng số cá theo lever
                initTotalSummaryFish();
            }
        }

        private void loadConfigAuto()
        {
            IsDashboard = true;
            IsEditNameEmu = false;
        }

        private void startAuto(object sender, MouseButtonEventArgs e)
        {
            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.isAuto = true);
            AutoValue = Auto._ListAuto.Single(r => r.auto_id == Config._id);
            emulator._EmulatorList.Where(emu => emu._id == Config._id).ToList().ForEach(s => s.isAuto = true);
            Config = emulator.getEmulatorById(Config._id);
            ic_Auto.ItemsSource = emulator._EmulatorList;
            ic_Auto.Items.Refresh();
            ic_Play.Visibility = Visibility.Hidden;
            ic_Pause.Visibility = Visibility.Visible;
            AutoValue.newTaskAuto().Start();
            //AutoValue.getColor();
            //AutoValue.getSiteMapFishingRod();
        }

        private void stopAuto(object sender, MouseButtonEventArgs e)
        {
            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.isAuto = false);
            AutoValue = Auto._ListAuto.Single(r => r.auto_id == Config._id);
            emulator._EmulatorList.Where(emu => emu._id == Config._id).ToList().ForEach(s => s.isAuto = false);
            Config = emulator.getEmulatorById(Config._id);
            ic_Auto.ItemsSource = emulator._EmulatorList;
            ic_Auto.Items.Refresh();
            ic_Play.Visibility = Visibility.Visible;
            ic_Pause.Visibility = Visibility.Hidden;

            AutoValue.stopAuto();
            AutoValue.removeDraw();
        }

        private void showTabSetting(object sender, MouseButtonEventArgs e)
        {
            emulator._EmulatorList.Where(emu => emu._id == Config._id).ToList().ForEach(s => s.tabSelect = 0);
            Config = emulator.getEmulatorById(Config._id);
        }

        private void showTabSummary(object sender, MouseButtonEventArgs e)
        {
            emulator._EmulatorList.Where(emu => emu._id == Config._id).ToList().ForEach(s => s.tabSelect = 1);
            Config = emulator.getEmulatorById(Config._id);
        }

        private void showDashboard(object sender, MouseButtonEventArgs e)
        {
            IsDashboard = true;
            emulator.removeAllActive();
        }

        private void change_NameEmu(object sender, MouseButtonEventArgs e)
        {
            IsEditNameEmu = true;
        }

        private void saveChangeNameEmu(object sender, MouseButtonEventArgs e)
        {
            Label inputName = sender as Label;
            string newName = ((inputName.Parent as Grid).Children[9] as TextBox).Text;
            if(Config.name != newName)
            {
                string txtQuery = $"UPDATE Auto SET name='{newName}' WHERE _id='{Config._id}'";
                try
                {
                    db.ExecuteQuery(txtQuery);
                    IsEditNameEmu = false;
                    new Notification() { id = Guid.NewGuid().ToString(), title = "Chỉnh sửa tên thành công", content = "Tên phân biệt của thiết bị đã được thay đổi", type = "success", timeStrat = DateTime.Now };
                }
                catch
                {
                    new Notification() { id = Guid.NewGuid().ToString(), title = "Chỉnh sửa tên thất bại", content = "Đã có một lỗi dữ liệu xảy ra khi thay đổi tên phân biệt của thiết bị!", type = "error", timeStrat = DateTime.Now };
                }
            } else
            {
                IsEditNameEmu = false;
            }
        }

        // Xoá thiết bị

        private void click_RemoveEmu(object sender, MouseButtonEventArgs e)
        {
            DialogResult result = CMessageBox.Show("Xác nhận xoá thiết bị", "Bạn có muốn xoá thiết bị này không, việc này sẽ xoá toàn bộ cấu hình auto của thiết bị", "Xoá", "Đóng");
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                ConfigFishSize.removeConfigOneEmu(Config._id);
                FishingRodConfig.removeConfigFishingRod(Config._id);
                ConfigAuto.removeConfigAutoOneEmu(Config._id);
                string txtQuery = $"DELETE FROM Auto WHERE _id='{Config._id}'";
                try
                {
                    db.ExecuteQuery(txtQuery);
                    IsDashboard = true;
                    emulator.removeEmulatorById(Config._id);
                    new Notification() { id = Guid.NewGuid().ToString(), title = "Xoá thiết bị thành công", content = "Thiết bị cùng với các cấu hình auto của thiết bị đã được xoá bỏ", type = "success", timeStrat = DateTime.Now };
                }
                catch
                {
                    new Notification() { id = Guid.NewGuid().ToString(), title = "Xoá thiết bị thất bại", content = "Đã có một lỗi dữ liệu xảy ra khi xoá thiết bị!", type = "error", timeStrat = DateTime.Now };
                }
            }
        }

        private void toggle_ListSelectDevice(object sender, MouseButtonEventArgs e)
        {

        }

        private void CheckBoxFSChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = (sender as CheckBox);
            bool isChecked = checkbox.IsChecked.Value;
            var property_size = checkbox.DataContext.GetType().GetProperty("size");
            int size = (int)property_size.GetValue(checkbox.DataContext, null);
            ConfigFishSize.changeCheckboxActive(emulator.getEmulatorIdActive(), size, isChecked);
        }

        private void changeMaxSF(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (sender as TextBox);
            var property_max = textbox.DataContext.GetType().GetProperty("max");
            int max = (int)property_max.GetValue(textbox.DataContext, null);
            var property_size = textbox.DataContext.GetType().GetProperty("size");
            int size = (int)property_size.GetValue(textbox.DataContext, null);
            string id = ConfigFish.listSize[size - 1]._id;
            //var select = cb_ListFastFS.SelectedItem.GetType().GetProperty("label");
            //string selectValue = (string)select.GetValue(cb_ListFastFS.SelectedItem, null);
            //Debug.WriteLine(selectValue);


            if (checkNewMaxFS(size, max) && textbox.Text != "")
            {
                if (size == 6)
                {
                    ConfigFishSize.changeMinMax(id, max);
                } else
                {
                    string nextId = ConfigFish.listSize[size]._id;
                    ConfigFishSize.changeMinMax(id, max);
                    ConfigFishSize.changeMinMax(nextId, max + 1, "min");
                }
            } else
            {
                int currentMax = currentFocusTextboxFS;
                textbox.Text = currentMax.ToString();
                new Notification() { id = Guid.NewGuid().ToString(), title = "Lỗi cài đặt bỏ bóng cá", content = "Giá trị cài đặt không hợp lệ, vui lòng kiểm tra lại", type = "error", timeStrat = DateTime.Now };
            }
            TextboxIsBeingChanged = false;
        }

        private int currentFocusTextboxFS = 0;
        private bool TextboxIsBeingChanged = false;

        private bool checkNewMaxFS(int size, int newMax)
        {
            int start = 1;
            int end = 99999999;
            if (size == 1)
            {
                int nextMax = ConfigFish.listSize[size].max;
                end = nextMax - 1;
            }
            else if (size == 6)
            {
                int prevMax = ConfigFish.listSize[size - 2].max;
                start = prevMax + 1;
            }
            else
            {
                int prevMax = ConfigFish.listSize[size - 2].max;
                int nextMax = ConfigFish.listSize[size].max;
                start = prevMax + 1;
                end = nextMax - 1;
            }

            if (newMax > start && newMax < end)
            {
                return true;
            } else
            {
                return false;
            }
        }

        private void startChangeMaxSF(object sender, RoutedEventArgs e)
        {
            TextboxIsBeingChanged = true;
            TextBox textbox = (sender as TextBox);
            var property_max = textbox.DataContext.GetType().GetProperty("max");
            int max = (int)property_max.GetValue(textbox.DataContext, null);
            currentFocusTextboxFS = max;
        }

        private void changeModeFS(object sender, SelectionChangedEventArgs e)
        {
            fastConfigFishSize mode = cb_ListFastFS.SelectedItem as fastConfigFishSize;
            if(ConfigFish != null)
            {
                if (ConfigFish.mode_setting != mode.value)
                {
                    ConfigFishSize.changeModeFS(ConfigFish.auto_id, mode.value);
                    //if(mode.value == "auto")
                    //{
                    //    zz
                    //}
                }
            }
        }

        private void changeLanguage(object sender, SelectionChangedEventArgs e)
        {
            languageConfig language = cb_ListLanguage.SelectedItem as languageConfig;
            if (ConfigAutoValue != null && language != null)
            {
                if (ConfigAutoValue.language != language.value)
                {
                    ConfigAuto.changeLanguage(language.value, ConfigAutoValue.auto_id);
                }
            }
        }

        // Cài đặt cần câu

        private void changeFishingRod1(object sender, SelectionChangedEventArgs e)
        {
            FishingRod fishingRod = cb_ListFishingRod.SelectedItem as FishingRod;
            if (ConfigFishingRod != null)
            {
                if(fishingRod.value != ConfigFishingRod.fishing_rod_2 && fishingRod.value != ConfigFishingRod.fishing_rod_1)
                {
                    FishingRodConfig.changeDataFishingRod(ConfigFishingRod._id, fishingRod.value, "fishing_rod_1");
                } else
                {
                    cb_ListFishingRod.SelectedIndex = ConfigFishingRod.fishing_rod_1 - 1;
                    if (fishingRod.value == ConfigFishingRod.fishing_rod_2)
                    {
                        new Notification() { id = Guid.NewGuid().ToString(), title = "Lỗi cài đặt cần câu", content = "Cần câu chính phải khác loại cần câu phụ, vui lòng chọn lại!", type = "error", timeStrat = DateTime.Now };
                    }
                }
                
            }
        }

        private void changeFishingRod2(object sender, SelectionChangedEventArgs e)
        {
            FishingRod fishingRod = cb_ListFishingRod2.SelectedItem as FishingRod;
            if (ConfigFishingRod != null)
            {
                if (fishingRod.value != ConfigFishingRod.fishing_rod_1 && fishingRod.value != ConfigFishingRod.fishing_rod_2)
                {
                    FishingRodConfig.changeDataFishingRod(ConfigFishingRod._id, fishingRod.value, "fishing_rod_2");
                }
                else
                {
                    cb_ListFishingRod2.SelectedIndex = ConfigFishingRod.fishing_rod_2 - 1;
                    if (fishingRod.value == ConfigFishingRod.fishing_rod_1)
                    {
                        new Notification() { id = Guid.NewGuid().ToString(), title = "Lỗi cài đặt cần câu", content = "Cần câu phụ phải khác loại cần câu chính, vui lòng chọn lại!", type = "error", timeStrat = DateTime.Now };
                    }
                }

            }
        }

        private void isChangeFishingRodChanged(object sender, RoutedEventArgs e)
        {
            if (ConfigFishingRod != null)
            {
                CheckBox checkbox = (sender as CheckBox);
                bool isChecked = checkbox.IsChecked.Value;
                FishingRodConfig.changeDataFishingRod(ConfigFishingRod._id, isChecked ? 1 : 0, "change_fishing_rod");
            }
        }

        private void startChangeStepChange(object sender, RoutedEventArgs e)
        {
            TextboxIsBeingChanged = true;
        }

        private void changeStepChange(object sender, RoutedEventArgs e)
        {
            TextboxIsBeingChanged = false;
            TextBox textbox = (sender as TextBox);
            if (textbox.Text == "" || textbox.Text == "0")
            {
                textbox.Text = ConfigFishingRod.step_change.ToString();
                new Notification() { id = Guid.NewGuid().ToString(), title = "Lỗi cài đặt cần câu", content = "Giá trị cài đặt không hợp lệ, vui lòng nhập một số lớn hơn 0", type = "error", timeStrat = DateTime.Now };
            } else
            {
                int value = Int32.Parse(textbox.Text);
                FishingRodConfig.changeDataFishingRod(ConfigFishingRod._id, value, "step_change");
            }
        }

        private void isChangeReGetInfoRod(object sender, RoutedEventArgs e)
        {
            if (ConfigFishingRod != null)
            {
                CheckBox checkbox = (sender as CheckBox);
                bool isChecked = checkbox.IsChecked.Value;
                FishingRodConfig.changeDataFishingRod(ConfigFishingRod._id, isChecked ? 1 : 0, "get_info_rod");
            }
        }

        private void startChangeDelayFishingTest(object sender, RoutedEventArgs e)
        {
            TextboxIsBeingChanged = true;
        }

        private void changeDelayFishingTest(object sender, RoutedEventArgs e)
        {
            TextboxIsBeingChanged = false;
            TextBox textbox = (sender as TextBox);
            if (textbox.Text == "" || textbox.Text == "0")
            {
                textbox.Text = ConfigAutoValue.delay_fishing_test.ToString();
                new Notification() { id = Guid.NewGuid().ToString(), title = "Lỗi cài đặt kiểm tra cắn câu", content = "Giá trị cài đặt không hợp lệ, vui lòng nhập một số lớn hơn 0", type = "error", timeStrat = DateTime.Now };
            }
            else
            {
                int value = Int32.Parse(textbox.Text);
                ConfigAuto.changeDataConfigAuto(ConfigAutoValue._id, value, "delay_fishing_test");
            }
        }

        private void isChangeSaveExclamation(object sender, RoutedEventArgs e)
        {
            if (ConfigAutoValue != null)
            {
                CheckBox checkbox = (sender as CheckBox);
                bool isChecked = checkbox.IsChecked.Value;
                ConfigAuto.changeDataConfigAuto(ConfigAutoValue._id, isChecked ? 1 : 0, "save_exclamation");
            }
        }

        private void speedAutoChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (tb_ValueSpeedName != null && ConfigAutoValue != null)
            {
                int valueChange = Convert.ToInt32(slider_SpeedAuto.Value);
                tb_ValueSpeedName.Text = valueChange.ToString();
                ConfigAuto.changeDataConfigAuto(ConfigAutoValue._id, valueChange, "speed");
            }
        }

        private void startChangeDelayCheck(object sender, RoutedEventArgs e)
        {
            TextboxIsBeingChanged = true;
        }

        private void changeDelayCheck(object sender, RoutedEventArgs e)
        {
            TextboxIsBeingChanged = false;
            TextBox textbox = (sender as TextBox);
            if (textbox.Text == "")
            {
                textbox.Text = ConfigAutoValue.delay_check.ToString();
                new Notification() { id = Guid.NewGuid().ToString(), title = "Lỗi cài đặt độ trể kiểm tra cắn câu", content = "Giá trị cài đặt không hợp lệ, vui lòng vào một số", type = "error", timeStrat = DateTime.Now };
            }
            else
            {
                int value = Int32.Parse(textbox.Text);
                ConfigAuto.changeDataConfigAuto(ConfigAutoValue._id, value, "delay_check");
            }
        }

        public void setMassageNotiStep(string auto_id, int type, string message = "")
        {
            if(Config._id == auto_id)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    if (type == 0)
                    {
                        fishingDone.Visibility = Visibility.Hidden;
                        notiStep.Visibility = Visibility.Visible;
                        notiStep.Text = message;
                    }
                    else if (type == 1)
                    {
                        notiStep.Visibility = Visibility.Hidden;
                        fishingDone.Visibility = Visibility.Visible;
                        fishingDoneName.Text = AutoValue.nameFishNotiStep;
                        fishingDoneName.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(AutoValue.colorLevelFishNotiStep);
                        fishingDoneVM.Visibility = AutoValue.isVMFishNotiStep ? Visibility.Visible : Visibility.Hidden;
                        fishingDoneVM.Width = AutoValue.isVMFishNotiStep ? 20 : 0;
                        fishingDonePrice.Text = AutoValue.priceFishNotiStep.ToString();
                        fishingDoneMoney.Source = AutoValue.sizeFishNotiStep > 0 ? new BitmapImage(new Uri("pack://application:,,,/images/icon/icon_money_1.png")) : new BitmapImage(new Uri("pack://application:,,,/images/icon/icon_recycle_1.png"));
                    }
                });
            }
        }


        // ------------- Setter giá trị thóng kê --------------- //
        public void setSummaryTotal(string auto_id, int value, int type)
        {
            if (Config._id == auto_id)
            {
                switch (type)
                {
                    case 1:
                        total_quangCan.Text = value.ToString();
                        break;
                    case 2:
                        total_boQuaCa.Text = value.ToString();
                        break;
                    case 3:
                        total_dutDay.Text = value.ToString();
                        break;
                    case 4:
                        total_cauLoi.Text = value.ToString();
                        break;
                    case 5:
                        total_soCa.Text = value.ToString();
                        break;
                    case 6:
                        total_doTaiChe.Text = value.ToString();
                        break;
                    case 7:
                        total_tienBanCa.Text = value.ToString();
                        break;
                    case 8:
                        total_tienSuaCan.Text = value.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        public void setNullSummaryTotal(string auto_id)
        {
            if (Config._id == auto_id)
            {
                total_quangCan.Text = "0";
                total_boQuaCa.Text = "0";
                total_dutDay.Text = "0";
                total_cauLoi.Text = "0";
                total_soCa.Text = "0";
                total_doTaiChe.Text = "0";
                total_tienBanCa.Text = "0";
                total_tienSuaCan.Text = "0";
            }
        }

        public void setSummaryTotalCurent(string _id)
        {
            AutoValue = Auto._ListAuto.Single(r => r.auto_id == _id);
            total_quangCan.Text = AutoValue.summaryTotal.quangCan.ToString();
            total_boQuaCa.Text = AutoValue.summaryTotal.boQuaCa.ToString();
            total_dutDay.Text = AutoValue.summaryTotal.dutDay.ToString();
            total_cauLoi.Text = AutoValue.summaryTotal.cauLoi.ToString();
            total_soCa.Text = AutoValue.summaryTotal.soCa.ToString();
            total_doTaiChe.Text = AutoValue.summaryTotal.doTaiChe.ToString();
            total_tienBanCa.Text = AutoValue.summaryTotal.tienBanCa.ToString();
            total_tienSuaCan.Text = AutoValue.summaryTotal.tienSuaCan.ToString();
        }

        public void setTime(string auto_id)
        {
            if (Config._id == auto_id)
            {
                AutoValue = Auto._ListAuto.Single(r => r.auto_id == auto_id);
                total_Hour.Text = String.Format("{0:00}", AutoValue.totalTime.hour);
                total_Minute.Text = String.Format("{0:00}", AutoValue.totalTime.minute);
                total_Second.Text = String.Format("{0:00}", AutoValue.totalTime.second);
            }
        }

        public void initTotalSummaryFish()
        {
            int count1 = AutoValue.summaryFish.Where(x => x.level == 1).ToList().Count;
            int count2 = AutoValue.summaryFish.Where(x => x.level == 2).ToList().Count;
            int count3 = AutoValue.summaryFish.Where(x => x.level == 3).ToList().Count;
            int count4 = AutoValue.summaryFish.Where(x => x.level == 4).ToList().Count;
            tb_TotalFish1.Text = count1.ToString();
            tb_TotalFish2.Text = count2.ToString();
            tb_TotalFish3.Text = count3.ToString();
            tb_TotalFish4.Text = count4.ToString();
        }

        public void updateTotalSummaryFish(int level, string auto_id)
        {
            if (Config._id == auto_id)
            {
                int count = AutoValue.summaryFish.Where(x => x.level == level).ToList().Count;
                switch (level)
                {
                    case 1:
                        tb_TotalFish1.Text = count.ToString();
                        break;
                    case 2:
                        tb_TotalFish2.Text = count.ToString();
                        break;
                    case 3:
                        tb_TotalFish3.Text = count.ToString();
                        break;
                    case 4:
                        tb_TotalFish4.Text = count.ToString();
                        break;
                }
            }
        }

        public void refreshTotalSummaryFish()
        {
            tb_TotalFish1.Text = "0";
            tb_TotalFish2.Text = "0";
            tb_TotalFish3.Text = "0";
            tb_TotalFish4.Text = "0";
        }

        public void sortChange(string auto_id, int typeSort, int typeSelect)
        {
            if (Config._id == auto_id)
            {
                AutoValue = Auto._ListAuto.Single(r => r.auto_id == auto_id);
                sortDown_STT.Opacity = AutoValue.summaryFishSort.sortSTT == 1 ? 1 : 0.5;
                sortUp_STT.Opacity = AutoValue.summaryFishSort.sortSTT == 2 ? 1 : 0.5;
                sortDown_Money.Opacity = AutoValue.summaryFishSort.sortMoney == 1 ? 1 : 0.5;
                sortUp_Money.Opacity = AutoValue.summaryFishSort.sortMoney == 2 ? 1 : 0.5;
                sortDown_Shadow.Opacity = AutoValue.summaryFishSort.sortShadow == 1 ? 1 : 0.5;
                sortUp_Shadow.Opacity = AutoValue.summaryFishSort.sortShadow == 2 ? 1 : 0.5;

                int valueSort = 9;
                int valueSelect = 9;



                if (typeSelect == 2)
                {
                    valueSelect = AutoValue.summaryFishSort.sortLevel;
                    if(valueSelect != -1)
                    {
                        AutoValue.summaryFishSort.listSort = AutoValue.summaryFish.Where(x => x.level == valueSelect).ToList();
                    } else
                    {
                        AutoValue.summaryFishSort.listSort = AutoValue.summaryFish;
                    }
                }
                else if (typeSelect == 4)
                {
                    valueSelect = AutoValue.summaryFishSort.sortSize;
                    if(valueSelect != 0)
                    {
                        AutoValue.summaryFishSort.listSort = AutoValue.summaryFish.Where(x => x.size == valueSelect).ToList();
                    } else
                    {
                        AutoValue.summaryFishSort.listSort = AutoValue.summaryFish;
                    }
                }

                if (typeSort == 1)
                {
                    valueSort = AutoValue.summaryFishSort.sortSTT;
                    if(valueSort == 0 || valueSort == 1)
                    {
                        AutoValue.summaryFishSort.listSort = AutoValue.summaryFishSort.listSort.OrderByDescending(item => item.stt).ToList();
                    } else if (valueSort == 2)
                    {
                        AutoValue.summaryFishSort.listSort = AutoValue.summaryFishSort.listSort.OrderBy(item => item.stt).ToList();
                    }
                } else if (typeSort == 3)
                {
                    valueSort = AutoValue.summaryFishSort.sortMoney;
                    if (valueSort == 1)
                    {
                        AutoValue.summaryFishSort.listSort = AutoValue.summaryFishSort.listSort.OrderByDescending(item => item.money).ToList();
                    }
                    else if (valueSort == 2)
                    {
                        AutoValue.summaryFishSort.listSort = AutoValue.summaryFishSort.listSort.OrderBy(item => item.money).ToList();
                    }
                } else if (typeSort == 5)
                {
                    valueSort = AutoValue.summaryFishSort.sortShadow;
                    if (valueSort == 1)
                    {
                        AutoValue.summaryFishSort.listSort = AutoValue.summaryFishSort.listSort.OrderByDescending(item => item.shadow).ToList();
                    }
                    else if (valueSort == 2)
                    {
                        AutoValue.summaryFishSort.listSort = AutoValue.summaryFishSort.listSort.OrderBy(item => item.shadow).ToList();
                    }
                }
                int totalPage = AutoValue.summaryFishSort.listSort.Count % 10 > 0 ? AutoValue.summaryFishSort.listSort.Count / 10 + 1 : AutoValue.summaryFishSort.listSort.Count / 10;
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.Totalpage = totalPage);
                tb_pageSummaryFish.Text = AutoValue.summaryFishSort.page.ToString();
                AutoValue.summaryFishSort.listSort = AutoValue.summaryFishSort.listSort.Skip((AutoValue.summaryFishSort.page - 1) * 10).Take(10).ToList();
                if(AutoValue.summaryFishSort.listSort.Count > 0)
                {
                    ic_SummaryFish.ItemsSource = AutoValue.summaryFishSort.listSort;
                    ic_SummaryFish.Items.Refresh();
                    ic_SummaryFish.Visibility = Visibility.Visible;
                    action_Sort.Visibility = Visibility.Visible;
                    action_Sort.Height = Double.NaN;
                    action_Sort.Margin = new Thickness(0, 10, 0, 2);
                    paging_ListFish.Visibility = Visibility.Visible;
                    notFoundSortData.Visibility = Visibility.Hidden;
                } else
                {
                    ic_SummaryFish.ItemsSource = AutoValue.summaryFishSort.listSort;
                    ic_SummaryFish.Items.Refresh();
                    ic_SummaryFish.Visibility = Visibility.Hidden;
                    if(AutoValue.summaryFish.Count > 0)
                    {
                        action_Sort.Visibility = Visibility.Visible;
                        action_Sort.Height = Double.NaN;
                        action_Sort.Margin = new Thickness(0, 10, 0, 2);
                    } else
                    {
                        action_Sort.Visibility = Visibility.Hidden;
                        action_Sort.Height = 0;
                        action_Sort.Margin = new Thickness(0, 0, 0, 2);
                    }
                    paging_ListFish.Visibility = Visibility.Hidden;
                    notFoundSortData.Visibility = Visibility.Visible;
                }
            }
        }

        private void changeSortSTT(object sender, MouseButtonEventArgs e)
        {
            int sortNextValue = 0;
            AutoValue = Auto._ListAuto.Single(r => r.auto_id == Config._id);
            if (AutoValue.summaryFishSort.sortSTT == 1)
            {
                sortNextValue = 2;
            } else if (AutoValue.summaryFishSort.sortSTT == 2)
            {
                sortNextValue = 1;
            } else if (AutoValue.summaryFishSort.sortSTT == 0)
            {
                sortNextValue = 2;
            }
            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortSTT = sortNextValue);
            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.typeSort = 1);

            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortMoney = 0);
            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortShadow = 0);
            sortChange(Config._id, 1, AutoValue.summaryFishSort.typeSelect);
        }

        // Init danh sách level
        public void updateListLevel(int index)
        {
            listLevel = itemSelectLevel._LevelList;
            cb_SelectLevel.ItemsSource = listLevel;
            cb_SelectLevel.Items.Refresh();
            cb_SelectLevel.SelectedIndex = index;
        }

        private void changeLevelSelect(object sender, SelectionChangedEventArgs e)
        {
            itemSelectLevel level = cb_SelectLevel.SelectedItem as itemSelectLevel;
            if (Auto._ListAuto.Count > 0 && level != null)
            {
                AutoValue = Auto._ListAuto.Single(r => r.auto_id == Config._id);
                if (AutoValue.summaryFishSort.sortLevel != level.value)
                {
                    Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortLevel = level.value);
                    Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.typeSelect = 2);

                    Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortSize = 0);
                    cb_SelectSize.SelectedIndex = 0;
                    Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.page = 1);
                    sortChange(Config._id, AutoValue.summaryFishSort.typeSort, 2);
                    Debug.WriteLine(level.label);
                }
            }
        }

        private void changeSortMoney(object sender, MouseButtonEventArgs e)
        {
            int sortNextValue = 0;
            AutoValue = Auto._ListAuto.Single(r => r.auto_id == Config._id);
            if(AutoValue.summaryFishSort.sortMoney == 0)
            {
                sortNextValue = 1;
            } else if (AutoValue.summaryFishSort.sortMoney == 1)
            {
                sortNextValue = 2;
            } else if (AutoValue.summaryFishSort.sortMoney == 2)
            {
                sortNextValue = 0;
            }
            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortMoney = sortNextValue);
            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.typeSort = 3);

            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortSTT = 0);
            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortShadow = 0);
            sortChange(Config._id, 3, AutoValue.summaryFishSort.typeSelect);
        }



        // Init danh sách size
        public void updateListSize(int index)
        {
            listSize = itemSelectSize._SizeList;
            cb_SelectSize.ItemsSource = listSize;
            cb_SelectSize.Items.Refresh();
            cb_SelectSize.SelectedIndex = index;
        }

        private void changeSizeSelect(object sender, SelectionChangedEventArgs e)
        {
            itemSelectSize size = cb_SelectSize.SelectedItem as itemSelectSize;
            if (Auto._ListAuto.Count > 0 && size != null)
            {
                AutoValue = Auto._ListAuto.Single(r => r.auto_id == Config._id);
                if (AutoValue.summaryFishSort.sortSize != size.value)
                {
                    Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortSize = size.value);
                    Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.typeSelect = 4);

                    Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortLevel = -1);
                    cb_SelectLevel.SelectedIndex = 0;
                    Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.page = 1);
                    sortChange(Config._id, AutoValue.summaryFishSort.typeSort, 4);
                    Debug.WriteLine(size.label);
                }
            }
        }

        private void changeSortShadow(object sender, MouseButtonEventArgs e)
        {
            int sortNextValue = 0;
            AutoValue = Auto._ListAuto.Single(r => r.auto_id == Config._id);
            if (AutoValue.summaryFishSort.sortShadow == 0)
            {
                sortNextValue = 1;
            }
            else if (AutoValue.summaryFishSort.sortShadow == 1)
            {
                sortNextValue = 2;
            }
            else if (AutoValue.summaryFishSort.sortShadow == 2)
            {
                sortNextValue = 0;
            }
            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortShadow = sortNextValue);
            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.typeSort = 5);

            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortSTT = 0);
            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortMoney = 0);
            sortChange(Config._id, 5, AutoValue.summaryFishSort.typeSelect);
        }

        private void sort_PrevPage(object sender, MouseButtonEventArgs e)
        {
            AutoValue = Auto._ListAuto.Single(r => r.auto_id == Config._id);
            int currentPage = AutoValue.summaryFishSort.page;
            if (currentPage > 1)
            {
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.page = currentPage - 1);
                sortChange(Config._id, AutoValue.summaryFishSort.typeSort, AutoValue.summaryFishSort.typeSelect);
                tb_pageSummaryFish.Text = (currentPage - 1).ToString();
            }
        }

        private void sort_NextPage(object sender, MouseButtonEventArgs e)
        {
            AutoValue = Auto._ListAuto.Single(r => r.auto_id == Config._id);
            int currentPage = AutoValue.summaryFishSort.page;
            if (currentPage < AutoValue.summaryFishSort.Totalpage)
            {
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.page = currentPage + 1);
                sortChange(Config._id, AutoValue.summaryFishSort.typeSort, AutoValue.summaryFishSort.typeSelect);
                tb_pageSummaryFish.Text = (currentPage + 1).ToString();
            }
        }

        private void startChangeSortPage(object sender, RoutedEventArgs e)
        {
            TextboxIsBeingChanged = true;
        }

        private void changeSortPage(object sender, RoutedEventArgs e)
        {
            AutoValue = Auto._ListAuto.Single(r => r.auto_id == Config._id);
            TextboxIsBeingChanged = false;
            TextBox textbox = (sender as TextBox);
            if (textbox.Text == "" || Int32.Parse(textbox.Text) > AutoValue.summaryFishSort.Totalpage || Int32.Parse(textbox.Text) < 1)
            {
                textbox.Text = AutoValue.summaryFishSort.page.ToString();
                new Notification() { id = Guid.NewGuid().ToString(), title = "Lỗi chuyển trang thống kê", content = "Số trang không hợp lệ", type = "error", timeStrat = DateTime.Now };
            }
            else
            {
                int page = Int32.Parse(textbox.Text);
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.page = page);
                sortChange(Config._id, AutoValue.summaryFishSort.typeSort, AutoValue.summaryFishSort.typeSelect);
            }
        }

        // Xoá dữ liệu thống kê

        private void click_RemoveSummary(object sender, MouseButtonEventArgs e)
        {
            DialogResult result = CMessageBox.Show("Xác nhận làm mới dữ liệu thống kê", "Bạn có muốn làm mới dữ liệu thống kê không? việc này sẽ xoá toàn bộ dữ liệu thống kê hiện tại", "Làm mới", "Đóng");
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Debug.WriteLine("Làm mới");
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort = new SummaryFishSort());
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.page = 1);
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortSTT = 0);
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortMoney = 0);
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortShadow = 0);
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortLevel = -1);
                cb_SelectLevel.SelectedIndex = 0;
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFishSort.sortSize = 0);
                cb_SelectSize.SelectedIndex = 0;
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryTotal = new SummaryTotal(0, 0, 0, 0, 0, 0, 0, 0));
                setNullSummaryTotal(Config._id);
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.summaryFish = new List<SummaryFish>());
                sortChange(Config._id, 1, 2);
                Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.totalTime = new SummaryTotalTime());
                refreshTotalSummaryFish();

                new Notification() { id = Guid.NewGuid().ToString(), title = "Làm mới thống kê thành công", content = "Dữ liệu thống kê đã được làm mới, các dữ liệu trước đó đã bị xoá bỏ", type = "success", timeStrat = DateTime.Now };
                
            }
        }

        public void getTimeGame()
        {
            var moment = DateTime.Now;
            Debug.WriteLine(moment.Minute);
            Debug.WriteLine(moment.Second);
            int minute = moment.Minute >= 30 ? moment.Minute - 30 : moment.Minute;
            int totalSecond = minute * 60 + moment.Second;

            Debug.WriteLine(totalSecond);
            hourGame = totalSecond / 75;
            hourGame = hourGame > 12 ? hourGame - 12 : hourGame;
            sessionGame = hourGame > 12 ? "PM" : "AM";
            minuteGame = (int)((totalSecond % 75) / 1.25);

            int totalMinuteGame = (int)(totalSecond / 1.25);
            setTimeGame(totalMinuteGame);

            Debug.WriteLine(hourGame);
            Debug.WriteLine(minuteGame);
        }

        public void setTimeGame(int totalMinute)
        {
            Task setTime = new Task(() =>
            {
                while (true)
                {
                    totalMinute++;
                    if (totalMinute == 1440)
                    {
                        totalMinute = 1;
                    }
                    totalMinuteGame = totalMinute;
                    hourGame = totalMinute / 60;
                    hourGame = hourGame > 12 ? hourGame - 12 : hourGame;
                    if (totalMinute >= 720)
                    {
                        sessionGame = "PM";
                    } else
                    {
                        sessionGame = "AM";
                    }
                    
                    minuteGame = totalMinute % 60;
                    //Debug.WriteLine(totalMinute);
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            if(game_Hour.Text != null)
                            {
                                game_Hour.Text = hourGame.ToString();
                                game_Minute.Text = String.Format("{0:00}", minuteGame);
                                game_Session.Text = sessionGame;
                            }
                        } catch { }
                    });

                    //Debug.WriteLine(hourGame + ":" + minuteGame + " " + sessionGame);
                    Thread.Sleep(1250);
                }
            });
            setTime.Start();
        }

        private void getLocation(object sender, MouseButtonEventArgs e)
        {
            Task getPosition = new Task(() =>
            {
                ConfigAuto.changeDataConfigAuto(ConfigAutoValue._id, true ? 1 : 0, "save_exclamation");
                AutoValue = Auto._ListAuto.Single(r => r.auto_id == Config._id);
                App.Current.Dispatcher.Invoke(() =>
                {
                    tb_StatusGetPosition.Visibility = Visibility.Visible;
                    button_StopGetPosition.Visibility = Visibility.Visible;
                    cb_isSaveExclamation.IsChecked = true;
                    if (AutoValue.detect_ExclamationArea != null)
                    {
                        get_Position.Source = new BitmapImage(new Uri("pack://application:,,,/images/icon/get-location.png"));
                    }
                });
                
                Thread.Sleep(100);
                Debug.WriteLine("AutoValue: " + AutoValue.isAuto);
                AutoValue.controlGetPosition();
            });
            getPosition.Start();
        }

        public void updateStatusGetPosition(int statusGetPosition)
        {
            AutoValue = Auto._ListAuto.Single(r => r.auto_id == Config._id);
            Thread.Sleep(100);
            if (AutoValue.isGetPosition)
            {
                tb_StatusGetPosition.Visibility = Visibility.Visible;
                button_StopGetPosition.Visibility = Visibility.Visible;
            } else
            {
                tb_StatusGetPosition.Visibility = Visibility.Hidden;
                button_StopGetPosition.Visibility = Visibility.Hidden;
            }
            if (statusGetPosition == 2)
            {
                get_Position.Source = new BitmapImage(new Uri("pack://application:,,,/images/icon/get-location-error.png"));
            } else if (statusGetPosition == 0)
            {
                get_Position.Source = new BitmapImage(new Uri("pack://application:,,,/images/icon/get-location.png"));
            }
            else
            {
                get_Position.Source = AutoValue.detect_ExclamationArea.Count > 0 && AutoValue.detect_CheckSizeArea.Count > 0 ? new BitmapImage(new Uri("pack://application:,,,/images/icon/get-location-success.png")) : new BitmapImage(new Uri("pack://application:,,,/images/icon/get-location.png"));
            }
        }

        private void stopGetPosition(object sender, MouseButtonEventArgs e)
        {
            Auto._ListAuto.Where(emu => emu.auto_id == Config._id).ToList().ForEach(s => s.isGetPosition = false);
            tb_StatusGetPosition.Visibility = Visibility.Hidden;
            button_StopGetPosition.Visibility = Visibility.Hidden;
            get_Position.Source = new BitmapImage(new Uri("pack://application:,,,/images/icon/get-location.png"));
            App.Current.Dispatcher.Invoke(() =>
            {
                setMassageNotiStep(Config._id, 0, "Đã dừng lấy toạ độ thủ công");
            });
        }
    }
}
