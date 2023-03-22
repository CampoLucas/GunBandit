using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Observer
{
    private CharacterSO _stats;
    private Image _healthBar;
    private Image _background;
    private Damageable _damageable;
    
    private void Awake()
    {
        _damageable = GetComponent<Damageable>();
        _stats = GetComponent<Character>().GetData() as CharacterSO;
        foreach (var image in GetComponentsInChildren<Image>())
        {
            if (image.gameObject.CompareTag("HealthBar"))
                _healthBar = image;
            else
            {
                _background = image;
            }
        }
    }
    
    private void Start()
    {
        if(_damageable != null) 
            _damageable.Subscribe(this);
    }
    
    private void Update()
    {
        if (_damageable.CurrentLife >= _stats.MaxHealth)
        {
            _healthBar.enabled = false;
            _background.enabled = false;
        }
        else
        {
            _healthBar.enabled = true;
            _background.enabled = true;
        }
    }
    
    public override void OnNotify(string message, params object[] args)
    {
        //if (message != "TAKE_DAMAGE") return;
    
        FillHealthBar(_damageable.CurrentLife, _stats.MaxHealth);
    }
    
    public void FillHealthBar(float currentHealth, float maxHealth)
    {
        _healthBar.fillAmount = currentHealth / maxHealth;
    }
}
