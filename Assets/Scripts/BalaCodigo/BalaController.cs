using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BalaController : MonoBehaviour
{
    public float velocity=20;
    float realVelocity;

    Rigidbody2D rb;

        private GameManagerT1 gameManager;


    public void SetRightDirection(){

        realVelocity=velocity;
    }
               
    public void SetLeftDirection(){

        realVelocity=-velocity;
    }

    void Start()
    {
        rb= GetComponent<Rigidbody2D>(); 
        Destroy(this.gameObject,5);
                gameManager= FindObjectOfType<GameManagerT1>();

    }

    void Update()
    {
        rb.velocity= new Vector2(realVelocity,0);

    }

    void OnCollisionEnter2D(Collision2D other) {

        Destroy(this.gameObject); //Destrir al objeto y la bala    
        if(other.gameObject.tag== "Enemy"){
            Destroy(other.gameObject);// DESTRUIR DONDE CHOCA
            gameManager.GanarPuntos(10);

        }    
    
    }
}
