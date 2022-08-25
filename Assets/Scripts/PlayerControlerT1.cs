using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlerT1 : MonoBehaviour
{

    public float velocity =10, jumpForce=5;
    bool puedeSaltar = true;

    
    Rigidbody2D rb; 
    SpriteRenderer sr; 
    Animator animator;

    const int ANIMACION_QUIETO=0;
    const int ANIMACION_CORRER=1;
    const int ANIMACION_ATACAR=2;
    const int ANIMACION_saltar=4;

    // Start is called before the first frame update
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

        puedeSaltar = true;
        Debug.Log("Puede saltar"+puedeSaltar.ToString());

        if(Input.GetKey(KeyCode.RightArrow) ){

            rb.velocity =new Vector2(velocity,rb.velocity.y);
            sr.flipX=false;
            cambiarAnimacion(ANIMACION_CORRER);
        }      
        

        else if(Input.GetKey(KeyCode.LeftArrow)){

            rb.velocity =new Vector2(-velocity,rb.velocity.y);
            sr.flipX=true;
            cambiarAnimacion(ANIMACION_CORRER);
        }

        else if(Input.GetKeyUp(KeyCode.Space)&& puedeSaltar){
            rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            puedeSaltar=false;
        }

        //Atacar
        else if(Input.GetKey(KeyCode.Z)){

            cambiarAnimacion(ANIMACION_ATACAR);
        }
        //saltar
        else if(Input.GetKey(KeyCode.Space)){

            cambiarAnimacion(ANIMACION_saltar);
        }
       

        else{

            rb.velocity = new Vector2(0, rb.velocity.y);
             cambiarAnimacion(ANIMACION_QUIETO);


        }

    }

    void OnCollisionEnter2D(Collision2D other){

        Debug.Log("Puede saltar");
        puedeSaltar=true;

            if(other.gameObject.tag == "Enemy"){

                Debug.Log("Estas muerto");
            }
    }
    void cambiarAnimacion(int animacion){
        animator.SetInteger("Estado",animacion);
    }
}


   
