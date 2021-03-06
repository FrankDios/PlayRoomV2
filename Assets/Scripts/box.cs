﻿using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    private bool usable = false;
    private bool holded = false;
    public Collider2D hitbox;
    public Collider2D grab;
    public CircleCollider2D detect;
    public Rigidbody2D rb;
    public float level;
    public Sprite caja;
    public Sprite borde;
    public Animator anim;
    GameObject Player;
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = caja;

    }

    // Update is called once per frame
    void Update()
    {

        if (usable && Input.GetKeyDown(KeyCode.E))
        {
            Player.GetComponent<MainChar>().anim.SetBool("box", true);
            Player.GetComponent<MainChar>().box = true;
            Player.GetComponent<MainChar>().anim.SetFloat("boxNum", level);
            Player.GetComponent<SpriteRenderer>().flipX = false;
            usable = false;
            holded = true;
            hitbox.enabled = false;
            grab.enabled = false;
            detect.enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (holded && Input.GetKeyDown(KeyCode.E) && usable == false && Player.GetComponent<Animator>().GetBool("push") == false)
        {
            hitbox.enabled = true;
            holded = false;
            detect.enabled = true;
            Player.GetComponent<MainChar>().anim.SetBool("box", false);
            //Player.GetComponent<MainChar>().anim.PlayInFixedTime(Player.GetComponent<MainChar>().anim.GetCurrentAnimatorClipInfo(0)[0].clip.name, 8, 0f);
            GetComponent<SpriteRenderer>().enabled = true;
            Player.GetComponent<MainChar>().box = false;
            switch (Player.GetComponent<MainChar>().face)
            {
                case ('r'):
                    {
                        rb.AddForce(Vector3.right * 6969);
                        break;
                    }
                case ('l'):
                    {
                        rb.AddForce(Vector3.left * 6969);
                        break;
                    }
                case ('f'):
                    {
                        rb.AddForce(Vector3.up * 6969);
                        break;
                    }
                case ('b'):
                    {
                        rb.AddForce(Vector3.down * 6969);
                        break;
                    }
            }
            //Player.GetComponent<MainChar>().anim.PlayInFixedTime()
        }
        if (holded)
        {
            switch (Player.GetComponent<MainChar>().face)
            {
                case ('r'):
                    {
                        gameObject.transform.position = Player.transform.position + Vector3.right;
                        break;
                    }
                case ('l'):
                    {
                        gameObject.transform.position = Player.transform.position + Vector3.left;
                        break;
                    }
                case ('f'):
                    {
                        gameObject.transform.position = Player.transform.position + Vector3.up;
                        break;
                    }
                case ('b'):
                    {
                        gameObject.transform.position = Player.transform.position + Vector3.down;
                        break;
                    }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && Vector2.Distance(other.gameObject.transform.position, gameObject.transform.position) < 2f)
        {
            Player = other.gameObject;
            usable = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = borde;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player" )
        {
            usable = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = caja;
        }
    }
    void play(){

    }
}
