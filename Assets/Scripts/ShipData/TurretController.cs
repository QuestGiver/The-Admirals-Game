using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private int enemyTeam;

    [SerializeField]
    private bool testFire = false;

    [SerializeField]
    public Transform[] turrets;
    [SerializeField]
    private List<ParticleSystem> turretGuns;

    [SerializeField]
    private Transform _targetPosition;
    [SerializeField]


    public bool rotate = false;
    public float traversPercent = 0;
    public float traverseSpeed = 0.5f;
    public bool permissionToFire = true;

    public Transform TargetPosition
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

    public int EnemyTeam
    {
        get
        {
            return enemyTeam;
        }

        set
        {
            enemyTeam = value;
            onSetEnemyTeam();
        }
    }


    // Use this for initialization
    void Start()
    {
        

        for (int i = 0; i < turrets.Length; i++)
        {

            turretGuns.Add(turrets[i].GetComponentInChildren<ParticleSystem>());
            
            var emit = turretGuns[i].emission;
            emit.enabled = false ;


            //turrets[i].GetComponentInChildren<CollisionModuleWrapper>().EnemyTeam = EnemyTeam;


        }
        
    }


    void onSetEnemyTeam()
    {
        for (int i = 0; i < turrets.Length; i++)
        {

            turrets[i].GetComponentInChildren<CollisionModuleWrapper>().EnemyTeam = EnemyTeam;

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


        if (permissionToFire)
        {
            if (rotate)
            {
                //Debug.Log("Attemting to rotate");
                for (int i = 0; i < turrets.Length; i++)
                {
                    //Debug.Log("iterating rotation loop");



                    Vector3 dir = (_targetPosition.position - turrets[i].position).normalized;
                    Quaternion newRotation = Quaternion.LookRotation(dir, turrets[i].transform.up);

                    traversPercent = Time.deltaTime * traverseSpeed;
                    turrets[i].rotation = Quaternion.Slerp(turrets[i].rotation, newRotation, traversPercent);

                    turrets[i].localEulerAngles = new Vector3(0f, turrets[i].localEulerAngles.y, 0f);

                    if (traversPercent >= 1)
                    {
                        traversPercent = 1;
                    }

                    if (traversPercent >= 0.02)
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
    }


    public void EnableFiring(int i, bool OpenFire)
    {
        var emit = turretGuns[i].emission;
        emit.enabled = OpenFire;
    }
}
