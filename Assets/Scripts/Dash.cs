using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public RightPosition rightPosition;
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position2 = rightPosition.position2;

    }

    public void CheckDash()
    {
        if (rightPosition)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
