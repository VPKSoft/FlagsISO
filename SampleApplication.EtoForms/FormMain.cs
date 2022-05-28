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
using Eto.Drawing;
using Eto.Forms;
using FlagsISO;
using FlagsISO.Enumerations;
using FlagsISO.EtoForms;

namespace SampleApplication.EtoForms;

/// <summary>
/// A form to display a moon phase calendar.
/// Implements the <see cref="Eto.Forms.Form" />
/// </summary>
/// <seealso cref="Eto.Forms.Form" />
public class FormMain : Form
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FormMain"/> class.
    /// </summary>
    public FormMain()
    {
        MinimumSize = new Size(500, 500);

        lbIconList = new ListBox { Height = 400, Width = 200, };
        rbSizes = new RadioButtonList { Items = { "16x16", "24x24", "32x32", "48x48", "64x64", "Custom", }, Orientation = Orientation.Vertical, Padding = 5, };
        rbSizes.SelectedIndexChanged += LbIconList_SelectedIndexChanged;

        iwFlag = new ImageView();
        nsImageWidth = new NumericStepper { MinValue = 70, MaxValue = 300, Value = 100, };
        nsImageWidth.ValueChanged += LbIconList_SelectedIndexChanged;

        cbShiny = new CheckBox { Text = "Shiny", ThreeState = false, };
        cbShiny.CheckedChanged += LbIconList_SelectedIndexChanged;

        var panelRight = new StackLayout
        {
            Items =
            {
                new Label { Text = "Flag sizes",},
                rbSizes,
                cbShiny,
                new Label { Text = "Custom size",},
                nsImageWidth,
                iwFlag,
                new Label { Text = "ComboBox sample",},
                new ComboBoxCountrySelect { UseGlossyImage = true, FlagSizeType = FlagSizeType.Size64, NameDisplayType = CountryNameDisplay.Native,},
            },
            Padding = 5,
        };


        Content = new TableLayout
        {
            Rows =
            {
                new TableRow(lbIconList, panelRight) { ScaleHeight = true,},
            },
        };

        foreach (string country in CountryFlagsISO.GetCountries())
        {
            lbIconList.Items.Add(country);
        }
        lbIconList.SelectedIndex = 0;

        lbIconList.SelectedIndexChanged += LbIconList_SelectedIndexChanged;
    }

    private readonly ImageProvider provider = new();

    private void LbIconList_SelectedIndexChanged(object? sender, EventArgs e)
    {
        var size = FlagSizes.Size_x_16;
        switch (rbSizes.SelectedIndex)
        {
            case 0:
                size = FlagSizes.Size_x_16;
                break;

            case 1:
                size = FlagSizes.Size_x_24;
                break;

            case 2:
                size = FlagSizes.Size_x_32;
                break;

            case 3:
                size = FlagSizes.Size_x_48;
                break;

            case 4:
                size = FlagSizes.Size_x_64;
                break;

            case 5:
                if (lbIconList.SelectedKey != null)
                {
                    iwFlag.Image = provider.GetScaledBitmap(lbIconList.SelectedKey.Trim('\r'), (float)nsImageWidth.Value);
                }
                return;
        }

        if (lbIconList.SelectedKey != null)
        {
            iwFlag.Image = provider.GetImage(lbIconList.SelectedKey.Trim('\r'), size, cbShiny.Checked ?? false);
        }
    }

    private readonly ListBox lbIconList;
    private readonly CheckBox cbShiny;
    private readonly RadioButtonList rbSizes;
    private readonly ImageView iwFlag;
    private readonly NumericStepper nsImageWidth;
}