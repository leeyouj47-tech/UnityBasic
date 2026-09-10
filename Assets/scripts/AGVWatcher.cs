using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AGVWatcher : MonoBehaviour
{
    public enum MoveState
    {
        None = 0,

        Watching,

        Tracing,

        Returning,

        Max
    }
    public NavMeshAgent agent;
    public Transform target;
    public MoveState currentState = MoveState.Watching;
    
    [Header("감시 모드 관련")]
    public Transform watchPoint;
    public Transform eyePoint;
    [Range(5f, 100f)]
    public float watchableDistance = 5f;
    [Range(10f, 90f)]
    public float watchableAngle = 10f;
    public float watchRotateSpeed = 20f;
    public float watchRotateAngle = 15f;

    [Header("추적 모드 관련")]
    public GameObject explosionEffect;
    public float interval = 1f;     //AI가 00초에 한번씩 판단함
    public float explosionDistance = 1f;    //플레이어에 접근해서 자폭하는 거리
    public int explosionDamage = 1;     //자폭 데미지
    private bool isLeft = false;       //왼쪽으로 회전 중인가
    private float nextTime = 0f;    //다음 판단 시간.

    //항상화면에 그려주는 함수
    private void OnDrawGizmos()
    {
        //시야 반경을 원형으로 그리기
        if(currentState == MoveState.Watching)
            Handles.color = Color.orange;
        else if(currentState == MoveState.Tracing)
            Handles.color = Color.red;

        Handles.DrawWireDisc(eyePoint.position, Vector3.up, watchableDistance);

        //부채꼴의 왼쪽/오른쪽 경계선 
        Vector3 viewAngleLeft = CalculateAngle(-watchableAngle, true);
        Vector3 viewAngleRight = CalculateAngle(watchableAngle, true);
        Handles.DrawLine(eyePoint.position, viewAngleLeft * watchableDistance + eyePoint.position);
        Handles.DrawLine(eyePoint.position, viewAngleRight * watchableDistance + eyePoint.position);
        if (currentState == MoveState.Watching)
            Handles.color = new Color(1, 0.647f, 0, 0.1f);
        else if (currentState == MoveState.Tracing)
            Handles.color = new Color(1, 0, 0 , 0.1f);

        Handles.DrawSolidArc(eyePoint.position, Vector3.up, viewAngleLeft, watchableAngle * 2f, watchableDistance);
    }
    //게임 오브젝트를 클릭해 선택된 상태일때만 화면에 그려주는 함수
/*    private void OnDrawGizmosSelected()
    {

    }*/

    private Vector3 CalculateAngle(float angle, bool anglesGlobal)
    {
        if (anglesGlobal)
        {
            angle += eyePoint.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angle *  Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    void Start()
    {
        if(agent == null)
            agent = GetComponent<NavMeshAgent>();

        switch(currentState)
        {
            case MoveState.Watching:
                SetWatchMode();
                break;
            case MoveState.Tracing:
                SetTraceMode();
                break;
            default:
                break;
        }

    }

    private void SetTraceMode()
    {
        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.SetDestination(target.position);

        nextTime = Time.time + interval;
        currentState = MoveState.Tracing;
    }

    private void SetWatchMode()
    {
        isLeft = true;
        agent.updatePosition = false;
        agent.updateRotation = false;
        currentState = MoveState.Watching;
    }

    void Update()
    {
        switch (currentState)
        {
            case MoveState.Watching:
                Watch();
                break;
            case MoveState.Tracing:
                Trace();
                break;
            case MoveState.Returning:
                Return();
                break;
            default:
                break;
        }
    }

    private void Trace()
    {
        if(target == null)
        {
            SetReturnMode();
            return;
        }
        if(!CheckTarget(true, out float distance))
        {
            SetReturnMode();
            return;
        }

        if(distance < explosionDistance)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            currentState = MoveState.None;
            return;
        }
        if (nextTime > Time.time)
            return;

        nextTime = Time.time + interval;
        agent.SetDestination(target.position);
    }

    private void SetReturnMode()
    {
        currentState = MoveState.Returning;
        agent.updatePosition = true;
        agent.updateRotation = true;

        //감시 지역으로 되돌아가기
        agent.SetDestination(watchPoint.position);
    }
    private void Return()
    {
        //귀환 도중에 타겟이 감지되면 다시 추적모드로 전환
        if (target != null && CheckTarget(false, out float distance))
        {
            SetTraceMode();
            return ;
        }
        //감시지역까지 도착했는지 확인.
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            SetWatchMode();
        }
    }

    private void Watch()
    {
        Quaternion destination = isLeft ? watchPoint.rotation * Quaternion.Euler(0f, -watchableAngle, 0f) :
            watchPoint.rotation * Quaternion.Euler(0f, watchableAngle, 0f);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, destination, watchRotateSpeed * Time.deltaTime);
        if(Quaternion.Angle (transform.rotation, destination) < 0.001f)
        {
            isLeft = !isLeft;
        }
        if(target != null && CheckTarget(false, out float distance))
        {
            SetTraceMode();
        }
    }

    //타겟이 시야거리, 시야각 안에 감지했는지 확인하고, 감지여부를 반환함
    private bool CheckTarget(bool onlyDistanceCheck, out float targetDistance)
    {
        //타켓의 방향을 알아내야함
        //목적지 위치 - 현재 위치 => 현재 위치기준 목적의 방향과 거리를 구할 수 있다.
        Vector3 toward = target.position - transform.position;
        targetDistance = toward.magnitude;
        if(targetDistance > watchableDistance)
            return false;
        if(onlyDistanceCheck)
            return true;
        //벡터의 내적을 이용해서 정면방향에 목적지가 있는지 값을 구할 수 있다.
        //목적 방향과 일치할 경우 1, 완전 반대방향이면 -1, 90도 방향이면 0
        if(Vector3.Dot(eyePoint.forward, toward.normalized) > Mathf.Cos(watchableAngle * Mathf.Deg2Rad))
        {
            return true;
        }
        return false;
    }
}
