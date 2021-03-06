// Copyright (c) 2015 Augie R. Maddox, Guavaman Enterprises. All rights reserved.
#pragma warning disable 0219
#pragma warning disable 0618
#pragma warning disable 0649

namespace Rewired.Data {

    using UnityEngine;
    using System.Collections.Generic;
    using Rewired;

    /// <summary>
    /// Class for saving data to PlayerPrefs. Add this as a component to your Rewired Input Manager to save and load data automatically to PlayerPrefs.
    /// Copy this class and customize it to your needs to create a new custom data storage system.
    /// </summary>
    public class UserDataStore_PlayerPrefs : UserDataStore {

        private const string thisScriptName = "UserDataStore_PlayerPrefs";
        private const string editorLoadedMessage = "\nIf unexpected input issues occur, the loaded XML data may be outdated or invalid. Clear PlayerPrefs using the inspector option on the UserDataStore_PlayerPrefs component.";

        [SerializeField]
        private bool isEnabled = true;

        [SerializeField]
        private bool loadDataOnStart = true;

        [SerializeField]
        private string playerPrefsKeyPrefix = "RewiredSaveData";

        #region UserDataStore Implementation

        // Public Methods

        /// <summary>
        /// Save all data now.
        /// </summary>
        public override void Save() {
            if(!isEnabled) {
                Debug.LogWarning(thisScriptName + " is disabled and will not save any data.", this);
                return;
            }
            SaveAll();

#if UNITY_EDITOR
            Debug.Log("Rewired: " + thisScriptName + " saved all user data to XML.");
#endif
        }

        /// <summary>
        /// Save all data for a specific controller for a Player.
        /// </summary>
        /// <param name="playerId">Player id</param>
        /// <param name="controllerType">Controller type</param>
        /// <param name="controllerId">Controller id</param>
        public override void SaveControllerData(int playerId, ControllerType controllerType, int controllerId) {
            if(!isEnabled) {
                Debug.LogWarning(thisScriptName + " is disabled and will not save any data.", this);
                return;
            }
            SaveControllerDataNow(playerId, controllerType, controllerId);

#if UNITY_EDITOR
            Debug.Log("Rewired: " + thisScriptName + " saved " + controllerType + " " + controllerId + " data for Player " + playerId + " to XML.");
#endif
        }

        /// <summary>
        /// Save all data for a specific controller. Does not save Player data.
        /// </summary>
        /// <param name="controllerType">Controller type</param>
        /// <param name="controllerId">Controller id</param>
        public override void SaveControllerData(ControllerType controllerType, int controllerId) {
            if(!isEnabled) {
                Debug.LogWarning(thisScriptName + " is disabled and will not save any data.", this);
                return;
            }
            SaveControllerDataNow(controllerType, controllerId);

#if UNITY_EDITOR
            Debug.Log("Rewired: " + thisScriptName + " saved " + controllerType + " " + controllerId + " data to XML.");
#endif
        }

        /// <summary>
        /// Save all data for a specific Player.
        /// </summary>
        /// <param name="playerId">Player id</param>
        public override void SavePlayerData(int playerId) {
            if(!isEnabled) {
                Debug.LogWarning(thisScriptName + " is disabled and will not save any data.", this);
                return;
            }
            SavePlayerDataNow(playerId);

#if UNITY_EDITOR
            Debug.Log("Rewired: " + thisScriptName + " saved all user data for Player " + playerId + " to XML.");
#endif
        }

        /// <summary>
        /// Save all data for a specific InputBehavior for a Player.
        /// </summary>
        /// <param name="playerId">Player id</param>
        /// <param name="behaviorId">Input Behavior id</param>
        public override void SaveInputBehavior(int playerId, int behaviorId) {
            if(!isEnabled) {
                Debug.LogWarning(thisScriptName + " is disabled and will not save any data.", this);
                return;
            }
            SaveInputBehaviorNow(playerId, behaviorId);

#if UNITY_EDITOR
            Debug.Log("Rewired: " + thisScriptName + " saved Input Behavior data for Player " + playerId + " to XML.");
#endif
        }

        /// <summary>
        /// Load all data now.
        /// </summary>
        public override void Load() {
            if(!isEnabled) {
                Debug.LogWarning(thisScriptName + " is disabled and will not load any data.", this);
                return;
            }
            int count = LoadAll();

#if UNITY_EDITOR
            if(count > 0) Debug.Log("Rewired: " + thisScriptName + " loaded all user data from XML. " + editorLoadedMessage);
#endif
        }

        /// <summary>
        /// Load all data for a specific controller for a Player.
        /// </summary>
        /// <param name="playerId">Player id</param>
        /// <param name="controllerType">Controller type</param>
        /// <param name="controllerId">Controller id</param>
        public override void LoadControllerData(int playerId, ControllerType controllerType, int controllerId) {
            if(!isEnabled) {
                Debug.LogWarning(thisScriptName + " is disabled and will not load any data.", this);
                return;
            }
            int count = LoadControllerDataNow(playerId, controllerType, controllerId);

#if UNITY_EDITOR
            if(count > 0) Debug.Log("Rewired: " + thisScriptName + " loaded user data for " + controllerType + " " + controllerId + " for Player " + playerId + " from XML. " + editorLoadedMessage);
#endif
        }

        /// <summary>
        /// Load all data for a specific controller. Does not load Player data.
        /// </summary>
        /// <param name="controllerType">Controller type</param>
        /// <param name="controllerId">Controller id</param>
        public override void LoadControllerData(ControllerType controllerType, int controllerId) {
            if(!isEnabled) {
                Debug.LogWarning(thisScriptName + " is disabled and will not load any data.", this);
                return;
            }
            int count = LoadControllerDataNow(controllerType, controllerId);

#if UNITY_EDITOR
            if(count > 0) Debug.Log("Rewired: " + thisScriptName + " loaded user data for " + controllerType + " " + controllerId + " from XML. " + editorLoadedMessage);
#endif
        }

        /// <summary>
        /// Load all data for a specific Player.
        /// </summary>
        /// <param name="playerId">Player id</param>
        public override void LoadPlayerData(int playerId) {
            if(!isEnabled) {
                Debug.LogWarning(thisScriptName + " is disabled and will not load any data.", this);
                return;
            }
            int count = LoadPlayerDataNow(playerId);

#if UNITY_EDITOR
            if(count > 0) Debug.Log("Rewired: " + thisScriptName + " loaded Player + " + playerId + " user data from XML. " + editorLoadedMessage);
#endif
        }

        /// <summary>
        /// Load all data for a specific InputBehavior for a Player.
        /// </summary>
        /// <param name="playerId">Player id</param>
        /// <param name="behaviorId">Input Behavior id</param>
        public override void LoadInputBehavior(int playerId, int behaviorId) {
            if(!isEnabled) {
                Debug.LogWarning(thisScriptName + " is disabled and will not load any data.", this);
                return;
            }
            int count = LoadInputBehaviorNow(playerId, behaviorId);

#if UNITY_EDITOR
            if(count > 0) Debug.Log("Rewired: " + thisScriptName + " loaded Player + " + playerId + " InputBehavior data from XML. " + editorLoadedMessage);
#endif
        }

        // Event Handlers

        /// <summary>
        /// Called when SaveDataStore is initialized.
        /// </summary>
        protected override void OnInitialize() {
            if(loadDataOnStart) Load();
        }

        /// <summary>
        /// Called when a controller is connected.
        /// </summary>
        /// <param name="args">ControllerStatusChangedEventArgs</param>
        protected override void OnControllerConnected(ControllerStatusChangedEventArgs args) {
            if(!isEnabled) return;

            // Load data when joystick is connected
            if(args.controllerType == ControllerType.Joystick) {
                int count = LoadJoystickData(args.controllerId);
#if UNITY_EDITOR
                if(count > 0) Debug.Log("Rewired: " + thisScriptName + " loaded Joystick " + args.controllerId + " (" + ReInput.controllers.GetJoystick(args.controllerId).hardwareName + ") data from XML. " + editorLoadedMessage);
#endif
            }
        }

        /// <summary>
        /// Calls after a controller has been disconnected.
        /// </summary>
        /// <param name="args">ControllerStatusChangedEventArgs</param>
        protected override void OnControllerPreDiscconnect(ControllerStatusChangedEventArgs args) {
            if(!isEnabled) return;

            // Save data before joystick is disconnected
            if(args.controllerType == ControllerType.Joystick) {
                SaveJoystickData(args.controllerId);
#if UNITY_EDITOR
                Debug.Log("Rewired: " + thisScriptName + " saved Joystick " + args.controllerId + " (" + ReInput.controllers.GetJoystick(args.controllerId).hardwareName + ") data to XML.");
#endif
            }
        }

        /// <summary>
        /// Called when a controller is about to be disconnected.
        /// </summary>
        /// <param name="args">ControllerStatusChangedEventArgs</param>
        protected override void OnControllerDisconnected(ControllerStatusChangedEventArgs args) {
            if(!isEnabled) return;

            // Nothing to do
        }

        #endregion

        #region Load

        private int LoadAll() {

            int count = 0;

            // Load all data for all players
            IList<Player> allPlayers = ReInput.players.AllPlayers;
            for(int i = 0; i < allPlayers.Count; i++) {
                count += LoadPlayerDataNow(allPlayers[i]);
            }

            // Load all joystick calibration maps
            count += LoadAllJoystickCalibrationData();

            return count;
        }

        private int LoadPlayerDataNow(int playerId) {
            return LoadPlayerDataNow(ReInput.players.GetPlayer(playerId));
        }
        private int LoadPlayerDataNow(Player player) {
            if(player == null) return 0;

            int count = 0;

            // Load Input Behaviors
            count += LoadInputBehaviors(player.id);

            // Load Keyboard Maps
            count += LoadControllerMaps(player.id, ControllerType.Keyboard, 0);

            // Load Mouse Maps
            count += LoadControllerMaps(player.id, ControllerType.Mouse, 0);

            // Load Joystick Maps for each joystick
            foreach(Joystick joystick in player.controllers.Joysticks) {
                count += LoadControllerMaps(player.id, ControllerType.Joystick, joystick.id);
            }

            return count;
        }

        private int LoadAllJoystickCalibrationData() {
            int count = 0;
            // Load all calibration maps from all joysticks
            IList<Joystick> joysticks = ReInput.controllers.Joysticks;
            for(int i = 0; i < joysticks.Count; i++) {
                count += LoadJoystickCalibrationData(joysticks[i]);
            }
            return count;
        }

        private int LoadJoystickCalibrationData(Joystick joystick) {
            if(joystick == null) return 0;
            return joystick.ImportCalibrationMapFromXmlString(GetJoystickCalibrationMapXml(joystick)) ? 1 : 0; // load joystick calibration map
        }
        private int LoadJoystickCalibrationData(int joystickId) {
            return LoadJoystickCalibrationData(ReInput.controllers.GetJoystick(joystickId));
        }

        private int LoadJoystickData(int joystickId) {
            int count = 0;
            // Load joystick maps in all Players for this joystick id
            IList<Player> allPlayers = ReInput.players.AllPlayers;
            for(int i = 0; i < allPlayers.Count; i++) { // this controller may be owned by more than one player, so check all
                Player player = allPlayers[i];
                if(!player.controllers.ContainsController(ControllerType.Joystick, joystickId)) continue; // player does not have the joystick
                count += LoadControllerMaps(player.id, ControllerType.Joystick, joystickId); // load the maps
            }

            // Load calibration maps for joystick
            count += LoadJoystickCalibrationData(joystickId);

            return count;
        }

        private int LoadControllerDataNow(int playerId, ControllerType controllerType, int controllerId) {

            int count = 0;

            // Load map data
            count += LoadControllerMaps(playerId, controllerType, controllerId);

            // Loat other controller data
            count += LoadControllerDataNow(controllerType, controllerId);

            return count;
        }
        private int LoadControllerDataNow(ControllerType controllerType, int controllerId) {

            int count = 0;

            // Load calibration data for joysticks
            if(controllerType == ControllerType.Joystick) {
                count += LoadJoystickCalibrationData(controllerId);
            }

            return count;
        }

        private int LoadControllerMaps(int playerId, ControllerType controllerType, int controllerId) {
            int count = 0;
            Player player = ReInput.players.GetPlayer(playerId);
            if(player == null) return count;

            Controller controller = ReInput.controllers.GetController(controllerType, controllerId);
            if(controller == null) return count;

            // Load the controller maps first and make sure we have them to load
            List<string> xmlMaps = GetAllControllerMapsXml(player, true, controllerType, controller);
            if(xmlMaps.Count == 0) return count;

            // Load Joystick Maps
            count += player.controllers.maps.AddMapsFromXml(controllerType, controllerId, xmlMaps); // load controller maps

            return count;
        }

        private int LoadInputBehaviors(int playerId) {
            Player player = ReInput.players.GetPlayer(playerId);
            if(player == null) return 0;

            int count = 0;

            // All players have an instance of each input behavior so it can be modified
            IList<InputBehavior> behaviors = ReInput.mapping.GetInputBehaviors(player.id); // get all behaviors from player
            for(int i = 0; i < behaviors.Count; i++) {
                count += LoadInputBehaviorNow(player, behaviors[i]);
            }

            return count;
        }

        private int LoadInputBehaviorNow(int playerId, int behaviorId) {
            Player player = ReInput.players.GetPlayer(playerId);
            if(player == null) return 0;

            InputBehavior behavior = ReInput.mapping.GetInputBehavior(playerId, behaviorId);
            if(behavior == null) return 0;

            return LoadInputBehaviorNow(player, behavior);
        }
        private int LoadInputBehaviorNow(Player player, InputBehavior inputBehavior) {
            if(player == null || inputBehavior == null) return 0;

            string xml = GetInputBehaviorXml(player, inputBehavior.id); // try to the behavior for this id
            if(xml == null || xml == string.Empty) return 0; // no data found for this behavior
            return inputBehavior.ImportXmlString(xml) ? 1 : 0; // import the data into the behavior
        }

        #endregion

        #region Save

        private void SaveAll() {

            // Save all data in all Players including System Player
            IList<Player> allPlayers = ReInput.players.AllPlayers;
            for(int i = 0; i < allPlayers.Count; i++) {
                SavePlayerDataNow(allPlayers[i]);
            }

            // Save joystick calibration maps
            SaveAllJoystickCalibrationData();

            // Save changes to PlayerPrefs
            PlayerPrefs.Save();
        }

        private void SavePlayerDataNow(int playerId) {
            SavePlayerDataNow(ReInput.players.GetPlayer(playerId));

            // Save changes to PlayerPrefs
            PlayerPrefs.Save();
        }
        private void SavePlayerDataNow(Player player) {
            if(player == null) return;

            // Get all savable data from player
            PlayerSaveData playerData = player.GetSaveData(true);

            // Save Input Behaviors
            SaveInputBehaviors(player, playerData);

            // Save controller maps
            SaveControllerMaps(player, playerData);
        }

        private void SaveAllJoystickCalibrationData() {
            // Save all calibration maps from all joysticks
            IList<Joystick> joysticks = ReInput.controllers.Joysticks;
            for(int i = 0; i < joysticks.Count; i++) {
                SaveJoystickCalibrationData(joysticks[i]);
            }
        }

        private void SaveJoystickCalibrationData(int joystickId) {
            SaveJoystickCalibrationData(ReInput.controllers.GetJoystick(joystickId));
        }
        private void SaveJoystickCalibrationData(Joystick joystick) {
            if(joystick == null) return;
            JoystickCalibrationMapSaveData saveData = joystick.GetCalibrationMapSaveData();
            string key = GetJoystickCalibrationMapPlayerPrefsKey(saveData);
            PlayerPrefs.SetString(key, saveData.map.ToXmlString()); // save the map to player prefs in XML format
        }

        private void SaveJoystickData(int joystickId) {
            // Save joystick maps in all Players for this joystick id
            IList<Player> allPlayers = ReInput.players.AllPlayers;
            for(int i = 0; i < allPlayers.Count; i++) { // this controller may be owned by more than one player, so check all
                Player player = allPlayers[i];
                if(!player.controllers.ContainsController(ControllerType.Joystick, joystickId)) continue; // player does not have the joystick

                // Save controller maps
                SaveControllerMaps(player.id, ControllerType.Joystick, joystickId);
            }

            // Save calibration data
            SaveJoystickCalibrationData(joystickId);
        }

        private void SaveControllerDataNow(int playerId, ControllerType controllerType, int controllerId) {

            // Save map data
            SaveControllerMaps(playerId, controllerType, controllerId);

            // Save other controller data
            SaveControllerDataNow(controllerType, controllerId);

            // Save changes to PlayerPrefs
            PlayerPrefs.Save();
        }
        private void SaveControllerDataNow(ControllerType controllerType, int controllerId) {

            // Save calibration data for joysticks
            if(controllerType == ControllerType.Joystick) {
                SaveJoystickCalibrationData(controllerId);
            }

            // Save changes to PlayerPrefs
            PlayerPrefs.Save();
        }

        private void SaveControllerMaps(Player player, PlayerSaveData playerSaveData) {
            foreach(ControllerMapSaveData saveData in playerSaveData.AllControllerMapSaveData) {
                string key = GetControllerMapPlayerPrefsKey(player, saveData);
                PlayerPrefs.SetString(key, saveData.map.ToXmlString()); // save the map to player prefs in XML format
            }
        }
        private void SaveControllerMaps(int playerId, ControllerType controllerType, int controllerId) {
            Player player = ReInput.players.GetPlayer(playerId);
            if(player == null) return;

            // Save controller maps in this player for this controller id
            string key;
            if(!player.controllers.ContainsController(controllerType, controllerId)) return; // player does not have the controller

            // Save controller maps
            ControllerMapSaveData[] saveData = player.controllers.maps.GetMapSaveData(controllerType, controllerId, true);
            if(saveData == null) return;

            for(int i = 0; i < saveData.Length; i++) {
                key = GetControllerMapPlayerPrefsKey(player, saveData[i]);
                PlayerPrefs.SetString(key, saveData[i].map.ToXmlString()); // save the map to player prefs in XML format
            }
        }

        private void SaveInputBehaviors(Player player, PlayerSaveData playerSaveData) {
            if(player == null) return;
            InputBehavior[] inputBehaviors = playerSaveData.inputBehaviors;
            for(int i = 0; i < inputBehaviors.Length; i++) {
                SaveInputBehaviorNow(player, inputBehaviors[i]);
            }
        }

        private void SaveInputBehaviorNow(int playerId, int behaviorId) {
            Player player = ReInput.players.GetPlayer(playerId);
            if(player == null) return;

            InputBehavior behavior = ReInput.mapping.GetInputBehavior(playerId, behaviorId);
            if(behavior == null) return;

            SaveInputBehaviorNow(player, behavior);

            // Save changes to PlayerPrefs
            PlayerPrefs.Save();
        }
        private void SaveInputBehaviorNow(Player player, InputBehavior inputBehavior) {
            if(player == null || inputBehavior == null) return;

            string key = GetInputBehaviorPlayerPrefsKey(player, inputBehavior);
            PlayerPrefs.SetString(key, inputBehavior.ToXmlString()); // save the behavior to player prefs in XML format
        }

#endregion

#region PlayerPrefs Methods

        /* NOTE ON PLAYER PREFS:
         * PlayerPrefs on Windows Standalone is saved in the registry. There is a bug in Regedit that makes any entry with a name equal to or greater than 255 characters
         * (243 + 12 unity appends) invisible in Regedit. Unity will still load the data fine, but if you are debugging and wondering why your data is not showing up in
         * Regedit, this is why. If you need to delete the values, either call PlayerPrefs.Clear or delete the key folder in Regedit -- Warning: both methods will
         * delete all player prefs including any ones you've created yourself or other plugins have created.
         */

        // WARNING: Do not use & symbol in keys. Linux cannot load them after the current session ends.

        private string GetBasePlayerPrefsKey(Player player) {
            string key = playerPrefsKeyPrefix;
            key += "|playerName=" + player.name; // make a key for this specific player, could use id, descriptive name, or a custom profile identifier of your choice
            return key;
        }

        private string GetControllerMapPlayerPrefsKey(Player player, ControllerMapSaveData saveData) {
            // Create a player prefs key like a web querystring so we can search for player prefs key when loading maps
            string key = GetBasePlayerPrefsKey(player);
            key += "|dataType=ControllerMap";
            key += "|controllerMapType=" + saveData.mapTypeString;
            key += "|categoryId=" + saveData.map.categoryId + "|" + "layoutId=" + saveData.map.layoutId;
            key += "|hardwareIdentifier=" + saveData.controllerHardwareIdentifier; // the hardware identifier string helps us identify maps for unknown hardware because it doesn't have a Guid
            if(saveData.mapType == typeof(JoystickMap)) { // store special info for joystick maps
                key += "|hardwareGuid=" + ((JoystickMapSaveData)saveData).joystickHardwareTypeGuid.ToString(); // the identifying GUID that determines which known joystick this is
            }
            return key;
        }

        private string GetControllerMapXml(Player player, ControllerType controllerType, int categoryId, int layoutId, Controller controller) {
            string key = GetBasePlayerPrefsKey(player);
            key += "|dataType=ControllerMap";
            key += "|controllerMapType=" + controller.mapTypeString;
            key += "|categoryId=" + categoryId + "|" + "layoutId=" + layoutId;
            key += "|hardwareIdentifier=" + controller.hardwareIdentifier; // the hardware identifier string helps us identify maps for unknown hardware because it doesn't have a Guid
            if(controllerType == ControllerType.Joystick) {
                Joystick joystick = (Joystick)controller;
                key += "|hardwareGuid=" + joystick.hardwareTypeGuid.ToString(); // the identifying GUID that determines which known joystick this is
            }

            if(!PlayerPrefs.HasKey(key)) return string.Empty; // key does not exist
            return PlayerPrefs.GetString(key); // return the data
        }

        private List<string> GetAllControllerMapsXml(Player player, bool userAssignableMapsOnly, ControllerType controllerType, Controller controller) {
            // Because player prefs does not allow us to search for partial keys, we have to check all possible category ids and layout ids to find the maps to load

            List<string> mapsXml = new List<string>();

            IList<InputMapCategory> categories = ReInput.mapping.MapCategories;
            for(int i = 0; i < categories.Count; i++) {
                InputMapCategory cat = categories[i];
                if(userAssignableMapsOnly && !cat.userAssignable) continue; // skip map because not user-assignable

                IList<InputLayout> layouts = ReInput.mapping.MapLayouts(controllerType);
                for(int j = 0; j < layouts.Count; j++) {
                    InputLayout layout = layouts[j];
                    string xml = GetControllerMapXml(player, controllerType, cat.id, layout.id, controller);
                    if(xml == string.Empty) continue;
                    mapsXml.Add(xml);
                }
            }

            return mapsXml;
        }

        private string GetJoystickCalibrationMapPlayerPrefsKey(JoystickCalibrationMapSaveData saveData) {
            // Create a player prefs key like a web querystring so we can search for player prefs key when loading maps
            string key = playerPrefsKeyPrefix;
            key += "|dataType=CalibrationMap";
            key += "|controllerType=" + saveData.controllerType.ToString();
            key += "|hardwareIdentifier=" + saveData.hardwareIdentifier; // the hardware identifier string helps us identify maps for unknown hardware because it doesn't have a Guid
            key += "|hardwareGuid=" + saveData.joystickHardwareTypeGuid.ToString();
            return key;
        }

        private string GetJoystickCalibrationMapXml(Joystick joystick) {
            string key = playerPrefsKeyPrefix;
            key += "|dataType=CalibrationMap";
            key += "|controllerType=" + joystick.type.ToString();
            key += "|hardwareIdentifier=" + joystick.hardwareIdentifier; // the hardware identifier string helps us identify maps for unknown hardware because it doesn't have a Guid
            key += "|hardwareGuid=" + joystick.hardwareTypeGuid.ToString();

            if(!PlayerPrefs.HasKey(key)) return string.Empty; // key does not exist
            return PlayerPrefs.GetString(key); // return the data
        }

        private string GetInputBehaviorPlayerPrefsKey(Player player, InputBehavior saveData) {
            // Create a player prefs key like a web querystring so we can search for player prefs key when loading maps
            string key = GetBasePlayerPrefsKey(player);
            key += "|dataType=InputBehavior";
            key += "|id=" + saveData.id;
            return key;
        }

        private string GetInputBehaviorXml(Player player, int id) {
            string key = GetBasePlayerPrefsKey(player);
            key += "|dataType=InputBehavior";
            key += "|id=" + id;

            if(!PlayerPrefs.HasKey(key)) return string.Empty; // key does not exist
            return PlayerPrefs.GetString(key); // return the data
        }

#endregion
    }
}