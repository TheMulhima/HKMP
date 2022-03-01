﻿using Hkmp.Game.Client;
using Hkmp.Game.Server;
using Hkmp.Game.Settings;
using Hkmp.Networking.Client;
using Hkmp.Networking.Packet;
using Hkmp.Networking.Server;
using Hkmp.Ui;
using Hkmp.Ui.Resources;
using Hkmp.Util;

namespace Hkmp.Game {
    /**
     * Instantiates all necessary classes to start multiplayer activities
     */
    public class GameManager {
        public GameManager(ModSettings modSettings) {
            ThreadUtil.Instantiate();

            FontManager.LoadFonts();
            TextureManager.LoadTextures();

            var packetManager = new PacketManager();

            var netClient = new NetClient(packetManager);
            var netServer = new NetServer(packetManager);

            var clientGameSettings = new Settings.GameSettings();
            var serverGameSettings = modSettings.GameSettings ?? new Settings.GameSettings();

            var uiManager = new UiManager(
                clientGameSettings,
                modSettings,
                netClient
            );
            
            var serverManager = new ModServerManager(
                netServer, 
                serverGameSettings, 
                packetManager,
                uiManager
            );
            serverManager.Initialize();

            new ClientManager(
                netClient,
                serverManager,
                packetManager,
                uiManager,
                clientGameSettings,
                modSettings
            );
        }
    }
}