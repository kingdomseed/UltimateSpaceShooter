using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    private float _speed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // move up at 10 speed
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
	}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
