using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController10 : MonoBehaviour
{
    // Start is called before the first frame update
    public float JumpForce = 20;

    public float velocity = 10;
    public int Saltos;
    Rigidbody2D rb; 
    SpriteRenderer sr;
    Animator animator;

    
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CORRER = 2;
    const int ANIMATION_SALTAR = 3;
    const int ANIMATION_DESLIZAR = 4;
    const int ANIMATION_MORIR = 5;
    const int ANIMATION_CORRERDISPARAR = 7;
    const int ANIMATION_SALTARCUERPOCUERPO = 8;
    const int ANIMATION_TIROSALTO = 9;
    const int ANIMATION_SHOOTDISP = 10;
    const int ANIMATION_MELEEPLEACONF = 11;


    bool puedeSaltar = true;

    private int saltosHechos;
    public int limiteSaltos = 2;

    private Vector3 lastCheckpointPosition;
   
    void Start()
    {
        Debug.Log("Iniciamos script de player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log("Puede saltar"+puedeSaltar.ToString());
         puedeSaltar = true;

        //CORRER
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.X)){
            rb.velocity = new Vector2(-20, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_CORRER);
        }

        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.X)){
            rb.velocity = new Vector2(20, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_CORRER);
        }
        
        // //CAMINAR
        // else if (Input.GetKey(KeyCode.RightArrow)){
        //     rb.velocity = new Vector2(velocity, rb.velocity.y);  
        //     sr.flipX = false;
        //     ChangeAnimation(ANIMATION_CAMINAR);
        // }
        // else if (Input.GetKey(KeyCode.LeftArrow)){
        //     rb.velocity = new Vector2(-velocity, rb.velocity.y);
        //     sr.flipX = true;
        //     ChangeAnimation(ANIMATION_CAMINAR);
        // }

        //SALTAR
        else if(Input.GetKeyUp(KeyCode.Space)){
            if(saltosHechos<limiteSaltos){

                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                
                saltosHechos++;
                ChangeAnimation(ANIMATION_SALTAR);
                
            }
            
        }

        //DESLIZAR
       else if(Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_DESLIZAR);
        }

        //DEAD MORIR
       else if(Input.GetKeyUp(KeyCode.M))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_MORIR);
        }

        //CORRER Y DISPARAR


        //SALTAR CUERPO A CUERPO


        //TIRO YSALTO


        //QUIETO
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_QUIETO);
        }
    }

    
    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Puede saltar");
        puedeSaltar = true;
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("Estas muerto");
            } 
            if (other.gameObject.name == "DarkHole")
            {
                if (lastCheckpointPosition != null)
                {
                    transform.position = lastCheckpointPosition;
                }
            }

            if(other.collider.tag=="Tilemap"){
            saltosHechos = 0;  
        }
            
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger");
        lastCheckpointPosition = transform.position;
    }
   
    private void ChangeAnimation(int animation){
        animator.SetInteger("Estado", animation);

    }

}
