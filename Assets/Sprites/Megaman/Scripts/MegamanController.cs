using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanController : MonoBehaviour
{
   //MALONI

    public float velocity =10, jumpForce=5;
    //bool puedeSaltar = true;

    //public GameObject bullet;
    public GameObject bullet2;

    public GameObject priBala;
    public GameObject segBala;
    public GameObject terBala;


    private float tiempo=0;
    Rigidbody2D rb; 
    SpriteRenderer sr; 
    Animator animator;
    

    const int ANIMACION_QUIETO=0;
    const int ANIMACION_CORRER=2;
    
    const int ANIMACION_SALTAR=3;
    const int ANIMACION_ENERGIA=7;
    const int ANIMACION_ATACAR=9;
    const int ANIMACION_COLOR=10;



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

       //CORRER DERECHA
        if(Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(velocity,rb.velocity.y);
            cambiarAnimacion(ANIMACION_CORRER);
            sr.flipX = false;
        }

        //CORRER IZQUIERDA
        if(Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-velocity,rb.velocity.y);
            cambiarAnimacion(ANIMACION_CORRER);
            sr.flipX = true;
        }
        //SALTAR   2 saltos

        else if(Input.GetKeyDown(KeyCode.Space)){
                    if(salRealizados<limSaltos){
                    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    salRealizados++;     

                    }
                    cambiarAnimacion(ANIMACION_SALTAR);
                }

        //CARGAR ENERGIA DISPARAR
        else if(Input.GetKey(KeyCode.X)){

            cambiarAnimacion(ANIMACION_ENERGIA);
            cambiarAnimacion(ANIMACION_COLOR);

            tiempo+=Time.deltaTime;
            Debug.Log("Tiempo: " + tiempo);

        }

        if(tiempo < 1){
            if(Input.GetKeyUp(KeyCode.X) && sr.flipX == false){
                cambiarAnimacion(ANIMACION_ATACAR);
                var priBalaPosition = transform.position + new Vector3(3,0,0);
                var gb = Instantiate(priBala, priBalaPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<BalaMegaman>();
                controller.SetRightDirection();

            }
            if(Input.GetKeyUp(KeyCode.X) && sr.flipX == true){
                        cambiarAnimacion(ANIMACION_ATACAR);
                var priBalaPosition = transform.position + new Vector3(-3,0,0);
                var gb = Instantiate(priBala, priBalaPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<BalaMegaman>();
                controller.SetLeftDirection();
            }

        }
         else if(tiempo > 2 && tiempo < 4){
                if(sr.flipX == false && Input.GetKeyUp(KeyCode.X)){
                        cambiarAnimacion(ANIMACION_ATACAR);
                var segBalaPosition = transform.position + new Vector3(3,0,0);
                var gb = Instantiate(segBala, segBalaPosition, Quaternion.identity) as GameObject;
                gb.transform.localScale = new Vector3(5,5,5);
                var controller = gb.GetComponent<BalaMegaman>();
                controller.Danio(2);
                controller.SetRightDirection();
                }
                if(sr.flipX == true && Input.GetKeyUp(KeyCode.X)){
                cambiarAnimacion(ANIMACION_ATACAR);
                var priBalaPosition = transform.position + new Vector3(-3,0,0);
                var gb = Instantiate(segBala, priBalaPosition, Quaternion.identity) as GameObject;
                gb.transform.localScale = new Vector3(5,5,5);
                var controller = gb.GetComponent<BalaMegaman>();
                controller.Danio(2);
                controller.SetLeftDirection();
                }
            }
            else if(tiempo > 5){
                if(Input.GetKeyUp(KeyCode.X)){
                   if(sr.flipX == false && Input.GetKeyUp(KeyCode.X)){
                        cambiarAnimacion(ANIMACION_ATACAR);
                        var priBalaPosition = transform.position + new Vector3(3,0,0);
                        var gb = Instantiate(terBala, priBalaPosition, Quaternion.identity) as GameObject;
                        gb.transform.localScale = new Vector3(8,8,8);
                        var controller = gb.GetComponent<BalaMegaman>();
                        controller.Danio(3);
                        controller.SetRightDirection();
                    }
                    if(sr.flipX == true && Input.GetKeyUp(KeyCode.X)){
                        cambiarAnimacion(ANIMACION_ATACAR);
                        var terBalaPosition = transform.position + new Vector3(-3,0,0);
                        var gb = Instantiate(terBala, terBalaPosition, Quaternion.identity) as GameObject;
                        gb.transform.localScale = new Vector3(8,8,8);
                        var controller = gb.GetComponent<BalaMegaman>();
                        controller.Danio(3);
                        controller.SetLeftDirection();
                    } 
                }

             if(Input.GetKeyUp(KeyCode.X)){
                tiempo = 0;



        }


        }

        else{

            //CUANDO NO HACE NADA ESTA QUIETO
            rb.velocity = new Vector2(0, rb.velocity.y);
             cambiarAnimacion(ANIMACION_QUIETO);

            
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
            if(other.collider.tag =="SueloSal"){   
            salRealizados= 0;  
            }

    }
    void cambiarAnimacion(int animacion){
        animator.SetInteger("Estado",animacion);
    }
}
