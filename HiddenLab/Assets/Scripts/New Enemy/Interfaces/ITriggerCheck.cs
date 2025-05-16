using UnityEngine;

public interface ITriggerCheck
{
    bool CanSeePlayer {get; set;}
    void setCanSeePlayer(bool newValue);
}
