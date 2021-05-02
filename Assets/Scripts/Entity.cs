using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour , ITargetable
{
    [SerializeField]
    [Range(10,30)]
    protected int ArmorClass;

    [SerializeField]
    [Min(0)]
    protected int Health;

    [SerializeField]
    protected List<LookUp.DmgTypes> Resistances;

    [SerializeField]
    protected List<LookUp.DmgTypes> Immunities;

    public List<Action> Actions;

    public Attributes Stats;

    public bool DetermineAttackOutcome(int AttackValue, int DamageValue, LookUp.DmgTypes DamageType)
    {
        if(AttackValue >= ArmorClass)
        {
            TakeDamage(DamageValue, DamageType);
            return true;
        }
        return false;
    }

    public void TakeDamage(int DamageAmount, LookUp.DmgTypes DamageType)
    {
        if (Immunities.Contains(DamageType))
        {
            DamageAmount = 0;
        }
        else if (Resistances.Contains(DamageType))
        {
            DamageAmount = DamageAmount / 2;
        }
        Health -= DamageAmount;

        if (Health <= 0)
        {
            Death();
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(5, LookUp.DmgTypes.Force);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(5, LookUp.DmgTypes.Lightning);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(5, LookUp.DmgTypes.Acid);
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    public bool MakeSave(int SaveDC, LookUp.AttributeTypes SaveType)
    {
        throw new System.NotImplementedException();
    }
}

/// <summary>
/// Contains the Stats Associated with the Entity
/// </summary>
[System.Serializable]
public struct Attributes
{
    [Range(1, 20)]
    public int Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma;

    /// <summary>
    /// Return the attribute modifier of the specified Stat
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetAttribute(LookUp.AttributeTypes type)
    {
        int temp = 0;
        switch (type)
        {
            case LookUp.AttributeTypes.Strength:
                temp = Strength;
                break;
            case LookUp.AttributeTypes.Dexterity:
                temp = Dexterity;
                break;
            case LookUp.AttributeTypes.Constitution:
                temp = Constitution;
                break;
            case LookUp.AttributeTypes.Intelligence:
                temp = Intelligence;
                break;
            case LookUp.AttributeTypes.Wisdom:
                temp = Wisdom;
                break;
            case LookUp.AttributeTypes.Charisma:
                temp = Charisma;
                break;
        }
        return (temp - 10)/2;
    }
}

