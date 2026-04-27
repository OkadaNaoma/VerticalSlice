using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    private int score = 0;
    private float timeRemaining = 60f;
    private bool inspected = false;
    private bool decided = false;
    private string correctDecision = "Approve";

    void Start()
    {
        SetupCase1();
    }

    void Update()
    {
        if (decided) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0f) timeRemaining = 0f;

        timeText.text = "Time:" + Mathf.CeilToInt(timeRemaining);

        if (timeRemaining <= 0f)
        {
            decided = true;
            feedbackPanel.SetActive(true);
            feedbackText.text = "Time up. Please restart the demo.";
        }
    }

    void SetupCase1()
    {
        inspected = false;
        decided = false;
        timeRemaining = 60f;
        correctDecision = "Approve";

        caseText.text = "Case 1";
        scoreText.text = "Score: " + score;
        timeText.text = "Time:60";

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
        feedbackText.text = "Inspect the case, then approve or reject.";
    }

    public void InspectCase()
    {
        if (decided) return;

        inspected = true;

        bagSlot1.SetActive(true);
        bagSlot2.SetActive(true);
        bagSlot3.SetActive(false);
        bagSlot4.SetActive(false);

        luggageHintText.text = "Found items: Clothes, Snacks";
        feedbackPanel.SetActive(true);
        feedbackText.text = "Inspection complete. The luggage matches the declaration.";
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
        if (decided) return;

        decided = true;

        bool correct = (playerDecision == correctDecision);

        if (correct)
        {
            score += 10;
            feedbackText.text = "Correct. This traveler should be approved.";
        }
        else
        {
            score -= 5;
            feedbackText.text = "Wrong. This traveler was safe to approve.";
        }

        scoreText.text = "Score: " + score;
    }

    public void NextCase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}