using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : InterfaceBase {

    public GameObject Content;

    public GameObject PointText;
    public GameObject HighScoreText;

    public GameObject Name;


   //Show Points
    void OnEnable()
    {
        PointText.GetComponent<Text>().text = GameManager.instance.Points.ToString();
        Content.transform.localPosition = new Vector3(0, 0, 0);
        

    }
    //Set Hightscore and save Points
    public void SetHighScore()
    {
        Content.transform.localPosition = new Vector3(560, 0, 0);
        GameManager.instance.AddScore(Name.GetComponent<Text>().text, GameManager.instance.Points);
        ShowHighscore();
    }

    //Show the Highscore Screen
    void ShowHighscore()
    {
        HighScoreText.GetComponent<Text>().text = "";
        foreach(var item in GameManager.instance.Highscore)
        {
            HighScoreText.GetComponent<Text>().text += item.Name.ToString() + " - " + item.Points.ToString() + "\n";
        }

        
    }




}
