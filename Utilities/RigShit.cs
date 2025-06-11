using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using HarmonyLib;
using MalachiTemp.Backend;

namespace dark.efijiPOIWikjek
{
    /*
       PROTECTION NOTE: THIS TEMPLATE IS PROTECTED MATERIAL FROM "Project Malachi". 
       IF ANY MATERIAL FROM "MalachiTemp" FOUND IN ANY OTHER PROJECT/THING WITHOUT 
       CREDIT OR PERMISSION MUST AND WILL BE REMOVED IMMEDIATELY
    */
    internal class RigShit : MonoBehaviour
    {
        public static VRRig GetVRRigFromPlayer(Player p)
        {
            return GorillaGameManager.instance.FindPlayerVRRig(p);
        }
        public static Player GetPlayerFromVRRig(VRRig p)
        {
            return p.Creator.GetPlayerRef();
        }
        public static NetworkView GetNetworkFromRig(VRRig rig)
        {
            return (NetworkView)Traverse.Create(rig).Field("netView").GetValue();
        }
        public static Player GetPlayerFromGun()
        {
            return GetPlayerFromVRRig(Mods.raycastHit.collider.GetComponent<VRRig>());
        }
        public static PhotonView GetPhotonViewFromVRRig(VRRig p)
        {
            return (PhotonView)Traverse.Create(p).Field("photonView").GetValue();
        }
        public static VRRig GetOwnVRRig()
        {
            return GorillaTagger.Instance.offlineVRRig;
        }
    }
}
