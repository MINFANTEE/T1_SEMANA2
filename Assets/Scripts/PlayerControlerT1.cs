using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlerT1 : MonoBehaviour
{
    //MALONI

    public float velocity =10, jumpForce=5;
    bool puedeSaltar = true;

    
    Rigidbody2D rb; 
    SpriteRenderer sr; 
    Animator animator;

    const int ANIMACION_QUIETO=0;
    const int ANIMACION_CORRER=1;
    const int ANIMACION_ATACAR=2;
    const int ANIMACION_CAMINAR=3;
    const int ANIMACION_SALTAR=4;

    private Vector3 lastCheckpointPosition;


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

        //puedeSaltar = true;  //SALTAR MAS DE DOS VECES 
        Debug.Log("Puede saltar"+puedeSaltar.ToString());

        //MOVER DERECHA
        if(Input.GetKey(KeyCode.RightArrow)&& Input.GetKey("x") ){

            rb.velocity =new Vector2(20,rb.velocity.y);
            sr.flipX=false;
            cambiarAnimacion(ANIMACION_CORRER);
        }      
        
        //MOVER IZQUIERDA
        else if(Input.GetKey(KeyCode.LeftArrow)&& Input.GetKey("x")){

            rb.velocity =new Vector2(-20,rb.velocity.y);
            sr.flipX=true;
            cambiarAnimacion(ANIMACION_CORRER);
        }

//---------------------------------------------------------------------
        //MOVER DERECHA
        else if(Input.GetKey(KeyCode.RightArrow) ){

            rb.velocity =new Vector2(velocity,rb.velocity.y);
            sr.flipX=false;
            cambiarAnimacion(ANIMACION_CAMINAR);
        }      
        
        //MOVER IZQUIERDA
        else if(Input.GetKey(KeyCode.LeftArrow)){

            rb.velocity =new Vector2(-velocity,rb.velocity.y);
            sr.flipX=true;
            cambiarAnimacion(ANIMACION_CAMINAR);
        }
//---------------------------------------------------------------------

        //SALTAR
        else if(Input.GetKeyUp(KeyCode.Space)&& puedeSaltar){
            rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            puedeSaltar=false;
            cambiarAnimacion(ANIMACION_SALTAR);
        }

        //ATACAR
        else if(Input.GetKey(KeyCode.Z)){

            cambiarAnimacion(ANIMACION_ATACAR);
        }
             

        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
             cambiarAnimacion(ANIMACION_QUIETO);
        }

    }


    private void OnTriggerEnter2D(Collider2D other) {
        lastCheckpointPosition=transform.position;
        
    }

    void OnCollisionEnter2D(Collision2D other){

        Debug.Log("Puede saltar");
        puedeSaltar=true;

            if(other.gameObject.tag == "Enemy"){

                Debug.Log("Estas muerto");
            }

            if(other.gameObject.name =="DarkHole"){

            if(lastCheckpointPosition!=null){
                transform.position=lastCheckpointPosition;
            }

            }
//2checkpoint
//------------------------------------------
            if(other.gameObject.name =="DarkHole"){

            if(lastCheckpointPosition!=null){
                transform.position=lastCheckpointPosition;
            }

            }
            

//------------------------------------------




    }
    void cambiarAnimacion(int animacion){
        animator.SetInteger("Estado",animacion);
    }
}


   
