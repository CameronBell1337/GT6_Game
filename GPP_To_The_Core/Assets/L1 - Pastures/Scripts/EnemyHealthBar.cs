using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private Enemy enemyScript;
    private CaveSpider caveSpiderScript;

    void Start()
    {
        enemyScript = transform.parent.parent.GetComponent<Enemy>();
        caveSpiderScript = transform.parent.parent.GetComponent<CaveSpider>();
    }

    void Update()
    {
        if (enemyScript != null)
        {
            SetHealth(enemyScript.health);
        }
        else if (caveSpiderScript != null)
        {
            SetHealth(caveSpiderScript.health);
        }
    }
    
    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
