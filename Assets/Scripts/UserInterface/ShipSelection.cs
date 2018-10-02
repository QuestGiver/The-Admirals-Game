using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelection : MonoBehaviour
{

    [SerializeField]
    Transform[] AvailibleShips;

    [SerializeField]
    List<AbstractShip> SelectedShips;

    public RectTransform selectionBox;





    // Use this for initialization
    void Awake()
    {
        selectionBox.gameObject.SetActive(false);

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
                else
                {
                    AvailibleShips[i].GetComponent<HelmAI>().AIControlled = false;
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


            

            Vector3 selectionBoxEnd = Input.mousePosition;

                selectionBox.gameObject.SetActive(true);

            selectionBox.transform.localScale = (startPos - selectionBoxEnd).normalized;


            if (leftClickInstance == false)
            {
                SelectedShips.Clear();
                selectionBox.transform.position = startPos;

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

                    Debug.Log("checking selected ships");

                    if (Mathf.Clamp(shipX, LesserNumber(startPos.x, endPos.x), GreaterNumber(startPos.x, endPos.x)) == shipX)
                    {
                        if (Mathf.Clamp(shipY, LesserNumber( startPos.y, endPos.y), GreaterNumber(startPos.y, endPos.y)) == shipY)
                        {

                            SelectedShips.Add(AvailibleShips[i].GetComponent<AbstractShip>());
                        }
                    }
                }
                else
                {
                    Debug.LogError("Unpopulated element on ShipSelection script");
                }

            }

            selectionBox.gameObject.SetActive(false);

            leftClickInstance = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < SelectedShips.Count - 1; i++)
            {
                //Debug.Log("assigning destination");

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                int mask = LayerMask.NameToLayer("UI");

                Physics.Raycast(ray, out hit, 200, mask, QueryTriggerInteraction.Collide);

                //SelectedShips[i].Destination = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

                SelectedShips[i].Destination = hit.point;
            }
        }
    }

    public float GreaterNumber(float A, float B)
    {
        if (A > B)
        {
            return A;
        }
        else
        {
            return B;
        }
    }

    public float LesserNumber(float A, float B)
    {
        if (A < B)
        {
            return A;
        }
        else
        {
            return B;
        }
        
    }
}
