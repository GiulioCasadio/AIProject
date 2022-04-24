using System;
using BehaviorDesigner.Runtime;
using TuesdayNights;
using UnityEngine;

namespace Ca_Pa_Ro
{
    public class CaPaRoInputFiller : tnStandardAIInputFillerBase
    {
        private BehaviorTree m_behavior_tree = null;

        private AIRole m_Role = AIRole.Null;

        // STATIC VARIABLES
        private static string s_Params = "Data/AI/AIParams";

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

            PlayerFocus.SharedPlayerFocus ss = (PlayerFocus.SharedPlayerFocus)m_behavior_tree.GetVariable("m_playerFocus");
            
            if (ss != null)
            {
                Debug.Log(ss.Value.m_state);
            }
            

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