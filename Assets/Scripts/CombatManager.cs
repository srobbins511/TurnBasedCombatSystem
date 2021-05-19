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

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public Dropdown PlayerActionSelect;

    [Tooltip("The Current Active Entity taking its turn")]
    public Entity ActiveEntity;

    private Entity target;
    public Entity TargetedEntity
    {
        get {
            return target;
            }
        set
        {
            if(TargetedEntity != null)
            {
                TargetedEntity.ResetColor();
            }
            target = value;
        }
    }

    public Coroutine CombatManagment;

    public Entity[] PlayerTeam;
    public Entity[] Enemyteam;

    public delegate void EventHandler<T>(T Value);

    public WriteText TextDisplay;
    public event EventHandler<CombatState> OnTurnChange;

    /// <summary>
    /// The States that a combat could be in
    /// </summary>
    public enum CombatState
    {
        PlayerTurn,
        Targeting,
        EnemyTurn,
        Win,
        Lose
    }

    public CombatState CurrentGameState;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        CombatManagment = StartCoroutine(StateController());
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(CurrentGameState == CombatState.PlayerTurn)
            {
                ChangeTurn(CombatState.EnemyTurn);
            }
            else
            {
                ChangeTurn(CombatState.PlayerTurn);
            }
        }
    }

    public void ChangeTurn(CombatState newTurn)
    {
        CurrentGameState = newTurn;
        OnTurnChange?.Invoke(newTurn);
    }

    public void StartTargeting()
    {
        CurrentGameState = CombatState.Targeting;
    }

    public void DisplayText(string text)
    {
        TextDisplay.DisplayText(text);
    }

    public void DisplayText(string text,float time)
    {
        TextDisplay.DisplayText(text, time);
    }

    /// <summary>
    /// Logic for winning the Game
    /// </summary>
    public void Win()
    {

    }

    /// <summary>
    /// Logic for Losing the Game
    /// </summary>
    public void Lose()
    {

    }

    /// <summary>
    /// Coroutine for managing the Turn Order
    /// </summary>
    /// <returns></returns>
    public IEnumerator StateController()
    {
        while(CurrentGameState != CombatState.Lose || CurrentGameState != CombatState.Win)
        {
            if(CurrentGameState == CombatState.PlayerTurn)
            {
                yield return new WaitWhile(() => CombatState.PlayerTurn == CurrentGameState);
            }
            else if (CurrentGameState == CombatState.EnemyTurn)
            {
                yield return new WaitWhile(() => CombatState.EnemyTurn == CurrentGameState);
            }
            yield return null;
        }

        if(CurrentGameState != CombatState.Lose)
        {
            Lose();
        }

        if(CurrentGameState != CombatState.Win)
        {
            Win();
        }
        
    }

}
