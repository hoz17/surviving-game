using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameControl : MonoBehaviour {
    [SerializeField] private List<Transform> entityList;

    private const int mobCap = 50;
    private int mobCount = 0;
    private Vector2 spawnDir;
    private WaitForSeconds spawnTimer = new(.5f);
    private bool canSpawn = true;

    private DateTime startTime;
    private DateTime endTime;
    private TimeSpan duration;

    public static GameControl Instance { private set; get; }

    private enum GameState {
        GamePlaying,
        GameOver,
        GamePaused
    }
    private GameState gameState;
    public event EventHandler OnGameStateChanged;
    public event EventHandler OnGamePaused;
    private void Start() {
        startTime = DateTime.Now;
    }
    private void Awake() {
        Instance = this;
    }
    private void FixedUpdate() {
        switch (gameState) {
            case GameState.GamePlaying:
                if (Player.Instance.GetHealth() <= 0) {
                    //GameOver
                    gameState = GameState.GameOver;
                    break;
                }

                if (mobCount < mobCap && canSpawn) {
                    StartCoroutine(Spawn());
                }
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                endTime = DateTime.Now;
                duration = endTime - startTime;
                OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                break;
        }


    }
    IEnumerator Spawn() {
        spawnDir.x = UnityEngine.Random.Range(-180, 180);
        spawnDir.y = UnityEngine.Random.Range(-180, 180);
        spawnDir.Normalize();
        int index = UnityEngine.Random.Range(0, entityList.Count);
        Instantiate(entityList[index],
            Player.Instance.transform.position + (Vector3)spawnDir * 7,
            transform.rotation);
        mobCount++;
        canSpawn = false;
        yield return spawnTimer;
        canSpawn = true;
    }
    public bool IsGameOver() {
        return gameState == GameState.GameOver;
    }
    public string GetPlayedTime() {
        return $"{duration.Minutes}:{duration.Seconds:00}";
    }
}

