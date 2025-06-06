using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyPageDisplay : MonoBehaviour
{
    // 텍스트
    public TMP_Text monsterTitle;

    // 몬스터 이미지들 (순서: ECC, Asan, HakMoon, HakKwan)
    public Image[] monsterImages;

    // 아이템 이미지들
    public Image cookieImage;
    public Image iceTinoImage;

    void Start()
    {
        // 몬스터 수 표시
        int count = PlayerPrefs.GetInt("MonsterCount", 0);
        monsterTitle.text = $"몬스터 처치 ({count}/4)";

        // 몬스터 이미지 활성화 (처치 수만큼만 표시)
        for (int i = 0; i < monsterImages.Length; i++)
        {
            monsterImages[i].enabled = i < count;
        }

        // 아이템 보유 여부에 따라 이미지 색상 설정
        cookieImage.color = PlayerPrefs.GetInt("Cookie") == 1 ? Color.white : new Color(1f, 1f, 1f, 0.3f);
        iceTinoImage.color = PlayerPrefs.GetInt("IceTino") == 1 ? Color.white : new Color(1f, 1f, 1f, 0.3f);
    }
}