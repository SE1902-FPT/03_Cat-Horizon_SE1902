#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using TMPro;

public class SettingSceneSetup : Editor
{
    [MenuItem("Tools/Setup Setting Scene")]
    public static void SetupSettingScene()
    {
        // Open the Setting scene
        string scenePath = "Assets/Scenes/Setting.unity";
        var scene = EditorSceneManager.OpenScene(scenePath);

        // Remove existing Canvas if any
        Canvas[] existingCanvases = FindObjectsOfType<Canvas>();
        foreach (var c in existingCanvases)
        {
            DestroyImmediate(c.gameObject);
        }

        // Remove existing EventSystem if any
        var existingES = FindObjectsOfType<UnityEngine.EventSystems.EventSystem>();
        foreach (var es in existingES)
        {
            DestroyImmediate(es.gameObject);
        }

        // === Create EventSystem ===
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
        eventSystem.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();

        // === Create Canvas ===
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;
        canvasObj.AddComponent<GraphicRaycaster>();

        // Add SettingsManager to Canvas
        SettingsManager settingsManager = canvasObj.AddComponent<SettingsManager>();

        // === Background Panel ===
        GameObject bgPanel = CreatePanel(canvasObj.transform, "BackgroundPanel",
            new Color(0.1f, 0.1f, 0.15f, 1f));
        RectTransform bgRect = bgPanel.GetComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.sizeDelta = Vector2.zero;

        // === Content Panel (centered) ===
        GameObject contentPanel = new GameObject("ContentPanel");
        contentPanel.transform.SetParent(canvasObj.transform, false);
        RectTransform contentRect = contentPanel.AddComponent<RectTransform>();
        Image contentBg = contentPanel.AddComponent<Image>();
        contentBg.color = new Color(0.15f, 0.15f, 0.22f, 0.9f);
        contentRect.anchorMin = new Vector2(0.2f, 0.1f);
        contentRect.anchorMax = new Vector2(0.8f, 0.9f);
        contentRect.sizeDelta = Vector2.zero;

        // Add Vertical Layout Group
        VerticalLayoutGroup vlg = contentPanel.AddComponent<VerticalLayoutGroup>();
        vlg.padding = new RectOffset(60, 60, 40, 40);
        vlg.spacing = 25;
        vlg.childAlignment = TextAnchor.UpperCenter;
        vlg.childControlWidth = true;
        vlg.childControlHeight = false;
        vlg.childForceExpandWidth = true;
        vlg.childForceExpandHeight = false;

        // === Title ===
        GameObject titleObj = CreateTMPText(contentPanel.transform, "Title", "SETTINGS",
            60, FontStyles.Bold, Color.white, TextAlignmentOptions.Center, 80);

        // === Volume Section ===
        CreateTMPText(contentPanel.transform, "VolumeLabel", "Master Volume",
            28, FontStyles.Normal, new Color(0.8f, 0.8f, 0.8f), TextAlignmentOptions.Left, 40);

        GameObject volumeSliderObj = CreateSlider(contentPanel.transform, "VolumeSlider", 50);
        Slider volumeSlider = volumeSliderObj.GetComponent<Slider>();

        // === SFX Section ===
        CreateTMPText(contentPanel.transform, "SFXLabel", "SFX Volume",
            28, FontStyles.Normal, new Color(0.8f, 0.8f, 0.8f), TextAlignmentOptions.Left, 40);

        GameObject sfxSliderObj = CreateSlider(contentPanel.transform, "SFXSlider", 50);
        Slider sfxSlider = sfxSliderObj.GetComponent<Slider>();

        // === Music Toggle Section ===
        GameObject musicRow = CreateRowContainer(contentPanel.transform, "MusicRow", 50);
        CreateTMPText(musicRow.transform, "MusicLabel", "Music",
            28, FontStyles.Normal, new Color(0.8f, 0.8f, 0.8f), TextAlignmentOptions.Left, 50);
        GameObject musicToggleObj = CreateToggle(musicRow.transform, "MusicToggle", 50);
        Toggle musicToggle = musicToggleObj.GetComponent<Toggle>();

        // === Resolution Section ===
        CreateTMPText(contentPanel.transform, "ResolutionLabel", "Resolution",
            28, FontStyles.Normal, new Color(0.8f, 0.8f, 0.8f), TextAlignmentOptions.Left, 40);

        GameObject dropdownObj = CreateDropdown(contentPanel.transform, "ResolutionDropdown", 50);
        TMP_Dropdown dropdown = dropdownObj.GetComponent<TMP_Dropdown>();

        // === Fullscreen Toggle Section ===
        GameObject fullscreenRow = CreateRowContainer(contentPanel.transform, "FullscreenRow", 50);
        CreateTMPText(fullscreenRow.transform, "FullscreenLabel", "Fullscreen",
            28, FontStyles.Normal, new Color(0.8f, 0.8f, 0.8f), TextAlignmentOptions.Left, 50);
        GameObject fullscreenToggleObj = CreateToggle(fullscreenRow.transform, "FullscreenToggle", 50);
        Toggle fullscreenToggle = fullscreenToggleObj.GetComponent<Toggle>();

        // === Back Button ===
        GameObject backBtn = CreateButton(contentPanel.transform, "BackButton", "BACK", 60);
        BackToMenu backToMenu = backBtn.AddComponent<BackToMenu>();
        Button backButton = backBtn.GetComponent<Button>();

        // Wire up event callbacks using UnityEditor serialization
        UnityEditor.Events.UnityEventTools.AddPersistentListener(
            volumeSlider.onValueChanged, settingsManager.ChangeVolume);

        UnityEditor.Events.UnityEventTools.AddPersistentListener(
            sfxSlider.onValueChanged, settingsManager.ChangeSFXVolume);

        UnityEditor.Events.UnityEventTools.AddPersistentListener(
            musicToggle.onValueChanged, settingsManager.ToggleMusic);

        UnityEditor.Events.UnityEventTools.AddPersistentListener(
            dropdown.onValueChanged, settingsManager.SetResolution);

        UnityEditor.Events.UnityEventTools.AddPersistentListener(
            fullscreenToggle.onValueChanged, settingsManager.SetFullscreen);

        UnityEditor.Events.UnityEventTools.AddPersistentListener(
            backButton.onClick, backToMenu.Back);

        // Assign references to SettingsManager
        settingsManager.volumeSlider = volumeSlider;
        settingsManager.sfxSlider = sfxSlider;
        settingsManager.musicToggle = musicToggle;
        settingsManager.resolutionDropdown = dropdown;
        settingsManager.fullscreenToggle = fullscreenToggle;

        // Save the scene
        EditorSceneManager.SaveScene(scene);
        Debug.Log("Setting Scene setup complete!");
    }

    // ============ HELPER METHODS ============

    private static GameObject CreatePanel(Transform parent, string name, Color color)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent, false);
        RectTransform rect = panel.AddComponent<RectTransform>();
        Image img = panel.AddComponent<Image>();
        img.color = color;
        return panel;
    }

    private static GameObject CreateTMPText(Transform parent, string name, string text,
        int fontSize, FontStyles style, Color color, TextAlignmentOptions alignment, float height)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent, false);
        RectTransform rect = textObj.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0, height);

        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.fontStyle = style;
        tmp.color = color;
        tmp.alignment = alignment;

        LayoutElement le = textObj.AddComponent<LayoutElement>();
        le.preferredHeight = height;

        return textObj;
    }

    private static GameObject CreateSlider(Transform parent, string name, float height)
    {
        GameObject sliderObj = new GameObject(name);
        sliderObj.transform.SetParent(parent, false);
        RectTransform sliderRect = sliderObj.AddComponent<RectTransform>();
        sliderRect.sizeDelta = new Vector2(0, height);

        LayoutElement le = sliderObj.AddComponent<LayoutElement>();
        le.preferredHeight = height;

        // Background
        GameObject bg = new GameObject("Background");
        bg.transform.SetParent(sliderObj.transform, false);
        RectTransform bgRect = bg.AddComponent<RectTransform>();
        Image bgImg = bg.AddComponent<Image>();
        bgImg.color = new Color(0.3f, 0.3f, 0.4f, 1f);
        bgRect.anchorMin = new Vector2(0, 0.25f);
        bgRect.anchorMax = new Vector2(1, 0.75f);
        bgRect.sizeDelta = Vector2.zero;

        // Fill Area
        GameObject fillArea = new GameObject("Fill Area");
        fillArea.transform.SetParent(sliderObj.transform, false);
        RectTransform fillAreaRect = fillArea.AddComponent<RectTransform>();
        fillAreaRect.anchorMin = new Vector2(0, 0.25f);
        fillAreaRect.anchorMax = new Vector2(1, 0.75f);
        fillAreaRect.offsetMin = new Vector2(5, 0);
        fillAreaRect.offsetMax = new Vector2(-5, 0);

        // Fill
        GameObject fill = new GameObject("Fill");
        fill.transform.SetParent(fillArea.transform, false);
        RectTransform fillRect = fill.AddComponent<RectTransform>();
        Image fillImg = fill.AddComponent<Image>();
        fillImg.color = new Color(0.4f, 0.7f, 1f, 1f);
        fillRect.sizeDelta = new Vector2(0, 0);
        fillRect.anchorMin = Vector2.zero;
        fillRect.anchorMax = Vector2.zero;

        // Handle Slide Area
        GameObject handleArea = new GameObject("Handle Slide Area");
        handleArea.transform.SetParent(sliderObj.transform, false);
        RectTransform handleAreaRect = handleArea.AddComponent<RectTransform>();
        handleAreaRect.anchorMin = new Vector2(0, 0);
        handleAreaRect.anchorMax = new Vector2(1, 1);
        handleAreaRect.offsetMin = new Vector2(10, 0);
        handleAreaRect.offsetMax = new Vector2(-10, 0);

        // Handle
        GameObject handle = new GameObject("Handle");
        handle.transform.SetParent(handleArea.transform, false);
        RectTransform handleRect = handle.AddComponent<RectTransform>();
        Image handleImg = handle.AddComponent<Image>();
        handleImg.color = Color.white;
        handleRect.sizeDelta = new Vector2(20, 0);
        handleRect.anchorMin = Vector2.zero;
        handleRect.anchorMax = Vector2.zero;

        // Setup Slider component
        Slider slider = sliderObj.AddComponent<Slider>();
        slider.fillRect = fillRect;
        slider.handleRect = handleRect;
        slider.targetGraphic = handleImg;
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.value = 1;

        return sliderObj;
    }

    private static GameObject CreateToggle(Transform parent, string name, float height)
    {
        GameObject toggleObj = new GameObject(name);
        toggleObj.transform.SetParent(parent, false);
        RectTransform toggleRect = toggleObj.AddComponent<RectTransform>();
        toggleRect.sizeDelta = new Vector2(50, height);

        LayoutElement le = toggleObj.AddComponent<LayoutElement>();
        le.preferredWidth = 50;
        le.preferredHeight = height;

        // Background
        GameObject bg = new GameObject("Background");
        bg.transform.SetParent(toggleObj.transform, false);
        RectTransform bgRect = bg.AddComponent<RectTransform>();
        Image bgImg = bg.AddComponent<Image>();
        bgImg.color = new Color(0.3f, 0.3f, 0.4f, 1f);
        bgRect.anchorMin = new Vector2(0.5f, 0.5f);
        bgRect.anchorMax = new Vector2(0.5f, 0.5f);
        bgRect.sizeDelta = new Vector2(40, 40);

        // Checkmark
        GameObject checkmark = new GameObject("Checkmark");
        checkmark.transform.SetParent(bg.transform, false);
        RectTransform checkRect = checkmark.AddComponent<RectTransform>();
        Image checkImg = checkmark.AddComponent<Image>();
        checkImg.color = new Color(0.4f, 0.7f, 1f, 1f);
        checkRect.anchorMin = new Vector2(0.5f, 0.5f);
        checkRect.anchorMax = new Vector2(0.5f, 0.5f);
        checkRect.sizeDelta = new Vector2(28, 28);

        // Setup Toggle component
        Toggle toggle = toggleObj.AddComponent<Toggle>();
        toggle.targetGraphic = bgImg;
        toggle.graphic = checkImg;
        toggle.isOn = true;

        return toggleObj;
    }

    private static GameObject CreateDropdown(Transform parent, string name, float height)
    {
        GameObject ddObj = new GameObject(name);
        ddObj.transform.SetParent(parent, false);
        RectTransform ddRect = ddObj.AddComponent<RectTransform>();
        ddRect.sizeDelta = new Vector2(0, height);

        LayoutElement le = ddObj.AddComponent<LayoutElement>();
        le.preferredHeight = height;

        // Background image
        Image ddBg = ddObj.AddComponent<Image>();
        ddBg.color = new Color(0.25f, 0.25f, 0.35f, 1f);

        // Label
        GameObject label = new GameObject("Label");
        label.transform.SetParent(ddObj.transform, false);
        RectTransform labelRect = label.AddComponent<RectTransform>();
        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = new Vector2(10, 0);
        labelRect.offsetMax = new Vector2(-30, 0);
        TextMeshProUGUI labelTMP = label.AddComponent<TextMeshProUGUI>();
        labelTMP.text = "Select...";
        labelTMP.fontSize = 22;
        labelTMP.color = Color.white;
        labelTMP.alignment = TextAlignmentOptions.Left;

        // Arrow
        GameObject arrow = new GameObject("Arrow");
        arrow.transform.SetParent(ddObj.transform, false);
        RectTransform arrowRect = arrow.AddComponent<RectTransform>();
        arrowRect.anchorMin = new Vector2(1f, 0f);
        arrowRect.anchorMax = new Vector2(1f, 1f);
        arrowRect.sizeDelta = new Vector2(30, 0);
        arrowRect.anchoredPosition = new Vector2(-15, 0);
        TextMeshProUGUI arrowTMP = arrow.AddComponent<TextMeshProUGUI>();
        arrowTMP.text = "\u25BC";
        arrowTMP.fontSize = 18;
        arrowTMP.color = Color.white;
        arrowTMP.alignment = TextAlignmentOptions.Center;

        // Template
        GameObject template = new GameObject("Template");
        template.transform.SetParent(ddObj.transform, false);
        template.SetActive(false);
        RectTransform templateRect = template.AddComponent<RectTransform>();
        templateRect.anchorMin = new Vector2(0, 0);
        templateRect.anchorMax = new Vector2(1, 0);
        templateRect.pivot = new Vector2(0.5f, 1f);
        templateRect.sizeDelta = new Vector2(0, 200);
        Image templateBg = template.AddComponent<Image>();
        templateBg.color = new Color(0.2f, 0.2f, 0.3f, 1f);
        ScrollRect scrollRect = template.AddComponent<ScrollRect>();

        // Viewport
        GameObject viewport = new GameObject("Viewport");
        viewport.transform.SetParent(template.transform, false);
        RectTransform viewportRect = viewport.AddComponent<RectTransform>();
        viewportRect.anchorMin = Vector2.zero;
        viewportRect.anchorMax = Vector2.one;
        viewportRect.sizeDelta = Vector2.zero;
        viewport.AddComponent<Image>().color = new Color(1, 1, 1, 0.01f);
        viewport.AddComponent<Mask>().showMaskGraphic = false;

        // Content
        GameObject content = new GameObject("Content");
        content.transform.SetParent(viewport.transform, false);
        RectTransform contentRt = content.AddComponent<RectTransform>();
        contentRt.anchorMin = new Vector2(0, 1);
        contentRt.anchorMax = new Vector2(1, 1);
        contentRt.pivot = new Vector2(0.5f, 1f);
        contentRt.sizeDelta = new Vector2(0, 30);

        // Item
        GameObject item = new GameObject("Item");
        item.transform.SetParent(content.transform, false);
        RectTransform itemRect = item.AddComponent<RectTransform>();
        itemRect.anchorMin = new Vector2(0, 0.5f);
        itemRect.anchorMax = new Vector2(1, 0.5f);
        itemRect.sizeDelta = new Vector2(0, 30);
        Toggle itemToggle = item.AddComponent<Toggle>();

        // Item Label
        GameObject itemLabel = new GameObject("Item Label");
        itemLabel.transform.SetParent(item.transform, false);
        RectTransform itemLabelRect = itemLabel.AddComponent<RectTransform>();
        itemLabelRect.anchorMin = Vector2.zero;
        itemLabelRect.anchorMax = Vector2.one;
        itemLabelRect.sizeDelta = Vector2.zero;
        itemLabelRect.offsetMin = new Vector2(10, 0);
        TextMeshProUGUI itemLabelTMP = itemLabel.AddComponent<TextMeshProUGUI>();
        itemLabelTMP.text = "Option";
        itemLabelTMP.fontSize = 20;
        itemLabelTMP.color = Color.white;

        scrollRect.viewport = viewportRect;
        scrollRect.content = contentRt;

        // Setup Dropdown
        TMP_Dropdown dropdown = ddObj.AddComponent<TMP_Dropdown>();
        dropdown.targetGraphic = ddBg;
        dropdown.template = templateRect;
        dropdown.captionText = labelTMP;
        dropdown.itemText = itemLabelTMP;

        return ddObj;
    }

    private static GameObject CreateButton(Transform parent, string name, string text, float height)
    {
        GameObject btnObj = new GameObject(name);
        btnObj.transform.SetParent(parent, false);
        RectTransform btnRect = btnObj.AddComponent<RectTransform>();
        btnRect.sizeDelta = new Vector2(0, height);

        LayoutElement le = btnObj.AddComponent<LayoutElement>();
        le.preferredHeight = height;

        Image btnImg = btnObj.AddComponent<Image>();
        btnImg.color = new Color(0.3f, 0.5f, 0.8f, 1f);

        Button btn = btnObj.AddComponent<Button>();
        btn.targetGraphic = btnImg;

        ColorBlock colors = btn.colors;
        colors.normalColor = new Color(0.3f, 0.5f, 0.8f, 1f);
        colors.highlightedColor = new Color(0.4f, 0.6f, 0.9f, 1f);
        colors.pressedColor = new Color(0.2f, 0.4f, 0.7f, 1f);
        btn.colors = colors;

        // Button text
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(btnObj.transform, false);
        RectTransform textRect = textObj.AddComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;
        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = 30;
        tmp.fontStyle = FontStyles.Bold;
        tmp.color = Color.white;
        tmp.alignment = TextAlignmentOptions.Center;

        return btnObj;
    }

    private static GameObject CreateRowContainer(Transform parent, string name, float height)
    {
        GameObject row = new GameObject(name);
        row.transform.SetParent(parent, false);
        RectTransform rowRect = row.AddComponent<RectTransform>();
        rowRect.sizeDelta = new Vector2(0, height);

        LayoutElement le = row.AddComponent<LayoutElement>();
        le.preferredHeight = height;

        HorizontalLayoutGroup hlg = row.AddComponent<HorizontalLayoutGroup>();
        hlg.childAlignment = TextAnchor.MiddleLeft;
        hlg.childControlWidth = true;
        hlg.childControlHeight = true;
        hlg.childForceExpandWidth = true;
        hlg.childForceExpandHeight = false;
        hlg.spacing = 20;

        return row;
    }
}
#endif
