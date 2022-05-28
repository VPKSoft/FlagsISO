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

using System;
using System.Windows.Forms;
using FlagsISO;

namespace SampleApplication;

public partial class FormMain : Form
{
    public FormMain()
    {
        InitializeComponent();
        foreach (string country in CountryFlagsISO.GetCountries())
        {
            lbIconList.Items.Add(country);
        }
        lbIconList.SelectedIndex = 0;
    }

    private FlagsISO.WinForms.ImageProvider provider = new();

    private void lbIconList_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbShiny.Enabled = !rbCustom.Checked;

        FlagsISO.FlagSizes size = FlagSizes.Size_x_16;
        if (rb16.Checked)
        {
            size = FlagsISO.FlagSizes.Size_x_16;
        }
        else if (rb24.Checked)
        {
            size = FlagsISO.FlagSizes.Size_x_24;
        }
        else if (rb32.Checked)
        {
            size = FlagsISO.FlagSizes.Size_x_32;
        }
        else if (rb48.Checked)
        {
            size = FlagsISO.FlagSizes.Size_x_48;
        }
        else if (rb64.Checked)
        {
            size = FlagsISO.FlagSizes.Size_x_64;
        }
        else if (rbCustom.Checked)
        {
            var bitmap = provider.GetScaledBitmap(lbIconList.SelectedItem.ToString(), (float)numericUpDown1.Value);
            pbFlag.Image = bitmap;
            lbCountryLongName.Text = CountryFlagsISO.GetNameByTwoLetterISO(lbIconList.SelectedItem.ToString());
            return;
        }

        var png = provider.GetImageBitmap(lbIconList.SelectedItem.ToString(), size, cbShiny.Checked);

        pbFlag.Image = png;

    }
}