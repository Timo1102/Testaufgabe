using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : InterfaceBase {

    public Animator ContentAnimation;

    public GameObject PointText;

    public GameObject Name;

    void OnEnable()
    {
        PointText.GetComponent<Text>().text = GameManager.instance.Points.ToString();
    }

    public void SetHighScore()
    {
        ContentAnimation.SetBool("Swipe", true);
    }


}
