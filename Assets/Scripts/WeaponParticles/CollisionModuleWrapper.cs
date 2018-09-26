using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionModuleWrapper : MonoBehaviour
{
    private ParticleSystem ps;

    [SerializeField]
    public int Team = 9;


    // Use this for initialization
    void Start()
    {
        ps = GetComponent<ParticleSystem>();


        //this weapons are too innaccurate if this is turned on
        var shape = ps.shape;
        shape.enabled = false;

        var col = ps.collision;
        col.enabled = true;

        col.collidesWith = (1 << Team);


    }


}
