using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smenue : MonoBehaviour
{
    // to show the start menue when the player press Enter button and ESC to exit from the star button
    public GameObject StartMenue;
    bool ispaused = false;
      void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            StartMenue.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            StartMenue.SetActive(false);
        }
    }
    public void pausegame()
    { 
            Time.timeScale = 1;
            ispaused = false;
    }
}
