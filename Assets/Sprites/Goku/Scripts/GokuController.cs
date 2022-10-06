using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GokuController : MonoBehaviour
{
    public float velocity=10;
    public GameObject bullet;


    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    private Vector2 direction;
    private bool tieneNube=false;
    private float defaulGravity;
    
 

    void Start()
    {

        rb= GetComponent<Rigidbody2D>(); 
        sr= GetComponent<SpriteRenderer>(); 
        defaulGravity=rb.gravityScale;
        animator=GetComponent<Animator>();
                
    }

    void Update()
    {     

       
       float x =Input.GetAxis("Horizontal");
       float y=Input.GetAxis("Vertical");   
       direction=new Vector2(x,y); 
       Run();
        

        // hacer que vuele el personaje

        if(Input.GetKey(KeyCode.UpArrow)&& tieneNube){
            rb.velocity=new Vector2(rb.velocity.x, velocity);
        }

        if(Input.GetKey(KeyCode.DownArrow)&& tieneNube){
            rb.velocity=new Vector2(rb.velocity.x, -velocity);
        }



        // disparar vala P
        else if(Input.GetKeyUp(KeyCode.M)&& sr.flipX==false){

            var bulletPosition=transform.position+new Vector3(3,0,0);
            var gb=Instantiate(bullet, bulletPosition,Quaternion.identity)as GameObject;
            var controller=gb.GetComponent<BalaController>();
            controller.SetRightDirection();

        }
        
    }

    private void Run(){

        rb.velocity =new Vector2 (direction.x * velocity, rb.velocity.y);
        sr.flipX=direction.x < 0;

    }

     void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.name == "nube"){
             rb.gravityScale=0;
            tieneNube=true;
            animator.SetInteger("Estado",1);
        } 

        if(other.gameObject.name == "Flecha"){
             
            SceneManager.LoadScene(3);
        } 




    }

    //ver con quien colisiona
    void OnCollisionEnter2D(Collision2D other) {

       // puedeSaltar=true;

        if(other.gameObject.tag == "SueloSal"){
            rb.gravityScale=defaulGravity;
            tieneNube=false;
            animator.SetInteger("Estado",0);
        }      

        
    }

   
}

