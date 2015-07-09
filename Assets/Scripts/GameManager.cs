using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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

    public class Score
    {
        public Score(string name, int score)
        {
            this.Name = name;
            this.Points = score;
        }

        public string Name;
        public int Points;
    }


    public List<Score> Highscore = new List<Score>();

    TextAsset savegame;

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

        LoadSaveGame();
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


    void LoadSaveGame()
    {
        savegame = Resources.Load("save") as TextAsset;
        string[] lines = savegame.text.Split("\n"[0]);
        foreach (string line in lines)
        {
            if(line != "")
            {

            
            string[] split = line.Split("-"[0]);
            Highscore.Add(new Score(split[0], int.Parse(split[1])));
            }
        }


    }

    void SaveSaveGame()
    {
         savegame = Resources.Load("save") as TextAsset;
         StreamWriter sw = new StreamWriter("Assets/Resources/save.txt");
        
        foreach(Score item in Highscore)
        {
            sw.WriteLine(item.Name + " - " + item.Points);
        }

        sw.Flush();
        sw.Close();


    }

    public void AddScore(string name, int Score)
    {
        Highscore.Add(new Score(name, Score));

        Highscore.Sort(
            delegate(Score x, Score y)
            {
                return y.Points.CompareTo(x.Points);
            }
            );
        SaveSaveGame();
        

        
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
