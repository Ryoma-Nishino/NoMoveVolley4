using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProを使用するためのnamespace

public class LeftPosition : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // 表示するTextMeshProUGUIをInspectorから設定
    Vector3 position2;
    public Vector3 difference;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get the position of the game object
        Vector3 position = transform.position;

        // Log the position
        Debug.Log("Position: " + position);

        if (Input.GetMouseButton(0))
        {
            position2 = transform.position;
        }
        difference = position - position2;

        // Display the difference on the screen using TextMeshProUGUI
        textMeshPro.text = "Difference: " + difference.ToString();
    }

    public void ClickPosition()
    {
        if (Input.GetMouseButton(0))
        {
            position2 = transform.position;
        }
    }
}
