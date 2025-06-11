 using GTAG_NotificationLib;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;

namespace MalachiTemp.Backend
{
    /*
       PROTECTION NOTE: THIS TEMPLATE IS PROTECTED MATERIAL FROM "Project Malachi". 
       IF ANY MATERIAL FROM "MalachiTemp" FOUND IN ANY OTHER PROJECT/THING WITHOUT 
       CREDIT OR PERMISSION MUST AND WILL BE REMOVED IMMEDIATELY
    */
    [HarmonyPatch(typeof(GorillaNot), "SendReport")]
    internal class anticheatnotif : MonoBehaviour
    {
        private static bool Prefix(string susReason, string susId, string susNick)
        {
            if (susReason != "empty rig" && susId == PhotonNetwork.LocalPlayer.UserId)
            {
                NotifiLib.SendNotification("[<color=red>ANTICHEAT</color>] REPORTED FOR: " + susReason);
            }
            return false;
        }
    }
}
