using UnityEngine;
using TMPro; // Thêm thư viện này
using System.Collections;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private BossController m_Boss;
    [SerializeField] private Transform[] m_WayPoints;
    [SerializeField] private float m_SpawnDelay = 5f;

    [Header("Warning UI")]
    [SerializeField] private TextMeshProUGUI m_WarningText;
    [SerializeField] private float m_WarningDuration = 3f; // Thời gian hiện cảnh báo

    void Start()
    {
        if (m_Boss != null)
        {
            m_Boss.gameObject.SetActive(false);
            if (m_WarningText != null) m_WarningText.gameObject.SetActive(false);

            StartCoroutine(SpawnRoutine());
        }
    }

    IEnumerator SpawnRoutine()
    {
        // 1. Chờ một khoảng thời gian trước khi hiện cảnh báo
        yield return new WaitForSeconds(m_SpawnDelay - m_WarningDuration);

        // 2. Hiện và nhấp nháy dòng chữ Warning
        if (m_WarningText != null)
        {
            m_WarningText.gameObject.SetActive(true);
            float elapsed = 0f;
            while (elapsed < m_WarningDuration)
            {
                // Đảo ngược trạng thái hiển thị (Bật/Tắt) để tạo hiệu ứng nháy
                m_WarningText.enabled = !m_WarningText.enabled;
                yield return new WaitForSeconds(0.3f); // Tốc độ nháy
                elapsed += 0.3f;
            }
            m_WarningText.gameObject.SetActive(false); // Tắt hẳn sau khi xong
        }

        // 3. Xuất hiện Boss
        if (m_Boss != null)
        {
            m_Boss.gameObject.SetActive(true);
            m_Boss.Init(m_WayPoints);
        }
    }
}