using UnityEngine;

public class CharMovingController : MonoBehaviour
{
    public RectTransform target;
    public float duration = 1f;       // 진동 시간
    public float magnitude = 10f;        // 진동 크기 (너무 크면 과하게 튐)
    public int frequency = 4;           // 진동 횟수 (높을수록 많이 튐)

    private Vector3 originalPos;

    public void StartShake()
    {
        originalPos = target.anchoredPosition;
        StopAllCoroutines();
        StartCoroutine(SoftBounce());
    }

    System.Collections.IEnumerator SoftBounce()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offset = Mathf.Sin(elapsed * frequency * Mathf.PI * 2) * magnitude;
            target.anchoredPosition = originalPos + new Vector3(0, offset, 0); // 위아래 bounce 느낌

            elapsed += Time.deltaTime;
            yield return null;
        }

        target.anchoredPosition = originalPos;
        Debug.Log(target.anchoredPosition);

    }
}