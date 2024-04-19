using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    public float spikeForce = 1000f;  // �X�p�C�N�̗�

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
        // �{�[���ƏՓ˂������ǂ������m�F
        if (collision.gameObject.CompareTag("Ball"))
        {
            // �{�[���ɗ͂�������
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            ballRigidbody.AddForce(transform.up * spikeForce);
        }
    }
}
