using System;
using System.Globalization;
using System.Xml;

/// <summary>
/// This class is being inherited by the other classes within the Serialization folder
/// and is being used for data extraction from the OSM XML files.
/// </summary>
class BaseOsm
{
    /// <summary>
    /// Exctracts the data values from the data source and converts their data type.
    /// </summary>
    /// <typeparam name="T">data type</typeparam>
    /// <param name="attrName">name of the attribute</param>
    /// <param name="attributes">the collection of attributes within the data</param>
    /// <returns>The value of the attribute converted to the required type</returns>
    protected T GetAttribute<T>(string attrName, XmlAttributeCollection attributes)
    {
        string strValue = attributes[attrName].Value;
        return (T)Convert.ChangeType(strValue, typeof(T));
    }

    /// <summary>
    /// Extracts the data values from the data source wich are of type float.
    /// </summary>
    /// <param name="attrName">name of the attribute</param>
    /// <param name="attributes">the collection of attributes within the data</param>
    /// <returns>The value of the attribute converted to float</returns>
    protected float GetFloat(string attrName, XmlAttributeCollection attributes)
    {
        string strValue = attributes[attrName].Value;
        return float.Parse(strValue, new CultureInfo("en-US").NumberFormat);
    }
}



