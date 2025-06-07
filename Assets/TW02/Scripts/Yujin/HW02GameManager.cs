using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HW02GameManager : MonoBehaviour
{
    public static HW02GameManager _Instance { get; private set; }
    private bool isEndingLoaded = false;

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (isEndingLoaded) return;

        // PlayerPrefs에서 각 값들을 가져옴
        int asan = PlayerPrefs.GetInt("Asan", 0);
        int hakKwan = PlayerPrefs.GetInt("HakKwan", 0);
        int hakMoon = PlayerPrefs.GetInt("HakMoon", 0);
        int ecc = PlayerPrefs.GetInt("ECC", 0);
        int asanM = PlayerPrefs.GetInt("AsanM", 0);
        int hakKwanM = PlayerPrefs.GetInt("HakKwanM", 0);
        int hakMoonM = PlayerPrefs.GetInt("HakMoonM", 0);
        int eccM = PlayerPrefs.GetInt("ECCM", 0);

        // 모든 값이 1이면 조건을 열어줌 (딱 1번만 호출)
        if (asan == 1 && hakKwan == 1 && hakMoon == 1 && ecc == 1 &&
            asanM == 1 && hakKwanM == 1 && hakMoonM == 1 && eccM == 1)
        {
            isEndingLoaded = true;
            StartCoroutine(LoadEndingSceneOnce());
        }
    }
    
    private IEnumerator LoadEndingSceneOnce()
    {
        yield return null; // 한 프레임 대기(안전하게)
        SceneManager.LoadScene("EndingScene");
    }
}

