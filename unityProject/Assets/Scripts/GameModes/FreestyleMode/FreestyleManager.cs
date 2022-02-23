using UnityEngine;
using UnityEngine.UI;

public class FreestyleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject airplane;
    [SerializeField]
    private PathGenerator pathGenerator;
    [SerializeField]
    private Text scoreText;
    private uint score;
    private float lastTime;
    private float totalTime;

    void Start()
    {
        score = 0;
        lastTime = Time.time;
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();

        float currentTime = Time.time;
        float runTime = (Time.time - lastTime);
        lastTime = currentTime;
        totalTime += runTime;

        scoreText.text = "Score : " + score.ToString() + "\nTime : " + runTime.ToString("0.00") + "\nMean time : " + (totalTime / score).ToString("0.00");

        // Increment difficulty
        pathGenerator.maxNextGenerationAngle += 0.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Gate")
        {
            other.GetComponent<BoxCollider>().enabled = false;
            IncrementScore();
            pathGenerator.GenerateNextGate();
        }
    }
}
