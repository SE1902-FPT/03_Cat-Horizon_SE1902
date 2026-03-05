using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Material material;

    [SerializeField]
    private float parallaxFactor = 0.1f; // Độ nhạy của hiệu ứng trôi

    private float offset;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        ParallaxScroll();
    }

    private void ParallaxScroll()
    {
        // --- THAY ĐỔI QUAN TRỌNG ---
        // Lấy hướng di chuyển trực tiếp từ script Movement
        float moveDirection = Movement.HorizontalInput;

        // Công thức mới: Offset chỉ tăng khi moveDirection khác 0 (khi nhân vật di chuyển)
        // Nếu moveDirection = 0, toàn bộ phép nhân bằng 0 -> Map đứng yên
        offset += moveDirection * parallaxFactor * Time.deltaTime;

        // Cập nhật vị trí ảnh (Texture Offset) theo trục ngang (X)
        material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        // ---------------------------
    }
}