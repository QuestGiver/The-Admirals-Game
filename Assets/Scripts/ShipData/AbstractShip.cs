﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;


[System.Serializable]
public struct ShipStats
{
    public int health;
    public int speed;
    public int radarRange;


    
    public int team;
    public int enemyTeam;
}


[RequireComponent(typeof(TurretController))]
public class AbstractShip : MonoBehaviour
{

    public ShipStats stats;
    public TurretController turretController;
    public NavMeshAgent agent;

    LayerMask nativeTeam;
    LayerMask nativeEnemyTeam;


    [SerializeField]
    private Vector3 _destination;
    private void Reset()//on clicking reset button in editor
    {
        turretController = GetComponent<TurretController>();
    }

    void Start()
    {

        nativeTeam = (nativeTeam | (1 << stats.team));
        nativeEnemyTeam = (nativeEnemyTeam | (1 << stats.enemyTeam));


        if (stats.team != gameObject.layer)
        {
            gameObject.layer = stats.team;
            //Debug.LogError("Ship team and and object layer do not match");
        }

            //.layer = (stats.team | (1 << ));
            turretController.EnemyTeam = stats.enemyTeam;
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

    public Vector3 itemPos;

    public virtual IEnumerator Detection()
    {
        while (true)
        {
            Debug.Log("Running Detection");


            foreach (Collider item in Physics.OverlapSphere(transform.position, stats.radarRange, nativeTeam))
            {
                if (gameObject.name == "Battleship")
                {
                    Debug.Log(nativeTeam.value);
                    Debug.Log(nativeEnemyTeam.value);
                }
                if (item != null)
                {
                    Debug.Log("checkpoint 1");
                    if (item != gameObject.GetComponent<Collider>())
                    {
                        Debug.Log("checkpoint 2");

                        if (item.transform.tag == "Ship")
                        {


                            //string bin = stats.enemyTeam.value.ToString();
                            //Debug.Log(bin);
                            Debug.Log("checkpoint 3");
                            itemPos = item.transform.position;

                            if (stats.enemyTeam == item.transform.gameObject.layer)
                            {
                                Debug.Log("checkpoint 4");

                                turretController.TargetPosition = item.transform;
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


        if (gameObject.name == "Destroyer")
        {
            Gizmos.color = Color.blue;
        }
        if (gameObject.name == "Battleship")
        {
            Gizmos.color = Color.red;
        }
        if (gameObject.name == "Cruiser")
        {
            Gizmos.color = Color.yellow;
        }

        Gizmos.DrawWireCube(itemPos, new Vector3(2,2,2));
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
