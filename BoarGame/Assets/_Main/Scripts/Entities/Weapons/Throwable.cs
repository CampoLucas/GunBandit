using UnityEngine;

public class Throwable : MonoBehaviour, IThrowable
{
    private WeaponSO _stats;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _stats = GetComponent<IWeapon>().GetData() as WeaponSO;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Throw()
    {
        transform.parent = null;
        if (_stats != null) _rigidbody.AddForce(transform.up * _stats.ThrowStrength, ForceMode2D.Impulse);
    }
}
