namespace AgingOutOfStock1
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridCondition = new System.Windows.Forms.DataGridView();
            this.Timing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridAdjustQ = new System.Windows.Forms.DataGridView();
            this.TGreater = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TLess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SGreater = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SLess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimsS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridSmall = new System.Windows.Forms.DataGridView();
            this.SS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SMedium = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SBig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridLarge = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboItem = new System.Windows.Forms.ComboBox();
            this.comboWH = new System.Windows.Forms.ComboBox();
            this.comboRR = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQGeneral = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioQT = new System.Windows.Forms.RadioButton();
            this.radioQGeneral = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDaysAllowTO = new System.Windows.Forms.MaskedTextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtETA = new System.Windows.Forms.MaskedTextBox();
            this.labETA = new System.Windows.Forms.Label();
            this.dataGridResult = new System.Windows.Forms.DataGridView();
            this.model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitsPerDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DaysOutStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderTimes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.chkResetS = new System.Windows.Forms.CheckBox();
            this.chkResetT = new System.Windows.Forms.CheckBox();
            this.chkMultiOrder = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtClearancePoint = new System.Windows.Forms.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.chkOptimizeQ = new System.Windows.Forms.CheckBox();
            this.txtItem = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtItemLevel = new System.Windows.Forms.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtItemDesciption = new System.Windows.Forms.MaskedTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtWH = new System.Windows.Forms.MaskedTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtWHSize = new System.Windows.Forms.MaskedTextBox();
            this.chkWriteExcel = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAdjustQ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLarge)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResult)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridCondition
            // 
            this.dataGridCondition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCondition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Timing,
            this.Action});
            this.dataGridCondition.Location = new System.Drawing.Point(739, 29);
            this.dataGridCondition.Name = "dataGridCondition";
            this.dataGridCondition.Size = new System.Drawing.Size(569, 211);
            this.dataGridCondition.TabIndex = 4;
            // 
            // Timing
            // 
            this.Timing.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.Timing.DefaultCellStyle = dataGridViewCellStyle1;
            this.Timing.HeaderText = "Timing";
            this.Timing.Name = "Timing";
            this.Timing.Width = 63;
            // 
            // Action
            // 
            this.Action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.Action.DefaultCellStyle = dataGridViewCellStyle2;
            this.Action.HeaderText = "Action";
            this.Action.Name = "Action";
            this.Action.Width = 62;
            // 
            // dataGridAdjustQ
            // 
            this.dataGridAdjustQ.AllowUserToAddRows = false;
            this.dataGridAdjustQ.AllowUserToDeleteRows = false;
            this.dataGridAdjustQ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridAdjustQ.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridAdjustQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAdjustQ.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TGreater,
            this.TLess,
            this.SGreater,
            this.SLess,
            this.TimsS,
            this.AddS});
            this.dataGridAdjustQ.Location = new System.Drawing.Point(884, 310);
            this.dataGridAdjustQ.Name = "dataGridAdjustQ";
            this.dataGridAdjustQ.Size = new System.Drawing.Size(401, 199);
            this.dataGridAdjustQ.TabIndex = 7;
            // 
            // TGreater
            // 
            this.TGreater.HeaderText = "T>";
            this.TGreater.Name = "TGreater";
            this.TGreater.Width = 45;
            // 
            // TLess
            // 
            this.TLess.HeaderText = "T<=";
            this.TLess.Name = "TLess";
            this.TLess.Width = 51;
            // 
            // SGreater
            // 
            this.SGreater.HeaderText = "S>";
            this.SGreater.Name = "SGreater";
            this.SGreater.Width = 45;
            // 
            // SLess
            // 
            this.SLess.HeaderText = "S<=";
            this.SLess.Name = "SLess";
            this.SLess.Width = 51;
            // 
            // TimsS
            // 
            this.TimsS.HeaderText = "S* (+)  S/ (-)";
            this.TimsS.Name = "TimsS";
            this.TimsS.Width = 88;
            // 
            // AddS
            // 
            this.AddS.HeaderText = "S+";
            this.AddS.Name = "AddS";
            this.AddS.Width = 45;
            // 
            // dataGridSmall
            // 
            this.dataGridSmall.AllowUserToAddRows = false;
            this.dataGridSmall.AllowUserToDeleteRows = false;
            this.dataGridSmall.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridSmall.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridSmall.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SS,
            this.SMedium,
            this.SBig});
            this.dataGridSmall.Location = new System.Drawing.Point(884, 539);
            this.dataGridSmall.Name = "dataGridSmall";
            this.dataGridSmall.RowHeadersWidth = 70;
            this.dataGridSmall.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridSmall.Size = new System.Drawing.Size(224, 99);
            this.dataGridSmall.TabIndex = 9;
            this.dataGridSmall.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridSmall_CellClick);
            this.dataGridSmall.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridSmall_CellEndEdit);
            // 
            // SS
            // 
            this.SS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SS.HeaderText = "Small";
            this.SS.Name = "SS";
            this.SS.Width = 50;
            // 
            // SMedium
            // 
            this.SMedium.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SMedium.HeaderText = "Medium";
            this.SMedium.Name = "SMedium";
            this.SMedium.Width = 50;
            // 
            // SBig
            // 
            this.SBig.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SBig.HeaderText = "Big";
            this.SBig.Name = "SBig";
            this.SBig.Width = 50;
            // 
            // dataGridLarge
            // 
            this.dataGridLarge.AllowUserToAddRows = false;
            this.dataGridLarge.AllowUserToDeleteRows = false;
            this.dataGridLarge.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridLarge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridLarge.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridLarge.Location = new System.Drawing.Point(1114, 539);
            this.dataGridLarge.Name = "dataGridLarge";
            this.dataGridLarge.RowHeadersWidth = 70;
            this.dataGridLarge.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridLarge.Size = new System.Drawing.Size(224, 99);
            this.dataGridLarge.TabIndex = 10;
            this.dataGridLarge.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridLarge_CellClick);
            this.dataGridLarge.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridLarge_CellEndEdit);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.HeaderText = "Small";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.HeaderText = "Medium";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.HeaderText = "Big";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(936, 523);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Small/Medium Item";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1179, 523);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Large Item";
            // 
            // comboItem
            // 
            this.comboItem.FormattingEnabled = true;
            this.comboItem.Items.AddRange(new object[] {
            "Small/Medium Item",
            "Large Item"});
            this.comboItem.Location = new System.Drawing.Point(799, 565);
            this.comboItem.Name = "comboItem";
            this.comboItem.Size = new System.Drawing.Size(82, 21);
            this.comboItem.TabIndex = 13;
            this.comboItem.SelectedIndexChanged += new System.EventHandler(this.Select_Item);
            // 
            // comboWH
            // 
            this.comboWH.FormattingEnabled = true;
            this.comboWH.Items.AddRange(new object[] {
            "Small",
            "Medium",
            "Big"});
            this.comboWH.Location = new System.Drawing.Point(800, 592);
            this.comboWH.Name = "comboWH";
            this.comboWH.Size = new System.Drawing.Size(81, 21);
            this.comboWH.TabIndex = 14;
            this.comboWH.SelectedIndexChanged += new System.EventHandler(this.Select_Warehouse);
            // 
            // comboRR
            // 
            this.comboRR.FormattingEnabled = true;
            this.comboRR.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High"});
            this.comboRR.Location = new System.Drawing.Point(799, 617);
            this.comboRR.Name = "comboRR";
            this.comboRR.Size = new System.Drawing.Size(81, 21);
            this.comboRR.TabIndex = 15;
            this.comboRR.SelectedIndexChanged += new System.EventHandler(this.Select_RunRate);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(764, 568);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Item";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(730, 595);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Warehouse";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(741, 620);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "RunRate";
            // 
            // txtQGeneral
            // 
            this.txtQGeneral.BackColor = System.Drawing.Color.White;
            this.txtQGeneral.ForeColor = System.Drawing.Color.Silver;
            this.txtQGeneral.Location = new System.Drawing.Point(866, 265);
            this.txtQGeneral.Name = "txtQGeneral";
            this.txtQGeneral.ReadOnly = true;
            this.txtQGeneral.Size = new System.Drawing.Size(402, 20);
            this.txtQGeneral.TabIndex = 19;
            this.txtQGeneral.Text = "Q = N - OHnew";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioQT);
            this.groupBox1.Controls.Add(this.radioQGeneral);
            this.groupBox1.Location = new System.Drawing.Point(740, 262);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(120, 69);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Order Qty (Q)";
            // 
            // radioQT
            // 
            this.radioQT.AutoSize = true;
            this.radioQT.Checked = true;
            this.radioQT.Location = new System.Drawing.Point(7, 42);
            this.radioQT.Name = "radioQT";
            this.radioQT.Size = new System.Drawing.Size(114, 17);
            this.radioQT.TabIndex = 1;
            this.radioQT.TabStop = true;
            this.radioQT.Text = "Based On Time (T)";
            this.radioQT.UseVisualStyleBackColor = true;
            this.radioQT.CheckedChanged += new System.EventHandler(this.radioQT_CheckedChanged);
            // 
            // radioQGeneral
            // 
            this.radioQGeneral.AutoSize = true;
            this.radioQGeneral.Location = new System.Drawing.Point(7, 19);
            this.radioQGeneral.Name = "radioQGeneral";
            this.radioQGeneral.Size = new System.Drawing.Size(74, 17);
            this.radioQGeneral.TabIndex = 0;
            this.radioQGeneral.Text = "In General";
            this.radioQGeneral.UseVisualStyleBackColor = true;
            this.radioQGeneral.CheckedChanged += new System.EventHandler(this.radioQGeneral_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(739, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(779, 483);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Days Allow to TO";
            // 
            // txtDaysAllowTO
            // 
            this.txtDaysAllowTO.Location = new System.Drawing.Point(781, 499);
            this.txtDaysAllowTO.Name = "txtDaysAllowTO";
            this.txtDaysAllowTO.Size = new System.Drawing.Size(49, 20);
            this.txtDaysAllowTO.TabIndex = 25;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(15, 27);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(92, 23);
            this.btnOpen.TabIndex = 27;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtETA
            // 
            this.txtETA.Location = new System.Drawing.Point(231, 7);
            this.txtETA.Name = "txtETA";
            this.txtETA.Size = new System.Drawing.Size(32, 20);
            this.txtETA.TabIndex = 28;
            this.txtETA.Text = "3";
            // 
            // labETA
            // 
            this.labETA.AutoSize = true;
            this.labETA.Location = new System.Drawing.Point(154, 10);
            this.labETA.Name = "labETA";
            this.labETA.Size = new System.Drawing.Size(71, 13);
            this.labETA.TabIndex = 29;
            this.labETA.Text = "Average ETA";
            // 
            // dataGridResult
            // 
            this.dataGridResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.model,
            this.Rating,
            this.Sold,
            this.Lose,
            this.UnitsPerDay,
            this.DaysOutStock,
            this.OrderTimes});
            this.dataGridResult.Location = new System.Drawing.Point(12, 149);
            this.dataGridResult.Name = "dataGridResult";
            this.dataGridResult.Size = new System.Drawing.Size(705, 572);
            this.dataGridResult.TabIndex = 31;
            // 
            // model
            // 
            this.model.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.model.HeaderText = "Model";
            this.model.Name = "model";
            this.model.ReadOnly = true;
            this.model.Width = 61;
            // 
            // Rating
            // 
            this.Rating.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Rating.HeaderText = "Rating";
            this.Rating.Name = "Rating";
            this.Rating.ReadOnly = true;
            this.Rating.Width = 63;
            // 
            // Sold
            // 
            this.Sold.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Sold.HeaderText = "Sold";
            this.Sold.Name = "Sold";
            this.Sold.ReadOnly = true;
            this.Sold.Width = 53;
            // 
            // Lose
            // 
            this.Lose.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Lose.HeaderText = "Lose";
            this.Lose.Name = "Lose";
            this.Lose.ReadOnly = true;
            this.Lose.Width = 55;
            // 
            // UnitsPerDay
            // 
            this.UnitsPerDay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.UnitsPerDay.HeaderText = "Units/Day";
            this.UnitsPerDay.Name = "UnitsPerDay";
            this.UnitsPerDay.ReadOnly = true;
            this.UnitsPerDay.Width = 80;
            // 
            // DaysOutStock
            // 
            this.DaysOutStock.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DaysOutStock.HeaderText = "Days of OutStock";
            this.DaysOutStock.Name = "DaysOutStock";
            this.DaysOutStock.ReadOnly = true;
            this.DaysOutStock.Width = 106;
            // 
            // OrderTimes
            // 
            this.OrderTimes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.OrderTimes.HeaderText = "Number of POs";
            this.OrderTimes.Name = "OrderTimes";
            this.OrderTimes.ReadOnly = true;
            this.OrderTimes.Width = 78;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(289, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "Rating = (Sold + 2*Lose - 0.1*DaysOutStock) / UnitsPerDay";
            // 
            // chkResetS
            // 
            this.chkResetS.AutoSize = true;
            this.chkResetS.Checked = true;
            this.chkResetS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkResetS.Location = new System.Drawing.Point(457, 13);
            this.chkResetS.Name = "chkResetS";
            this.chkResetS.Size = new System.Drawing.Size(120, 17);
            this.chkResetS.TabIndex = 33;
            this.chkResetS.Text = "S=0 when receiving";
            this.chkResetS.UseVisualStyleBackColor = true;
            // 
            // chkResetT
            // 
            this.chkResetT.AutoSize = true;
            this.chkResetT.Location = new System.Drawing.Point(331, 13);
            this.chkResetT.Name = "chkResetT";
            this.chkResetT.Size = new System.Drawing.Size(120, 17);
            this.chkResetT.TabIndex = 34;
            this.chkResetT.Text = "T=0 when receiving";
            this.chkResetT.UseVisualStyleBackColor = true;
            // 
            // chkMultiOrder
            // 
            this.chkMultiOrder.AutoSize = true;
            this.chkMultiOrder.Location = new System.Drawing.Point(331, 36);
            this.chkMultiOrder.Name = "chkMultiOrder";
            this.chkMultiOrder.Size = new System.Drawing.Size(122, 17);
            this.chkMultiOrder.TabIndex = 35;
            this.chkMultiOrder.Text = "Allow Multi-ReOrder ";
            this.chkMultiOrder.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(143, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "Clearance Point";
            // 
            // txtClearancePoint
            // 
            this.txtClearancePoint.Location = new System.Drawing.Point(231, 33);
            this.txtClearancePoint.Name = "txtClearancePoint";
            this.txtClearancePoint.Size = new System.Drawing.Size(32, 20);
            this.txtClearancePoint.TabIndex = 36;
            this.txtClearancePoint.Text = "60";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(269, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 38;
            this.label10.Text = "days";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(269, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "days";
            // 
            // chkOptimizeQ
            // 
            this.chkOptimizeQ.AutoSize = true;
            this.chkOptimizeQ.Location = new System.Drawing.Point(457, 35);
            this.chkOptimizeQ.Name = "chkOptimizeQ";
            this.chkOptimizeQ.Size = new System.Drawing.Size(74, 17);
            this.chkOptimizeQ.TabIndex = 40;
            this.chkOptimizeQ.Text = "OptimizeQ";
            this.chkOptimizeQ.UseVisualStyleBackColor = true;
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(146, 66);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(100, 20);
            this.txtItem.TabIndex = 41;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(113, 69);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 42;
            this.label12.Text = "Item";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(257, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(33, 13);
            this.label13.TabIndex = 44;
            this.label13.Text = "Level";
            // 
            // txtItemLevel
            // 
            this.txtItemLevel.Location = new System.Drawing.Point(290, 66);
            this.txtItemLevel.Name = "txtItemLevel";
            this.txtItemLevel.Size = new System.Drawing.Size(50, 20);
            this.txtItemLevel.TabIndex = 43;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(359, 69);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 13);
            this.label14.TabIndex = 46;
            this.label14.Text = "Description";
            // 
            // txtItemDesciption
            // 
            this.txtItemDesciption.Location = new System.Drawing.Point(425, 66);
            this.txtItemDesciption.Name = "txtItemDesciption";
            this.txtItemDesciption.Size = new System.Drawing.Size(241, 20);
            this.txtItemDesciption.TabIndex = 45;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(113, 99);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(26, 13);
            this.label15.TabIndex = 48;
            this.label15.Text = "WH";
            // 
            // txtWH
            // 
            this.txtWH.Location = new System.Drawing.Point(146, 96);
            this.txtWH.Name = "txtWH";
            this.txtWH.Size = new System.Drawing.Size(144, 20);
            this.txtWH.TabIndex = 47;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(298, 99);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(27, 13);
            this.label16.TabIndex = 50;
            this.label16.Text = "Size";
            // 
            // txtWHSize
            // 
            this.txtWHSize.Location = new System.Drawing.Point(331, 96);
            this.txtWHSize.Name = "txtWHSize";
            this.txtWHSize.Size = new System.Drawing.Size(50, 20);
            this.txtWHSize.TabIndex = 49;
            // 
            // chkWriteExcel
            // 
            this.chkWriteExcel.AutoSize = true;
            this.chkWriteExcel.Location = new System.Drawing.Point(586, 13);
            this.chkWriteExcel.Name = "chkWriteExcel";
            this.chkWriteExcel.Size = new System.Drawing.Size(92, 17);
            this.chkWriteExcel.TabIndex = 51;
            this.chkWriteExcel.Text = "Write to Excel";
            this.chkWriteExcel.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 62);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 23);
            this.button2.TabIndex = 52;
            this.button2.Text = "ReCalculate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(422, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Age";
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(455, 100);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(50, 20);
            this.txtAge.TabIndex = 53;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 733);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.chkWriteExcel);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtWHSize);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtWH);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtItemDesciption);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtItemLevel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtItem);
            this.Controls.Add(this.chkOptimizeQ);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtClearancePoint);
            this.Controls.Add(this.chkMultiOrder);
            this.Controls.Add(this.chkResetT);
            this.Controls.Add(this.chkResetS);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dataGridResult);
            this.Controls.Add(this.labETA);
            this.Controls.Add(this.txtETA);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDaysAllowTO);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtQGeneral);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboRR);
            this.Controls.Add(this.comboWH);
            this.Controls.Add(this.comboItem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridLarge);
            this.Controls.Add(this.dataGridSmall);
            this.Controls.Add(this.dataGridAdjustQ);
            this.Controls.Add(this.dataGridCondition);
            this.Name = "Form1";
            this.Text = "PO Simulation";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAdjustQ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLarge)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridCondition;
        private System.Windows.Forms.DataGridViewTextBoxColumn Timing;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action;
        private System.Windows.Forms.DataGridView dataGridAdjustQ;
        private System.Windows.Forms.DataGridView dataGridSmall;
        private System.Windows.Forms.DataGridViewTextBoxColumn SS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SMedium;
        private System.Windows.Forms.DataGridViewTextBoxColumn SBig;
        private System.Windows.Forms.DataGridView dataGridLarge;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboItem;
        private System.Windows.Forms.ComboBox comboWH;
        private System.Windows.Forms.ComboBox comboRR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox txtQGeneral;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioQT;
        private System.Windows.Forms.RadioButton radioQGeneral;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TGreater;
        private System.Windows.Forms.DataGridViewTextBoxColumn TLess;
        private System.Windows.Forms.DataGridViewTextBoxColumn SGreater;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLess;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimsS;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox txtDaysAllowTO;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.MaskedTextBox txtETA;
        private System.Windows.Forms.Label labETA;
        private System.Windows.Forms.DataGridView dataGridResult;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkResetS;
        private System.Windows.Forms.CheckBox chkResetT;
        private System.Windows.Forms.CheckBox chkMultiOrder;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MaskedTextBox txtClearancePoint;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkOptimizeQ;
        private System.Windows.Forms.MaskedTextBox txtItem;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.MaskedTextBox txtItemLevel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.MaskedTextBox txtItemDesciption;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MaskedTextBox txtWH;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.MaskedTextBox txtWHSize;
        private System.Windows.Forms.CheckBox chkWriteExcel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rating;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sold;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lose;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitsPerDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn DaysOutStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderTimes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox txtAge;
    }
}

