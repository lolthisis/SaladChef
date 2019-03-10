using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Linq;
public class playerData
{
    public Dictionary<int, string> matrix { get; set; }

    public playerData()
    {
        matrix = new Dictionary<int, string>();
    }

    public playerData(Dictionary<int, string> mat){
        matrix = new Dictionary<int, string>();
        matrix = mat;
    }

    public playerData(int score,string names){
        matrix = new Dictionary<int, string>();
        matrix.Add(score, names);
    }
}

public class gameController : MonoBehaviour
{
    playerData pd;
    public static float timer=20f;
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

    string jsonString;
    // Start is called before the first frame update
    void Awake()
    {
        pd = new playerData();
        if (File.Exists(Application.streamingAssetsPath + "/PlayerData.json"))
        {
            jsonString = File.ReadAllText(Application.streamingAssetsPath + "/PlayerData.json");
        }
        else{
            print("ASd");
             jsonString = "";
        }

        if(jsonString!="")
            pd = JsonUtility.FromJson<playerData>(jsonString);

        print(pd.matrix.Count);
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
    JsonData playersJSON;
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

            if (score > PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetInt("highscore", score);
            }
            highscoreText.text = "" + PlayerPrefs.GetInt("highscore");

            if (jsonString == "")
                pd = new playerData(score, p1name.text + " and " + p2name.text);
            else
                pd.matrix.Add(score, p1name.text + " and " + p2name.text);
            pd.matrix.OrderBy(x => x.Key);
            playersJSON = JsonMapper.ToJson(pd);
            File.WriteAllText(Application.streamingAssetsPath + "/PlayerData.json", playersJSON.ToString());

            int temp = 0;
            foreach(int scr in pd.matrix.Keys){
                if(temp<10)
                    top10scores.text += scr + "\n";
                temp++;
            }
            temp = 0;
            foreach (string names in pd.matrix.Values)
            {
                if (temp < 10)
                    top10names.text +=names + "\n";
                temp++;
            }
            gameover.SetActive(true);
        }

        timerText.text = "" + (int)timer+"s";
        scoreText.text = "" + score;
        scoreEnd.text = "" + score;
    }

    [SerializeField]
    Text top10names,top10scores;

    public void restart(){
        Application.LoadLevel(Application.loadedLevel);
    }

    private void OnApplicationQuit()
    {
        if(!gameEnded&&gameStarted){
            if (jsonString == "")
                pd = new playerData(score, p1name.text + " and " + p2name.text);
            else
                pd.matrix.Add(score, p1name.text + " and " + p2name.text);

            pd.matrix.OrderByDescending(x => x.Key);
            playersJSON = JsonMapper.ToJson(pd);
            File.WriteAllText(Application.streamingAssetsPath + "/PlayerData.json", playersJSON.ToString());
        }
    }
}
