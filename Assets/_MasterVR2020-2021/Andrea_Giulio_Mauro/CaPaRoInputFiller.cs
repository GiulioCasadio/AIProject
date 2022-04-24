using BehaviorDesigner.Runtime;
using TuesdayNights;
using UnityEngine;

using Ca_Pa_Ro.CaPaRo_SharedVariables;

namespace Ca_Pa_Ro
{
    public class CaPaRoInputFiller : tnStandardAIInputFillerBase
    {
        private BehaviorTree m_behavior_tree = null;

        private AIRole m_Role = AIRole.Null;

        public AIInputData shared = new AIInputData();
        public AIOutputData output = new AIOutputData();

        // tnInputFiller's INTERFACE
        public CaPaRoInputFiller(GameObject i_Self, AIRole i_Role) : base(i_Self)
        {

            m_Role = i_Role;

            m_behavior_tree = i_Self.AddComponent<BehaviorTree>();
            m_behavior_tree.StartWhenEnabled = false;
            m_behavior_tree.ExternalBehavior = Resources.Load<BehaviorDesigner.Runtime.ExternalBehavior>("BT_CAPARO_Midfielder");

            m_behavior_tree.EnableBehavior();
        }

        public override void Setup(tnBaseAIData i_Data)
        {
            base.Setup(i_Data);


            //SETUP STATIC GLOBAL DATA
            shared.ball = ball;
            shared.ballPosition = ballPosition;
            shared.myPosition = myPosition;
            shared.ballRadius = ballRadius;
            
            shared.teamCharactersCount = teamCharactersCount;
            shared.teammatesCount = teammatesCount;
            shared.opponentsCount = opponentsCount;
            shared.myGoal = myGoal;
            shared.opponentGoal = opponentGoal;
            
            shared.fieldWidth = fieldWidth;
            shared.fieldHeight = fieldHeight;
            
            shared.gkAreaMinHeight = gkAreaMinHeight;
            shared.gkAreaMaxHeight = gkAreaMaxHeight;
            shared.gkAreaWidth = gkAreaWidth;
            shared.gkAreaHeight = gkAreaHeight;
            
            shared.goalWidth = goalWidth;
            shared.goalMaxHeight = goalMaxHeight;
            shared.goalMinHeight = goalMinHeight;
            
            shared.colliderRadius = colliderRadius;
            
            shared.m_Opponents = opponents;
            shared.m_Teams = teams;

            m_behavior_tree.SetVariableValue("Shared", shared);
        }

        public override void Fill(float i_FrameTime, tnInputData i_Data)
        {
            if (!initialized || self == null)
            {
                ResetInputData(i_Data);
                return;
            }

            if (m_Role == AIRole.Null)
            {
                ResetInputData(i_Data);
                return;
            }

            UpdateSharedInputData(i_FrameTime);

            // Fill input data.
            if (m_behavior_tree.GetVariable("Output").GetValue() is AIOutputData output)
            {
                var m_Axes = output.axes;

                i_Data.SetAxis(InputActions.s_HorizontalAxis, m_Axes.x);
                i_Data.SetAxis(InputActions.s_VerticalAxis, m_Axes.y);

                i_Data.SetButton(InputActions.s_PassButton, output.requestKick);
                i_Data.SetButton(InputActions.s_ShotButton, output.requestKick);

                i_Data.SetButton(InputActions.s_AttractButton, output.isAttracting);
            }
            else
            {
                Debug.LogError("No output set!");
            }

        }

        public override void Clear()
        {
            m_behavior_tree.SetVariableValue("Output", new AIOutputData());
        }

        private void UpdateSharedInputData(float i_FrameTime)
        {
            shared.myPosition = myPosition;
           
            
            shared.ballDistance = ballDistance;
            
            shared.topLeft = topLeft;
            shared.topRight = topRight;
            shared.bottomLeft = bottomLeft;
            shared.bottomRight = bottomRight;
            
            shared.ballPosition = ball != null ? ball.position : Vector3.zero;
            
            shared.midfield = midfield;

            m_behavior_tree.SetVariableValue("Shared", shared);
        }
    }
}