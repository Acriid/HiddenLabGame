using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour , IHealth
{
    public int _MaxHealth {get; set;}

    public int _CurrentHealth{get; set;}
    private PlayerAttributes playerAttributes;
    void Start()
    {
        _CurrentHealth += playerAttributes.PlayerHealth;
        playerAttributes.OnPlayerHealthChange += HandleHealthChange;
    }
    public void Damage()
    {
        if(_CurrentHealth == 2)
        {
            playerAttributes.RequestPlayerHealthChange(1);
        }
        else if(_CurrentHealth == 1)
        {
            Death();
        }
    }

    public void Death()
    {
        //TODO: show the death menu and pause the game
    }
    public void Heal()
    {
        if (_CurrentHealth != _MaxHealth)
        {
            playerAttributes.RequestPlayerHealthChange(2);
        }
    }

    private void HandleHealthChange(int newValue )
    {
        _CurrentHealth = newValue;
    }
}
