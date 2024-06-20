using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Changes the language settings of both scenes.
/// </summary>
public class LanguageSettings : MonoBehaviour
{
    [SerializeField] List<GameObject> UI_Eng;
    [SerializeField] List<GameObject> UI_Simplified_Chinese;

    // Standard Language is English.
    public static string Language = "English";

    private void Start()
    {
        if (Language == "English")
        {
            EnglishUI();
        }
        else if (Language == "SimplifiedChinese")
        {
            SimplifiedChineseUI();
        }
    }

    /// <summary>
    /// Switches all UI-Elements to English.
    /// </summary>
    public void EnglishUI()
    {

        foreach (var obj in UI_Eng)
           obj.SetActive(true);
        foreach (var obj in UI_SimplifiedChinese)
            obj.SetActive(false);

        Language = "English";
    }
    /// <summary>
    /// Switches all UI-Elements to Simplified Chinese.
    /// </summary>
    public void ChineseUI()
    {
        foreach (var obj in UI_Eng)
            obj.SetActive(false);

        foreach (var obj in UI_SimplifiedChinese)
            obj.SetActive(true);

        Language = "SimplifiedChinese";
    }
}
