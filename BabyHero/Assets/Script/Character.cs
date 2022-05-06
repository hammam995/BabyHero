using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float starthealth = 100; // the origin one
    public float health; // current health
    public float speed; // speed for movement and rotation
    public float turningspeed;
    public float jumpForce; // the mesure of jumping
    float rotationtime; // rotation time
    public bool onground; // to check if the character on the floor so he can jump
    public delegate void delegadem();
    public delegadem delmovemen; //delegade variable for movement and their animation
    public delegate void delegadej();
    public delegadej deljump; // it have the jump function and the jumping animation
    public delegate void delegadep();
    public delegadep delp; // it have the punch function which is only have the animation when we press on tab
    public delegate void delegades(float direction);
    public delegades dels;
    public delegate void delegadet();
    public delegadet delt; // is for throwinh animation when we press the button
    public Image healthbar; //for healthbar
    public Rigidbody rb;
    protected virtual void Start()
    {
        speed = 5;
        jumpForce = 7;
        health = starthealth;
    }
    void Update()
    {
        health = Mathf.Clamp(health, 0, 100);
    }
    public Character ()
    {
        speed = 5;
        jumpForce = 7;
        health = starthealth;
    }
    public void Characterr(float speed , float jumpForce , float health )
    {
        this.speed = speed = 5;
        this.jumpForce = jumpForce = 7;
        this.health = health = starthealth;
    }
    protected virtual void movementall() // method to walk in all destination
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            // side walking
            rotation(Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            // normal walking
            transform.Translate((Vector3.forward) * Input.GetAxis("Vertical") * Time.deltaTime * speed);
        }
    }
    protected virtual void jump() // method for jumping
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, jumpForce, 0 );
    }
    protected virtual void rotation(float direction) // turning around
    {
        if (direction != 0)
        {
            rotationtime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.AngleAxis(direction * speed, transform.up), rotationtime);
        }
    }
    protected  void takedamage(float amount) // if a character have a damage
    {
        health -= amount;
        healthbar.fillAmount = health / starthealth;
    }
}
