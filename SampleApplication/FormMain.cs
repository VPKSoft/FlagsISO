#region License
/*
FlagsISO

A library containing country (and some other) flags.
Copyright (C) 2015 VPKSoft, Petteri Kautonen

Contact: vpksoft@vpksoft.net

This file is part of FlagsISO.

FlagsISO is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

FlagsISO is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with FlagsISO.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;
using FlagsISO;
using System.IO;

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

    private void lbIconList_SelectedIndexChanged(object sender, EventArgs e)
    {
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

        using var memoryStream = new MemoryStream(CountryFlagsISO.GetForCountry(lbIconList.SelectedItem.ToString(), size, cbShiny.Checked));
        var bitmap = new Bitmap(memoryStream);

        pbFlag.Image = bitmap;
        lbCountryLongName.Text = CountryFlagsISO.GetNameByTwoLetterISO(lbIconList.SelectedItem.ToString());
    }
}