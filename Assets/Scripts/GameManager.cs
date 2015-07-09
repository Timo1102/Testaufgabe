using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour {

    //GameManager instance
    public static GameManager instance;

    //UIManager
    public UIManager UI;
    //Controll the Ship
    public PlayerController player;
    //Start Position
    public Vector3 PlayerStartPosition;

    public Enemy prefabEnemy;

    //Alls Enemys on the Screen
    public List<Enemy> activeEnemys = new List<Enemy>();
    //List of SpawnPoints where Enemys can Spawn
    public List<Transform> SpawnPoints = new List<Transform>();

    //Position of the Circle for the Enemys
    public Vector3 InnerCirclePosition;

    //NumberOf Enemys at Start
    public int EnemysAtStart = 2;
    //NumberOf Enemys in the NextRound
    int nextEnemys = 0;
    //Need to Calculate Next Enemys
    int RoundCount = 0;

    
    public ParticleSystem Background;

    //Score Class to store the Name with Points
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

    //List of all saved Scores
    public List<Score> Highscore = new List<Score>();

    
    TextAsset savegame;

    //Enemy Settings
    //Enemys Move Speed from SpawnPoint to the circle
    public int EnemyMoveInSpeed;
    //Enemys Speed in the Circle
    public int EnemySpeed;
    
    public int BombSpeed;
    //Bomb Spawn Time at Start
    public float defaultBombSpawnTime;
    //actual Bomb spawn time
    public float BombSpawnTime;

    //true, if the game is run
    bool _isPlay;
    public bool IsPlay
    {
        get
        {
            return _isPlay;
        }
    }

    //true, if the game is pause
   private bool _isPause;
    
     
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
        private set
        {
            _points = value;
            UI.InGame.GetComponent<InGame>().UpdatePoints();
        }
    }

    //Start Lives
    public int StartLives;
    int _lives;
    public int Lives
    {
        get
        {
            return _lives;
        }
        private set
        {
            _lives = value;
            UI.InGame.UpdateLives();
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
        //Load SaveGame
        LoadSaveGame();
    }

    //Spawn Enemys, when all active Enemys are Destroyed
    //Spawn rate is roundbased ( 2 x 2, 3 x 3, ... )
    IEnumerator SpawnEnemy()
    {
        RoundCount++;
        int rnd = (int)Random.Range(0, SpawnPoints.Count);
        Transform position = SpawnPoints[rnd];
        for(int i = 0;i<nextEnemys; i++)
        {
        Enemy enemyPrefab = Instantiate(prefabEnemy, position.position, Quaternion.identity) as Enemy;
        enemyPrefab.Init(EnemyMoveInSpeed, EnemySpeed, position.position.x < 0 ? 1 : -1);
        yield return new WaitForSeconds(0.1f);
        }
    }



    void Start()
    {
        GameInit();
        Pause();
      //  Play();
       // Next();
    }

    //Set all Init Game states
    void GameInit()
    {
        BombSpawnTime = defaultBombSpawnTime;
        Lives = StartLives;
        nextEnemys = EnemysAtStart;
        RoundCount = 0;
        activeEnemys.Clear();
    }

    //Load Save game
    //Savegame is store in "Assets/Resources"
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
    //Saved savegame
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

    //Add new score, sort and Save
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
    //StartGame
    public void StartGame()
    {
        Next();
        Play();
        UI.StartScreen.enable(false);
        UI.InGame.enable(true);
    }

    //Next Round
    void Next()
    {
        if(RoundCount == nextEnemys)
        {
            nextEnemys++;
            RoundCount = 0;
            BombSpawnTime = defaultBombSpawnTime;
        }
        BombSpawnTime -= 0.2f;
        StartCoroutine("SpawnEnemy");
    }

    //is called, when a enemy is destroyed,
    //if all active are destroyed, start NextRound
    public void EnemyDestroyed(Enemy enemy)
    {
        activeEnemys.Remove(enemy);
        if(activeEnemys.Count == 0)
        {
            Next();
        }
    }

    //Add Points and update the UI
   public void AddPoints(int points)
    {
        this._points += points;
        UI.InGame.GetComponent<InGame>().UpdatePoints();
    }
    //Subtract one Live and update the UI,
    //If zero lives, player is GameOver
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


    //Start GameOver UI and Pause the Game
    void GameOver()
    {
        Pause();
        UI.InGame.enable(false);
        UI.GameOver.enable(true);
    }

    //Reset all Game Settings and Show the StartScreen
    public void Restart()
    {
        foreach(Enemy item in activeEnemys)
        {
            Destroy(item.gameObject);
        }
        foreach(Bomb item in GameObject.FindObjectsOfType<Bomb>())
        {
            Destroy(item.gameObject);
        }

        GameInit();
        Points = 0;
        player.SetPlayer(PlayerStartPosition);
        UI.GameOver.enable(false);
        UI.StartScreen.enable(true);

        
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
