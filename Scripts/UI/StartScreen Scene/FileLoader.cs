using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// This class is controlling the Main Menu of the simulation and the whole 
/// "StartScreen" Scene. It also processes the user input of the path to the data 
/// source (the OSM XML file) and switches to the loading screen.
/// </summary>
public class FileLoader : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject MainMenu;
    public GameObject SimulationSource;
    public GameObject OptionsScreen;
    public GameObject AboutScreen;
    public GameObject CreditsScreen;
    public GameObject QuitScreen;
    public GameObject LoadingScreen;
    public GameObject FilePath;
    public GameObject Instructions1;
    public GameObject Instructions2;
    public GameObject Instructions3;
    public GameObject ErrorMessage;
    public GameObject ErrorMessage_2;
    public GameObject ErrorMessageOptions;
    public GameObject ErrorMessageOptions2;
    

    // Here will be the path to the OSM XML file stored.
    public static string ResourceFilePath;

    bool user_input = false;

    public static bool simulator_loaded = false;

    public static bool is_example = false;
    public static int example_indx = 0;

    private void Update()
    {
        // Checks if the user specified path is valid and loads the loading screen.
        if (user_input == true)
        {
            StartScreen.SetActive(false);
            LoadingScreen.SetActive(true);
            user_input = false;
            if(is_example)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                if (File.Exists(ResourceFilePath) && ResourceFilePath.EndsWith(".txt"))
                {
                    SceneManager.LoadScene(1);
                }
                else
                {
                    LoadingScreen.SetActive(false);
                    StartScreen.SetActive(true);
                    ErrorMessage.SetActive(true);
                }
            }
            
        }

        if (simulator_loaded)
        {
            // If the user specified a correct path to an .txt file which is not
            // in the XML format, the loading screen will return to the StartScreen
            // and this line of code will be executed.
            ErrorMessage_2.SetActive(true);
            simulator_loaded = false;
        }
    }


    /// <summary>
    /// Controls the "Launch Simulator" button in the Main Menu.
    /// </summary>
    public void StartSimulation()
    {
        MainMenu.SetActive(false);
        SimulationSource.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "START" button in the user input Menu.
    /// </summary>
    public void ConfirmPath()
    {
        ResourceFilePath = FilePath.GetComponent<Text>().text;
        if (ResourceFilePath != "")
        {
            user_input = true;
            is_example = false;
        }
    }

    public void ConfirmPath_LoadExample(int index)
    {
        user_input = true;
        is_example = true;
        example_indx = index;
    }

    public void ConnectUrl()
    {
        Application.OpenURL("https://www.openstreetmap.org/export#map=11/31.2126/121.4793");
    }


    /// <summary>
    /// Controls the "Help" button in the user input Menu.
    /// </summary>
    public void Help()
    {
        SimulationSource.SetActive(false);
        Instructions1.SetActive(true);
        ErrorMessage.SetActive(false);
    }

    /// <summary>
    /// Controls the "Start Simulation" button in the Instructions Menu.
    /// </summary>
    public void ReturnToSimulationStart()
    {
        Instructions1.SetActive(false);
        Instructions2.SetActive(false);
        Instructions3.SetActive(false);
        SimulationSource.SetActive(true);
    }

    /// <summary>
    /// Controls the "Next Page" button in the Instructions Menu.
    /// </summary>
    public void NextPage1()
    {
        Instructions1.SetActive(false);
        Instructions2.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Instructions Menu.
    /// </summary>
    public void ReturnPage()
    {
        Instructions1.SetActive(true);
        Instructions2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Next Page" button in the Instructions Menu.
    /// </summary>
    public void NexPage2()
    {
        Instructions2.SetActive(false);
        Instructions3.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Instructions Menu.
    /// </summary>
    public void Return2()
    {
        Instructions3.SetActive(false);
        Instructions2.SetActive(true);
    }

    /// <summary>
    /// Controls the "Options" button in the Main Menu.
    /// </summary>
    public void Options()
    {
        MainMenu.SetActive(false);
        OptionsScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Return" button in the Options Menu.
    /// </summary>
    public void ReturnfromOptions()
    {
        if (!UserPreferences.Buildings && !UserPreferences.Stations && !UserPreferences.AllStreets && !UserPreferences.PublicTransportStreets && !UserPreferences.PublicTransportRailways)
        {
            ErrorMessageOptions2.SetActive(false);
            ErrorMessageOptions.SetActive(true);
        }
        else if(UserPreferences.Stations && !UserPreferences.Subways && !UserPreferences.Trams && !UserPreferences.Trains && !UserPreferences.Railways && !UserPreferences.LightRails && !UserPreferences.Busses)
        {
            ErrorMessageOptions.SetActive(false);
            ErrorMessageOptions2.SetActive(true);
        }
        else
        {
            ErrorMessageOptions.SetActive(false);
            ErrorMessageOptions2.SetActive(false);
            OptionsScreen.SetActive(false);
            MainMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Controls the "About" button in the Main Menu.
    /// </summary>
    public void About()
    {
        MainMenu.SetActive(false);
        AboutScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Credits" button in the Main Menu.
    /// </summary>
    public void Credits()
    {
        MainMenu.SetActive(false);
        CreditsScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Exit Simulation" button in the Main Menu.
    /// </summary>
    public void Close()
    {
        MainMenu.SetActive(false);
        QuitScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Yes" button in the Exit Menu.
    /// </summary>
    public void Quit_yes()
    {
        Application.Quit();
    }

    /// <summary>
    /// Controls the "No" button in the Exit Menu.
    /// </summary>
    public void Quit_no()
    {
        QuitScreen.SetActive(false);
        MainMenu.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Credits, About, Options and 
    /// User Input Menu.
    /// </summary>
    public void Return()
    {
        CreditsScreen.SetActive(false);
        AboutScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        SimulationSource.SetActive(false);
        MainMenu.SetActive(true);
        ErrorMessage.SetActive(false);
    }
}


using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// This class is controlling the Main Menu of the simulation and the whole 
/// "StartScreen" Scene. It also processes the user input of the path to the data 
/// source (the OSM XML file) and switches to the loading screen.
/// </summary>
public class FileLoader : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject MainMenu;
    public GameObject SimulationSource;
    public GameObject OptionsScreen;
    public GameObject AboutScreen;
    public GameObject CreditsScreen;
    public GameObject QuitScreen;
    public GameObject LoadingScreen;
    public GameObject FilePath;
    public GameObject Instructions1;
    public GameObject Instructions2;
    public GameObject Instructions3;
    public GameObject ErrorMessage;
    public GameObject ErrorMessage_2;
    public GameObject ErrorMessageOptions;
    public GameObject ErrorMessageOptions2;
    

    // Here will be the path to the OSM XML file stored.
    public static string ResourceFilePath;

    bool user_input = false;

    public static bool simulator_loaded = false;

    public static bool is_example = false;
    public static int example_indx = 0;

    private void Update()
    {
        // Checks if the user specified path is valid and loads the loading screen.
        if (user_input == true)
        {
            StartScreen.SetActive(false);
            LoadingScreen.SetActive(true);
            user_input = false;
            if(is_example)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                if (File.Exists(ResourceFilePath) && ResourceFilePath.EndsWith(".txt"))
                {
                    SceneManager.LoadScene(1);
                }
                else
                {
                    LoadingScreen.SetActive(false);
                    StartScreen.SetActive(true);
                    ErrorMessage.SetActive(true);
                }
            }
            
        }

        if (simulator_loaded)
        {
            // If the user specified a correct path to an .txt file which is not
            // in the XML format, the loading screen will return to the StartScreen
            // and this line of code will be executed.
            ErrorMessage_2.SetActive(true);
            simulator_loaded = false;
        }
    }


    /// <summary>
    /// Controls the "Launch Simulator" button in the Main Menu.
    /// </summary>
    public void StartSimulation()
    {
        MainMenu.SetActive(false);
        SimulationSource.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "START" button in the user input Menu.
    /// </summary>
    public void ConfirmPath()
    {
        ResourceFilePath = FilePath.GetComponent<Text>().text;
        if (ResourceFilePath != "")
        {
            user_input = true;
            is_example = false;
        }
    }

    public void ConfirmPath_LoadExample(int index)
    {
        user_input = true;
        is_example = true;
        example_indx = index;
    }

    public void ConnectUrl()
    {
        Application.OpenURL("https://www.openstreetmap.org/export#map=11/31.2126/121.4793");
    }


    /// <summary>
    /// Controls the "Help" button in the user input Menu.
    /// </summary>
    public void Help()
    {
        SimulationSource.SetActive(false);
        Instructions1.SetActive(true);
        ErrorMessage.SetActive(false);
    }

    /// <summary>
    /// Controls the "Start Simulation" button in the Instructions Menu.
    /// </summary>
    public void ReturnToSimulationStart()
    {
        Instructions1.SetActive(false);
        Instructions2.SetActive(false);
        Instructions3.SetActive(false);
        SimulationSource.SetActive(true);
    }

    /// <summary>
    /// Controls the "Next Page" button in the Instructions Menu.
    /// </summary>
    public void NextPage1()
    {
        Instructions1.SetActive(false);
        Instructions2.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Instructions Menu.
    /// </summary>
    public void ReturnPage()
    {
        Instructions1.SetActive(true);
        Instructions2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Next Page" button in the Instructions Menu.
    /// </summary>
    public void NexPage2()
    {
        Instructions2.SetActive(false);
        Instructions3.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Instructions Menu.
    /// </summary>
    public void Return2()
    {
        Instructions3.SetActive(false);
        Instructions2.SetActive(true);
    }

    /// <summary>
    /// Controls the "Options" button in the Main Menu.
    /// </summary>
    public void Options()
    {
        MainMenu.SetActive(false);
        OptionsScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Return" button in the Options Menu.
    /// </summary>
    public void ReturnfromOptions()
    {
        if (!UserPreferences.Buildings && !UserPreferences.Stations && !UserPreferences.AllStreets && !UserPreferences.PublicTransportStreets && !UserPreferences.PublicTransportRailways)
        {
            ErrorMessageOptions2.SetActive(false);
            ErrorMessageOptions.SetActive(true);
        }
        else if(UserPreferences.Stations && !UserPreferences.Subways && !UserPreferences.Trams && !UserPreferences.Trains && !UserPreferences.Railways && !UserPreferences.LightRails && !UserPreferences.Busses)
        {
            ErrorMessageOptions.SetActive(false);
            ErrorMessageOptions2.SetActive(true);
        }
        else
        {
            ErrorMessageOptions.SetActive(false);
            ErrorMessageOptions2.SetActive(false);
            OptionsScreen.SetActive(false);
            MainMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Controls the "About" button in the Main Menu.
    /// </summary>
    public void About()
    {
        MainMenu.SetActive(false);
        AboutScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Credits" button in the Main Menu.
    /// </summary>
    public void Credits()
    {
        MainMenu.SetActive(false);
        CreditsScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Exit Simulation" button in the Main Menu.
    /// </summary>
    public void Close()
    {
        MainMenu.SetActive(false);
        QuitScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Yes" button in the Exit Menu.
    /// </summary>
    public void Quit_yes()
    {
        Application.Quit();
    }

    /// <summary>
    /// Controls the "No" button in the Exit Menu.
    /// </summary>
    public void Quit_no()
    {
        QuitScreen.SetActive(false);
        MainMenu.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Credits, About, Options and 
    /// User Input Menu.
    /// </summary>
    public void Return()
    {
        CreditsScreen.SetActive(false);
        AboutScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        SimulationSource.SetActive(false);
        MainMenu.SetActive(true);
        ErrorMessage.SetActive(false);
    }
}



using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// This class is controlling the Main Menu of the simulation and the whole 
/// "StartScreen" Scene. It also processes the user input of the path to the data 
/// source (the OSM XML file) and switches to the loading screen.
/// </summary>
public class FileLoader : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject MainMenu;
    public GameObject SimulationSource;
    public GameObject OptionsScreen;
    public GameObject AboutScreen;
    public GameObject CreditsScreen;
    public GameObject QuitScreen;
    public GameObject LoadingScreen;
    public GameObject FilePath;
    public GameObject Instructions1;
    public GameObject Instructions2;
    public GameObject Instructions3;
    public GameObject ErrorMessage;
    public GameObject ErrorMessage_2;
    public GameObject ErrorMessageOptions;
    public GameObject ErrorMessageOptions2;
    

    // Here will be the path to the OSM XML file stored.
    public static string ResourceFilePath;

    bool user_input = false;

    public static bool simulator_loaded = false;

    public static bool is_example = false;
    public static int example_indx = 0;

    private void Update()
    {
        // Checks if the user specified path is valid and loads the loading screen.
        if (user_input == true)
        {
            StartScreen.SetActive(false);
            LoadingScreen.SetActive(true);
            user_input = false;
            if(is_example)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                if (File.Exists(ResourceFilePath) && ResourceFilePath.EndsWith(".txt"))
                {
                    SceneManager.LoadScene(1);
                }
                else
                {
                    LoadingScreen.SetActive(false);
                    StartScreen.SetActive(true);
                    ErrorMessage.SetActive(true);
                }
            }
            
        }

        if (simulator_loaded)
        {
            // If the user specified a correct path to an .txt file which is not
            // in the XML format, the loading screen will return to the StartScreen
            // and this line of code will be executed.
            ErrorMessage_2.SetActive(true);
            simulator_loaded = false;
        }
    }


    /// <summary>
    /// Controls the "Launch Simulator" button in the Main Menu.
    /// </summary>
    public void StartSimulation()
    {
        MainMenu.SetActive(false);
        SimulationSource.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "START" button in the user input Menu.
    /// </summary>
    public void ConfirmPath()
    {
        ResourceFilePath = FilePath.GetComponent<Text>().text;
        if (ResourceFilePath != "")
        {
            user_input = true;
            is_example = false;
        }
    }

    public void ConfirmPath_LoadExample(int index)
    {
        user_input = true;
        is_example = true;
        example_indx = index;
    }

    public void ConnectUrl()
    {
        Application.OpenURL("https://www.openstreetmap.org/export#map=11/31.2126/121.4793");
    }


    /// <summary>
    /// Controls the "Help" button in the user input Menu.
    /// </summary>
    public void Help()
    {
        SimulationSource.SetActive(false);
        Instructions1.SetActive(true);
        ErrorMessage.SetActive(false);
    }

    /// <summary>
    /// Controls the "Start Simulation" button in the Instructions Menu.
    /// </summary>
    public void ReturnToSimulationStart()
    {
        Instructions1.SetActive(false);
        Instructions2.SetActive(false);
        Instructions3.SetActive(false);
        SimulationSource.SetActive(true);
    }

    /// <summary>
    /// Controls the "Next Page" button in the Instructions Menu.
    /// </summary>
    public void NextPage1()
    {
        Instructions1.SetActive(false);
        Instructions2.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Instructions Menu.
    /// </summary>
    public void ReturnPage()
    {
        Instructions1.SetActive(true);
        Instructions2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Next Page" button in the Instructions Menu.
    /// </summary>
    public void NexPage2()
    {
        Instructions2.SetActive(false);
        Instructions3.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Instructions Menu.
    /// </summary>
    public void Return2()
    {
        Instructions3.SetActive(false);
        Instructions2.SetActive(true);
    }

    /// <summary>
    /// Controls the "Options" button in the Main Menu.
    /// </summary>
    public void Options()
    {
        MainMenu.SetActive(false);
        OptionsScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Return" button in the Options Menu.
    /// </summary>
    public void ReturnfromOptions()
    {
        if (!UserPreferences.Buildings && !UserPreferences.Stations && !UserPreferences.AllStreets && !UserPreferences.PublicTransportStreets && !UserPreferences.PublicTransportRailways)
        {
            ErrorMessageOptions2.SetActive(false);
            ErrorMessageOptions.SetActive(true);
        }
        else if(UserPreferences.Stations && !UserPreferences.Subways && !UserPreferences.Trams && !UserPreferences.Trains && !UserPreferences.Railways && !UserPreferences.LightRails && !UserPreferences.Busses)
        {
            ErrorMessageOptions.SetActive(false);
            ErrorMessageOptions2.SetActive(true);
        }
        else
        {
            ErrorMessageOptions.SetActive(false);
            ErrorMessageOptions2.SetActive(false);
            OptionsScreen.SetActive(false);
            MainMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Controls the "About" button in the Main Menu.
    /// </summary>
    public void About()
    {
        MainMenu.SetActive(false);
        AboutScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Credits" button in the Main Menu.
    /// </summary>
    public void Credits()
    {
        MainMenu.SetActive(false);
        CreditsScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Exit Simulation" button in the Main Menu.
    /// </summary>
    public void Close()
    {
        MainMenu.SetActive(false);
        QuitScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Yes" button in the Exit Menu.
    /// </summary>
    public void Quit_yes()
    {
        Application.Quit();
    }

    /// <summary>
    /// Controls the "No" button in the Exit Menu.
    /// </summary>
    public void Quit_no()
    {
        QuitScreen.SetActive(false);
        MainMenu.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Credits, About, Options and 
    /// User Input Menu.
    /// </summary>
    public void Return()
    {
        CreditsScreen.SetActive(false);
        AboutScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        SimulationSource.SetActive(false);
        MainMenu.SetActive(true);
        ErrorMessage.SetActive(false);
    }
}




using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// This class is controlling the Main Menu of the simulation and the whole 
/// "StartScreen" Scene. It also processes the user input of the path to the data 
/// source (the OSM XML file) and switches to the loading screen.
/// </summary>
public class FileLoader : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject MainMenu;
    public GameObject SimulationSource;
    public GameObject OptionsScreen;
    public GameObject AboutScreen;
    public GameObject CreditsScreen;
    public GameObject QuitScreen;
    public GameObject LoadingScreen;
    public GameObject FilePath;
    public GameObject Instructions1;
    public GameObject Instructions2;
    public GameObject Instructions3;
    public GameObject ErrorMessage;
    public GameObject ErrorMessage_2;
    public GameObject ErrorMessageOptions;
    public GameObject ErrorMessageOptions2;
    

    // Here will be the path to the OSM XML file stored.
    public static string ResourceFilePath;

    bool user_input = false;

    public static bool simulator_loaded = false;

    public static bool is_example = false;
    public static int example_indx = 0;

    private void Update()
    {
        // Checks if the user specified path is valid and loads the loading screen.
        if (user_input == true)
        {
            StartScreen.SetActive(false);
            LoadingScreen.SetActive(true);
            user_input = false;
            if(is_example)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                if (File.Exists(ResourceFilePath) && ResourceFilePath.EndsWith(".txt"))
                {
                    SceneManager.LoadScene(1);
                }
                else
                {
                    LoadingScreen.SetActive(false);
                    StartScreen.SetActive(true);
                    ErrorMessage.SetActive(true);
                }
            }
            
        }

        if (simulator_loaded)
        {
            // If the user specified a correct path to an .txt file which is not
            // in the XML format, the loading screen will return to the StartScreen
            // and this line of code will be executed.
            ErrorMessage_2.SetActive(true);
            simulator_loaded = false;
        }
    }


    /// <summary>
    /// Controls the "Launch Simulator" button in the Main Menu.
    /// </summary>
    public void StartSimulation()
    {
        MainMenu.SetActive(false);
        SimulationSource.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "START" button in the user input Menu.
    /// </summary>
    public void ConfirmPath()
    {
        ResourceFilePath = FilePath.GetComponent<Text>().text;
        if (ResourceFilePath != "")
        {
            user_input = true;
            is_example = false;
        }
    }

    public void ConfirmPath_LoadExample(int index)
    {
        user_input = true;
        is_example = true;
        example_indx = index;
    }

    public void ConnectUrl()
    {
        Application.OpenURL("https://www.openstreetmap.org/export#map=11/31.2126/121.4793");
    }


    /// <summary>
    /// Controls the "Help" button in the user input Menu.
    /// </summary>
    public void Help()
    {
        SimulationSource.SetActive(false);
        Instructions1.SetActive(true);
        ErrorMessage.SetActive(false);
    }

    /// <summary>
    /// Controls the "Start Simulation" button in the Instructions Menu.
    /// </summary>
    public void ReturnToSimulationStart()
    {
        Instructions1.SetActive(false);
        Instructions2.SetActive(false);
        Instructions3.SetActive(false);
        SimulationSource.SetActive(true);
    }

    /// <summary>
    /// Controls the "Next Page" button in the Instructions Menu.
    /// </summary>
    public void NextPage1()
    {
        Instructions1.SetActive(false);
        Instructions2.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Instructions Menu.
    /// </summary>
    public void ReturnPage()
    {
        Instructions1.SetActive(true);
        Instructions2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Next Page" button in the Instructions Menu.
    /// </summary>
    public void NexPage2()
    {
        Instructions2.SetActive(false);
        Instructions3.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Instructions Menu.
    /// </summary>
    public void Return2()
    {
        Instructions3.SetActive(false);
        Instructions2.SetActive(true);
    }

    /// <summary>
    /// Controls the "Options" button in the Main Menu.
    /// </summary>
    public void Options()
    {
        MainMenu.SetActive(false);
        OptionsScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Return" button in the Options Menu.
    /// </summary>
    public void ReturnfromOptions()
    {
        if (!UserPreferences.Buildings && !UserPreferences.Stations && !UserPreferences.AllStreets && !UserPreferences.PublicTransportStreets && !UserPreferences.PublicTransportRailways)
        {
            ErrorMessageOptions2.SetActive(false);
            ErrorMessageOptions.SetActive(true);
        }
        else if(UserPreferences.Stations && !UserPreferences.Subways && !UserPreferences.Trams && !UserPreferences.Trains && !UserPreferences.Railways && !UserPreferences.LightRails && !UserPreferences.Busses)
        {
            ErrorMessageOptions.SetActive(false);
            ErrorMessageOptions2.SetActive(true);
        }
        else
        {
            ErrorMessageOptions.SetActive(false);
            ErrorMessageOptions2.SetActive(false);
            OptionsScreen.SetActive(false);
            MainMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Controls the "About" button in the Main Menu.
    /// </summary>
    public void About()
    {
        MainMenu.SetActive(false);
        AboutScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Credits" button in the Main Menu.
    /// </summary>
    public void Credits()
    {
        MainMenu.SetActive(false);
        CreditsScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Exit Simulation" button in the Main Menu.
    /// </summary>
    public void Close()
    {
        MainMenu.SetActive(false);
        QuitScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Yes" button in the Exit Menu.
    /// </summary>
    public void Quit_yes()
    {
        Application.Quit();
    }

    /// <summary>
    /// Controls the "No" button in the Exit Menu.
    /// </summary>
    public void Quit_no()
    {
        QuitScreen.SetActive(false);
        MainMenu.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Credits, About, Options and 
    /// User Input Menu.
    /// </summary>
    public void Return()
    {
        CreditsScreen.SetActive(false);
        AboutScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        SimulationSource.SetActive(false);
        MainMenu.SetActive(true);
        ErrorMessage.SetActive(false);
    }
}




using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// This class is controlling the Main Menu of the simulation and the whole 
/// "StartScreen" Scene. It also processes the user input of the path to the data 
/// source (the OSM XML file) and switches to the loading screen.
/// </summary>
public class FileLoader : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject MainMenu;
    public GameObject SimulationSource;
    public GameObject OptionsScreen;
    public GameObject AboutScreen;
    public GameObject CreditsScreen;
    public GameObject QuitScreen;
    public GameObject LoadingScreen;
    public GameObject FilePath;
    public GameObject Instructions1;
    public GameObject Instructions2;
    public GameObject Instructions3;
    public GameObject ErrorMessage;
    public GameObject ErrorMessage_2;
    public GameObject ErrorMessageOptions;
    public GameObject ErrorMessageOptions2;
    

    // Here will be the path to the OSM XML file stored.
    public static string ResourceFilePath;

    bool user_input = false;

    public static bool simulator_loaded = false;

    public static bool is_example = false;
    public static int example_indx = 0;

    private void Update()
    {
        // Checks if the user specified path is valid and loads the loading screen.
        if (user_input == true)
        {
            StartScreen.SetActive(false);
            LoadingScreen.SetActive(true);
            user_input = false;
            if(is_example)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                if (File.Exists(ResourceFilePath) && ResourceFilePath.EndsWith(".txt"))
                {
                    SceneManager.LoadScene(1);
                }
                else
                {
                    LoadingScreen.SetActive(false);
                    StartScreen.SetActive(true);
                    ErrorMessage.SetActive(true);
                }
            }
            
        }

        if (simulator_loaded)
        {
            // If the user specified a correct path to an .txt file which is not
            // in the XML format, the loading screen will return to the StartScreen
            // and this line of code will be executed.
            ErrorMessage_2.SetActive(true);
            simulator_loaded = false;
        }
    }


    /// <summary>
    /// Controls the "Launch Simulator" button in the Main Menu.
    /// </summary>
    public void StartSimulation()
    {
        MainMenu.SetActive(false);
        SimulationSource.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "START" button in the user input Menu.
    /// </summary>
    public void ConfirmPath()
    {
        ResourceFilePath = FilePath.GetComponent<Text>().text;
        if (ResourceFilePath != "")
        {
            user_input = true;
            is_example = false;
        }
    }

    public void ConfirmPath_LoadExample(int index)
    {
        user_input = true;
        is_example = true;
        example_indx = index;
    }

    public void ConnectUrl()
    {
        Application.OpenURL("https://www.openstreetmap.org/export#map=11/31.2126/121.4793");
    }


    /// <summary>
    /// Controls the "Help" button in the user input Menu.
    /// </summary>
    public void Help()
    {
        SimulationSource.SetActive(false);
        Instructions1.SetActive(true);
        ErrorMessage.SetActive(false);
    }

    /// <summary>
    /// Controls the "Start Simulation" button in the Instructions Menu.
    /// </summary>
    public void ReturnToSimulationStart()
    {
        Instructions1.SetActive(false);
        Instructions2.SetActive(false);
        Instructions3.SetActive(false);
        SimulationSource.SetActive(true);
    }

    /// <summary>
    /// Controls the "Next Page" button in the Instructions Menu.
    /// </summary>
    public void NextPage1()
    {
        Instructions1.SetActive(false);
        Instructions2.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Instructions Menu.
    /// </summary>
    public void ReturnPage()
    {
        Instructions1.SetActive(true);
        Instructions2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Next Page" button in the Instructions Menu.
    /// </summary>
    public void NexPage2()
    {
        Instructions2.SetActive(false);
        Instructions3.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Instructions Menu.
    /// </summary>
    public void Return2()
    {
        Instructions3.SetActive(false);
        Instructions2.SetActive(true);
    }

    /// <summary>
    /// Controls the "Options" button in the Main Menu.
    /// </summary>
    public void Options()
    {
        MainMenu.SetActive(false);
        OptionsScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Return" button in the Options Menu.
    /// </summary>
    public void ReturnfromOptions()
    {
        if (!UserPreferences.Buildings && !UserPreferences.Stations && !UserPreferences.AllStreets && !UserPreferences.PublicTransportStreets && !UserPreferences.PublicTransportRailways)
        {
            ErrorMessageOptions2.SetActive(false);
            ErrorMessageOptions.SetActive(true);
        }
        else if(UserPreferences.Stations && !UserPreferences.Subways && !UserPreferences.Trams && !UserPreferences.Trains && !UserPreferences.Railways && !UserPreferences.LightRails && !UserPreferences.Busses)
        {
            ErrorMessageOptions.SetActive(false);
            ErrorMessageOptions2.SetActive(true);
        }
        else
        {
            ErrorMessageOptions.SetActive(false);
            ErrorMessageOptions2.SetActive(false);
            OptionsScreen.SetActive(false);
            MainMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Controls the "About" button in the Main Menu.
    /// </summary>
    public void About()
    {
        MainMenu.SetActive(false);
        AboutScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Credits" button in the Main Menu.
    /// </summary>
    public void Credits()
    {
        MainMenu.SetActive(false);
        CreditsScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Exit Simulation" button in the Main Menu.
    /// </summary>
    public void Close()
    {
        MainMenu.SetActive(false);
        QuitScreen.SetActive(true);
        ErrorMessage_2.SetActive(false);
    }

    /// <summary>
    /// Controls the "Yes" button in the Exit Menu.
    /// </summary>
    public void Quit_yes()
    {
        Application.Quit();
    }

    /// <summary>
    /// Controls the "No" button in the Exit Menu.
    /// </summary>
    public void Quit_no()
    {
        QuitScreen.SetActive(false);
        MainMenu.SetActive(true);
    }

    /// <summary>
    /// Controls the "Return" button in the Credits, About, Options and 
    /// User Input Menu.
    /// </summary>
    public void Return()
    {
        CreditsScreen.SetActive(false);
        AboutScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        SimulationSource.SetActive(false);
        MainMenu.SetActive(true);
        ErrorMessage.SetActive(false);
    }
}
