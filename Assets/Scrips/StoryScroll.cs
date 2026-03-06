using UnityEngine;

public class StoryScroll : MonoBehaviour
{
    public float scrollSpeed = 30f;

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}
