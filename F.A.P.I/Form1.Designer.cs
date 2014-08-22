namespace F.A.P.I
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.titleCN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleJP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.officalSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weekDayJP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weekDayCN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeJP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeCN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newBgm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isOrderRabbit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.episode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchKeyword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fansub = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "FAPI!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.titleCN,
            this.titleJP,
            this.officalSite,
            this.weekDayJP,
            this.weekDayCN,
            this.timeJP,
            this.timeCN,
            this.newBgm,
            this.isOrderRabbit,
            this.episode,
            this.searchKeyword,
            this.fansub});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(986, 110);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // titleCN
            // 
            this.titleCN.DataPropertyName = "titleCN";
            this.titleCN.FillWeight = 45.45454F;
            this.titleCN.HeaderText = "titleCN";
            this.titleCN.MinimumWidth = 200;
            this.titleCN.Name = "titleCN";
            this.titleCN.ReadOnly = true;
            // 
            // titleJP
            // 
            this.titleJP.DataPropertyName = "titleJP";
            this.titleJP.HeaderText = "titleJP";
            this.titleJP.Name = "titleJP";
            this.titleJP.Visible = false;
            // 
            // officalSite
            // 
            this.officalSite.DataPropertyName = "officalSite";
            this.officalSite.HeaderText = "officalSite";
            this.officalSite.Name = "officalSite";
            this.officalSite.Visible = false;
            // 
            // weekDayJP
            // 
            this.weekDayJP.DataPropertyName = "weekDayJP";
            this.weekDayJP.HeaderText = "weekDayJP";
            this.weekDayJP.Name = "weekDayJP";
            this.weekDayJP.Visible = false;
            // 
            // weekDayCN
            // 
            this.weekDayCN.DataPropertyName = "weekDayCN";
            this.weekDayCN.HeaderText = "weekDayCN";
            this.weekDayCN.Name = "weekDayCN";
            this.weekDayCN.Visible = false;
            // 
            // timeJP
            // 
            this.timeJP.DataPropertyName = "timeJP";
            this.timeJP.HeaderText = "timeJP";
            this.timeJP.Name = "timeJP";
            this.timeJP.Visible = false;
            // 
            // timeCN
            // 
            this.timeCN.DataPropertyName = "timeCN";
            this.timeCN.HeaderText = "timeCN";
            this.timeCN.Name = "timeCN";
            this.timeCN.Visible = false;
            // 
            // newBgm
            // 
            this.newBgm.DataPropertyName = "newBgm";
            this.newBgm.HeaderText = "newBgm";
            this.newBgm.Name = "newBgm";
            this.newBgm.Visible = false;
            // 
            // isOrderRabbit
            // 
            this.isOrderRabbit.DataPropertyName = "isOrderRabbit";
            this.isOrderRabbit.FalseValue = "0";
            this.isOrderRabbit.FillWeight = 118.1818F;
            this.isOrderRabbit.HeaderText = "isOrderRabbit";
            this.isOrderRabbit.MinimumWidth = 100;
            this.isOrderRabbit.Name = "isOrderRabbit";
            this.isOrderRabbit.TrueValue = "1";
            // 
            // episode
            // 
            this.episode.DataPropertyName = "episode";
            this.episode.HeaderText = "episode";
            this.episode.MinimumWidth = 60;
            this.episode.Name = "episode";
            // 
            // searchKeyword
            // 
            this.searchKeyword.DataPropertyName = "searchKeyword";
            this.searchKeyword.FillWeight = 118.1818F;
            this.searchKeyword.HeaderText = "searchKeyword";
            this.searchKeyword.MinimumWidth = 200;
            this.searchKeyword.Name = "searchKeyword";
            // 
            // fansub
            // 
            this.fansub.DataPropertyName = "fansub";
            this.fansub.FillWeight = 118.1818F;
            this.fansub.HeaderText = "fansub";
            this.fansub.Items.AddRange(new object[] {
            "极影字幕",
            "HKG字幕組",
            "梦幻恋樱字幕组",
            "千夏字幕组",
            "DHR動研字幕組",
            "动漫国字幕组",
            "轻之国度",
            "幻樱字幕组",
            "雪飘工作室",
            "SOSG字幕团",
            "WOLF字幕组",
            "澄空学园",
            "华盟字幕社",
            "恶魔岛字幕组",
            "异域动漫",
            "◆漫游FREEWIND工作室",
            "猪猪字幕组",
            "SGS曙光社",
            "X2字幕组",
            "琵琶行字幕组",
            "动音漫影",
            "悠哈C9字幕社",
            "动漫先锋字幕组",
            "KPDM字幕组",
            "流雲字幕組",
            "光荣字幕组"});
            this.fansub.MinimumWidth = 150;
            this.fansub.Name = "fansub";
            this.fansub.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.fansub.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Sun",
            "Mon",
            "Tue",
            "Wed",
            "Thu",
            "Fri",
            "Sat"});
            this.comboBox1.Location = new System.Drawing.Point(184, 15);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 20);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(287, 15);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(100, 20);
            this.comboBox2.TabIndex = 8;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(391, 15);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(100, 20);
            this.comboBox3.TabIndex = 9;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(986, 110);
            this.panel1.TabIndex = 10;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(986, 162);
            this.splitContainer1.SplitterDistance = 48;
            this.splitContainer1.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.comboBox3);
            this.panel2.Controls.Add(this.comboBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(986, 48);
            this.panel2.TabIndex = 12;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(620, 15);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(31, 23);
            this.button5.TabIndex = 15;
            this.button5.Text = "del";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(581, 15);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(34, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "add";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(657, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(39, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "help";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(494, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "downloadsoft";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(154, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(30, 16);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = " ";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(701, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "(⌒,_ゝ⌒)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 162);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "F.A.P.I.";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }





        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleCN;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleJP;
        private System.Windows.Forms.DataGridViewTextBoxColumn officalSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn weekDayJP;
        private System.Windows.Forms.DataGridViewTextBoxColumn weekDayCN;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeJP;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeCN;
        private System.Windows.Forms.DataGridViewTextBoxColumn newBgm;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isOrderRabbit;
        private System.Windows.Forms.DataGridViewTextBoxColumn episode;
        private System.Windows.Forms.DataGridViewTextBoxColumn searchKeyword;
        private System.Windows.Forms.DataGridViewComboBoxColumn fansub;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

