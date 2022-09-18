using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManagerT1 : MonoBehaviour
{
    public Text monedaText;//moneda1
    public Text moneda2Text;
    public Text moneda3Text;

    public Text scoreText;
    

    public Text livesText;
    public Text balasText;
    


    private int moneda;
    private int moneda2;
    private int moneda3;


    private int score;
    private int lives;
    private int balas;


    void Start()
    {
        balas=5;
        score=0;
        lives=1;

        moneda=0;
        moneda2=0;
        moneda3=0;      



        PrintScoreInScreen();
        PrintLivesInScreen();
        PrintBalasInScreen();

        PrintMonedaInScreen();
        PrintMoneda2InScreen();
        PrintMoneda3InScreen();
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

        data.Moneda=moneda;
        data.Moneda2=moneda2;
        data.Moneda3=moneda3;

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
        moneda=data.Moneda;
        moneda2=data.Moneda2;
        moneda3=data.Moneda3;
        PrintScoreInScreen();
        PrintMonedaInScreen();
        PrintMoneda2InScreen();
        PrintMoneda3InScreen();

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
    public int Moneda2(){
        return moneda2;
    }
    public int Moneda3(){
        return moneda3;
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

    public void GanarMonedas2(int money2){
        moneda2+=money2;
        PrintMoneda2InScreen();

    }

    public void GanarMonedas3(int money3){
        moneda3+=money3;
        PrintMoneda3InScreen();

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

    private void PrintMoneda2InScreen(){
        moneda2Text.text="Moneda tipo 2: "+ moneda2;
    }

    private void PrintMoneda3InScreen(){
        moneda3Text.text="Moneda tipo 3: "+ moneda3;
    }

    private void PrintScoreInScreen(){
        scoreText.text="Puntaje: "+ score;
    }

    private void PrintLivesInScreen(){
        livesText.text="Vida: "+ lives;
    }

    

}
