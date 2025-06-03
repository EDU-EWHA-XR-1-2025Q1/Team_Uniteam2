using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D13_Pick_Controller : MonoBehaviour
{
    int clickCounter = 0;
    public GameObject UI;

    public void add_Click(GameObject Clone){
        clickCounter = UI.GetComponent<D13_UI_Controller>().GetPickCounts();
        clickCounter++;
        print($"clickCount: {clickCounter}");
        Destroy(Clone);

        UI.GetComponent<D13_UI_Controller>().Display_PickCounts(clickCounter);
    }
}
