using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Recoil : MonoBehaviour
{
    private Ranged _ranged;
    private GunSO _stats;
    private Vector3 _currentRotation;
    private Vector3 _targetRotation;

    private Transform _bulletSpawnPos;

    private void Awake()
    {
        _ranged = GetComponent<Ranged>();
        _stats = _ranged.GetData() as GunSO;
        foreach (Transform child in gameObject.transform)
        {
            if (child.CompareTag($"GunBarrel"))
                _bulletSpawnPos = child.transform;
        }
    }

    private void Update()
    {
        if(_ranged.CurrentState != WeaponState.Equipped) return;
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _stats.ReturnSpeed * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _stats.Snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(_currentRotation);
    }

    public void RecoilFire()
    {
        _targetRotation += new Vector3(0, 0, Random.Range(-_stats.Recoil, _stats.Recoil));
    }
}
