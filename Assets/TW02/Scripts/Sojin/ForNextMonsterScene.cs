using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터가 등장하는 시점을 제어하며, 몬스터 처치 여부를 확인하는 클래스입니다.
/// 건물에 해당하는 몬스터 정보를 DialogueManager를 통해 표시합니다.
/// </summary>
public class ForNextMonsterScene : MonoBehaviour
{
    /// <summary>
    /// 대화 UI와 버튼을 관리하는 DialogueManager입니다.
    /// </summary>
    public DialogueManager dialogueManager;

    /// PlayerPrefs에 저장된 몬스터 처치 여부 키입니다. (1이면 처치된 상태, AsanD 등)
    public string monsterDefeatedKey;

    /// 현재 건물에 해당하는 몬스터 키입니다. (예: "AsanM", "ECCM" 등)
    public string buildingMonsterKey;

    /// <summary>
    /// 시작 시 해당 몬스터가 이미 처치된 경우 오브젝트를 비활성화합니다.
    /// </summary>
    private void Start()
    {
        // 몬스터가 이미 처치된 경우, 이 오브젝트는 비활성화
        if (PlayerPrefs.GetInt(monsterDefeatedKey, 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 몬스터 오브젝트를 클릭했을 때 몬스터 등장 대사를 보여줍니다.
    /// </summary>
    private void OnMouseDown()
    {
        dialogueManager.ShowMonsterAppeared(buildingMonsterKey);
        PlayerPrefs.SetString("CurrentMonster", buildingMonsterKey);
    }
}
