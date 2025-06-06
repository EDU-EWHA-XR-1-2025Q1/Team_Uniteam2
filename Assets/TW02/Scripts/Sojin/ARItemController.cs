using UnityEngine;

/// <summary>
/// AR 아이템을 터치(모바일)또는 클릭(pc)해서 수집하는 기능을 담당하는 클래스입니다.
/// </summary>
public class ARItemCollector : MonoBehaviour
{
    public string itemKey;     /// PlayerPrefs에 저장할 아이템의 키 이름
    public string itemDescription; /// 아이템 설명

    /// <summary>
    /// 대화 내용을 출력하는 DialogueManager 참조입니다.
    /// </summary>
    public DialogueManager dialogueManager;

    /// <summary>
    /// 매 프레임마다 호출되며, 모바일 터치 입력을 감지하여 아이템 수집 시도합니다.
    /// </summary>
    void Update()
    {
        // 모바일 터치 입력 감지
        // 화면을 터치했는지 & 터치가 시작된 순간인지 확인
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            // 터치된 화면 좌표를 기준으로 레이 생성
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            TryCollect(ray);
        }
    }

    /// <summary>
    /// 마우스 클릭 시 아이템을 수집 처리합니다.
    /// </summary>
    private void OnMouseDown()
    {
        // 현재 저장된 아이템 개수를 가져옵니다.
        int currentCount = PlayerPrefs.GetInt(itemKey, 0);
        // 아이템 개수를 1 증가시켜 저장합니다.
        PlayerPrefs.SetInt(itemKey, currentCount + 1);

        // 설명 메시지 생성 (개수 포함)
        string message = $"{itemDescription}\n지금까지 {currentCount}개 모았다.";
        // 대화 내용에 아이템 설명 출력
        dialogueManager.ShowItemInfo(itemKey, message);

        Debug.Log($"{itemKey} 획득됨! 현재 개수: {currentCount}");
        // 아이템 오브젝트를 씬에서 제거
        Destroy(gameObject);
    }

    /// <summary>
    /// 레이캐스트가 아이템에 충돌하면 아이템을 수집 처리합니다.
    /// </summary>
    /// <param name="ray">화면 터치 위치에서 생성된 레이입니다.</param>
    void TryCollect(Ray ray)
    {
        RaycastHit hit;
        // 레이캐스트 실행: 광선이 물체와 충돌했는지 검사
        if (Physics.Raycast(ray, out hit))
        {
            // 충돌한 오브젝트가 이 스크립트가 붙은 오브젝트인지 확인
            if (hit.transform == this.transform)
            {
                // 현재 저장된 아이템 개수 불러오기 (기본값 0)
                int currentCount = PlayerPrefs.GetInt(itemKey, 0);
                // 아이템 개수 1 증가 후 저장
                PlayerPrefs.SetInt(itemKey, currentCount + 1);

                // 아이템 설명 메시지 생성 (개수 포함)
                string message = $"{itemDescription}\n지금까지 {currentCount}개 모았다.";
                // 대화 내용에 아이템 설명 출력
                dialogueManager.ShowItemInfo(itemKey, message);

                Debug.Log($"{itemKey} 획득됨! 현재 개수: {currentCount}");
                // 아이템 오브젝트 제거
                Destroy(gameObject);
            }
        }
    }
}
