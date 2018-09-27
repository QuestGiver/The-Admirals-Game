using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelection : MonoBehaviour
{

    Transform[] AvailibleShips;



    // Use this for initialization
    void Start()
    {
        if (AvailibleShips.Length == 0)
        {
            Debug.LogError("AvailibleShip length is 0");
        }
        else
        {
            for (int i = 0; i < AvailibleShips.Length; i++)
            {
                if (AvailibleShips[i] == null)
                {
                    Debug.LogError("AvailibleShips[" + i + "] is null");
                }
            }
        }     
    }





    bool leftClickInstance;
    Vector3 startPos = Vector3.zero;
    Vector3 endPos = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (leftClickInstance == false)
            {
                startPos = Input.mousePosition;

                leftClickInstance = true;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;

            leftClickInstance = false;
        }
    }
}
