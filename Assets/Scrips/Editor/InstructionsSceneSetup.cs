#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using TMPro;

public class InstructionsSceneSetup : Editor
{
    [MenuItem("Tools/Setup Instructions Scene")]
    public static void SetupInstructionsScene()
    {
        // Open the Introduction scene (we will use it as Instructions)
        string scenePath = "Assets/Scenes/Introduction.unity";
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

        // Add InstructionsManager to Canvas
        InstructionsManager instructionsManager = canvasObj.AddComponent<InstructionsManager>();

        // === Background Panel ===
        GameObject bgPanel = CreatePanel(canvasObj.transform, "BackgroundPanel",
            new Color(0.08f, 0.08f, 0.12f, 1f));
        RectTransform bgRect = bgPanel.GetComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.sizeDelta = Vector2.zero;

        // === Main Content Panel ===
        GameObject contentPanel = new GameObject("ContentPanel");
        contentPanel.transform.SetParent(canvasObj.transform, false);
        RectTransform contentRect = contentPanel.AddComponent<RectTransform>();
        Image contentBg = contentPanel.AddComponent<Image>();
        contentBg.color = new Color(0.12f, 0.12f, 0.2f, 0.95f);
        contentRect.anchorMin = new Vector2(0.15f, 0.08f);
        contentRect.anchorMax = new Vector2(0.85f, 0.92f);
        contentRect.sizeDelta = Vector2.zero;

        // Vertical Layout for content
        VerticalLayoutGroup vlg = contentPanel.AddComponent<VerticalLayoutGroup>();
        vlg.padding = new RectOffset(50, 50, 30, 30);
        vlg.spacing = 20;
        vlg.childAlignment = TextAnchor.UpperCenter;
        vlg.childControlWidth = true;
        vlg.childControlHeight = false;
        vlg.childForceExpandWidth = true;
        vlg.childForceExpandHeight = false;

        // === Header Title (static "HOW TO PLAY") ===
        GameObject headerObj = CreateTMPText(contentPanel.transform, "Header", "HOW TO PLAY",
            50, FontStyles.Bold, new Color(0.4f, 0.7f, 1f, 1f), TextAlignmentOptions.Center, 70);

        // === Divider ===
        GameObject divider = CreatePanel(contentPanel.transform, "Divider",
            new Color(0.4f, 0.7f, 1f, 0.5f));
        LayoutElement divLE = divider.AddComponent<LayoutElement>();
        divLE.preferredHeight = 3;

        // === Page Title (dynamic) ===
        GameObject titleObj = CreateTMPText(contentPanel.transform, "PageTitle", "CONTROLS",
            38, FontStyles.Bold, Color.white, TextAlignmentOptions.Center, 55);
        TextMeshProUGUI titleTMP = titleObj.GetComponent<TextMeshProUGUI>();

        // === Content Text (dynamic) ===
        GameObject contentTextObj = CreateTMPText(contentPanel.transform, "ContentText",
            "<b>Movement:</b>\n• Use <b>Arrow Keys</b> or <b>A/D</b> to move left/right\n• Press <b>Space</b> to jump\n\n<b>Actions:</b>\n• Press <b>E</b> to interact\n• Press <b>Esc</b> to pause",
            26, FontStyles.Normal, new Color(0.85f, 0.85f, 0.9f, 1f),
            TextAlignmentOptions.Left, 350);
        TextMeshProUGUI contentTMP = contentTextObj.GetComponent<TextMeshProUGUI>();
        contentTMP.enableWordWrapping = true;
        contentTMP.richText = true;

        // === Page Indicator ===
        GameObject pageIndicator = CreateTMPText(contentPanel.transform, "PageIndicator", "1 / 3",
            24, FontStyles.Italic, new Color(0.6f, 0.6f, 0.7f, 1f),
            TextAlignmentOptions.Center, 35);
        TextMeshProUGUI pageIndicatorTMP = pageIndicator.GetComponent<TextMeshProUGUI>();

        // === Navigation Buttons Row ===
        GameObject navRow = new GameObject("NavigationRow");
        navRow.transform.SetParent(contentPanel.transform, false);
        RectTransform navRect = navRow.AddComponent<RectTransform>();
        navRect.sizeDelta = new Vector2(0, 60);
        LayoutElement navLE = navRow.AddComponent<LayoutElement>();
        navLE.preferredHeight = 60;

        HorizontalLayoutGroup navHLG = navRow.AddComponent<HorizontalLayoutGroup>();
        navHLG.childAlignment = TextAnchor.MiddleCenter;
        navHLG.childControlWidth = true;
        navHLG.childControlHeight = true;
        navHLG.childForceExpandWidth = true;
        navHLG.childForceExpandHeight = false;
        navHLG.spacing = 30;
        navHLG.padding = new RectOffset(80, 80, 0, 0);

        // Previous Button
        GameObject prevBtn = CreateButton(navRow.transform, "PrevButton", "◄ PREVIOUS",
            new Color(0.3f, 0.4f, 0.6f, 1f), 55);
        Button prevButton = prevBtn.GetComponent<Button>();

        // Next Button
        GameObject nextBtn = CreateButton(navRow.transform, "NextButton", "NEXT ►",
            new Color(0.3f, 0.4f, 0.6f, 1f), 55);
        Button nextButton = nextBtn.GetComponent<Button>();

        // === Back Button ===
        GameObject backBtn = CreateButton(contentPanel.transform, "BackButton", "BACK TO MENU",
            new Color(0.6f, 0.2f, 0.2f, 1f), 55);
        BackToMenu backToMenu = backBtn.AddComponent<BackToMenu>();
        Button backButton = backBtn.GetComponent<Button>();

        // Wire up event callbacks
        UnityEditor.Events.UnityEventTools.AddPersistentListener(
            prevButton.onClick, instructionsManager.PreviousPage);

        UnityEditor.Events.UnityEventTools.AddPersistentListener(
            nextButton.onClick, instructionsManager.NextPage);

        UnityEditor.Events.UnityEventTools.AddPersistentListener(
            backButton.onClick, backToMenu.Back);

        // Assign references to InstructionsManager
        instructionsManager.titleText = titleTMP;
        instructionsManager.contentText = contentTMP;
        instructionsManager.pageIndicatorText = pageIndicatorTMP;
        instructionsManager.nextButton = nextButton;
        instructionsManager.prevButton = prevButton;

        // Save the scene
        EditorSceneManager.SaveScene(scene);
        Debug.Log("Instructions Scene setup complete!");
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
        tmp.enableWordWrapping = true;
        tmp.richText = true;

        LayoutElement le = textObj.AddComponent<LayoutElement>();
        le.preferredHeight = height;

        return textObj;
    }

    private static GameObject CreateButton(Transform parent, string name, string text,
        Color bgColor, float height)
    {
        GameObject btnObj = new GameObject(name);
        btnObj.transform.SetParent(parent, false);
        RectTransform btnRect = btnObj.AddComponent<RectTransform>();
        btnRect.sizeDelta = new Vector2(0, height);

        LayoutElement le = btnObj.AddComponent<LayoutElement>();
        le.preferredHeight = height;

        Image btnImg = btnObj.AddComponent<Image>();
        btnImg.color = bgColor;

        Button btn = btnObj.AddComponent<Button>();
        btn.targetGraphic = btnImg;

        ColorBlock colors = btn.colors;
        colors.normalColor = bgColor;
        colors.highlightedColor = new Color(
            Mathf.Min(bgColor.r + 0.1f, 1f),
            Mathf.Min(bgColor.g + 0.1f, 1f),
            Mathf.Min(bgColor.b + 0.1f, 1f), 1f);
        colors.pressedColor = new Color(
            Mathf.Max(bgColor.r - 0.1f, 0f),
            Mathf.Max(bgColor.g - 0.1f, 0f),
            Mathf.Max(bgColor.b - 0.1f, 0f), 1f);
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
        tmp.fontSize = 24;
        tmp.fontStyle = FontStyles.Bold;
        tmp.color = Color.white;
        tmp.alignment = TextAlignmentOptions.Center;

        return btnObj;
    }
}
#endif
