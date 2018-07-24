namespace UPPY.Tools
{
    partial class MainForm
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
            this.btnCheckTechRoutesName = new System.Windows.Forms.Button();
            this.clbValues = new System.Windows.Forms.CheckedListBox();
            this.btnRepairTechRoutesName = new System.Windows.Forms.Button();
            this.cbCheckAll = new System.Windows.Forms.CheckBox();
            this.btnCheckDoubles = new System.Windows.Forms.Button();
            this.btnRepairDoubles = new System.Windows.Forms.Button();
            this.progressPanel1 = new DevExpress.XtraWaitForm.ProgressPanel();
            this.btnCheckUnusableTechRoutes = new System.Windows.Forms.Button();
            this.btnDelUnusableTechRoutes = new System.Windows.Forms.Button();
            this.btnCheckUnusableCollection = new System.Windows.Forms.Button();
            this.btnDelUnusableCollection = new System.Windows.Forms.Button();
            this.btnRebuildIndexes = new System.Windows.Forms.Button();
            this.btnCheckHier = new System.Windows.Forms.Button();
            this.btnRemoveHier = new System.Windows.Forms.Button();
            this.btnCheckWorkHourDrawing = new System.Windows.Forms.Button();
            this.btnDelWorkHourDrawing = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCheckDeletedTO = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCheckTechRoutesName
            // 
            this.btnCheckTechRoutesName.Location = new System.Drawing.Point(12, 12);
            this.btnCheckTechRoutesName.Name = "btnCheckTechRoutesName";
            this.btnCheckTechRoutesName.Size = new System.Drawing.Size(242, 23);
            this.btnCheckTechRoutesName.TabIndex = 0;
            this.btnCheckTechRoutesName.Text = "Проверить имена тех. маршрутов";
            this.btnCheckTechRoutesName.UseVisualStyleBackColor = true;
            this.btnCheckTechRoutesName.Click += new System.EventHandler(this.btnCheckTechRoutesName_Click);
            // 
            // clbValues
            // 
            this.clbValues.CheckOnClick = true;
            this.clbValues.FormattingEnabled = true;
            this.clbValues.Location = new System.Drawing.Point(12, 272);
            this.clbValues.Name = "clbValues";
            this.clbValues.Size = new System.Drawing.Size(600, 184);
            this.clbValues.TabIndex = 2;
            // 
            // btnRepairTechRoutesName
            // 
            this.btnRepairTechRoutesName.Enabled = false;
            this.btnRepairTechRoutesName.Location = new System.Drawing.Point(370, 12);
            this.btnRepairTechRoutesName.Name = "btnRepairTechRoutesName";
            this.btnRepairTechRoutesName.Size = new System.Drawing.Size(242, 23);
            this.btnRepairTechRoutesName.TabIndex = 3;
            this.btnRepairTechRoutesName.Text = "Починить имена тех. маршрутов";
            this.btnRepairTechRoutesName.UseVisualStyleBackColor = true;
            this.btnRepairTechRoutesName.Click += new System.EventHandler(this.btnRepairTechRoutesName_Click);
            // 
            // cbCheckAll
            // 
            this.cbCheckAll.AutoSize = true;
            this.cbCheckAll.Location = new System.Drawing.Point(15, 252);
            this.cbCheckAll.Name = "cbCheckAll";
            this.cbCheckAll.Size = new System.Drawing.Size(15, 14);
            this.cbCheckAll.TabIndex = 4;
            this.cbCheckAll.UseVisualStyleBackColor = true;
            this.cbCheckAll.CheckedChanged += new System.EventHandler(this.cbCheckAll_CheckedChanged);
            // 
            // btnCheckDoubles
            // 
            this.btnCheckDoubles.Location = new System.Drawing.Point(12, 70);
            this.btnCheckDoubles.Name = "btnCheckDoubles";
            this.btnCheckDoubles.Size = new System.Drawing.Size(242, 23);
            this.btnCheckDoubles.TabIndex = 5;
            this.btnCheckDoubles.Text = "Проверить дубли тех. маршрутов";
            this.btnCheckDoubles.UseVisualStyleBackColor = true;
            this.btnCheckDoubles.Click += new System.EventHandler(this.btnCheckDoubles_Click);
            // 
            // btnRepairDoubles
            // 
            this.btnRepairDoubles.Enabled = false;
            this.btnRepairDoubles.Location = new System.Drawing.Point(370, 70);
            this.btnRepairDoubles.Name = "btnRepairDoubles";
            this.btnRepairDoubles.Size = new System.Drawing.Size(242, 23);
            this.btnRepairDoubles.TabIndex = 6;
            this.btnRepairDoubles.Text = "Починить дубли тех. маршрутов";
            this.btnRepairDoubles.UseVisualStyleBackColor = true;
            this.btnRepairDoubles.Click += new System.EventHandler(this.btnRepairDoubles_Click);
            // 
            // progressPanel1
            // 
            this.progressPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel1.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.progressPanel1.Appearance.Options.UseBackColor = true;
            this.progressPanel1.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.progressPanel1.AppearanceCaption.Options.UseFont = true;
            this.progressPanel1.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanel1.AppearanceDescription.Options.UseFont = true;
            this.progressPanel1.Location = new System.Drawing.Point(220, 272);
            this.progressPanel1.Name = "progressPanel1";
            this.progressPanel1.Size = new System.Drawing.Size(246, 66);
            this.progressPanel1.TabIndex = 7;
            this.progressPanel1.Text = "progressPanel1";
            this.progressPanel1.Visible = false;
            // 
            // btnCheckUnusableTechRoutes
            // 
            this.btnCheckUnusableTechRoutes.Location = new System.Drawing.Point(13, 99);
            this.btnCheckUnusableTechRoutes.Name = "btnCheckUnusableTechRoutes";
            this.btnCheckUnusableTechRoutes.Size = new System.Drawing.Size(242, 23);
            this.btnCheckUnusableTechRoutes.TabIndex = 8;
            this.btnCheckUnusableTechRoutes.Text = "Проверить неиспользуемые тех. маршруты";
            this.btnCheckUnusableTechRoutes.UseVisualStyleBackColor = true;
            this.btnCheckUnusableTechRoutes.Click += new System.EventHandler(this.btnCheckUnusableTechRoutes_Click);
            // 
            // btnDelUnusableTechRoutes
            // 
            this.btnDelUnusableTechRoutes.Enabled = false;
            this.btnDelUnusableTechRoutes.Location = new System.Drawing.Point(370, 99);
            this.btnDelUnusableTechRoutes.Name = "btnDelUnusableTechRoutes";
            this.btnDelUnusableTechRoutes.Size = new System.Drawing.Size(242, 23);
            this.btnDelUnusableTechRoutes.TabIndex = 9;
            this.btnDelUnusableTechRoutes.Text = "Удалить неиспользуемые тех. маршруты";
            this.btnDelUnusableTechRoutes.UseVisualStyleBackColor = true;
            this.btnDelUnusableTechRoutes.Click += new System.EventHandler(this.btnDelUnusableTechRoutes_Click);
            // 
            // btnCheckUnusableCollection
            // 
            this.btnCheckUnusableCollection.Location = new System.Drawing.Point(13, 128);
            this.btnCheckUnusableCollection.Name = "btnCheckUnusableCollection";
            this.btnCheckUnusableCollection.Size = new System.Drawing.Size(242, 23);
            this.btnCheckUnusableCollection.TabIndex = 10;
            this.btnCheckUnusableCollection.Text = "Проверить неиспользуемые коллекции";
            this.btnCheckUnusableCollection.UseVisualStyleBackColor = true;
            this.btnCheckUnusableCollection.Click += new System.EventHandler(this.btnCheckUnusableCollection_Click);
            // 
            // btnDelUnusableCollection
            // 
            this.btnDelUnusableCollection.Enabled = false;
            this.btnDelUnusableCollection.Location = new System.Drawing.Point(370, 128);
            this.btnDelUnusableCollection.Name = "btnDelUnusableCollection";
            this.btnDelUnusableCollection.Size = new System.Drawing.Size(242, 23);
            this.btnDelUnusableCollection.TabIndex = 11;
            this.btnDelUnusableCollection.Text = "Удалить неиспользуемые коллекции";
            this.btnDelUnusableCollection.UseVisualStyleBackColor = true;
            this.btnDelUnusableCollection.Click += new System.EventHandler(this.btnDelUnusableCollection_Click);
            // 
            // btnRebuildIndexes
            // 
            this.btnRebuildIndexes.Location = new System.Drawing.Point(370, 157);
            this.btnRebuildIndexes.Name = "btnRebuildIndexes";
            this.btnRebuildIndexes.Size = new System.Drawing.Size(242, 23);
            this.btnRebuildIndexes.TabIndex = 12;
            this.btnRebuildIndexes.Text = "Перестроить индексы";
            this.btnRebuildIndexes.UseVisualStyleBackColor = true;
            this.btnRebuildIndexes.Click += new System.EventHandler(this.btnRebuildIndexes_Click);
            // 
            // btnCheckHier
            // 
            this.btnCheckHier.Location = new System.Drawing.Point(13, 186);
            this.btnCheckHier.Name = "btnCheckHier";
            this.btnCheckHier.Size = new System.Drawing.Size(242, 23);
            this.btnCheckHier.TabIndex = 14;
            this.btnCheckHier.Text = "Проверить целостность иер. колл.";
            this.btnCheckHier.UseVisualStyleBackColor = true;
            this.btnCheckHier.Click += new System.EventHandler(this.btnCheckHier_Click);
            // 
            // btnRemoveHier
            // 
            this.btnRemoveHier.Location = new System.Drawing.Point(370, 186);
            this.btnRemoveHier.Name = "btnRemoveHier";
            this.btnRemoveHier.Size = new System.Drawing.Size(242, 23);
            this.btnRemoveHier.TabIndex = 15;
            this.btnRemoveHier.Text = "Удалить висящие в воздухе";
            this.btnRemoveHier.UseVisualStyleBackColor = true;
            this.btnRemoveHier.Click += new System.EventHandler(this.btnRemoveHier_Click);
            // 
            // btnCheckWorkHourDrawing
            // 
            this.btnCheckWorkHourDrawing.Location = new System.Drawing.Point(12, 215);
            this.btnCheckWorkHourDrawing.Name = "btnCheckWorkHourDrawing";
            this.btnCheckWorkHourDrawing.Size = new System.Drawing.Size(242, 23);
            this.btnCheckWorkHourDrawing.TabIndex = 16;
            this.btnCheckWorkHourDrawing.Text = "Проверить лишние WorkHourDrawing";
            this.btnCheckWorkHourDrawing.UseVisualStyleBackColor = true;
            this.btnCheckWorkHourDrawing.Click += new System.EventHandler(this.btnCheckWorkHourDrawing_Click);
            // 
            // btnDelWorkHourDrawing
            // 
            this.btnDelWorkHourDrawing.Location = new System.Drawing.Point(370, 215);
            this.btnDelWorkHourDrawing.Name = "btnDelWorkHourDrawing";
            this.btnDelWorkHourDrawing.Size = new System.Drawing.Size(242, 23);
            this.btnDelWorkHourDrawing.TabIndex = 17;
            this.btnDelWorkHourDrawing.Text = "Удалить лишние WorkHourDrawing";
            this.btnDelWorkHourDrawing.UseVisualStyleBackColor = true;
            this.btnDelWorkHourDrawing.Click += new System.EventHandler(this.btnDelWorkHourDrawing_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(370, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(242, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Починить имена тех. маршрутов";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnCheckDeletedTO
            // 
            this.btnCheckDeletedTO.Location = new System.Drawing.Point(12, 41);
            this.btnCheckDeletedTO.Name = "btnCheckDeletedTO";
            this.btnCheckDeletedTO.Size = new System.Drawing.Size(242, 23);
            this.btnCheckDeletedTO.TabIndex = 18;
            this.btnCheckDeletedTO.Text = "Проверить удалённые ТО";
            this.btnCheckDeletedTO.UseVisualStyleBackColor = true;
            this.btnCheckDeletedTO.Click += new System.EventHandler(this.btnCheckDeletedTO_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(15, 463);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(597, 96);
            this.rtbLog.TabIndex = 20;
            this.rtbLog.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(370, 244);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(242, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "Восстановить удалённые чертежи";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 570);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCheckDeletedTO);
            this.Controls.Add(this.btnDelWorkHourDrawing);
            this.Controls.Add(this.btnCheckWorkHourDrawing);
            this.Controls.Add(this.btnRemoveHier);
            this.Controls.Add(this.btnCheckHier);
            this.Controls.Add(this.btnRebuildIndexes);
            this.Controls.Add(this.btnDelUnusableCollection);
            this.Controls.Add(this.btnCheckUnusableCollection);
            this.Controls.Add(this.btnDelUnusableTechRoutes);
            this.Controls.Add(this.btnCheckUnusableTechRoutes);
            this.Controls.Add(this.progressPanel1);
            this.Controls.Add(this.btnRepairDoubles);
            this.Controls.Add(this.btnCheckDoubles);
            this.Controls.Add(this.cbCheckAll);
            this.Controls.Add(this.btnRepairTechRoutesName);
            this.Controls.Add(this.clbValues);
            this.Controls.Add(this.btnCheckTechRoutesName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Утилиты обслуживания БД";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCheckTechRoutesName;
        private System.Windows.Forms.CheckedListBox clbValues;
        private System.Windows.Forms.Button btnRepairTechRoutesName;
        private System.Windows.Forms.CheckBox cbCheckAll;
        private System.Windows.Forms.Button btnCheckDoubles;
        private System.Windows.Forms.Button btnRepairDoubles;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel1;
        private System.Windows.Forms.Button btnCheckUnusableTechRoutes;
        private System.Windows.Forms.Button btnDelUnusableTechRoutes;
        private System.Windows.Forms.Button btnCheckUnusableCollection;
        private System.Windows.Forms.Button btnDelUnusableCollection;
        private System.Windows.Forms.Button btnRebuildIndexes;
        private System.Windows.Forms.Button btnCheckHier;
        private System.Windows.Forms.Button btnRemoveHier;
        private System.Windows.Forms.Button btnCheckWorkHourDrawing;
        private System.Windows.Forms.Button btnDelWorkHourDrawing;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCheckDeletedTO;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button button2;
    }
}

