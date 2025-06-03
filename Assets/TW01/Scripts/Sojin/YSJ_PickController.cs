using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSJ_PickController : MonoBehaviour
{
    int clickCounter = 0;
    public GameObject UI;

    /// <summary>
    /// Adds the click.
    /// </summary>
    /// <param name="clone">The clone.</param>
    public void Add_Click(GameObject clone)
    {
        clickCounter++;
        print($"clickCount: {clickCounter}");
        Destroy(clone);
   
        //UI에 업데이트된 클릭 수 표시
        UI.GetComponent<YSJ_UIController>().Display_PickCounts(clickCounter); 
        
    }

}
