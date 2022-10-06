using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BalaController : MonoBehaviour
{
    public float velocity=20;
    float realVelocity;

    Rigidbody2D rb;
    SpriteRenderer sr;
    public int danio=1;

    private GameManagerT1 gameManager;


    public void SetRightDirection(){

        realVelocity=velocity;
    }
               
    public void SetLeftDirection(){

        realVelocity=-velocity;
    }


    public void SetDanio(int d){
        danio=d;
    }
    
    void Start()
    {
        rb= GetComponent<Rigidbody2D>(); 
        sr=GetComponent<SpriteRenderer>();
        gameManager= FindObjectOfType<GameManagerT1>();
        Destroy(this.gameObject,5);


    }

    void Update()
    {
        rb.velocity= new Vector2(realVelocity,0);

        if(realVelocity==-20){
            sr.flipX=true;
        }

    }

    void OnCollisionEnter2D(Collision2D other) {

        Destroy(this.gameObject); //Destrir al objeto y la bala  
          
        if(other.gameObject.tag== "Enemy"){

            other.gameObject.GetComponent<EnemyController>().Damage(danio);

            //Destroy(other.gameObject);// DESTRUIR DONDE CHOCA
            
            gameManager.GanarPuntos(2);
            gameManager.SaveGame();

        }    
    
    }
}
