using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private bool canTripleShot = false;
    [SerializeField]
    private bool canSpeedBoost = false;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShot;

    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _speedBoost = 2.0f;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;

    [SerializeField]
    private int _playerHealth = 5;
    [SerializeField]
    private GameObject _playerExplosionPrefab;

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
        if(canTripleShot)
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(_tripleShot, transform.position, Quaternion.identity);
        } else
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position + new Vector3(0.0f, 0.90f, 0.0f), Quaternion.identity);
        }
    }

    private void Movement()
    {
        // Declare and assign control axis float variables which will range between -1 and 1.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Based on input variables float values (between -1 and 1), determine which way the player ship should move.
        if(canSpeedBoost)
        {
            transform.Translate(Vector3.right * Time.deltaTime * (_speed * _speedBoost) * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * (_speed * _speedBoost) * verticalInput);
        } else
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);
        }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PowerUp>())
        {
            PowerUp powerUp = collision.GetComponent<PowerUp>();
            if (powerUp.getPowerUpId() == 0)
            {
                canTripleShot = true;
                StartCoroutine(TripleShotPowerDownRoutine());
            }
            else if (powerUp.getPowerUpId() == 1)
            {
                canSpeedBoost = true;
                StartCoroutine(SpeedBoostPowerDownRoutine());
            }
            else if (powerUp.getPowerUpId() == 2)
            {

            }
            Destroy(collision.gameObject);
        }
        if(collision.GetComponent<EnemyAI>() || collision.GetComponent<EnemyLaser>())
        {
            _playerHealth--;
            if(_playerHealth < 1)
            {
                PlayerIsDestroyed();
            }
        }
    }

    private void PlayerIsDestroyed()
    {
        Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(6.0f);
        canSpeedBoost = false;
    }
}
