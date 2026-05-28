using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;


public class SimpleCase1Manager : MonoBehaviour
{
    [Header("Case Data")]
    public TravelerCaseData[] cases;

    [Header("Top Bar")]
    public TextMeshProUGUI caseText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    [Header("Traveler Panel")]
    public TextMeshProUGUI travelerNameText;
    public TextMeshProUGUI travelerStatusText;
    public TextMeshProUGUI travelerHintText;

    [Header("Documents")]
    public TextMeshProUGUI passportNameText;
    public TextMeshProUGUI passportCountryText;
    public TextMeshProUGUI passportIdText;
    public TextMeshProUGUI declarationItemsText;

    [Header("Luggage")]
    public TextMeshProUGUI luggageHintText;
    public GameObject bagSlot1;
    public GameObject bagSlot2;
    public GameObject bagSlot3;
    public GameObject bagSlot4;

    [Header("Feedback")]
    public GameObject feedbackPanel;
    public Image feedbackPanelImage;
    public TextMeshProUGUI feedbackText;
    public SpriteRenderer feedbackShaderEffect;
    public Material feedbackNormalMaterial;
    public Material feedbackCorrectMaterial;
    public Material feedbackWrongMaterial;

    [Header("Tutorial")]
    public GameObject tutorialPanel;

    [Header("Action Buttons")]
    public GameObject inspectButton;
    public GameObject approveButton;
    public GameObject rejectButton;
    public GameObject nextButton;

    private int score = 0;
    private float timeRemaining = 60f;
    private bool inspected = false;
    private bool decided = false;
    private bool gameStarted = false;

    private int currentCaseIndex = 0;
    private TravelerCaseData currentCase;

    void Start()
    {
        if (cases == null || cases.Length == 0)
        {
            Debug.LogError("No TravelerCaseData assets assigned to the cases array.");
            return;
        }

        if (feedbackPanelImage == null && feedbackPanel != null)
        {
            feedbackPanelImage = feedbackPanel.GetComponent<Image>();
        }

        LoadCase(0, true);
    }

    void Update()
    {
        if (!gameStarted) return;
        if (decided) return;
        if (currentCase == null) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0f) timeRemaining = 0f;

        UpdateTimeText();

        if (timeRemaining <= 0f)
        {
            TimeUp();
        }
    }

    void LoadCase(int index, bool showTutorial)
    {
        currentCaseIndex = index;
        currentCase = cases[currentCaseIndex];

        inspected = false;
        decided = false;
        gameStarted = !showTutorial;
        timeRemaining = currentCase.timeLimit;

        caseText.text = "Case " + currentCase.caseNumber;
        scoreText.text = "Score: " + score;
        UpdateTimeText();

        travelerNameText.text = "Name: " + currentCase.travelerName;
        travelerStatusText.text = "Status: " + currentCase.travelerStatusAtStart;
        travelerHintText.text = currentCase.travelerHint;

        passportNameText.text = "Name: " + currentCase.travelerName;
        passportCountryText.text = "Country: " + currentCase.passportCountry;
        passportIdText.text = "ID: " + currentCase.passportId;

        declarationItemsText.text = "Declared Items:\n" + currentCase.declaredItems;
        luggageHintText.text = "Press Inspect to check the luggage.";

        HideAllBagSlots();

        feedbackPanel.SetActive(true);
        SetFeedbackMaterial(feedbackNormalMaterial);

        if (showTutorial)
        {
            tutorialPanel.SetActive(true);
            feedbackText.text = "Read the tutorial, then press Start.";
            SetDecisionButtons(false);
        }
        else
        {
            tutorialPanel.SetActive(false);
            feedbackText.text = currentCase.startFeedback;
            SetDecisionButtons(true);
        }

        SetActiveIfAssigned(nextButton, false);
    }

    public void StartGame()
    {
        if (currentCase == null) return;
        if (decided) return;

        gameStarted = true;
        tutorialPanel.SetActive(false);

        SetDecisionButtons(true);
        SetActiveIfAssigned(nextButton, false);

        feedbackPanel.SetActive(true);
        SetFeedbackMaterial(feedbackNormalMaterial);
        feedbackText.text = currentCase.startFeedback;
    }

    public void InspectCase()
    {
        if (!gameStarted) return;
        if (decided) return;
        if (currentCase == null) return;

        inspected = true;

        timeRemaining -= currentCase.inspectTimeCost;
        if (timeRemaining < 0f) timeRemaining = 0f;
        UpdateTimeText();

        ShowBagSlots(currentCase.visibleBagSlots);

        luggageHintText.text = currentCase.foundItems;
        feedbackPanel.SetActive(true);
        SetFeedbackMaterial(feedbackNormalMaterial);
        feedbackText.text = currentCase.inspectionFeedback;

        if (timeRemaining <= 0f)
        {
            TimeUp();
        }
    }

    public void ApproveCase()
    {
        MakeDecision("Approve");
    }

    public void RejectCase()
    {
        MakeDecision("Reject");
    }

    void MakeDecision(string playerDecision)
    {
        if (!gameStarted) return;
        if (decided) return;
        if (currentCase == null) return;

        decided = true;

        bool correct = playerDecision == currentCase.correctDecision;

        if (correct)
        {
            score += currentCase.correctScore;
            SetFeedbackMaterial(feedbackCorrectMaterial);

            if (inspected)
            {
                feedbackText.text = currentCase.correctFeedbackAfterInspection;
            }
            else
            {
                feedbackText.text = currentCase.correctFeedbackWithoutInspection;
            }
        }
        else
        {
            score -= currentCase.wrongPenalty;
            SetFeedbackMaterial(feedbackWrongMaterial);
            feedbackText.text = currentCase.wrongFeedback;
        }

        travelerStatusText.text = "Status: " + playerDecision;
        scoreText.text = "Score: " + score;

        feedbackPanel.SetActive(true);

        SetDecisionButtons(false);
        SetActiveIfAssigned(nextButton, true);
    }

    void TimeUp()
    {
        decided = true;

        feedbackPanel.SetActive(true);
        SetFeedbackMaterial(feedbackNormalMaterial);
        feedbackText.text = "Time up. Please press Next.";

        SetDecisionButtons(false);
        SetActiveIfAssigned(nextButton, true);
    }

    public void NextCase()
    {
        if (currentCaseIndex + 1 < cases.Length)
        {
            LoadCase(currentCaseIndex + 1, false);
        }
        else
        {
            gameStarted = false;
            decided = true;

            feedbackPanel.SetActive(true);
            SetFeedbackMaterial(feedbackNormalMaterial);
            feedbackText.text = "All cases complete. Final score: " + score;

            SetDecisionButtons(false);
            SetActiveIfAssigned(nextButton, false);
        }
    }

    void UpdateTimeText()
    {
        timeText.text = "Time: " + Mathf.CeilToInt(timeRemaining);
    }

    void SetDecisionButtons(bool visible)
    {
        SetActiveIfAssigned(inspectButton, visible);
        SetActiveIfAssigned(approveButton, visible);
        SetActiveIfAssigned(rejectButton, visible);
    }

    void HideAllBagSlots()
    {
        SetActiveIfAssigned(bagSlot1, false);
        SetActiveIfAssigned(bagSlot2, false);
        SetActiveIfAssigned(bagSlot3, false);
        SetActiveIfAssigned(bagSlot4, false);
    }

    void ShowBagSlots(int count)
    {
        SetActiveIfAssigned(bagSlot1, count >= 1);
        SetActiveIfAssigned(bagSlot2, count >= 2);
        SetActiveIfAssigned(bagSlot3, count >= 3);
        SetActiveIfAssigned(bagSlot4, count >= 4);
    }

    void SetFeedbackMaterial(Material material)
    {
        if (material == null) return;

        if (feedbackPanelImage != null)
        {
            feedbackPanelImage.gameObject.SetActive(true);
            feedbackPanelImage.material = material;
            feedbackPanelImage.color = Color.white;
        }

        if (feedbackShaderEffect != null)
        {
            feedbackShaderEffect.gameObject.SetActive(true);
            feedbackShaderEffect.material = material;
        }
    }

    void SetActiveIfAssigned(GameObject target, bool active)
    {
        if (target != null)
        {
            target.SetActive(active);
        }
    }
}