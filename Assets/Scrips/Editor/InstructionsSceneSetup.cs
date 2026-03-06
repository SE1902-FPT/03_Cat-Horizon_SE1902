#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using TMPro;

/// <summary>
/// Editor utility — builds the Instructions scene UI automatically.
/// Menu: Tools > Setup Instructions Scene
/// </summary>
public class InstructionsSceneSetup : MonoBehaviour
{
    [MenuItem("Tools/Setup Instructions Scene")]
    public static void SetupScene()
    {
        // ── Open the Introduction scene ──
        string scenePath = "Assets/Scenes/Introduction.unity";
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
        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
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
            new Color(0.10f, 0.10f, 0.16f, 0.95f));
        SetStretch(bgPanel.GetComponent<RectTransform>());

        // ── Header Title ("HOW TO PLAY") ──
        GameObject headerGO = CreateTMPText(canvasGO.transform, "HeaderText", "HOW TO PLAY",
            52, FontStyles.Bold, TextAlignmentOptions.Center);
        RectTransform headerRT = headerGO.GetComponent<RectTransform>();
        headerRT.anchorMin = new Vector2(0.5f, 1f);
        headerRT.anchorMax = new Vector2(0.5f, 1f);
        headerRT.pivot = new Vector2(0.5f, 1f);
        headerRT.anchoredPosition = new Vector2(0, -30);
        headerRT.sizeDelta = new Vector2(700, 70);

        // ── Content Panel ──
        GameObject contentPanel = new GameObject("ContentPanel");
        contentPanel.transform.SetParent(canvasGO.transform, false);
        RectTransform cpRT = contentPanel.AddComponent<RectTransform>();
        cpRT.anchorMin = new Vector2(0.5f, 0.5f);
        cpRT.anchorMax = new Vector2(0.5f, 0.5f);
        cpRT.sizeDelta = new Vector2(750, 500);
        cpRT.anchoredPosition = new Vector2(0, 20);

        // Background for content
        Image cpImg = contentPanel.AddComponent<Image>();
        cpImg.color = new Color(0.15f, 0.15f, 0.22f, 0.8f);

        // ── Page Title ──
        GameObject pageTitleGO = CreateTMPText(contentPanel.transform, "PageTitle", "CONTROLS",
            36, FontStyles.Bold, TextAlignmentOptions.Center);
        RectTransform ptRT = pageTitleGO.GetComponent<RectTransform>();
        ptRT.anchorMin = new Vector2(0.5f, 1f);
        ptRT.anchorMax = new Vector2(0.5f, 1f);
        ptRT.pivot = new Vector2(0.5f, 1f);
        ptRT.anchoredPosition = new Vector2(0, -15);
        ptRT.sizeDelta = new Vector2(700, 50);

        // ── Content Text ──
        GameObject contentTextGO = CreateTMPText(contentPanel.transform, "ContentText",
            "Content goes here...", 22, FontStyles.Normal, TextAlignmentOptions.TopLeft);
        RectTransform ctRT = contentTextGO.GetComponent<RectTransform>();
        ctRT.anchorMin = new Vector2(0f, 0f);
        ctRT.anchorMax = new Vector2(1f, 1f);
        ctRT.offsetMin = new Vector2(40, 20);
        ctRT.offsetMax = new Vector2(-40, -75);
        contentTextGO.GetComponent<TextMeshProUGUI>().enableWordWrapping = true;
        contentTextGO.GetComponent<TextMeshProUGUI>().richText = true;

        // ── Page Indicator ──
        GameObject pageIndGO = CreateTMPText(canvasGO.transform, "PageIndicator", "1 / 3",
            24, FontStyles.Italic, TextAlignmentOptions.Center);
        RectTransform piRT = pageIndGO.GetComponent<RectTransform>();
        piRT.anchorMin = new Vector2(0.5f, 0f);
        piRT.anchorMax = new Vector2(0.5f, 0f);
        piRT.pivot = new Vector2(0.5f, 0f);
        piRT.anchoredPosition = new Vector2(0, 130);
        piRT.sizeDelta = new Vector2(200, 40);

        // ── Previous Button ──
        GameObject prevBtnGO = CreateButton(canvasGO.transform, "PreviousButton", "< PREVIOUS",
            new Color(0.3f, 0.5f, 0.8f, 1f));
        RectTransform prevRT = prevBtnGO.GetComponent<RectTransform>();
        prevRT.anchorMin = new Vector2(0.5f, 0f);
        prevRT.anchorMax = new Vector2(0.5f, 0f);
        prevRT.pivot = new Vector2(0.5f, 0f);
        prevRT.anchoredPosition = new Vector2(-150, 70);
        prevRT.sizeDelta = new Vector2(200, 50);

        // ── Next Button ──
        GameObject nextBtnGO = CreateButton(canvasGO.transform, "NextButton", "NEXT >",
            new Color(0.3f, 0.5f, 0.8f, 1f));
        RectTransform nextRT = nextBtnGO.GetComponent<RectTransform>();
        nextRT.anchorMin = new Vector2(0.5f, 0f);
        nextRT.anchorMax = new Vector2(0.5f, 0f);
        nextRT.pivot = new Vector2(0.5f, 0f);
        nextRT.anchoredPosition = new Vector2(150, 70);
        nextRT.sizeDelta = new Vector2(200, 50);

        // ── Back Button ──
        GameObject backBtnGO = CreateButton(canvasGO.transform, "BackButton", "BACK TO MENU",
            new Color(0.85f, 0.25f, 0.25f, 1f));
        RectTransform backRT = backBtnGO.GetComponent<RectTransform>();
        backRT.anchorMin = new Vector2(0.5f, 0f);
        backRT.anchorMax = new Vector2(0.5f, 0f);
        backRT.pivot = new Vector2(0.5f, 0f);
        backRT.anchoredPosition = new Vector2(0, 15);
        backRT.sizeDelta = new Vector2(250, 50);

        // ── Attach InstructionsManager ──
        InstructionsManager im = canvasGO.AddComponent<InstructionsManager>();
        im.titleText      = pageTitleGO.GetComponent<TextMeshProUGUI>();
        im.contentText    = contentTextGO.GetComponent<TextMeshProUGUI>();
        im.pageIndicator  = pageIndGO.GetComponent<TextMeshProUGUI>();
        im.previousButton = prevBtnGO.GetComponent<Button>();
        im.nextButton     = nextBtnGO.GetComponent<Button>();
        im.backButton     = backBtnGO.GetComponent<Button>();

        // ── Save ──
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        Debug.Log("[InstructionsSceneSetup] Instructions scene built successfully!");
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
        tmp.fontSize = 22;
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
