using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyController;
using UnityEngine.VFX;

public class EnemyAttack : MonoBehaviour
{
    #region FIELDS

    [Header("Attack Settings")]
    public float attackRange = 1.5f;

    public int attackDamage = 10;
    public float attackCooldown = 1.5f;
    public float knockbackForce = 5f;

    private Transform player;
    private EntityHealth playerHealth;
    private Rigidbody playerRigidbody;
    private float timeSinceLastAttack = 0f;
    private EnemyController enemyController;
    private bool nextAttackIsLeft = true;

    #endregion FIELDS

    #region UNITY METHODS

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<EntityHealth>();
            playerRigidbody = playerObject.GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (enemyController.CurrentState == EnemyState.Chasing && CanAttackPlayer())
        {
            AttackPlayer();
            timeSinceLastAttack = 0f;
        }
    }

    #endregion UNITY METHODS

    #region METHODS

    private bool CanAttackPlayer()
    {
        if (player == null) return false;

        bool isInRange = Vector3.Distance(transform.position, player.position) <= attackRange;
        bool canAttack = timeSinceLastAttack >= attackCooldown;
        bool hasLineOfSight = !Physics.Linecast(transform.position, player.position, LayerMask.GetMask("Default"));

        return isInRange && canAttack && hasLineOfSight;
    }

    private void AttackPlayer()
    {
        enemyController.ChangeState(nextAttackIsLeft ? EnemyState.AttackingL : EnemyState.AttackingR);
        nextAttackIsLeft = !nextAttackIsLeft;

        // Damage the player
        if (playerHealth)
        {
            playerHealth.TakeDamage(attackDamage);
        }

        // Apply knockback to the player
        if (playerRigidbody)
        {
            Vector3 knockbackDirection = (player.position - transform.position).normalized;
            playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
    }

    #endregion METHODS
}