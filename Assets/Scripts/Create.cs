using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public GameObject objectToSpawn; // ��������I�u�W�F�N�g��Prefab

    private void Start()
    {
        InvokeRepeating("Spawn", 0f, 1f); // 0�b�ォ��J�n���A1�b���Ƃ�Spawn���\�b�h���Ăяo��
    }

    private void Spawn()
    {
        Instantiate(objectToSpawn, transform.position, Quaternion.identity); // �I�u�W�F�N�g�𐶐�
    }
}
