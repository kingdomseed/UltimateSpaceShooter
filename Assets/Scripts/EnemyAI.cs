using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private GameObject _enemyLaserPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    // Update is called once per frame
    void Update () {
        Movement();
        if(Time.time > _nextFire)
        {
            Fire();
        }
	}

    private void Fire()
    {
        _nextFire = Time.time + _fireRate;
        Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0.0f, -0.90f, 0.0f), Quaternion.identity);
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Laser>() || collision.GetComponent<Player>())
        {
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 9.0f, 0.0f);
    }
}
