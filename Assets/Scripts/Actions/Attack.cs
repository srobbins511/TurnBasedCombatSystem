using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*   Name: Sean Robbins
*   ID: 2328696
*   Email: srobbins@chapman.edu
*   Class: CPSC245
*   Turn Based Combat System
*   This is my own work. I did not cheat on this assignment
*/
[CreateAssetMenu(fileName = "Attack", menuName = "Scripts/Actions/Attack", order = 1)]
public class Attack : Action
{
    [SerializeField]
    [Tooltip("The Character Attribute used in determining how well a character can use the Attack they are attempting")]
    protected LookUp.AttributeTypes AttrubuteUsed;

    [SerializeField]
    [Range(0, 15)]
    [Tooltip("The bonus used on determining how likely an attack is to hit")]
    protected int AttackBonus;

    [SerializeField]
    [Tooltip("The Type of Damage the Attack deals")]
    protected LookUp.DmgTypes DamageType;

    [SerializeField]
    [Min(1)]
    [Tooltip("X is the Min Damage Value, Y is the Max Damage Value")]
    protected Vector2Int DamageRange;

    //The property that determins how much damage is dealth a specific time that damage is dealt
    protected int DamageAmount
    {
        get { return Random.Range(DamageRange.x, DamageRange.y); }
    }

    //The property that determines is a specific attempt to attack an enemy will succeed
    protected int AttackRoll
    {
        get { return Random.Range(1, 20) + AttackBonus; }
    }

    public override void Use(Entity Self)
    {
        if(Ready)
        {
            Target.DetermineAttackOutcome(AttackRoll + Self.Stats.GetAttribute(AttrubuteUsed) ,DamageAmount + Self.Stats.GetAttribute(AttrubuteUsed), DamageType);
        }
        else
        {
            Debug.Log("Not Ready to Use Attack");
        }
    }

}
