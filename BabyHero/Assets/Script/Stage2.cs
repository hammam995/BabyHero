using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage2 : MonoBehaviour
{
    //things happens in the stage 2 like moving to the game over scene and if the hero kill n number of enemy before the timer finish we will move to the winner screen if the time finish before that the player
    public Player Ph;
    public GameManager GM;
    GameObject Baby;
    GameObject Enemy;
    public int EnemyDeathNumber;
    public Stage1 END;
    public float myTimer;
    public Text timerText;
    private bool timerIsActive = true;
    void Start()
    {
        Baby = GameObject.FindWithTag("Baby");
        Enemy = GameObject.FindWithTag("Enemy");
        EnemyDeathNumber = 0;
        myTimer = 120;
        END.EnemyDeathNumber = 0;
    }
    void Update()
    {
        EnemyDeathNumber = END.EnemyDeathNumber;
        END.EnemyDeathNumber = EnemyDeathNumber;
        if (Ph.health <= 0)
        {
            Debug.Log("Character die"); // gameOverScene
            StartCoroutine(Wait(3));
        }
        if (EnemyDeathNumber == 8)
        {
            Debug.Log("Enemy die"); // winner scene
            StartCoroutine(Wait2(1));
        }
        Timer();
        timerText.text = ("The Time Remaining : " + (int)myTimer);
}
IEnumerator Wait(float duration) // to go to GAMEOVER
    {
        //This is a coroutine
        Debug.Log("Float duration = " + duration);
        yield return new WaitForSeconds(duration);   //Wait
        Debug.Log("End Wait() function and the time is: ");
        GM.NextScene1("GAMEOVER2");
    }
    IEnumerator Wait2(float duration) // to go to Winner
    {
        //This is a coroutine
        Debug.Log("Float duration = " + duration);
        yield return new WaitForSeconds(duration);   //Wait
        Debug.Log("End Wait() function and the time is: ");
        GM.NextScene1("Winner");
    }
    // the timer for stage 2 we check the condition if the enemy didnt die before thhat time you lose
    public void Timer()
    {
        if (timerIsActive)
        {
           myTimer -= Time.deltaTime;
            if (myTimer <= 0)
            {
                myTimer = 0;
                StartCoroutine(Wait(1));
                Debug.Log("You lose");
            }
        }
    }
}
