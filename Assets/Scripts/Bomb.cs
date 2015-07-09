using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public Vector3 end;
    int Speed;
	

    //Calculate the way to the border of the Screen from the middle of the screen
	void Start () {
        Vector3 direction = Vector3.zero - this.transform.position;
        direction.Normalize();
        end = this.transform.position - (direction * 20f);
        this.Speed = GameManager.instance.BombSpeed;
	}

    
	
	//Destroy when the gameobject are not visible for the main camera
	void Update () {
        if (!GameManager.instance.IsPlay)
            return;

        if (!this.GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, end, Speed * Time.deltaTime);
        }

	}
}
