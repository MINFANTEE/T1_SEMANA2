using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NinjaHombreController : MonoBehaviour
{
   

    public float velocity=0, JumpForce=20,defaulVelocity=10;

    //bool puedeSaltar=true;

    private int saltosHechos;
    public int limiteSaltos = 2;

    ///pasar ecenna matando enemigos
    public int ZombiMuertos=0;
    public int conteoMonedas=0;
    private bool Flecha=false;

    ///////////usarcatanaa
    private bool usarCatana=false;

    private int salRealizados;
    private GameManagerT1 gameManager;
    public  int limSaltos;
    public GameObject bullet;
    public AudioClip saltarClip;
    public AudioClip balaClip;
    public AudioClip monedaClip;
    private MenuPrincipalInicio menuController; 


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
    const int ANIMACION_ATACAR=12;



    //private int balas=0;


    private Vector3 lastCheckpointPosition;

    void Start()
    {

        rb= GetComponent<Rigidbody2D>(); 
        sr=GetComponent<SpriteRenderer>(); 
        animator=GetComponent<Animator>();
        audioSource=GetComponent<AudioSource>();
        gameManager=FindObjectOfType<GameManagerT1>();
        menuController=FindObjectOfType<MenuPrincipalInicio>();
        
    }

    void Update()
    {     

        Debug.Log("sombissmuertos" + ZombiMuertos);



        // if(ZombiMuertos==2){
        //     SceneManager.LoadScene(0);
        // }
        ///pasar ecenna matando enemigos

        // if(ZombiMuertos==1 && Flecha==true){
        //     SceneManager.LoadScene(0);
        // }
       
        // CAMINAR FLECHAS
         if (Input.GetKeyDown(KeyCode.LeftArrow))
        {         
            WalkToleft();
        }        
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {          
            StopWalk();

        }
        
        
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {        
            WalkRight();
        }        
        else if (Input.GetKey(KeyCode.RightArrow))
        {          
            StopWalk();  
        }


        //Deslizarse
        else if(Input.GetKey(KeyCode.C)){
                CambiarAnimacion(ANIMACION_DESLIZAR);
        }


        //Morir
        // else if(Input.GetKey(KeyCode.D)){
        //         CambiarAnimacion(ANIMACION_MORIR);
        // }

        // hacer saltar
         else if (Input.GetKeyDown(KeyCode.Space)){                 
            Jump();
        }
        else if (Input.GetKey(KeyCode.P))   
        {          
            rb.velocity = new Vector2(0, rb.velocity.y);
            Attack();  
        }

        //ATACAR
        else if(Input.GetKey(KeyCode.Z)){

            CambiarAnimacion(ANIMACION_ATACAR);
        }

        else if(gameManager.lives == 0){

            rb.velocity = new Vector2(0, rb.velocity.y);
            CambiarAnimacion(ANIMACION_MORIR);

            }


        else {
        //Cuando aplasto avanza (parado)
        rb.velocity= new Vector2(0,rb.velocity.y);
        //CambiarAnimacion(ANIMACION_QUIETO);

        }

        Walk();
                   
    }

//------------------------------------------------------------------------


    //ver con quien colisiona
    void OnCollisionEnter2D(Collision2D other) {

        //puedeSaltar=true;
        if(other.gameObject.tag == "DarkHole"){            
            //regresar a la ultima posiion de checkpoint
            if(lastCheckpointPosition != null){
                transform.position= lastCheckpointPosition;
            }
        }

        //saltar
        if(other.collider.tag=="Tilemap"){
          saltosHechos= 0;  
        }  

         if(other.gameObject.tag=="crecerPlayer") {

            transform.localScale=new Vector3(2,2,1.5f);
        }

        if(other.gameObject.tag=="reducirPlayer") {

            transform.localScale=new Vector3(0.98f,0.82f,1);
        }    

        //COGER MONEDAS
        if(other.gameObject.tag == "Moneda"){
            
            Destroy(other.gameObject);
            gameManager.GanarMonedas(1);
            audioSource.PlayOneShot(monedaClip);
            
        }

        if(other.gameObject.tag == "Moneda2"){
            
            Destroy(other.gameObject);
            gameManager.GanarMonedas2(20);
            audioSource.PlayOneShot(monedaClip);
            
        }

        if(other.gameObject.tag == "Moneda3"){
            
            Destroy(other.gameObject);
            gameManager.GanarMonedas3(30);
            audioSource.PlayOneShot(monedaClip);
            
        }

        //destruir enemigo
        if(other.gameObject.tag == "Enemy"){

            gameManager.PerderVida();
            //Destroy(other.gameObject);      
        }

        if(other.gameObject.tag== "Enemy" && usarCatana == true){
            Destroy(other.gameObject);
            gameManager.GanarPuntos(2);
            gameManager.SaveGame();
        }
    }

    //
    private void OnTriggerEnter2D(Collider2D other) {
        lastCheckpointPosition= transform.position;

        //////////////////////////////////////////
        gameManager.SaveGame();

        if(other.gameObject.name == "Flecha" && ZombiMuertos==10){
             // Flecha=true;

            SceneManager.LoadScene(2);

        } 

    }

    // met cambiar animaciones
    void CambiarAnimacion(int animation){

        animator.SetInteger("Estado",animation);
    }


//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

    public void Deslizar(){


    }



    public void Attack(){

        if(sr.flipX == true && menuController.next==1){
        var bulletPosition=transform.position+new Vector3(-3,0,0);
        var gb=Instantiate(bullet, bulletPosition,Quaternion.identity)as GameObject;
        var controller=gb.GetComponent<BalaController>();
        controller.SetLeftDirection();
        controller.SetDanio(1);
        audioSource.PlayOneShot(balaClip);
        }
        if(sr.flipX == false&& menuController.next==1){
        var bulletPosition=transform.position+new Vector3(3,0,0);
        var gb=Instantiate(bullet, bulletPosition,Quaternion.identity)as GameObject;
        var controller=gb.GetComponent<BalaController>();
        controller.SetRightDirection();
        controller.SetDanio(1);
        audioSource.PlayOneShot(balaClip);
        }

        if(menuController.next==0){
            usarCatana=true;
            CambiarAnimacion(ANIMACION_ATACAR);

        }

    }

     //STOP SALTAR
    public void StopWalk(){
        velocity=0;
        usarCatana=false;
        CambiarAnimacion(ANIMACION_QUIETO);
    } 

    public void Jump(){
        
         if (saltosHechos < limiteSaltos)
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            saltosHechos++;
            CambiarAnimacion(ANIMACION_SALTAR);
        }
        
    }

    //CAMINAR DERECHA
    public void WalkRight(){
        velocity=defaulVelocity;
        CambiarAnimacion(ANIMACION_CORRER);
        
    } 

    // CAMINAR IZQUIEDA
    public void WalkToleft(){
        velocity=-defaulVelocity;
        CambiarAnimacion(ANIMACION_CORRER);

    }

     //CAMINAR
    public void Walk(){
        rb.velocity= new Vector2(velocity, rb.velocity.y);
        if(velocity<0){
            sr.flipX=true;
        }
        if(velocity>0){
            sr.flipX=false;
        }
    }






}
