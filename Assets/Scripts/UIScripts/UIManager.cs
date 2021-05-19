using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    System.Action<GameObject> Deactivate = (obj) => obj.SetActive(false);
    System.Action<GameObject> Activate = (obj) => obj.SetActive(true);

}
