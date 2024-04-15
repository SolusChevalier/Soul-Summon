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

    public Transform player;
    public EntityHealth playerHealth;
    private float timeSinceLastAttack = 0f;
    private EnemyController enemyController;
    private bool nextAttackIsLeft = true;

    #endregion FIELDS

    #region UNITY METHODS

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();

        GameObject[] playerObject = GameObject.FindGameObjectsWithTag("Player");
        foreach (var pl in playerObject)
        {
            if (pl.GetComponent<EntityHealth>())
            {
                player = pl.transform;
                playerHealth = pl.GetComponent<EntityHealth>();
            }
        }
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        //Debug.Log("" + (enemyController.CurrentState == EnemyState.Chasing) + " : " + (CanAttackPlayer()));
        if (enemyController.CurrentState == EnemyState.Chasing && CanAttackPlayer())
        {
            AttackPlayer();
            //Debug.Log("Attacking player");
            timeSinceLastAttack = 0f;
        }
    }

    #endregion UNITY METHODS

    #region METHODS

    private bool CanAttackPlayer()
    {
        //Debug.Log("Checking Attack");
        if (player == null)
        {
            //Debug.Log("player doesnt exist");
            return false;
        }
        //Debug.Log("player does exist");
        bool isInRange = Vector3.Distance(transform.position, player.position) <= attackRange;
        //Debug.Log("Is In Range: " + (isInRange));
        bool canAttack = timeSinceLastAttack >= attackCooldown;
        //Debug.Log("Can Attack: " + (canAttack));
        //bool hasLineOfSight = !Physics.Linecast(transform.position, player.position, LayerMask.GetMask("Default"));
        //Debug.Log("Has Line Of Sight: " + (hasLineOfSight));

        //return isInRange && canAttack && hasLineOfSight;
        return isInRange && canAttack;
    }

    private void AttackPlayer()
    {
        enemyController.ChangeState(nextAttackIsLeft ? EnemyState.AttackingL : EnemyState.AttackingR);
        nextAttackIsLeft = !nextAttackIsLeft;
        playerHealth.TakeDamage(attackDamage);
    }

    #endregion METHODS
}