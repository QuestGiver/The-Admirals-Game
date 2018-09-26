using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


[System.Serializable]
public struct ShipStats
{
    public int health;
    public int speed;
    public int radarRange;
    public LayerMask team;
    public LayerMask enemyTeam;
}


[RequireComponent(typeof(TurretController))]
public class AbstractShip : MonoBehaviour
{

    public ShipStats stats;
    public TurretController turretController;
    public NavMeshAgent agent;

    [SerializeField]
    private Vector3 _destination;
    private void Reset()//on clicking reset button in editor
    {
        turretController = GetComponent<TurretController>();
    }

    void Start()
    {
        stats.team = gameObject.layer;
        turretController.enemyTeam = stats.enemyTeam;
        StartCoroutine(Detection());
    }

    public Vector3 Destination
    {
        get
        {
            return _destination;
        }

        set
        {
            _destination = value;
            OnRecieveDestination();
        }
    }

    public virtual void OnRecieveDestination()
    {
        agent.destination = _destination;
    }



    public virtual IEnumerator Detection()
    {
        while (true)
        {
            Debug.Log("Running Detection");


            foreach (Collider item in Physics.OverlapSphere(transform.position, stats.radarRange, stats.team))
            {
                if (item != null)
                {
                    Debug.Log("checkpoint 1");
                    if (item != gameObject.GetComponent<Collider>())
                    {
                        Debug.Log("checkpoint 2");

                        if (item.tag == "Ship")
                        {
                            Debug.Log("checkpoint 3");

                            if (item.gameObject.layer != stats.team)
                            {
                                Debug.Log("checkpoint 4");

                                turretController.TargetPosition = item.transform.position;
                                break;
                            }
                        }
                    }
                }
            }
            yield return new WaitForSeconds(2);
        }





    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.radarRange);
    }


    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Macro")
        {
            stats.health -= 2;
            Mathf.Clamp(stats.health, 0, 100);
        }

        if (other.tag == "Inferno")
        {
            stats.health -= 7;
        }


    }

    void Update()
    {

    }





}
