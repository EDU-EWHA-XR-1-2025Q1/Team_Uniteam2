using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController_Home : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("InGameRoadScene");
    }

    public void LoadMyPageScene()
    {
        SceneManager.LoadScene("MyPageScene");
    }

    public void LoadMapScene()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void LoadSettingScene()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void LoadHomeScene()
    {
        SceneManager.LoadScene("HomeScene");
    }
}