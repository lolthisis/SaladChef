using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

struct playerData
{
    public string p1n, p2n;
    public int score;

    public playerData(string p1, string p2, int scr)
    {
        p1n = p1;
        p2n = p2;
        score = scr;
    }
}

public class gameController : MonoBehaviour
{
    playerData pd;
    public float timer=120f;
    public static int score=0;

    [SerializeField]
    Text timerText, scoreText, scoreEnd, highscoreText;

    [SerializeField]
    GameObject menu, gameover;

    [SerializeField]
    GameObject[] scripts;

    [SerializeField]
    InputField p1name, p2name;

    [SerializeField]
    Button startgameButton;

    // Start is called before the first frame update
    void Awakes()
    {
        score = 00;
        timer = 120f;
        gameStarted = false; 
        foreach (GameObject a in scripts)
        {
            a.SetActive(false);
        }
    }

    bool gameStarted,gameEnded;
    public void gameOn(){
        gameStarted = true;
        menu.gameObject.SetActive(false);

        PlayerPrefs.SetString("p1name", p1name.text);
        PlayerPrefs.SetString("p2name", p2name.text);
        foreach(GameObject a in scripts){
            a.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted){
            if (timer > 0f)
                timer -= Time.deltaTime;
            else
                gameEnded = true;
        }else{
            if(p1name.text!=""&&p2name.text!=""){
                startgameButton.interactable = true;
            }
        }

        if(gameEnded){
            foreach (GameObject a in scripts)
            {
                a.SetActive(false);
            }
            gameEnded = false;
            gameover.SetActive(true);

            if (score > PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetInt("highscore", score);
            }
            highscoreText.text = "" + PlayerPrefs.GetInt("highscore");
        }

        timerText.text = "" + (int)timer;
        scoreText.text = "" + score;
        scoreEnd.text = "" + score;
    }

    public void restart(){
        Application.LoadLevel(Application.loadedLevel);
    }
}
