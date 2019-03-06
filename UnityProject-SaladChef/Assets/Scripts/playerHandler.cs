using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHandler : MonoBehaviour
{
    public float speed;
    private Vector2 moveVelocity;
    Rigidbody2D rb;

    [SerializeField]
    string horizontalAxisName="Horizontal", VerticalAxisName="Vertical";

    [SerializeField]
    GameObject interactableAlert;

    [SerializeField]
    GameObject plateA, plateB;

    [SerializeField]
    Text plateAText, plateBText;

    string plateAin, plateBin;

    public bool p1;

    // Start is called before the first frame update
    void Start()
    {
        plateAin = "";
        plateBin = "";
        rb = this.GetComponent<Rigidbody2D>();
        canMove = true;
    }

    bool canMove;
    // Update is called once per frame
    void Update()
    {
        Vector2 inputVal = new Vector2(Input.GetAxisRaw(horizontalAxisName), Input.GetAxisRaw(VerticalAxisName));

        if (canMove)
            moveVelocity = inputVal.normalized * speed;
        else
            moveVelocity = Vector2.zero;

        if (plateAin != "")
        {
            plateA.SetActive(true);
            plateAText.text = "" + plateAin;
        }else{
            plateA.SetActive(false);
        }

        if (plateBin != "")
        {
            plateB.SetActive(true);
            plateBText.text = "" + plateBin;
        }
        else
        {
            plateB.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ingredient")
        {
            if(plateBin=="")
                interactableAlert.SetActive(true);
        }else
            interactableAlert.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "ingredient")
        {
            if (p1)
            {
                if (Input.GetKeyDown(KeyCode.E)){
                    if(plateAin=="")
                        plateAin = ""+other.GetComponent<ingredientHandler>().ingredient;
                    else if(plateBin=="")
                        plateBin = "" + other.GetComponent<ingredientHandler>().ingredient;
                }
            }else{
                if (Input.GetKeyDown(KeyCode.M))
                {
                    if (plateAin == "")
                        plateAin = "" + other.GetComponent<ingredientHandler>().ingredient;
                    else if (plateBin == "")
                        plateBin = "" + other.GetComponent<ingredientHandler>().ingredient;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactableAlert.SetActive(false);
    }
}
