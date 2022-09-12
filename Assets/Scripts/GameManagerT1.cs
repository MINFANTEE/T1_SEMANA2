using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerT1 : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;


    private int score;
    private int lives;


    void Start()
    {
        score=0;
        lives=1;
        PrintScoreInScreen();
        PrintLivesInScreen();
    }

    public int Score(){
        return score;
    }

    public int Lives(){
        return lives;
    }

    public void GanarPuntos(int puntos){
        score+=puntos;
        PrintScoreInScreen();

    }

    public void PerderVida(){
        lives-= 1;
        PrintLivesInScreen();

        if(lives==0){
            livesText.text="Fin Juego";

        }
    }

    private void PrintScoreInScreen(){
        scoreText.text="Puntaje: "+ score;
    }

    private void PrintLivesInScreen(){
        livesText.text="Vida: "+ lives;
    }
}
