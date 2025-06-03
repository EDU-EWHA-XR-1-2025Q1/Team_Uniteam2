using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bowl에 들어온 아이템을 감지하고 삭제하는 클래스입니다.
/// Author: Yewon
/// </summary>
public class Bowl_Controller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
            Debug.Log($"Item entered the bowl and was destroyed.");
            //score증가
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
        }
        
    }
}