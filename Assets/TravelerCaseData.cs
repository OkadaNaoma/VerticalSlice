using UnityEngine;

[CreateAssetMenu(fileName = "NewTravelerCase", menuName = "Customs Game/Traveler Case Data")]
public class TravelerCaseData : ScriptableObject
{
    [Header("Basic Case Info")]
    public int caseNumber = 1;
    public string travelerName = "Alex Petrov";
    public string travelerStatusAtStart = "Waiting";

    [TextArea(2, 4)]
    public string travelerHint = "Check the passport, declaration, and luggage.";

    [Header("Passport")]
    public string passportCountry = "Arstotzka";
    public string passportId = "P-001";

    [Header("Declaration")]
    [TextArea(2, 5)]
    public string declaredItems = "- Clothes\n- Snacks";

    [Header("Luggage")]
    [TextArea(2, 5)]
    public string foundItems = "Found items: Clothes, Snacks";
    public int visibleBagSlots = 2;

    [Header("Decision")]
    public string correctDecision = "Approve";
    public int correctScore = 10;
    public int wrongPenalty = 5;

    [Header("Time")]
    public float timeLimit = 60f;
    public float inspectTimeCost = 5f;

    [Header("Feedback Text")]
    [TextArea(2, 5)]
    public string startFeedback = "Check the documents. You can approve or reject now, or inspect the luggage if needed.";

    [TextArea(2, 5)]
    public string inspectionFeedback = "Inspection complete. The luggage matches the declaration.";

    [TextArea(2, 5)]
    public string correctFeedbackWithoutInspection = "Correct. The documents looked safe, so approving without inspection was efficient.";

    [TextArea(2, 5)]
    public string correctFeedbackAfterInspection = "Correct. The inspection confirmed the correct decision.";

    [TextArea(2, 5)]
    public string wrongFeedback = "Wrong decision. Check the documents and luggage more carefully.";
}