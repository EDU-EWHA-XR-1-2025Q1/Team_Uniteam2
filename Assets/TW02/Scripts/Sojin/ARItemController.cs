using UnityEngine;

/// <summary>
/// AR 아이템을 터치(또는 클릭)해서 수집하는 기능을 담당하는 클래스입니다.
/// </summary>
public class ARItemCollector : MonoBehaviour
{
    /// <summary>
    /// PlayerPrefs에 저장할 아이템의 키 이름입니다.
    /// 예: "IceTino", "Cookie" 등
    /// </summary>
    public string itemKey;

    /// <summary>
    /// 매 프레임마다 호출됩니다.
    /// 테스트용 PC 마우스 클릭과 모바일용 터치 입력을 감지하고,
    /// 터치 또는 클릭 위치에서 레이캐스트로 아이템을 수집합니다.
    /// </summary>
    void Update()
    {
        //// PC용 마우스 좌클릭 입력 감지
        //if (Input.GetMouseButtonDown(0))
        //{
        //    int currentCount = PlayerPrefs.GetInt(itemKey, 0);
        //    // 아이템 개수 1 증가시키고 저장
        //    PlayerPrefs.SetInt(itemKey, currentCount + 1);
        //    Debug.Log($"아이템 획득됨! 현재 개수: {currentCount + 1}");
        //    // 아이템 오브젝트 제거
        //    Destroy(gameObject);
        //}

        // 모바일 터치 입력 감지
        // 화면을 터치했는지 & 화면이 막 눌렸을 때 한번만 인식
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            // 터치된 화면 좌표를 기준으로 레이 생성
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            TryCollect(ray);
        }
    }

    private void OnMouseDown()
    {
        int currentCount = PlayerPrefs.GetInt(itemKey, 0);
        // 아이템 개수 1 증가시키고 저장
        PlayerPrefs.SetInt(itemKey, currentCount + 1);
        Debug.Log($"{itemKey} 획득됨! 현재 개수: {currentCount + 1}");
        // 아이템 오브젝트 제거
        Destroy(gameObject);
    }

    /// <summary>
    /// 레이가 아이템과 충돌하면 수집 처리합니다.
    /// </summary>
    /// <param name="ray"></param>
    void TryCollect(Ray ray)
    {
        RaycastHit hit;
        // 레이캐스트 실행: 광선이 물체와 충돌했는지 검사
        if (Physics.Raycast(ray, out hit))
        {
            // 충돌한 오브젝트가 이 스크립트가 붙은 오브젝트와 같은지 확인
            if (hit.transform == this.transform)
            {
                // 현재 저장된 아이템 개수 불러오기 (기본 0)
                int currentCount = PlayerPrefs.GetInt(itemKey, 0);
                // 아이템 개수 1 증가시키고 저장
                PlayerPrefs.SetInt(itemKey, currentCount + 1);
                Debug.Log($"아이템 획득됨! 현재 개수: {currentCount + 1}");
                // 아이템 오브젝트 제거
                Destroy(gameObject);
            }
        }
    }
}
