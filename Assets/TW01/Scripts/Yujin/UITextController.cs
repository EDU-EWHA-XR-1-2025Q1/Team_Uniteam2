using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// UIText를 업데이트하는 등 제어하는 컨트롤러 스크립트입니다.
/// </summary>
/// <remarks>
/// Author: 전유진
/// </remarks>
public class UITextController : MonoBehaviour
{
    /// <summary>
    /// ScoreText를 인스펙터창에서 할당합니다.
    /// </summary>
    [SerializeField]
    private TMP_Text ScoreText;

    /// <summary>
    /// 스코어 텍스트 UI를 업데이트합니다.
    /// </summary>
    /// <param name="score">할당할 점수를 입력합니다.</param>
    public void UpdateScoreText(int score)
    {
        if (ScoreText != null)
        {
            ScoreText.text = score.ToString();
        }
        else
        {
            Debug.LogError("ScoreText is not assigned in the inspector.");
        }
    }
}
