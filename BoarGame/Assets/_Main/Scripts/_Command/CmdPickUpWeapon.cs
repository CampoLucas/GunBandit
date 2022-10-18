using UnityEngine;

public class CmdPickUpWeapon : ICommand
{
    private readonly Weapon2 _weapon;
    private readonly Transform _transform;
    private readonly Transform _handTransform;

    public CmdPickUpWeapon(Weapon2 weapon, Transform transform, Transform handTransform)
    {
        _weapon = weapon;
        _transform = transform;
        _handTransform = handTransform;
    }
    
    public void Do()
    {
        var item = _weapon.gameObject;
        var handTransform = _handTransform.transform;
        
        item.transform.parent = _transform;
        item.transform.rotation = handTransform.rotation;
        item.transform.position = handTransform.position;
    }

    public void Undo()
    {
        var item = _weapon.gameObject;
        item.transform.parent = null;
    }
}
