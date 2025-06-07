using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class QuizController : MonoBehaviour
{
    
    public GameObject mascot;
    public GameObject quizPanel;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI mascotText;
    public TextMeshProUGUI recoverItemNum;

    private GameObject monster;
    private int currentQuizIndex = 0;
    private int attemptsLeft;
    private string building = null;
    private List<Quiz> currentQuizzes;

    private Dictionary<string, List<Quiz>> quizBank = new Dictionary<string, List<Quiz>>();

    private void Start()
    {
        InitQuizBank();

        building = GetCurrentBuildingKey();

        if (building == null)
        {
            Debug.LogError("현재 건물을 찾을 수 없습니다.");
            return;
        }
        string quizBankKey = building + "M"; 
        currentQuizzes = quizBank[quizBankKey];
        attemptsLeft = PlayerPrefs.GetInt("IceTino", 0) + PlayerPrefs.GetInt("Cookie", 0);
        InitMonster();
        ShowQuiz();
    }

    private void InitMonster()
    {
        monster = GameObject.Find(PlayerPrefs.GetString("CurrentMonster"));
    }

    private void InitQuizBank()
    {
        quizBank["HakMoonM"] = new List<Quiz>
        {
            new Quiz("학문관 인쇄실은 무료다.", "O", "맞아! 학문관에서는 흑백 인쇄를 무료로 할 수 있어! 많이 이용해줘~"),
            new Quiz("학문관에서 수업을 한다.", "X", "학문관에서는 수업을 하지 않아! 여기서는 푹 쉬어~")
        };

        quizBank["HakKwanM"] = new List<Quiz>
        {
            new Quiz("학관 기린라운지의 ‘기린’은 신화 속 동물을 의미한다.", "O", "맞아! 기린 라운지의 ‘기린’은 인문관의 상징인 신화 속 동물 기린이야!"),
            new Quiz("학관은 2023년에 공사가 완료되었다.", "O", "맞아 학관은 2023년에 완공된 따끈따끈한 신관이야!")
        };

        quizBank["ECCM"] = new List<Quiz>
        {
            new Quiz("ECC는 Ewha Campus Congratulations의 약자다.", "X", "ECC의 약자는 Ewha Campus Complex야!"),
            new Quiz("잉여계단은 B3에 있다.", "X", "이화의 명물 잉여계단은 B1~B2층에 걸쳐 있어!")
        };

        quizBank["AsanM"] = new List<Quiz>
        {
            new Quiz("공대 도서관은 지하 2층에 있다.", "X", "공대 도서관은 지상 2층에 있어!"),
            new Quiz("공대 강당은 신공학관에 있다.", "X", "공대 강당은 아산공학관에 있어!")
        };
    }

    private string GetCurrentBuildingKey()
    {
        string currentBuding = PlayerPrefs.GetString("CurrentBuilding","null");

        if (currentBuding.Equals("null"))
        {
            return null;
        }
        return currentBuding;
    }

    private void ShowQuiz()
    {
        questionText.text = currentQuizzes[currentQuizIndex].question;
    }

    public void Obt()
    {
        SubmitAnswer("O");
    }

    public void Xbt()
    {
        SubmitAnswer("X");
    }

    public void SubmitAnswer(string userAnswer)
    {
        var quiz = currentQuizzes[currentQuizIndex];
        mascot.SetActive(true);

        if (quiz.answer == userAnswer)
        {
            mascotText.text = quiz.mascotComment;
            monster.SetActive(false);
            quizPanel.SetActive(false);
            PlayerPrefs.SetInt(building+"M", 1);
            Invoke(nameof(SuccessMonster), 4f);
            Debug.Log("정답!");
        }
        else
        {
            if (attemptsLeft <= 0)
            {
                mascotText.text = "기회가 모두 소진되었습니다!";
                quizPanel.SetActive(false);
                Debug.Log("퀴즈 실패");
                Invoke(nameof(FailMonster), 2f);
            }
            else
            {
                attemptsLeft--;
                recoverItemNum.text = attemptsLeft.ToString();

                if (PlayerPrefs.GetInt("Cookie", 0) > 0)
                {
                    PlayerPrefs.SetInt("Cookie", PlayerPrefs.GetInt("Cookie") - 1);
                }
                else
                {
                    PlayerPrefs.SetInt("IceTino", PlayerPrefs.GetInt("IceTino") - 1);
                }
            }

            mascotText.text = "틀렸어! 회복 아이템으로 다시 도전해봐!";
            currentQuizIndex = (currentQuizIndex + 1) % currentQuizzes.Count;
            Invoke(nameof(ShowQuiz), 1.5f); // 1.5초 후 다음 퀴즈 표시
        }

        
    }

    public void SuccessMonster()
    {
        mascotText.text = "몬스터를 물리쳐줘서 고마워!";
        // 씬 전환
        SceneManager.LoadScene("InGameRoadScene");
    }

    public void FailMonster()
    {
        mascotText.text = "다음 기회에 다시 도전해봐!";
        // 씬 전환
        SceneManager.LoadScene("InGameRoadScene");
    }

    private class Quiz
    {
        public string question;
        public string answer;
        public string mascotComment;

        public Quiz(string q, string a, string m)
        {
            question = q;
            answer = a;
            mascotComment = m;
        }
    }
}
