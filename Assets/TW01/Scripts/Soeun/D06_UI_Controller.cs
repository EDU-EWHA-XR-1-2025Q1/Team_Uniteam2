using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class D06_UI_Controller : MonoBehaviour
{
    public TMP_Text PickCounts;            
    public TMP_Text PutCounts;              


    public void Display_PickCounts(int count)
    {
        PickCounts.text = count.ToString();
    }

    public void Display_PutCounts()
    {
        int lastPutCount = int.Parse(PutCounts.text);
        int currentPutCount = lastPutCount + 1;
        PutCounts.text = currentPutCount.ToString();
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score")+1);
        Debug.Log("프리팹 점수 : " + PlayerPrefs.GetInt("Score"));
    }

    public void Decrease_PickCounts()
    {
        int lastPickCount = int.Parse(PickCounts.text);
        int currentPickCount = lastPickCount - 1;
        PickCounts.text = currentPickCount.ToString();
    }

    public int GetPickCounts()
    {
        int pickCounts = int.Parse(PickCounts.text);
        return pickCounts;
    }
}
