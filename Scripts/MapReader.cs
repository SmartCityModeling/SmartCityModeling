﻿using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.SceneManagement;


/// <summary>
/// This script extracts the various data nodes from the XML file and generates
/// the respective instances based on the classes from the serialization folder.
/// The objects are then stored in dictionaries and lists to access these data
/// structures from other scripts. As soon as the data has been processed, the
/// variable "IsReady" turns true and the script "MapBuilder" is initiated.
/// </summary>
class MapReader : MonoBehaviour
{
    public ExampleMaps exampleMaps;

    public static OsmBounds bounds;  

    public static Dictionary<ulong, OsmNode> nodes;  

    public static Dictionary<ulong, OsmWay> ways;  

    public static List<OsmRelation> relations; 

    public static bool IsReady { get; private set; }  

    void Start()
    {
        nodes = new Dictionary<ulong, OsmNode>();
        ways = new Dictionary<ulong, OsmWay>();
        relations = new List<OsmRelation>();

        FileLoader.simulator_loaded = true;  

        XmlDocument doc = new XmlDocument();
        try
        {
            // The XML file is being loaded and the data nodes are being extracted using
            // the classes from the serialization folder.
            if(FileLoader.is_example)
            {
                doc.LoadXml(exampleMaps.ExampleMap[FileLoader.example_indx].text);
            }
            else
            {
                doc.Load(FileLoader.ResourceFilePath);
            }
            
            SetBounds(doc.SelectSingleNode("/osm/bounds"));
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetNodes(doc.SelectNodes("/osm/node"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways)
            {
                GetWays(doc.SelectNodes("/osm/way"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetRelations(doc.SelectNodes("/osm/relation"));
            }
        }

        // If the user input path to the XML file is not in XML format, the user is
        // being returned to the Main Menu with an error message.
        catch
        {
            SceneManager.LoadScene(0); 
        }

        // Triggers the start of the "MapBuilder" script.
        IsReady = true;  
    }

    /// <summary>
    /// Extracts the boundary information from the XML file and generates the corresponding
    /// instance.
    /// </summary>
    /// <param name="xmlNode">XML node</param>
    void SetBounds(XmlNode xmlNode)
    {
        bounds = new OsmBounds(xmlNode);
    }

    /// <summary>
    /// Extracts the nodes information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetNodes(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode n in xmlNodeList)
        {
            OsmNode node = new OsmNode(n);
            nodes[node.ID] = node;
        }
    }

    /// <summary>
    /// Extracts the way information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetWays(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        { 
            OsmWay way = new OsmWay(node);

            if (way.IsRailway == true)
            {
                if (UserPreferences.PublicTransportRailways)
                {
                    ways[way.ID] = way;
                }
            }
            else if (way.IsStreet == true)
            {
                if (UserPreferences.PublicTransportStreets)
                {
                    ways[way.ID] = way;
                }
            }
        }
    }

    /// <summary>
    /// Extract the relation information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetRelations(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        {
            OsmRelation relation = new OsmRelation(node);

            if(relation.Route == true)
            {
                relations.Add(relation);

                if (UserPreferences.PublicTransportRailways || UserPreferences.PublicTransportStreets)
                {
                    TagPublicTransportWays(relation);
                }

                if (UserPreferences.Stations)
                {
                    foreach (ulong NodeID in relation.StoppingNodeIDs)
                    {
                        try
                        {
                            nodes[NodeID].TransportLines.Add(relation.Name);
                            relation.StationNames.Add(nodes[NodeID].StationName);
                        }
                        catch (KeyNotFoundException)
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// If public transport roads and railroads are activated in the options menu,
    /// this function will extract the last data informations from these nodes.
    /// </summary>
    /// <param name="r">XML node</param>
    void TagPublicTransportWays(OsmRelation r)
    {
        foreach (ulong WayID in r.WayIDs)
        {
            try
            {
                switch (r.TransportType)
                {
                    case "subway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("subway");
                        break;
                    case "tram":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("tram");
                        break;
                    case "train":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("train");
                        break;
                    case "railway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("railway");
                        break;
                    case "light_rail":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("light_rail");
                        break;
                    case "bus":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportStreet = true;
                        ways[WayID].TransportTypes.Add("bus");
                        break;
                }
            }
            catch (KeyNotFoundException)
            {
                continue;
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.SceneManagement;


/// <summary>
/// This script extracts the various data nodes from the XML file and generates
/// the respective instances based on the classes from the serialization folder.
/// The objects are then stored in dictionaries and lists to access these data
/// structures from other scripts. As soon as the data has been processed, the
/// variable "IsReady" turns true and the script "MapBuilder" is initiated.
/// </summary>
class MapReader : MonoBehaviour
{
    public ExampleMaps exampleMaps;

    public static OsmBounds bounds;  

    public static Dictionary<ulong, OsmNode> nodes;  

    public static Dictionary<ulong, OsmWay> ways;  

    public static List<OsmRelation> relations; 

    public static bool IsReady { get; private set; }  

    void Start()
    {
        nodes = new Dictionary<ulong, OsmNode>();
        ways = new Dictionary<ulong, OsmWay>();
        relations = new List<OsmRelation>();

        FileLoader.simulator_loaded = true;  

        XmlDocument doc = new XmlDocument();
        try
        {
            // The XML file is being loaded and the data nodes are being extracted using
            // the classes from the serialization folder.
            if(FileLoader.is_example)
            {
                doc.LoadXml(exampleMaps.ExampleMap[FileLoader.example_indx].text);
            }
            else
            {
                doc.Load(FileLoader.ResourceFilePath);
            }
            
            SetBounds(doc.SelectSingleNode("/osm/bounds"));
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetNodes(doc.SelectNodes("/osm/node"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways)
            {
                GetWays(doc.SelectNodes("/osm/way"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetRelations(doc.SelectNodes("/osm/relation"));
            }
        }

        // If the user input path to the XML file is not in XML format, the user is
        // being returned to the Main Menu with an error message.
        catch
        {
            SceneManager.LoadScene(0); 
        }

        // Triggers the start of the "MapBuilder" script.
        IsReady = true;  
    }

    /// <summary>
    /// Extracts the boundary information from the XML file and generates the corresponding
    /// instance.
    /// </summary>
    /// <param name="xmlNode">XML node</param>
    void SetBounds(XmlNode xmlNode)
    {
        bounds = new OsmBounds(xmlNode);
    }

    /// <summary>
    /// Extracts the nodes information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetNodes(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode n in xmlNodeList)
        {
            OsmNode node = new OsmNode(n);
            nodes[node.ID] = node;
        }
    }

    /// <summary>
    /// Extracts the way information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetWays(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        { 
            OsmWay way = new OsmWay(node);

            if (way.IsRailway == true)
            {
                if (UserPreferences.PublicTransportRailways)
                {
                    ways[way.ID] = way;
                }
            }
            else if (way.IsStreet == true)
            {
                if (UserPreferences.PublicTransportStreets)
                {
                    ways[way.ID] = way;
                }
            }
        }
    }

    /// <summary>
    /// Extract the relation information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetRelations(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        {
            OsmRelation relation = new OsmRelation(node);

            if(relation.Route == true)
            {
                relations.Add(relation);

                if (UserPreferences.PublicTransportRailways || UserPreferences.PublicTransportStreets)
                {
                    TagPublicTransportWays(relation);
                }

                if (UserPreferences.Stations)
                {
                    foreach (ulong NodeID in relation.StoppingNodeIDs)
                    {
                        try
                        {
                            nodes[NodeID].TransportLines.Add(relation.Name);
                            relation.StationNames.Add(nodes[NodeID].StationName);
                        }
                        catch (KeyNotFoundException)
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// If public transport roads and railroads are activated in the options menu,
    /// this function will extract the last data informations from these nodes.
    /// </summary>
    /// <param name="r">XML node</param>
    void TagPublicTransportWays(OsmRelation r)
    {
        foreach (ulong WayID in r.WayIDs)
        {
            try
            {
                switch (r.TransportType)
                {
                    case "subway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("subway");
                        break;
                    case "tram":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("tram");
                        break;
                    case "train":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("train");
                        break;
                    case "railway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("railway");
                        break;
                    case "light_rail":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("light_rail");
                        break;
                    case "bus":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportStreet = true;
                        ways[WayID].TransportTypes.Add("bus");
                        break;
                }
            }
            catch (KeyNotFoundException)
            {
                continue;
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.SceneManagement;


/// <summary>
/// This script extracts the various data nodes from the XML file and generates
/// the respective instances based on the classes from the serialization folder.
/// The objects are then stored in dictionaries and lists to access these data
/// structures from other scripts. As soon as the data has been processed, the
/// variable "IsReady" turns true and the script "MapBuilder" is initiated.
/// </summary>
class MapReader : MonoBehaviour
{
    public ExampleMaps exampleMaps;

    public static OsmBounds bounds;  

    public static Dictionary<ulong, OsmNode> nodes;  

    public static Dictionary<ulong, OsmWay> ways;  

    public static List<OsmRelation> relations; 

    public static bool IsReady { get; private set; }  

    void Start()
    {
        nodes = new Dictionary<ulong, OsmNode>();
        ways = new Dictionary<ulong, OsmWay>();
        relations = new List<OsmRelation>();

        FileLoader.simulator_loaded = true;  

        XmlDocument doc = new XmlDocument();
        try
        {
            // The XML file is being loaded and the data nodes are being extracted using
            // the classes from the serialization folder.
            if(FileLoader.is_example)
            {
                doc.LoadXml(exampleMaps.ExampleMap[FileLoader.example_indx].text);
            }
            else
            {
                doc.Load(FileLoader.ResourceFilePath);
            }
            
            SetBounds(doc.SelectSingleNode("/osm/bounds"));
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetNodes(doc.SelectNodes("/osm/node"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways)
            {
                GetWays(doc.SelectNodes("/osm/way"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetRelations(doc.SelectNodes("/osm/relation"));
            }
        }

        // If the user input path to the XML file is not in XML format, the user is
        // being returned to the Main Menu with an error message.
        catch
        {
            SceneManager.LoadScene(0); 
        }

        // Triggers the start of the "MapBuilder" script.
        IsReady = true;  
    }

    /// <summary>
    /// Extracts the boundary information from the XML file and generates the corresponding
    /// instance.
    /// </summary>
    /// <param name="xmlNode">XML node</param>
    void SetBounds(XmlNode xmlNode)
    {
        bounds = new OsmBounds(xmlNode);
    }

    /// <summary>
    /// Extracts the nodes information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetNodes(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode n in xmlNodeList)
        {
            OsmNode node = new OsmNode(n);
            nodes[node.ID] = node;
        }
    }

    /// <summary>
    /// Extracts the way information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetWays(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        { 
            OsmWay way = new OsmWay(node);

            if (way.IsRailway == true)
            {
                if (UserPreferences.PublicTransportRailways)
                {
                    ways[way.ID] = way;
                }
            }
            else if (way.IsStreet == true)
            {
                if (UserPreferences.PublicTransportStreets)
                {
                    ways[way.ID] = way;
                }
            }
        }
    }

    /// <summary>
    /// Extract the relation information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetRelations(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        {
            OsmRelation relation = new OsmRelation(node);

            if(relation.Route == true)
            {
                relations.Add(relation);

                if (UserPreferences.PublicTransportRailways || UserPreferences.PublicTransportStreets)
                {
                    TagPublicTransportWays(relation);
                }

                if (UserPreferences.Stations)
                {
                    foreach (ulong NodeID in relation.StoppingNodeIDs)
                    {
                        try
                        {
                            nodes[NodeID].TransportLines.Add(relation.Name);
                            relation.StationNames.Add(nodes[NodeID].StationName);
                        }
                        catch (KeyNotFoundException)
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// If public transport roads and railroads are activated in the options menu,
    /// this function will extract the last data informations from these nodes.
    /// </summary>
    /// <param name="r">XML node</param>
    void TagPublicTransportWays(OsmRelation r)
    {
        foreach (ulong WayID in r.WayIDs)
        {
            try
            {
                switch (r.TransportType)
                {
                    case "subway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("subway");
                        break;
                    case "tram":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("tram");
                        break;
                    case "train":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("train");
                        break;
                    case "railway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("railway");
                        break;
                    case "light_rail":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("light_rail");
                        break;
                    case "bus":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportStreet = true;
                        ways[WayID].TransportTypes.Add("bus");
                        break;
                }
            }
            catch (KeyNotFoundException)
            {
                continue;
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.SceneManagement;


/// <summary>
/// This script extracts the various data nodes from the XML file and generates
/// the respective instances based on the classes from the serialization folder.
/// The objects are then stored in dictionaries and lists to access these data
/// structures from other scripts. As soon as the data has been processed, the
/// variable "IsReady" turns true and the script "MapBuilder" is initiated.
/// </summary>
class MapReader : MonoBehaviour
{
    public ExampleMaps exampleMaps;

    public static OsmBounds bounds;  

    public static Dictionary<ulong, OsmNode> nodes;  

    public static Dictionary<ulong, OsmWay> ways;  

    public static List<OsmRelation> relations; 

    public static bool IsReady { get; private set; }  

    void Start()
    {
        nodes = new Dictionary<ulong, OsmNode>();
        ways = new Dictionary<ulong, OsmWay>();
        relations = new List<OsmRelation>();

        FileLoader.simulator_loaded = true;  

        XmlDocument doc = new XmlDocument();
        try
        {
            // The XML file is being loaded and the data nodes are being extracted using
            // the classes from the serialization folder.
            if(FileLoader.is_example)
            {
                doc.LoadXml(exampleMaps.ExampleMap[FileLoader.example_indx].text);
            }
            else
            {
                doc.Load(FileLoader.ResourceFilePath);
            }
            
            SetBounds(doc.SelectSingleNode("/osm/bounds"));
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetNodes(doc.SelectNodes("/osm/node"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways)
            {
                GetWays(doc.SelectNodes("/osm/way"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetRelations(doc.SelectNodes("/osm/relation"));
            }
        }

        // If the user input path to the XML file is not in XML format, the user is
        // being returned to the Main Menu with an error message.
        catch
        {
            SceneManager.LoadScene(0); 
        }

        // Triggers the start of the "MapBuilder" script.
        IsReady = true;  
    }

    /// <summary>
    /// Extracts the boundary information from the XML file and generates the corresponding
    /// instance.
    /// </summary>
    /// <param name="xmlNode">XML node</param>
    void SetBounds(XmlNode xmlNode)
    {
        bounds = new OsmBounds(xmlNode);
    }

    /// <summary>
    /// Extracts the nodes information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetNodes(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode n in xmlNodeList)
        {
            OsmNode node = new OsmNode(n);
            nodes[node.ID] = node;
        }
    }

    /// <summary>
    /// Extracts the way information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetWays(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        { 
            OsmWay way = new OsmWay(node);

            if (way.IsRailway == true)
            {
                if (UserPreferences.PublicTransportRailways)
                {
                    ways[way.ID] = way;
                }
            }
            else if (way.IsStreet == true)
            {
                if (UserPreferences.PublicTransportStreets)
                {
                    ways[way.ID] = way;
                }
            }
        }
    }

    /// <summary>
    /// Extract the relation information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetRelations(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        {
            OsmRelation relation = new OsmRelation(node);

            if(relation.Route == true)
            {
                relations.Add(relation);

                if (UserPreferences.PublicTransportRailways || UserPreferences.PublicTransportStreets)
                {
                    TagPublicTransportWays(relation);
                }

                if (UserPreferences.Stations)
                {
                    foreach (ulong NodeID in relation.StoppingNodeIDs)
                    {
                        try
                        {
                            nodes[NodeID].TransportLines.Add(relation.Name);
                            relation.StationNames.Add(nodes[NodeID].StationName);
                        }
                        catch (KeyNotFoundException)
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// If public transport roads and railroads are activated in the options menu,
    /// this function will extract the last data informations from these nodes.
    /// </summary>
    /// <param name="r">XML node</param>
    void TagPublicTransportWays(OsmRelation r)
    {
        foreach (ulong WayID in r.WayIDs)
        {
            try
            {
                switch (r.TransportType)
                {
                    case "subway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("subway");
                        break;
                    case "tram":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("tram");
                        break;
                    case "train":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("train");
                        break;
                    case "railway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("railway");
                        break;
                    case "light_rail":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("light_rail");
                        break;
                    case "bus":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportStreet = true;
                        ways[WayID].TransportTypes.Add("bus");
                        break;
                }
            }
            catch (KeyNotFoundException)
            {
                continue;
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.SceneManagement;


/// <summary>
/// This script extracts the various data nodes from the XML file and generates
/// the respective instances based on the classes from the serialization folder.
/// The objects are then stored in dictionaries and lists to access these data
/// structures from other scripts. As soon as the data has been processed, the
/// variable "IsReady" turns true and the script "MapBuilder" is initiated.
/// </summary>
class MapReader : MonoBehaviour
{
    public ExampleMaps exampleMaps;

    public static OsmBounds bounds;  

    public static Dictionary<ulong, OsmNode> nodes;  

    public static Dictionary<ulong, OsmWay> ways;  

    public static List<OsmRelation> relations; 

    public static bool IsReady { get; private set; }  

    void Start()
    {
        nodes = new Dictionary<ulong, OsmNode>();
        ways = new Dictionary<ulong, OsmWay>();
        relations = new List<OsmRelation>();

        FileLoader.simulator_loaded = true;  

        XmlDocument doc = new XmlDocument();
        try
        {
            // The XML file is being loaded and the data nodes are being extracted using
            // the classes from the serialization folder.
            if(FileLoader.is_example)
            {
                doc.LoadXml(exampleMaps.ExampleMap[FileLoader.example_indx].text);
            }
            else
            {
                doc.Load(FileLoader.ResourceFilePath);
            }
            
            SetBounds(doc.SelectSingleNode("/osm/bounds"));
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetNodes(doc.SelectNodes("/osm/node"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways)
            {
                GetWays(doc.SelectNodes("/osm/way"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetRelations(doc.SelectNodes("/osm/relation"));
            }
        }

        // If the user input path to the XML file is not in XML format, the user is
        // being returned to the Main Menu with an error message.
        catch
        {
            SceneManager.LoadScene(0); 
        }

        // Triggers the start of the "MapBuilder" script.
        IsReady = true;  
    }

    /// <summary>
    /// Extracts the boundary information from the XML file and generates the corresponding
    /// instance.
    /// </summary>
    /// <param name="xmlNode">XML node</param>
    void SetBounds(XmlNode xmlNode)
    {
        bounds = new OsmBounds(xmlNode);
    }

    /// <summary>
    /// Extracts the nodes information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetNodes(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode n in xmlNodeList)
        {
            OsmNode node = new OsmNode(n);
            nodes[node.ID] = node;
        }
    }

    /// <summary>
    /// Extracts the way information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetWays(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        { 
            OsmWay way = new OsmWay(node);

            if (way.IsRailway == true)
            {
                if (UserPreferences.PublicTransportRailways)
                {
                    ways[way.ID] = way;
                }
            }
            else if (way.IsStreet == true)
            {
                if (UserPreferences.PublicTransportStreets)
                {
                    ways[way.ID] = way;
                }
            }
        }
    }

    /// <summary>
    /// Extract the relation information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetRelations(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        {
            OsmRelation relation = new OsmRelation(node);

            if(relation.Route == true)
            {
                relations.Add(relation);

                if (UserPreferences.PublicTransportRailways || UserPreferences.PublicTransportStreets)
                {
                    TagPublicTransportWays(relation);
                }

                if (UserPreferences.Stations)
                {
                    foreach (ulong NodeID in relation.StoppingNodeIDs)
                    {
                        try
                        {
                            nodes[NodeID].TransportLines.Add(relation.Name);
                            relation.StationNames.Add(nodes[NodeID].StationName);
                        }
                        catch (KeyNotFoundException)
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// If public transport roads and railroads are activated in the options menu,
    /// this function will extract the last data informations from these nodes.
    /// </summary>
    /// <param name="r">XML node</param>
    void TagPublicTransportWays(OsmRelation r)
    {
        foreach (ulong WayID in r.WayIDs)
        {
            try
            {
                switch (r.TransportType)
                {
                    case "subway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("subway");
                        break;
                    case "tram":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("tram");
                        break;
                    case "train":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("train");
                        break;
                    case "railway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("railway");
                        break;
                    case "light_rail":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("light_rail");
                        break;
                    case "bus":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportStreet = true;
                        ways[WayID].TransportTypes.Add("bus");
                        break;
                }
            }
            catch (KeyNotFoundException)
            {
                continue;
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.SceneManagement;


/// <summary>
/// This script extracts the various data nodes from the XML file and generates
/// the respective instances based on the classes from the serialization folder.
/// The objects are then stored in dictionaries and lists to access these data
/// structures from other scripts. As soon as the data has been processed, the
/// variable "IsReady" turns true and the script "MapBuilder" is initiated.
/// </summary>
class MapReader : MonoBehaviour
{
    public ExampleMaps exampleMaps;

    public static OsmBounds bounds;  

    public static Dictionary<ulong, OsmNode> nodes;  

    public static Dictionary<ulong, OsmWay> ways;  

    public static List<OsmRelation> relations; 

    public static bool IsReady { get; private set; }  

    void Start()
    {
        nodes = new Dictionary<ulong, OsmNode>();
        ways = new Dictionary<ulong, OsmWay>();
        relations = new List<OsmRelation>();

        FileLoader.simulator_loaded = true;  

        XmlDocument doc = new XmlDocument();
        try
        {
            // The XML file is being loaded and the data nodes are being extracted using
            // the classes from the serialization folder.
            if(FileLoader.is_example)
            {
                doc.LoadXml(exampleMaps.ExampleMap[FileLoader.example_indx].text);
            }
            else
            {
                doc.Load(FileLoader.ResourceFilePath);
            }
            
            SetBounds(doc.SelectSingleNode("/osm/bounds"));
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetNodes(doc.SelectNodes("/osm/node"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways)
            {
                GetWays(doc.SelectNodes("/osm/way"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetRelations(doc.SelectNodes("/osm/relation"));
            }
        }

        // If the user input path to the XML file is not in XML format, the user is
        // being returned to the Main Menu with an error message.
        catch
        {
            SceneManager.LoadScene(0); 
        }

        // Triggers the start of the "MapBuilder" script.
        IsReady = true;  
    }

    /// <summary>
    /// Extracts the boundary information from the XML file and generates the corresponding
    /// instance.
    /// </summary>
    /// <param name="xmlNode">XML node</param>
    void SetBounds(XmlNode xmlNode)
    {
        bounds = new OsmBounds(xmlNode);
    }

    /// <summary>
    /// Extracts the nodes information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetNodes(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode n in xmlNodeList)
        {
            OsmNode node = new OsmNode(n);
            nodes[node.ID] = node;
        }
    }

    /// <summary>
    /// Extracts the way information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetWays(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        { 
            OsmWay way = new OsmWay(node);

            if (way.IsRailway == true)
            {
                if (UserPreferences.PublicTransportRailways)
                {
                    ways[way.ID] = way;
                }
            }
            else if (way.IsStreet == true)
            {
                if (UserPreferences.PublicTransportStreets)
                {
                    ways[way.ID] = way;
                }
            }
        }
    }

    /// <summary>
    /// Extract the relation information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetRelations(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        {
            OsmRelation relation = new OsmRelation(node);

            if(relation.Route == true)
            {
                relations.Add(relation);

                if (UserPreferences.PublicTransportRailways || UserPreferences.PublicTransportStreets)
                {
                    TagPublicTransportWays(relation);
                }

                if (UserPreferences.Stations)
                {
                    foreach (ulong NodeID in relation.StoppingNodeIDs)
                    {
                        try
                        {
                            nodes[NodeID].TransportLines.Add(relation.Name);
                            relation.StationNames.Add(nodes[NodeID].StationName);
                        }
                        catch (KeyNotFoundException)
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// If public transport roads and railroads are activated in the options menu,
    /// this function will extract the last data informations from these nodes.
    /// </summary>
    /// <param name="r">XML node</param>
    void TagPublicTransportWays(OsmRelation r)
    {
        foreach (ulong WayID in r.WayIDs)
        {
            try
            {
                switch (r.TransportType)
                {
                    case "subway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("subway");
                        break;
                    case "tram":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("tram");
                        break;
                    case "train":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("train");
                        break;
                    case "railway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("railway");
                        break;
                    case "light_rail":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("light_rail");
                        break;
                    case "bus":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportStreet = true;
                        ways[WayID].TransportTypes.Add("bus");
                        break;
                }
            }
            catch (KeyNotFoundException)
            {
                continue;
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.SceneManagement;


/// <summary>
/// This script extracts the various data nodes from the XML file and generates
/// the respective instances based on the classes from the serialization folder.
/// The objects are then stored in dictionaries and lists to access these data
/// structures from other scripts. As soon as the data has been processed, the
/// variable "IsReady" turns true and the script "MapBuilder" is initiated.
/// </summary>
class MapReader : MonoBehaviour
{
    public ExampleMaps exampleMaps;

    public static OsmBounds bounds;  

    public static Dictionary<ulong, OsmNode> nodes;  

    public static Dictionary<ulong, OsmWay> ways;  

    public static List<OsmRelation> relations; 

    public static bool IsReady { get; private set; }  

    void Start()
    {
        nodes = new Dictionary<ulong, OsmNode>();
        ways = new Dictionary<ulong, OsmWay>();
        relations = new List<OsmRelation>();

        FileLoader.simulator_loaded = true;  

        XmlDocument doc = new XmlDocument();
        try
        {
            // The XML file is being loaded and the data nodes are being extracted using
            // the classes from the serialization folder.
            if(FileLoader.is_example)
            {
                doc.LoadXml(exampleMaps.ExampleMap[FileLoader.example_indx].text);
            }
            else
            {
                doc.Load(FileLoader.ResourceFilePath);
            }
            
            SetBounds(doc.SelectSingleNode("/osm/bounds"));
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetNodes(doc.SelectNodes("/osm/node"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways)
            {
                GetWays(doc.SelectNodes("/osm/way"));
            }
            if(UserPreferences.PublicTransportStreets || UserPreferences.PublicTransportRailways || UserPreferences.Stations)
            {
                GetRelations(doc.SelectNodes("/osm/relation"));
            }
        }

        // If the user input path to the XML file is not in XML format, the user is
        // being returned to the Main Menu with an error message.
        catch
        {
            SceneManager.LoadScene(0); 
        }

        // Triggers the start of the "MapBuilder" script.
        IsReady = true;  
    }

    /// <summary>
    /// Extracts the boundary information from the XML file and generates the corresponding
    /// instance.
    /// </summary>
    /// <param name="xmlNode">XML node</param>
    void SetBounds(XmlNode xmlNode)
    {
        bounds = new OsmBounds(xmlNode);
    }

    /// <summary>
    /// Extracts the nodes information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetNodes(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode n in xmlNodeList)
        {
            OsmNode node = new OsmNode(n);
            nodes[node.ID] = node;
        }
    }

    /// <summary>
    /// Extracts the way information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetWays(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        { 
            OsmWay way = new OsmWay(node);

            if (way.IsRailway == true)
            {
                if (UserPreferences.PublicTransportRailways)
                {
                    ways[way.ID] = way;
                }
            }
            else if (way.IsStreet == true)
            {
                if (UserPreferences.PublicTransportStreets)
                {
                    ways[way.ID] = way;
                }
            }
        }
    }

    /// <summary>
    /// Extract the relation information from the XML file and generates the corresponding
    /// instances.
    /// </summary>
    /// <param name="xmlNodeList">XML node</param>
    void GetRelations(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        {
            OsmRelation relation = new OsmRelation(node);

            if(relation.Route == true)
            {
                relations.Add(relation);

                if (UserPreferences.PublicTransportRailways || UserPreferences.PublicTransportStreets)
                {
                    TagPublicTransportWays(relation);
                }

                if (UserPreferences.Stations)
                {
                    foreach (ulong NodeID in relation.StoppingNodeIDs)
                    {
                        try
                        {
                            nodes[NodeID].TransportLines.Add(relation.Name);
                            relation.StationNames.Add(nodes[NodeID].StationName);
                        }
                        catch (KeyNotFoundException)
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// If public transport roads and railroads are activated in the options menu,
    /// this function will extract the last data informations from these nodes.
    /// </summary>
    /// <param name="r">XML node</param>
    void TagPublicTransportWays(OsmRelation r)
    {
        foreach (ulong WayID in r.WayIDs)
        {
            try
            {
                switch (r.TransportType)
                {
                    case "subway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("subway");
                        break;
                    case "tram":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("tram");
                        break;
                    case "train":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("train");
                        break;
                    case "railway":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("railway");
                        break;
                    case "light_rail":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportRailway = true;
                        ways[WayID].TransportTypes.Add("light_rail");
                        break;
                    case "bus":
                        ways[WayID].TransportLines.Add(r.Name);
                        ways[WayID].PublicTransportStreet = true;
                        ways[WayID].TransportTypes.Add("bus");
                        break;
                }
            }
            catch (KeyNotFoundException)
            {
                continue;
            }
        }
    }
}
