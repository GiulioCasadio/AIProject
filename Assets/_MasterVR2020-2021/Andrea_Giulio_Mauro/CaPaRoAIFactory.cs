using System.Collections.Generic;
using UnityEngine;
using TuesdayNights;

namespace Ca_Pa_Ro
{
    public class CaPaRoAIFactory : tnBaseStandardMatchAIFactory
    {
        #region Roles

        private static AIRole[] s_Roles_1 = new AIRole[]
        {
            AIRole.Midfielder
        };
        
        private static AIRole[] s_Roles_2 = new AIRole[]
        {
            AIRole.Midfielder,
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
            return CreateInputFiller(role, i_Character);
        }
        
        // INTERNALS

        private static tnStandardAIInputFillerBase CreateInputFiller(AIRole i_Role, GameObject i_Character)
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
