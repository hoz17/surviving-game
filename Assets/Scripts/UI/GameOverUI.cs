using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private TextMeshProUGUI timeSurvivedTMP;

    private void Awake() {
        playAgainButton.onClick.AddListener(() => {
            ReloadCurrentScene();
            Time.timeScale = 1.0f;
        });
    }
    private void ReloadCurrentScene() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    private void Start() {
        GameControl.Instance.OnGameStateChanged += GameOverUI_OnGameStateChanged;
        Hide();
    }

    private void GameOverUI_OnGameStateChanged(object sender, EventArgs e) {
        if (GameControl.Instance.IsGameOver()) {
            Show();
            timeSurvivedTMP.SetText(GameControl.Instance.GetPlayedTime());
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}