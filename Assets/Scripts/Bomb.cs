using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public Vector3 end;
    int Speed;
	// Use this for initialization
	void Start () {
        Vector3 direction = Vector3.zero - this.transform.position;
        direction.Normalize();
        end = this.transform.position - (direction * 20f);
        this.Speed = GameManager.instance.BombSpeed;
	}


	
	// Update is called once per frame
	void Update () {
       
        if (!this.GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, end, Speed * Time.deltaTime);
        }

	}
}
