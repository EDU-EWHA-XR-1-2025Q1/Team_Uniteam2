using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelectController : MonoBehaviour
{
    public GameObject StudentNumInputPanel;
    public GameObject ModeSelectPanel;
    public GameObject QuizPanel;

    public void Alone()
    {
        ModeSelectPanel.SetActive(false);
        QuizPanel.SetActive(true);
    }

    public void Together()
    {
        ModeSelectPanel.SetActive(false);
        StudentNumInputPanel.SetActive(true);
    }
}
