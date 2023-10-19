using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Attack : MonoBehaviour {
    [SerializeField] private Entity entity;
    [SerializeField] private GameObject projectile;
    private Quaternion rotation;
    private bool canAttack = true;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Player.Instance.PlayerTakeDamage(5);
            Debug.Log(Player.Instance.GetHealth());
        }
    }
    private void FixedUpdate() {
        if (entity.GetIsInRange() && canAttack) {
            StartCoroutine(AttackPlayer());
        }
    }
    IEnumerator AttackPlayer() {
        // Attack function
        float angle = Mathf.Atan2(entity.GetPlayerDirection().y, entity.GetPlayerDirection().x) * Mathf.Rad2Deg;
        rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        GameObject arrow = Instantiate(projectile, entity.transform.position, rotation);
        arrow.GetComponent<Arrow>().SetDamage(entity.GetAttackDamage());
        arrow.GetComponent<Arrow>().SetSpeed(.1f);
        arrow.GetComponent<Arrow>().SetMoveDir(entity.GetPlayerDirection());
        canAttack = false;
        yield return new WaitForSeconds(entity.getAttackCoolDown());
        canAttack = true;
    }
}