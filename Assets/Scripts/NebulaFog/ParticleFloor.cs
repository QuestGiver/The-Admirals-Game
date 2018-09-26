using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleFloor : MonoBehaviour
{

    public float xOrg = 0;
    public float yOrg = 0;
    public float scrollSpeed = 1;
    public float noiseScale = 1;
    public float amplitudeScale = 1;

    public int sizeX;
    public int sizeY;
    int numParticlesAlive;
    public new ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;
    public new ParticleSystemRenderer renderer;

    Vector3[] rgb;
    // Use this for initialization

    void Start()
    {
        renderer = GetComponent<ParticleSystemRenderer>();
        //perlinTexture = GetComponent<PerlinTexture>();
        particleSystem = GetComponent<ParticleSystem>();


        var main = particleSystem.main;
        main.startSpeed = 0;
        main.startLifetime = Mathf.Infinity;
        main.loop = false;
        main.maxParticles = sizeX * sizeY;


        var shape = particleSystem.shape;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = 0;
        shape.radiusThickness = 0;

        var emission = particleSystem.emission;
        emission.rateOverTime = 0;
        emission.enabled = false;

        //renderer = GetComponent<ParticleSystemRenderer>();
        //renderer.renderMode = ParticleSystemRenderMode.Mesh;
        //renderer.mesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
        //renderer.alignment = ParticleSystemRenderSpace.World;
        renderer.enabled = true;



        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();


        //sizeX = sizeY = 10;
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                emitParams.position = new Vector3(shape.position.x + x, shape.position.y, shape.position.z + y);
                particleSystem.Emit(emitParams, 1);
            }
        }


        if (particles == null || particles.Length < particleSystem.main.maxParticles)
        {
            Debug.Log(" count = " + particleSystem.particleCount);
            particles = new ParticleSystem.Particle[particleSystem.particleCount];
            particleSystem.GetParticles(particles);
        }


        rgb = new Vector3[sizeX * sizeY];



        numParticlesAlive = particleSystem.GetParticles(particles);
    }


    // Update is called once per frame

    void Update()
    {
        xOrg += Time.deltaTime * scrollSpeed;
        yOrg += Time.deltaTime * scrollSpeed;


        //int width = (int)Mathf.Sqrt(particles.Length);




        for (int idx = 0; idx < particles.Length; idx++)
        {
            //y = idx / width;
            //x = idx - y * width;
            //x = (x / width) * tw;
            //y = (y / width) * th;
            //PerlinTexture.noiseTex.GetPixel(idx / width, idx - ((idx / width) * width)).grayscale       [(idx / width, idx - ((idx / width) * width))]

            float greyScale = PerlinTexture.VectorToGreyScale(PerlinTexture.ColorVector(sizeX, sizeY, rgb, xOrg, yOrg, noiseScale, amplitudeScale)[idx]);
            particles[idx].position = new Vector3(particles[idx].position.x, greyScale, particles[idx].position.z);

        }


        particleSystem.SetParticles(particles, numParticlesAlive);
    }


    IEnumerator Grey()
    {
        while (true)
        {
            xOrg += Time.deltaTime * scrollSpeed;
            yOrg += Time.deltaTime * scrollSpeed;


            //int width = (int) Mathf.Sqrt(particles.Length);




            for (int idx = 0; idx < particles.Length; idx++)
            {
                //y = idx / width;
                //x = idx - y * width;
                //x = (x / width) * tw;
                //y = (y / width) * th;
                //PerlinTexture.noiseTex.GetPixel(idx / width, idx - ((idx / width) * width)).grayscale       [(idx / width, idx - ((idx / width) * width))]

                float greyScale = PerlinTexture.VectorToGreyScale(PerlinTexture.ColorVector(sizeX, sizeY, rgb, xOrg, yOrg, noiseScale, amplitudeScale)[idx]);
                particles[idx].position = new Vector3(particles[idx].position.x, greyScale, particles[idx].position.z);
                yield return null;
            }
        }
    }
}
