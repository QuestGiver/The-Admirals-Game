using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmAI : MonoBehaviour
{

    public AbstractShip ship;
    public float orderCooldown = 1;
    Vector3 destination;
    public bool AIControlled = true;
    public float range = 30;

    public Vector3 Destination
    {
        get
        {
            return destination;
        }

        set
        {
            destination = value;
            OnDestinationOrder();
        }
    }

    // Use this for initialization
    void Start()
    {
        ship.agent.speed = ship.stats.speed;
        if (AIControlled)
        {
            StartCoroutine(MovementOrder());
        }

    }


    public IEnumerator MovementOrder()
    {

        while (true)
        {
            if (ship.turretController.TargetPosition != null)
            {
                Destination = ship.turretController.TargetPosition.position + (Random.insideUnitSphere * range);

            }
            else
            {
                Destination = transform.position + (Random.insideUnitSphere * range);
            }
            yield return new WaitForSeconds(orderCooldown);
        }

    }

    void OnDestinationOrder()
    {
        ship.Destination = destination;
    }

}
