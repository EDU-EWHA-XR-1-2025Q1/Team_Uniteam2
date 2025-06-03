using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put할 수 있는 구역 안에서 아이템을 던지는 기능을 담당하는 클래스입니다.
/// Author: Yewon
/// </summary>
public class Put_Controller : MonoBehaviour
{
    /// <summary>
    /// 던질 아이템 프리팹입니다.
    /// </summary>
    public GameObject targetObjectToThrow;

    /// <summary>
    /// 플레이어의 카메라 트랜스폼입니다.
    /// </summary>
    public Transform playerCamera;

    /// <summary>
    /// Pick 수를 관리하는 Pick Controller입니다.
    /// </summary>
    public Pick_Controller pickController;

    private bool isInArea = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isInArea)
        {
            if (pickController != null && pickController.HasPick())
            {
                ThrowItem();
                pickController.DecreasePick();
            }
            else
            {
                Debug.Log("No items to throw!");
            }
        }
    }

    /// <summary>
    /// 아이템을 생성하고 앞으로 던지는 함수입니다.
    /// </summary>
    private void ThrowItem()
    {
        Vector3 spawnPos = playerCamera.position + playerCamera.forward;

        Quaternion randomRot = Quaternion.Euler(
            Random.value * 360f,
            Random.value * 360f,
            Random.value * 360f
        );

        GameObject clone = Instantiate(targetObjectToThrow, spawnPos, randomRot);
        clone.SetActive(true);

        Rigidbody rb = clone.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.AddForce(playerCamera.forward * 400f); // 예전처럼 힘 세게
        }

        Destroy(clone, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FPSController" || other.name == "FirstPersonCharacter")
        {
            isInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "FPSController" || other.name == "FirstPersonCharacter")
        {
            isInArea = false;
        }
    }
}