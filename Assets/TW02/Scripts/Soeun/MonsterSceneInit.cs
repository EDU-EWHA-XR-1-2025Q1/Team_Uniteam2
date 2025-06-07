using UnityEngine;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

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
        var monsterList = new GameObject[] { AsanMonster, HakMoonMonster, ECCMonster, HakKwanMonster };
        AsanMonster.SetActive(false);
        ECCMonster.SetActive(false);
        HakKwanMonster.SetActive(false);
        HakMoonMonster.SetActive(false);

        string currentMonster = PlayerPrefs.GetString("CurrentMonster");
        foreach(var monster in monsterList)
        {
            if (monster.name.Equals(currentMonster))
            {
                monster.SetActive(true);
                break;
            }
        }
        
        string monsterNameTextTmp = currentMonster + "onster";
        MonsterNameText.text = monsterNameTextTmp;
           
        RecoverItemNumText.text = (PlayerPrefs.GetInt("IceTino", 0) + PlayerPrefs.GetInt("Cookie", 0)).ToString();

    }
}
