using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : Character
{
    public float distancetotheground;
    public float range;
    public float timeToDie = 1; // time to destroy the bullet its time entermanos
    public float timeBetweenshootmode;
    public float shootspeed;
    public float myFloat;
    float tiempo; //time
    public int tiempoEntreMens = 3; // the time that which we enter it , and iit does not matter if we change it
    [SerializeField] int transcurrido = 0; // the counter or the pinter which is i
    public int njump;
    public int i = 0;
    public bool canshoot;
    public bool iscreate = false;
    public bool B = false;
    public bool BB = false; // the boolean to make the Bannana text appear when we are in the Bannana table
    public bool AA = false; // the boolean to make the Apple text appear when we are in the Apple table
    public bool PP = false; // the boolean to make the Pear text appear when we are in the Pear table
    public bool Ws = false; // the boolean to make wich weapon we are currently use  when we swap the weapon by pressing the BackQuote whether was by pressing or by holding the BackQuote
    public Text Bannana;
    public Text Apple;
    public Text Pear;
    public Text Bullets;
    public Text currentWeapons;
    private Animator anim;
    public GameObject Banana;
    public GameObject Bullet;
    GameObject instBullet;
    Rigidbody instBulletRigidbody;
    public Weaponsinfo Wi;
    public Weaponsinfo WElist;
    private void FixedUpdate()
    {
        delmovemen();
        delp();
        delt();
       if (Input.GetAxis("Jump") != 0 && isground())
        {
            deljump();
        }
    }
    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        delmovemen = movementall;
        delmovemen += isWalking;
        delmovemen += isRunning;
        njump = 1;
        deljump = jump;
        deljump += isJumping;
        delp += isPunching;
        delt += isThrowing;
        distancetotheground = Mathf.Clamp( distancetotheground ,0.4f, 0.51f);
        Snumofuse();
        timeBetweenshootmode = 2;
        canshoot = false;
    }
    void Update()
    {
        shoot();
        Wrange();
        TableText();
    }
    public Player()
    {

    }
    public void isWalking() // the condition to activate the animation for walking and turnningaround by using everyoune it is input axis
    {
            anim.SetFloat("Walk", Input.GetAxis("Vertical"));
            anim.SetFloat("turnaround", Input.GetAxis("Horizontal"));
    }
    public void isRunning() // the condition activate the running animation
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") != 0 || Input.GetKey(KeyCode.RightShift) && Input.GetAxis("Vertical") != 0)
        {
            anim.SetFloat("Walk", 2 * Input.GetAxis("Vertical"));
        }
    }
    public void isAttacking()
    {
        // condition for 2 move in layer 2 in Animation
        // when we walking and we do the seccond move and when we are running
        //the isAttacking trigger is working in this method
        if (Input.GetAxis("Vertical") > 0.01 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") > 0.01 || Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }
    public void isJumping()
    {
        anim.SetTrigger("Jump");
    }
    public void isPunching() // when we press the Tab button the animation of the punching will be activated
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            anim.SetTrigger("Punch");
        }
        isAttacking();
    }
    public void isThrowing() // when we press Q the animation of the throwing will be activated
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("Throw");
        }
        isAttacking();
    }
    public bool isground() // the condition for Jump we put ray to check the distance between the character and the floor if it is the length is hit the floor we can Jump else we can not because the player is flying
    {
        return Physics.Raycast(transform.position, Vector3.down, distancetotheground);
    }
    public void dyingmsg() // a massege will appear in the Debug if the Character die
    {
        if (health <= 0)
        {
            Debug.Log("Character die");
        }
    }
    public void shoot()
    {
        //changing weapons function
        SwitchingW();
        //shoot function
        if (Input.GetKeyDown(KeyCode.E) && iscreate==false) //aim
        {
            if (Wi.weaponsList[i].numOfUse > 0)
            {
                instBullet = Instantiate(Wi.weaponsList[i].weaponObject, Bullet.transform.position, Wi.weaponsList[i].weaponObject.transform.rotation, Bullet.transform) as GameObject;
                //Debug.Log("Wepon inistiate");
                if (instBullet.GetComponent<Rigidbody>() == null)
                {
                    instBullet.AddComponent<Rigidbody>();
                    instBullet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                }
                instBullet.transform.position = Bullet.transform.position;
                iscreate = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && iscreate==true)
        {
            anim.SetTrigger("Throw");
            instBullet.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * shootspeed, ForceMode.Impulse);
            Wi.weaponsList[i].numOfUse -= 1;
            instBullet.transform.SetParent(null);
            iscreate = false;
        }
        if(Input.GetKey(KeyCode.Q) && i == 1 && Wi.weaponsList[i].numOfUse>0) //apple will work as a machine gun by holding the Q button
        {
            instBullet = Instantiate(Wi.weaponsList[i].weaponObject, Bullet.transform.position, Wi.weaponsList[i].weaponObject.transform.rotation, Bullet.transform) as GameObject;
            if (instBullet.GetComponent<Rigidbody>() == null)
            {
                instBullet.AddComponent<Rigidbody>();
                instBullet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            }
            instBullet.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * shootspeed, ForceMode.Impulse);
            Wi.weaponsList[i].numOfUse -= 1;
            instBullet.transform.SetParent(null);
        }
        // by using the timer method
        if ( iscreate ==true)    // in each seccond we will check enter to see the condition
        {
            myFloat += Time.deltaTime;
            if (myFloat >= 1)
            {
                if (transcurrido == tiempoEntreMens) // every seccond we are checking the condition if it is not equal we increse the counter in reset the float
                {
                    // do if the time come after n secconds
                    iscreate = false;
                    transcurrido = 0; //make the pointer 0 to reset it
                    return; /// it will return to the first if
                }
                transcurrido++;
                myFloat = 0; //we will reset it because the transcurrido here will count the secconds assummed
            }
        }
        else
        {
            transcurrido = 0;
        }
        Destroy(instBullet, 3); // to force the bullet object to be destroyed
    }
    public void Timer() // is timer to controll the shooting time for the enemy
    {
        myFloat += Time.deltaTime;
        if (myFloat >= 1)    // in each seccond we will check enter to see the condition
        {
            if (transcurrido == tiempoEntreMens) // every seccond we are checking the condition if it is not equal we increse the counter in reset the float
            {
                // do if the time come after n secconds
                transcurrido = 0; //make the pointer 0 to reset it
                return; /// it will return to the first if
            }
            transcurrido++;
            myFloat = 0; //we will reset it because the transcurrido here will count the secconds assummed
        }
    }
        public void Snumofuse()
    {
        // to mkae all the weapons at the beginning is zero (to reset it at the beginning of the Game)
        for(int i = 0; i<Wi.weaponsList.Count; i++)
        {
            Wi.weaponsList[i].numOfUse = 0;
        }
    }
    public void Wrange() // to put the range of all weapens from 0 to 100 it is the range of the use not the range of the distance of shooting
    {
        for (int i = 0; i < Wi.weaponsList.Count; i++)
        {
            Wi.weaponsList[i].numOfUse = Mathf.Clamp(Wi.weaponsList[i].numOfUse, 0, 100); // the range of number of using for the weapon
        }
    }
   
    public void SwitchingW() // method to change the weapon we are using it is used in the shooting method
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            i++;
            if (i >= Wi.weaponsList.Count)
            {
                Debug.Log(i);
                i = 0;
            }
        }
    }
    void OnTriggerStay(Collider collision)
    {
        // if the character collide with the table that have weapon it will increased the number of the weapon in that table buy using the trigger the character will go inside the table
        if (collision.gameObject.tag == "TB")
        {
            BB = true;
            Debug.Log("Hit detect with Table Banana");
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Wi.weaponsList[0].numOfUse += 1;
            }
        }
        if (collision.gameObject.tag == "TA")
        {
            AA = true;
            Debug.Log("Hit detect Table Apple");
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Wi.weaponsList[1].numOfUse += 1;
            }
        }
        if (collision.gameObject.tag == "TP")
        { 
            PP = true;
            Debug.Log("Hit detect with Table Pear ");
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Wi.weaponsList[2].numOfUse += 1;
            }
        }
    }
    private void OnTriggerExit(Collider collision) // when we are outside from the tables we make the boolean condition is false
    {
        if (collision.gameObject.tag == "TB")
        {
            BB = false;
        }
        if (collision.gameObject.tag == "TA")
        {
            AA = false;
        }
        if (collision.gameObject.tag == "TP")
        {
            PP = false;
        }
    }
    private void OnTriggerEnter(Collider collision) // this if the weapon is enter the collider of the character
    {
        //we devide the damage / 2 because for strange reason is collide 2 times nut in the enemy is weork well
        if (collision.gameObject.tag == "EnemyBannanaWeapon" )
        {
            Debug.Log(collision.transform.name);
            Destroy(collision.gameObject);
            takedamage(WElist.weaponsList[0].weaponDamagae/2);
            Debug.Log("the player have taken bannana Damage = " + WElist.weaponsList[0].weaponDamagae/2);
            Debug.Log("the new health is =" + health);
            Debug.Log("You have been hitting by Enemy Bannana Weapons");
        }
        if (collision.gameObject.tag == "EnemyAppleWeapon")
        {
            Destroy(collision.gameObject);
            takedamage(WElist.weaponsList[1].weaponDamagae/2);
            Debug.Log("the player have taken Apple Damage = " + WElist.weaponsList[1].weaponDamagae/2);
            Debug.Log("the new health is =" + health);
            Debug.Log("You have been hitting by Enemy Apple Weapons");
        }
        if (collision.gameObject.tag == "EnemyPearWeapon")
        {
            Destroy(collision.gameObject);
            Destroy(collision.gameObject);
            takedamage(WElist.weaponsList[2].weaponDamagae/2);
            Debug.Log("the player have taken Pear Damage = " + WElist.weaponsList[2].weaponDamagae/2);
            Debug.Log("the new health is =" + health);
            Debug.Log("You have been hitting by Enemy Pear Weapons");
        }
    }
    public void TableText() // method for all Texts in the Game to make it apears and dissapperas when we activate it is conditon then it is disapper
    {
        Bullets.text = ("Weapons \nBannana : " + Wi.weaponsList[0].numOfUse + "\nApple       : " + Wi.weaponsList[1].numOfUse + "\nPear         : " + Wi.weaponsList[2].numOfUse);
        currentWeapons.text = ("your current weapon is " + Wi.weaponsList[i].weaponName);
        if (BB)
        {
            Bannana.gameObject.SetActive(true);
        }
        else
        {
            Bannana.gameObject.SetActive(false);
        }
        if (AA)
        {
            Apple.gameObject.SetActive(true);
        }
        else
        {
            Apple.gameObject.SetActive(false);

        }
        if (PP)
        {
            Pear.gameObject.SetActive(true);
        }
        else
        {
            Pear.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKey(KeyCode.BackQuote))
        {
            currentWeapons.enabled = true;
        }
        else
        {
            currentWeapons.enabled = false;
        }
    }
    
}
