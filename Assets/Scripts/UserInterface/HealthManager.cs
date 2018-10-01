using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private AbstractShip ship;

    [SerializeField]
    private Slider healthbar;



    // Use this for initialization
    void Start()
    {
        healthbar.maxValue = ship.stats.health;
        healthbar.value = ship.stats.health;
        ship.recievedDamage = HealthBarUpdate;
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }


    void HealthBarUpdate()
    {
        healthbar.value = ship.stats.health;
        if (healthbar.value <= 0)
        {
            healthbar.enabled = false;
        }

    }


}
