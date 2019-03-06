using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    public float timer=120f;
    public static int score=0;

    [SerializeField]
    Text timerText, scoreText, scoreEnd;

    [SerializeField]
    GameObject menu, gameover;

    [SerializeField]
    GameObject[] scripts;
    // Start is called before the first frame update
    void Start()
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
        }
        if(gameEnded){
            foreach (GameObject a in scripts)
            {
                a.SetActive(false);
            }
            gameEnded = false;
        }
        timerText.text = "" + (int)timer;
        scoreText.text = "" + score;
        scoreEnd.text = "" + score;
    }

    public void restart(){
        Application.LoadLevel(Application.loadedLevel);
    }
}
