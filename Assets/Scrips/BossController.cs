using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private Transform[] m_WayPoints;

    private int m_CurrentWayPointIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int nextWayPoint = m_CurrentWayPointIndex + 1;
        if (nextWayPoint > m_WayPoints.Length - 1)
            nextWayPoint = 0;

        transform.position = Vector3.MoveTowards(transform.position, m_WayPoints[nextWayPoint].position, m_MoveSpeed * Time.deltaTime);
        if (transform.position == m_WayPoints[nextWayPoint].position )
            m_CurrentWayPointIndex = nextWayPoint;
    }
}
