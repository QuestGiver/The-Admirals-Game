using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleToVertex : MonoBehaviour
{
    public int sizeX;
    public int sizeY;
    int numParticlesAlive;
    public new ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;
    public new ParticleSystemRenderer renderer;
    Vector3[] Verticies;// = GetComponent<MeshFilter>().mesh.vertices;

    // Use this for initialization

    void Start()
    {
        Verticies = GetComponent<MeshFilter>().mesh.vertices;

        renderer = GetComponent<ParticleSystemRenderer>();
        //perlinTexture = GetComponent<PerlinTexture>();
        particleSystem = GetComponent<ParticleSystem>();

        //GetComponent<Material>().
        var main = particleSystem.main;
        main.startSpeed = 0;
        main.startLifetime = Mathf.Infinity;
        main.loop = false;
        main.maxParticles = Verticies.Length;


        var shape = particleSystem.shape;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = 0;
        shape.radiusThickness = 0;

        var emission = particleSystem.emission;
        emission.rateOverTime = 0;
        emission.enabled = false;

        //renderer = GetComponent<ParticleSystemRenderer>();
        renderer.renderMode = ParticleSystemRenderMode.Mesh;
        renderer.mesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
        renderer.alignment = ParticleSystemRenderSpace.World;
        renderer.enabled = true;



        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();


        for (int i = 0; i < Verticies.Length; i++)
        {
            emitParams.position = new Vector3(shape.position.x, shape.position.y, shape.position.z);
            particleSystem.Emit(emitParams, 1);
        }


        if (particles == null || particles.Length < particleSystem.main.maxParticles)
        {
            Debug.Log(" count = " + particleSystem.particleCount);
            particles = new ParticleSystem.Particle[particleSystem.particleCount];
            particleSystem.GetParticles(particles);
        }

        numParticlesAlive = particleSystem.GetParticles(particles);
    }

    // Update is called once per frame
    void Update()
    {

        int i = 0;
        foreach (Vector3 vert in Verticies)
        {
            Vector3 vertexPlace = transform.TransformPoint(vert);
            //Vector2 screenLocation = new Vector2(vertexPlace.x, vertexPlace.y);


            particles[i].position = new Vector3(transform.position.x - vertexPlace.x, transform.position.y - vertexPlace.y, transform.position.z - vertexPlace.z );

            particleSystem.SetParticles(particles, numParticlesAlive);
            //Gizmos.DrawSphere(transform.TransformPoint(vert), vertexSize);
            //drawString("Vertex # " + i, transform.TransformPoint(vert));
            i++;
        }

    }
}
