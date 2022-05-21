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

namespace FlagsISO;

/// <summary>
/// A size of the flag in pixels (NxN).
/// </summary>
public enum FlagSizes
{
    /// <summary>
    /// 16x16 bitmap.
    /// </summary>
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
    Size_x_64
};

/// <summary>
/// A class to get coutry flags to your application.
/// </summary>
public class CountryFlagsISO
{
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
        return (byte[])FlagIcons.ResourceManager.GetObject(resBitmapName);
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
    /// <para/>have globally accepted country status or is't
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
                // do nothin
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
                new RegionInfo(i.LCID);
                retVal.Add(i);
            }
            catch
            {
                // do nothin
            }
        }
        return retVal;
    }
}