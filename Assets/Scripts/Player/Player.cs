using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHealthPoint {
    private InputHandler inputHandler;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxHealth = 100;
    private float health;

    public event EventHandler<IHealthPoint.OnHealthChangedEventArgs> OnHealthChanged;

    public static Player Instance { get; private set; }
    private void Awake() {
        Instance = this;
        inputHandler = new InputHandler();
        inputHandler.Enable();
    }
    private void Update() {
        transform.position += speed * Time.deltaTime * (Vector3)GetVectorNormalized();

    }
    public Vector2 GetVectorNormalized() {
        Vector2 inputVector = inputHandler.PlayerMovement.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
    private void Start() {
        health = maxHealth;
    }

    public void PlayerTakeDamage(float damage) {
        health -= damage;
        OnHealthChanged?.Invoke(this, new IHealthPoint.OnHealthChangedEventArgs {
            health = health / maxHealth
        });
    }
    public float GetHealth() {
        return health;
    }
}
