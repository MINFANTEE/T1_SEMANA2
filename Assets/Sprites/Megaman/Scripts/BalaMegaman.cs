using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaMegaman : MonoBehaviour
{
     public float velocity=20;
    float realVelocity;
    public int danio =1;

    Rigidbody2D rb;
    SpriteRenderer sr;

    private GameManagerT1 gameManager;


    public void SetRightDirection(){

        realVelocity=velocity;
    }
               
    public void SetLeftDirection(){

        realVelocity=-velocity;
    }

    public void Danio(int dan){

        danio=-dan;
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
            other.gameObject.GetComponent<ZombiController>().Ataque(danio);

        }    
    
    }
}
