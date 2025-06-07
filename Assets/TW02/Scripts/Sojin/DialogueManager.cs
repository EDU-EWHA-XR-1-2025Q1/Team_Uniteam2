using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils.Datums;
using UnityEngine;

/// <summary>
/// 대화 UI를 관리하는 클래스입니다.
/// 건물 설명과 아이템 설명을 화면에 표시합니다.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    /// <summary>
    /// 대화 내용을 표시할 TextMeshProUGUI 컴포넌트입니다.
    /// </summary>
    public TextMeshProUGUI dialogueText;
    public GameObject goAndDefeatButton;
    /// <summary>
    /// 시작 시 잡으러가기 버튼을 숨김
    /// </summary>
    void Start()
    {
        if (goAndDefeatButton != null)
        {
            goAndDefeatButton.SetActive(false);
        }
    }

    /// <summary>
    /// 건물 설명을 화면에 표시합니다.
    /// </summary>
    /// <param name="buildingKey">건물의 고유 키 값입니다.</param>
    /// <param name="description">건물의 설명 텍스트입니다.</param>
    public void ShowBuildingInfo(string buildingKey, string description)
    {
        dialogueText.text = description;
        if (goAndDefeatButton != null)
        {
            goAndDefeatButton.SetActive(false);
        }
    }

    /// <summary>
    /// 아이템 설명과 현재 수집 개수를 포함한 메시지를 화면에 표시합니다.
    /// </summary>
    /// <param name="itemKey">아이템의 고유 키 값입니다.</param>
    /// <param name="messageWithCount">아이템 설명 및 수집 개수를 포함한 메시지입니다.</param>
    public void ShowItemInfo(string itemKey, string messageWithCount)
    {
        dialogueText.text = messageWithCount;
        if (goAndDefeatButton != null)
        {
            goAndDefeatButton.SetActive(false);
        }
    }

    public void ShowMonsterAppeared(string buildingMonsterKey)
    {
        dialogueText.text = "몬스터가 나타났어! 몬스터를 처치해줘.";


        if (goAndDefeatButton != null)
        {
            goAndDefeatButton.SetActive(true);
            Debug.Log("처치하기 버튼 등장");
        }
    }
    public void OnMonsterDefeatButtonClicked()
    {
        
        // 몬스터 처치 씬으로 이동
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGameMonsterScene");
    }

}
