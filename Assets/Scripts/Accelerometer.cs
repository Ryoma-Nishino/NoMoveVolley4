using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Accelerometer : MonoBehaviour
{
    private Vector3 lastVelocity = Vector3.zero;
    private float timeElapsed = 0.0f;
    private StreamWriter writer;

    void Start()
    {
        writer = new StreamWriter("acceleration.csv", false);
        writer.WriteLine("Time,Acceleration");
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= 0.01f)
        {
            Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
            Vector3 acceleration = (currentVelocity - lastVelocity) / timeElapsed;

            writer.WriteLine(timeElapsed + "," + acceleration.magnitude);

            lastVelocity = currentVelocity;
            timeElapsed = 0.0f;
        }
    }

    void OnDestroy()
    {
        writer.Close();
    }
}
