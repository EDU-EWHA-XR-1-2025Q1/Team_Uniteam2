using UnityEngine;
using TMPro;

public class MonsterSceneInit : MonoBehaviour
{

    public GameObject AsanMonster;
    public GameObject HakMoonMonster;
    public GameObject ECCMonster;
    public GameObject HakKwanMonster;

    public TextMeshProUGUI MonsterNameText;
    public TextMeshProUGUI RecoverItemNumText;

    private void Start()
    {
        AsanMonster.SetActive(false);
        ECCMonster.SetActive(false);
        HakKwanMonster.SetActive(false);
        HakMoonMonster.SetActive(false);

        var monsterList = new (int value, GameObject monsterObj, string name)[]
        {
            (PlayerPrefs.GetInt("AsanM", 0), AsanMonster, "아산공학관 몬스터"),
            (PlayerPrefs.GetInt("HakMoonM", 0), HakMoonMonster, "학문관 몬스터"),
            (PlayerPrefs.GetInt("ECCM", 0), ECCMonster, "ECC 몬스터"),
            (PlayerPrefs.GetInt("HakKwanM", 0), HakKwanMonster, "학관 몬스터")
        };

        foreach (var (value, monsterObj, name) in monsterList)
        {
            if (value == 1)
            {
                monsterObj.SetActive(true);
                MonsterNameText.text = name;
                break;
            }
        }

        RecoverItemNumText.text = (PlayerPrefs.GetInt("IceTino", 0) + PlayerPrefs.GetInt("Cookie", 0)).ToString();

    }
}
