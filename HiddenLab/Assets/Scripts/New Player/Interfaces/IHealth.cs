using UnityEngine;

public interface IHealth
{
    void Damage(float damageAmount);

    void Death();
    void Heal();

    float _MaxHealth {get; set;}

    float _CurrentHealth{get; set;}
}
