using UnityEngine;
using Vuforia;

/// <summary>
/// Vuforia 이미지 타겟 인식 시 건물 방문 기록과 설명 표시를 담당하는 클래스입니다.
/// </summary>
public class BuildingTarget : MonoBehaviour
{
    public string buildingKey; ///PlayerPrefs에 저장할 건물의 키 이름
    public string description; ///건물 설명
    public string buildingName;
    /// <summary>
    /// 대화 UI를 관리하는 DialogueManager 객체입니다.
    /// </summary>
    public DialogueManager dialogueManager;

    /// <summary>
    /// 이미지 타겟 상태 변경을 감지하는 ObserverBehaviour 컴포넌트입니다.
    /// </summary>
    private ObserverBehaviour observerBehaviour;

    private bool isWaitingForSecondClick = false;
    private bool hasShownFirstMessage = false;

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

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            hasShownFirstMessage = false;

            if (PlayerPrefs.GetInt(buildingKey, 0) == 0)
            {
                // 건물 설명 먼저 보여주고
                dialogueManager.ShowBuildingInfo(buildingKey, description);
                isWaitingForSecondClick = true;
            }
            else
            {
                // 이미 등록된 건물이면 바로 설명만 보여줌
                dialogueManager.ShowBuildingInfo(buildingKey, description);
            }
        }
    }

    public void OnTargetFoundSetCurrentBuilding()
    {
        // 건물 이름을 PlayerPrefs에 저장
        PlayerPrefs.SetString("CurrentBuilding", buildingName);
    }

    /// <summary>
    /// 매 프레임마다 사용자 입력을 감지하여 건물도감을 획득하도록 처리합니다.
    /// </summary>
    void Update()
    {
        // 사용자가 설명을 본 후 두 번째 클릭을 기다리고 있는 상태이며,
        // 아직 건물도감 획득 메시지를 보여주지 않았고, 마우스 클릭이 발생한 경우
        if (isWaitingForSecondClick && !hasShownFirstMessage && Input.GetMouseButtonDown(0))
        {
           
            // 건물 방문 여부를 저장 (1 = 방문 완료)
            PlayerPrefs.SetInt(buildingKey, 1);


            // 건물도감을 획득했다는 메시지를 표시
            dialogueManager.ShowBuildingInfo(buildingKey, $"{buildingName}의 건물도감을 획득했어! 마이페이지의 건물도감에서 확인할 수 있어~");

            // 메시지를 이미 표시했음을 기록하고, 두 번째 클릭 대기 상태 종료
            hasShownFirstMessage = true;
            isWaitingForSecondClick = false;
          
            
        }
    }

}
