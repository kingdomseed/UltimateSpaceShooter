﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    private AudioClip _clip;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1.0f);
    }

    void Update()
    {
        // move up at 10 speed
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // destroy enemy ship and this object
        if (collision.GetComponent<EnemyAI>())
        {
            Destroy(gameObject);
        }

    }
}
