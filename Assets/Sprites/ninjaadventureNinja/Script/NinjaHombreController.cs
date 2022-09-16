using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaHombreController : MonoBehaviour
{
   

    public float velocity=10, jumpForce=5;

    bool puedeSaltar=true;
    private int salRealizados;
    private GameManagerT1 gameManager;
    public  int limSaltos;
    public GameObject bullet;
    public AudioClip saltarClip;
    public AudioClip balaClip;
    public AudioClip monedaClip;
    


    AudioSource audioSource;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    const int ANIMACION_QUIETO=0;
    const int ANIMACION_CORRER=2;
    const int ANIMACION_SALTAR=3;
    const int ANIMACION_CAMINAR=3;
    const int ANIMACION_DESLIZAR=4;
    const int ANIMACION_MORIR=5;


    private int balas=0;


    private Vector3 lastCheckpointPosition;

    void Start()
    {

        rb= GetComponent<Rigidbody2D>(); 
        sr=GetComponent<SpriteRenderer>(); 
        animator=GetComponent<Animator>();
        audioSource=GetComponent<AudioSource>();
        gameManager=FindObjectOfType<GameManagerT1>();
        
    }

    void Update()
    {     

       
        //Correr MAS X
        if (Input.GetKey(KeyCode.RightArrow)&& Input.GetKey("x"))
        {            
            rb.velocity= new Vector2(20, rb.velocity.y);
            sr.flipX=false;
            CambiarAnimacion(ANIMACION_CORRER);
        }
        
        else if (Input.GetKey(KeyCode.LeftArrow)&& Input.GetKey("x"))
        {            
            rb.velocity= new Vector2(-20, rb.velocity.y);
            sr.flipX=true;
            CambiarAnimacion(ANIMACION_CORRER);
        } 

        // CAMINAR FLECHAS
        else if (Input.GetKey(KeyCode.RightArrow))
        {            
            rb.velocity= new Vector2(velocity, rb.velocity.y);
            sr.flipX=false;
            CambiarAnimacion(ANIMACION_CAMINAR);
        }
        
        else if (Input.GetKey(KeyCode.LeftArrow))
        {            
            rb.velocity= new Vector2(-velocity, rb.velocity.y);
            sr.flipX=true;
            CambiarAnimacion(ANIMACION_CAMINAR);
        }

        // salto 2 veces
        else if(Input.GetKeyDown(KeyCode.Space)){
                if(salRealizados<limSaltos){
                    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    salRealizados++;
                    }                
                    CambiarAnimacion(ANIMACION_SALTAR);
                    audioSource.PlayOneShot(saltarClip);
            }

        //Deslizarse
        else if(Input.GetKey(KeyCode.C)){
                CambiarAnimacion(ANIMACION_DESLIZAR);
        }


        //Morir
        else if(Input.GetKey(KeyCode.D)){
                CambiarAnimacion(ANIMACION_MORIR);
        }

        // disparar vala x
        else if(Input.GetKeyUp(KeyCode.M)&& sr.flipX==true &&balas<5){

            var bulletPosition=transform.position+new Vector3(-3,0,0);
            var gb=Instantiate(bullet, bulletPosition,Quaternion.identity)as GameObject;
            var controller=gb.GetComponent<BalaController>();
            controller.SetLeftDirection();
            audioSource.PlayOneShot(balaClip);
            gameManager.PerderBalas();
            balas++;
        }
        // disparar vala P
        else if(Input.GetKeyUp(KeyCode.M)&& sr.flipX==false  &&balas<5){

            var bulletPosition=transform.position+new Vector3(3,0,0);
            var gb=Instantiate(bullet, bulletPosition,Quaternion.identity)as GameObject;
            var controller=gb.GetComponent<BalaController>();
            controller.SetRightDirection();
            audioSource.PlayOneShot(balaClip);
            gameManager.PerderBalas();
            balas++;

        }

        else {
        
        //Cuando aplasto avanza (parado)
        rb.velocity= new Vector2(0,rb.velocity.y);
        CambiarAnimacion(ANIMACION_QUIETO);

        }
                
        


        
    }

//------------------------------------------------------------------------


    //ver con quien colisiona
    void OnCollisionEnter2D(Collision2D other) {

        puedeSaltar=true;

        if(other.gameObject.tag == "DarkHole"){
            
            //regresar a la ultima posiion de checkpoint
            if(lastCheckpointPosition != null){
                transform.position= lastCheckpointPosition;
            }
        }

        //saltar
        if(other.collider.tag=="SueloSal"){
          salRealizados= 0;  
        }  

         if(other.gameObject.tag=="crecerPlayer") {

            transform.localScale=new Vector3(2,2,1.5f);
        }

        if(other.gameObject.tag=="reducirPlayer") {

            transform.localScale=new Vector3(0.98f,0.82f,1);
        }    

        if(other.gameObject.tag == "Moneda"){
            
            Destroy(other.gameObject);
            gameManager.GanarMonedas(20);
            audioSource.PlayOneShot(monedaClip);
            
        }
        
         

        
    }

    //
    private void OnTriggerEnter2D(Collider2D other) {
        lastCheckpointPosition= transform.position;
    }

    // met cambiar animaciones
    void CambiarAnimacion(int animation){

        animator.SetInteger("Estado",animation);
    }

}
