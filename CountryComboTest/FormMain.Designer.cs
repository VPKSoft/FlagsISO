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

using FlagsISO.Enumerations;

namespace CountryComboTest
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
            this.btLeave = new System.Windows.Forms.Button();
            this.btRemove = new System.Windows.Forms.Button();
            this.btReset = new System.Windows.Forms.Button();
            this.cmbCountrySelect = new FlagsISO.WinForms.ComboBoxCountrySelect();
            this.SuspendLayout();
            // 
            // btLeave
            // 
            this.btLeave.Location = new System.Drawing.Point(12, 39);
            this.btLeave.Name = "btLeave";
            this.btLeave.Size = new System.Drawing.Size(260, 23);
            this.btLeave.TabIndex = 1;
            this.btLeave.Text = "Leave current region and US to the ComboBox.";
            this.btLeave.UseVisualStyleBackColor = true;
            this.btLeave.Click += new System.EventHandler(this.btLeave_Click);
            // 
            // btRemove
            // 
            this.btRemove.Location = new System.Drawing.Point(12, 68);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(260, 23);
            this.btRemove.TabIndex = 2;
            this.btRemove.Text = "Remove current region and US from the ComboBox.";
            this.btRemove.UseVisualStyleBackColor = true;
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
            // 
            // btReset
            // 
            this.btReset.Location = new System.Drawing.Point(12, 97);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(260, 23);
            this.btReset.TabIndex = 3;
            this.btReset.Text = "Reset the region list in the ComboBox to default.";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // cmbCountrySelect
            // 
            this.cmbCountrySelect.DontGetShiny = false;
            this.cmbCountrySelect.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCountrySelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountrySelect.FlagSize = FlagSizeType.Size16;
            this.cmbCountrySelect.IntegralHeight = false;
            this.cmbCountrySelect.ItemHeight = 15;
            this.cmbCountrySelect.Location = new System.Drawing.Point(12, 12);
            this.cmbCountrySelect.Name = "cmbCountrySelect";
            this.cmbCountrySelect.Size = new System.Drawing.Size(260, 21);
            this.cmbCountrySelect.TabIndex = 0;
            this.cmbCountrySelect.UseNativeName = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 131);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.btRemove);
            this.Controls.Add(this.btLeave);
            this.Controls.Add(this.cmbCountrySelect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "ContryComboTest";
            this.ResumeLayout(false);

        }

        #endregion

        private FlagsISO.WinForms.ComboBoxCountrySelect cmbCountrySelect;
        private System.Windows.Forms.Button btLeave;
        private System.Windows.Forms.Button btRemove;
        private System.Windows.Forms.Button btReset;
    }
}


