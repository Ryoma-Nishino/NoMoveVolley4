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
    public float rotationSpeed = 60f; // 回転速度（度/秒）

    void Update()
    {
        // Y軸を中心にオブジェクトを回転させる
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
