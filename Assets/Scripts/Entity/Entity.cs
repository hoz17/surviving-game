using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Entity : MonoBehaviour, IHealthPoint {
    protected float healthPoint;
    [SerializeField] protected float maxHealthPoint;
    [SerializeField] protected float attackDammage;
    [SerializeField] protected float defence;
    [SerializeField] protected float levelMultiplier;
    [SerializeField] protected float speed;
    [SerializeField] protected float xpValue;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackCoolDown;

    [SerializeField] protected bool canMove;
    [SerializeField] protected bool isInRange;

    protected CircleCollider2D circleCollider;
    protected BoxCollider2D boxCollider2D;
    protected Vector3 moveDir;
    protected bool boxCastHit;
    [SerializeField] protected LayerMask layerMask;

    public event EventHandler<IHealthPoint.OnHealthChangedEventArgs> OnHealthChanged;

    private void Update() {
        moveDir = (Vector3)GetPlayerDirection();
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, moveDir, speed * Time.deltaTime, layerMask);
        foreach (RaycastHit2D hit in hits) {
            if (hit.collider is BoxCollider2D) {
                boxCastHit = true;
                break;
            } else boxCastHit = false;
        }
        if (canMove && !boxCastHit) {
            ApproachPlayer();
        }
    }
    protected virtual void Start() {
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {

            canMove = false;
            isInRange = true;

        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (this.GetComponent<Collider2D>() is CircleCollider2D) {
                canMove = true;
                isInRange = false;
            }
        }
    }

    private void Awake() {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = attackRange;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    public void ApproachPlayer() {
        transform.position += speed * Time.deltaTime * moveDir;
    }
    public Vector2 GetPlayerDirection() {
        Vector3 output = Player.Instance.transform.position - transform.position;
        return (Vector2)output.normalized;
    }
    public float getAttackCoolDown() {
        return attackCoolDown;
    }
    public void setAttackCoolDown(float time) {
        attackCoolDown = time;
    }
    public float getAttackRange() {
        return attackRange;
    }
    public bool GetIsInRange() {
        return isInRange;
    }
    public void SetIsInRange(bool isInRange) {
        this.isInRange = isInRange;
    }
    public float GetAttackDamage() {
        return attackDammage;
    }
    public Vector3 GetMoveDir() {
        return moveDir;
    }

}
