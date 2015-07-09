using UnityEngine;
using System.Collections;

public class Projectil : MonoBehaviour {

    public float speed;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
      
        if (this.transform.position != Vector3.zero)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, Vector3.zero, speed * Time.deltaTime);
        }else
        {
            Destroy(this.gameObject);
        }

	}
}
