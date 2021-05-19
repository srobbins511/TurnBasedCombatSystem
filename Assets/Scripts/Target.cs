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
public interface ITargetable
{
    //Method that allows a character to be hit by an attack
    bool DetermineAttackOutcome(int AttackValue, int DamageValue, LookUp.DmgTypes DamageType);

    //Method that allows a character to be affected by effects that need the character to save against it
    bool MakeSave(int SaveDC, LookUp.AttributeTypes SaveType);
}
