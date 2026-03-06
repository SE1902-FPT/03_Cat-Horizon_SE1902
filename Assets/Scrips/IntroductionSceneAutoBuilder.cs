using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroductionSceneAutoBuilder : MonoBehaviour
{
    [ContextMenu("Build Introduction UI")]
    public void BuildUI()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGo = new GameObject("Canvas");
            canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGo.AddComponent<CanvasScaler>();
            canvasGo.AddComponent<GraphicRaycaster>();
        }

        GameObject panelGo = new GameObject("BackgroundPanel");
        panelGo.transform.SetParent(canvas.transform, false);
        panelGo.AddComponent<Image>().color = new Color(0.1f, 0.2f, 0.1f, 0.9f);
        RectTransform panelRect = panelGo.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.sizeDelta = Vector2.zero;

        CreateText(panelGo.transform, "INSTRUCTIONS", new Vector2(0, 220), 50);

        string instructions = "DI CHUYỂN: Phím A / D hoặc Mũi tên\n\nNHẢY: Phím W hoặc Space\n\nBẮN LASER: Phím J hoặc Chuột trái\n\nMỤC TIÊU: Bảo vệ hũ Pate khỏi Alien!";
        CreateText(panelGo.transform, instructions, new Vector2(0, 0), 24);

        GameObject backBtn = CreateButton(panelGo.transform, "BACK", new Vector2(0, -220));

        if (GetComponent<InstructionsMenu>() == null) gameObject.AddComponent<InstructionsMenu>();

        Debug.Log("Đã tạo xong bộ khung UI cho Introduction Scene!");
    }

    private GameObject CreateText(Transform parent, string content, Vector2 pos, float size)
    {
        GameObject go = new GameObject("Text");
        go.transform.SetParent(parent, false);
        TextMeshProUGUI txt = go.AddComponent<TextMeshProUGUI>();
        txt.text = content;
        txt.fontSize = size;
        txt.alignment = TextAlignmentOptions.Center;
        txt.rectTransform.anchoredPosition = pos;
        txt.rectTransform.sizeDelta = new Vector2(600, 300);
        return go;
    }

    private GameObject CreateButton(Transform parent, string label, Vector2 pos)
    {
        GameObject btnGo = new GameObject("Button_" + label);
        btnGo.transform.SetParent(parent, false);
        btnGo.AddComponent<RectTransform>().sizeDelta = new Vector2(160, 50);
        btnGo.AddComponent<CanvasRenderer>();
        btnGo.AddComponent<Image>().color = Color.white;
        btnGo.AddComponent<Button>();
        btnGo.GetComponent<RectTransform>().anchoredPosition = pos;

        CreateText(btnGo.transform, label, Vector2.zero, 24).GetComponent<TextMeshProUGUI>().color = Color.black;
        return btnGo;
    }
}
