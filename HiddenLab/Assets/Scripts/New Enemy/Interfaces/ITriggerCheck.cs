using UnityEngine;

public interface IEnemyTriggerCheck
{
    bool CanSeePlayer {get; set;}
    void setCanSeePlayer(bool newValue);
}
