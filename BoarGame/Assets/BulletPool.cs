using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet prefab;
    [SerializeField] private int initialSize = 10;

    private readonly Queue<Bullet> _pool = new Queue<Bullet>();

    private BulletPool _bulletPool;
    private SwapState _swapState;

    private void Awake()
    {
        _swapState = GetComponent<SwapState>();

        // Solo crear pool si no está en estado Pickable
        if (_swapState == null || _swapState.CurrentState != WeaponState.Pickable)
        {
            _bulletPool = GetComponentInChildren<BulletPool>();

            if (_bulletPool == null)
            {
                Debug.LogWarning("No se encontró el BulletPool en el arma equipada.");
            }
        }
    }


    private void Start()
    {
        for (int i = 0; i < initialSize; i++)
        {
            AddBulletToPool();
        }
    }

    private Bullet AddBulletToPool()
    {
        if (prefab == null)
        {
            Debug.LogError("Bullet prefab no asignado en el BulletPool.");
            return null;
        }

        Bullet bullet = Instantiate(prefab, transform);
        bullet.gameObject.SetActive(false);
        bullet.SetPool(this);
        _pool.Enqueue(bullet);
        return bullet;
    }
    public Bullet GetBullet()
    {
        if (_pool.Count == 0)
            AddBulletToPool();

        Bullet bullet = _pool.Dequeue();
        bullet.gameObject.SetActive(true);
        return bullet;
    }

    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        _pool.Enqueue(bullet);
    }
}
