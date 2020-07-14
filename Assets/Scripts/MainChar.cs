﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainChar : MonoBehaviour
{
    public static MainChar instace;
    public Rigidbody2D rb;
    public float spd;
    public bool run = false;
    public int idleT;
    public bool oso = true;
    [HideInInspector] public Vector2 move;
    public float dashVel;
    private float dashT;
    public float dashDur;
    private bool dash = true;
    private float dashChg;
    public float dashC;
    private float tmpSpd;
    private float slow;
    [HideInInspector] public char face;
    public Animator anim;
    void Start()
    {
        instace = this;
        dashT = dashDur + 1;
        tmpSpd = spd;
        anim.SetBool("ted", oso);
        anim.SetBool("run", run);
        anim.SetBool("box", false);
        anim.SetBool("push", false);
        anim.SetBool("light", false);
    }
    void Update()
    {
        move.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        dashChg += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && dash && dashChg >= dashC)
        {
            spd = tmpSpd * dashVel;
            dashChg = 0.0f;
        }
        if (spd < tmpSpd)
            tmpSpd -= slow;

    }

    void FixedUpdate()
    {

        if (move.y == 1 && move.x == 0)
        {
            anim.SetFloat("blend", 1);
            face = 'f';
        }
        if (move.y == -1 && move.x == 0)
        {
            anim.SetFloat("blend", 0);
            face = 'b';
        }
        if (move.x == 1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            anim.SetFloat("blend", 0.5f);
            face = 'r';
        }
        if (move.x == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            anim.SetFloat("blend", 0.5f);
            face = 'l';
        }
        
        if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0)//movimiento
            rb.MovePosition(rb.position + move.normalized * spd * Time.fixedDeltaTime);
        else
            rb.MovePosition(rb.position + move * spd * Time.fixedDeltaTime);
        if (dashT > dashDur) //Si el tiempo con Dash se vuelve mayor a la duración..
        {
            dashT = 0; //El tiempo de dash se queda en 0
            dash = false;
            spd = tmpSpd; //Velocidad base
        }
        if (dashT < dashDur)
        {  //Si el tiempo con Dash es menor a la duración..
            dash = true;
            slow = 1f;
        }
        dashT += Time.fixedDeltaTime;
        if (spd < tmpSpd)
            slow += Time.smoothDeltaTime;
    }
}
