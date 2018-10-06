using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour {

    [SerializeField]
    private float _speed = 10.0f;

    void Update()
    {
        // move up at 10 speed
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
