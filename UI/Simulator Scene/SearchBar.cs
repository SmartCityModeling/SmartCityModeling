using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Used for the search functionality within the simulated scene (controlls the search bar).
/// </summary>
public class SearchBar : MonoBehaviour
{
    public GameObject TextBar;
    public GameObject DropDown;

    public List<string> FoundStations; 
    public List<string> AllStation; 

    /// <summary>
    /// Stores all generated station names in a list.
    /// </summary>
    void Start()
    {
        if(LanguageSettings.Language == "English")
        {
            transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Search for stations..";
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Suche nach Stationen..";
        }

        FoundStations = new List<string>();
        AllStation = new List<string>();

        List<GameObject> allObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(allObjects);

        for(int i = 0; i < allObjects.Count; i++)
        {
            if(allObjects[i].name == "Station(Clone)")
            {
                if (AllStation.Contains(allObjects[i].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text))
                {
                    continue;
                }
                AllStation.Add(allObjects[i].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text);
            }
        }
    }

    /// <summary>
    /// Using the user input, this function searches for all stations which include the user input. These
    /// are then returned in a list as "Search results".
    /// </summary>
    /// <param name="searchInput">User input from the search bar</param>
    public void SearchForStation(string searchInput)
    {
        if(searchInput == "")
        {
            return;
        }

        DropDown.SetActive(true);
        FoundStations.Clear();
        if(LanguageSettings.Language == "English")
        {
            FoundStations.Add("Search results:");
        }
        else
        {
            FoundStations.Add("Suchergebnisse:");
        }
        
        for(int i = 0; i < AllStation.Count; i++)
        {
            if (AllStation[i].ToLower().Contains(searchInput.ToLower()))
            {
                FoundStations.Add(AllStation[i]);
            }
        }

        DropDown.GetComponent<Dropdown>().options.Clear();
        DropDown.GetComponent<Dropdown>().AddOptions(FoundStations);
    }
}
