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

        MoveSceneWithInGameState(SceneName);
    }

    /// <summary>
    /// InGame 상태에 따라 씬을 이동하는 로직을 함수로 분리
    /// </summary>
    /// <param name="SceneName"></param>
    private void MoveSceneWithInGameState(string SceneName)
    {
        //현재 씬 이름이 "InGame"으로 시작하는지 확인후, InGame 씬 상태 저장
        SetHW02InGameState();
        //씬 이동
        if (PlayerPrefs.GetInt("HW02_InGame_Monster") == 1)
        {
            SceneManager.LoadScene(SceneName.Trim() + "Monster" + "Scene");
        }
        else if (PlayerPrefs.GetInt("HW02_InGame_Road") == 1)
        {
            SceneManager.LoadScene(SceneName.Trim() + "Road" + "Scene");
        }
        else
        {
            
            //Trim() : 앞뒤 공백 제거
            //만약 게임 하던 중이 아니면, 기본적으로 roadscene 이동
            if(SceneName.Trim() == "InGame")
            {
                SceneManager.LoadScene(SceneName.Trim() + "Road" + "Scene");
            }
            SceneManager.LoadScene(SceneName.Trim());
        }
    }

    /// <summary>
    /// 현재 씬이 "InGame"으로 시작하는 경우, 현재 씬 상태 저장
    /// </summary>
    private void SetHW02InGameState()
    {
        string activedSceneName = SceneManager.GetActiveScene().name;

        if (activedSceneName.Contains("InGame"))
        {
            if (activedSceneName.Contains("Monster"))
            {
                PlayerPrefs.SetInt("HW02_InGame_Monster", 1);
                PlayerPrefs.SetInt("HW02_InGame_Road", 0);
            }
            else if (activedSceneName.Contains("Road"))
            {
                PlayerPrefs.SetInt("HW02_InGame_Road", 1);
                PlayerPrefs.SetInt("HW02_InGame_Monster", 0);
            }
            PlayerPrefs.Save();
        }
        else if (activedSceneName == "SettingScene")
        {
            // 세팅씬에서는 이전 인게임 상태를 변경하지 않음
            // 아무 동작도 하지 않음
        }
        else
        {
            PlayerPrefs.SetInt("HW02_InGame_Monster", 0);
            PlayerPrefs.SetInt("HW02_InGame_Road", 0);
        }
    }
}

