using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotation : MonoBehaviour
{
    MySerial serial;
    float _pitch;
    float _roll;
    public string portNum;
    public float smoothing = 1f; // �X���[�W���O�̋����𒲐�����p�����[�^
    //public float minAngle = -45f; // �s�b�`�ƃ��[���̍ŏ��l
    //public float maxAngle = 45f; // �s�b�`�ƃ��[���̍ő�l

    // Start is called before the first frame update
    void Start()
    {
        serial = MySerial.Instance;
        bool success = serial.Open(portNum, MySerial.Baudrate.B_115200);
        if (!success)
        {
            return;
        }
        serial.OnDataReceived += SerialCallBack;
    }

    private void OnDisable()
    {
        serial.Close();
        serial.OnDataReceived -= SerialCallBack;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SerialCallBack(string m)
    {
        objRotation(m);
    }

    void objRotation(string message)
    {
        string[] a;
        a = message.Split("="[0]);
        if (a.Length != 2)
        {
            return;
        }
        float v = float.Parse(a[1]);
        switch (a[0])
        {
            case "pitch":
                v = Mathf.Repeat(v, 360f); // �p�x��0�`360�x�ɐ��K��
                if (v > 180f) v -= 360f; // �p�x��-180�`180�x�ɕϊ�
                _pitch = Mathf.Lerp(_pitch, v, smoothing); // �X���[�W���O��K�p
                break;
            case "roll":
                v = Mathf.Repeat(v, 360f); // �p�x��0�`360�x�ɐ��K��
                if (v > 180f) v -= 360f; // �p�x��-180�`180�x�ɕϊ�
                _roll = Mathf.Lerp(_roll, v, smoothing); // �X���[�W���O��K�p
                break;
        }
        Quaternion AddRot = Quaternion.identity;
        AddRot.eulerAngles = new Vector3(-_pitch, 0, -_roll);
        transform.rotation = AddRot;
    }

}
