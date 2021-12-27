using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace Auto_Demo.services
{
    class database
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        private void SetConnection()
        {
            sql_con = new SQLiteConnection("Data Source=database/database.db;Version=3;New=False;Compress=True;");
        }

        public void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        public DataTable getDataTable(string tabelName)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = $"SELECT * FROM {tabelName}";
            this.DB = new SQLiteDataAdapter(CommandText, sql_con);
            this.DS.Reset();
            this.DB.Fill(this.DS);
            this.DT = this.DS.Tables[0];
            sql_con.Close();
            return this.DT;
        }

        public DataTable getDataFishSize(string id_auto)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = $"SELECT * FROM FishSize WHERE auto_id='{id_auto}' ORDER BY size ASC";
            this.DB = new SQLiteDataAdapter(CommandText, sql_con);
            this.DS.Reset();
            this.DB.Fill(this.DS);
            this.DT = this.DS.Tables[0];
            sql_con.Close();
            return this.DT;
        }

        public DataTable getDataFishingRodByIdAuto(string id_auto)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = $"SELECT * FROM FishingRod WHERE auto_id='{id_auto}'";
            this.DB = new SQLiteDataAdapter(CommandText, sql_con);
            this.DS.Reset();
            this.DB.Fill(this.DS);
            this.DT = this.DS.Tables[0];
            sql_con.Close();
            return this.DT;
        }

        public DataTable getDataConfigAutoByIdAuto(string id_auto)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = $"SELECT * FROM Config WHERE auto_id='{id_auto}'";
            this.DB = new SQLiteDataAdapter(CommandText, sql_con);
            this.DS.Reset();
            this.DB.Fill(this.DS);
            this.DT = this.DS.Tables[0];
            sql_con.Close();
            return this.DT;
        }

        public DataTable getDataCategoryFish(int language, int level, int words)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "";
            if (language == 0)
            {
                CommandText = $"SELECT * FROM Fish WHERE level={level} AND words_vi={words}";
            } else
            {
                CommandText = $"SELECT * FROM Fish WHERE level={level} AND words_en={words}";
            }
            this.DB = new SQLiteDataAdapter(CommandText, sql_con);
            this.DS.Reset();
            this.DB.Fill(this.DS);
            this.DT = this.DS.Tables[0];
            sql_con.Close();
            return this.DT;
        }
    }
}
