using YuG.Framework.APP.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YuG.Framework;
using System.Web.UI.WebControls;

namespace YuG.Framework.APP
{
    public partial class Form1 : Form
    {
        private static string dbPath = $@"{Environment.CurrentDirectory}\fisk_app.db";
        private readonly static SqlSugarScope _client = new SqlSugarScope(new ConnectionConfig()
        {
            ConnectionString = $@"Data Source={dbPath}",
            DbType = SqlSugar.DbType.Sqlite,
            IsAutoCloseConnection = true
        });

        public Form1()
        {
            InitializeComponent();
            CheckDb();
            InitBind_DbType_Combox();
            InitBind_DbList_Combox();
        }

        /// <summary>
        /// 检查配置数据库是否存在，不存在则创建
        /// </summary>
        private void CheckDb()
        {
            if (!File.Exists(dbPath))
            {
                _client.DbMaintenance.CreateDatabase();
                _client.CodeFirst.InitTables(typeof(BasicConfigEntity));
                MessageBox.Show("Db创建成功！");
            }
        }

        /// <summary>
        /// 绑定数据库类型
        /// </summary>
        private void InitBind_DbType_Combox()
        {
            var list = new List<ListItem>();
            list.Add(new ListItem("MySql", "0"));
            list.Add(new ListItem("SqlServer", "1"));
            list.Add(new ListItem("Sqlite", "2"));
            dbType.DataSource = list;
        }

        private async void InitBind_DbList_Combox()
        {
            var data = await _client.Queryable<BasicConfigEntity>()
                                    .Select(e => new
                                    {
                                        e.ID,
                                        e.Name
                                    })
                                    .ToListAsync();
            var res = data.Select(e => new ListItem() { Text = e.Name, Value = e.ID.ToString() }).ToList();
            res.Insert(0, new ListItem() { Text = "请选择", Value = "0" });

            dbList.DataSource = res;
            entityDbConnection.DataSource = res;
        }


        /// <summary>
        /// 保存Db配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void dbSaveBtn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dbList.SelectedValue ?? 0);

            bool nameIsExists = await _client.Queryable<BasicConfigEntity>()
                                            .Where(a => a.Name == dbConnectionName.Text)
                                            .WhereIF(id != 0, a => a.ID != id)
                                            .AnyAsync();
            if (nameIsExists)
            {
                MessageBox.Show("名称已存在！");
                return;
            }

            if (id == 0)
            {
                var model = new BasicConfigEntity();
                model.Name = dbConnectionName.Text;
                model.DbType = Convert.ToInt32(dbType.SelectedValue);
                model.Connection = dbConnection.Text;
                await _client.Insertable(model).ExecuteCommandAsync();
            }
            else
            {
                var model = await _client.Queryable<BasicConfigEntity>()
                                         .Where(a => a.ID == id)
                                         .FirstAsync();
                model.Name = dbConnectionName.Text;
                model.DbType = Convert.ToInt32(dbType.SelectedValue);
                model.Connection = dbConnection.Text;
                await _client.Updateable(model).ExecuteCommandAsync();
            }

            MessageBox.Show("保存成功！");
        }

        private async void dbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dbList.SelectedValue ?? 0);
            if (id != 0)
            {
                var model = await _client.Queryable<BasicConfigEntity>()
                                         .Where(a => a.ID == id)
                                         .FirstAsync();
                dbConnectionName.Text = model.Name;
                dbConnection.Text = model.Connection;
                dbType.SelectedValue = (dbType.DataSource as List<ListItem>).Where(a => a.Value == model.DbType.ToString()).FirstOrDefault()?.Value;
            }
            else
            {
                dbConnectionName.Text = "";
                dbConnection.Text = "";
                dbType.SelectedIndex = 0;
            }
        }
    }
}
