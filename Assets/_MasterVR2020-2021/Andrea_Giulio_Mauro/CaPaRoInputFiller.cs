using System;
using BehaviorDesigner.Runtime;
using TuesdayNights;
using UnityEngine;

using Ca_Pa_Ro.CaPaRo_SharedVariables;
using Ca_Pa_Ro.Player;
using Coach;

namespace Ca_Pa_Ro
{
    public class CaPaRoInputFiller : tnStandardAIInputFillerBase
    {
        protected internal BehaviorTree m_behavior_tree = null;

        private AIRole m_Role = AIRole.Null;

        public AIInputData shared = new AIInputData();
        public AIOutputData output = new AIOutputData();

        private bool PrevDash = false;
        private bool PrevKick = false;

        // tnInputFiller's INTERFACE
        public CaPaRoInputFiller(GameObject i_Self, AIRole i_Role) : base(i_Self)
        {

            m_Role = i_Role;

            m_behavior_tree = i_Self.AddComponent<BehaviorTree>();
            m_behavior_tree.StartWhenEnabled = false;

            switch (i_Role)
            {
                case AIRole.Null:
                    break;
                case AIRole.Defender:
                    break;
                case AIRole.Midfielder:
                    m_behavior_tree.ExternalBehavior = Resources.Load<BehaviorDesigner.Runtime.ExternalBehavior>("BT_CAPARO_Midfielder");
                    break;
                case AIRole.Striker:
                    m_behavior_tree.ExternalBehavior = Resources.Load<BehaviorDesigner.Runtime.ExternalBehavior>("AndreaPlayerTest");
                    break;
                case AIRole.CoachPlayer:
                    m_behavior_tree.ExternalBehavior = Resources.Load<BehaviorDesigner.Runtime.ExternalBehavior>("BT_CAPARO_CoachPlayer");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(i_Role), i_Role, null);
            }
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

            shared.ballRadiusNearTreshold = ballRadius * 3;
            
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

            foreach (var friend in teams)
            {
                if (friend.position.Equals(myPosition))
                {
                    shared.myTransform = friend;
                    break;
                }
            }

            shared.m_tnBaseMatchController = i_Data.BaseMatchController;
            
            m_behavior_tree.SetVariableValue("Shared", shared);
            
            SharedCoachVariables sharedCoachVariables = (SharedCoachVariables)m_behavior_tree.GetVariable("m_coachVariables");

            if (sharedCoachVariables != null)
            {
                
                sharedCoachVariables.Value.initializeFieldZoneCache(fieldWidth, fieldHeight, Mathf.Sign(myGoal.position.x));
            }
            
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

                if (PrevKick)
                {
                    i_Data.SetButton(InputActions.s_PassButton, false);
                    PrevKick = false;
                }
                else
                {
                    i_Data.SetButton(InputActions.s_PassButton, output.requestKick);
                    PrevKick = true;
                }

                if (PrevDash)
                {
                    i_Data.SetButton(InputActions.s_ShotButton, false); // shot button eseguo uno scatto
                    PrevDash = false;
                }
                else
                {
                    i_Data.SetButton(InputActions.s_ShotButton, output.requestDash); // shot button eseguo uno scatto
                    PrevDash = true;
                }
                i_Data.SetButton(InputActions.s_AttractButton, output.requestAttracting);

                // reset output
                output.axes = new Vector2(0, 0);
                output.requestKick = false;
                output.requestDash = false;
                output.requestAttracting = false;

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

        public PlayerFocus GetPlayerFocus()
        {
            SharedPlayerFocus sharedPlayerfocus = (SharedPlayerFocus)m_behavior_tree.GetVariable("m_playerFocus");

            if (sharedPlayerfocus != null)
            {
                return sharedPlayerfocus.Value;
            }

            return null;
        }
    }
}