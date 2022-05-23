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
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using FlagsISO.Enumerations;

namespace FlagsISO;

/// <summary>
/// A size of the flag in pixels (NxN).
/// </summary>
public enum FlagSizes
{
    /// <summary>
    /// 16x16 bitmap.
    /// </summary>
    // ReSharper disable five times InconsistentNaming
    Size_x_16,
    /// <summary>
    /// 24 x 24 bitmap.
    /// </summary>
    Size_x_24,
    /// <summary>
    /// 32x32 bitmap.
    /// </summary>
    Size_x_32,
    /// <summary>
    /// 48x48 bitmap.
    /// </summary>
    Size_x_48,
    /// <summary>
    /// 64x64 bitmap.
    /// </summary>
    Size_x_64,
};

/// <summary>
/// A class to get country flags to your application.
/// </summary>
public static class CountryFlagsISO
{
    /// <summary>
    /// Gets the SVG flag two letter ISO codes.
    /// </summary>
    /// <value>The SVG flag two letter ISO codes.</value>
    public static IReadOnlyList<string> SvgFlagCodes { get; } = new List<string>(new[]
    {
        "ac", "ad", "ae", "af", "ag", "ai", "al", "am", "ao", "aq", "ar", "as", "at", "au", "aw", "ax", "az", "ba",
        "bb", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bl", "bm", "bn", "bo", "bq", "br", "bs", "bt", "bv", "bw",
        "by", "bz", "ca", "cc", "cd", "cefta", "cf", "cg", "ch", "ci", "ck", "cl", "cm", "cn", "co", "cp", "cr", "cu",
        "cv", "cw", "cx", "cy", "cz", "de", "dg", "dj", "dk", "dm", "do", "dz", "ea", "ec", "ee", "eg", "eh", "er",
        "es-ct", "es-ga", "es", "et", "eu", "fi", "fj", "fk", "fm", "fo", "fr", "ga", "gb-eng", "gb-nir", "gb-sct",
        "gb-wls", "gb", "gd", "ge", "gf", "gg", "gh", "gi", "gl", "gm", "gn", "gp", "gq", "gr", "gs", "gt", "gu", "gw",
        "gy", "hk", "hm", "hn", "hr", "ht", "hu", "ic", "id", "ie", "il", "im", "in", "io", "iq", "ir", "is", "it",
        "je", "jm", "jo", "jp", "ke", "kg", "kh", "ki", "km", "kn", "kp", "kr", "kw", "ky", "kz", "la", "lb", "lc",
        "li", "lk", "lr", "ls", "lt", "lu", "lv", "ly", "ma", "mc", "md", "me", "mf", "mg", "mh", "mk", "ml", "mm",
        "mn", "mo", "mp", "mq", "mr", "ms", "mt", "mu", "mv", "mw", "mx", "my", "mz", "na", "nc", "ne", "nf", "ng",
        "ni", "nl", "no", "np", "nr", "nu", "nz", "om", "pa", "pe", "pf", "pg", "ph", "pk", "pl", "pm", "pn", "pr",
        "ps", "pt", "pw", "py", "qa", "re", "ro", "rs", "ru", "rw", "sa", "sb", "sc", "sd", "se", "sg", "sh", "si",
        "sj", "sk", "sl", "sm", "sn", "so", "sr", "ss", "st", "sv", "sx", "sy", "sz", "ta", "tc", "td", "tf", "tg",
        "th", "tj", "tk", "tl", "tm", "tn", "to", "tr", "tt", "tv", "tw", "tz", "ua", "ug", "um", "un", "us", "uy",
        "uz", "va", "vc", "ve", "vg", "vi", "vn", "vu", "wf", "ws", "xk", "xx", "ye", "yt", "za", "zm", "zw",
    });

    /// <summary>
    /// Gets the internal flag size enumeration used on the library.
    /// </summary>
    public static FlagSizes FlagSizeInternal(FlagSizeType flagSize)
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

    /// <summary>
    /// Get the assigned flag size in pixels.
    /// </summary>
    public static int FlagSizePixels(FlagSizeType flagSize)
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

    /// <summary>
    /// Gets a bitmap of a country flag.
    /// </summary>
    /// <param name="culture">CultureInfo class instance to use to get the flag.</param>
    /// <param name="flagSize">A FlagSizes enumeration which tells the size of the flag.</param>
    /// <param name="shiny">If true a a flag is returned with "lovely glossy finish".</param>
    /// <returns>A byte[] containing the requested flag or null if the flag was not found.</returns>
    public static byte[] GetForCountry(CultureInfo culture, FlagSizes flagSize, bool shiny)
    {
        RegionInfo ri = new RegionInfo(culture.Name);
        return GetForCountry(ri, flagSize, shiny);
    }

    /// <summary>
    /// Gets a bitmap of a country flag.
    /// </summary>
    /// <param name="culture">CultureInfo class instance to use to get the flag.</param>
    /// <param name="flagSize">A FlagSizes enumeration which tells the size of the flag.</param>
    /// <returns>A byte[] containing the requested flag or null if the flag was not found.</returns>
    public static byte[] GetForCountry(CultureInfo culture, FlagSizes flagSize)
    {
        RegionInfo ri = new RegionInfo(culture.Name);
        return GetForCountry(ri, flagSize, false);
    }

    /// <summary>
    /// Gets a bitmap of a country flag.
    /// </summary>
    /// <param name="ri">A RegionInfo class instance to use to get the flag.</param>
    /// <param name="flagSize">A FlagSizes enumeration which tells the size of the flag.</param>
    /// <param name="shiny">If true a a flag is returned with "lovely glossy finish".</param>
    /// <returns>A byte[] containing the requested flag or null if the flag was not found.</returns>
    public static byte[] GetForCountry(RegionInfo ri, FlagSizes flagSize, bool shiny)
    {
        return GetForCountry(ri.TwoLetterISORegionName, flagSize, shiny);
    }

    /// <summary>
    /// Gets a bitmap of a country flag.
    /// </summary>
    /// <param name="ri">A RegionInfo class instance to use to get the flag.</param>
    /// <param name="flagSize">A FlagSizes enumeration which tells the size of the flag.</param>
    /// <returns>A byte[] containing the requested flag or null if the flag was not found.</returns>
    public static byte[] GetForCountry(RegionInfo ri, FlagSizes flagSize)
    {
        return GetForCountry(ri.TwoLetterISORegionName, flagSize, false);
    }

    /// <summary>
    /// Gets a bitmap of a country flag.
    /// </summary>
    /// <param name="name">A two letter ISO country name.</param>
    /// <param name="flagSize">A FlagSizes enumeration which tells the size of the flag.</param>
    /// <param name="shiny">If true a a flag is returned with "lovely glossy finish".</param>
    /// <returns>A byte[] containing the requested flag or null if the flag was not found.</returns>
    public static byte[] GetForCountry(string name, FlagSizes flagSize, bool shiny)
    {
        name = name.ToUpperInvariant();

        string resBitmapName = shiny ? "shiny" : "flat";
        if (flagSize == FlagSizes.Size_x_16)
        {
            resBitmapName += "_16x";
        }
        else if (flagSize == FlagSizes.Size_x_24)
        {
            resBitmapName += "_24x";
        }
        else if (flagSize == FlagSizes.Size_x_32)
        {
            resBitmapName += "_32x";
        }
        else if (flagSize == FlagSizes.Size_x_48)
        {
            resBitmapName += "_48x";
        }
        else if (flagSize == FlagSizes.Size_x_64)
        {
            resBitmapName += "_64x";
        }
        resBitmapName += name;
        return (byte[]?)FlagIcons.ResourceManager.GetObject(resBitmapName) ?? Array.Empty<byte>();
    }

    /// <summary>
    /// Gets a bitmap of a country flag.
    /// </summary>
    /// <param name="name">A two letter ISO country name.</param>
    /// <param name="flagSize">A FlagSizes enumeration which tells the size of the flag.</param>
    /// <returns>A byte[] containing the requested flag or null if the flag was not found.</returns>
    public static byte[] GetForCountry(string name, FlagSizes flagSize)
    {
        return GetForCountry(name, flagSize, false);
    }

    /// <summary>
    /// A list of country codes in upper case which country
    /// <para/>flags are included in this library. The codes
    /// <para/>are in two letter ISO country names except for
    /// <para/>few that aren't considered as countries or don't
    /// <para/>have globally accepted country status or isn't
    /// <para/>a country (e.g. "_united_nations").
    /// </summary>
    /// <returns>A list of strings in two letter ISO country names.</returns>
    public static List<string> GetCountries()
    {
        List<string> retVal = new List<string>();
        retVal.AddRange(FlagIcons.countryList.Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries));
        retVal.Sort();
        return retVal;
    }

    /// <summary>
    /// Returns the country name by it's two letter ISO country name. 
    /// </summary>
    /// <param name="twoISO">Two letter ISO country name.</param>
    /// <returns>Country name</returns>
    public static string GetNameByTwoLetterISO(string twoISO)
    {
        string[] longCountryList = FlagIcons.countryListLong.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, string> dict = new Dictionary<string, string>();
        for (int i = 0; i < longCountryList.Length; i++)
        {
            dict.Add(longCountryList[i].Split('=')[1], longCountryList[i].Split('=')[0]);
        }
        try
        {
            return dict[twoISO].Replace("-", " ");
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// A list of regions of which are supported
    /// <para/>by the .NET Framework.
    /// </summary>
    /// <returns>A list of RegionInfo instances.</returns>
    public static List<RegionInfo> GetRegions()
    {
        List<RegionInfo> retVal = new List<RegionInfo>();
        CultureInfo[] ciArr = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
        foreach (CultureInfo i in ciArr)
        {
            try
            {
                retVal.Add(new RegionInfo(i.LCID));
            }
            catch
            {
                // do nothing
            }
        }
        return retVal;
    }

    /// <summary>
    /// A list of cultures of which are supported
    /// <para/>by the .NET Framework that also can be converted
    /// <para/>to regions.
    /// </summary>
    /// <returns>A list of RegionInfo instances.</returns>        
    public static List<CultureInfo> GetCultures()
    {
        List<CultureInfo> retVal = new List<CultureInfo>();
        CultureInfo[] ciArr = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
        foreach (CultureInfo i in ciArr)
        {
            try
            {
                _ = new RegionInfo(i.LCID);
                retVal.Add(i);
            }
            catch
            {
                // do nothing
            }
        }
        return retVal;
    }
}