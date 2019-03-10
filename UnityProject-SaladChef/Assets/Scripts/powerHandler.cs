using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerHandler : MonoBehaviour
{
    [SerializeField]
    RawImage img;

    [SerializeField]
    Texture[] powers;

    public int power;
    public bool p1;
    // Start is called before the first frame update
    void Start()
    {
        power = Random.Range(0, powers.Length);
        img.texture = powers[power];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
