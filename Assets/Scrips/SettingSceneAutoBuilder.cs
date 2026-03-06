using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SettingSceneAutoBuilder : MonoBehaviour
{
    [ContextMenu("Build Setting UI")]
    public void BuildUI()
    {
        // 1. Tìm hoặc tạo Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGo = new GameObject("Canvas");
            canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGo.AddComponent<CanvasScaler>();
            canvasGo.AddComponent<GraphicRaycaster>();
        }

        // 2. Tạo Panel nền
        GameObject panelGo = new GameObject("BackgroundPanel");
        panelGo.transform.SetParent(canvas.transform, false);
        Image panelImage = panelGo.AddComponent<Image>();
        panelImage.color = new Color(0.1f, 0.1f, 0.2f, 0.8f);
        RectTransform panelRect = panelGo.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.sizeDelta = Vector2.zero;

        // 3. Tạo Tiêu đề
        CreateText(panelGo.transform, "SETTINGS", new Vector2(0, 200), 50);

        // 4. Tạo Music Slider
        CreateText(panelGo.transform, "Music Volume", new Vector2(-150, 50), 24);
        GameObject musicSlider = CreateSlider(panelGo.transform, new Vector2(100, 50));

        // 5. Tạo Sound Slider
        CreateText(panelGo.transform, "Sound Volume", new Vector2(-150, -50), 24);
        GameObject soundSlider = CreateSlider(panelGo.transform, new Vector2(100, -50));

        // 6. Tạo Nút Back
        GameObject backBtn = CreateButton(panelGo.transform, "BACK", new Vector2(0, -200));

        // 7. Gắn script SettingsMenu và kết nối
        SettingsMenu menu = gameObject.GetComponent<SettingsMenu>();
        if (menu == null) menu = gameObject.AddComponent<SettingsMenu>();

        menu.musicSlider = musicSlider.GetComponent<Slider>();
        menu.soundSlider = soundSlider.GetComponent<Slider>();

        // Gán sự kiện nút Back (Cần gán thủ công trong Inspector hoặc qua code này)
        Button b = backBtn.GetComponent<Button>();
        // b.onClick.AddListener(menu.BackToMainMenu); // Sẽ gán qua Inspector cho chắc chắn

        Debug.Log("Đã tạo xong bộ khung UI cho Setting Scene! Hãy nhấn vào nút Back và gán hàm BackToMainMenu vào nhé.");
    }

    private GameObject CreateText(Transform parent, string content, Vector2 pos, float size)
    {
        GameObject go = new GameObject("Text_" + content);
        go.transform.SetParent(parent, false);
        TextMeshProUGUI txt = go.AddComponent<TextMeshProUGUI>();
        txt.text = content;
        txt.fontSize = size;
        txt.alignment = TextAlignmentOptions.Center;
        txt.rectTransform.anchoredPosition = pos;
        return go;
    }

    private GameObject CreateSlider(Transform parent, Vector2 pos)
    {
        // Sử dụng UI Slider mặc định của Unity (phức tạp để tạo hoàn toàn bằng code từ zero)
        // Ở đây ta tạo một GameObject có component Slider cơ bản
        GameObject sliderGo = new GameObject("Slider");
        sliderGo.transform.SetParent(parent, false);
        Slider s = sliderGo.AddComponent<Slider>();
        sliderGo.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 40);
        sliderGo.GetComponent<RectTransform>().anchoredPosition = pos;

        // Lưu ý: Để Slider chạy được cần có Background/Fill/Handle. 
        // Trong Editor nên sử dụng: GameObject -> UI -> Slider sẽ nhanh hơn.
        return sliderGo;
    }

    private GameObject CreateButton(Transform parent, string label, Vector2 pos)
    {
        GameObject btnGo = new GameObject("Button_" + label);
        btnGo.transform.SetParent(parent, false);
        btnGo.AddComponent<RectTransform>().sizeDelta = new Vector2(160, 50);
        btnGo.AddComponent<CanvasRenderer>();
        Image img = btnGo.AddComponent<Image>();
        img.color = Color.white;
        btnGo.AddComponent<Button>();
        btnGo.GetComponent<RectTransform>().anchoredPosition = pos;

        CreateText(btnGo.transform, label, Vector2.zero, 24).GetComponent<TextMeshProUGUI>().color = Color.black;
        return btnGo;
    }
}
