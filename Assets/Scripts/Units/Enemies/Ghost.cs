using UnityEngine;

public class Ghost : Enemy
{
    private HealthBar _healthBar;

    protected override void Start()
    {
        base.Start();
        _healthBar = GetComponentInChildren<HealthBar>();
    }

    public override void Damage(float damageAmount)
    {
        base.Damage(damageAmount);

        _healthBar.UpdateHealthBar(MaxHealth, CurrentHealth);
    }
}
