﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    



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

    void AddPoints(int points)
    {
        this._points += points;
    }

    void SubLive()
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

    void Pause()
    {
        _isPlay = false;
        _isPause = true;
    }

    void Play()
    {
        _isPlay = true;
        _isPause = false;
    }


}