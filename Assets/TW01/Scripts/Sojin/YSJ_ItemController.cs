using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class YSJ_ItemController.
/// </summary>
public class YSJ_ItemController : MonoBehaviour
{
    /// <summary>
    /// The pick controller
    /// </summary>
    public GameObject PickController;

    /// <summary>
    /// Called when [mouse down].
    /// </summary>
    void OnMouseDown()
    {
        print($"{gameObject.name} clicked!");
        PickController.GetComponent<YSJ_PickController>().Add_Click(gameObject);
        //pickController에 보고
       
    }
}
