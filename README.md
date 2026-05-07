# GDIM33 Vertical Slice
## Milestone 1 Devlog
## Visual Scripting Graph Explanation
For this build, I used a Visual Scripting State Machine called `VSFeedbackStateMachine`. This graph has two states: `Waiting` and `DecisionFeedback`. The game starts in the `Waiting` state while the player is checking the traveler’s documents and luggage. When the player clicks Approve or Reject, my C# script sends a custom event called `DecisionMade` to the Visual Scripting graph. This event triggers the transition from `Waiting` to `DecisionFeedback`. I also added a Debug Log in the `DecisionFeedback` state to confirm that the state machine is working. The main gameplay is controlled by C#, but this graph handles the state change after the player makes a decision.

## Updated Break-down
![Updated break-down](DevlogFiles/GDIM33_VerticalSlice_Break-down_Milestone1.png)

I updated my break-down by adding the state machine system. In the previous version, the break-down mainly showed the documents, luggage, buttons, timer, score, and feedback UI. In the updated version, I added `VSFeedbackStateMachine` and connected it to the decision buttons and the feedback system. The Approve and Reject buttons call the main case manager script, and then the script sends the `DecisionMade` event to the state machine.

The state machine starts in the `Waiting` state while the player is checking the case. After the player chooses Approve or Reject, it moves to the `DecisionFeedback` state. This state is related to the feedback UI because the game shows whether the decision was correct or not after the decision is made. It is also related to the score system because the score changes at the same time. For Milestone 1, this state machine is simple, but it helps organize the game flow and can be expanded later with more states, such as Tutorial, Inspecting, Result, and NextCase.


## Milestone 2 Devlog
1. For Milestone 2, I will improve the final shift summary. The game already shows a basic completed message and final score, but I want the ending to give the player more useful information. The improved summary will show the final score, completed cases, remaining time, and a short performance message.

1) Add more information to the final summary.
   1. Find the part of the game that currently shows the “Completed” message.
   2. Add text for the number of completed cases, such as “Cases Completed: 3 / 3.”
   3. Add text for the remaining time, such as “Time Left: 35s.”
   4. Keep the existing final score text.
   5. Run the game and test if the new text appears after the final traveler.

2) Add a simple performance message.
   1. Use the final score to decide what message to show.
   2. If the score is high, show a positive message, such as “Good inspection!”
   3. If the score is low, show a message like “Needs more careful inspection.”
   4. Test the game with different results if possible.
   5. Check if the message changes correctly based on the final score.

3) Make the ending state clear to the player.
   1. Make sure the Inspect, Approve, and Reject buttons are not useful after the final case.
   2. Keep the Restart button visible.
   3. Make sure the timer does not confuse the player after the game is completed.
   4. Run the game from the beginning and complete all three cases.
   5. Confirm that the final summary appears clearly at the end.

2. 


## Milestone 3 Devlog
Milestone 3 Devlog goes here.
## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.
## Open-source assets
- Modular Characters by Kenney  
  License: Creative Commons CC0  
  URL: https://kenney.nl/assets/modular-characters
- Platformer Art Candy by Kenney  
  License: Creative Commons CC0  
  URL: https://kenney.nl/assets/platformer-art-candy