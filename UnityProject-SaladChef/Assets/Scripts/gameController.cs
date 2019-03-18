using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

[System.Serializable]
public class playerData
{
    public Dictionary<string, int> players;

    public playerData(){}

    public playerData(string names,int score){
        players = new Dictionary<string, int>();
        players.Add(names, score);
    }
}

public class gameController : MonoBehaviour
{
    playerData pd;
    public static float timer=120f;
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
             jsonString = "";
            pd.players = new Dictionary<string, int>();
        }
        print(jsonString);

        if (jsonString != "")
        {
            pd.players=JsonConvert.DeserializeObject<Dictionary<string,int>>(jsonString);
            print(pd.players.Count);
        }

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
            gameStarted = false;
            foreach (GameObject a in scripts)
            {
                a.SetActive(false);
            }

            if (score > PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetInt("highscore", score);
            }
            highscoreText.text = "" + PlayerPrefs.GetInt("highscore");

            if (jsonString == "")
                pd = new playerData( p1name.text + " and " + p2name.text,score);
            else
                pd.players.Add( p1name.text + " and " + p2name.text,score);

            int temp = 0;
            foreach (KeyValuePair<string, int> author in pd.players.OrderByDescending(key => key.Value))  
            {
                if (temp < 10)
                {
                    top10scores.text += author.Value + "\n";
                    top10names.text += author.Key + "\n";
                }
                else
                    continue;
                temp++;
            }  
            playersJSON = JsonMapper.ToJson(pd.players);
            File.WriteAllText(Application.streamingAssetsPath + "/PlayerData.json", playersJSON.ToString());

           
            gameover.SetActive(true);
            gameEnded = false;
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
                pd = new playerData( p1name.text + " and " + p2name.text,score);
            else
                pd.players.Add(p1name.text + " and " + p2name.text,score);

            //pd.players.OrderByDescending(x => x.Value);

            playersJSON = JsonMapper.ToJson(pd.players);
            File.WriteAllText(Application.streamingAssetsPath + "/PlayerData.json", playersJSON.ToString());
        }
    }
}
