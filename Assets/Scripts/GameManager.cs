using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public UIManager UI;
    public PlayerController player;
    public Vector3 PlayerStartPosition;

    public Enemy prefabEnemy;

    public List<Enemy> activeEnemys = new List<Enemy>();
    public List<Transform> SpawnPoints = new List<Transform>();

    public Vector3 InnerCirclePosition;

    public int EnemysAtStart = 2;
    int RoundCount = 0;

    public ParticleSystem Background;


    //Enemy Settings
    public int EnemyMoveInSpeed;
    public int EnemySpeed;
    public int BombSpeed;
    public float defaultBombSpawnTime;
    public float BombSpawnTime;


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


    int _points = 0;
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

    IEnumerator SpawnEnemy()
    {
        RoundCount++;
        int rnd = (int)Random.Range(0, SpawnPoints.Count);
        Transform position = SpawnPoints[rnd];
        for(int i = 0;i<EnemysAtStart; i++)
        {

        

        Enemy enemyPrefab = Instantiate(prefabEnemy, position.position, Quaternion.identity) as Enemy;
        enemyPrefab.Init(EnemyMoveInSpeed, EnemySpeed, position.position.x < 0 ? 1 : -1);
        yield return new WaitForSeconds(0.1f);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Pause();
        }
    }

    void Start()
    {
        BombSpawnTime = defaultBombSpawnTime;
        _lives = StartLives;
        Pause();
      //  Play();
       // Next();
    }

    public void StartGame()
    {
        Next();
        Play();
        UI.StartScreen.enable(false);
        UI.InGame.enable(true);
    }

    void Next()
    {
        if(RoundCount == EnemysAtStart)
        {
            EnemysAtStart++;
            RoundCount = 0;
            BombSpawnTime = defaultBombSpawnTime;
        }
        BombSpawnTime -= 0.2f;
        StartCoroutine("SpawnEnemy");
    }

    public void EnemyDestroyed(Enemy enemy)
    {
        activeEnemys.Remove(enemy);
        if(activeEnemys.Count == 0)
        {
            Next();
        }
    }

   public void AddPoints(int points)
    {
        this._points += points;
        UI.InGame.GetComponent<InGame>().UpdatePoints();
    }

   public void SubLive()
    {
        this._lives -= 1;
        UI.InGame.GetComponent<InGame>().UpdateLives();
        if(_lives == 0)
        {
            GameOver();
        }else
        {
            player.SetPlayer(PlayerStartPosition);
        }
    }



    void GameOver()
    {
        Pause();
        UI.InGame.enable(false);
        UI.GameOver.enable(true);
    }

    public void Pause()
    {
        Background.Pause();
        _isPlay = false;
        _isPause = true;
    }

    public void Play()
    {
        Background.Play();
        _isPlay = true;
        _isPause = false;
        
    }


}
