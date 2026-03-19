using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages the Instructions (How To Play) scene.
/// Displays 3 pages of content with Previous / Next navigation.
/// </summary>
public class InstructionsManager : MonoBehaviour
{
    // ───────────────────────── UI References ─────────────────────────

    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;
    public TextMeshProUGUI pageIndicator;
    public Button previousButton;
    public Button nextButton;
    public Button backButton;

    // ───────────────────────── Page Data ─────────────────────────

    private int currentPage = 0;

    private readonly string[] pageTitles = new string[]
    {
        "CONTROLS",
        "OBJECTIVES",
        "TIPS & TRICKS"
    };

    private readonly string[] pageContents = new string[]
    {
        // Page 1: Controls
        "<b>Movement:</b>\n" +
        "  Arrow Keys / WASD — Move character\n\n" +
        "<b>Jump:</b>\n" +
        "  Space — Jump\n\n" +
        "<b>Attack:</b>\n" +
        "  Left Click / Z — Shoot laser\n\n" +
        "<b>Pause:</b>\n" +
        "  ESC — Pause game",

        // Page 2: Objectives
        "<b>Protect the Pate Jar!</b>\n" +
        "  Aliens are trying to steal your precious pate.\n" +
        "  Defeat all enemies to protect it!\n\n" +
        "<b>Enemies:</b>\n" +
        "  • Regular aliens — easy to beat\n" +
        "  • Fast aliens — move quickly\n" +
        "  • Boss aliens — tough and powerful\n\n" +
        "<b>Scoring:</b>\n" +
        "  Each enemy gives points.\n" +
        "  Clear all enemies to advance to the next level!",

        // Page 3: Tips
        "<b>Tips & Tricks:</b>\n\n" +
        "  • Keep moving to dodge enemy attacks\n" +
        "  • Focus on fast aliens first\n" +
        "  • Save your power-ups for boss fights\n" +
        "  • Watch your health bar!\n" +
        "  • Explore each level for hidden items\n" +
        "  • Boss enemies have attack patterns —\n    learn them to win easily!"
    };

    // ───────────────────────── Unity lifecycle ─────────────────────────

    private void Start()
    {
        if (previousButton != null) previousButton.onClick.AddListener(PreviousPage);
        if (nextButton != null) nextButton.onClick.AddListener(NextPage);
        if (backButton != null) backButton.onClick.AddListener(GoBack);

        ShowPage(0);
    }

    // ───────────────────────── Navigation ─────────────────────────

    public void NextPage()
    {
        if (currentPage < pageTitles.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // ───────────────────────── Display ─────────────────────────

    private void ShowPage(int index)
    {
        currentPage = Mathf.Clamp(index, 0, pageTitles.Length - 1);

        if (titleText != null)
            titleText.text = pageTitles[currentPage];
        if (contentText != null)
            contentText.text = pageContents[currentPage];
        if (pageIndicator != null)
            pageIndicator.text = (currentPage + 1) + " / " + pageTitles.Length;

        // Enable/disable nav buttons at boundaries
        if (previousButton != null) previousButton.interactable = currentPage > 0;
        if (nextButton != null) nextButton.interactable = currentPage < pageTitles.Length - 1;
    }
}
