using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: 안유경
/// 톱니바퀴 버튼을 누르면 설정씬으로 이동합니다.
/// </summary>
/// <remarks>
/// SettingScene의 SceneNumber=5로 두고 있습니다.
/// </remarks>
/// 
public class AYK_SceneController : MonoBehaviour
{
    public void OnClick_GoToSetting(Object Target)
    {
        SceneManager.LoadScene(5);
    }
}