using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour , IHealth
{
    public float _MaxHealth {get; set;}

    public float _CurrentHealth{get; set;}
    void Start()
    {
        
    }
    public void Damage(float damageAmount)
    {
        _CurrentHealth -= damageAmount;
        if(_CurrentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {

    }
    public void Heal()
    {
        if (_CurrentHealth != _MaxHealth)
        {
            _CurrentHealth += 1;
        }
    }

    
}
