using UnityEngine;
using TMPro;
/// <summary>
/// Author: 안유경
/// 몬스터를 모두 처치하면 나오는 엔딩씬의 대사를 다루는 클래스입니다.
/// 화면을 클릭하면 다음 대사로 넘어갑니다.
/// </summary>

public class EndingDialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText2;
    public float typingSpeed = 0.05f;

    public CharMovingController shaker;

    private string[] dialogues = {
        "와~ 드디어 캠퍼스의\n모든 몬스터를 물리쳤어요!",
        "친구벗과 함께 학교를\n돌아다녀보니 이제 이화 캠퍼스에\n좀 익숙해졌나요?",
        "게임에서 방문하지 못했던\n건물들도 친구벗과 함께\n방문해보는걸 추천해요.",
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
