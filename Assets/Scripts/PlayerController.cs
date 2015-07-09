using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float rotateSpeed = 8.0f;

	// Use this for initialization
	void Start () {
	    
	}


    void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, rotateSpeed * Input.GetAxis("Horizontal"));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
