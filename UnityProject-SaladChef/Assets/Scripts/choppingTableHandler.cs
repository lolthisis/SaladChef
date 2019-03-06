using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choppingTableHandler : MonoBehaviour
{
    [SerializeField]
    Image fill;

    public Text item;

    [SerializeField]
    float timeTaken=2f;

    public float temp;
    // Start is called before the first frame update
    void Start()
    {
        item.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        fill.fillAmount = temp / timeTaken;
    }

    public void addItem(string text){
        if (text.Contains("*"))
        {
            temp = 0f;
            if (item.text.Contains("*"))
                item.text = item.text.Split('*')[0] + text;
            else
                item.text = text.Split('*')[0];
        }
        else
        {
            if (item.text.Contains("*"))
                item.text =item.text.Split('*')[0]+ text;
            else
                item.text += text;
            temp = timeTaken;
        }
        once = true;
    }

    bool once = true;
    public void decreaseTime()
    {
        if(temp>0f)
            temp -= Time.deltaTime;
        else{
            temp = 0f;
            if (once)
            {
                item.text += "*";
                once = false;
            }
        }
    }
}
