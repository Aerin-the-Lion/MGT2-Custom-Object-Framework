using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using HarmonyLib;
using UnityEngine.UI;

namespace MCOFramework
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInProcess("Mad Games Tycoon 2.exe")]
    public class Main : BaseUnityPlugin
    {
        public const string PluginGuid = "me.Aerin_the_Lion.Mad_Games_Tycoon_2.MCOFramework";
        public const string PluginName = "MCOFramework";
        public const string PluginVersion = "1.0.0.0";
        public static ConfigEntry<bool> CFG_IS_ENABLED { get; private set; }

        public void LoadConfig()
        {
            string textIsEnable = "0. MOD Settings";
            CFG_IS_ENABLED = Config.Bind<bool>(textIsEnable, "Activate the MOD", true, "If you need to disable the mod, Toggle to Disabled");



        }

        void Awake()
        {
            LoadConfig();
            //Harmony.CreateAndPatchAll(typeof(MCOFramework));
        }
        void Update()
        {
            //UpdateCount++;
            //Debug.Log("Update Count : " + UpdateCount);
        }

    }
}
