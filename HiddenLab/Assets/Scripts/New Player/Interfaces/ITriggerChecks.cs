using UnityEngine;

public interface ITriggerChecks
{
    bool isInPickuprange { get; set; }
    void setisInPickuprange(bool value);
    bool isInFilerange { get; set; }
    void setisInFilerange(bool value);
    bool isInSaverange { get; set; }
    void setisInSaverange(bool value);
}
