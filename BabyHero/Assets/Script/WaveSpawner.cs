using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    // it is wave script we can put any numbers of waves we want with number of enemy in every wave and the waiting time between the waves and the delay time also between them
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    [System.Serializable] //we need to make it [serializable class] so it can be appear in the editor it is like the idea with the scriptable object class we can put this class in a scriptable object and from it we can inistiate the variable wave list
    public class Wave // it does not matter if we put the class outside the monobehavior class it is the same
    {
        public string name; // to take the enemy name to kno which round the enemy come from so in every number of round we are putting the name of that round just for checking
        public Transform enemy; // to take the enemy transform which is from the respawn point
        public int count; // the count of the enemy that will go out from the respawn
        public float rate; // the delay
    }
    public Wave[] waves;
    private int nextWave = 0;
    public int NextWave  // it is kind of overrittten just to show the variable current valuses
    {
        get { return nextWave + 1; }
    }
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    public float WaveCountdown 
    {
        get { return waveCountdown; }
    }
    private float searchCountdown = 1f; // the search countdowe for enemy tag will be every seccond
    private SpawnState state = SpawnState.COUNTING;
    public SpawnState State
    {
        get { return state; }
    }
    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }
        waveCountdown = timeBetweenWaves; //defaulting the wave countdown which is equal to the time between the waves which is 5 secconds after wave end
    }
    void Update()
    {
        if (state == SpawnState.WAITING) // we are checking if the enemy still alive or not
        {
            if (!EnemyIsAlive()) // if there is no enemy alive then we finished from the wave and the wave is completed and we increment the number of enemy for the next wave
            {
                //begin new wave
                WaveCompleted();
            }
            else
            {
                return; // it will return to the above if it will not go down because we want to check the enemy number which means the player still killing them
            }
        }
        if (waveCountdown <= 0) // if it equal or less than zero
        {
            if (state != SpawnState.SPAWNING) // to check if we are spawnning or not if not then we will spawn at the begginning we are not spawnning
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime; // if countdown is not equal or less than zero we substract the wave waiting time or the wave countdown
        }
    }
    void WaveCompleted() // when the wave completed
    {
        // we can do what we want when the wave complete like make the player health is full againg or increase the number of weapons
        Debug.Log("Wave Completed!");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1) // reseting some values of the wave if it was bigger than the length we are resetting it
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }
        else
        {
            nextWave++; // the wave index will increament if we are not out of the range
        }
    }
    bool EnemyIsAlive() // to check if the enemy is alive or not
    {
        searchCountdown -= Time.deltaTime; // to control the searching time on the enemy tag rather than 1 fream it will be every 1 secconds
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator SpawnWave(Wave _wave) // we will take the things from the wave class
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING; //the things or the enemy that we are spawnning

        for (int i = 0; i < _wave.count; i++) // the number of enemy spawnning depends on the count every new spawn it increase
        {
            SpawnEnemy(_wave.enemy);
            //after we are respawnning one enemy we want to wait amount of secconds before we are respawnning another enemy
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.WAITING; // if we are not respawnning enemies we will go to waiting between the times
        yield break;
    }
    void SpawnEnemy(Transform _enemy) // from here we are spawnning enemies
    {
        Debug.Log("Spawning Enemy: " + _enemy.name); // to kno which round the enemy come

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)]; // it will be in a list
        Instantiate(_enemy, _sp.position, _sp.rotation); // from it we are instantiating the enemy
    }
}
