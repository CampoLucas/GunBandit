using System;
public interface ISwapState
{
    /// <summary>
    /// The state the weapon is currently on
    /// </summary>
    public WeaponState CurrentState { get; }
    
    /// <summary>
    /// Event that happens when the weapon is changed
    /// </summary>
    Action OnWeaponChange { get; }
    /// <summary>
    /// Changes between weapon states
    /// </summary>
    /// <param name="state"></param>
    void ChangeState(WeaponState state);
}
