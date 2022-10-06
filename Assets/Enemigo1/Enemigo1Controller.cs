using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1Controller : MonoBehaviour
{
    public float velocity =10;    
    Rigidbody2D rb; 

    public float vidaEnemigo=5;



    void Start()
    {
        rb=GetComponent<Rigidbody2D>();


        //APARECER ZOMBI
        StartCoroutine (PonerZombi ());

    }


    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-velocity, rb.velocity.y);

        if (vidaEnemigo<=0){
            Destroy(this.gameObject);
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

    public GameObject zombi;

    IEnumerator PonerZombi()
    {
    int segundos = Random.Range(2,3);

    yield return new WaitForSeconds(segundos);

    Instantiate (zombi,  transform.position, Quaternion.identity);

    StartCoroutine(PonerZombi());

    }


}
