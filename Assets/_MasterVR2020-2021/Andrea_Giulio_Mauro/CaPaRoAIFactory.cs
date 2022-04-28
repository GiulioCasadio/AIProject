using System.Collections.Generic;
using Coach;
using UnityEngine;
using TuesdayNights;

namespace Ca_Pa_Ro
{
    public class CaPaRoAIFactory : tnBaseStandardMatchAIFactory
    {

        private CaPaRoInputFiller m_coachInputFiller;
        private List<CaPaRoInputFiller> m_otherPlayers = new List<CaPaRoInputFiller>(); 
        
        
        #region Roles

        private static AIRole[] s_Roles_1 = new AIRole[]
        {
            AIRole.CoachPlayer
        };
        
        private static AIRole[] s_Roles_2 = new AIRole[]
        {
            AIRole.CoachPlayer,
            AIRole.Midfielder
        };
        
        private static AIRole[] s_Roles_3 = new AIRole[]
        {
            AIRole.CoachPlayer,
            AIRole.Striker,
            AIRole.Striker
        };

        private static AIRole[][] s_Roles = new AIRole[][]
        {
            s_Roles_1,
            s_Roles_2,
            s_Roles_3
        };
        
        #endregion
        
        private static AIRole s_DefaultRole = AIRole.Striker;
        private List<AIRole> m_Roles = null;
        protected int m_AICreated = 0;

        #region Overrides

        protected override void OnConfigure(tnTeamDescription i_TeamDescription)
        {
            if (i_TeamDescription == null)
                return;

            var charactersCount = i_TeamDescription.charactersCount;
            
            if (charactersCount <= 0 || charactersCount > s_Roles.Length)
                return;

            var roles = s_Roles[charactersCount - 1];

            if (roles == null || roles.Length == 0 || roles.Length != charactersCount)
                return;

            var aiIndex = 0;

            for (var characterIndex = 0; characterIndex < charactersCount; ++characterIndex)
            {
                var characterDescription = i_TeamDescription.GetCharacterDescription(characterIndex);

                if (characterDescription == null)
                    continue;

                var playerId = characterDescription.playerId;
                var playerData = tnGameData.GetPlayerDataMain(playerId);

                if (playerData != null) continue;
                var role = roles[aiIndex++];
                m_Roles.Add(role);
            }

            m_Roles.Sort();
        }
        
        protected override tnStandardAIInputFillerBase OnCreateAI(int i_Index, GameObject i_Character)
        {
            if (m_Roles.Count == 0 || m_AICreated >= m_Roles.Count)
            {
                return CreateInputFiller(s_DefaultRole, i_Character);
            }

            var role = m_Roles[m_AICreated++];

            CaPaRoInputFiller currentPlayer = CreateInputFiller(role, i_Character);
            
            if (role == AIRole.CoachPlayer)
            {
                m_coachInputFiller = currentPlayer;
            }

            else
            {
                m_otherPlayers.Add(currentPlayer);
            }

            if (m_AICreated == m_Roles.Count)
            {
                fillCoachData();
            }
            
            return currentPlayer;
        }

        private void fillCoachData()
        {
            CoachVariables.SharedCoachVariables sharedCoachVariables = (CoachVariables.SharedCoachVariables)m_coachInputFiller.m_behavior_tree.GetVariable("m_coachVariables");
            
            sharedCoachVariables.Value.playersCommunications.Add(new CoachPlayerCommunication(m_coachInputFiller.GetPlayerFocus()));

            foreach (CaPaRoInputFiller player in m_otherPlayers)
            {
                sharedCoachVariables.Value.playersCommunications.Add(
                    new CoachPlayerCommunication(player.GetPlayerFocus()));
            }
        }

        // INTERNALS

        private static CaPaRoInputFiller CreateInputFiller(AIRole i_Role, GameObject i_Character)
        {
            return new CaPaRoInputFiller(i_Character, i_Role);
        }

        #endregion

        public CaPaRoAIFactory()
            : base()
        {
            m_Roles = new List<AIRole>();
        }
    }
}
