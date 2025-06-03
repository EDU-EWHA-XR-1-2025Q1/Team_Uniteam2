using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSJ_PutController : MonoBehaviour
{

    public GameObject TargetObjectToThrow; //던질 오브젝트

    public Transform PlayerCamera; //카메라의 정보(위치, 스케일 등)가 transform에 담겨있음
    bool isInTheArea = false;
    public GameObject UI;

    /// <summary>
    /// Updates this instance.
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isInTheArea)
        {
            int pickCounts = UI.GetComponent<YSJ_UIController>().GetPickCounts(); 
            if (pickCounts > 0)
            {
                Throw();
                UI.GetComponent<YSJ_UIController>().Decrease_PickCounts();
                
            }
        }
    }

    /// <summary>
    /// Throws this instance.
    /// </summary>
    void Throw()
    {
        Vector3 Pos = PlayerCamera.transform.position+PlayerCamera.transform.forward;
        //transform:월드의 전방이 아니라 플레이어 기준 전방
                
        float randomAngleX = Random.value * 360f;
        float randomAngleY = Random.value * 360f;
        float randomAngleZ = Random.value * 360f;

        Quaternion randomRot = Quaternion.Euler(randomAngleX, randomAngleY, randomAngleZ);

        GameObject clone = Instantiate(TargetObjectToThrow, Pos, randomRot); 
        //플레이어가 향하는 방향으로, xyz가 랜덤인 상태로 instantiate됨

        clone.SetActive(true);

        //던질때는 그릇에 들어가느냐로 판정

        clone.GetComponent<Collider>().isTrigger = false; //던진 오브젝트가 그릇을 통과하지 않도록 isTrigger->false
        clone.GetComponent<Rigidbody>().useGravity = true; //포물선으로 날아가도록 중력 영향o

        //던지는 힘
        clone.GetComponent<Rigidbody>().AddForce(PlayerCamera.forward * 400f);


        Destroy(clone, 3); //던지고 3초후 소멸
    }


    /// <summary>
    /// Called when [trigger enter]. put영역 안에 있어야만 던질 수 있게 
    /// </summary>
    /// <param name="other">The other.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "FPSController")
        {
            isInTheArea = true;
        }
    }
    /// <summary>
    /// Called when [trigger exit].
    /// </summary>
    /// <param name="other">The other.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.name == "FPSController")
        {
            isInTheArea = false;
        }
    }



}
