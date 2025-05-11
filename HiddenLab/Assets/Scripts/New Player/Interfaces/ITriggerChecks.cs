using UnityEngine;

public interface ITriggerChecks 
{
    bool isInPickuprange { get; set; }
    void setisInPickuprange(bool value);
}
