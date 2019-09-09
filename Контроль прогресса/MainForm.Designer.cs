namespace Контроль_прогресса
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series37 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series38 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series39 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series40 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series41 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series42 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button5 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.действияСДневникамиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.определитьДневникПоУмолчаниюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьЗаписьОТренировкеВДневникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьДневникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.начатьНовыйДневникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button6 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button7 = new System.Windows.Forms.Button();
            this.написатьАвторуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.скачатьМатериалыПоПауэрлифтингуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
            chartArea7.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea7);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend7.Name = "Legend1";
            this.chart1.Legends.Add(legend7);
            this.chart1.Location = new System.Drawing.Point(0, 24);
            this.chart1.Name = "chart1";
            this.chart1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            series37.BorderWidth = 2;
            series37.ChartArea = "ChartArea1";
            series37.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series37.Color = System.Drawing.Color.Blue;
            series37.Legend = "Legend1";
            series37.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series37.Name = "Присед";
            series37.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series38.BorderWidth = 2;
            series38.ChartArea = "ChartArea1";
            series38.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series38.Color = System.Drawing.Color.Lime;
            series38.Legend = "Legend1";
            series38.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series38.Name = "Жим";
            series38.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series39.BorderWidth = 2;
            series39.ChartArea = "ChartArea1";
            series39.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series39.Color = System.Drawing.Color.Red;
            series39.Legend = "Legend1";
            series39.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series39.Name = "Тяга";
            series39.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series40.BorderWidth = 3;
            series40.ChartArea = "ChartArea1";
            series40.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series40.Color = System.Drawing.Color.Black;
            series40.Legend = "Legend1";
            series40.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series40.Name = "Сумма";
            series40.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series41.BorderWidth = 2;
            series41.ChartArea = "ChartArea1";
            series41.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series41.Color = System.Drawing.Color.Green;
            series41.Legend = "Legend1";
            series41.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series41.Name = "Тоннаж";
            series41.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series41.YValuesPerPoint = 2;
            series42.BorderWidth = 3;
            series42.ChartArea = "ChartArea1";
            series42.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series42.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            series42.Legend = "Legend1";
            series42.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series42.Name = "Собственный вес";
            series42.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            this.chart1.Series.Add(series37);
            this.chart1.Series.Add(series38);
            this.chart1.Series.Add(series39);
            this.chart1.Series.Add(series40);
            this.chart1.Series.Add(series41);
            this.chart1.Series.Add(series42);
            this.chart1.Size = new System.Drawing.Size(850, 572);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(42, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(16, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "Добавить запись в дневник";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(715, 168);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 67);
            this.button3.TabIndex = 3;
            this.button3.Text = "Обновить график результатов";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 98);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(24, 28);
            this.button4.TabIndex = 4;
            this.button4.Text = "Показать дневник";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 497);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(549, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Замечания и советы по улучшению дневника принимаю по адресу ";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.White;
            this.linkLabel1.Location = new System.Drawing.Point(412, 524);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(252, 19);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://vk.com/romandisease";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(33, 132);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(631, 290);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 52);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(24, 27);
            this.button5.TabIndex = 9;
            this.button5.Text = "Опеределить дневник по умолчанию";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.действияСДневникамиToolStripMenuItem,
            this.написатьАвторуToolStripMenuItem,
            this.скачатьМатериалыПоПауэрлифтингуToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(850, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // действияСДневникамиToolStripMenuItem
            // 
            this.действияСДневникамиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.определитьДневникПоУмолчаниюToolStripMenuItem,
            this.добавитьЗаписьОТренировкеВДневникToolStripMenuItem,
            this.открытьДневникToolStripMenuItem,
            this.начатьНовыйДневникToolStripMenuItem});
            this.действияСДневникамиToolStripMenuItem.Name = "действияСДневникамиToolStripMenuItem";
            this.действияСДневникамиToolStripMenuItem.Size = new System.Drawing.Size(149, 20);
            this.действияСДневникамиToolStripMenuItem.Text = "Действия с дневниками";
            // 
            // определитьДневникПоУмолчаниюToolStripMenuItem
            // 
            this.определитьДневникПоУмолчаниюToolStripMenuItem.Name = "определитьДневникПоУмолчаниюToolStripMenuItem";
            this.определитьДневникПоУмолчаниюToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.определитьДневникПоУмолчаниюToolStripMenuItem.Text = "Определить дневник по умолчанию";
            this.определитьДневникПоУмолчаниюToolStripMenuItem.Click += new System.EventHandler(this.определитьДневникПоУмолчаниюToolStripMenuItem_Click);
            // 
            // добавитьЗаписьОТренировкеВДневникToolStripMenuItem
            // 
            this.добавитьЗаписьОТренировкеВДневникToolStripMenuItem.Name = "добавитьЗаписьОТренировкеВДневникToolStripMenuItem";
            this.добавитьЗаписьОТренировкеВДневникToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.добавитьЗаписьОТренировкеВДневникToolStripMenuItem.Text = "Добавить запись о тренировке в дневник";
            this.добавитьЗаписьОТренировкеВДневникToolStripMenuItem.Click += new System.EventHandler(this.добавитьЗаписьОТренировкеВДневникToolStripMenuItem_Click);
            // 
            // открытьДневникToolStripMenuItem
            // 
            this.открытьДневникToolStripMenuItem.Name = "открытьДневникToolStripMenuItem";
            this.открытьДневникToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.открытьДневникToolStripMenuItem.Text = "Открыть дневник";
            this.открытьДневникToolStripMenuItem.Click += new System.EventHandler(this.открытьДневникToolStripMenuItem_Click);
            // 
            // начатьНовыйДневникToolStripMenuItem
            // 
            this.начатьНовыйДневникToolStripMenuItem.Name = "начатьНовыйДневникToolStripMenuItem";
            this.начатьНовыйДневникToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.начатьНовыйДневникToolStripMenuItem.Text = "Начать новый дневник";
            this.начатьНовыйДневникToolStripMenuItem.Click += new System.EventHandler(this.начатьНовыйДневникToolStripMenuItem_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(716, 320);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(121, 72);
            this.button6.TabIndex = 12;
            this.button6.Text = "Быстрая оценка ПМ";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(715, 241);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(123, 73);
            this.button7.TabIndex = 13;
            this.button7.Text = "Мои макс. результаты";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // написатьАвторуToolStripMenuItem
            // 
            this.написатьАвторуToolStripMenuItem.Name = "написатьАвторуToolStripMenuItem";
            this.написатьАвторуToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.написатьАвторуToolStripMenuItem.Text = "Написать автору";
            this.написатьАвторуToolStripMenuItem.Click += new System.EventHandler(this.написатьАвторуToolStripMenuItem_Click);
            // 
            // скачатьМатериалыПоПауэрлифтингуToolStripMenuItem
            // 
            this.скачатьМатериалыПоПауэрлифтингуToolStripMenuItem.Name = "скачатьМатериалыПоПауэрлифтингуToolStripMenuItem";
            this.скачатьМатериалыПоПауэрлифтингуToolStripMenuItem.Size = new System.Drawing.Size(233, 20);
            this.скачатьМатериалыПоПауэрлифтингуToolStripMenuItem.Text = "Скачать материалы по пауэрлифтингу";
            this.скачатьМатериалыПоПауэрлифтингуToolStripMenuItem.Click += new System.EventHandler(this.скачатьМатериалыПоПауэрлифтингуToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 596);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Контроль прогресса по трём базовым движениям. Дм. ПА.";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button4;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.LinkLabel linkLabel1;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem действияСДневникамиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem определитьДневникПоУмолчаниюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьЗаписьОТренировкеВДневникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьДневникToolStripMenuItem;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ToolStripMenuItem начатьНовыйДневникToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.ToolStripMenuItem написатьАвторуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem скачатьМатериалыПоПауэрлифтингуToolStripMenuItem;
    }
}

