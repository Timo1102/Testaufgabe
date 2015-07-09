using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public PlayerController player;



    bool _isPlay;
    public bool IsPlay
    {
        get
        {
            return _isPlay;
        }
    }

    bool _isPause;
    public bool IsPause
    {
        get
        {
            return _isPause;
        }
    }


    int _points;
    public int Points
    {
        get
        {
            return _points;
        }
    }

    public int StartLives;
    int _lives;
    public int Lives
    {
        get
        {
            return _lives;
        }
    }



    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        if(instance != this)
        {
            if (this != instance)
                Destroy(this.gameObject);
        }
        
    }

    void Start()
    {
        _lives = StartLives;
    }

   public void AddPoints(int points)
    {
        this._points += points;
    }

   public void SubLive()
    {
        this._lives -= 1;
        if(_lives == 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {

    }

    public void Pause()
    {
        _isPlay = false;
        _isPause = true;
    }

    public void Play()
    {
        _isPlay = true;
        _isPause = false;
    }


}
