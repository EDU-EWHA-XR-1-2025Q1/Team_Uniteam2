using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 설정에 따라 audio를 재생 / 음소거하는 싱글톤 클래스
/// </summary>
/// <remarks>
/// author : 전유진
/// </remarks>
/// <remarks>
/// 사용법 :
/// 1. 시작 씬에 빈 GameObject를 생성하고, 이름을 AudioManager로 설정합니다.
/// 2. AudioManager 스크립트를 해당 GameObject에 추가합니다.
/// 3. AudioManager 스크립트의 bgmSource 변수에 BGM을 재생할 AudioSource 컴포넌트를 할당합니다.
/// 4. 씬이 시작될 때 AudioManager가 자동으로 BGM을 재생합니다.
/// </remarks>
/// @TODO 홈씬에 AudioManager 오브젝트 추가하기
public class AudioManager : MonoBehaviour
{
    /*-------------------public---------------------------*/

    /*싱글톤 관련 변수들*/
    public static AudioManager Instance { get; private set; }
    
    

    /*-----------------------------------------------------*/
    /*-------------------private---------------------------*/
    /*인스펙터창에 열어주는 변수들*/
    [Header("BGM Audio Source")]
    [SerializeField] private AudioSource bgmSource;
    /*BGM On/Off 상태저장 관련 변수들*/
    private const string PREF_BGM_KEY = "BGM_ONOFF";
    private bool bBgmOn;
    /*BGM Button UI 관련 변수들*/
    private Image bgmButtonImage;
    /*-----------------------------------------------------*/

    // Start is called before the first frame update
    void Start()
    {
        /*-------------------초기화---------------------------*/
        /*싱글톤에 대한 초기화들*/
        if(Instance!=null&&Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        /*-----------------------------------------------------*/

        /*-------------------로직---------------------------*/
        /*BGM On/Off 상태저장 관련 로직들*/
        bBgmOn = PlayerPrefs.GetInt(PREF_BGM_KEY, 1) == 1;
        ApplyBgmState();
        /*BGM Button UI 관련 로직들*/
        SceneManager.sceneLoaded += OnSceneLoaded;
        bgmButtonImage = GameObject.Find("Button_BGM").GetComponent<Image>();
        UpdateBgmButtonColor();
        /*-----------------------------------------------------*/
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*-------------------private---------------------------*/
    /// <summary>
    /// BGM On/Off 상태를 토글하는 함수 (토글 호출용)
    /// </summary>
    public void ToggleBgm()
    {
        bBgmOn = !bBgmOn;
        UpdateBgmButtonColor();
        SaveBgmState();
        ApplyBgmState();
    }


    /// <summary>
    /// 씬이 로드될 때 콜백되는 함수
    /// </summary>
    /// <remarks>
    /// 버튼 색을 bgm on,off 상태에 맞게 업데이트합니다.
    /// </remarks>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().name == "SettingScene")
        {
            //씬 재로드 시 참조 끊기므로, 다시 참조 이어주기
            var buttonObj = GameObject.Find("Button_BGM");
            if (buttonObj != null)
            {
                bgmButtonImage = buttonObj.GetComponent<Image>();
                //버튼의 OnClicked이벤트를 항상 오디오매니저로 연결
                var btn = buttonObj.GetComponent<Button>();
                if (btn != null)
                {
                    //중복방지
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(ToggleBgm);
                }
            }
            else
            {
                bgmButtonImage = null;
            }

            // 씬이 로드될 때마다 버튼 이미지의 컬러를 업데이트합니다.
            UpdateBgmButtonColor();
        }
        
    }


    /*-----------------------------------------------------*/

    /*-------------------private---------------------------*/

    /// <summary>
    /// BGM On/Off 상태에 따라 bgmSource의 음소거 여부를 결정하는 함수
    /// </summary>
    private void ApplyBgmState()
    {
        if(bgmSource==null)
        {
            return;
        }

        if (bBgmOn)
        {
            if(!bgmSource.isPlaying)
            {
                bgmSource.Play();
            }
            bgmSource.volume = 1f;
        }
        else
        {
            bgmSource.Pause();
            bgmSource.volume = 0f;
        }
    }
    /// <summary>
    /// BGM On/Off 상태를 PlayerPrefs에 저장하는 함수
    /// </summary>
    private void SaveBgmState()
    {
        PlayerPrefs.SetInt(PREF_BGM_KEY, bBgmOn ? 1 : 0);
        /*
         Q. set하면 이미 저장된거 아냐? 굳이 save 왜 또 호출해?
         A. PlayerPrefs.SetInt는 값을 저장하는 함수지만, 실제로 저장된 값을 디스크에 기록하는 것은 PlayerPrefs.Save()를 호출해야만 이루어집니다. 프로그램 종료시 Save가 호출되지만, 앱이 비정상종료됐을 경우를 대비해 즉시 Save해줍니다.
         */
        PlayerPrefs.Save();
    }

    /// <summary>
    /// BGM On/Off 상태에 따라 BGM 버튼의 색상을 업데이트하는 함수
    /// </summary>
    private void UpdateBgmButtonColor()
    {
        if (bgmButtonImage)
        {
            if (bBgmOn)
            {
                bgmButtonImage.color = Color.green;
            }
            else
            {
                bgmButtonImage.color = Color.white;
            }
        }
    }


    /*-----------------------------------------------------*/

}
