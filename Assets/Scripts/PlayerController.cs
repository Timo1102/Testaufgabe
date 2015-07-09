using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    
    public GameObject Projectil;

    public float rotateSpeed = 8.0f;
    public float ProjectilSpeed = 8.0f;



	// Set Player Position
	void Start () {
        SetPlayer(GameManager.instance.PlayerStartPosition);
	}

    
    public void SetPlayer(Vector3 position)
    {
        this.transform.position = position;
        this.transform.rotation = Quaternion.identity;
    }


	
	//Rotate around center by hitting Left, Richt, A, D. Press Space to shoot
	void Update () {
        if (!GameManager.instance.IsPlay)
            return;

        transform.RotateAround(Vector3.zero, Vector3.forward, rotateSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);
    

        if (Input.GetKeyDown(KeyCode.Space))
        {
           var prefab = Instantiate(Projectil, this.transform.position, Quaternion.identity) as GameObject;
           prefab.GetComponent<Projectil>().speed = ProjectilSpeed;
        }



	}

    //if collide with a bomb
    void OnTriggerEnter(Collider other)
    {
        
        if(other.GetComponent<Bomb>())
        {
            GameManager.instance.SubLive();

        }
    }
}
