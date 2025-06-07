using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 마이페이지에서 몬스터 도감과 건물 도감을 표시하는 클래스입니다.
/// </summary>
/// <remarks>
/// 몬스터 도감은 총 처치 수(최대 4마리)를 표시하며,
/// 건물 도감은 개별 방문 여부에 따라 이미지/텍스트의 투명도를 조정합니다.
/// </remarks>
public class MyPageDisplay : MonoBehaviour
{
    [Header("몬스터 도감")]

    /// <summary>
    /// 몬스터 총 처치 수를 표시하는 텍스트입니다.
    /// </summary>
    public TMP_Text monsterCountText;

    /// <summary>
    /// 몬스터 이미지 배열 (총 4개).
    /// 순서는 ECC, Asan, HakMoon, HakKwan에 대응합니다.
    /// </summary>
    public Image[] monsterImages;

    [Header("건물 도감")]

    /// <summary>
    /// 건물 키 값 목록 (예: "ECC", "Asan", "HakMoon", "HakKwan").
    /// PlayerPrefs 키값으로 사용됩니다.
    /// </summary>
    public string[] buildingKeys;

    /// <summary>
    /// 건물 이미지 배열 (각 건물에 대응).
    /// 방문 여부에 따라 흐리게 처리됩니다.
    /// </summary>
    public Image[] buildingImages;

    /// <summary>
    /// 건물 이름 텍스트 배열 (건물 이름 표시용).
    /// 방문 여부에 따라 텍스트 색상이 달라집니다.
    /// </summary>
    public TMP_Text[] buildingTexts;

    /// <summary>
    /// 씬 시작 시 도감 UI를 업데이트합니다.
    /// </summary>
    void Start()
    {
        UpdateMonsterSection();
        UpdateBuildingSection();
    }

    /// <summary>
    /// 몬스터 도감 섹션을 업데이트합니다.
    /// </summary>
    void UpdateMonsterSection()
    {
        var monsterKeys = new string[] { "ECCM", "AsanM", "HakMoonM", "HakKwanM" };
        int monsterCount = 0;

        for (int i = 0; i < monsterKeys.Length; i++)
        {
            string key = monsterKeys[i];
            bool hasMonster = PlayerPrefs.GetInt(key, 0) != 0;
            if (hasMonster)
            {
                monsterCount++;
            }
            if (i < monsterImages.Length)
            {
                monsterImages[i].color = hasMonster ? Color.white : new Color(1f, 1f, 1f, 0.3f);
            }
        }
        monsterCountText.text = $"몬스터 처치 ({monsterCount}/4)";
    }

    /// <summary>
    /// 건물 도감 섹션을 업데이트합니다.
    /// </summary>
    void UpdateBuildingSection()
    {
        for (int i = 0; i < buildingKeys.Length; i++)
        {
            // buildingKeys[i]는 "ECC", "Asan" 등 (M 접미사 없음)
            // 빌딩 여부 확인 (키: buildingKeys[i])
            bool hasBuilding = PlayerPrefs.GetInt(buildingKeys[i], 0) != 0;

            // 빌딩 이미지 투명도 조절
            buildingImages[i].color = hasBuilding ? Color.white : new Color(1f, 1f, 1f, 0.3f);

            // 텍스트 색상 조절
            buildingTexts[i].color = hasBuilding ? Color.black : new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }
}