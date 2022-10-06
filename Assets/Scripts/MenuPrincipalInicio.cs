using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPrincipalInicio : MonoBehaviour
{
    public SpriteRenderer srCharacter;
    public Sprite[] sprites;
    public int next=1;

    void Start()
    {            
       
    }



    // INIICO DE JUEGO
    public void PlayGame(){

        SceneManager.LoadScene(1);
    }

    // GUARDADO DE PUNTAJE
    public void StarPuntaje(){

        SceneManager.LoadScene(2);
    }


    //regresar menu desde juego
    public void RegresarMenu(){

        SceneManager.LoadScene(0);
    }


    //regresar menu desde puntaje guardado
    public void RegresarMenuDesdeGuardado(){

        SceneManager.LoadScene(0);
    }

    public void ChangeCharacter(){
        srCharacter.sprite=sprites[next];
        next++;
        if(next + 1 > sprites.Length){
            next=0;
        }
    }




}
