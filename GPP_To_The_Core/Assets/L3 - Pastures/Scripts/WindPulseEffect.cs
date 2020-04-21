using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPulseEffect : MonoBehaviour
{
    public float power = 15.0f;
    public float radius = 5.0f;
    public float upForce = 1.0f;
    public int counting = 3;

    private PlayerInput input;
    private WindPulseCollect collector;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        collector = GetComponent<WindPulseCollect>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (input.inputAction3 && counting > 0 && collector.windPulseCollected)
        {
            StartCoroutine(time());
            Detonate();
        }
    }

    void Detonate()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach (Collider hit in colliders)

        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null && hit.gameObject.layer == LayerMask.NameToLayer("Enemies"))
            {
             rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);   
            }
            
        }
    }

    IEnumerator time()
    {
        if (counting == 3)
        {
            counting = 2;
        }else if (counting == 2)
        {
            counting = 1;
        }else if (counting == 1)
        {
            counting = 0;
        }
        yield return new WaitForSeconds(0.5f);
    }
}