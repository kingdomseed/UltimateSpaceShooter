using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyShip;
    [SerializeField]
    private GameObject[] _powerUps;

    [SerializeField]
    private GameObject _playerReference;

    [SerializeField]
    private float _enemySpawnRate = 5.0f;

    [SerializeField]
    private GameManager _gameManager;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyCoroutine());
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
            while (_gameManager.IsGameStarted())
            {
                Instantiate(_enemyShip, new Vector3(Random.Range(-8.0f, 8.0f), 9.0f, 0.0f), Quaternion.identity);
                yield return new WaitForSeconds(_enemySpawnRate);
            }
    }

    private IEnumerator SpawnPowerUps()
    {
        while (_gameManager.IsGameStarted())
        {
            yield return new WaitForSeconds(Random.Range(10.0f, 21.0f));
            Instantiate(_powerUps[Random.Range(0,3)], new Vector3(Random.Range(-8.0f, 8.0f), 9.0f, 0.0f), Quaternion.identity);
        }
    }
}
