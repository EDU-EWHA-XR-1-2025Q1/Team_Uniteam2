using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 클릭을 감지하고 Pick Controller로 전달하는 클래스입니다.
/// Author: Yewon
/// </summary>
public class Item_Controller : MonoBehaviour
{
    /// <summary>
    /// 클릭 처리를 담당할 Pick Controller입니다.
    /// </summary>
    public GameObject pickController;

    private void OnMouseDown()
    {
        Debug.Log($"{gameObject.name} clicked");
        pickController.GetComponent<Pick_Controller>().AddClick(gameObject);
    }
}