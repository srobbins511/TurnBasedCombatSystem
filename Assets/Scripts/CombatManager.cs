using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public Coroutine CombatManagment;

    public Entity[] PlayerTeam;
    public Entity[] Enemyteam;

    public delegate void EventHandler<T>(T Value);
    public event EventHandler<CombatState> OnTurnChange;
    public enum CombatState
    {
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose
    }

    public CombatState CurrentGameState;

    public void Start()
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
