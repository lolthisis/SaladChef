using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class customerHandler : MonoBehaviour
{
    seatHandler sH;

    [SerializeField]
    Image fill;

    public Text order;

    [SerializeField]
    ingredientHandler[] ingredientHandlers;

    [SerializeField]
    float TimeFor3Order = 30f, TimeFor2Order = 20f;
   
    public float tempTimer;

    [SerializeField]
    int no;

    private void Start()
    {
        sH = FindObjectOfType<seatHandler>();
    }

    bool three;
    // Start is called before the first frame update
    void OnEnable()
    {
        sH = FindObjectOfType<seatHandler>();
        decreasingPower = 1f;
        if (Random.Range(0, 100) > 60)
        {
            three = true;
            order.text = ingredientHandlers[Random.Range(0, ingredientHandlers.Length)].ingredient + ingredientHandlers[Random.Range(0, ingredientHandlers.Length)].ingredient + ingredientHandlers[Random.Range(0, ingredientHandlers.Length)].ingredient + "*";
            tempTimer = TimeFor3Order;
        }
        else
        {
            order.text = ingredientHandlers[Random.Range(0, ingredientHandlers.Length)].ingredient + ingredientHandlers[Random.Range(0, ingredientHandlers.Length)].ingredient + "*";
            tempTimer = TimeFor2Order;
        }
    }

    public float decreasingPower = 1f;
    // Update is called once per frame
    void Update()
    {
        if(tempTimer>0f)
            tempTimer -= Time.deltaTime*decreasingPower;
        else
        {
            tempTimer = 0f;
            gameController.score -= 500;
            if (gameController.score < 0)
                gameController.score = 0;
            this.gameObject.SetActive(false);
        }
        if(three){
            fill.fillAmount = tempTimer / TimeFor3Order;
        }else{
            fill.fillAmount = tempTimer / TimeFor2Order;
        }
    }

    private void OnDisable()
    {
        if(sH)
        sH.randomiseTime(no);
    }
}
