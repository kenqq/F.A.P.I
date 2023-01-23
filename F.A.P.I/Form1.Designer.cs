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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            this.longepisode = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.episode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxepisode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchKeyword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fansub = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lastDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clearTime = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.button9 = new System.Windows.Forms.Button();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
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
            this.longepisode,
            this.episode,
            this.maxepisode,
            this.searchKeyword,
            this.fansub,
            this.lastDate,
            this.clearTime});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1276, 110);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // titleCN
            // 
            this.titleCN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.titleCN.DataPropertyName = "titleCN";
            this.titleCN.FillWeight = 45.45454F;
            this.titleCN.HeaderText = "标题";
            this.titleCN.MinimumWidth = 200;
            this.titleCN.Name = "titleCN";
            this.titleCN.ReadOnly = true;
            this.titleCN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.titleCN.Width = 200;
            // 
            // titleJP
            // 
            this.titleJP.DataPropertyName = "titleJP";
            this.titleJP.HeaderText = "titleJP";
            this.titleJP.Name = "titleJP";
            this.titleJP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.titleJP.Visible = false;
            // 
            // officalSite
            // 
            this.officalSite.DataPropertyName = "officalSite";
            this.officalSite.HeaderText = "officalSite";
            this.officalSite.Name = "officalSite";
            this.officalSite.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.officalSite.Visible = false;
            // 
            // weekDayJP
            // 
            this.weekDayJP.DataPropertyName = "weekDayJP";
            this.weekDayJP.HeaderText = "weekDayJP";
            this.weekDayJP.Name = "weekDayJP";
            this.weekDayJP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.weekDayJP.Visible = false;
            // 
            // weekDayCN
            // 
            this.weekDayCN.DataPropertyName = "weekDayCN";
            this.weekDayCN.HeaderText = "weekDayCN";
            this.weekDayCN.Name = "weekDayCN";
            this.weekDayCN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.weekDayCN.Visible = false;
            // 
            // timeJP
            // 
            this.timeJP.DataPropertyName = "timeJP";
            this.timeJP.HeaderText = "timeJP";
            this.timeJP.Name = "timeJP";
            this.timeJP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.timeJP.Visible = false;
            // 
            // timeCN
            // 
            this.timeCN.DataPropertyName = "timeCN";
            this.timeCN.HeaderText = "timeCN";
            this.timeCN.Name = "timeCN";
            this.timeCN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.timeCN.Visible = false;
            // 
            // newBgm
            // 
            this.newBgm.DataPropertyName = "newBgm";
            this.newBgm.HeaderText = "newBgm";
            this.newBgm.Name = "newBgm";
            this.newBgm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.newBgm.Visible = false;
            // 
            // isOrderRabbit
            // 
            this.isOrderRabbit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.isOrderRabbit.DataPropertyName = "isOrderRabbit";
            this.isOrderRabbit.FalseValue = "0";
            this.isOrderRabbit.HeaderText = "是否shimarin?";
            this.isOrderRabbit.MinimumWidth = 90;
            this.isOrderRabbit.Name = "isOrderRabbit";
            this.isOrderRabbit.TrueValue = "1";
            this.isOrderRabbit.Width = 90;
            // 
            // longepisode
            // 
            this.longepisode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.longepisode.DataPropertyName = "longepisode";
            this.longepisode.FalseValue = "0";
            this.longepisode.HeaderText = "年/半年 番";
            this.longepisode.MinimumWidth = 80;
            this.longepisode.Name = "longepisode";
            this.longepisode.TrueValue = "1";
            this.longepisode.Width = 80;
            // 
            // episode
            // 
            this.episode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.episode.DataPropertyName = "episode";
            this.episode.HeaderText = "集数";
            this.episode.MinimumWidth = 40;
            this.episode.Name = "episode";
            this.episode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.episode.Width = 40;
            // 
            // maxepisode
            // 
            this.maxepisode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.maxepisode.DataPropertyName = "maxepisode";
            this.maxepisode.HeaderText = "最大集数";
            this.maxepisode.MinimumWidth = 80;
            this.maxepisode.Name = "maxepisode";
            this.maxepisode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.maxepisode.Width = 80;
            // 
            // searchKeyword
            // 
            this.searchKeyword.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.searchKeyword.DataPropertyName = "searchKeyword";
            this.searchKeyword.FillWeight = 118.1818F;
            this.searchKeyword.HeaderText = "搜索关键字";
            this.searchKeyword.MinimumWidth = 200;
            this.searchKeyword.Name = "searchKeyword";
            this.searchKeyword.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fansub
            // 
            this.fansub.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.fansub.DataPropertyName = "fansub";
            this.fansub.FillWeight = 118.1818F;
            this.fansub.HeaderText = "字幕组";
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
            this.fansub.Width = 150;
            // 
            // lastDate
            // 
            this.lastDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.lastDate.DataPropertyName = "lastDate";
            this.lastDate.HeaderText = "下次执行时间";
            this.lastDate.MinimumWidth = 150;
            this.lastDate.Name = "lastDate";
            this.lastDate.ReadOnly = true;
            this.lastDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.lastDate.Width = 150;
            // 
            // clearTime
            // 
            this.clearTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.clearTime.HeaderText = "清空时间";
            this.clearTime.MinimumWidth = 60;
            this.clearTime.Name = "clearTime";
            this.clearTime.Width = 60;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1276, 110);
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
            this.splitContainer1.Size = new System.Drawing.Size(1276, 162);
            this.splitContainer1.SplitterDistance = 48;
            this.splitContainer1.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1276, 48);
            this.panel2.TabIndex = 12;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel6);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Size = new System.Drawing.Size(1276, 48);
            this.splitContainer2.SplitterDistance = 700;
            this.splitContainer2.TabIndex = 17;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button11);
            this.panel3.Controls.Add(this.button10);
            this.panel3.Controls.Add(this.button6);
            this.panel3.Controls.Add(this.checkBox2);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.checkBox1);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.comboBox1);
            this.panel3.Controls.Add(this.comboBox3);
            this.panel3.Controls.Add(this.comboBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(700, 48);
            this.panel3.TabIndex = 0;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(173, 3);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(70, 44);
            this.button11.TabIndex = 29;
            this.button11.Text = " ";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(88, 3);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(85, 44);
            this.button10.TabIndex = 28;
            this.button10.Text = "Refresh Title!";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(414, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(29, 43);
            this.button6.TabIndex = 27;
            this.button6.Text = "转移";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(563, 30);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 26;
            this.checkBox2.Text = "全选";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(485, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(31, 23);
            this.button5.TabIndex = 25;
            this.button5.Text = "del";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(446, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(34, 23);
            this.button4.TabIndex = 24;
            this.button4.Text = "add";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(522, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(39, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "help";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(446, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "设置下载软件";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(563, 9);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(90, 16);
            this.checkBox1.TabIndex = 21;
            this.checkBox1.Text = " 只显示订阅";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 44);
            this.button1.TabIndex = 17;
            this.button1.Text = "FAPI!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.comboBox1.Location = new System.Drawing.Point(248, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 20);
            this.comboBox1.TabIndex = 18;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(353, 26);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(60, 20);
            this.comboBox3.TabIndex = 20;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(353, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(60, 20);
            this.comboBox2.TabIndex = 19;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel4);
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(240, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(332, 48);
            this.panel6.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.checkBox5);
            this.panel4.Controls.Add(this.checkBox3);
            this.panel4.Controls.Add(this.checkBox4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(91, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(102, 48);
            this.panel4.TabIndex = 16;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(0, 16);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(96, 16);
            this.checkBox5.TabIndex = 16;
            this.checkBox5.Text = "?分钟fapYi次";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBox3.Location = new System.Drawing.Point(0, 0);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(102, 16);
            this.checkBox3.TabIndex = 14;
            this.checkBox3.Text = "无视时间限制";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.checkBox4.Location = new System.Drawing.Point(0, 32);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(102, 16);
            this.checkBox4.TabIndex = 15;
            this.checkBox4.Text = "种子/磁链";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.button8);
            this.panel5.Controls.Add(this.button7);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(193, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(139, 48);
            this.panel5.TabIndex = 7;
            // 
            // button8
            // 
            this.button8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button8.Location = new System.Drawing.Point(0, 25);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(139, 23);
            this.button8.TabIndex = 13;
            this.button8.Text = "过滤(一行,空格分割)";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Dock = System.Windows.Forms.DockStyle.Top;
            this.button7.Location = new System.Drawing.Point(0, 0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(139, 25);
            this.button7.TabIndex = 12;
            this.button7.Text = "编辑字幕组";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.button9);
            this.panel7.Controls.Add(this.comboBox4);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(91, 48);
            this.panel7.TabIndex = 7;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(3, 26);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(82, 21);
            this.button9.TabIndex = 14;
            this.button9.Text = "hey!";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "dmhy(anoneko)",
            "dmhy(带cookies)",
            "acg.rip",
            "nya(新)",
            "dmhy临时版",
            "ktxp(R.I.P.)"});
            this.comboBox4.Location = new System.Drawing.Point(3, 3);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(82, 20);
            this.comboBox4.TabIndex = 13;
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("MS PMincho", 9F);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 48);
            this.label1.TabIndex = 11;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            this.label1.DoubleClick += new System.EventHandler(this.label1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 162);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.Name = "Form1";
            this.Text = "F.A.P.I.3 ver.0.0.3 2023新春版";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }





        #endregion

        public  System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleCN;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleJP;
        private System.Windows.Forms.DataGridViewTextBoxColumn officalSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn weekDayJP;
        private System.Windows.Forms.DataGridViewTextBoxColumn weekDayCN;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeJP;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeCN;
        private System.Windows.Forms.DataGridViewTextBoxColumn newBgm;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isOrderRabbit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn longepisode;
        private System.Windows.Forms.DataGridViewTextBoxColumn episode;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxepisode;
        private System.Windows.Forms.DataGridViewTextBoxColumn searchKeyword;
        private System.Windows.Forms.DataGridViewComboBoxColumn fansub;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastDate;
        private System.Windows.Forms.DataGridViewButtonColumn clearTime;
    }
}

