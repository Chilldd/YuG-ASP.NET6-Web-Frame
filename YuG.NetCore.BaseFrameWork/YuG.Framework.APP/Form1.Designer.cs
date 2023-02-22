namespace YuG.Framework.APP
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.codeGeneratorsTab = new System.Windows.Forms.TabPage();
            this.codeGenerators = new System.Windows.Forms.TabControl();
            this.basicConfig = new System.Windows.Forms.TabPage();
            this.dbList = new System.Windows.Forms.ComboBox();
            this.text_DbList = new System.Windows.Forms.Label();
            this.dbSaveBtn = new System.Windows.Forms.Button();
            this.dbType = new System.Windows.Forms.ComboBox();
            this.text_DbType = new System.Windows.Forms.Label();
            this.dbConnection = new System.Windows.Forms.TextBox();
            this.text_DbConnection = new System.Windows.Forms.Label();
            this.buildEntity = new System.Windows.Forms.TabPage();
            this.buildServiceCode = new System.Windows.Forms.TabPage();
            this.appList = new System.Windows.Forms.TabControl();
            this.dbConnectionName = new System.Windows.Forms.TextBox();
            this.text_dbName = new System.Windows.Forms.Label();
            this.entityDbConnection = new System.Windows.Forms.ComboBox();
            this.text_EntityDbConnection = new System.Windows.Forms.Label();
            this.codeGeneratorsTab.SuspendLayout();
            this.codeGenerators.SuspendLayout();
            this.basicConfig.SuspendLayout();
            this.buildEntity.SuspendLayout();
            this.appList.SuspendLayout();
            this.SuspendLayout();
            // 
            // codeGeneratorsTab
            // 
            this.codeGeneratorsTab.Controls.Add(this.codeGenerators);
            this.codeGeneratorsTab.Location = new System.Drawing.Point(8, 51);
            this.codeGeneratorsTab.Name = "codeGeneratorsTab";
            this.codeGeneratorsTab.Padding = new System.Windows.Forms.Padding(3);
            this.codeGeneratorsTab.Size = new System.Drawing.Size(1048, 831);
            this.codeGeneratorsTab.TabIndex = 2;
            this.codeGeneratorsTab.Text = "代码生成器";
            this.codeGeneratorsTab.UseVisualStyleBackColor = true;
            // 
            // codeGenerators
            // 
            this.codeGenerators.Controls.Add(this.basicConfig);
            this.codeGenerators.Controls.Add(this.buildEntity);
            this.codeGenerators.Controls.Add(this.buildServiceCode);
            this.codeGenerators.Dock = System.Windows.Forms.DockStyle.Top;
            this.codeGenerators.Location = new System.Drawing.Point(3, 3);
            this.codeGenerators.Name = "codeGenerators";
            this.codeGenerators.Padding = new System.Drawing.Point(10, 10);
            this.codeGenerators.SelectedIndex = 0;
            this.codeGenerators.Size = new System.Drawing.Size(1042, 847);
            this.codeGenerators.TabIndex = 0;
            // 
            // basicConfig
            // 
            this.basicConfig.Controls.Add(this.dbConnectionName);
            this.basicConfig.Controls.Add(this.text_dbName);
            this.basicConfig.Controls.Add(this.dbList);
            this.basicConfig.Controls.Add(this.text_DbList);
            this.basicConfig.Controls.Add(this.dbSaveBtn);
            this.basicConfig.Controls.Add(this.dbType);
            this.basicConfig.Controls.Add(this.text_DbType);
            this.basicConfig.Controls.Add(this.dbConnection);
            this.basicConfig.Controls.Add(this.text_DbConnection);
            this.basicConfig.Location = new System.Drawing.Point(8, 51);
            this.basicConfig.Name = "basicConfig";
            this.basicConfig.Padding = new System.Windows.Forms.Padding(3);
            this.basicConfig.Size = new System.Drawing.Size(1026, 788);
            this.basicConfig.TabIndex = 0;
            this.basicConfig.Text = "基础配置";
            this.basicConfig.UseVisualStyleBackColor = true;
            // 
            // dbList
            // 
            this.dbList.DisplayMember = "Text";
            this.dbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dbList.FormattingEnabled = true;
            this.dbList.Location = new System.Drawing.Point(259, 54);
            this.dbList.Name = "dbList";
            this.dbList.Size = new System.Drawing.Size(400, 32);
            this.dbList.TabIndex = 6;
            this.dbList.ValueMember = "Value";
            this.dbList.SelectedIndexChanged += new System.EventHandler(this.dbList_SelectedIndexChanged);
            // 
            // text_DbList
            // 
            this.text_DbList.AutoSize = true;
            this.text_DbList.Location = new System.Drawing.Point(40, 57);
            this.text_DbList.Name = "text_DbList";
            this.text_DbList.Size = new System.Drawing.Size(202, 24);
            this.text_DbList.TabIndex = 5;
            this.text_DbList.Text = "已保存数据源连接";
            // 
            // dbSaveBtn
            // 
            this.dbSaveBtn.Location = new System.Drawing.Point(529, 313);
            this.dbSaveBtn.Name = "dbSaveBtn";
            this.dbSaveBtn.Size = new System.Drawing.Size(130, 56);
            this.dbSaveBtn.TabIndex = 4;
            this.dbSaveBtn.Text = "保存配置";
            this.dbSaveBtn.UseVisualStyleBackColor = true;
            this.dbSaveBtn.Click += new System.EventHandler(this.dbSaveBtn_Click);
            // 
            // dbType
            // 
            this.dbType.DisplayMember = "Text";
            this.dbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dbType.FormattingEnabled = true;
            this.dbType.Location = new System.Drawing.Point(259, 252);
            this.dbType.Name = "dbType";
            this.dbType.Size = new System.Drawing.Size(400, 32);
            this.dbType.TabIndex = 3;
            this.dbType.ValueMember = "Value";
            // 
            // text_DbType
            // 
            this.text_DbType.AutoSize = true;
            this.text_DbType.Location = new System.Drawing.Point(112, 252);
            this.text_DbType.Name = "text_DbType";
            this.text_DbType.Size = new System.Drawing.Size(130, 24);
            this.text_DbType.TabIndex = 2;
            this.text_DbType.Text = "数据库类型";
            // 
            // dbConnection
            // 
            this.dbConnection.Location = new System.Drawing.Point(259, 179);
            this.dbConnection.Name = "dbConnection";
            this.dbConnection.Size = new System.Drawing.Size(400, 35);
            this.dbConnection.TabIndex = 1;
            // 
            // text_DbConnection
            // 
            this.text_DbConnection.AutoSize = true;
            this.text_DbConnection.Location = new System.Drawing.Point(40, 185);
            this.text_DbConnection.Name = "text_DbConnection";
            this.text_DbConnection.Size = new System.Drawing.Size(202, 24);
            this.text_DbConnection.TabIndex = 0;
            this.text_DbConnection.Text = "数据库连接字符串";
            // 
            // buildEntity
            // 
            this.buildEntity.Controls.Add(this.entityDbConnection);
            this.buildEntity.Controls.Add(this.text_EntityDbConnection);
            this.buildEntity.Location = new System.Drawing.Point(8, 51);
            this.buildEntity.Name = "buildEntity";
            this.buildEntity.Padding = new System.Windows.Forms.Padding(3);
            this.buildEntity.Size = new System.Drawing.Size(1026, 788);
            this.buildEntity.TabIndex = 1;
            this.buildEntity.Text = "实体生成";
            this.buildEntity.UseVisualStyleBackColor = true;
            // 
            // buildServiceCode
            // 
            this.buildServiceCode.Location = new System.Drawing.Point(8, 51);
            this.buildServiceCode.Name = "buildServiceCode";
            this.buildServiceCode.Padding = new System.Windows.Forms.Padding(3);
            this.buildServiceCode.Size = new System.Drawing.Size(1026, 788);
            this.buildServiceCode.TabIndex = 2;
            this.buildServiceCode.Text = "业务代码生成";
            this.buildServiceCode.UseVisualStyleBackColor = true;
            // 
            // appList
            // 
            this.appList.Controls.Add(this.codeGeneratorsTab);
            this.appList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appList.Location = new System.Drawing.Point(0, 0);
            this.appList.Name = "appList";
            this.appList.Padding = new System.Drawing.Point(10, 10);
            this.appList.SelectedIndex = 0;
            this.appList.Size = new System.Drawing.Size(1064, 890);
            this.appList.TabIndex = 0;
            // 
            // dbConnectionName
            // 
            this.dbConnectionName.Location = new System.Drawing.Point(259, 119);
            this.dbConnectionName.Name = "dbConnectionName";
            this.dbConnectionName.Size = new System.Drawing.Size(400, 35);
            this.dbConnectionName.TabIndex = 8;
            // 
            // text_dbName
            // 
            this.text_dbName.AutoSize = true;
            this.text_dbName.Location = new System.Drawing.Point(136, 122);
            this.text_dbName.Name = "text_dbName";
            this.text_dbName.Size = new System.Drawing.Size(106, 24);
            this.text_dbName.TabIndex = 7;
            this.text_dbName.Text = "连接名称";
            // 
            // entityDbConnection
            // 
            this.entityDbConnection.DisplayMember = "Key";
            this.entityDbConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.entityDbConnection.FormattingEnabled = true;
            this.entityDbConnection.Location = new System.Drawing.Point(260, 60);
            this.entityDbConnection.Name = "entityDbConnection";
            this.entityDbConnection.Size = new System.Drawing.Size(400, 32);
            this.entityDbConnection.TabIndex = 8;
            this.entityDbConnection.ValueMember = "Value";
            // 
            // text_EntityDbConnection
            // 
            this.text_EntityDbConnection.AutoSize = true;
            this.text_EntityDbConnection.Location = new System.Drawing.Point(84, 64);
            this.text_EntityDbConnection.Name = "text_EntityDbConnection";
            this.text_EntityDbConnection.Size = new System.Drawing.Size(130, 24);
            this.text_EntityDbConnection.TabIndex = 7;
            this.text_EntityDbConnection.Text = "数据源连接";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 890);
            this.Controls.Add(this.appList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.codeGeneratorsTab.ResumeLayout(false);
            this.codeGenerators.ResumeLayout(false);
            this.basicConfig.ResumeLayout(false);
            this.basicConfig.PerformLayout();
            this.buildEntity.ResumeLayout(false);
            this.buildEntity.PerformLayout();
            this.appList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage codeGeneratorsTab;
        private System.Windows.Forms.TabControl codeGenerators;
        private System.Windows.Forms.TabPage basicConfig;
        private System.Windows.Forms.TabPage buildEntity;
        private System.Windows.Forms.TabControl appList;
        private System.Windows.Forms.TabPage buildServiceCode;
        private System.Windows.Forms.Label text_DbConnection;
        private System.Windows.Forms.TextBox dbConnection;
        private System.Windows.Forms.Label text_DbType;
        private System.Windows.Forms.ComboBox dbType;
        private System.Windows.Forms.Button dbSaveBtn;
        private System.Windows.Forms.ComboBox dbList;
        private System.Windows.Forms.Label text_DbList;
        private System.Windows.Forms.TextBox dbConnectionName;
        private System.Windows.Forms.Label text_dbName;
        private System.Windows.Forms.ComboBox entityDbConnection;
        private System.Windows.Forms.Label text_EntityDbConnection;
    }
}

