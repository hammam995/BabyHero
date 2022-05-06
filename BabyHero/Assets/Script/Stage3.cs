using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage3 : MonoBehaviour
{
    // in stage 3 it is only for surviving there is no winner scene the player must survive as possible as he can
    public Player Ph;
    public GameManager GM;
    void Update()
    {
        if (Ph.health <= 0)
        {
            Debug.Log("Character die");
            StartCoroutine(Wait(3));
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
}
