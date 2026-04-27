using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class SimpleCase1Manager : MonoBehaviour
{
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
    public TextMeshProUGUI feedbackText;

    [Header("Visual Scripting")]
    public GameObject vsFeedbackController;

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
    private string correctDecision = "Approve";

    void Start()
    {
        SetupCase1();
        ShowTutorial();
    }

    void Update()
    {
        if (!gameStarted) return;
        if (decided) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0f) timeRemaining = 0f;

        UpdateTimeText();

        if (timeRemaining <= 0f)
        {
            TimeUp();
        }
    }

    void SetupCase1()
    {
        inspected = false;
        decided = false;
        gameStarted = false;
        timeRemaining = 60f;
        correctDecision = "Approve";

        caseText.text = "Case 1";
        scoreText.text = "Score: " + score;
        UpdateTimeText();

        travelerNameText.text = "Name: Alex Petrov";
        travelerStatusText.text = "Status: Waiting";
        travelerHintText.text = "Check the passport, declaration, and luggage.";

        passportNameText.text = "Name: Alex Petrov";
        passportCountryText.text = "Country: Arstotzka";
        passportIdText.text = "ID: P-001";

        declarationItemsText.text = "Declared Items:\n- Clothes\n- Snacks";
        luggageHintText.text = "Press Inspect to check the luggage.";

        bagSlot1.SetActive(false);
        bagSlot2.SetActive(false);
        bagSlot3.SetActive(false);
        bagSlot4.SetActive(false);

        feedbackPanel.SetActive(true);
        feedbackText.text = "Read the tutorial, then press Start.";
    }

    void ShowTutorial()
    {
        tutorialPanel.SetActive(true);

        SetDecisionButtons(false);
        SetActiveIfAssigned(nextButton, false);
    }

    public void StartGame()
    {
        if (decided) return;

        gameStarted = true;
        tutorialPanel.SetActive(false);

        SetDecisionButtons(true);
        SetActiveIfAssigned(nextButton, false);

        feedbackPanel.SetActive(true);
        feedbackText.text = "Check the documents. You can approve or reject now, or inspect the luggage if needed.";
    }

    public void InspectCase()
    {
        if (!gameStarted) return;
        if (decided) return;

        inspected = true;

        // Inspection takes time, so the player should use it carefully.
        timeRemaining -= 5f;
        if (timeRemaining < 0f) timeRemaining = 0f;
        UpdateTimeText();

        bagSlot1.SetActive(true);
        bagSlot2.SetActive(true);
        bagSlot3.SetActive(false);
        bagSlot4.SetActive(false);

        luggageHintText.text = "Found items: Clothes, Snacks";
        feedbackPanel.SetActive(true);
        feedbackText.text = "Inspection complete. The luggage matches the declaration.";

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

        decided = true;

        bool correct = (playerDecision == correctDecision);

        if (correct)
        {
            score += 10;

            if (inspected)
            {
                feedbackText.text = "Correct. The inspection confirmed that this traveler should be approved.";
            }
            else
            {
                feedbackText.text = "Correct. The documents looked safe, so approving without inspection was efficient.";
            }
        }
        else
        {
            score -= 5;
            feedbackText.text = "Wrong. This traveler was safe to approve.";
        }

        travelerStatusText.text = "Status: " + playerDecision;
        scoreText.text = "Score: " + score;
        if (vsFeedbackController != null)
        {
            CustomEvent.Trigger(vsFeedbackController, "DecisionMade");
        }

        feedbackPanel.SetActive(true);

        SetDecisionButtons(false);
        SetActiveIfAssigned(nextButton, true);
    }

    void TimeUp()
    {
        decided = true;

        feedbackPanel.SetActive(true);
        feedbackText.text = "Time up. Please restart the demo.";

        SetDecisionButtons(false);
        SetActiveIfAssigned(nextButton, true);
    }

    void UpdateTimeText()
    {
        timeText.text = "Time:" + Mathf.CeilToInt(timeRemaining);
    }

    void SetDecisionButtons(bool visible)
    {
        SetActiveIfAssigned(inspectButton, visible);
        SetActiveIfAssigned(approveButton, visible);
        SetActiveIfAssigned(rejectButton, visible);
    }

    void SetActiveIfAssigned(GameObject target, bool active)
    {
        if (target != null)
        {
            target.SetActive(active);
        }
    }

    public void NextCase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}