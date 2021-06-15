using UnityEngine;
using UnityEngine.AI;

public class NavMove : MonoBehaviour
{
    public NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    public LayerMask layer;

    public float velocity;
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitData, 1000, layer))
            {
                //이동 -> Jogging 애니 시작
                agent.destination = hitData.point;

                //끝나면 -> idle 애니 시작
            }
        }
        velocity = agent.velocity.sqrMagnitude;
        animator.SetFloat("velocity", velocity);
    }
    public Vector3[] corners;
    private void OnDrawGizmos()
    {
        NavMeshPath path = agent.path;
        // 2 : 0 -> 1
        // 3 : 1 -> 2
        if (path == null)
            return;
        corners = path.corners;
        for (int i = 0; i < path.corners.Length; i++)
        {
            if (i == 0)
            {
                //지금 위치랑 최종 목적지랑 표시 
                var pos1 = path.corners[i];
                var pos2 = transform.position;
                Gizmos.DrawLine(pos1, pos2);

            }
            else // 1,2,3...
            {
                var pos1 = path.corners[i];
                var pos2 = path.corners [i-1];
                Gizmos.DrawLine(pos1, pos2);
            }
        }
    }
}
