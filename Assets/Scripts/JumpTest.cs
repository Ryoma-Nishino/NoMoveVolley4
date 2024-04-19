using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpTest : MonoBehaviour
{

    public LeftPosition leftPosition;
    public RightPosition rightPosition;

    public Vector3 leftdifference;
    public Vector3 rightdifference;

    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI frequencyText;
    public TextMeshProUGUI countText; // 条件が満たされた回数を表示するためのTextMeshProUGUI
    public TextMeshProUGUI countDownText;
    //public TextMeshProUGUI jumpTMP;

    public float leftLeg = 1;
    public float rightLeg = 1;

    public int condition = 0;

    public GameObject testPlayer;

    public float jumpForce = 20;

    private bool isFirstRight = false; // 最初にRightになったかどうかを追跡するフラグ

    private float countdownTime = 10f; // カウントダウン時間
    private bool isCounting = false; // カウントダウンが進行中かどうかを追跡するフラグ

    private int rightConditionCount = 0; // 条件が満たされた回数を追跡するカウンター
    private float frequency = 0f;
    private int lastCondition = 0; // 前回の状態を追跡する変数

    public float judgement = 0.35f;

    // Start is called before the first frame update
    void Start()
    {
        //testPlayer = GameObject.Find("TestPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        DifferenceReset();

        Test();

        countDownText.text = "CountDownTime:" + countdownTime;

        // 左クリックが押されたらカウントダウンを開始
        if (Input.GetMouseButtonDown(0))
        {
            isCounting = true;
            rightConditionCount = 0;
            lastCondition = 0; // 前回の状態を追跡する変数
}
        countText.text = "RightCount:" + rightConditionCount; // 条件が満たされた回数をテキストとして表示
        frequencyText.text = "Frequency:" + frequency + "Hz"; // 周波数をテキストとして表示
        // カウントダウンが進行中なら時間を減らす
        if (isCounting)
        {
            countdownTime -= Time.deltaTime;
            if (countdownTime <= 0)
            {
                isCounting = false;
                countdownTime = 10f; // カウントダウン時間をリセット
                frequency = rightConditionCount / countdownTime;
                //Debug.Log("60秒経過しました！");
                //rightConditionCountText.text = rightConditionCount.ToString;
                //frequencyText.text = "Frequency：" + frequency + "Hz"; // 周波数をテキストとして表示
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpAction("TestPlayer");
            Debug.Log("スペースキーが押されました！");
        }
    }

    public void DifferenceReset()
    {
        leftdifference = leftPosition.difference;
        rightdifference = rightPosition.difference;
    }

    public void JumpAction(string objectName)
    {
        // Find the game object
        GameObject obj = GameObject.Find(objectName);

        // Check if the object exists
        if (obj != null)
        {
            // Get the Rigidbody component
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            // Check if the Rigidbody component exists
            if (rb != null)
            {
                if (IsGrounded(obj))
                {
                    // Reset the velocity
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    // Apply an upward force
                    //rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                    rb.velocity = Vector3.up * jumpForce;
                }
            }
            else
            {
                Debug.Log("No Rigidbody component found on " + objectName);
            }
        }
        else
        {
            Debug.Log("No game object named " + objectName + " found");
        }
    }

    bool IsGrounded(GameObject obj)
    {
        // Cast a ray downward with a distance just slightly more than the collider bounds
        return Physics.Raycast(obj.transform.position, -Vector3.up, obj.GetComponent<Collider>().bounds.extents.y + 0.1f);
    }

    public void Test()
    {
        if (leftdifference.y >= judgement && rightdifference.y >= judgement)
        {
            textMeshPro.text = "Jump";

            condition = 3;
            if (lastCondition != 3)
            {
                JumpAction("TestPlayer");
            }
            lastCondition = 3;
        }
        else if (leftdifference.y >= judgement && rightdifference.y < judgement)
        {
            textMeshPro.text = "Left";
            condition = 1;
        }
        else if (leftdifference.y < judgement && rightdifference.y >= judgement)
        {
            textMeshPro.text = "Right";
            condition = 2;

            // カウントダウンが進行中ならカウンターを増やす
            if (isCounting && lastCondition != 2)
            {
                rightConditionCount++;
            }
            // 現在の状態を保存
            lastCondition = 2;
        }
        else
        {
            textMeshPro.text = "Stay";
            lastCondition = 0;
            condition = 0;
        }

        if (Input.GetMouseButton(0))
        {
            //JumpAction("TestPlayer");
        }
    }

}
