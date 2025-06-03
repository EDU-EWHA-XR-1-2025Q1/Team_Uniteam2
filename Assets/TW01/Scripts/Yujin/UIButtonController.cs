using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UIButton을 활성화/ 비활성화 등 제어하는 컨트롤러 스크립트입니다.
/// </summary>
/// <remarks>
/// Author: 전유진
/// </remarks>
public class UIButtonController : MonoBehaviour
{
    /// <summary>
    /// 마우스 클릭 시 오브젝트를 활성화하는 메소드입니다.
    /// </summary>
    /// <param name="Object"> 활성화 할 오브젝트를 지정합니다. </param>
    public void OnMouseClick_EnableObject(GameObject Object)
    {
        Object.SetActive(true);
    }
    /// <summary>
    /// 마우스 클릭 시 오브젝트를 비활성화하는 메소드입니다.
    /// </summary>
    /// <param name="Object"> 비활성화 할 오브젝트를 지정합니다. </param>
    public void OnMouseClick_UnEnableObject(GameObject Object)
    {
        Object.SetActive(false);
    }
}
