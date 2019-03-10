using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHandler : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;

    string plateAin, plateBin;

    [SerializeField]
    string horizontalAxisName="Horizontal", VerticalAxisName="Vertical";

    [SerializeField]
    GameObject interactableAlert;

    [SerializeField]
    GameObject plateA, plateB;

    [SerializeField]
    Text plateAText, plateBText;

    public bool p1;

    private GameObject stayingOn;
    private Vector2 moveVelocity;

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
        
        Vector2 inputVal=Vector2.zero;
        if (p1)
        {
            if (!Input.GetKey(KeyCode.E))
                inputVal = new Vector2(Input.GetAxisRaw(horizontalAxisName), Input.GetAxisRaw(VerticalAxisName));
        }else
        {
            if (!Input.GetKey(KeyCode.M))
                inputVal = new Vector2(Input.GetAxisRaw(horizontalAxisName), Input.GetAxisRaw(VerticalAxisName));
        }
        if (canMove)
            moveVelocity = inputVal.normalized * speed;
        else
            moveVelocity = Vector2.zero;
        
        plateAText.text = "" + plateAin;
        plateBText.text = "" + plateBin;
         

        if (stayingOn != null){
            if (stayingOn.tag == "ingredient")
            {
                if (p1)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                        handleIngredient();
                }
                else
                    if (Input.GetKeyDown(KeyCode.M))
                    handleIngredient();

            }
            else if (stayingOn.tag == "extraplate")
            {
                if (p1)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                        handleExtraPlate();
                }
                else
                    if (Input.GetKeyDown(KeyCode.M))
                    handleExtraPlate();

            }
            else if (stayingOn.tag == "trash")
            {
                //To stop interactable alert when we have emptied the plate
                if (plateAin == "")
                    interactableAlert.SetActive(false);
                if (p1)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                        handleTrash();
                }
                else
                    if (Input.GetKeyDown(KeyCode.M))
                    handleTrash();
            }
            else if (stayingOn.tag == "chopping")
            {
                if (p1)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                        handleChoppingBoard();
                    if (Input.GetKey(KeyCode.E))
                    {
                        stayingOn.GetComponent<choppingTableHandler>().decreaseTime();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.M))
                        handleChoppingBoard();
                    if (Input.GetKey(KeyCode.M))
                    {
                        stayingOn.GetComponent<choppingTableHandler>().decreaseTime();
                    }
                }
            }
            else if(stayingOn.tag=="customer"){
                if (plateAin == "")
                    interactableAlert.SetActive(false);
                if(p1){
                    if (Input.GetKeyDown(KeyCode.E))
                        handleCustomer();
                }else
                    if (Input.GetKeyDown(KeyCode.M))
                        handleCustomer();
            }
        }
    }

    void handleCustomer(){
        if (plateAin != "")
        {
            if(stayingOn.GetComponent<customerHandler>().order.text == plateAin){
                gameController.score += (int)stayingOn.GetComponent<customerHandler>().tempTimer * 100;
                stayingOn.GetComponent<customerHandler>().correctOrder(p1);
                stayingOn.gameObject.SetActive(false);
            }else{
                stayingOn.GetComponent<customerHandler>().wrongOrder();
            }
            plateAin = plateBin;
            plateBin = "";
        }
    }

    void handleChoppingBoard(){
        if(stayingOn.GetComponent<choppingTableHandler>().item.text==""){
            if(plateAin!=""){
                stayingOn.GetComponent<choppingTableHandler>().addItem(plateAin);
                plateAin = plateBin;
                plateBin = "";
            }
        }else{
            if(plateAin!=""&& stayingOn.GetComponent<choppingTableHandler>().temp == 0f){
                if (stayingOn.GetComponent<choppingTableHandler>().item.text.ToCharArray().Length >= 4)
                {
                    interactableAlert.SetActive(false);
                    if (plateBin == "")
                    {
                        plateBin = "" + stayingOn.GetComponent<choppingTableHandler>().item.text;
                        stayingOn.GetComponent<choppingTableHandler>().item.text = "";
                    }
                }
                else
                {
                    stayingOn.GetComponent<choppingTableHandler>().addItem(plateAin);
                    plateAin = plateBin;
                    plateBin = "";
                }
            }
            else if(plateAin==""&&stayingOn.GetComponent<choppingTableHandler>().temp==0f){
                plateAin = "" + stayingOn.GetComponent<choppingTableHandler>().item.text;
                stayingOn.GetComponent<choppingTableHandler>().item.text = "";
            }
        }
    }

    void handleExtraPlate(){
        if (stayingOn.GetComponent<extraPlateHandler>().item.text == "")
        {
            if (plateAin != "")
            {
                stayingOn.GetComponent<extraPlateHandler>().item.text = "" + plateAin;
                plateAin = plateBin;
                plateBin = "";
            }
        }
        else
        {
            if (plateAin == "")
            {
                plateAin = "" + stayingOn.GetComponent<extraPlateHandler>().item.text;
                stayingOn.GetComponent<extraPlateHandler>().item.text = "";
            }
            else if (plateBin == "")
            {
                plateBin = "" + stayingOn.GetComponent<extraPlateHandler>().item.text;
                stayingOn.GetComponent<extraPlateHandler>().item.text = "";
            }
        }
    }

    void handleIngredient(){
        if (plateAin == "")
            plateAin = "" + stayingOn.GetComponent<ingredientHandler>().ingredient;
        else if (plateBin == "")
            plateBin = "" + stayingOn.GetComponent<ingredientHandler>().ingredient;
    }

    void handleTrash(){
        if (plateAin != ""){
            plateAin = plateBin;
            plateBin = "";
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
        }else if(other.tag=="extraplate"){
            if(other.GetComponent<extraPlateHandler>().item.text==""&&plateAin==""){
                interactableAlert.SetActive(false);
            }else{
                interactableAlert.SetActive(true);
            }
        }else if (other.tag == "trash")
        {
            if(plateAin!="")
                interactableAlert.SetActive(true);
        }else if(other.tag=="power"){
            if(other.GetComponent<powerHandler>().p1==p1){
                if (other.GetComponent<powerHandler>().power == 2)
                {
                    speed += 50;
                }
                else if (other.GetComponent<powerHandler>().power == 1)
                {
                    gameController.timer += 10f;
                }
                else
                    gameController.score += 2000;
                Destroy(other.gameObject);
            }
        }else
            interactableAlert.SetActive(true);


    }

    private void OnTriggerStay2D(Collider2D other)
    {
        stayingOn = other.gameObject;
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        stayingOn = null;
        interactableAlert.SetActive(false);
    }
}

