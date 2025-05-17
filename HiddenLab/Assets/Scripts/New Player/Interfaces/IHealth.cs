using UnityEngine;

public interface IHealth
{
    void Damage();
    void Death();
    void Heal();
    int _CurrentHealth{get; set;}
}
