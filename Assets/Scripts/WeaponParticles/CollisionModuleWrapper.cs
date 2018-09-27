using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionModuleWrapper : MonoBehaviour
{
    private ParticleSystem ps;

    [SerializeField]
    private int _enemyTeam;

    public int EnemyTeam
    {
        get
        {
            return _enemyTeam;
        }

        set
        {
            _enemyTeam = value;
            onSetEnemyTeam();
        }
    }


    // Use this for initialization
    void Start()
    {
        ps = GetComponent<ParticleSystem>();


        //this weapons are too innaccurate if this is turned on
        var shape = ps.shape;
        shape.enabled = false;

        var col = ps.collision;
        col.enabled = true;

    }

    void onSetEnemyTeam()
    {
        if (ps == null)
        {
            ps = GetComponent<ParticleSystem>();
        }
        var col = ps.collision;
        col.collidesWith = (col.collidesWith | (1 << EnemyTeam));//EnemyTeam;
    }


}
