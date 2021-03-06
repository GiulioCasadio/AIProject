using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using GoUI;

using FullInspector;

using BaseMatchEvents;

using TrueSync;

using TypeReferences;

[fiInspectorOnly]
public class tnSubbuteoMatchController : tnBaseMatchController
{
    // STATIC

    private static string s_ScorePanelSpawnPoint = "Spawn_Score";

    // Serializable fields

    [InspectorHeader("Game")]

    [SerializeField]
    [InspectorCategory("GAME")]
    private float m_WaitBetweenFinalWhistleAndResults = 1.5f;
    [SerializeField]
    [InspectorCategory("GAME")]
    private float m_WaitAfterResults = 1.5f;

    [InspectorHeader("UI")]

    [SerializeField]
    [InspectorCategory("UI")]
    private tnPanel_SubbuteoMatchInstructions m_InstructionsPanelPrefab = null;
    [SerializeField]
    [InspectorCategory("UI")]
    private tnPanel_Celebration m_CelebrationPanelPrefab = null;
    [SerializeField]
    [InspectorCategory("UI")]
    private tnPanel_FlatBackground m_FlatBackgroundPanelPrefab = null;
    [SerializeField]
    [InspectorCategory("UI")]
    private tnPanel_SubbuteoMatchResults m_SubbuteoMatchResultsPanelPrefab = null;

    [InspectorHeader("Animations")]

    [SerializeField]
    [InspectorCategory("ANIMATIONS")]
    private float m_CelebrationShakeTime = 0f;
    [SerializeField]
    [InspectorCategory("ANIMATIONS")]
    private float m_CelebrationShakeAmount = 0f;

    [InspectorHeader("Effects")]

    [SerializeField]
    [InspectorCategory("EFFECTS")]
    private Effect m_KickOffEffect = null;

    [SerializeField]
    [InspectorCategory("EFFECTS")]
    private SfxDescriptor m_KickOffSfx = null;
    [SerializeField]
    [InspectorCategory("EFFECTS")]
    private SfxDescriptor m_GoalSfx = null;
    [SerializeField]
    [InspectorCategory("EFFECTS")]
    private SfxDescriptor m_GoldenGoalSfx = null;
    [SerializeField]
    [InspectorCategory("EFFECTS")]
    private SfxDescriptor m_FinalWhistleSfx = null;

    // Fields

    private tnPanel_SubbuteoMatchInstructions m_InstructionsPanel = null;
    private tnPanel_Celebration m_CelebrationPanel = null;
    private tnPanel_FlatBackground m_FlatBackgroundPanel = null;
    private tnPanel_SubbuteoMatchResults m_SubbuteoMatchResultsPanel = null;

    private tnSubbuteoMatchAIFactoryComponent m_AIFactoryComponent = null;

    private List<int> m_GoalEffectsTicks = new List<int>();
    private List<int> m_KickOffEffectsTicks = new List<int>();
    private List<int> m_GoldenGoalEffectsTicks = new List<int>();

    // MonoBehaviour's interface

    protected override void Awake()
    {
        base.Awake();

        // Get AI Factory component.

        m_AIFactoryComponent = GetComponent<tnSubbuteoMatchAIFactoryComponent>();

        // UI

        if (m_InstructionsPanelPrefab != null)
        {
            tnPanel_SubbuteoMatchInstructions instructionsPanelInstance = Instantiate<tnPanel_SubbuteoMatchInstructions>(m_InstructionsPanelPrefab);
            instructionsPanelInstance.transform.SetParent(transform);

            m_InstructionsPanel = instructionsPanelInstance;
        }

        if (m_CelebrationPanelPrefab != null)
        {
            tnPanel_Celebration celebrationPanelInstance = Instantiate<tnPanel_Celebration>(m_CelebrationPanelPrefab);
            celebrationPanelInstance.transform.SetParent(transform);

            m_CelebrationPanel = celebrationPanelInstance;
        }

        if (m_FlatBackgroundPanelPrefab != null)
        {
            tnPanel_FlatBackground flatBackgroundPanelInstance = Instantiate<tnPanel_FlatBackground>(m_FlatBackgroundPanelPrefab);
            flatBackgroundPanelInstance.transform.SetParent(transform);

            m_FlatBackgroundPanel = flatBackgroundPanelInstance;
        }

        if (m_SubbuteoMatchResultsPanelPrefab != null)
        {
            tnPanel_SubbuteoMatchResults subbuteoMatchResultsPanelInstance = Instantiate<tnPanel_SubbuteoMatchResults>(m_SubbuteoMatchResultsPanelPrefab);
            subbuteoMatchResultsPanelInstance.transform.SetParent(transform);

            m_SubbuteoMatchResultsPanel = subbuteoMatchResultsPanelInstance;
        }
    }

    protected override void Start()
    {
        base.Start();

        UI.OpenPanel(m_CelebrationPanel);
    }

    protected override void OnMatchBecomeInvalid()
    {
        // Close HUD.

        UI.ClosePanel(m_CelebrationPanel);
    }

    // tnMatchController's interface

    protected override void OnPreInit()
    {
        base.OnPreInit();

        tnTeamsModule teamsModule = GameModulesManager.GetModuleMain<tnTeamsModule>();

        if (teamsModule == null || teamsModule.teamsCount == 0)
            return;

        if (m_AIFactoryComponent != null)
        {
            m_AIFactoryComponent.Setup(teamsModule.teamsCount);
        }
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        SpawnScorePanel();
    }

    protected override void OnCreateTeam(int i_TeamIndex, tnTeamDescription i_TeamDescription)
    {
        base.OnCreateTeam(i_TeamIndex, i_TeamDescription);

        if (m_AIFactoryComponent != null)
        {
            m_AIFactoryComponent.CreateTeamAIFactory(i_TeamIndex, i_TeamDescription);
        }
    }

    protected override tnCharacterResults CreateCharacterResults(int i_Id)
    {
        return new tnSubbuteoMatchCharacterResults(i_Id);
    }

    protected override tnTeamResults CreateTeamResults(int i_Id)
    {
        return new tnSubbuteoMatchTeamResults(i_Id);
    }

    protected override Comparison<tnTeamResults> GetSortDelegate()
    {
        return SortFunctor;
    }

    protected override void OnSetupMatch()
    {
        base.OnSetupMatch();

        Action callback = () => { RequestStartGame(); };
        StartCoroutine(ShowInstructions(callback));
    }

    protected override void OnStartMatch()
    {
        base.OnStartMatch();

        bool online = !PhotonNetwork.offlineMode;
        tnStartMatchEventParams param = new tnStartMatchEventParams(online);

        param.SetGameModeId(gameModeId);
        param.SetBallId(ballId);
        param.SetStadiumId(stadiumId);
        param.SetHasReferee(hasReferee);
        param.SetGoldenGoalEnabled(goldenGoalEnabled);

        // Compute human players.

        tnSubbuteoMatchTeamResults teamAResults = (tnSubbuteoMatchTeamResults)GetTeamResultsByIndex(0);
        if (teamAResults != null)
        {
            param.SetTeamAId(teamAResults.id);
            param.SetTeamASize(teamAResults.charactersResultsCount);

            int humanPlayers = 0;

            if (!online)
            {
                for (int index = 0; index < teamAResults.charactersResultsCount; ++index)
                {
                    tnCharacterResults characterResults = teamAResults.GetCharacterResults(index);
                    if (characterResults != null)
                    {
                        if (characterResults.isHuman)
                        {
                            ++humanPlayers;
                        }
                    }
                }
            }
            else
            {
                humanPlayers = teamAResults.charactersResultsCount;
            }

            param.SetTeamAHumanPlayers(humanPlayers);
        }

        tnSubbuteoMatchTeamResults teamBResults = (tnSubbuteoMatchTeamResults)GetTeamResultsByIndex(1);
        if (teamBResults != null)
        {
            param.SetTeamBId(teamBResults.id);
            param.SetTeamBSize(teamBResults.charactersResultsCount);

            int humanPlayers = 0;

            if (!online)
            {
                for (int index = 0; index < teamBResults.charactersResultsCount; ++index)
                {
                    tnCharacterResults characterResults = teamBResults.GetCharacterResults(index);
                    if (characterResults != null)
                    {
                        if (characterResults.isHuman)
                        {
                            ++humanPlayers;
                        }
                    }
                }
            }
            else
            {
                humanPlayers = teamBResults.charactersResultsCount;
            }

            param.SetTeamBHumanPlayers(humanPlayers);
        }

        // Compute local characters and teams.

        for (int index = 0; index < localCharactersCount; ++index)
        {
            GameObject character = GetLocalCharacterByIndex(index);

            if (character == null)
                continue;

            tnCharacterInfo characterInfo = character.GetComponent<tnCharacterInfo>();
            if (characterInfo != null)
            {
                int characterId = characterInfo.characterId;
                int teamId = characterInfo.teamId;

                param.AddLocalCharacter(characterId);
                param.AddLocalTeam(teamId);
            }
        }

        // Raise event.

        Messenger.Broadcast<tnStartMatchEventParams>("MatchStarted", param);
    }

    protected override void OnEndMatch()
    {
        base.OnEndMatch();

        bool online = !PhotonNetwork.offlineMode;
        tnEndMatchEventParams param = new tnEndMatchEventParams(online);

        // Compute human players.

        param.SetGameModeId(gameModeId);
        param.SetBallId(ballId);
        param.SetStadiumId(stadiumId);
        param.SetHasReferee(hasReferee);
        param.SetGoldenGoalEnabled(goldenGoalEnabled);
        param.SetGoldenGoal(goldenGoal);

        tnSubbuteoMatchTeamResults teamAResults = (tnSubbuteoMatchTeamResults)GetTeamResultsByIndex(0);
        if (teamAResults != null)
        {
            param.SetTeamAId(teamAResults.id);
            param.SetScoreA(teamAResults.score);
            param.SetTeamASize(teamAResults.charactersResultsCount);

            int humanPlayers = 0;

            if (!online)
            {
                for (int index = 0; index < teamAResults.charactersResultsCount; ++index)
                {
                    tnCharacterResults characterResults = teamAResults.GetCharacterResults(index);
                    if (characterResults != null)
                    {
                        if (characterResults.isHuman)
                        {
                            ++humanPlayers;
                        }
                    }
                }
            }
            else
            {
                humanPlayers = teamAResults.charactersResultsCount;
            }

            param.SetTeamAHumanPlayers(humanPlayers);
        }

        tnSubbuteoMatchTeamResults teamBResults = (tnSubbuteoMatchTeamResults)GetTeamResultsByIndex(1);
        if (teamBResults != null)
        {
            param.SetTeamBId(teamBResults.id);
            param.SetScoreB(teamBResults.score);
            param.SetTeamBSize(teamBResults.charactersResultsCount);

            int humanPlayers = 0;

            if (!online)
            {
                for (int index = 0; index < teamBResults.charactersResultsCount; ++index)
                {
                    tnCharacterResults characterResults = teamBResults.GetCharacterResults(index);
                    if (characterResults != null)
                    {
                        if (characterResults.isHuman)
                        {
                            ++humanPlayers;
                        }
                    }
                }
            }
            else
            {
                humanPlayers = teamBResults.charactersResultsCount;
            }

            param.SetTeamBHumanPlayers(humanPlayers);
        }

        // Compute local characters and teams.

        for (int index = 0; index < localCharactersCount; ++index)
        {
            GameObject character = GetLocalCharacterByIndex(index);

            if (character == null)
                continue;

            tnCharacterInfo characterInfo = character.GetComponent<tnCharacterInfo>();
            if (characterInfo != null)
            {
                int characterId = characterInfo.characterId;
                int teamId = characterInfo.teamId;

                param.AddLocalCharacter(characterId);
                param.AddLocalTeam(teamId);
            }
        }

        // Raise event.

        Messenger.Broadcast<tnEndMatchEventParams>("MatchEnded", param);
    }

    // tnBaseMatchController's interface

    protected override bool Draw()
    {
        bool retValue = true;

        for (int teamIndex = 0; teamIndex < teamsCount - 1; ++teamIndex)
        {
            tnSubbuteoMatchTeamResults resultsA = (tnSubbuteoMatchTeamResults)GetTeamResultsByIndex(teamIndex);
            tnSubbuteoMatchTeamResults resultsB = (tnSubbuteoMatchTeamResults)GetTeamResultsByIndex(teamIndex + 1);

            if (resultsA == null)
                continue;

            if (resultsB == null)
                continue;

            bool draw = (resultsA.score == resultsB.score);
            retValue &= draw;
        }

        return retValue;
    }

    protected override void OnKickOff()
    {
        base.OnKickOff();

        int currentTick = TrueSyncManager.ticksMain;

        if (!m_KickOffEffectsTicks.Contains(currentTick))
        {
            EffectUtils.PlayEffect(m_KickOffEffect, midfieldPosition, Quaternion.identity);
            SfxPlayer.PlayMain(m_KickOffSfx);

            m_KickOffEffectsTicks.Add(currentTick);
        }
    }

    protected override void OnGoldenGoalStart()
    {
        base.OnGoldenGoalStart();

        int currentTick = TrueSyncManager.ticksMain;

        if (!m_GoldenGoalEffectsTicks.Contains(currentTick))
        {
            SfxPlayer.PlayMain(m_GoldenGoalSfx);
            m_GoldenGoalEffectsTicks.Add(currentTick);
        }
    }

    protected override void OnGoal(tnGoalEventParams i_Params)
    {
        base.OnGoal(i_Params);

        int currentTick = TrueSyncManager.ticksMain;

        if (!m_GoalEffectsTicks.Contains(currentTick))
        {
            // Play effects.

            SfxPlayer.PlayMain(m_GoalSfx);

            // Play celebration animation and screen shake.

            tnScreenShake screenShake = null;
            tnGameCamera gameCamera = cameraGo.GetComponent<tnGameCamera>();
            if (gameCamera != null)
            {
                screenShake = gameCamera.GetComponentInChildren<tnScreenShake>();
            }

            if (m_CelebrationPanel != null)
            {
                m_CelebrationPanel.SetCelebrationText(i_Params.scorerId);
                m_CelebrationPanel.StartCelebration();
            }

            if (screenShake != null)
            {
                screenShake.ForceShake(m_CelebrationShakeTime, m_CelebrationShakeAmount, ShakeMode.Interruput);
            }

            m_GoalEffectsTicks.Add(currentTick);
        }

        // Update results.

        for (int teamResultsIndex = 0; teamResultsIndex < teamsCount; ++teamResultsIndex)
        {
            tnSubbuteoMatchTeamResults teamResults = (tnSubbuteoMatchTeamResults)GetTeamResultsByIndex(teamResultsIndex);
            if (teamResults != null)
            {
                if (teamResults.id != i_Params.teamId) // Assign a score to the team.
                {
                    ++teamResults.score;
                }
            }
        }

        if (i_Params.hasValidScorer)
        {
            if (!i_Params.isOwnGoal)
            {
                tnSubbuteoMatchCharacterResults characterResults = (tnSubbuteoMatchCharacterResults)GetCharacterResultsById(i_Params.scorerId);
                if (characterResults != null)
                {
                    ++characterResults.goalScored;
                }
            }
        }
    }

    protected override tnBaseAIInputFiller CreateBaseAIInputFiller(int i_TeamIndex, int i_Index, GameObject i_Character)
    {
        if (m_AIFactoryComponent != null)
        {
            return m_AIFactoryComponent.CreateAIInputFiller(i_TeamIndex, i_Index, i_Character);
        }

        return new tnNullBaseAIInputFiller(i_Character); // Invalid AI Factory, create null input filler.
    }

    protected override IEnumerator EndMatchSequence()
    {
        // Final wistle.

        SfxPlayer.PlayMain(m_FinalWhistleSfx);

        // Wait.

        yield return new WaitForSeconds(m_WaitBetweenFinalWhistleAndResults);

        // Close HUD.

        UI.ClosePanel(m_CelebrationPanel);

        // Show results.

        UI.OpenPanel(m_FlatBackgroundPanel);

        if (m_SubbuteoMatchResultsPanel != null)
        {
            m_SubbuteoMatchResultsPanel.Config(this);
        }

        UI.OpenPanel(m_SubbuteoMatchResultsPanel);

        // Wait.

        yield return new WaitForSeconds(m_WaitAfterResults);
    }

    // INTERNALS

    private void SpawnScorePanel()
    {
        tnMatchSettingsModule matchSettingsModule = GameModulesManager.GetModuleMain<tnMatchSettingsModule>();

        if (matchSettingsModule == null)
            return;

        GameObject scorePanelSpawnPointGo = GameObject.Find(s_ScorePanelSpawnPoint);

        if (scorePanelSpawnPointGo == null)
            return;

        int stadiumId = matchSettingsModule.stadiumId;
        tnStadiumData stadiumData = tnGameData.GetStadiumDataMain(stadiumId);

        if (stadiumData == null)
            return;

        int gameModeId = matchSettingsModule.gameModeId;

        GameObject scorePanelPrefab = stadiumData.LoadAndGetScorePanelPrefab(gameModeId);

        if (scorePanelPrefab == null)
            return;

        Vector3 spawnPosition = scorePanelSpawnPointGo.transform.position;
        Quaternion spawnRotation = scorePanelSpawnPointGo.transform.rotation;

        GameObject scorePanelInstance = Instantiate<GameObject>(scorePanelPrefab);
        scorePanelInstance.name = "ScorePanel";

        scorePanelInstance.transform.position = spawnPosition;
        scorePanelInstance.transform.rotation = spawnRotation;

        tnUISubbuteoScorePanel scorePanel = scorePanelInstance.GetComponentInChildren<tnUISubbuteoScorePanel>();
        if (scorePanel != null)
        {
            scorePanel.Bind(this);
        }
    }

    private IEnumerator ShowInstructions(Action i_Callback = null)
    {
        bool online = !PhotonNetwork.offlineMode;
        if (!online)
        {
            if (m_ShowTutorial)
            {
                if (m_InstructionsPanel != null)
                {
                    bool proceed = false;
                    Action callback = () => { proceed = true; };

                    m_InstructionsPanel.onProceedEvent += callback;

                    UI.OpenPanel(m_InstructionsPanel);

                    yield return new WaitUntil(() => (proceed == true));

                    UI.ClosePanel(m_InstructionsPanel);

                    m_InstructionsPanel.onProceedEvent -= callback;
                }
            }
        }

        if (i_Callback != null)
        {
            i_Callback();
        }
    }

    // FUNCTORS

    private static int SortFunctor(tnTeamResults i_Res1, tnTeamResults i_Res2)
    {
        tnSubbuteoMatchTeamResults results1 = (tnSubbuteoMatchTeamResults)i_Res1;
        tnSubbuteoMatchTeamResults results2 = (tnSubbuteoMatchTeamResults)i_Res2;

        if (results1.score > results2.score) return -1;
        if (results1.score < results2.score) return 1;

        return 0;
    }
}