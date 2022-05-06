using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage1 : MonoBehaviour
{
    //things happens in the stage like moving to the game over scene and if the hero kill n number of enemy we will move to the winner screen After 3 secconds
    public Player Ph;
    public GameManager GM;
    GameObject Baby;
    GameObject Enemy;
    public int EnemyDeathNumber;
    void Start()
    {
        Baby = GameObject.FindWithTag("Baby");
        Enemy = GameObject.FindWithTag("Enemy");
        EnemyDeathNumber = 0;
    }

    void Update()
    {
        if (Ph.health <= 0)
        {
            Debug.Log("Character die");
            StartCoroutine(Wait(3));
        }
        if (EnemyDeathNumber == 8)
        {
            Debug.Log("Enemy die"); // winner scene
            StartCoroutine(Wait2(3));
        }
    }
    IEnumerator Wait(float duration)
    {
        //This is a coroutine
        Debug.Log("Float duration = " + duration);
        yield return new WaitForSeconds(duration);   //Wait
        Debug.Log("End Wait() function and the time is: ");
        GM.NextScene1("GAMEOVER");
    }
    IEnumerator Wait2(float duration) // to go to Winner
    {
        //This is a coroutine
        Debug.Log("Float duration = " + duration);
        yield return new WaitForSeconds(duration);   //Wait
        Debug.Log("End Wait() function and the time is: ");
        GM.NextScene1("Winner");
    }
}
