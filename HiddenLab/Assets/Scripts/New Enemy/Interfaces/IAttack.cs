using System.Collections;
using UnityEngine;

public interface IAttack
{
    float AttackTimer {get; set;}
    float AttackRange {get; set;}
    IEnumerator AttackWait(float waitTime);
}
