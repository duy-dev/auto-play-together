using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Diagnostics;
using Thread = System.Threading.Thread;
using KAutoHelper;
using System.Windows.Media.Animation;
using Auto_Demo.services;
using Auto_Demo.services.config;

namespace Auto_Demo.popups
{
    /// <summary>
    /// Interaction logic for add_device.xaml
    /// </summary>
    /// 
    public class Device
    {
        public int id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string icon { get; set; }
        //public string className { get; set; }
        //public string text { get; set; }
        public bool isCustom { get; set; }

        public Device()
        {
            this.isCustom = false;
        }
    }

    public partial class add_device : Window
    {
        public static MainWindow wnd = (MainWindow)Application.Current.MainWindow;

        private int heightPopup = 300;
        private int widthPopup = 380;
        private string nameCustomDevice = "";
        private string typeSelect = "";
        Storyboard sb_SelectDeviceShow;
        Storyboard sb_SelectDeviceHidden;
        database db = new database();
        public add_device()
        {
            InitializeComponent();
            window_main.Width = this.widthPopup;
            window_main.Height = this.heightPopup;
            initConfig();
        }

        private void initConfig()
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.widthPopup) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.heightPopup) / 2;
        }

        private void Window_MouseMove(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void close_Popup(object sender, MouseButtonEventArgs e)
        {
            Thread.Sleep(200);
            Close();
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

        private void load_Data(object sender, RoutedEventArgs e)
        {
            List<Device> listDevice = new List<Device>();
            Device customDevice = new Device() { id = 0, title = "", type = "custom", icon = "../images/android.png", isCustom = true };
            listDevice.Add(customDevice);

            var processes = Process.GetProcesses().Where(p => !string.IsNullOrEmpty(p.MainWindowTitle)).ToList();
            foreach (var process in processes)
            {
                if (process.ProcessName == "HD-Player")
                {
                    Device info = new Device() { id = process.Id, title = process.MainWindowTitle, type = "bluestacks", icon = "../images/bluestacks.png" };
                    listDevice.Add(info);
                }
                else if (process.ProcessName == "dnplayer")
                {
                    Device info = new Device() { id = process.Id, title = process.MainWindowTitle, type = "ldplayer", icon = "../images/ldplayer.png" };
                    listDevice.Add(info);
                }
                else if (process.ProcessName == "Nox")
                {
                    Device info = new Device() { id = process.Id, title = process.MainWindowTitle, type = "nox", icon = "../images/nox.png" };
                    listDevice.Add(info);
                }
                else if (process.ProcessName == "MEmu")
                {
                    Device info = new Device() { id = process.Id, title = process.MainWindowTitle, type = "memu", icon = "../images/memu.png" };
                    listDevice.Add(info);
                }
            }

            lb_ListDevice.ItemsSource = listDevice;
            lb_ListDevice.SelectionChanged += selectDevice;
            sb_SelectDeviceShow = this.FindResource("lb_device_down") as Storyboard;
            sb_SelectDeviceHidden = this.FindResource("lb_device_up") as Storyboard;
        }

        void selectDevice(object sender, SelectionChangedEventArgs e)
        {
            Device deviceSelect = lb_ListDevice.SelectedItem as Device;
            if (!deviceSelect.isCustom)
            {
                tb_SelectName.Text = deviceSelect.title;
                tb_SelectID.Text = deviceSelect.id.ToString();
                label_ID.Text = "ID: ";
                //tb_ClassName.Text = deviceSelect.className;
                //tb_Text.Text = deviceSelect.text;
                var uriSource = new Uri(deviceSelect.icon, UriKind.Relative);
                img_IconEmulator.Source = new BitmapImage(uriSource);
                typeSelect = deviceSelect.type;
                Thread.Sleep(200);
                isShowSelectDevice = false;
                sb_SelectDeviceHidden.Begin();
            }
        }

        private Process getProcessByTitle(string title)
        {
            var processes = Process.GetProcesses().Where(p => !string.IsNullOrEmpty(p.MainWindowTitle)).ToList();
            foreach (var process in processes)
            {
                if (process.MainWindowTitle == title)
                {
                    return process;
                }
            }
            return null;
        }

        private void bt_AddDeviceCustom(object sender, RoutedEventArgs e)
        {
            
            IntPtr win = KAutoHelper.AutoControl.FindWindowHandle(null, nameCustomDevice);
            if (win == IntPtr.Zero)
            {
                new Notification() { id = Guid.NewGuid().ToString(), title = "Không tìm thấy thiết bị", content = "vui kiểm tra lại tên giả lập, bảo đảm rằng tên giả lập là duy nhất", type = "error", timeStrat = DateTime.Now };
            } else
            {
                tb_SelectName.Text = nameCustomDevice;
                tb_SelectID.Text = getProcessByTitle(nameCustomDevice).Id.ToString();
                label_ID.Text = "ID: ";
                //tb_ClassName.Text = "";
                //tb_Text.Text = "";
                var uriSource = new Uri("../images/android.png", UriKind.Relative);
                img_IconEmulator.Source = new BitmapImage(uriSource);
                typeSelect = "custom";
                Thread.Sleep(200);
                isShowSelectDevice = false;
                sb_SelectDeviceHidden.Begin();
            }
        }

        bool isShowSelectDevice = false;
        private void toggle_ListSelectDevice(object sender, MouseButtonEventArgs e)
        {
            if (isShowSelectDevice)
            {
                sb_SelectDeviceHidden.Begin();
                isShowSelectDevice = !isShowSelectDevice;
            } else
            {
                sb_SelectDeviceShow.Begin();
                isShowSelectDevice = !isShowSelectDevice;
            }
        }

        private void bt_AddDevice(object sender, RoutedEventArgs e)
        {
            if (tb_Name.Text == "")
            {
                new Notification() { id = Guid.NewGuid().ToString(), title = "Tên không được để trống", content = "vui lòng nhập tên phân biệt cho thiết bị", type = "error", timeStrat = DateTime.Now };
            } 
            //else if (tb_ClassName.Text == "")
            //{
            //    new Notification() { id = Guid.NewGuid().ToString(), title = "Class name không được để trống", content = "vui lòng nhập class name của thiết bị", type = "error", timeStrat = DateTime.Now };
            //}
            //else if (tb_Text.Text == "")
            //{
            //    new Notification() { id = Guid.NewGuid().ToString(), title = "Text control không được để trống", content = "vui lòng nhập text control của thiết bị", type = "error", timeStrat = DateTime.Now };
            //}
            else if (tb_SelectID.Text == "")
            {
                new Notification() { id = Guid.NewGuid().ToString(), title = "Thiết bị không được để trống", content = "vui lòng chọn một thiết bị", type = "error", timeStrat = DateTime.Now };
            } else
            {
                Guid id = Guid.NewGuid();
                string txtQuery = $"INSERT INTO Auto (_id,name,emulator_id,emulator_name,emulator_type) VALUES ('{id.ToString()}', '{tb_Name.Text}', '{tb_SelectID.Text}', '{tb_SelectName.Text}', '{typeSelect}')";
                Guid id_FishingRod = Guid.NewGuid();
                string txtQueryConfigFishingRod = $"INSERT INTO FishingRod (_id,auto_id,fishing_rod_1,fishing_rod_2,change_fishing_rod,step_change,get_info_rod) VALUES ('{id_FishingRod}', '{id}', 1, 2, 0, 5, 1)";
                Guid id_ConfigAuto = Guid.NewGuid();
                string txtQueryConfigAuto = $"INSERT INTO Config (_id,auto_id,delay_fishing_test,speed,save_exclamation,delay_check) VALUES ('{id_ConfigAuto}', '{id}', 16, 100, 0, 100)";
                try
                {
                    // Khởi tạo cấu hình thiết bị
                    db.ExecuteQuery(txtQuery);
                    string _id = id.ToString();
                    string name = tb_Name.Text;
                    string emulator_id = tb_SelectID.Text;
                    string emulator_name = tb_SelectName.Text;
                    //string className = tb_ClassName.Text;
                    //string text = tb_Text.Text;
                    string emulator_type = typeSelect;
                    new emulator(_id, name, emulator_id, emulator_name, emulator_type, true);
                    emulator.changeActive(_id);

                    // Khởi tạo cài đặt cần câu
                    db.ExecuteQuery(txtQueryConfigFishingRod);
                    new FishingRodConfig(id_FishingRod.ToString(), id.ToString(), 1, 2, 0, 5, 1, true);

                    // Khởi tạo cài đặt auto
                    db.ExecuteQuery(txtQueryConfigAuto);
                    new ConfigAuto(id_ConfigAuto.ToString(), id.ToString(), 16, -1, 100, 0, 100, true);

                    // Khởi tạo cài đặt bỏ bóng cá
                    List<FishSize> listSize = new List<FishSize>();

                    for (int size = 1; size < 7; size++)
                    {
                        try
                        {
                            Guid id_SizeFish = Guid.NewGuid();
                            string txtQueryConfigSizeFish = $"INSERT INTO FishSize (_id,auto_id,size,min,max,status,setting) VALUES ('{id_SizeFish}', '{id}', {size}, {1 + (size -1) * 30}, {size * 30}, 0, 'custom')";
                            db.ExecuteQuery(txtQueryConfigSizeFish);
                            FishSize fishSize = new FishSize(id_SizeFish.ToString(), size, 1 + (size - 1) * 30, size * 30, 0, id.ToString());
                            listSize.Add(fishSize);
                        } catch
                        {
                            new Notification() { id = Guid.NewGuid().ToString(), title = "Cấu hình cài đặt thất bại", content = "Đã có lỗi xảy ra trong quá trình cấu hình cài đặt auto, vui lòng xoá thiết vừa thêm và thử lại", type = "error", timeStrat = DateTime.Now };
                            break;
                        }
                    }

                    new ConfigFishSize(listSize, id.ToString(), "custom", -1, true);
                    new fastConfigFishSize(_id, name);

                    wnd.IsDashboard = false;

                    // Khởi tạo auto
                    new Auto(id.ToString(), emulator_type, emulator_name, true);
                    new Notification() { id = Guid.NewGuid().ToString(), title = "Thêm thiết bị auto thành công", content = "hãy nhớ setting thiết bị trước khi tiến hành chạy auto", type = "success", timeStrat = DateTime.Now };
                }
                catch
                {
                    new Notification() { id = Guid.NewGuid().ToString(), title = "Thêm thiết bị auto thất bại", content = "Đã có một lỗi dữ liệu xảy ra khi thêm mới thiết bị!", type = "error", timeStrat = DateTime.Now };
                }

                
                Thread.Sleep(200);
                Close();
            }
        }

        private void changeNameCustom(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                nameCustomDevice = textBox.Text;
            }
        }
    }
}
