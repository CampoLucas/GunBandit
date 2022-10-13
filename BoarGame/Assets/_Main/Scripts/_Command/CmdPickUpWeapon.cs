using UnityEngine;

public class CmdPickUpWeapon : ICommand
{
    private readonly Weapon2 _weapon;
    private readonly Transform _transform;

    public CmdPickUpWeapon(Weapon2 weapon, Transform transform)
    {
        _weapon = weapon;
        _transform = transform;
    }
    
    public void Do()
    {
        var item = _weapon.gameObject;
        var transform1 = _transform.transform;
        
        item.transform.parent = _transform;
        item.transform.rotation = transform1.rotation;
        item.transform.position = transform1.position;
    }

    public void Undo()
    {
        var item = _weapon.gameObject;
        item.transform.parent = null;
    }
}
