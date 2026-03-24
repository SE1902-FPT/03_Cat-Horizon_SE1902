using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private Transform[] m_WayPoints;
    [SerializeField] private BossAtkController m_Projectile;
    [SerializeField] private Transform m_FiringPoint;
    [SerializeField] private float m_FiringCooldown;

    private float m_TempCooldown;
    private int m_CurrentWayPointIndex;
    private bool m_Active;

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
        if (transform.position == m_WayPoints[nextWayPoint].position)
            m_CurrentWayPointIndex = nextWayPoint;

        if (m_TempCooldown <= 0)
        {
            Fire();
            m_TempCooldown = m_FiringCooldown;
        }

        m_TempCooldown -= Time.deltaTime;
    }

    public void Init(Transform[] wayPoints)
    {
        m_WayPoints = wayPoints;
        m_Active = true;
        transform.position = wayPoints[0].position;
        m_TempCooldown = m_FiringCooldown;
    }

    private void Fire()
    {
        BossAtkController projectile = Instantiate(m_Projectile, m_FiringPoint.position, Quaternion.identity, null);
        projectile.Fire();
    }


}