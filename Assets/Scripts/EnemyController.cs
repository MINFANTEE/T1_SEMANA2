using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public float velocity =10;    
    Rigidbody2D rb; 

    public float vidaEnemigo=4;


    ///pasar ecenna matando enemigos
    private NinjaHombreController ninjaHombreController;



    void Start()
    {
        rb=GetComponent<Rigidbody2D>();

        ///pasar ecenna matando enemigos
        ninjaHombreController=FindObjectOfType<NinjaHombreController>();


        //APARECER ZOMBI
        // StartCoroutine (PonerZombi ());

    }


    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-velocity, rb.velocity.y);

        if (vidaEnemigo<=0){

            Destroy(this.gameObject);
            ///pasar ecenna matando enemigos
            ninjaHombreController.ZombiMuertos++;
        }

    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Limite"){
            velocity *=-1;

        }
    }

    public void Damage(int a){
        vidaEnemigo -=a;

    }

    //APARECER ZOMBI

    // public GameObject zombi;

    // IEnumerator PonerZombi()
    // {
    // int segundos = Random.Range(2,3);

    // yield return new WaitForSeconds(segundos);

    // Instantiate (zombi,  transform.position, Quaternion.identity);

    // StartCoroutine(PonerZombi());

    // }


}
