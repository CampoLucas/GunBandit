using System;
using UnityEngine;

public class Throwable : MonoBehaviour, IThrowable
{
    private WeaponSO _stats;
    private Rigidbody2D _rigidbody;
    private ISwapState _swap;

    private void Awake()
    {
        _stats = GetComponent<Weapon2>().GetData() as WeaponSO;
        _rigidbody = GetComponent<Rigidbody2D>();
        _swap = GetComponent<ISwapState>();
    }

    private void FixedUpdate()
    {
        var rbVelocity = new Vector2(Mathf.Abs(_rigidbody.velocity.x), Mathf.Abs(_rigidbody.velocity.y));
        if(_swap.CurrentState == WeaponState.Thrown && Vector2.Distance(rbVelocity ,Vector2.zero) < 0.4f)
            _swap.ChangeState(WeaponState.Pickable);
    }

    public void Throw()
    {
        if (_stats != null) _rigidbody.AddForce(transform.up * _stats.ThrowStrength, ForceMode2D.Impulse);
    }
}
