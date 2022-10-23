
public interface IInventory
{
    Weapon2 CurrentWeapon();
    bool HasFullInventory();
    bool HasCurrentWeapon();
    void AddItem(Pickable item);
    void DropItem();
    void ChangeWeapon(bool up);
}
