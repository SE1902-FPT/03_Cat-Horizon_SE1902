using UnityEngine;

public class Story_teller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float scrollSpeed = 30f;

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}
