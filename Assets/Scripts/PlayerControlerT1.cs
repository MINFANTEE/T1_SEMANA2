using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlerT1 : MonoBehaviour
{
    //MALONI

    public float velocity =10, jumpForce=5;
    //bool puedeSaltar = true;

    public GameObject bullet;
    
    Rigidbody2D rb; 
    SpriteRenderer sr; 
    Animator animator;
    

    const int ANIMACION_QUIETO=0;
    const int ANIMACION_CORRER=1;
    const int ANIMACION_ATACAR=2;
    const int ANIMACION_CAMINAR=3;
    const int ANIMACION_SALTAR=4;
    const int ANIMACION_MORIR=5;

    private Vector3 lastCheckpointPosition;
    private int salRealizados;
    public  int limSaltos;

    private GameManagerT1 gameManager;


    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Empezando juego");

        rb=GetComponent<Rigidbody2D>();
        sr=GetComponent<SpriteRenderer>();
        animator=GetComponent<Animator>();
        gameManager= FindObjectOfType<GameManagerT1>();
        
    }

    // Update is called once per frame
    void Update()
    {

       

        // //puedeSaltar = true;  //SALTAR MAS DE DOS VECES 
        // Debug.Log("Puede saltar"+puedeSaltar.ToString());

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

        //SALTAR   2 saltos

        else if(Input.GetKeyDown(KeyCode.Space)){
                    if(salRealizados<limSaltos){
                    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    salRealizados++;     

                    }
                    cambiarAnimacion(ANIMACION_SALTAR);
                }

       // disparar vala x
        else if(Input.GetKeyUp(KeyCode.O)&& sr.flipX==true){

            var bulletPosition=transform.position+new Vector3(-3,0,0);
            var gb=Instantiate(bullet, bulletPosition,Quaternion.identity)as GameObject;
            var controller=gb.GetComponent<BalaController>();
            controller.SetLeftDirection();

        }
        // disparar vala P
        else if(Input.GetKeyUp(KeyCode.O)&& sr.flipX==false){

            var bulletPosition=transform.position+new Vector3(3,0,0);
            var gb=Instantiate(bullet, bulletPosition,Quaternion.identity)as GameObject;
            var controller=gb.GetComponent<BalaController>();
            controller.SetRightDirection();

        }
        //ATACAR
        else if(Input.GetKey(KeyCode.Z)){

            cambiarAnimacion(ANIMACION_ATACAR);
        }

        // //MORIR
        // else if(Input.GetKey(KeyCode.Z)){

        //     cambiarAnimacion(ANIMACION_MORIR);
        // }

        //MORIR
        else if(gameManager.livesText.text=="Fin Juego"){
            
            rb.velocity =new Vector2(0,rb.velocity.y);
            cambiarAnimacion(ANIMACION_MORIR);
        }
             

        else{

            //CUANDO NO HACE NADA ESTA QUIETO
            rb.velocity = new Vector2(0, rb.velocity.y);
             cambiarAnimacion(ANIMACION_QUIETO);

            //CORRE AL INICIAR 
            // rb.velocity =new Vector2(5,rb.velocity.y);
            // sr.flipX=false;
            // cambiarAnimacion(ANIMACION_CORRER);
        }

    }
    

    private void OnTriggerEnter2D(Collider2D other) {
        lastCheckpointPosition=transform.position;
        
    }

    void OnCollisionEnter2D(Collision2D other){

        Debug.Log("Puede saltar");
        //puedeSaltar=true;

            if(other.gameObject.tag == "Enemy"){

                Debug.Log("Estas muerto");

                gameManager.PerderVida();


            }

            if(other.gameObject.tag =="DarkHole"){

                if(lastCheckpointPosition!=null){

                transform.position=lastCheckpointPosition;
            }



            }           
            //Saltar
            if(other.collider.tag =="Tilemap"){   
            salRealizados= 0;  
            }

    }
    void cambiarAnimacion(int animacion){
        animator.SetInteger("Estado",animacion);
    }
}


   
