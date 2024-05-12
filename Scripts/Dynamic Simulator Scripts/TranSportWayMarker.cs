using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TranSportWayMarker : MonoBehaviour
{
    public static List<int> SelectedWays; // Here we store the current changes to the way objects.
    static List<int> SelectedStations; // Here we store the current changes to the station objects.
    public static Material PreviousMaterial; // Here we store the previous object color.

    public static List<Vector3> StationOrder; 
    
    static bool SelectionStarted = false; 

    private void Start()
    {

    }

    /// <summary>
    /// This function is called as soon as the user selects an option from the station dropdown. This undoes the
    /// previous changes, and marks the currently selected public transport line.
    /// </summary>
    /// <param name="selection">The dropdown selection of the user from the station UI</param>
    public static void SelectPublicTransportLine(string selection)
    {
        
    }
}
