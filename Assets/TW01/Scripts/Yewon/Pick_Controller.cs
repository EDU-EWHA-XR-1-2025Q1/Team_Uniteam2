using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pick한 아이템 수를 관리하는 클래스입니다.
/// Author: Yewon
/// </summary>
public class Pick_Controller : MonoBehaviour
{
    private int pickCount = 0;

    /// <summary>
    /// 아이템 클릭 시 Pick 수를 증가시키는 함수입니다.
    /// </summary>
    /// <param name="clickedItem">클릭된 아이템 GameObject</param>
    public void AddClick(GameObject clickedItem)
    {
        pickCount++;
        Debug.Log($"Pick Count: {pickCount}");
        Destroy(clickedItem);
    }

    /// <summary>
    /// 현재 가지고 있는 Pick 수가 1 이상인지 확인하는 함수입니다.
    /// </summary>
    /// <returns>Pick 수가 1 이상이면 true, 아니면 false 반환</returns>
    public bool HasPick()
    {
        return pickCount > 0;
    }

    /// <summary>
    /// Pick 수를 1 감소시키는 함수입니다.
    /// </summary>
    public void DecreasePick()
    {
        pickCount--;
    }
}