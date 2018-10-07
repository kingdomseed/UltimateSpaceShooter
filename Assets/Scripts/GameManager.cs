using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool isGameStarted = false;

    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private SpawnManager _spawnManager;

    private UIManager _uiManager;

    private void Awake()
    {
        if (FindObjectOfType<UIManager>())
        {
            _uiManager = FindObjectOfType<UIManager>();
        }
    }

    private void Update () {
        if (Input.GetButtonDown("Fire1") && !isGameStarted)
        {
            StartGame();
        }
	}

    private void StartGame()
    {
        isGameStarted = true;
        _spawnManager.StartSpawning();
        Instantiate(_playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        _uiManager.SetTitleScreen(false);
        _uiManager.UpdateLives(3);
        _uiManager.SetInitialScore();
    }

    public void GameOver()
    {
        isGameStarted = false;
        _uiManager.SetTitleScreen(true);
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        PowerUp[] powerUps = FindObjectsOfType<PowerUp>();
        foreach (EnemyAI enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        foreach (PowerUp powerUp in powerUps)
        {
            Destroy(powerUp.gameObject);
        }
    }

    public bool IsGameStarted()
    {
        return isGameStarted;
    }
}
