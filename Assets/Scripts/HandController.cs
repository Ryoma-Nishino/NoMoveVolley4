using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    public float spikeForce = 1000f;  // スパイクの力

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // ボールと衝突したかどうかを確認
        if (collision.gameObject.CompareTag("Ball"))
        {
            // ボールに力を加える
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            ballRigidbody.AddForce(transform.up * spikeForce);
        }
    }
}
