using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public float mouseSensativity = 100;
    Camera cam;


    bool leftClickInstance = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }
    Vector3 startPos = Vector3.zero;
    Vector3 endPos = Vector3.zero;
    // Update is called once per frame
    void Update()
    {



        //rb.AddRelativeTorque(transform.up * Input.GetAxisRaw("Mouse X") * mouseSensativity * Time.deltaTime);


        //rb.AddRelativeTorque(transform.right * Input.GetAxisRaw("Mouse Y") * -mouseSensativity * Time.deltaTime);

        //rb.AddRelativeTorque(Vector3.forward * (0 - transform.rotation.z) * 1000 * Time.deltaTime);
        //transform.Rotate(Vector3.up, Input.GetAxisRaw("Mouse X") * mouseSensativity * Time.deltaTime, Space.World);
        //transform.Rotate(Vector3.right, Input.GetAxisRaw("Mouse Y") * -mouseSensativity * Time.deltaTime, Space.World);


        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddForce(Vector3.down * speed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.E))
        {
            rb.AddForce(Vector3.up * speed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(Vector3.forward * (speed * 2) * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.right * -speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(Vector3.right * -(speed * 2) * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.forward * -speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(Vector3.forward * -(speed * 2) * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(Vector3.right * (speed * 2) * Time.deltaTime);
            }
        }


        if (Input.GetMouseButtonDown(0))
        {

            if (leftClickInstance == false)
            {
                startPos = Input.mousePosition;
                Debug.Log(Input.mousePosition);
                leftClickInstance = true;
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            rb.AddForce((startPos - endPos).x *speed* Time.deltaTime, 0, (startPos - endPos).y * speed * Time.deltaTime);
            Debug.Log(Input.mousePosition);
            leftClickInstance = false;
        }
    }
}
