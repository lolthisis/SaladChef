using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speedA, speedB;

    private Vector2 moveVelocityA, moveVelocityB;

    [SerializeField]
    Rigidbody2D rbA, rbB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputValA = new Vector2(Input.GetAxisRaw("HorizontalA"), Input.GetAxisRaw("VerticalA"));
        moveVelocityA = inputValA.normalized * speedA;

        Vector2 inputValB = new Vector2(Input.GetAxisRaw("HorizontalB"), Input.GetAxisRaw("VerticalB"));
        moveVelocityB = inputValB.normalized * speedA;

    }

    private void FixedUpdate()
    {
        rbA.MovePosition(rbA.position + moveVelocityA * Time.fixedDeltaTime);
        rbB.MovePosition(rbB.position + moveVelocityB * Time.fixedDeltaTime);
    }
}
