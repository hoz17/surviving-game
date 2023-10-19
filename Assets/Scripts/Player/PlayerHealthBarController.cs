using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBarController : MonoBehaviour {
    private ITakeDamage takeDamage;
    private void Start() {
        takeDamage = Player.Instance.GetComponent<ITakeDamage>();
        takeDamage.OnTakeDamage += TakeDamage_OnTakeDamage;
    }

    private void TakeDamage_OnTakeDamage(object sender, ITakeDamage.OnTakeDamageEventArgs e) {
        Player.Instance.PlayerTakeDamage(e.damage);
        Debug.Log("take damage");
    }
}