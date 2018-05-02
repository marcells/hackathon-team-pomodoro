namespace PomodoroForms
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listViewPomodoros = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonFlood = new System.Windows.Forms.Button();
            this.numericUpDownFlood = new System.Windows.Forms.NumericUpDown();
            this.checkBoxScramble = new System.Windows.Forms.CheckBox();
            this.checkBoxBigLoad = new System.Windows.Forms.CheckBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.timerTick = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFlood)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewPomodoros
            // 
            this.listViewPomodoros.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewPomodoros.FullRowSelect = true;
            this.listViewPomodoros.Location = new System.Drawing.Point(12, 30);
            this.listViewPomodoros.Name = "listViewPomodoros";
            this.listViewPomodoros.Size = new System.Drawing.Size(331, 277);
            this.listViewPomodoros.TabIndex = 0;
            this.listViewPomodoros.UseCompatibleStateImageBehavior = false;
            this.listViewPomodoros.View = System.Windows.Forms.View.Details;
            this.listViewPomodoros.SelectedIndexChanged += new System.EventHandler(this.listViewPomodoros_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 145;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Time";
            this.columnHeader2.Width = 160;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(360, 30);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(268, 331);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 333);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(69, 333);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(171, 20);
            this.textBoxName.TabIndex = 4;
            this.textBoxName.Text = "C# ftw :-)";
            // 
            // buttonFlood
            // 
            this.buttonFlood.Location = new System.Drawing.Point(370, 284);
            this.buttonFlood.Name = "buttonFlood";
            this.buttonFlood.Size = new System.Drawing.Size(75, 23);
            this.buttonFlood.TabIndex = 5;
            this.buttonFlood.Text = "Flood";
            this.buttonFlood.UseVisualStyleBackColor = true;
            this.buttonFlood.Click += new System.EventHandler(this.buttonFlood_Click);
            // 
            // numericUpDownFlood
            // 
            this.numericUpDownFlood.Location = new System.Drawing.Point(370, 313);
            this.numericUpDownFlood.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownFlood.Name = "numericUpDownFlood";
            this.numericUpDownFlood.Size = new System.Drawing.Size(101, 20);
            this.numericUpDownFlood.TabIndex = 6;
            this.numericUpDownFlood.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // checkBoxScramble
            // 
            this.checkBoxScramble.AutoSize = true;
            this.checkBoxScramble.Location = new System.Drawing.Point(370, 339);
            this.checkBoxScramble.Name = "checkBoxScramble";
            this.checkBoxScramble.Size = new System.Drawing.Size(70, 17);
            this.checkBoxScramble.TabIndex = 7;
            this.checkBoxScramble.Text = "Scramble";
            this.checkBoxScramble.UseVisualStyleBackColor = true;
            // 
            // checkBoxBigLoad
            // 
            this.checkBoxBigLoad.AutoSize = true;
            this.checkBoxBigLoad.Location = new System.Drawing.Point(370, 361);
            this.checkBoxBigLoad.Name = "checkBoxBigLoad";
            this.checkBoxBigLoad.Size = new System.Drawing.Size(65, 17);
            this.checkBoxBigLoad.TabIndex = 8;
            this.checkBoxBigLoad.Text = "BigLoad";
            this.checkBoxBigLoad.UseVisualStyleBackColor = true;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(268, 355);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 9;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(360, 88);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 10;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // timerTick
            // 
            this.timerTick.Interval = 1000;
            this.timerTick.Tick += new System.EventHandler(this.timerTick_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 390);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.checkBoxBigLoad);
            this.Controls.Add(this.checkBoxScramble);
            this.Controls.Add(this.numericUpDownFlood);
            this.Controls.Add(this.buttonFlood);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.listViewPomodoros);
            this.Name = "FormMain";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFlood)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewPomodoros;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonFlood;
        private System.Windows.Forms.NumericUpDown numericUpDownFlood;
        private System.Windows.Forms.CheckBox checkBoxScramble;
        private System.Windows.Forms.CheckBox checkBoxBigLoad;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Timer timerTick;
    }
}

