using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임매니저 클래스입니다. 게임의 전반적인 상태를 관리합니다.
/// 게임 내 유일무이한 클래스이므로 싱글톤으로 관리합니다.
/// </summary>
/// <remarks>
/// Author: 전유진
/// </remarks>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// PausePanel입니다. 인스펙터창에서 할당합니다.
    /// </summary>
    [SerializeField]
    private GameObject PausePanelPrefab;

    /// <summary>
    /// PausePanel의 인스턴스를 저장합니다.
    /// </summary>
    private GameObject _pausePanelInstance;

    /// <summary>
    /// 싱글톤 인스턴스를 저장할 필드
    /// </summary>
    private static GameManager _instance;

    /// <summary>
    /// 싱글톤 인스턴스를 외부에서 접근할 수 있도록 제공하는 프로퍼티
    /// </summary>
    public static GameManager Instance 
    {
        get {return _instance; }
        private set
        {
            _instance = value;
        }
    }

    private void Awake()
    {

        if (_instance == null)
        {
            //현재 인스턴스를 저장
            _instance = this;
            //씬이 바뀌어도 파괴되지 않도록 설정  
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //중복된 GameManager가 존재하면 현재 인스턴스를 파괴.
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //esc키를 누를 경우 esc패널을 토글합니다.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausePanel();
        }
    }

    /// <summary>
    /// pausepanel을 토글합니다.
    /// </summary>
    private void TogglePausePanel()
    {
        //PausePanel 인스턴스가 없으면 생성
        if (_pausePanelInstance == null)
        {
            _pausePanelInstance = Instantiate(PausePanelPrefab);
        

            //캔버스할당 //FindObjectOfType<T>: 씬 내에서 특정 컴포넌트 찾을 때 사용 
            Canvas ExistingCanvas = FindObjectOfType<Canvas>();
            if (ExistingCanvas != null)
            {
                //캔버스 이미 있으면 자식으로 할당
                _pausePanelInstance.transform.SetParent(ExistingCanvas.transform, false);
            }
            else
            {
                //Canvas없으면 새로 생성
                GameObject NewCanvas = new GameObject("PauseCanvas");
                Canvas CanvasComponent = NewCanvas.AddComponent<Canvas>();
                CanvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
                CanvasScaler CanvasScaler = NewCanvas.AddComponent<CanvasScaler>();
                CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                CanvasScaler.referenceResolution = new Vector2(1920, 1080);
                NewCanvas.AddComponent<GraphicRaycaster>();
                //새로 생성한 canvas의 자식으로 설정
                _pausePanelInstance.transform.SetParent(NewCanvas.transform, false);
            }

            //RectTransform 위치 초기화
            RectTransform RectTransform = _pausePanelInstance.GetComponent<RectTransform>();
            if (RectTransform != null)
            {
                RectTransform.anchoredPosition = Vector2.zero;
            }

            //처음에는 비활성화상태
            _pausePanelInstance.SetActive(false);
        }

        //PausePanel 활성화 / 비활성화 토글
        if (_pausePanelInstance != null)
        {
            _pausePanelInstance.SetActive(!_pausePanelInstance.activeSelf);
        }

        //점수 업데이트
        UITextController UITextController = _pausePanelInstance.GetComponentInChildren<UITextController>();
        if (UITextController != null)
        {
            UITextController.UpdateScoreText(PlayerPrefs.GetInt("Score", 0));
        }    
    }
}
