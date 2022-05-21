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

using System.ComponentModel;
using System.Globalization;

namespace FlagsISO.WinForms;

/// <summary>
/// A combo box control for country selection.
/// </summary>
public partial class ComboBoxCountrySelect : ComboBox
{
    /// <summary>
    /// The class constructor.
    /// </summary>
    public ComboBoxCountrySelect()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Reconstructs the combo box drawing arguments if on a a property change.
    /// </summary>
    private void ReInit()
    {
        this.Items.Clear();
        this.IntegralHeight = false;
        List<string> countriesTwoLetter = FlagsISO.CountryFlagsISO.GetCountries();
        List<KeyValuePair<string, string>> countries = new List<KeyValuePair<string, string>>();

        int currentIndex = 0, counter = 0;

        foreach (string twoLetter in countriesTwoLetter)
        {
            if (CountryFlagsISO.GetNameByTwoLetterISO(twoLetter) != string.Empty)
            {
                RegionInfo ri;
                try
                {
                    ri = new RegionInfo(twoLetter);
                }
                catch
                {
                    continue;
                }
                countries.Add(new KeyValuePair<string, string>(twoLetter, useNativeName ? ri.NativeName : ri.EnglishName));
            }
        }
        countries.Sort((a, b) => a.Value.CompareTo(b.Value)); // Sort the country list alphabetical order.
        foreach (KeyValuePair<string, string> country in countries)
        {
            try
            {
                RegionInfo ri;
                ri = new RegionInfo(country.Key);
                if (ri.Equals(RegionInfo.CurrentRegion))
                {
                    currentIndex = counter;
                }
                counter++;
            }
            catch
            {

            }
        }

        foreach (KeyValuePair<string, string> country in countries)
        {
            this.Items.Add(country);
        }

        if (this.Items.Count > 0 && currentIndex < this.Items.Count)
        {
            this.SelectedIndex = currentIndex;
        }
        else if (this.Items.Count > 0)
        {
            this.SelectedIndex = 0;
        }

        this.DropDownStyle = ComboBoxStyle.DropDownList;

        DrawItem -= ComboBoxCountrySelect_DrawItem!; // Lets not stack them

        DrawItem += ComboBoxCountrySelect_DrawItem!; // Attach the drawing event
        DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed; // Set the drawing mode
    }

    Size stringSize = new Size();

    /// <summary>
    /// The custom draw event of the ComboBox.
    /// </summary>
    /// <param name="sender">The sender for the event.</param>
    /// <param name="e">Provides data for the DrawItem event.</param>
    void ComboBoxCountrySelect_DrawItem(object sender, DrawItemEventArgs e)
    {
        e.DrawBackground();
        if (e.Index == -1)
        {
            return;
        }

        string drawString = ((KeyValuePair<string, string>)this.Items[e.Index]).Value;

        try
        {
            stringSize = e.Graphics.MeasureString(drawString, this.Font).ToSize();
        }
        catch
        {
            // ignored
        }

        using var memoryStream = new MemoryStream(CountryFlagsISO.GetForCountry(((KeyValuePair<string, string>)this.Items[e.Index]).Key, FlagSizeInternal, !dontGetShiny));
        using var bitmap = new Bitmap(memoryStream);

        e.Graphics.DrawImage(bitmap, new Point(e.Bounds.Left, e.Bounds.Top));

        try
        {
            e.Graphics.DrawString(drawString,
                this.Font,
                new SolidBrush(this.ForeColor),
                new PointF(this.RightToLeft == RightToLeft.Yes ? e.Bounds.Right - stringSize.Width : e.Bounds.Left + FlagSizePixels + 2,
                    e.Bounds.Top + e.Bounds.Height / 2 - stringSize.Height / 2));
        }
        catch
        {

        }
    }

    /// <summary>
    /// Leaves the given regions to the ComboBox and removes the others.
    /// </summary>
    /// <param name="regions">A list of RegionInfo class instances<para/>
    /// to leave to the ComboBox.</param>
    public void LeaveRegions(params RegionInfo[] regions)
    {
        LeaveRegions(new List<RegionInfo>(regions));
    }

    /// <summary>
    /// Leaves the given regions to the ComboBox and removes the others.
    /// </summary>
    /// <param name="regions">A list of RegionInfo class instances<para/>
    /// to leave to the ComboBox.</param>
    public void LeaveRegions(List<RegionInfo> regions)
    {
        if (regions.Count == 0)
        {
            return;
        }

        for (int i = this.Items.Count - 1; i >= 0; i--)
        {
            if (!regions.Exists((r) => r.TwoLetterISORegionName == ((KeyValuePair<string, string>)this.Items[i]).Key))
            {
                this.Items.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Removes the given regions from the ComboBox and leaves the others.
    /// </summary>
    /// <param name="regions">A list of RegionInfo class instances<para/>
    /// to remove from the ComboBox.</param>
    public void RemoveRegions(params RegionInfo[] regions)
    {
        RemoveRegions(new List<RegionInfo>(regions));
    }

    /// <summary>
    /// Removes the given regions from the ComboBox and leaves the others.
    /// </summary>
    /// <param name="regions">A list of RegionInfo class instances<para/>
    /// to remove from the ComboBox.</param>
    public void RemoveRegions(List<RegionInfo> regions)
    {
        if (regions.Count == 0)
        {
            return;
        }

        for (int i = this.Items.Count - 1; i >= 0; i--)
        {
            if (regions.Exists((r) => r.TwoLetterISORegionName == ((KeyValuePair<string, string>)this.Items[i]).Key))
            {
                this.Items.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Fills the ComboBox with default regions.
    /// </summary>
    public void ResetRegions()
    {
        ReInit();
    }


    #region CountryFlagsISO

    /// <summary>
    /// A value indicating wether to use a native or english name of the regions in the ComboBox.
    /// </summary>
    private bool useNativeName = false;

    /// <summary>
    /// A value indicating wether to use shiny flag icons in the ComboBox.
    /// </summary>
    private bool dontGetShiny = false;

    /// <summary>
    /// An icon size in pixels to use in the ComboBox.
    /// </summary>
    public enum FlagSizeType
    {
        /// <summary>
        /// 16 pixel icon.
        /// </summary>
        Size16,

        /// <summary>
        /// 24 pixel icon.
        /// </summary>
        Size24,

        /// <summary>
        /// 32 pixel icon.
        /// </summary>
        Size32,

        /// <summary>
        /// 48 pixel icon.
        /// </summary>
        Size48,

        /// <summary>
        /// 64 pixel icon.
        /// </summary>
        Size64
    }

    /// <summary>
    /// Gets or sets a value indicating wether to use a native or english name of the regions in the ComboBox.
    /// </summary>
    [Description("Gets or sets a value indicating wether to use a native or english name of the regions in the ComboBox."), Category("CountryFlagsISO")]
    public bool UseNativeName
    {
        get
        {
            return useNativeName;
        }

        set
        {
            useNativeName = value;
            ReInit();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating wether to use shiny flag icons in the ComboBox.
    /// </summary>
    [Description("Gets or sets a value indicating wether to use shiny flag icons in the ComboBox."), Category("CountryFlagsISO")]
    public bool DontGetShiny
    {
        get
        {
            return dontGetShiny;
        }

        set
        {
            dontGetShiny = value;
            ReInit();
        }
    }

    /// <summary>
    /// Gets or sets the selected region of the ComboBox.
    /// </summary>
    [Description("Gets or sets the selected region of the ComboBox."), Category("CountryFlagsISO")]
    public RegionInfo SelectedRegion
    {
        get
        {
            return this.SelectedIndex == -1 ? RegionInfo.CurrentRegion : new RegionInfo(((KeyValuePair<string, string>)this.Items[this.SelectedIndex]).Key);
        }

        set
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (((KeyValuePair<string, string>)this.Items[i]).Key == value.TwoLetterISORegionName)
                {
                    this.SelectedIndex = i;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Gets the selected region's CultureInfo class instance of the ComboBox.
    /// </summary>
    [Description("Gets the selected region's CultureInfo class instance of the ComboBox."), Category("CountryFlagsISO")]
    public CultureInfo SelectedCulture
    {
        get
        {
            return CultureInfo.GetCultureInfoByIetfLanguageTag(SelectedRegion.TwoLetterISORegionName.ToLower());
        }
    }

    /// <summary>
    /// Gets a two letter ISO country name of the selected region of the ComboBox.
    /// </summary>
    [Description("Gets a two letter ISO country name of the selected region of the ComboBox."), Category("CountryFlagsISO")]
    public string SelectedTwoLetterIsoName
    {
        get
        {
            return SelectedRegion.TwoLetterISORegionName;
        }
    }

    /// <summary>
    /// Gets a native country name of the selected region of the ComboBox.
    /// </summary>
    [Description("Gets a native country name of the selected region of the ComboBox."), Category("CountryFlagsISO")]
    public string SelectedNativeName
    {
        get
        {
            return SelectedRegion.NativeName;
        }
    }

    /// <summary>
    /// Gets an english country name of the selected region of the ComboBox.
    /// </summary>
    [Description("Gets an english country name of the selected region of the ComboBox."), Category("CountryFlagsISO")]
    public string SelectedEnglishName
    {
        get
        {
            return SelectedRegion.EnglishName;
        }
    }

    private FlagSizeType flagSize = FlagSizeType.Size16;

    /// <summary>
    /// Gets or sets the flag size used in the ComboBox.
    /// </summary>
    [Description("Gets or sets the flag size used in the ComboBox."), Category("CountryFlagsISO")]
    public FlagSizeType FlagSize
    {
        get
        {
            return flagSize;
        }

        set
        {
            flagSize = value;
            switch (value)
            {
                case FlagSizeType.Size16:
                    ItemHeight = 15;
                    break;
                case FlagSizeType.Size24:
                    ItemHeight = 23;
                    break;
                case FlagSizeType.Size32:
                    ItemHeight = 31;
                    break;
                case FlagSizeType.Size48:
                    ItemHeight = 47;
                    break;
                case FlagSizeType.Size64:
                    ItemHeight = 63;
                    break;
            }
        }
    }
    #endregion

    /// <summary>
    /// Gets the internal flag size enumeration used on the library.
    /// </summary>
    private FlagSizes FlagSizeInternal
    {
        get
        {
            switch (flagSize)
            {
                case FlagSizeType.Size16:
                    return FlagSizes.Size_x_16;
                case FlagSizeType.Size24:
                    return FlagSizes.Size_x_24;
                case FlagSizeType.Size32:
                    return FlagSizes.Size_x_32;
                case FlagSizeType.Size48:
                    return FlagSizes.Size_x_48;
                case FlagSizeType.Size64:
                    return FlagSizes.Size_x_64;
            }
            return FlagSizes.Size_x_16;
        }
    }

    /// <summary>
    /// Get the assigned flag size in pixels.
    /// </summary>
    private int FlagSizePixels
    {
        get
        {
            switch (flagSize)
            {
                case FlagSizeType.Size16:
                    return 16;
                case FlagSizeType.Size24:
                    return 24;
                case FlagSizeType.Size32:
                    return 32;
                case FlagSizeType.Size48:
                    return 48;
                case FlagSizeType.Size64:
                    return 64;
            }
            return 16;
        }
    }


    #region HideBase

    /// <summary>
    /// Gets or sets a value specifying the style of the combo box.
    /// </summary>
    [Browsable(false)]
    public new ComboBoxStyle DropDownStyle
    {
        get
        {
            return base.DropDownStyle;
        }

        set
        {
            base.DropDownStyle = value;
        }
    }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [Browsable(false)]
    public new string Text
    {
        get
        {
            return base.Text;
        }

        set
        {
            base.Text = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether your code or the operating system will handle drawing of elements in the list.
    /// </summary>
    [Browsable(false)]
    public new DrawMode DrawMode
    {
        get
        {
            return base.DrawMode;
        }

        set
        {
            base.DrawMode = value;
        }
    }

    /// <summary>
    /// Gets a custom System.Collections.Specialized.StringCollection to use when the AutoCompleteSource property is set to CustomSource.
    /// </summary>
    [Browsable(false)]
    public new AutoCompleteStringCollection AutoCompleteCustomSource
    {
        get
        {
            return base.AutoCompleteCustomSource;
        }
    }

    /// <summary>
    /// Gets an option that controls how automatic completion works for the ComboBox.
    /// </summary>
    [Browsable(false)]
    public new AutoCompleteMode AutoCompleteMode
    {
        get
        {
            return base.AutoCompleteMode;
        }
    }

    /// <summary>
    /// Gets a value specifying the source of complete strings used for automatic completion.
    /// </summary>
    [Browsable(false)]
    public new AutoCompleteSource AutoCompleteSource
    {
        get
        {
            return base.AutoCompleteSource;
        }
    }

    /// <summary>
    /// Gets the format-specifier characters that indicate how a value is to be displayed.
    /// </summary>
    [Browsable(false)]
    public new string FormatString
    {
        get
        {
            return base.FormatString;
        }
    }

    /// <summary>
    /// Gets a value indicating whether formatting is applied to the DisplayMember property of the ListControl.
    /// </summary>
    [Browsable(false)]
    public new bool FormattingEnabled
    {
        get
        {
            return base.FormattingEnabled;
        }
    }

    /// <summary>
    /// Gets the data source for this ComboBox.
    /// </summary>
    [Browsable(false)]
    public new object DataSource
    {
        get
        {
            return base.DataSource;
        }
    }

    /// <summary>
    /// Gets the data bindings for the control.
    /// </summary>
    [Browsable(false)]
    public new ControlBindingsCollection DataBindings
    {
        get
        {
            return base.DataBindings;
        }
    }

    /// <summary>
    /// Gets an object representing the collection of the items contained in this ComboBox.
    /// </summary>
    [Browsable(false)]
    public new ObjectCollection Items
    {
        get
        {
            return base.Items;

        }
    }

    /// <summary>
    /// Gets the path of the property to use as the actual value for the items in the ListControl.
    /// </summary>
    [Browsable(false)]
    public new string ValueMember
    {
        get
        {
            return base.ValueMember;
        }
    }

    /// <summary>
    /// Gets the property to display for this ListControl.
    /// </summary>
    [Browsable(false)]
    public new string DisplayMember
    {
        get
        {
            return base.DisplayMember;
        }
    }

    /// <summary>
    /// Gets or sets the height of an item in the combo box.
    /// </summary>
    [Browsable(false)]
    public new int ItemHeight
    {
        get
        {
            return base.ItemHeight;
        }

        set
        {
            base.ItemHeight = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control should resize to avoid showing partial items.
    /// </summary>
    [Browsable(false)]
    public new bool IntegralHeight
    {
        get
        {
            return base.IntegralHeight;
        }

        set
        {
            base.IntegralHeight = value;
        }
    }

    #endregion
}