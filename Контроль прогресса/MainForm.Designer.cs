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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series49 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series50 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series51 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series52 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series53 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series54 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
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
            chartArea9.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea9);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend9.Name = "Legend1";
            this.chart1.Legends.Add(legend9);
            this.chart1.Location = new System.Drawing.Point(0, 24);
            this.chart1.Name = "chart1";
            this.chart1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            series49.BorderWidth = 2;
            series49.ChartArea = "ChartArea1";
            series49.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series49.Color = System.Drawing.Color.Blue;
            series49.Legend = "Legend1";
            series49.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series49.Name = "Присед";
            series49.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series50.BorderWidth = 2;
            series50.ChartArea = "ChartArea1";
            series50.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series50.Color = System.Drawing.Color.Lime;
            series50.Legend = "Legend1";
            series50.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series50.Name = "Жим";
            series50.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series51.BorderWidth = 2;
            series51.ChartArea = "ChartArea1";
            series51.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series51.Color = System.Drawing.Color.Red;
            series51.Legend = "Legend1";
            series51.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series51.Name = "Тяга";
            series51.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series52.BorderWidth = 3;
            series52.ChartArea = "ChartArea1";
            series52.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series52.Color = System.Drawing.Color.Black;
            series52.Legend = "Legend1";
            series52.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series52.Name = "Сумма";
            series52.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series53.BorderWidth = 2;
            series53.ChartArea = "ChartArea1";
            series53.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series53.Color = System.Drawing.Color.Green;
            series53.Legend = "Legend1";
            series53.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series53.Name = "Тоннаж";
            series53.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series53.YValuesPerPoint = 2;
            series54.BorderWidth = 3;
            series54.ChartArea = "ChartArea1";
            series54.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series54.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            series54.Legend = "Legend1";
            series54.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series54.Name = "Собственный вес";
            series54.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            this.chart1.Series.Add(series49);
            this.chart1.Series.Add(series50);
            this.chart1.Series.Add(series51);
            this.chart1.Series.Add(series52);
            this.chart1.Series.Add(series53);
            this.chart1.Series.Add(series54);
            this.chart1.Size = new System.Drawing.Size(850, 572);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(715, 517);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 67);
            this.button3.TabIndex = 3;
            this.button3.Text = "Обновить график результатов";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(715, 442);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(121, 57);
            this.button6.TabIndex = 12;
            this.button6.Text = "Быстрая оценка ПМ";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(715, 349);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(123, 73);
            this.button7.TabIndex = 13;
            this.button7.Text = "Мои макс. результаты";
            this.button7.UseVisualStyleBackColor = false;
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
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
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
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.LinkLabel linkLabel1;
        public System.Windows.Forms.PictureBox pictureBox1;
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

