using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
    bool DetermineAttackOutcome(int AttackValue, int DamageValue, LookUp.DmgTypes DamageType);

    bool MakeSave(int SaveDC, LookUp.AttributeTypes SaveType);
}
