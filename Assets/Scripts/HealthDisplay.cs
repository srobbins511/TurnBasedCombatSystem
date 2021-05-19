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

public class HealthDisplay : MonoBehaviour
{
    private float startScale;

    [SerializeField]
    private Entity Character;

    public float StartHealth;
    public float Health;

    public Coroutine healthBar;

    public float newScale;

    public void Start()
    {
        startScale = transform.localScale.x;
        StartHealth = Character.Health;
        Health = StartHealth;
        healthBar = StartCoroutine(HealthDisplayTiming());
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }

    public IEnumerator HealthDisplayTiming()
    {
        
        while (Health > 0)
        {
            newScale = startScale * (Health / StartHealth);
            while (newScale != transform.localScale.x)
            {
                //Smooth the change of the health bar
                transform.localScale =new Vector3( Mathf.Lerp(transform.localScale.x, newScale, .01f),transform.localScale.y,transform.localScale.z);
                //Threshold to determine when values have settled
                if(Mathf.Abs(transform.localScale.x - newScale) <.1)
                {
                    transform.localScale = new Vector3(newScale, transform.localScale.y, transform.localScale.z);
                }
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitWhile(() => Health == Character.Health);
            Debug.Log("Passed Waitwhile");
            Health = Character.Health;
        }
        Debug.Log("exit coroutine");
    }
}
