using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float rotationAmount;
    public float minRandomTime;
    public float maxRandomTime;

    private float rotateTimer;
    private float nextRotateTime;
    private bool rotateRight;

    void Start()
    {
        rotateTimer = 0;
        nextRotateTime = Random.Range(minRandomTime, maxRandomTime);
        rotateRight = true;
    }
    
    void Update()
    {
        rotateTimer += Time.deltaTime;

        if (rotateTimer >= nextRotateTime)
        {
            rotateTimer = 0;
            nextRotateTime = Random.Range(1f, 4f);

            if (rotateRight)
            {
                transform.rotation = Quaternion.AngleAxis(rotationAmount, Vector3.up) * transform.rotation;
            }
            else
            {
                transform.rotation = Quaternion.AngleAxis(-rotationAmount, Vector3.up) * transform.rotation;
            }

            rotateRight = !rotateRight;
        }
    }
}
