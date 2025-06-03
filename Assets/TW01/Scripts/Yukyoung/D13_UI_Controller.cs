using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class D13_UI_Controller : MonoBehaviour
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

        int currentScore = PlayerPrefs.GetInt("Score", 0);  // 현재 저장된 Score 가져오기
        currentScore += 1;                                  // Score도 1 올리기
        PlayerPrefs.SetInt("Score", currentScore);          // Score 저장
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

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Score");      // Score 초기화
    }
}
