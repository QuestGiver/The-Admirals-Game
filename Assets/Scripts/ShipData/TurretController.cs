using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    public int enemyTeam;

    [SerializeField]
    private bool testFire = false;

    [SerializeField]
    public Transform[] turrets;
    [SerializeField]
    private List<ParticleSystem> turretGuns;

    [SerializeField]
    private Vector3 _targetPosition;
    [SerializeField]


    public bool rotate = false;



    public Vector3 TargetPosition
    {
        get
        {
            return _targetPosition;
        }

        set
        {
            _targetPosition = value;
            if (_targetPosition == null)
            {
                rotate = false;
            }
            else
            {
                rotate = true;
                traversPercent = 0;
            }
        }
    }

    public float traversPercent = 0;
    public float traverseSpeed = 3;
    // Use this for initialization
    void Start()
    {
        

        for (int i = 0; i < turrets.Length; i++)
        {

            turretGuns.Add(turrets[i].GetComponentInChildren<ParticleSystem>());
            
            var emit = turretGuns[i].emission;
            emit.enabled = false ;


            turrets[i].GetComponentInChildren<CollisionModuleWrapper>().Team = enemyTeam;


        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (testFire)
        {
            for (int i = 0; i < turrets.Length; i++)
            {
                EnableFiring(i, true);
            }


        }
        else
        {

            for (int i = 0; i < turrets.Length; i++)
            {
                EnableFiring(i, false);
            }
        }



        if (rotate)
        {
            Debug.Log("Attemting to rotate");
            for (int i = 0; i < turrets.Length; i++)
            {
                Debug.Log("iteration");
                traversPercent += Time.deltaTime * traverseSpeed;
                Vector3.Slerp(turrets[i].rotation.eulerAngles, _targetPosition - turrets[i].transform.position, traversPercent);
                if (traversPercent >= 1)
                {
                    traversPercent = 1;
                }

                if (traversPercent >= 0.7)
                {
                    
                    EnableFiring(i, true);
                }
                else
                {
                    EnableFiring(i, false);
                }
            }
        }
    }


    public void EnableFiring(int i, bool OpenFire)
    {
        var emit = turretGuns[i].emission;
        emit.enabled = OpenFire;
    }
}
