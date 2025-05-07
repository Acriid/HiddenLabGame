using UnityEngine;

public interface ITriggerChecks 
{
    bool isInrange { get; set; }
    void setInRange(bool value);
}
