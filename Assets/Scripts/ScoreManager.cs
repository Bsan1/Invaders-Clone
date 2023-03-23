using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static event Action ScoreEvent;
    public TextMeshProUGUI TMPtext;
    public int score = 0;
    public static ScoreManager current;

    void Awake()
    {
        current = this;
        string json1 = File.ReadAllText(Application.dataPath + "/Save1.json");
        ScoreSave ScorePoint = JsonUtility.FromJson<ScoreSave>(json1);
        TMPtext.text = ScorePoint.scoredata.ToString();

    } 

    public void ScoreUp()
    {
        score++;
        TMPtext.text = score.ToString();
    }
}
