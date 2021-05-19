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

public class UIManager : MonoBehaviour
{
    public void Start()
    {
        CombatManager.Instance.OnTurnChange += OnTurnChange;
    }

    public List<GameObject> ActivePlayerTurn;

    public enum UIStates
    {
        PlayerTurn,
        EnemyTurn,
        DisplayActionResult,

    }

    public void OnTurnChange(CombatManager.CombatState state)
    {
        switch (state)
        {
            case CombatManager.CombatState.PlayerTurn:
                ActivePlayerTurn.ForEach(Activate);
                break;
            case CombatManager.CombatState.EnemyTurn:
                ActivePlayerTurn.ForEach(Deactivate);
                break;
        }

    }

    System.Action<GameObject> Deactivate = (obj) => obj.SetActive(false);
    System.Action<GameObject> Activate = (obj) => obj.SetActive(true);

}
