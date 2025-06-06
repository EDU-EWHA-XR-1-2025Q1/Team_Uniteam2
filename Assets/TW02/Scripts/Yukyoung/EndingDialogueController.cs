using UnityEngine;
using TMPro;
/// <summary>
/// Author: 안유경
/// 몬스터 처치가 끝나면 마무리인사(엔딩) 대사를 다루는 클래스입니다.
/// 화면을 클릭하면 다음 대사로 넘어갑니다.
/// </summary>
/// <remarks>
/// 첫번째 대사가 출력되면 캐릭터(새로니)가 통통 움직입니다.
/// </remarks>

public class EndingDialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText2;
    public float typingSpeed = 0.05f;

    public CharMovingController shaker;

    private string[] dialogues = {
        "와~ 드디어 캠퍼스의\n모든 몬스터를 물리쳤어요!",
        "친구벗과 학교를 돌아다녀보며\n이화 캠퍼스에 좀 익숙해졌나요?",
        "게임에서 방문하지 못했던\n건물도 친구벗과 함께\n방문해보는걸 추천해요.",
        "앞으로의 학교 생활을 응원해요!",
        "화이팅이화!"
    };

    private int currentIndex = 0;
    private bool isTyping = false;
    private bool cancelTyping = false;

    void Start()
    {
        StartCoroutine(TypeDialogue(dialogues[currentIndex]));
        shaker.StartShake();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                cancelTyping = true;  // 출력 중이면 → 전체 출력으로 전환
            }
            else
            {
                ShowNextDialogue();  // 출력 다 끝났으면 다음 대사로
            }
        }
    }

    void ShowNextDialogue()
    {
        currentIndex++;

        if (currentIndex < dialogues.Length)
        {
            StartCoroutine(TypeDialogue(dialogues[currentIndex]));
        }
        else
        {
            dialogueText2.text = "화이팅이화!";
            Debug.Log("엔딩");
        }
    }

    System.Collections.IEnumerator TypeDialogue(string line)
    {
        isTyping = true;
        cancelTyping = false;
        dialogueText2.text = "";

        for (int i = 0; i < line.Length; i++)
        {
            if (cancelTyping)
            {
                dialogueText2.text = line;
                break;
            }

            dialogueText2.text += line[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        cancelTyping = false;
    }
}
