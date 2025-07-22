using BepInEx;
using dark.efijiPOIWikjek;
using MalachiTemp.Backend;
using MalachiTemp.UI;
using HarmonyLib;
using Loading;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Malachis_Temp
{
    /*
       PROTECTION NOTE: THIS TEMPLATE IS PROTECTED MATERIAL FROM "Project Malachi". 
       IF ANY MATERIAL FROM "MalachiTemp" FOUND IN ANY OTHER PROJECT/THING WITHOUT 
       CREDIT OR PERMISSION MUST AND WILL BE REMOVED IMMEDIATELY
    */
    [BepInPlugin(Name, GUID, Version)]
    public class Plugin : BaseUnityPlugin
    {
        public const string Name = "malachistemp";
        public const string GUID = "malachis.temp";
        public const string Version = "1.0";

        private bool patchedHarmony = false;
        [System.Serializable]
        public class LoginData
        {
            public string license;

        }
        void Awake()
        {
            if (!patchedHarmony && Loader.loaded == false)
            {
                Harmony harmony = new Harmony(GUID);
                harmony.PatchAll();
                patchedHarmony = true;
                Loader.loaded = true;

            }
        }
    }
    [HarmonyPatch(typeof(GorillaLocomotion.GTPlayer), "FixedUpdate")]
    internal class UpdatePatch
    {
        private static bool alreadyInit;
        public static GameObject Gameobject;

        static void Postfix()
        {
           
            if (!alreadyInit)
            {
                alreadyInit = true;
                Gameobject = new GameObject();
                Gameobject.AddComponent<Plugin>();
                Gameobject.AddComponent<WristMenu>();
                Gameobject.AddComponent<RigShit>();
                Gameobject.AddComponent<Mods>();
                Gameobject.AddComponent<GhostPatch>();
                Gameobject.AddComponent<GTAG_NotificationLib.NotifiLib>();
                Mods.Load();
                Object.DontDestroyOnLoad(Gameobject);
            }
        }
    }
}
