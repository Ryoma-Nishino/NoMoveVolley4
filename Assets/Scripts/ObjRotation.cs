using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotation : MonoBehaviour
{
    MySerial serial;
    float _pitch;
    float _roll;
    public string portNum;
    public float smoothing = 1f; // スムージングの強さを調整するパラメータ
    //public float minAngle = -45f; // ピッチとロールの最小値
    //public float maxAngle = 45f; // ピッチとロールの最大値

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
                v = Mathf.Repeat(v, 360f); // 角度を0～360度に正規化
                if (v > 180f) v -= 360f; // 角度を-180～180度に変換
                _pitch = Mathf.Lerp(_pitch, v, smoothing); // スムージングを適用
                break;
            case "roll":
                v = Mathf.Repeat(v, 360f); // 角度を0～360度に正規化
                if (v > 180f) v -= 360f; // 角度を-180～180度に変換
                _roll = Mathf.Lerp(_roll, v, smoothing); // スムージングを適用
                break;
        }
        Quaternion AddRot = Quaternion.identity;
        AddRot.eulerAngles = new Vector3(-_pitch, 0, -_roll);
        transform.rotation = AddRot;
    }

}
