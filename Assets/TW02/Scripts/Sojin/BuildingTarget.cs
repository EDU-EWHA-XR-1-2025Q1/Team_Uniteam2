using UnityEngine;
using Vuforia;

/// <summary>
/// Vuforia 이미지 타겟 인식 시 건물 방문 기록과 설명 표시를 담당하는 클래스입니다.
/// </summary>
public class BuildingTarget : MonoBehaviour
{
    public string buildingKey; ///PlayerPrefs에 저장할 건물의 키 이름
    public string description; ///건물 설명

    /// <summary>
    /// 대화 UI를 관리하는 DialogueManager 객체입니다.
    /// </summary>
    public DialogueManager dialogueManager;

    /// <summary>
    /// 이미지 타겟 상태 변경을 감지하는 ObserverBehaviour 컴포넌트입니다.
    /// </summary>
    private ObserverBehaviour observerBehaviour;

    /// <summary>
    /// 초기화 메서드로 ObserverBehaviour 컴포넌트를 가져와
    /// 상태 변경 이벤트에 콜백을 등록합니다.
    /// </summary>
    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    /// <summary>
    /// 오브젝트가 파괴될 때 이벤트 구독을 해제합니다.
    /// </summary>
    private void OnDestroy()
    {
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    /// <summary>
    /// 이미지 타겟 상태가 변경될 때 호출되는 콜백 함수입니다.
    /// 타겟이 인식(Tracked)되면 방문 기록을 저장하고 건물 설명을 표시합니다.
    /// </summary>
    /// <param name="behaviour">상태가 변경된 ObserverBehaviour 객체</param>
    /// <param name="status">새로운 타겟 상태</param>
    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            // 방문 기록이 없으면 PlayerPrefs에 저장
            if (PlayerPrefs.GetInt(buildingKey, 0) == 0)
            {
                PlayerPrefs.SetInt(buildingKey, 1);
            }

            // 건물 설명 항상 표시
            dialogueManager.ShowBuildingInfo(buildingKey, description);
        }
    }
}
