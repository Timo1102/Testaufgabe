using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject PrefabBomb;

    bool isInCircle = false;
    public int Points;

    int MoveInSpeed, Speed, CircleDirection;
	// Use this for initialization
	void Start () {
	    
	}
	
    void OnEnable()
    {
        GameManager.instance.activeEnemys.Add(this);
    }

    //Init the Enemy, set Speed, direction
    public void Init(int MoveInSpeed, int Speed, int CircleDirection)
    {
        this.MoveInSpeed = MoveInSpeed;
        this.Speed = Speed;
        this.CircleDirection = CircleDirection;
    }

	//Move to the circle, and then move in the Circle
	void Update () {
        if (!GameManager.instance.IsPlay)
            return;

	    if(!isInCircle)
        {
            MoveIn();
        }else
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, CircleDirection * Speed * Time.deltaTime);
        }


	}

    //Spawn bomb
    void SpawnBomb()
    {
        if(GameManager.instance.IsPlay)
        {
        if (isInCircle)
        {
            Instantiate(PrefabBomb, this.transform.position, Quaternion.identity);
        }
        }
        Invoke("SpawnBomb", SpawnTime());
    }

    //Move from SpawnPoint to Cirlce Position
    void MoveIn()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, GameManager.instance.InnerCirclePosition, MoveInSpeed * Time.deltaTime);
        if (this.transform.position == GameManager.instance.InnerCirclePosition)
        {
            isInCircle = true;
            Invoke("SpawnBomb", SpawnTime());
        }
    }

    //Calculate the time by bombSpawnTime + a range of 0.5 seconds
    float SpawnTime()
    {
        float min = Mathf.Max(0.1f, GameManager.instance.BombSpawnTime - 0.5f);
        float max = Mathf.Max(0.1f, GameManager.instance.BombSpawnTime + 0.5f);


        return Random.Range(min, max);
    }

    //Destroy if hit by a Projectile
    void Hit()
    {
        GameManager.instance.AddPoints(Points);
        GameManager.instance.EnemyDestroyed(this);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Projectil>())
        {
            Hit();
            Destroy(other.gameObject);
        }
    }

}
