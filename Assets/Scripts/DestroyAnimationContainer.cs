using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimationContainer : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5.0f;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length * 3.0f);
    }

    private void Update()
    {
        // continue moving the animated death of the enemy
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }
}
