using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

public class TriggerModuleWrapper : MonoBehaviour
{

    private ParticleSystem ps;

    [SerializeField]
    public Collider[] interactables;

    public bool enter;
    public bool exit;
    public bool inside;
    public bool outside;

    public ParticleSystemOverlapAction enterAction;
    public ParticleSystemOverlapAction exitAction;
    public ParticleSystemOverlapAction insideAction;
    public ParticleSystemOverlapAction outsideAction;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();

        var shape = ps.shape;
        shape.enabled = false;

        var trigger = ps.trigger;
        trigger.enabled = true;

        for (int i = 0; i < interactables.Length; i++)
        {
            trigger.SetCollider(i, interactables[i]);
        }
    }

    void Update()
    {
        var trigger = ps.trigger;
        trigger.enter = enter ? enterAction : ParticleSystemOverlapAction.Ignore;
        trigger.exit = exit ? exitAction : ParticleSystemOverlapAction.Ignore;
        trigger.inside = inside ? insideAction : ParticleSystemOverlapAction.Ignore;
        trigger.outside = outside ? outsideAction : ParticleSystemOverlapAction.Ignore;
    }

    void OnGUI()
    {
        enter = GUI.Toggle(new Rect(25, 40, 200, 30), enter, "Enter Callback");
        exit = GUI.Toggle(new Rect(25, 80, 200, 30), exit, "Exit Callback");
        inside = GUI.Toggle(new Rect(25, 120, 200, 30), inside, "Inside Callback");
        outside = GUI.Toggle(new Rect(25, 160, 200, 30), outside, "Outside Callback");
    }

    //void OnParticleTrigger()
    //{
    //    if (enter)
    //    {
    //        List<ParticleSystem.Particle> enterList = new List<ParticleSystem.Particle>();
    //        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterList);

    //        for (int i = 0; i < numEnter; i++)
    //        {
    //            ParticleSystem.Particle p = enterList[i];

    //            //behavior goes here



    //            //behavior ends
    //            enterList[i] = p;
    //        }

    //        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterList);
    //    }

    //    if (exit)
    //    {
    //        List<ParticleSystem.Particle> exitList = new List<ParticleSystem.Particle>();
    //        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exitList);

    //        for (int i = 0; i < numExit; i++)
    //        {
    //            ParticleSystem.Particle p = exitList[i];
    //            //behavior goes here



    //            //behavior ends
    //            exitList[i] = p;
    //        }

    //        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exitList);
    //    }

    //    if (inside)
    //    {
    //        List<ParticleSystem.Particle> insideList = new List<ParticleSystem.Particle>();
    //        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, insideList);

    //        for (int i = 0; i < numInside; i++)
    //        {
    //            ParticleSystem.Particle p = insideList[i];
    //            //behavior goes here



    //            //behavior ends
    //            insideList[i] = p;
    //        }

    //        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, insideList);
    //    }

    //    if (outside)
    //    {
    //        List<ParticleSystem.Particle> outsideList = new List<ParticleSystem.Particle>();
    //        int numOutside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Outside, outsideList);

    //        for (int i = 0; i < numOutside; i++)
    //        {
    //            ParticleSystem.Particle p = outsideList[i];
    //            //behavior goes here



    //            //behavior ends
    //            outsideList[i] = p;
    //        }

    //        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Outside, outsideList);
    //    }
    //}

}
