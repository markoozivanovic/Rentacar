namespace TVP_PRVI_PROJEKAT
{
    partial class frmAutomobil
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtMarka = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtGodina = new System.Windows.Forms.TextBox();
            this.txtKaroserija = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.cbVrata = new System.Windows.Forms.ComboBox();
            this.cbMenjac = new System.Windows.Forms.ComboBox();
            this.cbGorivo = new System.Windows.Forms.ComboBox();
            this.cbPogon = new System.Windows.Forms.ComboBox();
            this.button6 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtKubikaza = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtKubikaza)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 382);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "Унеси";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Марка аутомобила";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Модел аутомобила";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "Годиште аутомобила";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 19);
            this.label5.TabIndex = 5;
            this.label5.Text = "Кубикажа";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 19);
            this.label6.TabIndex = 6;
            this.label6.Text = "Погон";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 235);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 19);
            this.label7.TabIndex = 7;
            this.label7.Text = "Врста мењача";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 264);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 19);
            this.label8.TabIndex = 8;
            this.label8.Text = "Каросерија";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 297);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 19);
            this.label9.TabIndex = 9;
            this.label9.Text = "Гориво";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(38, 336);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 19);
            this.label10.TabIndex = 10;
            this.label10.Text = "Број врата";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(106, 39);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(65, 26);
            this.txtID.TabIndex = 11;
            this.txtID.TextChanged += new System.EventHandler(this.txtID_TextChanged);
            // 
            // txtMarka
            // 
            this.txtMarka.Location = new System.Drawing.Point(194, 71);
            this.txtMarka.Name = "txtMarka";
            this.txtMarka.Size = new System.Drawing.Size(201, 26);
            this.txtMarka.TabIndex = 12;
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(194, 104);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(201, 26);
            this.txtModel.TabIndex = 13;
            // 
            // txtGodina
            // 
            this.txtGodina.Location = new System.Drawing.Point(194, 136);
            this.txtGodina.Name = "txtGodina";
            this.txtGodina.Size = new System.Drawing.Size(201, 26);
            this.txtGodina.TabIndex = 14;
            // 
            // txtKaroserija
            // 
            this.txtKaroserija.Location = new System.Drawing.Point(194, 264);
            this.txtKaroserija.Name = "txtKaroserija";
            this.txtKaroserija.Size = new System.Drawing.Size(201, 26);
            this.txtKaroserija.TabIndex = 18;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(164, 382);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 42);
            this.button2.TabIndex = 21;
            this.button2.Text = "Обриши";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(278, 382);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(108, 42);
            this.button3.TabIndex = 22;
            this.button3.Text = "Измени";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(5, 382);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(39, 42);
            this.button4.TabIndex = 23;
            this.button4.Text = "<<";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(392, 382);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(39, 42);
            this.button5.TabIndex = 24;
            this.button5.Text = ">>";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // cbVrata
            // 
            this.cbVrata.FormattingEnabled = true;
            this.cbVrata.Items.AddRange(new object[] {
            "5",
            "3"});
            this.cbVrata.Location = new System.Drawing.Point(194, 336);
            this.cbVrata.Name = "cbVrata";
            this.cbVrata.Size = new System.Drawing.Size(201, 27);
            this.cbVrata.TabIndex = 25;
            // 
            // cbMenjac
            // 
            this.cbMenjac.FormattingEnabled = true;
            this.cbMenjac.Items.AddRange(new object[] {
            "Аутоматски",
            "Мануелни",
            "Automacki",
            "Manuelni"});
            this.cbMenjac.Location = new System.Drawing.Point(194, 231);
            this.cbMenjac.Name = "cbMenjac";
            this.cbMenjac.Size = new System.Drawing.Size(201, 27);
            this.cbMenjac.TabIndex = 26;
            // 
            // cbGorivo
            // 
            this.cbGorivo.FormattingEnabled = true;
            this.cbGorivo.Items.AddRange(new object[] {
            "Бензин",
            "Benzin",
            "Гас",
            "Gas",
            "Дизел",
            "Dizel"});
            this.cbGorivo.Location = new System.Drawing.Point(194, 297);
            this.cbGorivo.Name = "cbGorivo";
            this.cbGorivo.Size = new System.Drawing.Size(201, 27);
            this.cbGorivo.TabIndex = 27;
            // 
            // cbPogon
            // 
            this.cbPogon.FormattingEnabled = true;
            this.cbPogon.Items.AddRange(new object[] {
            "Prednji",
            "Предњи",
            "Задњи",
            "Zadnji"});
            this.cbPogon.Location = new System.Drawing.Point(194, 198);
            this.cbPogon.Name = "cbPogon";
            this.cbPogon.Size = new System.Drawing.Size(201, 27);
            this.cbPogon.TabIndex = 28;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(164, 432);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(108, 42);
            this.button6.TabIndex = 29;
            this.button6.Text = "Нови";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolTip1.ForeColor = System.Drawing.Color.Red;
            // 
            // txtKubikaza
            // 
            this.txtKubikaza.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.txtKubikaza.Location = new System.Drawing.Point(194, 166);
            this.txtKubikaza.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.txtKubikaza.Name = "txtKubikaza";
            this.txtKubikaza.Size = new System.Drawing.Size(201, 26);
            this.txtKubikaza.TabIndex = 30;
            // 
            // frmAutomobil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(439, 486);
            this.Controls.Add(this.txtKubikaza);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.cbPogon);
            this.Controls.Add(this.cbGorivo);
            this.Controls.Add(this.cbMenjac);
            this.Controls.Add(this.cbVrata);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtKaroserija);
            this.Controls.Add(this.txtGodina);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtMarka);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmAutomobil";
            this.Opacity = 0.87D;
            this.Text = "Подаци о аутомобилу";
            this.Load += new System.EventHandler(this.frmAutomobil_Load);
            this.Leave += new System.EventHandler(this.frmAutomobil_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.txtKubikaza)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtMarka;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtGodina;
        private System.Windows.Forms.TextBox txtKaroserija;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox cbVrata;
        private System.Windows.Forms.ComboBox cbMenjac;
        private System.Windows.Forms.ComboBox cbGorivo;
        private System.Windows.Forms.ComboBox cbPogon;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown txtKubikaza;
    }
}