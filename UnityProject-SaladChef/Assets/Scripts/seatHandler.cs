using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class seatHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] customers;

    [SerializeField]
    float minTime = 3f, maxTime = 10f;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (this.gameObject.activeSelf)
            StartCoroutine(initialize());
    }

    IEnumerator initialize()
    {
        customers[0].SetActive(true);
        customers[3].SetActive(true);
        yield return new WaitForSeconds(Random.Range(minTime*2f, maxTime*2f));

        customers[1].SetActive(true);
        customers[2].SetActive(true);
    }

    public void randomiseTime(int no){
        if(this.gameObject.activeSelf)
            StartCoroutine(customer(no));
    }

    IEnumerator customer(int no){
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        customers[no].SetActive(true);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }



}
