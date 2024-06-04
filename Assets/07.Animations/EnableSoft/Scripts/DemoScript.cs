
using System.Collections;
using UnityEngine;

public class DemoScript : MonoBehaviour {

    private Animator _ani;
    public float smoothSpeed = 15f;
    public Vector3 offset;

    private bool _aniReady = true;

    private void Awake()
    {
        _ani = GetComponent<Animator>();
    }

    private void OnGUI()
    {
        if (_aniReady)
        {
            if (GUI.Button(new Rect(50, 50, 100, 30), "Attack_A_01"))
                SetTrigger("Attack_A_01");

            if (GUI.Button(new Rect(160, 50, 100, 30), "Attack_A_02"))
            {
                SetTrigger("Attack_A_01");
                SetTrigger("Attack_A_02");
            }

            if (GUI.Button(new Rect(270, 50, 100, 30), "Attack_A_03"))
            {
                SetTrigger("Attack_A_01");
                SetTrigger("Attack_A_02");
                SetTrigger("Attack_A_03");
            }

            if (GUI.Button(new Rect(50, 90, 100, 30), "Attack_B_01"))
                SetTrigger("Attack_B_01");

            if (GUI.Button(new Rect(160, 90, 100, 30), "Attack_B_02"))
            {
                SetTrigger("Attack_B_01");
                SetTrigger("Attack_B_02");
            }

            if (GUI.Button(new Rect(270, 90, 100, 30), "Attack_B_03"))
            {
                SetTrigger("Attack_B_01");
                SetTrigger("Attack_B_02");
                SetTrigger("Attack_B_03");
            }

            if (GUI.Button(new Rect(380, 90, 100, 30), "Attack_B_04"))
            {
                SetTrigger("Attack_B_01");
                SetTrigger("Attack_B_02");
                SetTrigger("Attack_B_03");
                SetTrigger("Attack_B_04");
            }

            if (GUI.Button(new Rect(50, 130, 100, 30), "Skill_A"))
                SetTrigger("Skill_A");

            if (GUI.Button(new Rect(50, 170, 100, 30), "Skill_B"))
                SetTrigger("Skill_B");

            if (GUI.Button(new Rect(50, 210, 100, 30), "Skill_C"))
                SetTrigger("Skill_C");

            if (GUI.Button(new Rect(50, 250, 100, 30), "Skill_D"))
                SetTrigger("Skill_D");

            if (GUI.Button(new Rect(50, 290, 100, 30), "Skill_E"))
                SetTrigger("Skill_E");

            if (GUI.Button(new Rect(50, 330, 100, 30), "Reaction"))
                SetTrigger("Reaction");

            if (GUI.Button(new Rect(50, 370, 100, 30), "Sturn"))
                SetTrigger("Sturn");

            if (GUI.Button(new Rect(50, 410, 100, 30), "KnockDown"))
                SetTrigger("KnockDown");

            if (GUI.Button(new Rect(50, 450, 100, 30), "Move"))
                SetTrigger("Move");

            if (GUI.Button(new Rect(50, 490, 100, 30), "Die"))
                SetTrigger("Die");

        }
        else
        {
            if (_ani.GetNextAnimatorStateInfo(0).IsName("Base Layer.2HS_Idle"))
            {
                _aniReady = true;
            }
        }
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(Camera.main.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        Camera.main.transform.position = smoothedPosition;
    }
    void effect(string effectname)
    {
        GameObject obj = Instantiate<GameObject>(Resources.Load<GameObject>("Effect/" + effectname));
        obj.transform.position = transform.position;
        StartCoroutine("DestoryEffect", obj);
    }

    IEnumerator DestoryEffect(GameObject obj)
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(obj);
    }

    void SetTrigger(string param)
    {
        _aniReady = false;
        _ani.SetTrigger(param); 
    }
}
