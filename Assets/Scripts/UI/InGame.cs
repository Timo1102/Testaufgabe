using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGame : InterfaceBase {


    public GameObject Points;
    public GameObject Lives;

    
    void Start()
    {
        UpdatePoints();
        UpdateLives();
    }

    //Update Points
    public void UpdatePoints()
    {
        Points.GetComponent<Text>().text = GameManager.instance.Points.ToString();
    }
    //Update Lives
    public void UpdateLives()
    {
        Lives.GetComponent<Text>().text = GameManager.instance.Lives.ToString();
    }


}
