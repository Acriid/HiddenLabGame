using UnityEngine;

public interface IHealth
{
    void Damage();

    void Death();
    void Heal();

    int _MaxHealth {get; set;}

    int _CurrentHealth{get; set;}
}
