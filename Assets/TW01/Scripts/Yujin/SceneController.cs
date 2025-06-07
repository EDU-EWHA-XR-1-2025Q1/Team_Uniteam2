using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Definitions;
using Unity.VisualScripting;
/// <summary>
/// Scene을 이동하는 컨트롤러 스크립트입니다.
/// </summary>
/// <remarks>
/// Author: 전유진
/// </remarks>
public class SceneController : MonoBehaviour
{
    /// <summary>
    /// 마우스 클릭 시 씬을 이동하는 메소드입니다.
    /// </summary>
    /// <param name="SceneName"> 이동할 씬 이름입니다.</param>
    /// <remarks>   
    /// 사용자정의타입(ENUM)을 인자로 못 넣는 이슈로 string으로 인자변경.
    /// * InGame일경우, InGame으로 넣기.
    /// </remarks>
    public void OnMouseClick_MoveScene(string SceneName)
    {

        //씬 이름 할당 안했을 시 예외처리
        if (string.IsNullOrEmpty(SceneName))
        {
            Debug.LogError("SceneName is null or empty.");
            return;
        }

        MoveScene(SceneName);
    }

    /// <summary>
    /// InGame 상태에 따라 씬을 이동하는 로직을 함수로 분리
    /// </summary>
    /// <param name="SceneName">
    /// 씬이름을 넣습니다. *InGame일 경우, "InGame"으로 넣어주세요.
    /// </param>
    private void MoveScene(string SceneName)
    {
        if(SceneName == "InGame")
        {
            // InGame 씬으로 이동
            SceneManager.LoadScene("InGameRoadScene");
            return;
        }
        SceneManager.LoadScene(SceneName.Trim());
        
    }


}

