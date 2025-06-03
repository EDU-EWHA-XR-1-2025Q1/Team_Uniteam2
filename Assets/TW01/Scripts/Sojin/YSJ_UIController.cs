using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class YSJ_UIController.
/// </summary>
public class YSJ_UIController : MonoBehaviour
{
    public TMP_Text PickCounts;

    /// <summary>
    /// Displays the pick counts in UI
    /// </summary>
    /// <param name="count">The count.</param>
    public void Display_PickCounts(int count)
    {
        PickCounts.text = count.ToString();
    }

    /// <summary>
    /// Decreases the pick counts.
    /// </summary>
    public void Decrease_PickCounts()
    {
        int lastPickCount= int.Parse(PickCounts.text); //string->int
        int currentPickCount = lastPickCount - 1;
        PickCounts.text=currentPickCount.ToString(); //int->string

        //점수 변경 후 PlayerPrefs에 저장
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score")+1);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Gets the pick counts.
    /// </summary>
    /// <returns>System.Int32.</returns>
    public int GetPickCounts()
    {
        int pickCounts=int.Parse(PickCounts.text);
        return pickCounts;
    }
}

//int savedPickCounts = PlayerPrefs.SetInt("Score", 0);
//Display_PickCounts(savedPickCounts);