using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;
    

    private void Start () {
        
        // Set starting position of player
        transform.position = new Vector3(0, 0, 0);
	}
	
	private void Update () {
        Movement();
        if (Input.GetButtonDown("Fire1") && Time.time > _nextFire)
        {
            Shoot();
        }
    }

    // Player fires using Fire1. The rate is limited by comparing the time in seconds since the game started
    // with the nextFire variable. NextFire contains the time since the game started plus the fireRate added
    // to it. Between each fire, add half a second to nextFire and wait for Time.time to catch up.
    private void Shoot()
    {
        _nextFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0.0f, 0.90f, 0.0f), Quaternion.identity);
    }

    private void Movement()
    {
        // Declare and assign control axis float variables which will range between -1 and 1.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Based on input variables float values (between -1 and 1), determine which way the player ship should move.
        transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
        transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);

        // Setting bounds of level for player's y position
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        // Setting bounds of level for player's x position
        // On the X axis, the PlayerShip can wrap from one side of the screen to the other
        if (transform.position.x > 9.4)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.4f)
        {
            transform.position = new Vector3(9.4f, transform.position.y, 0);
        }
    }
}
