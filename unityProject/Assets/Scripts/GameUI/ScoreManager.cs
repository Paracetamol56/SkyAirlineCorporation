using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text textScore;

    // TODO : mettre en place un syst�me de sauvegarde du score
    private int score = 0;

    private static ScoreManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("ScoreManager d�j� existant...");
            return;
        }

        instance = this;
    }

    private void Update()
    {
        textScore.text = "Score : " + score.ToString();
    }

    public ScoreManager GetInstance()
    {
        return instance;
    }

    public int GetScore
    {
        get { return score; }
        set
        {
            if (value < 0)
                score = 0;
            else
                score = value;
        }
    }
}
