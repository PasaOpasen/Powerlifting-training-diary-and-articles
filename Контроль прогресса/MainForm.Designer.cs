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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.действияСДневникамиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.определитьДневникПоУмолчаниюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьЗаписьОТренировкеВДневникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьДневникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.начатьНовыйДневникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.написатьАвторуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.скачатьМатериалыПоПауэрлифтингуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button6 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button7 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 24);
            this.chart1.Name = "chart1";
            this.chart1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Blue;
            series1.Legend = "Legend1";
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Присед";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Lime;
            series2.Legend = "Legend1";
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.Name = "Жим";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Red;
            series3.Legend = "Legend1";
            series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series3.Name = "Тяга";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series4.BorderWidth = 3;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Black;
            series4.Legend = "Legend1";
            series4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series4.Name = "Сумма";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series5.BorderWidth = 2;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.Green;
            series5.Legend = "Legend1";
            series5.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series5.Name = "Тоннаж";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series5.YValuesPerPoint = 2;
            series6.BorderWidth = 3;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            series6.Legend = "Legend1";
            series6.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series6.Name = "Собственный вес";
            series6.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(850, 405);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(715, 350);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 67);
            this.button3.TabIndex = 3;
            this.button3.Text = "Обновить график результатов";
            this.toolTip1.SetToolTip(this.button3, "Нарисовать графики по основным движениям, тоннажу и т. п.");
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(26, 74);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(632, 290);
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
            // написатьАвторуToolStripMenuItem
            // 
            this.написатьАвторуToolStripMenuItem.Name = "написатьАвторуToolStripMenuItem";
            this.написатьАвторуToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.написатьАвторуToolStripMenuItem.Text = "Контакты автора";
            this.написатьАвторуToolStripMenuItem.Click += new System.EventHandler(this.написатьАвторуToolStripMenuItem_Click);
            // 
            // скачатьМатериалыПоПауэрлифтингуToolStripMenuItem
            // 
            this.скачатьМатериалыПоПауэрлифтингуToolStripMenuItem.Name = "скачатьМатериалыПоПауэрлифтингуToolStripMenuItem";
            this.скачатьМатериалыПоПауэрлифтингуToolStripMenuItem.Size = new System.Drawing.Size(233, 20);
            this.скачатьМатериалыПоПауэрлифтингуToolStripMenuItem.Text = "Скачать материалы по пауэрлифтингу";
            this.скачатьМатериалыПоПауэрлифтингуToolStripMenuItem.Click += new System.EventHandler(this.скачатьМатериалыПоПауэрлифтингуToolStripMenuItem_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(715, 275);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(121, 57);
            this.button6.TabIndex = 12;
            this.button6.Text = "Быстрая оценка ПМ";
            this.toolTip1.SetToolTip(this.button6, "Оценить повторный максимум по волшебной формуле");
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(715, 182);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(123, 73);
            this.button7.TabIndex = 13;
            this.button7.Text = "Мои макс. результаты";
            this.toolTip1.SetToolTip(this.button7, "Вывести небольшую статистику тренировок");
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 429);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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

