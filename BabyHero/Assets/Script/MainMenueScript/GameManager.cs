using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    AsyncOperation loadAsync;
    public Slider slide;
    public Text entmsg;
    public GameObject LoadingPanel1;
    public GameObject StartMenue;
    bool ispaused = false;
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        entmsg.gameObject.SetActive(false);
        StartCoroutine(nextsc());
    }

    void Update()
    {
        pausegame2();
    }
    public void NextScene1(string Game)
    {
        SceneManager.LoadScene(Game);
    }
    IEnumerator nextsc() //method for doing the load scene it depends on the object and for every stage it have it owens object by name , so if it was the name of the GameManager object GM it will load the scene for the firist stage , if it is name GM2 we are in loading scene for stage2 and it will load stage 2
    {
        //to go to loading scene 1 for stage 1
        if (gameObject.name == "GM") // in loading you must press space so you can go to the scene
        {
            loadAsync = SceneManager.LoadSceneAsync("Game");
            loadAsync.allowSceneActivation = false;
            while (loadAsync.progress < 0.9f)
            {
                slide.value = loadAsync.progress;
                yield return null;
            }
            yield return new WaitForSeconds(3);
            entmsg.gameObject.SetActive(true);
            //  slide.value = 1;
            slide.value = loadAsync.progress;
            slide.value = 1;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // when the loading  finish you must press space button to go to the next scene or the stage everything
            loadAsync.allowSceneActivation = true;
        }
        if (gameObject.name == "GM2")
        {
            loadAsync = SceneManager.LoadSceneAsync("Game2");
            loadAsync.allowSceneActivation = false;
            while (loadAsync.progress < 0.9f)
            {
                slide.value = loadAsync.progress;
                yield return null;
            }
            yield return new WaitForSeconds(3);
            entmsg.gameObject.SetActive(true);
            //  slide.value = 1;
            slide.value = loadAsync.progress;
            slide.value = 1;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            loadAsync.allowSceneActivation = true;
        }
        if (gameObject.name == "GM3")
        {
            loadAsync = SceneManager.LoadSceneAsync("Game3");
            loadAsync.allowSceneActivation = false;
            while (loadAsync.progress < 0.9f)
            {
                slide.value = loadAsync.progress;
                yield return null;
            }
            yield return new WaitForSeconds(3);
            entmsg.gameObject.SetActive(true);
            //  slide.value = 1;
            slide.value = loadAsync.progress;
            slide.value = 1;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            loadAsync.allowSceneActivation = true;
        }
    }
    public void ChangeRes(Dropdown drop) // method for changing the resoulution
    {
        switch (drop.value)
        {
            case 0:
                Screen.SetResolution(1024, 768, true);
                break;
            case 1:
                Screen.SetResolution(1280, 720, true);
                break;

            case 2:
                Screen.SetResolution(1366, 768, true);
                break;
            case 3:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }
    public void changeVolume(Slider slider) // method for changing the sound
    {
        AudioListener.volume = slider.value;
    }
    public void CloseApp() // method for closing the app
    {
        Application.Quit();
    }
    public void pausegame() // method for pause the game when we press on the start button in the menue
    {
        // for the start button
        Time.timeScale = 1;
        ispaused = false;
    }
    //both methods help each others to prevent problem one is when we press the start button from the start menue and the other is after we  
    public void pausegame2() // method for pause the game when we press a button from the keyboard
    {
        //for the canvas
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
}
