using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {
    [SerializeField] private GameObject entity;
    [SerializeField] private Image healthBarImage;
    private IHealthPoint healthPoint;
    private void Start() {
        healthPoint = entity.GetComponent<IHealthPoint>();
        healthPoint.OnHealthChanged += HealthBarUI_OnHealthChanged;
        Hide();
    }

    private void HealthBarUI_OnHealthChanged(object sender, IHealthPoint.OnHealthChangedEventArgs e) {
        healthBarImage.fillAmount = e.health;
        if (e.health == 0f || e.health == 1f) {
            Hide();
        } else {
            Show();
        }
    }
    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}