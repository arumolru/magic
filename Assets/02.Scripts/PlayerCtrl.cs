using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCtrl : MonoBehaviour
{
    Animator anim; // 플레이어의 애니메이터 컴포넌트

    private float moveSpeed = 3f; // 플레이어의 이동 속도

    private float teleportCoolTime = 5f; // 플레이어의 텔포 쿨타임

    private bool isTeleported = false; // 텔포 재사용 가능
    private bool isMoveable = true; // 텔포를 쓰고 잠시 동안 이동 불가능

    private float timeCounter = 0.1f; // 플레이어 이동 애니메이션을 보다 부드럽게 하기 위한 변수

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 플레이어의 횡방향 이동
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 move = new Vector3(0, 0, Time.deltaTime * moveSpeed * h);

        // 플레이어가 이동 가능한 상황일 때
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

        // 플레이어의 점프(teleport) 이동
        if(Input.GetButtonDown("Jump") && !isTeleported)
        {
            Teleport();
        }

        // 플레이어의 윗 텔레포트 이동
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !isTeleported)
        {
            UpTeleport();
        }

        // 플레이어의 아래 텔레포트 이동
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isTeleported)
        {
            DownTeleport();
        }
    }

    void Teleport()
    {
        anim.SetTrigger("IsTeleport");
        isMoveable = false; // 플레이어의 이동 불가능
        isTeleported = true; // 텔레포트 쿨타임 시작

        // 방향에 따라 플레이어의 텔레포트 방향 설정
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
        isMoveable = false; // 플레이어의 이동 불가능
        isTeleported = true; // 텔레포트 쿨타임 시작

        transform.position += new Vector3(0, 3f, 0);

        StartCoroutine(MoveCool());
        StartCoroutine(TeleportCool());
    }

    void DownTeleport()
    {
        anim.SetTrigger("IsTeleport");
        isMoveable = false; // 플레이어의 이동 불가능
        isTeleported = true; // 텔레포트 쿨타임 시작

        transform.position += new Vector3(0, -3f, 0);

        StartCoroutine(MoveCool());
        StartCoroutine(TeleportCool());
    }

    // 잠시동안 플레이어가 이동 불가능
    IEnumerator MoveCool()
    {
        yield return new WaitForSeconds(0.867f);

        isMoveable = true;
    }

    // 텔레포트 쿨타임
    IEnumerator TeleportCool()
    {
        yield return new WaitForSeconds(teleportCoolTime);

        isTeleported = false;
    }
}
