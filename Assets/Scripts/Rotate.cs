using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public float rotationSpeed = 60f; // ��]���x�i�x/�b�j

    void Update()
    {
        // Y���𒆐S�ɃI�u�W�F�N�g����]������
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
