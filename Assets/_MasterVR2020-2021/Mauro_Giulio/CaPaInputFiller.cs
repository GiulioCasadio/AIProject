using BehaviorDesigner.Runtime;
using TuesdayNights;
using UnityEngine;

namespace Ca_Pa
{
    public class CaPaInputFiller : tnStandardAIInputFillerBase
    {
        private BehaviorTree m_behavior_tree = null;

        private AIRole m_Role = AIRole.Null;

        // STATIC VARIABLES
        private static string s_Params = "Data/AI/AIParams";

        // tnInputFiller's INTERFACE
        public CaPaInputFiller(GameObject i_Self, AIRole i_Role) : base(i_Self)
        {

            m_Role = i_Role;

            m_behavior_tree = i_Self.AddComponent<BehaviorTree>();
            m_behavior_tree.StartWhenEnabled = false;
            m_behavior_tree.ExternalBehavior = Resources.Load<BehaviorDesigner.Runtime.ExternalBehavior>("BT_CAPA_Midfielder");

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