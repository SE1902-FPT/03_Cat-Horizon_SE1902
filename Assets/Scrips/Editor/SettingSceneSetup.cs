#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using TMPro;

/// <summary>
/// Editor utility — builds the Setting scene UI automatically.
/// Menu: Tools > Setup Setting Scene
/// </summary>
public class SettingSceneSetup : MonoBehaviour
{
    [MenuItem("Tools/Setup Setting Scene")]
    public static void SetupScene()
    {
        // ── Open the Setting scene ──
        string scenePath = "Assets/Scenes/Setting.unity";
        var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

        // ── Clean existing root objects (except Camera) ──
        foreach (var go in scene.GetRootGameObjects())
        {
            if (go.GetComponent<Camera>() == null)
                Object.DestroyImmediate(go);
        }

        // ── Canvas ──
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGO.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
        canvasGO.AddComponent<GraphicRaycaster>();

        // ── EventSystem ──
        if (Object.FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<UnityEngine.EventSystems.EventSystem>();
            es.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        }

        // ── Background Panel ──
        GameObject bgPanel = CreatePanel(canvasGO.transform, "BackgroundPanel",
            new Color(0.12f, 0.12f, 0.18f, 0.95f));
        SetStretch(bgPanel.GetComponent<RectTransform>());

        // ── Title ──
        GameObject titleGO = CreateTMPText(canvasGO.transform, "TitleText", "SETTINGS",
            48, FontStyles.Bold, TextAlignmentOptions.Center);
        RectTransform titleRT = titleGO.GetComponent<RectTransform>();
        titleRT.anchorMin = new Vector2(0.5f, 1f);
        titleRT.anchorMax = new Vector2(0.5f, 1f);
        titleRT.pivot = new Vector2(0.5f, 1f);
        titleRT.anchoredPosition = new Vector2(0, -40);
        titleRT.sizeDelta = new Vector2(600, 70);

        // ── Settings Panel (vertical layout) ──
        GameObject settingsPanel = new GameObject("SettingsPanel");
        settingsPanel.transform.SetParent(canvasGO.transform, false);
        RectTransform spRT = settingsPanel.AddComponent<RectTransform>();
        spRT.anchorMin = new Vector2(0.5f, 0.5f);
        spRT.anchorMax = new Vector2(0.5f, 0.5f);
        spRT.sizeDelta = new Vector2(500, 400);
        spRT.anchoredPosition = new Vector2(0, 20);
        VerticalLayoutGroup vlg = settingsPanel.AddComponent<VerticalLayoutGroup>();
        vlg.spacing = 30;
        vlg.childAlignment = TextAnchor.MiddleCenter;
        vlg.childControlWidth = true;
        vlg.childControlHeight = false;
        vlg.childForceExpandWidth = true;
        vlg.childForceExpandHeight = false;

        // ── Sound Volume row ──
        GameObject soundRow = CreateSliderRow(settingsPanel.transform, "SoundRow", "Sound Volume");

        // ── Music Volume row ──
        GameObject musicRow = CreateSliderRow(settingsPanel.transform, "MusicRow", "Music Volume");

        // ── Music Toggle row ──
        GameObject toggleRow = CreateToggleRow(settingsPanel.transform, "MusicToggleRow", "Music On/Off");

        // ── Back Button ──
        GameObject backBtnGO = CreateButton(canvasGO.transform, "BackButton", "BACK",
            new Color(0.85f, 0.25f, 0.25f, 1f));
        RectTransform backRT = backBtnGO.GetComponent<RectTransform>();
        backRT.anchorMin = new Vector2(0.5f, 0f);
        backRT.anchorMax = new Vector2(0.5f, 0f);
        backRT.pivot = new Vector2(0.5f, 0f);
        backRT.anchoredPosition = new Vector2(0, 50);
        backRT.sizeDelta = new Vector2(200, 55);

        // ── Attach SettingsManager ──
        SettingsManager sm = canvasGO.AddComponent<SettingsManager>();
        sm.soundSlider = soundRow.GetComponentInChildren<Slider>();
        sm.musicSlider = musicRow.GetComponentInChildren<Slider>();
        sm.musicToggle = toggleRow.GetComponentInChildren<Toggle>();
        sm.backButton  = backBtnGO.GetComponent<Button>();

        // Try to find value labels
        var soundTexts = soundRow.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var t in soundTexts) { if (t.gameObject.name == "ValueText") sm.soundValueText = t; }
        var musicTexts = musicRow.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var t in musicTexts) { if (t.gameObject.name == "ValueText") sm.musicValueText = t; }

        // ── Save ──
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        Debug.Log("[SettingSceneSetup] Setting scene built successfully!");
    }

    // ─────────────── Helper methods ───────────────

    static GameObject CreatePanel(Transform parent, string name, Color color)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        Image img = go.AddComponent<Image>();
        img.color = color;
        return go;
    }

    static void SetStretch(RectTransform rt)
    {
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    static GameObject CreateTMPText(Transform parent, string name, string text,
        float fontSize, FontStyles style, TextAlignmentOptions alignment)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        TextMeshProUGUI tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.fontStyle = style;
        tmp.alignment = alignment;
        tmp.color = Color.white;
        return go;
    }

    static GameObject CreateSliderRow(Transform parent, string name, string label)
    {
        // Row container
        GameObject row = new GameObject(name);
        row.transform.SetParent(parent, false);
        RectTransform rowRT = row.AddComponent<RectTransform>();
        rowRT.sizeDelta = new Vector2(500, 50);
        HorizontalLayoutGroup hlg = row.AddComponent<HorizontalLayoutGroup>();
        hlg.spacing = 15;
        hlg.childAlignment = TextAnchor.MiddleCenter;
        hlg.childControlWidth = false;
        hlg.childControlHeight = true;
        hlg.childForceExpandWidth = false;
        hlg.childForceExpandHeight = true;

        // Label
        GameObject labelGO = CreateTMPText(row.transform, "Label", label, 22, FontStyles.Normal, TextAlignmentOptions.Left);
        labelGO.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 50);

        // Slider
        GameObject sliderGO = DefaultControls.CreateSlider(new DefaultControls.Resources());
        sliderGO.name = "Slider";
        sliderGO.transform.SetParent(row.transform, false);
        sliderGO.GetComponent<RectTransform>().sizeDelta = new Vector2(230, 30);
        Slider slider = sliderGO.GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 1f;

        // Value text
        GameObject valueGO = CreateTMPText(row.transform, "ValueText", "100%", 20, FontStyles.Normal, TextAlignmentOptions.Right);
        valueGO.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 50);

        return row;
    }

    static GameObject CreateToggleRow(Transform parent, string name, string label)
    {
        GameObject row = new GameObject(name);
        row.transform.SetParent(parent, false);
        RectTransform rowRT = row.AddComponent<RectTransform>();
        rowRT.sizeDelta = new Vector2(500, 50);
        HorizontalLayoutGroup hlg = row.AddComponent<HorizontalLayoutGroup>();
        hlg.spacing = 15;
        hlg.childAlignment = TextAnchor.MiddleCenter;
        hlg.childControlWidth = false;
        hlg.childControlHeight = true;
        hlg.childForceExpandWidth = false;
        hlg.childForceExpandHeight = true;

        // Label
        GameObject labelGO = CreateTMPText(row.transform, "Label", label, 22, FontStyles.Normal, TextAlignmentOptions.Left);
        labelGO.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 50);

        // Toggle
        GameObject toggleGO = DefaultControls.CreateToggle(new DefaultControls.Resources());
        toggleGO.name = "Toggle";
        toggleGO.transform.SetParent(row.transform, false);
        toggleGO.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 40);
        toggleGO.GetComponent<Toggle>().isOn = true;

        return row;
    }

    static GameObject CreateButton(Transform parent, string name, string label, Color bgColor)
    {
        GameObject btnGO = DefaultControls.CreateButton(new DefaultControls.Resources());
        btnGO.name = name;
        btnGO.transform.SetParent(parent, false);
        btnGO.GetComponent<Image>().color = bgColor;

        // Replace default Text with TMP
        Text oldText = btnGO.GetComponentInChildren<Text>();
        if (oldText != null) Object.DestroyImmediate(oldText.gameObject);

        GameObject txtGO = new GameObject("ButtonText");
        txtGO.transform.SetParent(btnGO.transform, false);
        TextMeshProUGUI tmp = txtGO.AddComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.fontSize = 24;
        tmp.fontStyle = FontStyles.Bold;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;
        RectTransform txtRT = txtGO.GetComponent<RectTransform>();
        txtRT.anchorMin = Vector2.zero;
        txtRT.anchorMax = Vector2.one;
        txtRT.offsetMin = Vector2.zero;
        txtRT.offsetMax = Vector2.zero;

        return btnGO;
    }
}
#endif
