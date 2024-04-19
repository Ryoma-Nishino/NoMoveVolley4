using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro���g�p���邽�߂�namespace

public class RightPosition : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // �\������TextMeshProUGUI��Inspector����ݒ�
    public Vector3 position2;
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

    /*public Vector3 GetPosition2()
    {
        return position2;
    }*/
}

