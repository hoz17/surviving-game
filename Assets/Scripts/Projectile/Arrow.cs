using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    private float speed;
    private float damage;
    private Vector3 moveDir;


    public void SetSpeed(float speed) {
        this.speed = speed;
    }
    public void SetDamage(float damage) {
        this.damage = damage;
    }
    public void SetMoveDir(Vector3 moveDir) {
        this.moveDir = moveDir;
    }
    private void FixedUpdate() {
        transform.position += moveDir * speed;
    }
    private void Start() {
        Invoke("DestroyObject", 5f);
    }
    private void DestroyObject() {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Player.Instance.PlayerTakeDamage(damage);
            DestroyObject();
        }
    }
}