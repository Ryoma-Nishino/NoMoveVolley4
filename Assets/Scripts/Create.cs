using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public GameObject objectToSpawn; // 生成するオブジェクトのPrefab

    private void Start()
    {
        InvokeRepeating("Spawn", 0f, 1f); // 0秒後から開始し、1秒ごとにSpawnメソッドを呼び出す
    }

    private void Spawn()
    {
        Instantiate(objectToSpawn, transform.position, Quaternion.identity); // オブジェクトを生成
    }
}
