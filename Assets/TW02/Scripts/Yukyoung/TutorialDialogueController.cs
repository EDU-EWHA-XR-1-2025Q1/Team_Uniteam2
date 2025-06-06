using UnityEngine;
using TMPro;
using UnityEngine.UI; // Image 컴포넌트 사용 시 필요
/// <summary>
/// Author: 안유경
/// 어플 이용 방법을 소개하는 튜토리얼 대사를 다루는 클래스입니다.
/// 화면을 클릭하면 다음 대사로 넘어갑니다.
/// 튜토리얼이 끝나면 PlayerPrefs.SetInt("Tutorial", 1);으로 설정하고 튜토리얼을 종료합니다.
/// </summary>
public class TutorialDialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public RectTransform imageToMove;  // 위치 변경할 이미지의 RectTransform
    public GameObject image2;
    public GameObject image3;

    // 대사 리스트
    private string[] dialogues = {
        "안녕하세요!\n저는 벗의 캠퍼스 투어를\n도와드릴 새로니라고 해요.",
        "저와 함께 이화 캠퍼스를\n 살펴볼 수 있어요.",
        "그 전에 사용 방법을\n알아볼까요?",
        "[1]\n캠퍼스 건물에 도착하면\n건물도감카드를 얻을 수 있어요.",
        "하나씩 모아보며\n나만의 캠퍼스 도감을 만들어봐요!",
        "앗! 캠퍼스 곳곳에\n 몬스터가 나타났대요!",
        "[2]\n건물의 특정 스팟에서\n몬스터가 나타나요.",
        "친구벗과 함께\n몬스터가 내는 퀴즈를 맞추면\n물리칠 수 있어요 ",
        "[3]\n저 새로니와 캠퍼스를 \n돌아다니다보면\n아이템을 얻을 수 있어요.",
        "이 아이템을 쓰면 전투 기회를\n한 번 더 얻을 수 있어요",
        "[4]\n우측 상단의 톱니버튼을 누르면\nHome, My page, Map으로\n이동할 수 있어요.",
        "My page에서는 건물 도감과\n처치한 몬스터를 확인할 수 있어요.",
        "그럼 이제 이화 캠퍼스를\n함께 걸어볼까요?",
    };

    private int currentIndex = 0;
    private bool moved = false;

    void Start()
    {
        // 첫 대사 출력
        dialogueText.text = dialogues[currentIndex];
        image2.SetActive(false);
        image3.SetActive(false);
    }

    void Update()
    {
        // 화면 터치 또는 마우스 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            ShowNextDialogue();
        }
    }

    void ShowNextDialogue()
    {
        currentIndex++;

        if (currentIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentIndex];
            // 마지막 대사가 표시될 때 위치 변경
            if (currentIndex == 3 && !moved)
            {
                //이미지 위치 변경
                imageToMove.anchoredPosition = new Vector2(150, -120); // 원하는 위치로 변경
                //이미지 크기 변경
                imageToMove.sizeDelta = new Vector2(150, 190);

                moved = true;
            }
            if (currentIndex == 6 || currentIndex == 7)
            {
                image2.SetActive(true);
                image3.SetActive(false);
            }
            else if (currentIndex == 8 || currentIndex == 9)
            {
                image2.SetActive(false);
                image3.SetActive(true);
            }
            else
            {
                image2.SetActive(false);
                image3.SetActive(false);
            }
        }
        else
        {
            // 마지막 대사 이후 처리 (예: 종료 메시지)
            dialogueText.text = "";
            PlayerPrefs.SetInt("Tutorial", 1);
            Debug.Log("튜토리얼 종료");

            // 튜토리얼 끝나면 이미지 다 숨김
            image2.SetActive(false);
            image3.SetActive(false);

        }
    }
}
