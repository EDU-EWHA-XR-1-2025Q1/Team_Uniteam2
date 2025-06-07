using UnityEngine;
using TMPro;
using UnityEngine.UI; // Image 컴포넌트 사용 시 필요
using System.Collections; // 코루틴 사용 시 필요

/// <summary>
/// Author: 안유경
/// 어플 이용 방법을 소개하는 튜토리얼 대사를 다루는 클래스입니다.
/// 화면을 클릭하면 다음 대사로 넘어갑니다.
/// </summary>
/// <remarks>
/// 게임 내 요소별(건물도감, 몬스터, 회복아이템)설명과 UI 기능 설명을 포함합니다.
/// 튜토리얼이 끝나면 PlayerPrefs.SetInt("Tutorial", 1);으로 설정하고 튜토리얼을 종료합니다.
/// </remarks>

public class TutorialDialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public RectTransform imageToMove;  // 위치 변경할 이미지의 RectTransform

    public Button nextButton; 

    public GameObject image2;
    public GameObject image3;
    public GameObject image4;

    public Image[] floatingImagesGroup1;    // image2 하위 이미지들을 배열로 받기
    public Image[] floatingImagesGroup2;    // image3 하위 이미지들을 배열로 받기
    public Image[] floatingImagesGroup3;    // image3 하위 이미지들을 배열로 받기

    // 대사 리스트
    private string[] dialogues = {
        "안녕하세요!\n저는 벗의 캠퍼스 투어를\n도와드릴 새로니라고 해요.",
        "저와 함께 이화 캠퍼스를\n 살펴볼 수 있어요.",
        "그 전에 사용 방법을\n알아볼까요?",
        "[1]\n캠퍼스 건물에 도착해서\n건물 이름을 인식하면\n건물마다 설명을 볼 수 있어요.",
        "건물에 방문할 때마다\n캠퍼스 도감에 추가돼요.",
        "앗! 캠퍼스 곳곳에\n 몬스터가 나타났대요!",
        "[2]\n건물의 특정 스팟에서\n몬스터가 나타나요.",
        "친구벗과 함께\n몬스터가 내는 퀴즈를 맞추면\n물리칠 수 있어요 ",
        "[3]\n건물 이름을 인식하면\n회복 아이템을 얻을 수 있어요.",
        "이 아이템을 쓰면 전투 기회를\n한 번 더 얻을 수 있어요.",
        "[4]\n우측 상단의 톱니버튼을 누르면\nHome, My page, Map으로\n이동할 수 있어요.",
        "언제든지 여기서\n배경 음악도 끌 수 있어요",
        "[5]\nMy page에서는\n획득한 건물 도감과\n처치한 몬스터를 확인할 수 있어요.",
        "자, 그럼 이제 이화 캠퍼스를\n함께 걸어볼까요?",
    };

    private int currentIndex = 0;
    private bool moved = false;
    private bool isTyping = false; // 타자 효과 중 여부
    private Coroutine typingCoroutine;
    void Start()
    {
        // 첫 대사 출력
        typingCoroutine = StartCoroutine(TypeDialogue(dialogues[currentIndex]));
        image2.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(false);
        nextButton.gameObject.SetActive(false); // 처음엔 안 보이게

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // 글자 출력 중이면 → 코루틴 멈추고 전체 문장 바로 표시
                StopCoroutine(typingCoroutine);
                dialogueText.text = dialogues[currentIndex];
                isTyping = false;
            }
            else
            {
                ShowNextDialogue();
            }
        }
    }


    void ShowNextDialogue()
    {
        currentIndex++;

        if (currentIndex < dialogues.Length)
        {
            if (currentIndex == 6 && !moved)            //설명 시작할 때 새로니 위치 변경
            {
                imageToMove.anchoredPosition = new Vector2(150, -120);
                imageToMove.sizeDelta = new Vector2(150, 190);
                moved = true;
            }
            if (currentIndex == 5)
            {
                StartCoroutine(ShakeImage()); // 쿵! 효과
            }
            else if (currentIndex == 6 || currentIndex == 7) //몬스터 실루엣 출력
            {
                image2.SetActive(true);
                image3.SetActive(false);
                image4.SetActive(false);
                if (currentIndex == 6) StartCoroutine(ShowFloatingImages(floatingImagesGroup1));

            }
            else if (currentIndex == 8 || currentIndex == 9) //아이템 출력
            {
                image2.SetActive(false);
                image3.SetActive(true);
                image4.SetActive(false);
                if (currentIndex == 8) StartCoroutine(ShowFloatingImages(floatingImagesGroup2));
            }
            else if (currentIndex == 10 || currentIndex == 11)
            {
                image2.SetActive(false);
                image3.SetActive(false); 
                image4.SetActive(true);
                if (currentIndex == 10) StartCoroutine(ShowFloatingImages(floatingImagesGroup3));

            }
            else if (currentIndex == 13)
            {
                image4.SetActive(false);
                imageToMove.anchoredPosition = new Vector2(0, 40);
                imageToMove.sizeDelta = new Vector2(255, 300);
            }
            else
            {
                image2.SetActive(false);
                image3.SetActive(false);
                image4.SetActive(false);
            }

            typingCoroutine = StartCoroutine(TypeDialogue(dialogues[currentIndex]));
        }
        else
        {
            dialogueText.text = "자, 그럼 이제 이화 캠퍼스를\n함께 걸어볼까요?";
            PlayerPrefs.SetInt("Tutorial", 1);
            Debug.Log("튜토리얼 종료");
            image2.SetActive(false);
            image3.SetActive(false);
            image4.SetActive(false);

            nextButton.gameObject.SetActive(true);
        }
    }
    IEnumerator ShowFloatingImages(Image[] images, float delay = 0.2f, float duration = 0.5f)
    {
        // 먼저 모든 이미지를 안 보이게 설정
        foreach (Image img in images)
        {
            Color invisible = img.color;
            invisible.a = 0f;
            img.color = invisible;
        }

        // 그 다음 하나씩 FadeAndFloat 실행
        foreach (Image img in images)
        {
            StartCoroutine(FadeAndFloat(img, duration));
            yield return new WaitForSeconds(delay);
        }
    }


    IEnumerator FadeAndFloat(Image img, float duration)
    {
        RectTransform rt = img.GetComponent<RectTransform>();
        Vector2 startPos = rt.anchoredPosition - new Vector2(0, 30);
        Vector2 endPos = startPos + new Vector2(0, 30); // 위로 떠오르기

        float elapsed = 0f;
        Color startColor = img.color;
        startColor.a = 0f;
        Color endColor = img.color;
        endColor.a = 1f;

        // 초기 세팅
        img.color = startColor;
        rt.anchoredPosition = startPos;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            rt.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            img.color = Color.Lerp(startColor, endColor, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 최종값 보정
        rt.anchoredPosition = endPos;
        img.color = endColor;
    }

    IEnumerator ShakeImage(float duration = 0.3f, float magnitude = 5f)
    {
        Vector2 originalPos = imageToMove.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            imageToMove.anchoredPosition = originalPos + new Vector2(offsetX, offsetY);

            elapsed += Time.deltaTime;
            yield return null;
        }

        imageToMove.anchoredPosition = originalPos;
    }
    IEnumerator TypeDialogue(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.04f); // 글자 출력 간격
        }

        isTyping = false;
    }
}