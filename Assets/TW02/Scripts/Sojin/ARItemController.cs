using UnityEngine;

/// <summary>
/// AR 아이템을 터치(모바일)또는 클릭(pc)해서 수집하는 기능을 담당하는 클래스입니다.
/// </summary>
public class ARItemCollector : MonoBehaviour
{
    public string itemKey;     // PlayerPrefs에 저장할 아이템의 키 이름
    public string itemDescription; // 아이템 설명

    public DialogueManager dialogueManager; // 대화 내용 출력

    /// <summary>
    /// 마우스 클릭 시 아이템을 수집 처리합니다.
    /// </summary>
    private void OnMouseDown()
    {
        CollectItem();
    }

    private void CollectItem()
    {

        // 현재 저장된 아이템 개수를 가져옵니다.
        int currentCount = PlayerPrefs.GetInt(itemKey, 0);
        // 아이템 개수를 1 증가시켜 저장합니다.
        PlayerPrefs.SetInt(itemKey, currentCount + 1);

        // 설명 메시지 생성 (개수 포함)
        string message = $"{itemDescription}\n지금까지 {PlayerPrefs.GetInt(itemKey)}개 모았다.";
        // 대화 내용에 아이템 설명 출력
        dialogueManager.ShowItemInfo(itemKey, message);

        Debug.Log($"{itemKey} 획득됨! 현재 개수: {PlayerPrefs.GetInt(itemKey)}");
        // 아이템 오브젝트를 씬에서 제거
        Destroy(gameObject);

    }

    
}
