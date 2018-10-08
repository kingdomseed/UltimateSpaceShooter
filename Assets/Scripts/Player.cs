using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private GameObject _shieldsGameObject;
    [SerializeField]
    private GameObject _playerExplosionPrefab;
    [SerializeField]
    private AudioClip _powerUpClip;

    private UIManager _uiManager;
    private GameManager _gameManager;

    [SerializeField]
    private bool _canTripleShot = false;
    [SerializeField]
    private bool _canSpeedBoost = false;
    [SerializeField]
    private bool _isShielded = false;

    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _speedBoost = 2.0f;

    [SerializeField]
    private float _fireRate = 0.5f;

    // Modified by Time.time
    private float _nextFire = 0.0f;

    [SerializeField]
    private int _playerLives = 3;

    private void Awake()
    {
        if(FindObjectOfType<GameManager>())
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
        if(FindObjectOfType<UIManager>())
        {
            _uiManager = FindObjectOfType<UIManager>();
        }
    }

    private void Update()
    {
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
        if (_canTripleShot)
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(_tripleShot, transform.position, Quaternion.identity);
        }
        else
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
        if (_canSpeedBoost)
        {
            transform.Translate(Vector3.right * Time.deltaTime * (_speed * _speedBoost) * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * (_speed * _speedBoost) * verticalInput);
        }
        else
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
        if (collision.GetComponent<PowerUp>())
        {
            ActivatePowerUp(collision);
        }
        if (collision.GetComponent<EnemyAI>() || collision.GetComponent<EnemyLaser>())
        {
            DamagePlayer();
        }
    }

    private void DamagePlayer()
    {
        if (_isShielded)
        {
            _isShielded = false;
            _shieldsGameObject.SetActive(false);
        }
        else
        {
            _playerLives--;
            _uiManager.UpdateLives(_playerLives);
            if (_playerLives < 1)
            {
                _gameManager.GameOver();
                PlayerIsDestroyed();
            }
        }
    }

    private void ActivatePowerUp(Collider2D collision)
    {
        PowerUp powerUp = collision.GetComponent<PowerUp>();
        AudioSource.PlayClipAtPoint(_powerUpClip, Camera.main.transform.position, 1.0f);
        if (powerUp.getPowerUpId() == 0)
        {
            _canTripleShot = true;
            StartCoroutine(TripleShotPowerDownRoutine());
        }
        else if (powerUp.getPowerUpId() == 1)
        {
            _canSpeedBoost = true;
            StartCoroutine(SpeedBoostPowerDownRoutine());
        }
        else if (powerUp.getPowerUpId() == 2)
        {
            _isShielded = true;
            _shieldsGameObject.SetActive(true);
        }
        Destroy(collision.gameObject);
    }

    private void PlayerIsDestroyed()
    {
        Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _canTripleShot = false;
    }

    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(6.0f);
        _canSpeedBoost = false;
    }

 
}
