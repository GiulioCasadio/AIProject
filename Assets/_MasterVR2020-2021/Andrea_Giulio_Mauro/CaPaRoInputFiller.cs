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

        // STATIC VARIABLES
        private static string s_Params = "Data/AI/AIParams";

        // tnInputFiller's INTERFACE
        public CaPaRoInputFiller(GameObject i_Self, AIRole i_Role) : base(i_Self)
        {

            m_Role = i_Role;

            m_behavior_tree = i_Self.AddComponent<BehaviorTree>();
            m_behavior_tree.StartWhenEnabled = false;
            m_behavior_tree.ExternalBehavior = Resources.Load<BehaviorDesigner.Runtime.ExternalBehavior>("BT_CAPARO_Midfielder");

            //m_behavior_tree.SetVariableValue("CharacterRole", m_Role);
            //  m_behavior_tree.SetVariableValue("Self", self);

            //tnStandardAIInputFillerParams aiParams = Resources.Load<tnStandardAIInputFillerParams>(s_Params);

            /*if (aiParams == null)
            {
                Debug.LogWarning("AI Params is null");
                return;
            }*/

            m_behavior_tree.EnableBehavior();
        }

        public override void Setup(tnBaseAIData i_Data)
        {
            base.Setup(i_Data);


            //SETUP STATIC GLOBAL DATA
            shared.ball = ball;
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
        }

        public override void Clear()
        {
        }
    }
}