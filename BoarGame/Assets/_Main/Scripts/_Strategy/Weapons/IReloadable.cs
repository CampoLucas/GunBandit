public interface IReloadable
{
    int CurrentAmmo { get; }
    int CurrentMagAmmo { get; }
    
    /// <summary>
    /// Reloads weapon
    /// </summary>
    void Reload();
    /// <summary>
    /// Check if the gun is out of ammo
    /// </summary>
    bool OutOfAmmo();
    /// <summary>
    /// Check if the player is reloading
    /// </summary>
    bool IsReloading();
    /// <summary>
    /// Add ammo, the amount of ammo will not be greater than the maximum amount you can have
    /// </summary>
    /// <param name="amount of ammo"></param>
    void GetAmmo(in int amount);
    /// <summary>
    /// Decrease ammo once
    /// </summary>
    void DecreaseAmmo();
    /// <summary>
    /// Decreases ammo by a certain amount
    /// </summary>
    /// <param name="amount"></param>
    void DecreaseAmmo(in int amount);
}
