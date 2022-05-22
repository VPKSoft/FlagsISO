#region License
/*
MIT License

Copyright(c) 2022 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

namespace SampleApplication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.pbFlag = new System.Windows.Forms.PictureBox();
            this.lbIconList = new System.Windows.Forms.ListBox();
            this.gbSizes = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.rb64 = new System.Windows.Forms.RadioButton();
            this.rb48 = new System.Windows.Forms.RadioButton();
            this.rb32 = new System.Windows.Forms.RadioButton();
            this.rb24 = new System.Windows.Forms.RadioButton();
            this.rb16 = new System.Windows.Forms.RadioButton();
            this.cbShiny = new System.Windows.Forms.CheckBox();
            this.lbCountryLongName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbFlag)).BeginInit();
            this.gbSizes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbFlag
            // 
            this.pbFlag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbFlag.Location = new System.Drawing.Point(251, 206);
            this.pbFlag.Name = "pbFlag";
            this.pbFlag.Size = new System.Drawing.Size(146, 88);
            this.pbFlag.TabIndex = 3;
            this.pbFlag.TabStop = false;
            // 
            // lbIconList
            // 
            this.lbIconList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbIconList.FormattingEnabled = true;
            this.lbIconList.ItemHeight = 15;
            this.lbIconList.Location = new System.Drawing.Point(12, 12);
            this.lbIconList.Name = "lbIconList";
            this.lbIconList.Size = new System.Drawing.Size(227, 289);
            this.lbIconList.TabIndex = 5;
            this.lbIconList.SelectedIndexChanged += new System.EventHandler(this.lbIconList_SelectedIndexChanged);
            // 
            // gbSizes
            // 
            this.gbSizes.Controls.Add(this.numericUpDown1);
            this.gbSizes.Controls.Add(this.rbCustom);
            this.gbSizes.Controls.Add(this.rb64);
            this.gbSizes.Controls.Add(this.rb48);
            this.gbSizes.Controls.Add(this.rb32);
            this.gbSizes.Controls.Add(this.rb24);
            this.gbSizes.Controls.Add(this.rb16);
            this.gbSizes.Location = new System.Drawing.Point(245, 12);
            this.gbSizes.Name = "gbSizes";
            this.gbSizes.Size = new System.Drawing.Size(152, 165);
            this.gbSizes.TabIndex = 6;
            this.gbSizes.TabStop = false;
            this.gbSizes.Text = "Flag size";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(92, 136);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(54, 23);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.lbIconList_SelectedIndexChanged);
            // 
            // rbCustom
            // 
            this.rbCustom.AutoSize = true;
            this.rbCustom.Location = new System.Drawing.Point(6, 136);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(67, 19);
            this.rbCustom.TabIndex = 5;
            this.rbCustom.Text = "Custom";
            this.rbCustom.UseVisualStyleBackColor = true;
            this.rbCustom.Click += new System.EventHandler(this.lbIconList_SelectedIndexChanged);
            // 
            // rb64
            // 
            this.rb64.AutoSize = true;
            this.rb64.Checked = true;
            this.rb64.Location = new System.Drawing.Point(6, 111);
            this.rb64.Name = "rb64";
            this.rb64.Size = new System.Drawing.Size(55, 19);
            this.rb64.TabIndex = 4;
            this.rb64.TabStop = true;
            this.rb64.Text = "64x64";
            this.rb64.UseVisualStyleBackColor = true;
            this.rb64.Click += new System.EventHandler(this.lbIconList_SelectedIndexChanged);
            // 
            // rb48
            // 
            this.rb48.AutoSize = true;
            this.rb48.Location = new System.Drawing.Point(6, 88);
            this.rb48.Name = "rb48";
            this.rb48.Size = new System.Drawing.Size(55, 19);
            this.rb48.TabIndex = 3;
            this.rb48.Text = "48x48";
            this.rb48.UseVisualStyleBackColor = true;
            this.rb48.Click += new System.EventHandler(this.lbIconList_SelectedIndexChanged);
            // 
            // rb32
            // 
            this.rb32.AutoSize = true;
            this.rb32.Location = new System.Drawing.Point(6, 65);
            this.rb32.Name = "rb32";
            this.rb32.Size = new System.Drawing.Size(55, 19);
            this.rb32.TabIndex = 2;
            this.rb32.Text = "32x32";
            this.rb32.UseVisualStyleBackColor = true;
            this.rb32.Click += new System.EventHandler(this.lbIconList_SelectedIndexChanged);
            // 
            // rb24
            // 
            this.rb24.AutoSize = true;
            this.rb24.Location = new System.Drawing.Point(6, 42);
            this.rb24.Name = "rb24";
            this.rb24.Size = new System.Drawing.Size(55, 19);
            this.rb24.TabIndex = 1;
            this.rb24.Text = "24x24";
            this.rb24.UseVisualStyleBackColor = true;
            this.rb24.Click += new System.EventHandler(this.lbIconList_SelectedIndexChanged);
            // 
            // rb16
            // 
            this.rb16.AutoSize = true;
            this.rb16.Location = new System.Drawing.Point(6, 19);
            this.rb16.Name = "rb16";
            this.rb16.Size = new System.Drawing.Size(55, 19);
            this.rb16.TabIndex = 0;
            this.rb16.Text = "16x16";
            this.rb16.UseVisualStyleBackColor = true;
            this.rb16.Click += new System.EventHandler(this.lbIconList_SelectedIndexChanged);
            // 
            // cbShiny
            // 
            this.cbShiny.AutoSize = true;
            this.cbShiny.Location = new System.Drawing.Point(251, 183);
            this.cbShiny.Name = "cbShiny";
            this.cbShiny.Size = new System.Drawing.Size(55, 19);
            this.cbShiny.TabIndex = 7;
            this.cbShiny.Text = "Shiny";
            this.cbShiny.UseVisualStyleBackColor = true;
            this.cbShiny.CheckedChanged += new System.EventHandler(this.lbIconList_SelectedIndexChanged);
            // 
            // lbCountryLongName
            // 
            this.lbCountryLongName.AutoSize = true;
            this.lbCountryLongName.Location = new System.Drawing.Point(251, 297);
            this.lbCountryLongName.Name = "lbCountryLongName";
            this.lbCountryLongName.Size = new System.Drawing.Size(119, 15);
            this.lbCountryLongName.TabIndex = 8;
            this.lbCountryLongName.Text = "lbCountryLongName";
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(414, 321);
            this.Controls.Add(this.lbCountryLongName);
            this.Controls.Add(this.cbShiny);
            this.Controls.Add(this.gbSizes);
            this.Controls.Add(this.lbIconList);
            this.Controls.Add(this.pbFlag);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "Sample application (FlagsISO)";
            ((System.ComponentModel.ISupportInitialize)(this.pbFlag)).EndInit();
            this.gbSizes.ResumeLayout(false);
            this.gbSizes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFlag;
        private System.Windows.Forms.ListBox lbIconList;
        private System.Windows.Forms.GroupBox gbSizes;
        private System.Windows.Forms.RadioButton rb64;
        private System.Windows.Forms.RadioButton rb48;
        private System.Windows.Forms.RadioButton rb32;
        private System.Windows.Forms.RadioButton rb24;
        private System.Windows.Forms.RadioButton rb16;
        private System.Windows.Forms.CheckBox cbShiny;
        private System.Windows.Forms.Label lbCountryLongName;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.RadioButton rbCustom;
    }
}


