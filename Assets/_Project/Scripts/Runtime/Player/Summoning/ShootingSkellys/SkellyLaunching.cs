using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.GraphicsBuffer;

public class SkellyLaunching : MonoBehaviour
{
    #region FIELDS

    [Header("REFERENCES")]
    private Rigidbody _rb;

    public GameObject Skelly;
    public GameObject anchor;

    [Header("MOVEMENT")]
    public float _speed = 15;

    public Axis SkellySpinAxis = Axis.Y;
    public float AnchorHeight = 5;
    public float SkellySpinSpeed = 15;

    [Header("SHOOTING")]
    public int damage = 10;

    public int numberOfEnemiesBeforeDestroy = 1;

    #endregion FIELDS

    #region UNITY METHODS

    public void Awake()
    {
        _rb = anchor.GetComponent<Rigidbody>();
        anchor.transform.parent = null;
    }

    private void FixedUpdate()
    {
        MaintainAnchorHeight();
        extremelySimpleShoot();
        updateSkellyPosition();
    }

    #endregion UNITY METHODS

    #region METHODS

    public void extremelySimpleShoot()
    {
        _rb.velocity = transform.forward * _speed;
    }

    public void SpinSkelly()
    {
        var axisVec = SkellySpinAxis == Axis.X ? Vector3.right : SkellySpinAxis == Axis.Y ? Vector3.up : Vector3.forward;
        Skelly.transform.Rotate(axisVec, SkellySpinSpeed);
    }

    public void updateSkellyPosition()
    {
        Skelly.transform.position = anchor.transform.position;
        SpinSkelly();
    }

    public void MaintainAnchorHeight()
    {
        var anchorPosition = anchor.transform.position;
        anchorPosition.y = AnchorHeight;
        anchor.transform.position = anchorPosition;
    }

    public void HitEnemy(Collider other)
    {
        other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        numberOfEnemiesBeforeDestroy--;
        if (numberOfEnemiesBeforeDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }
}

#endregion METHODS