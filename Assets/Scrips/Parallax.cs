using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Material material;
    [SerializeField] 
    private float parallaxFactor = 0.01f;
    private float offset;
    public float gameSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        ParallaxScroll();
    }

    private void ParallaxScroll()
    {
        // 1. Lấy tốc độ từ GameManager (nếu có) để đồng bộ
        float speed = gameSpeed * parallaxFactor;

        // 2. Tính toán tốc độ trôi dựa trên hệ số Parallax
        offset += Time.deltaTime * speed;

        

        // 4. Set lại offset cho Texture (SỬA LỖI Ở ĐÂY: _MainTex)
        material.SetTextureOffset("_MainTex", Vector2.right * offset);
    }
}
