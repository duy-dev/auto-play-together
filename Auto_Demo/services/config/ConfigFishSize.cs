using KAutoHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Auto_Demo.services.config
{
    public class ConfigFishSize
    {
        public static MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public List<FishSize> listSize { get; set; }
        public string auto_id { get; set; }
        public string mode_setting { get; set; }
        public int mode_auto_count_check { get; set; }

        public static ObservableCollection<ConfigFishSize> _ListConfigFishSize = new ObservableCollection<ConfigFishSize>();

        public ConfigFishSize(List<FishSize> _listSize, string _auto_id, string _mode_setting, int _mode_auto_count_check, bool isNew = false)
        {
            listSize = _listSize;
            auto_id = _auto_id;
            mode_setting = _mode_setting;
            mode_auto_count_check = _mode_auto_count_check;

            ConfigFishSize._ListConfigFishSize.Add(this);

            if (isNew)
            {
                wnd.updateListFishSize(_auto_id);
            }
        }

        public static ConfigFishSize getFishSizeByIdAuto(string id)
        {
            foreach (ConfigFishSize data in ConfigFishSize._ListConfigFishSize)
            {
                if (data.auto_id == id)
                {
                    return data;
                }
            }
            return null;
        }

        public static void changeCheckboxActive(string idAuto, int size, bool isChecked)
        {
            foreach (ConfigFishSize data in ConfigFishSize._ListConfigFishSize)
            {
                if (data.auto_id == idAuto)
                {
                    foreach (FishSize fishSize in data.listSize)
                    {
                        if (fishSize.size == size)
                        {
                            database db = new database();
                            fishSize.isChecked = isChecked;
                            wnd.updateListFishSize(idAuto);
                            int status = isChecked ? 1 : 0;
                            string txtQuery = $"UPDATE FishSize SET status='{status}' WHERE _id='{fishSize._id}'";
                            try
                            {
                                db.ExecuteQuery(txtQuery);
                            } catch { }
                            break;
                        }
                    }
                    break;
                }
            }
        }

        public static void removeConfigOneEmu(string idAuto)
        {
            foreach (ConfigFishSize data in ConfigFishSize._ListConfigFishSize)
            {
                if (data.auto_id == idAuto)
                {
                    database db = new database();
                    foreach (FishSize fishSize in data.listSize)
                    {
                       try
                       {
                            string txtQuery = $"DELETE FROM FishSize WHERE _id='{fishSize._id}'";
                            db.ExecuteQuery(txtQuery);
                        } catch { }
                    }
                }
            }
                
        }

        public static void changeMinMax(string id, int value, string type = "max")
        {
            database db = new database();
            string txtQuery = $"UPDATE FishSize SET {type}={value} WHERE _id='{id}'";
            try
            {
                db.ExecuteQuery(txtQuery);
            }
            catch { }
        }

        public static void changeMinMaxAuto(string auto_id, int size, int valueChange)
        {
            foreach (ConfigFishSize config in _ListConfigFishSize)
            {
                if (config.auto_id == auto_id)
                {
                    // Cập nhật lại size bỏ bóng cá
                    if((!config.listSize[size - 1].disable || config.listSize[size - 1].max < valueChange) && valueChange < config.listSize[size].min)
                    {
                        config.listSize[size - 1].max = valueChange;
                        config.listSize[size].min = valueChange + 1;
                        updateDBMinMax(config.listSize[size - 1].min, config.listSize[size - 1].max, config.listSize[size - 1]._id);
                        updateDBMinMax(config.listSize[size].min, config.listSize[size].max, config.listSize[size]._id);
                        for (int i = size; i < 6; i++)
                        {
                            if (config.listSize[i].max <= config.listSize[i].min)
                            {
                                config.listSize[i].max = config.listSize[i - 1].max * 2;
                                config.listSize[i + 1].min = config.listSize[i].max + 1;
                                updateDBMinMax(config.listSize[i].min, config.listSize[i].max, config.listSize[i]._id);
                                updateDBMinMax(config.listSize[i + 1].min, config.listSize[i + 1].max, config.listSize[i]._id);
                            }
                            else
                            {
                                break;
                            }
                        }
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            wnd.updateListFishSize(config.auto_id);
                        });
                    }
                    config.listSize[size - 1].disable = true;
                    if(config.mode_auto_count_check == 0)
                    {
                        config.listSize[3].max = (int)((double)valueChange * (double)(6 / size * 2));
                        config.listSize[3].min = config.listSize[2].max + 1;
                        updateDBMinMax(config.listSize[3].min, config.listSize[3].max, config.listSize[3]._id);

                        config.listSize[4].max = config.listSize[3].max * 4;
                        config.listSize[4].min = config.listSize[3].max + 1;
                        updateDBMinMax(config.listSize[4].min, config.listSize[4].max, config.listSize[4]._id);

                        config.listSize[3].disable = true;
                        config.listSize[4].disable = true;
                        config.listSize[5].disable = true;
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            wnd.updateListFishSize(config.auto_id);
                        });
                    }
                    config.mode_auto_count_check++;
                    break;
                }
            }
        }

        public static void updateDBMinMax(int min, int max, string _id)
        {
            database db = new database();
            string txtQuery = $"UPDATE FishSize SET max={max}, min={min} WHERE _id='{_id}'";
            try
            {
                db.ExecuteQuery(txtQuery);
            }
            catch { }
        }

        public static bool checkDisableAll(string auto_id)
        {
            foreach (ConfigFishSize config in _ListConfigFishSize)
            {
                if (config.auto_id == auto_id)
                {
                    foreach(FishSize size in config.listSize)
                    {
                        if(size.disable == false)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public static void changeModeFS(string id, string mode)
        {
            database db = new database();
            foreach (ConfigFishSize config in _ListConfigFishSize)
            {
                if(config.auto_id == id)
                {
                    config.mode_setting = mode;
                    if (mode == "default")
                    {
                        int index = 0;
                        config.mode_auto_count_check = -1;
                        foreach (FishSizeLite size in getConfigDefault(config.auto_id))
                        {
                            config.listSize[index].isChecked = Convert.ToBoolean(size.isChecked);
                            config.listSize[index].max = size.max;
                            config.listSize[index].min = size.min;
                            config.listSize[index].disable = true;
                            string txtQuery = $"UPDATE FishSize SET status={size.isChecked}, max={size.max}, min={size.min}, setting='default' WHERE _id='{config.listSize[index]._id}'";
                            try
                            {
                                db.ExecuteQuery(txtQuery);
                            }
                            catch { }
                            index++;
                        }
                        wnd.updateListFishSize(config.auto_id);
                    } else if(mode == "custom")
                    {
                        int index = 0;
                        config.mode_auto_count_check = -1;
                        foreach (FishSizeLite size in getConfigDefault(config.auto_id))
                        {
                            config.listSize[index].disable = true;
                            string txtQuery = $"UPDATE FishSize SET setting='custom' WHERE _id='{config.listSize[index]._id}'";
                            try
                            {
                                db.ExecuteQuery(txtQuery);
                            }
                            catch { }
                            index++;
                        }
                        wnd.updateListFishSize(config.auto_id);
                    } else if(mode == "auto")
                    {
                        int index = 0;
                        config.mode_auto_count_check = 0;
                        foreach (FishSizeLite size in getConfigDefault(config.auto_id))
                        {
                            //config.listSize[index].isChecked = Convert.ToBoolean(size.isChecked);
                            config.listSize[index].max = size.max;
                            config.listSize[index].min = size.min;
                            config.listSize[index].disable = false;
                            string txtQuery = $"UPDATE FishSize SET max={size.max}, min={size.min}, setting='auto' WHERE _id='{config.listSize[index]._id}'";
                            try
                            {
                                db.ExecuteQuery(txtQuery);
                            }
                            catch { }
                            index++;
                        }
                        wnd.updateListFishSize(config.auto_id);
                    } else
                    {
                        if(getConfigByDeviceId(mode) != null)
                        {
                            int index = 0;
                            config.mode_auto_count_check = -1;
                            foreach (FishSizeLite size in getConfigByDeviceId(mode))
                            {
                                config.listSize[index].isChecked = Convert.ToBoolean(size.isChecked);
                                config.listSize[index].max = size.max;
                                config.listSize[index].min = size.min;
                                config.listSize[index].disable = true;
                                string txtQuery = $"UPDATE FishSize SET status={size.isChecked}, max={size.max}, min={size.min}, setting='{mode}' WHERE _id='{config.listSize[index]._id}'";
                                try
                                {
                                    db.ExecuteQuery(txtQuery);
                                }
                                catch { }
                                index++;
                            }
                            wnd.updateListFishSize(config.auto_id);
                        }
                    }
                    break;
                }
            }
        }

        public static List<FishSizeLite> getConfigDefault(string auto_id)
        {
            emulator configEmu = emulator._EmulatorList.SingleOrDefault(r => r._id == auto_id);
            string emulator_type = configEmu.emulator_type;
            int handleWidth = 960;
            if (emulator_type == "memu" || emulator_type == "nox" || emulator_type == "ldplayer")
            {
                IntPtr win = AutoControl.FindWindowHandle(null, configEmu.emulator_name);
                IntPtr handle = AutoControl.FindHandle(win, "subWin", null);
                Image handleGetSize = CaptureHelper.CaptureWindow(handle);
                handleWidth = handleGetSize.Width == 1 ? handleWidth : handleGetSize.Width;
            }
            else if (emulator_type == "bluestacks")
            {
                IntPtr win = AutoControl.FindWindowHandle(null, configEmu.emulator_name);
                IntPtr handle = AutoControl.FindHandle(win, "BlueStacksApp", null);
                Image handleGetSize = CaptureHelper.CaptureWindow(handle);
                handleWidth = handleGetSize.Width == 1 ? handleWidth : handleGetSize.Width;
            }

            List<FishSizeLite> defaultConfig = new List<FishSizeLite>();
            int size1Max = (int)((double)(200 * handleWidth) / (double)960);
            int size2Max = (int)((double)(400 * handleWidth) / (double)960);
            int size3Max = (int)((double)(800 * handleWidth) / (double)960);
            int size4Max = (int)((double)(1600 * handleWidth) / (double)960);
            int size5Max = (int)((double)(6400 * handleWidth) / (double)960);
            FishSizeLite size1 = new FishSizeLite(0, 1, size1Max);
            FishSizeLite size2 = new FishSizeLite(0, size1Max + 1, size2Max);
            FishSizeLite size3 = new FishSizeLite(0, size2Max + 1, size3Max);
            FishSizeLite size4 = new FishSizeLite(0, size3Max + 1, size4Max);
            FishSizeLite size5 = new FishSizeLite(0, size4Max + 1, size5Max);
            FishSizeLite size6 = new FishSizeLite(0, size5Max + 1, 99999);
            defaultConfig.Add(size1);
            defaultConfig.Add(size2);
            defaultConfig.Add(size3);
            defaultConfig.Add(size4);
            defaultConfig.Add(size5);
            defaultConfig.Add(size6);
            return defaultConfig;
        }

        
        public static List<FishSizeLite> getConfigByDeviceId(string id)
        {
            database db = new database();
            List<FishSizeLite> defaultConfig = new List<FishSizeLite>();
            var dataFishSize = db.getDataFishSize(id);
            if (dataFishSize.Rows.Count == 6)
            {
                foreach (DataRow rowFishSize in dataFishSize.Rows)
                {
                    int min_FishSize = int.Parse(rowFishSize["min"].ToString());
                    int max_FishSize = int.Parse(rowFishSize["max"].ToString());
                    int status_FishSize = int.Parse(rowFishSize["status"].ToString());
                    defaultConfig.Add(new FishSizeLite(status_FishSize, min_FishSize, max_FishSize));
                }
                return defaultConfig;
            }
            return null;
        }
    }
}
