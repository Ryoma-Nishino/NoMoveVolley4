using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Example : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    private Joycon.Button? m_pressedButtonL;
    private Joycon.Button? m_pressedButtonR;

    public GameObject objectToMove; // Add this line to define the object to move

    float a = 1.1f;

    private float moveMultiplier = 1.0f; // 移動量の倍率。この値を変更することで移動量を調整できます。

    private void Start()
    {
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);
        m_joyconR = m_joycons.Find(c => !c.isLeft);
        
    }

    private void Update()
    {
        m_pressedButtonL = null;
        m_pressedButtonR = null;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        foreach (var button in m_buttons)
        {
            if (m_joyconL.GetButton(button))
            {
                m_pressedButtonL = button;
            }
            if (m_joyconR.GetButton(button))
            {
                m_pressedButtonR = button;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_joyconL.SetRumble(160, 320, 0.6f, 200);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            m_joyconR.SetRumble(160, 320, 0.6f, 200);
        }

        // 加速度を取得
        var accelL = m_joyconL.GetAccel();
        var accelR = m_joyconR.GetAccel();

        // 各成分の絶対値が閾値を超えているかどうかをチェック
        if ((Mathf.Abs(accelL.x) > a || Mathf.Abs(accelL.y) > a || Mathf.Abs(accelL.z) > a) &&
            !(Mathf.Abs(accelR.x) > a || Mathf.Abs(accelR.y) > a || Mathf.Abs(accelL.z) > a))
        {
            Debug.Log("Joy-Con (L) の加速度：" + accelL.sqrMagnitude);
            objectToMove.transform.Translate(Vector3.forward * Time.deltaTime * Mathf.Abs(accelL.sqrMagnitude) * moveMultiplier); // Move the object forward based on the absolute value of acceleration
        }
        else if ((Mathf.Abs(accelR.x) > a || Mathf.Abs(accelR.y) > a || Mathf.Abs(accelR.z) > a) &&
                 !(Mathf.Abs(accelL.x) > a || Mathf.Abs(accelL.y) > a || Mathf.Abs(accelR.z) > a))
        {
            Debug.Log("Joy-Con (R) の加速度：" + accelR.sqrMagnitude);
            objectToMove.transform.Translate(Vector3.forward * Time.deltaTime * Mathf.Abs(accelR.sqrMagnitude) * moveMultiplier); // Move the object forward based on the absolute value of acceleration
        }
    }


    private void OnGUI()
    {
        var style = GUI.skin.GetStyle("label");
        style.fontSize = 24;

        if (m_joycons == null || m_joycons.Count <= 0)
        {
            GUILayout.Label("Joy-Con が接続されていません");
            return;
        }

        if (!m_joycons.Any(c => c.isLeft))
        {
            GUILayout.Label("Joy-Con (L) が接続されていません");
            return;
        }

        if (!m_joycons.Any(c => !c.isLeft))
        {
            GUILayout.Label("Joy-Con (R) が接続されていません");
            return;
        }

        GUILayout.BeginHorizontal(GUILayout.Width(960));

        foreach (var joycon in m_joycons)
        {
            var isLeft = joycon.isLeft;
            var name = isLeft ? "Joy-Con (L)" : "Joy-Con (R)";
            var key = isLeft ? "Z キー" : "X キー";
            var button = isLeft ? m_pressedButtonL : m_pressedButtonR;
            var stick = joycon.GetStick();
            var gyro = joycon.GetGyro();
            var accel = joycon.GetAccel();
            var orientation = joycon.GetVector();

            GUILayout.BeginVertical(GUILayout.Width(480));
            GUILayout.Label(name);
            GUILayout.Label(key + "：振動");
            GUILayout.Label("押されているボタン：" + button);
            GUILayout.Label(string.Format("スティック：({0}, {1})", stick[0], stick[1]));
            GUILayout.Label("ジャイロ：" + gyro);
            GUILayout.Label("加速度：" + accel);
            GUILayout.Label("傾き：" + orientation);
            GUILayout.EndVertical();
        }

        GUILayout.EndHorizontal();
    }
}