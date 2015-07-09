using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : InterfaceBase {

    public Animator ContentAnimation;

    public GameObject PointText;
    public GameObject HighScoreText;

    public GameObject Name;

    void OnEnable()
    {
        PointText.GetComponent<Text>().text = GameManager.instance.Points.ToString();
    }

    public void SetHighScore()
    {
        ContentAnimation.SetBool("Swipe", true);

        GameManager.instance.AddScore(Name.GetComponent<Text>().text, GameManager.instance.Points);
        ShowHighscore();
    }

    void ShowHighscore()
    {
        HighScoreText.GetComponent<Text>().text = "";
        foreach(var item in GameManager.instance.Highscore)
        {
            HighScoreText.GetComponent<Text>().text += item.Name.ToString() + " - " + item.Points.ToString() + "\n";
        }

        
    }


}
