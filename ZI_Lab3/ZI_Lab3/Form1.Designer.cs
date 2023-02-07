
namespace ZI_Lab2
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
            this.rtbPlain = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fsw = new System.IO.FileSystemWatcher();
            this.btnEnable = new System.Windows.Forms.Button();
            this.btnDisable = new System.Windows.Forms.Button();
            this.ofdSourceFile = new System.Windows.Forms.OpenFileDialog();
            this.ofdTargetFile = new System.Windows.Forms.OpenFileDialog();
            this.btnSourceFileDialog = new System.Windows.Forms.Button();
            this.btnTargetFileDialog = new System.Windows.Forms.Button();
            this.txbSourceFile = new System.Windows.Forms.TextBox();
            this.txbTargetFile = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnChangeSetting = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txbKey = new System.Windows.Forms.TextBox();
            this.btnPick = new System.Windows.Forms.Button();
            this.cmbIzbor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbIV = new System.Windows.Forms.TextBox();
            this.ckbIV = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.fsw)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbPlain
            // 
            this.rtbPlain.Enabled = false;
            this.rtbPlain.Location = new System.Drawing.Point(90, 163);
            this.rtbPlain.Name = "rtbPlain";
            this.rtbPlain.Size = new System.Drawing.Size(688, 266);
            this.rtbPlain.TabIndex = 9;
            this.rtbPlain.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(384, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "PLAIN TEXT";
            // 
            // fsw
            // 
            this.fsw.EnableRaisingEvents = true;
            this.fsw.SynchronizingObject = this;
            this.fsw.Created += new System.IO.FileSystemEventHandler(this.fsw_Created);
            // 
            // btnEnable
            // 
            this.btnEnable.Location = new System.Drawing.Point(90, 435);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.Size = new System.Drawing.Size(92, 55);
            this.btnEnable.TabIndex = 13;
            this.btnEnable.Text = "Enable";
            this.btnEnable.UseVisualStyleBackColor = true;
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // btnDisable
            // 
            this.btnDisable.Location = new System.Drawing.Point(217, 435);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(92, 55);
            this.btnDisable.TabIndex = 14;
            this.btnDisable.Text = "Disable";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // ofdSourceFile
            // 
            this.ofdSourceFile.FileName = "openFileDialog1";
            // 
            // ofdTargetFile
            // 
            this.ofdTargetFile.FileName = "openFileDialog1";
            // 
            // btnSourceFileDialog
            // 
            this.btnSourceFileDialog.Location = new System.Drawing.Point(318, 91);
            this.btnSourceFileDialog.Name = "btnSourceFileDialog";
            this.btnSourceFileDialog.Size = new System.Drawing.Size(97, 37);
            this.btnSourceFileDialog.TabIndex = 15;
            this.btnSourceFileDialog.Text = "Source file";
            this.btnSourceFileDialog.UseVisualStyleBackColor = true;
            this.btnSourceFileDialog.Click += new System.EventHandler(this.btnSourceFileDialog_Click);
            // 
            // btnTargetFileDialog
            // 
            this.btnTargetFileDialog.Location = new System.Drawing.Point(669, 92);
            this.btnTargetFileDialog.Name = "btnTargetFileDialog";
            this.btnTargetFileDialog.Size = new System.Drawing.Size(122, 36);
            this.btnTargetFileDialog.TabIndex = 16;
            this.btnTargetFileDialog.Text = "Target file";
            this.btnTargetFileDialog.UseVisualStyleBackColor = true;
            this.btnTargetFileDialog.Click += new System.EventHandler(this.btnTargetFileDialog_Click);
            // 
            // txbSourceFile
            // 
            this.txbSourceFile.Location = new System.Drawing.Point(93, 93);
            this.txbSourceFile.Name = "txbSourceFile";
            this.txbSourceFile.Size = new System.Drawing.Size(209, 22);
            this.txbSourceFile.TabIndex = 17;
            // 
            // txbTargetFile
            // 
            this.txbTargetFile.Location = new System.Drawing.Point(479, 93);
            this.txbTargetFile.Name = "txbTargetFile";
            this.txbTargetFile.Size = new System.Drawing.Size(184, 22);
            this.txbTargetFile.TabIndex = 18;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(592, 435);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(90, 55);
            this.btnEncrypt.TabIndex = 19;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnChangeSetting
            // 
            this.btnChangeSetting.Location = new System.Drawing.Point(803, 247);
            this.btnChangeSetting.Name = "btnChangeSetting";
            this.btnChangeSetting.Size = new System.Drawing.Size(145, 72);
            this.btnChangeSetting.TabIndex = 32;
            this.btnChangeSetting.Text = "Change Enigma Settings";
            this.btnChangeSetting.UseVisualStyleBackColor = true;
            this.btnChangeSetting.Click += new System.EventHandler(this.btnChangeSetting_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(688, 435);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(90, 54);
            this.btnDecrypt.TabIndex = 33;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(813, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 48;
            this.label3.Text = "Key:";
            // 
            // txbKey
            // 
            this.txbKey.Location = new System.Drawing.Point(816, 163);
            this.txbKey.Name = "txbKey";
            this.txbKey.Size = new System.Drawing.Size(125, 22);
            this.txbKey.TabIndex = 47;
            // 
            // btnPick
            // 
            this.btnPick.Location = new System.Drawing.Point(451, 21);
            this.btnPick.Name = "btnPick";
            this.btnPick.Size = new System.Drawing.Size(75, 45);
            this.btnPick.TabIndex = 50;
            this.btnPick.Text = "Pick algorithm";
            this.btnPick.UseVisualStyleBackColor = true;
            this.btnPick.Click += new System.EventHandler(this.btnPick_Click);
            // 
            // cmbIzbor
            // 
            this.cmbIzbor.FormattingEnabled = true;
            this.cmbIzbor.Items.AddRange(new object[] {
            "Enigma",
            "XTEA"});
            this.cmbIzbor.Location = new System.Drawing.Point(305, 27);
            this.cmbIzbor.Name = "cmbIzbor";
            this.cmbIzbor.Size = new System.Drawing.Size(121, 24);
            this.cmbIzbor.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(563, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 17);
            this.label2.TabIndex = 52;
            this.label2.Text = "Initialization vector:";
            // 
            // txbIV
            // 
            this.txbIV.Location = new System.Drawing.Point(566, 44);
            this.txbIV.Name = "txbIV";
            this.txbIV.Size = new System.Drawing.Size(125, 22);
            this.txbIV.TabIndex = 51;
            // 
            // ckbIV
            // 
            this.ckbIV.AutoSize = true;
            this.ckbIV.Location = new System.Drawing.Point(711, 44);
            this.ckbIV.Name = "ckbIV";
            this.ckbIV.Size = new System.Drawing.Size(141, 21);
            this.ckbIV.TabIndex = 53;
            this.ckbIV.Text = "Enable/Disable IV";
            this.ckbIV.UseVisualStyleBackColor = true;
            this.ckbIV.CheckedChanged += new System.EventHandler(this.ckbIV_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 577);
            this.Controls.Add(this.ckbIV);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbIV);
            this.Controls.Add(this.btnPick);
            this.Controls.Add(this.cmbIzbor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txbKey);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnChangeSetting);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.txbTargetFile);
            this.Controls.Add(this.txbSourceFile);
            this.Controls.Add(this.btnTargetFileDialog);
            this.Controls.Add(this.btnSourceFileDialog);
            this.Controls.Add(this.btnDisable);
            this.Controls.Add(this.btnEnable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbPlain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "ENIGMA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.fsw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbPlain;
        private System.Windows.Forms.Label label1;
        private System.IO.FileSystemWatcher fsw;
        private System.Windows.Forms.Button btnEnable;
        private System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.TextBox txbTargetFile;
        private System.Windows.Forms.TextBox txbSourceFile;
        private System.Windows.Forms.Button btnTargetFileDialog;
        private System.Windows.Forms.Button btnSourceFileDialog;
        private System.Windows.Forms.OpenFileDialog ofdSourceFile;
        private System.Windows.Forms.OpenFileDialog ofdTargetFile;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnChangeSetting;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbKey;
        private System.Windows.Forms.Button btnPick;
        private System.Windows.Forms.ComboBox cmbIzbor;
        private System.Windows.Forms.CheckBox ckbIV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbIV;
    }
}

