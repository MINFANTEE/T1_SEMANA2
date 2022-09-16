using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManagerT1 : MonoBehaviour
{
    public Text monedaText;
    public Text scoreText;
    public Text livesText;
    public Text balasText;


    private int moneda;
    private int score;
    private int lives;
    private int balas;


    void Start()
    {
        balas=5;
        score=0;
        lives=1;
        PrintScoreInScreen();
        PrintLivesInScreen();
        PrintBalasInScreen();
        LoadGame();
    }


     public void SaveGame()
    {
        var filePath=Application.persistentDataPath + "/save.dat";

        FileStream file;

        if (File.Exists(filePath))
            file=File.OpenWrite(filePath);

        else 
            file=File.Create(filePath);

        GameDataT1 data =new GameDataT1();
        data.Score=score;

        BinaryFormatter bf =new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();

    }

    public void LoadGame()
    {
        var filePath=Application.persistentDataPath + "/save.dat";

        FileStream file;

        if (File.Exists(filePath))
            file=File.OpenRead(filePath);

        else {
            Debug.LogError("No se encuentra el archivo");
            
            return;
        
        }

        BinaryFormatter bf =new BinaryFormatter();
        GameDataT1 data = (GameDataT1)bf.Deserialize(file);
        file.Close();

        score=data.Score;
        PrintScoreInScreen();

    }


    //
    public int Balas(){
        return balas;
    }

    public int Moneda(){
        return moneda;
    }

    public int Score(){
        return score;
    }

    public int Lives(){
        return lives;
    }


    //

    public void PerderBalas(){
        balas -=1;
        PrintBalasInScreen();
    }

    public void GanarMonedas(int money){
        moneda+=money;
        PrintMonedaInScreen();

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

    //
    private void PrintBalasInScreen(){
    balasText.text="Balas: "+balas;
   }

    private void PrintMonedaInScreen(){
        monedaText.text="Moneda tipo 1: "+ moneda;
    }


    private void PrintScoreInScreen(){
        scoreText.text="Puntaje: "+ score;
    }

    private void PrintLivesInScreen(){
        livesText.text="Vida: "+ lives;
    }
}
