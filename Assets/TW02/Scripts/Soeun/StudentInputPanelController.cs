using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentInputPanelController : MonoBehaviour
{
    public GameObject StudentInputPanel;
    public GameObject QuizPanel;

    public void Enter()
    {
        StudentInputPanel.SetActive(false);
        QuizPanel.SetActive(true);
    }
}
