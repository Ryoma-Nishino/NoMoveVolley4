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
    public TextMeshProUGUI countText; // �������������ꂽ�񐔂�\�����邽�߂�TextMeshProUGUI
    public TextMeshProUGUI countDownText;
    //public TextMeshProUGUI jumpTMP;

    public float leftLeg = 1;
    public float rightLeg = 1;

    public int condition = 0;

    public GameObject testPlayer;

    public float jumpForce = 20;

    private bool isFirstRight = false; // �ŏ���Right�ɂȂ������ǂ�����ǐՂ���t���O

    private float countdownTime = 10f; // �J�E���g�_�E������
    private bool isCounting = false; // �J�E���g�_�E�����i�s�����ǂ�����ǐՂ���t���O

    private int rightConditionCount = 0; // �������������ꂽ�񐔂�ǐՂ���J�E���^�[
    private float frequency = 0f;
    private int lastCondition = 0; // �O��̏�Ԃ�ǐՂ���ϐ�

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

        // ���N���b�N�������ꂽ��J�E���g�_�E�����J�n
        if (Input.GetMouseButtonDown(0))
        {
            isCounting = true;
            rightConditionCount = 0;
            lastCondition = 0; // �O��̏�Ԃ�ǐՂ���ϐ�
}
        countText.text = "RightCount:" + rightConditionCount; // �������������ꂽ�񐔂��e�L�X�g�Ƃ��ĕ\��
        frequencyText.text = "Frequency:" + frequency + "Hz"; // ���g�����e�L�X�g�Ƃ��ĕ\��
        // �J�E���g�_�E�����i�s���Ȃ玞�Ԃ����炷
        if (isCounting)
        {
            countdownTime -= Time.deltaTime;
            if (countdownTime <= 0)
            {
                isCounting = false;
                countdownTime = 10f; // �J�E���g�_�E�����Ԃ����Z�b�g
                frequency = rightConditionCount / countdownTime;
                //Debug.Log("60�b�o�߂��܂����I");
                //rightConditionCountText.text = rightConditionCount.ToString;
                //frequencyText.text = "Frequency�F" + frequency + "Hz"; // ���g�����e�L�X�g�Ƃ��ĕ\��
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpAction("TestPlayer");
            Debug.Log("�X�y�[�X�L�[��������܂����I");
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

            // �J�E���g�_�E�����i�s���Ȃ�J�E���^�[�𑝂₷
            if (isCounting && lastCondition != 2)
            {
                rightConditionCount++;
            }
            // ���݂̏�Ԃ�ۑ�
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
