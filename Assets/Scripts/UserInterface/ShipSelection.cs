using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelection : MonoBehaviour
{

    [SerializeField]
    Transform[] AvailibleShips;

    [SerializeField]
    List<Transform> SelectedShips;

    public GameObject selectionBox;





    // Use this for initialization
    void Start()
    {
        selectionBox.SetActive(false);

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

    float shipX = 0;
    float shipY = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectionBox.transform.position = startPos;

            selectionBox.SetActive(true);

            Vector3 selectionBoxEnd = Input.mousePosition;

           

            //selectionBox.transform.localScale = Camera.main.ScreenToWorldPoint((startPos - selectionBoxEnd));
            

            if (leftClickInstance == false)
            {
                SelectedShips.Clear();
                startPos = Input.mousePosition;

                leftClickInstance = true;
            }
        }



        if (Input.GetMouseButtonUp(0))
        {          
            endPos = Input.mousePosition;

            for (int i = 0; i < AvailibleShips.Length; i++)
            {
                if (AvailibleShips[i].transform != null)
                {
                    shipX = Camera.main.WorldToScreenPoint(AvailibleShips[i].transform.position).x;
                    shipY = Camera.main.WorldToScreenPoint(AvailibleShips[i].transform.position).y;



                    if (Mathf.Clamp(shipX, startPos.x, endPos.x) == shipX)
                    {
                        if (Mathf.Clamp(shipY, startPos.y, endPos.y) == shipY)
                        {
                            SelectedShips.Add(AvailibleShips[i]);
                        }
                    }
                }
                else
                {
                    Debug.LogError("Unpopulated element on ShipSelection script");
                }

            }

            selectionBox.SetActive(false);

            leftClickInstance = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < SelectedShips.Count-1; i++)
            {
                SelectedShips[i].GetComponent<AbstractShip>().Destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }
}
