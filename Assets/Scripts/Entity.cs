using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
*   Name: Sean Robbins
*   ID: 2328696
*   Email: srobbins@chapman.edu
*   Class: CPSC245
*   Turn Based Combat System
*   This is my own work. I did not cheat on this assignment
*/
public class Entity : MonoBehaviour , ITargetable
{

    public string CharacterName;
    [SerializeField]
    [Range(10,30)]
    [Tooltip("How likely an entity is to be hit, 10 will be hit almost all the time, 20 is hard to hit, 30 practically impossible")]
    protected int ArmorClass;

    [SerializeField]
    [Min(0)]
    [Tooltip("How far from death a character is, how many attacks that they can take")]
    public int Health;

    [SerializeField]
    [Tooltip("Any damage type in this list will be halved")]
    protected List<LookUp.DmgTypes> Resistances;

    [SerializeField]
    [Tooltip("Entity will ignore any damage type in this list")]
    protected List<LookUp.DmgTypes> Immunities;

    public List<Action> Actions;

    public Attributes Stats;

    public Color baseColor;

    public Action SelectedAction;

    public bool DetermineAttackOutcome(int AttackValue, int DamageValue, LookUp.DmgTypes DamageType)
    {
        CombatManager.Instance.TargetedEntity = null;
        if (AttackValue >= ArmorClass)
        {
            TakeDamage(DamageValue, DamageType);
            return true;
        }
        CombatManager.Instance.DisplayText(CharacterName + " has been Missed", 3f);
        return false;
    }

    public void Start()
    {
        if(tag == "Player")
        {
            CombatManager.Instance.ActiveEntity = this;
            foreach(Action a in Actions)
            {
                Dropdown.OptionData temp = new Dropdown.OptionData();
                temp.text = a.name;
                CombatManager.Instance.PlayerActionSelect.options.Add(temp);
            }
        }

        baseColor = GetComponent<MeshRenderer>().material.color;
    }

    public void SelectAction()
    {
        if (CombatManager.Instance.PlayerActionSelect.value > 0)
        {
            SelectedAction = Actions[CombatManager.Instance.PlayerActionSelect.value - 1];
        }
    }

    public void Attack()
    {
        if (SelectedAction != null)
        {
            UseAction(SelectedAction);
        }
    }

    public void UseAction(Action action)
    {
        if(CombatManager.Instance.TargetedEntity)
        {
            action.SetTarget(CombatManager.Instance.TargetedEntity);
            action.Use(this);
            if (tag.Equals("Player"))
            {
                CombatManager.Instance.ChangeTurn(CombatManager.CombatState.EnemyTurn);
            }
            else
            {
                CombatManager.Instance.ChangeTurn(CombatManager.CombatState.PlayerTurn);
            }
        }
        
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
        CombatManager.Instance.DisplayText(CharacterName + " has taken " + DamageAmount + " " + DamageType.ToString() + " Damage", 5f);
        Health -= DamageAmount;

        if (Health <= 0)
        {
            Death();
        }

        
    }

    /// <summary>
    /// Testing functionality to be removed
    /// </summary>
    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A) && tag != "Player" && CombatManager.Instance.CurrentGameState == CombatManager.CombatState.EnemyTurn)
        {
            CombatManager.Instance.TargetedEntity = (GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>());
            SelectedAction = Actions[0];
            UseAction(SelectedAction);
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

    public void ResetColor()
    {
        GetComponent<MeshRenderer>().material.color = baseColor;
    }

    #region MouseEvents
    public void OnMouseEnter()
    {
        if (GetComponent<MeshRenderer>().material.color == baseColor)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    public void OnMouseExit()
    {
        if (GetComponent<MeshRenderer>().material.color == Color.green)
        {
            GetComponent<MeshRenderer>().material.color = baseColor;
        }
    }
    public void OnMouseUpAsButton()
    {
        if(CombatManager.Instance.CurrentGameState == CombatManager.CombatState.Targeting)
        {
            CombatManager.Instance.TargetedEntity = this;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    #endregion
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

