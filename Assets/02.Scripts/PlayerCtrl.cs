using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCtrl : MonoBehaviour
{
    Animator anim; // �÷��̾��� �ִϸ����� ������Ʈ

    private float moveSpeed = 3f; // �÷��̾��� �̵� �ӵ�

    private float teleportCoolTime = 5f; // �÷��̾��� ���� ��Ÿ��

    private bool isTeleported = false; // ���� ���� ����
    private bool isMoveable = true; // ������ ���� ��� ���� �̵� �Ұ���

    private float timeCounter = 0.1f; // �÷��̾� �̵� �ִϸ��̼��� ���� �ε巴�� �ϱ� ���� ����

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // �÷��̾��� Ⱦ���� �̵�
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 move = new Vector3(0, 0, Time.deltaTime * moveSpeed * h);

        // �÷��̾ �̵� ������ ��Ȳ�� ��
        if (isMoveable)
        {
            anim.SetBool("IsMove", true);

            if (h > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(move);
            }
            else if (h < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(-move);
            }

            if (h == 0)
            {
                timeCounter += Time.deltaTime;

                if (timeCounter >= 0.1f)
                {
                    anim.SetBool("IsMove", false);
                }
            }
            else
            {
                timeCounter = 0;
            }
        }

        // �÷��̾��� ����(teleport) �̵�
        if(Input.GetButtonDown("Jump") && !isTeleported)
        {
            Teleport();
        }

        // �÷��̾��� �� �ڷ���Ʈ �̵�
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !isTeleported)
        {
            UpTeleport();
        }

        // �÷��̾��� �Ʒ� �ڷ���Ʈ �̵�
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isTeleported)
        {
            DownTeleport();
        }
    }

    void Teleport()
    {
        anim.SetTrigger("IsTeleport");
        isMoveable = false; // �÷��̾��� �̵� �Ұ���
        isTeleported = true; // �ڷ���Ʈ ��Ÿ�� ����

        // ���⿡ ���� �÷��̾��� �ڷ���Ʈ ���� ����
        if (transform.rotation.eulerAngles.y < 5)
            transform.position += new Vector3(0, 0, 3f);

        else
            transform.position = new Vector3(0, 0, -3f);

        StartCoroutine(MoveCool());
        StartCoroutine(TeleportCool());
    }

    void UpTeleport()
    {
        anim.SetTrigger("IsTeleport");
        isMoveable = false; // �÷��̾��� �̵� �Ұ���
        isTeleported = true; // �ڷ���Ʈ ��Ÿ�� ����

        transform.position += new Vector3(0, 3f, 0);

        StartCoroutine(MoveCool());
        StartCoroutine(TeleportCool());
    }

    void DownTeleport()
    {
        anim.SetTrigger("IsTeleport");
        isMoveable = false; // �÷��̾��� �̵� �Ұ���
        isTeleported = true; // �ڷ���Ʈ ��Ÿ�� ����

        transform.position += new Vector3(0, -3f, 0);

        StartCoroutine(MoveCool());
        StartCoroutine(TeleportCool());
    }

    // ��õ��� �÷��̾ �̵� �Ұ���
    IEnumerator MoveCool()
    {
        yield return new WaitForSeconds(0.867f);

        isMoveable = true;
    }

    // �ڷ���Ʈ ��Ÿ��
    IEnumerator TeleportCool()
    {
        yield return new WaitForSeconds(teleportCoolTime);

        isTeleported = false;
    }
}
