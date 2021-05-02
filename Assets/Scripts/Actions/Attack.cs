using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Scripts/Actions/Attack", order = 1)]
public class Attack : Action
{
    [SerializeField]
    protected LookUp.AttributeTypes AttrubuteUsed;

    [SerializeField]
    [Range(0, 15)]
    protected int AttackBonus;

    [SerializeField]
    protected LookUp.DmgTypes DamageType;

    [SerializeField]
    [Tooltip("X is the Min Damage Value, Y is the Max Damage Value")]
    [Min(1)]
    protected Vector2Int DamageRange;

    protected int DamageAmount
    {
        get { return Random.Range(DamageRange.x, DamageRange.y); }
    }

    protected int AttackRoll
    {
        get { return Random.Range(1, 20) + AttackBonus; }
    }

    public override void Use(Entity Self)
    {
        if(Ready)
        {
            Target.DetermineAttackOutcome(AttackRoll + Self.Stats.GetAttribute(AttrubuteUsed) ,DamageAmount + + Self.Stats.GetAttribute(AttrubuteUsed), DamageType);
        }
    }

}
