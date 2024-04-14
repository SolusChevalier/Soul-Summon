using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkellyMovement : MonoBehaviour
{
    #region FIELDS

    [Header("REFERENCES")]
    private Rigidbody _rb;

    public GameObject Skelly;
    public GameObject anchor;
    public GameObject targetGO;
    public Rigidbody _target;

    [Header("MOVEMENT")]
    public float _speed = 15;

    public float _rotateSpeed = 95;
    public float AnchorHeight = 5;
    public float SkellySpinSpeed = 5;
    private Vector3 _Heading;

    [Header("AIM ASSIST")]
    public float _maxDistancePredict = 100;

    public float _minDistancePredict = 5;
    public float _maxTimePrediction = 5;
    private Vector3 _standardPrediction, _deviatedPrediction;
    public float _deviationAmount = 50;
    public float _deviationSpeed = 2;

    #endregion FIELDS

    #region UNITY METHODS

    /*public void Start()
    {
        _rb = anchor.GetComponent<Rigidbody>();
        anchor.transform.parent = null;
        _target = targetGO.GetComponent<Rigidbody>();
    }*/

    public void Awake()
    {
        targetGO = GameObject.Find("Player");
        _rb = anchor.GetComponent<Rigidbody>();
        anchor.transform.parent = null;
        _Heading = anchor.transform.forward;
    }

    private void FixedUpdate()
    {
        //_rb.velocity = transform.forward * _speed;

        MaintainAnchorHeight();
        //SimpleShoot();
        extremelySimpleShoot();
        //RotateAnchor();
        updateSkellyPosition();
        //SpinSkelly();
    }

    #endregion UNITY METHODS

    #region METHODS

    private void PredictMovement(float leadTimePercentage)
    {
        var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);

        _standardPrediction = _target.position + _target.velocity * predictionTime;
    }

    private void AddDeviation(float leadTimePercentage)
    {
        var deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, 0);

        var predictionOffset = transform.TransformDirection(deviation) * _deviationAmount * leadTimePercentage;

        _deviatedPrediction = _standardPrediction + predictionOffset;
    }

    private void RotateAnchor()
    {
        var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, Vector3.Distance(anchor.transform.position, _target.position));

        PredictMovement(leadTimePercentage);

        AddDeviation(leadTimePercentage);
        var heading = Vector3.Normalize(_deviatedPrediction - transform.position);
        _rb.velocity = heading * _speed;
        var rotation = Quaternion.LookRotation(heading);
        _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
    }

    public void SimpleShoot()
    {
        var heading = _target.position - transform.position;
        _rb.velocity = heading * _speed;
        var rotation = Quaternion.LookRotation(heading);
        _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
    }

    public void extremelySimpleShoot()
    {
        _rb.velocity = transform.forward * _speed;
    }

    public void MaintainAnchorHeight()
    {
        var anchorPosition = anchor.transform.position;
        anchorPosition.y = AnchorHeight;
        anchor.transform.position = anchorPosition;
    }

    public void SpinSkelly()
    {
        Skelly.transform.Rotate(Vector3.up, SkellySpinSpeed);
    }

    public void SetTarget(Rigidbody target)
    {
        _target = target;
    }

    public void updateSkellyPosition()
    {
        Skelly.transform.position = anchor.transform.position;

        //Skelly.transform.rotation = anchor.transform.rotation;
        SpinSkelly();
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _standardPrediction);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_standardPrediction, _deviatedPrediction);
    }*/

    #endregion METHODS
}