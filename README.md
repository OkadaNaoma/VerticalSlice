# GDIM33 Vertical Slice
## Milestone 1 Devlog
## Visual Scripting Graph Explanation
For this build, I used a Visual Scripting State Machine called `VSFeedbackStateMachine`. This graph has two states: `Waiting` and `DecisionFeedback`. The game starts in the `Waiting` state while the player is checking the traveler’s documents and luggage. When the player clicks Approve or Reject, my C# script sends a custom event called `DecisionMade` to the Visual Scripting graph. This event triggers the transition from `Waiting` to `DecisionFeedback`. I also added a Debug Log in the `DecisionFeedback` state to confirm that the state machine is working. The main gameplay is controlled by C#, but this graph handles the state change after the player makes a decision.

## Updated Break-down
![Updated break-down](DevlogFiles/GDIM33_VerticalSlice_Break-down_Milestone1.png)

I updated my break-down by adding the state machine system. In the previous version, the break-down mainly showed the documents, luggage, buttons, timer, score, and feedback UI. In the updated version, I added `VSFeedbackStateMachine` and connected it to the decision buttons and the feedback system. The Approve and Reject buttons call the main case manager script, and then the script sends the `DecisionMade` event to the state machine.

The state machine starts in the `Waiting` state while the player is checking the case. After the player chooses Approve or Reject, it moves to the `DecisionFeedback` state. This state is related to the feedback UI because the game shows whether the decision was correct or not after the decision is made. It is also related to the score system because the score changes at the same time. For Milestone 1, this state machine is simple, but it helps organize the game flow and can be expanded later with more states, such as Tutorial, Inspecting, Result, and NextCase.


## Milestone 2 Devlog
Milestone 2 Devlog goes here.
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