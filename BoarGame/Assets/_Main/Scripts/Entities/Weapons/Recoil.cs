using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Recoil : MonoBehaviour
{
    private Ranged _ranged;
    private Vector3 _currentRotation;
    private Vector3 _targetRotation;

    [SerializeField] private Transform bulletPos;
    [SerializeField] private float recoil;

    [Header("Settings")] 
    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    private void Awake()
    {
        _ranged = GetComponent<Ranged>();
    }

    private void Update()
    {
        if(_ranged.CurrentState != WeaponState.Equipped) return;
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, snappiness * Time.fixedDeltaTime);
        bulletPos.localRotation = Quaternion.Euler(_currentRotation);
    }

    public void RecoilFire()
    {
        _targetRotation += new Vector3(0, 0, Random.Range(-recoil, recoil));
    }
}
