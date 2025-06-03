using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D13_Item_Controller : MonoBehaviour
{
    public GameObject PickController;
    private void OnMouseDown()
    {
        print($"{gameObject.name} clicked");
        PickController.GetComponent<D13_Pick_Controller>().add_Click(gameObject);
    }
}
