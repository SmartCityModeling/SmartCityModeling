using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Changes the language settings of both scenes.
/// </summary>
public class LanguageSettings : MonoBehaviour
{
    [SerializeField] List<GameObject> UI_Eng;

    // Standard Language is English.
    public static string Language = "English";

    private void Start()
    {
        if (Language == "English")
        {
            EnglishUI();
        }
    }

    /// <summary>
    /// Switches all UI-Elements to English.
    /// </summary>
    public void EnglishUI()
    {

        foreach (var obj in UI_Eng)
           obj.SetActive(true);

        Language = "English";
    }

}
