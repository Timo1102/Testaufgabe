using UnityEngine;
using System.Collections;

public class Projectil : MonoBehaviour {

    public float speed;
	

	
	//Move to the center of the screen
	void Update () {
        if (!GameManager.instance.IsPlay)
            return;

        if (this.transform.position != Vector3.zero)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, Vector3.zero, speed * Time.deltaTime);
        }else
        {
            Destroy(this.gameObject);
        }

	}
}
