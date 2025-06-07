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
        QuizPanel.SetActive(true);
        ModeSelectPanel.SetActive(false);
    }

    public void Together()
    {
        StudentNumInputPanel.SetActive(true);
        ModeSelectPanel.SetActive(false);
    }
}
