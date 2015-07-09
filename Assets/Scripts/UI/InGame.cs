using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGame : InterfaceBase {

	// Use this for initialization
    public GameObject Points;
    public GameObject Lives;

    void Start()
    {
        UpdatePoints();
        UpdateLives();
    }

    public void UpdatePoints()
    {
        Points.GetComponent<Text>().text = GameManager.instance.Points.ToString();
    }

    public void UpdateLives()
    {
        Lives.GetComponent<Text>().text = GameManager.instance.Lives.ToString();
    }


}
