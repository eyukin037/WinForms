
namespace CalculatorMatrix
{
    partial class Form1
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
            this.comboOperation = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.trkDecimals = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnExecute = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новаяСессияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.данныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьЛогиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчётToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.статистикаВычисленийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMatrixA = new System.Windows.Forms.Button();
            this.btnMatrixB = new System.Windows.Forms.Button();
            this.btnResult = new System.Windows.Forms.Button();
            this.trkScale = new System.Windows.Forms.TrackBar();
            this.выбратьФайлЛогаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.trkDecimals)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkScale)).BeginInit();
            this.SuspendLayout();
            // 
            // comboOperation
            // 
            this.comboOperation.FormattingEnabled = true;
            this.comboOperation.Items.AddRange(new object[] {
            "A + B",
            "A - B",
            "B - A",
            "A × B",
            "B × A",
            "det(A)",
            "det(B)",
            "A⁻¹",
            "B⁻¹"});
            this.comboOperation.Location = new System.Drawing.Point(186, 29);
            this.comboOperation.Name = "comboOperation";
            this.comboOperation.Size = new System.Drawing.Size(124, 21);
            this.comboOperation.TabIndex = 3;
            this.comboOperation.Text = "Выберите операцию";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Масштаб отображения";
            // 
            // trkDecimals
            // 
            this.trkDecimals.Location = new System.Drawing.Point(172, 124);
            this.trkDecimals.Maximum = 5;
            this.trkDecimals.Name = "trkDecimals";
            this.trkDecimals.Size = new System.Drawing.Size(104, 45);
            this.trkDecimals.TabIndex = 6;
            this.trkDecimals.Value = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(157, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Кол-во знаков после запятой";
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.White;
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbLog.Location = new System.Drawing.Point(0, 201);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(322, 535);
            this.rtbLog.TabIndex = 8;
            this.rtbLog.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Ход выполнения операции";
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.White;
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.ForeColor = System.Drawing.Color.Lime;
            this.progressBar.Location = new System.Drawing.Point(0, 173);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(322, 28);
            this.progressBar.TabIndex = 10;
            // 
            // btnExecute
            // 
            this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecute.Location = new System.Drawing.Point(15, 56);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 11;
            this.btnExecute.Text = "Выполнить";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.данныеToolStripMenuItem,
            this.отчётToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(322, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбратьФайлЛогаToolStripMenuItem,
            this.новаяСессияToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // новаяСессияToolStripMenuItem
            // 
            this.новаяСессияToolStripMenuItem.Name = "новаяСессияToolStripMenuItem";
            this.новаяСессияToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.новаяСессияToolStripMenuItem.Text = "Новая сессия";
            this.новаяСессияToolStripMenuItem.Click += new System.EventHandler(this.новаяСессияToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // данныеToolStripMenuItem
            // 
            this.данныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem,
            this.загрузитьToolStripMenuItem,
            this.очиститьЛогиToolStripMenuItem});
            this.данныеToolStripMenuItem.Name = "данныеToolStripMenuItem";
            this.данныеToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.данныеToolStripMenuItem.Text = "Данные";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.загрузитьToolStripMenuItem_Click);
            // 
            // очиститьЛогиToolStripMenuItem
            // 
            this.очиститьЛогиToolStripMenuItem.Name = "очиститьЛогиToolStripMenuItem";
            this.очиститьЛогиToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.очиститьЛогиToolStripMenuItem.Text = "Очистить логи";
            this.очиститьЛогиToolStripMenuItem.Click += new System.EventHandler(this.очиститьЛогиToolStripMenuItem_Click);
            // 
            // отчётToolStripMenuItem
            // 
            this.отчётToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.статистикаВычисленийToolStripMenuItem});
            this.отчётToolStripMenuItem.Name = "отчётToolStripMenuItem";
            this.отчётToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.отчётToolStripMenuItem.Text = "Отчёт";
            // 
            // статистикаВычисленийToolStripMenuItem
            // 
            this.статистикаВычисленийToolStripMenuItem.Name = "статистикаВычисленийToolStripMenuItem";
            this.статистикаВычисленийToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.статистикаВычисленийToolStripMenuItem.Text = "Статистика вычислений";
            this.статистикаВычисленийToolStripMenuItem.Click += new System.EventHandler(this.статистикаВычисленийToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // btnMatrixA
            // 
            this.btnMatrixA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMatrixA.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMatrixA.Location = new System.Drawing.Point(12, 27);
            this.btnMatrixA.Name = "btnMatrixA";
            this.btnMatrixA.Size = new System.Drawing.Size(75, 23);
            this.btnMatrixA.TabIndex = 16;
            this.btnMatrixA.Text = "Матрица A";
            this.btnMatrixA.UseVisualStyleBackColor = true;
            this.btnMatrixA.Click += new System.EventHandler(this.btnMatrixA_Click);
            // 
            // btnMatrixB
            // 
            this.btnMatrixB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMatrixB.Location = new System.Drawing.Point(105, 27);
            this.btnMatrixB.Name = "btnMatrixB";
            this.btnMatrixB.Size = new System.Drawing.Size(75, 23);
            this.btnMatrixB.TabIndex = 17;
            this.btnMatrixB.Text = "Матрица B";
            this.btnMatrixB.UseVisualStyleBackColor = true;
            this.btnMatrixB.Click += new System.EventHandler(this.btnMatrixB_Click);
            // 
            // btnResult
            // 
            this.btnResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResult.Location = new System.Drawing.Point(105, 57);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(75, 23);
            this.btnResult.TabIndex = 18;
            this.btnResult.Text = "Результат";
            this.btnResult.UseVisualStyleBackColor = true;
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // trkScale
            // 
            this.trkScale.Location = new System.Drawing.Point(141, 87);
            this.trkScale.Maximum = 18;
            this.trkScale.Minimum = 12;
            this.trkScale.Name = "trkScale";
            this.trkScale.Size = new System.Drawing.Size(104, 45);
            this.trkScale.TabIndex = 5;
            this.trkScale.Value = 14;
            // 
            // выбратьФайлЛогаToolStripMenuItem
            // 
            this.выбратьФайлЛогаToolStripMenuItem.Name = "выбратьФайлЛогаToolStripMenuItem";
            this.выбратьФайлЛогаToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.выбратьФайлЛогаToolStripMenuItem.Text = "Выбрать файл лога";
            this.выбратьФайлЛогаToolStripMenuItem.Click += new System.EventHandler(this.выбратьФайлЛогаToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(322, 736);
            this.Controls.Add(this.trkScale);
            this.Controls.Add(this.btnResult);
            this.Controls.Add(this.btnMatrixB);
            this.Controls.Add(this.btnMatrixA);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trkDecimals);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboOperation);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Калькулятор матриц";
            ((System.ComponentModel.ISupportInitialize)(this.trkDecimals)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboOperation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trkDecimals;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новаяСессияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem данныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчётToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem очиститьЛогиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem статистикаВычисленийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.Button btnMatrixA;
        private System.Windows.Forms.Button btnMatrixB;
        private System.Windows.Forms.Button btnResult;
        private System.Windows.Forms.TrackBar trkScale;
        private System.Windows.Forms.ToolStripMenuItem выбратьФайлЛогаToolStripMenuItem;
    }
}

