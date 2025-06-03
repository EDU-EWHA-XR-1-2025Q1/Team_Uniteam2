using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D14_Put_Controller : MonoBehaviour
{
    public GameObject TargetObjectToThrow;
    public Transform PlayerCamera;
    bool isInTheArea = false;
    public GameObject UI;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isInTheArea==true){ //main mouse button을 눌렀다면
            int pickCounts = UI.GetComponent<D13_UI_Controller>().GetPickCounts();
            if (pickCounts > 0)
            {
                Throw();
                UI.GetComponent<D13_UI_Controller>().Decrease_PickCounts();
            }
        }
    }
    void Throw()
    {
        Vector3 Pos = PlayerCamera.transform.position + PlayerCamera.transform.forward;
        float randomAngleX = Random.value * 360f;
        float randomAngleY = Random.value * 360f;
        float randomAngleZ = Random.value * 360f;
        Quaternion randomRot = Quaternion.Euler(randomAngleX, randomAngleY, randomAngleZ);

        GameObject Clone = Instantiate(TargetObjectToThrow, Pos, randomRot);

        Clone.SetActive(true);
        Clone.GetComponent<Collider>().isTrigger = false;
        Clone.GetComponent<Rigidbody>().useGravity = true;

        Clone.GetComponent<Rigidbody>().AddForce(PlayerCamera.forward * 400f);

        Destroy(Clone, 3);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "FPSController")
        {
            isInTheArea = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == "FPSController")
        {
            isInTheArea = false;
        }
    }
}
