using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaves : Character
{
    // this script is diifrent from enemy4 it is only for the waves modes which is here always the enemy will follow and chase the player and shoot him there is no patroll moving
    public enum AIState { EnemyDetectionMode, AttackingMode };
    public AIState aiState = AIState.EnemyDetectionMode;
    NavMeshAgent nav;
    Transform player;
    public Transform[] wayPoints;
    public Vector3 tempRotation;
    public Color colorray;
    public GameObject Bullet;
    GameObject instBullet;
    Rigidbody instBulletRigidbody;
    public Weaponsinfo Wi;
    public Weaponsinfo WPlayer;
    private Animator anim;
    public bool Attack = false;
    public bool canshoot;
    public float shootspeed = 5;
    public float myFloat;
    public int wayPointIndex = 0;
    public int i; // index variable for weapons to make the enemy shoot randomly weapon
    public int tiempoEntreMens = 5; // the time that which we enter it , and iit does not matter if we change it
    protected override void Start()
    {
        speed = 2;
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Baby").transform;
        i = Random.Range(0, 2);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position); //to give us the distance between the player and the enemy
        Debug.DrawRay(transform.position, (player.position - transform.position).normalized * 10, colorray); // to draw the ray line
        dyingmsg();
        switch (aiState)
        {  
            case AIState.EnemyDetectionMode:
                if (CanSeePlayer())
                {
                    Chasing(dist);
                    if (Attack == true)// if in the previous method chasing the attack become true which is mean we are close to the player the enemy will shoot the player while he is standing and looking to the player 
                    {
                        aiState = AIState.AttackingMode;
                    }
                }
                break;
            case AIState.AttackingMode: // will follow the previous steps

                if (CanSeePlayer())
                {
                    if (Attack == true)
                    {
                        LooktoThePlayer();
                        if (dist > 6f)
                        {
                            LooktoThePlayer();
                            Attack = false;
                        }
                        else // the distance is less than 3f we look to the player and shoot after n time with timer method
                        {
                            Timer();
                        }
                    }
                    else
                    {
                        LooktoThePlayer();
                        aiState = AIState.EnemyDetectionMode; // then go back to detecting mode
                    }
                }
                break;
            default:
                break;
        }
    }
   
    public bool CanSeePlayer() // method to check if the Enemy can see the Player in the same level and in Angle of vistion but if the Player was behind the enemy the enemy can not see him 
    {
        return true;
    }
    public void Chasing(float distance) // chasing the player the Enemy will go after the player to track him if he saw him in a distance and there is no between them object to hide in the same layer and level of vistion
    {
        LooktoThePlayer();
        isWalking();
        nav.SetDestination(player.position); // go to the player position at that moment
        if (distance < 6f) //and we check the distance again between the enemy and the player
        {
            LooktoThePlayer();
            // nav.SetDestination(transform.position);  // we make the character stop then we go to the attack mode
            nav.isStopped = true;
            Attack = true; // if the distance is lees thans 3 the enemy will stop chasing and stay in it is position and go to attack mode int that mode the nemy will throw weapon very n secconds
        }
        else
        {
            nav.isStopped = false;
            Attack = false; // the enemy will go back to the patroll mode to follow it is tracking and moving system
        }
    }
    public void LooktoThePlayer() // rather than puting it every time when we look to the player
    {
        // turn the enemy toward the player
        tempRotation = transform.rotation.eulerAngles;
        transform.LookAt(player); // we change this to look to the player
        tempRotation.y = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(tempRotation);
    }
    public void Timer() // is timer to controll the shooting time for the enemy
    {
        anim.SetFloat("Walk", 0f);
        myFloat += Time.deltaTime;
        if (myFloat >= tiempoEntreMens)    // in each seccond we will check enter to see the condition
        {
            // Debug.Log(transcurrido);

            LooktoThePlayer();
            anim.SetFloat("Walk", 0f);
            shoot();

            // Debug.Log("transcurrido is ++ is" + transcurrido);
            myFloat = 0; //we will reset it because the transcurrido here will count the secconds assummed
        }
    }
    public void shoot() // method to make the Enemy shoot when he direct his face to the Player
    {
        //changing weapons function
        //shoot function
        anim.SetFloat("Walk", 0f);

        instBullet = Instantiate(Wi.weaponsList[i].weaponObject, Bullet.transform.position, Wi.weaponsList[i].weaponObject.transform.rotation, Bullet.transform) as GameObject;
        if (instBullet.GetComponent<Rigidbody>() == null)
        {
            instBullet.AddComponent<Rigidbody>();
            instBullet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
        instBullet.transform.position = Bullet.transform.position;
        anim.SetTrigger("Throw");

        instBullet.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * shootspeed, ForceMode.Impulse);
        instBullet.transform.SetParent(null);
        Destroy(instBullet, 3);
    }
    public void dyingmsg()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void isWalking() // method to controll the Enemy movement and walking Animation
    {
        anim.SetFloat("Walk", 0.5f);
    }
    private void OnTriggerEnter(Collider collision) // method to make the Enemy take a damage when he get hit by the Player Weapons
    {
        if (collision.gameObject.tag == "BannanaWeapon")
        {
            takedamage(WPlayer.weaponsList[0].weaponDamagae);
            Debug.Log("the enemy have taken bannana Damage = " + WPlayer.weaponsList[0].weaponDamagae);
            Debug.Log("the new health is =" + health);
            Debug.Log("Bannana Destroyed");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "AppleWeapon")
        {
            takedamage(WPlayer.weaponsList[1].weaponDamagae);
            Debug.Log("the enemy have taken Apple Damage = " + WPlayer.weaponsList[1].weaponDamagae);
            Debug.Log("the new health is =" + health);
            Debug.Log("Apple Destroyed");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "PearWeapon")
        {
            takedamage(WPlayer.weaponsList[2].weaponDamagae);
            Debug.Log("the enemy have taken Pear Damage = " + WPlayer.weaponsList[2].weaponDamagae);
            Debug.Log("the new health is =" + health);
            Debug.Log("Pear Destroyed");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "LeftHand")
        {
            takedamage(25);
            Debug.Log("the enemy have taken LeftHand Damage = ");
            Debug.Log("the new health is =" + health);
        }
    }
}
