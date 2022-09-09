using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiController : MonoBehaviour
{
    public float velocity =-0.5f;
    
    Rigidbody2D rb; 
    SpriteRenderer sr; 
    Animator animator;   

    const int ANIMACION_QUIETO=0;
    const int ANIMACION_CORRER=1;

    void Start()
    {

        Debug.Log("Empezando juego");

        rb=GetComponent<Rigidbody2D>();
        sr=GetComponent<SpriteRenderer>();
        animator=GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-velocity, rb.velocity.y);
        sr.flipX = true;
        cambiarAnimacion(ANIMACION_CORRER);       

    }
    void cambiarAnimacion(int animacion){
        animator.SetInteger("Estado",animacion);
    }
}