using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Cần thiết để sử dụng Coroutine

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    [SerializeField] private GameObject[] hearts;

    // --- PHẦN BỔ SUNG CHO ANIMATION & HIỆU ỨNG ---
    private Animator animator; // Tham chiếu đến Animator
    private SpriteRenderer spriteRenderer; // Tham chiếu đến SpriteRenderer để nháy đỏ
    private bool isInvincible = false; // Biến trạng thái bất tử tạm thời
    [SerializeField] private float invincibilityDuration = 0.5f; // Thời gian bất tử & nhấp nháy
    // ----------------------------------------------

    // --- PHẦN BỔ SUNG CHO GAME OVER ---
    [SerializeField] private GameObject gameOverPanel; // Kéo GameOverPanel vào đây
    // ----------------------------------------------

    void Start()
    {
        currentHealth = maxHealth;
        // Khởi tạo tham chiếu
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Đảm bảo GameOverPanel luôn tắt khi bắt đầu game
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        // Kiểm tra nếu đang bất tử thì không nhận sát thương
        if (isInvincible) return;

        currentHealth -= damage;
        UpdateHealthUI();

        // --- KÍCH HOẠT HIỆU ỨNG ---
        if (animator != null)
        {
            // Set trigger 'isHurt' để chuyển sang animation Hurt
            animator.SetBool("isHurt", true);
            // Bắt đầu hiệu ứng nhấp nháy và bất tử tạm thời
            StartCoroutine(DamageEffectRoutine());
        }
        // -------------------------

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over!");
            // Kích hoạt cửa sổ Game Over
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
            Time.timeScale = 0; // Tạm dừng trò chơi
        }
    }

    // Coroutine xử lý hiệu ứng nhấp nháy và bất tử tạm thời
    IEnumerator DamageEffectRoutine()
    {
        isInvincible = true; // Bật chế độ bất tử

        // Làm cho Sprite nháy đỏ
        spriteRenderer.color = Color.red;
        // Đợi 0.1 giây (hoặc điều chỉnh cho phù hợp với animation Hurt)
        yield return new WaitForSeconds(0.1f);
        // Trả lại màu bình thường
        spriteRenderer.color = Color.white;

        // Đợi thêm một chút trước khi kết thúc bất tử và animation Hurt
        yield return new WaitForSeconds(invincibilityDuration - 0.1f);

        if (animator != null)
        {
            // Trả parameter 'isHurt' về false để chuyển về Idle/Walk
            animator.SetBool("isHurt", false);
        }

        isInvincible = false; // Tắt chế độ bất tử
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentHealth);
        }
    }
}