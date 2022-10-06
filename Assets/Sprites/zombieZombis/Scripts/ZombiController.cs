using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiController : MonoBehaviour
{
    public float velocity =-0.5f;

    public float vidaEnemigo=4;
    
    Rigidbody2D rb; 
    SpriteRenderer sr; 
    Animator animator;   

    const int ANIMACION_QUIETO=0;
    const int ANIMACION_CORRER=1;

    private GameManagerT1 gameManager;


    void Start()
    {


        rb=GetComponent<Rigidbody2D>();
        sr=GetComponent<SpriteRenderer>();
        animator=GetComponent<Animator>();
        gameManager= FindObjectOfType<GameManagerT1>();

        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-velocity, rb.velocity.y);
        sr.flipX = true;
        cambiarAnimacion(ANIMACION_CORRER);       

        if (vidaEnemigo==0){

            Destroy(this.gameObject);
        }

    }

    public void Ataque(int ataque){
        vidaEnemigo-=ataque;
        Debug.Log("las vidas del enemigo son: " +vidaEnemigo);
    }

    void cambiarAnimacion(int animacion){
        animator.SetInteger("Estado",animacion);
    }
}