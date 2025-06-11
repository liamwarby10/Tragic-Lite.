using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections.Generic;

// This class is your main mod. You will instantiate this class once
// via your custom injection and hooking setup.
public class NetworkedGunMod
{
    // --- Custom Property Keys ---
    private const string GUN_MOD_ACTIVE_PROPERTY_KEY = "GunModActive";
    private const string LAST_GUN_SHOT_PROPERTY_KEY = "LastGunShot";

    // --- Gun Mod Specific Constants ---
    private const float GUN_FIRE_RATE_COOLDOWN = 0.1f;

    // --- Local Mod State Variables ---
    private bool isGunModEnabledLocally = false;
    private float lastGunFireTime = 0f;
    private int localGunShotCounter = 0;
    private int currentCubeColorIndex = 0;

    // --- For tracking received gunshots from other players ---
    private Dictionary<Player, int> lastProcessedGunShotCounters = new Dictionary<Player, int>();

    // --- Static instance to allow calling from outside (e.g., UI button) ---
    private static NetworkedGunMod _instance;

    // --- Constructor ---
    public NetworkedGunMod()
    {
        InitializeMod();
        _instance = this; // Set the static instance when constructed
    }

    // --- Initialization Method ---
    public void InitializeMod()
    {
        SetGunModStatus(false);
        Debug.Log("Networked Gun Mod Initialized!");
    }

    // --- Main Game Update Loop ---
    // This method needs to be called repeatedly every frame by your external hooking setup.
    public void OnGameUpdate()
    {
        // Only run gun logic if the mod is enabled locally
        if (isGunModEnabledLocally)
        {
            GunHelpers.DrawGunVisuals();
            // Check for right VR trigger input and fire
            if (Input.GetKeyDown(KeyCode.JoystickButton9) && Time.time - lastGunFireTime > GUN_FIRE_RATE_COOLDOWN)
            {
                ShootNetworkedCube();
                lastGunFireTime = Time.time;
            }
        }

        // Process networked shots from others regardless of local gun status
        ProcessNetworkedGunShotsFromOthers();
    }

    // --- NEW: Public Static Method to Toggle the Mod from a Button ---
    // This is the single method you'd link to your menu button's onClick event.
    public static void ToggleCubeMod()
    {
        if (_instance != null)
        {
            _instance.ToggleGunModStatus(); // Call the instance method
        }
        else
        {
            Debug.LogError("NetworkedGunMod instance is not initialized! Ensure your mod is properly loaded.");
        }
    }

    // --- Process Networked Gun Shots from Other Players (Polling Custom Properties) ---
    private void ProcessNetworkedGunShotsFromOthers()
    {
        if (!PhotonNetwork.InRoom || GorillaParent.instance == null) return;

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsLocal) continue;

            VRRig vrrig = null;
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                PhotonView rigView = rig.GetComponent<Photon.Pun.PhotonView>();
                if (rigView != null && rigView.Owner == player)
                {
                    vrrig = rig;
                    break;
                }
            }
            if (vrrig == null) continue;

            int lastProcessed = lastProcessedGunShotCounters.ContainsKey(player) ? lastProcessedGunShotCounters[player] : 0;

            if (player.CustomProperties.ContainsKey(LAST_GUN_SHOT_PROPERTY_KEY) &&
                player.CustomProperties[LAST_GUN_SHOT_PROPERTY_KEY] is object[] shotData)
            {
                Vector3 position = (Vector3)shotData[0];
                Vector3 forward = (Vector3)shotData[1];
                int colorIndex = (int)shotData[2];
                int currentShotCounter = (int)shotData[3];

                if (currentShotCounter > lastProcessed)
                {
                    GunHelpers.SpawnCubeLocal(position, forward, GunHelpers.rgbColors[colorIndex]);
                    lastProcessedGunShotCounters[player] = currentShotCounter;
                }
            }
        }
    }

    // --- Gun Mod Feature Toggle Methods ---
    private void ToggleGunModStatus()
    {
        isGunModEnabledLocally = !isGunModEnabledLocally;
        SetGunModStatus(isGunModEnabledLocally);
        Debug.Log($"My Gun Mod State: {(isGunModEnabledLocally ? "ACTIVE" : "INACTIVE")}");
    }

    private void SetGunModStatus(bool isEnabled)
    {
        if (PhotonNetwork.LocalPlayer == null) return;
        Hashtable customProperties = new Hashtable();
        customProperties[GUN_MOD_ACTIVE_PROPERTY_KEY] = isEnabled;
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
    }

    // --- Shoot Networked Cube Method ---
    private void ShootNetworkedCube()
    {
        Transform rightHandTransform = GunHelpers.FindChildRecursive(GorillaTagger.Instance.offlineVRRig.transform, "RightHand");

        if (rightHandTransform != null)
        {
            Vector3 spawnPosition = rightHandTransform.position + rightHandTransform.forward * 0.1f;
            Vector3 shootDirection = rightHandTransform.forward;

            Color cubeColor = GunHelpers.rgbColors[currentCubeColorIndex];
            GunHelpers.SpawnCubeLocal(spawnPosition, shootDirection, cubeColor);

            currentCubeColorIndex = (currentCubeColorIndex + 1) % GunHelpers.rgbColors.Length;
            localGunShotCounter++;

            object[] shotData = new object[] { spawnPosition, shootDirection, currentCubeColorIndex, localGunShotCounter };
            Hashtable customProperties = new Hashtable();
            customProperties[LAST_GUN_SHOT_PROPERTY_KEY] = shotData;
            PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
        }
    }
}

// --- Static Class for Gun-Related Helper Functions ---
public static class GunHelpers
{
    // --- RGB Colors for Projectiles ---
    public static Color[] rgbColors = { Color.red, Color.green, Color.blue };

    // --- Shared Helper Methods ---
    private static void DrawLine(Vector3 start, Vector3 end, UnityEngine.Color color, float thickness)
    {
        GameObject line = new GameObject("GunLine");
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("GUI/Text Shader"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        UnityEngine.Object.Destroy(line, Time.deltaTime);
    }

    public static Transform FindChildRecursive(Transform parent, string name)
    {
        if (parent == null) return null;
        foreach (Transform child in parent)
        {
            if (child.name.Contains(name))
            {
                return child;
            }
            Transform found = FindChildRecursive(child, name);
            if (found != null)
            {
                return found;
            }
        }
        return null;
    }

    // --- Gun Visuals (Sphere and Aiming Line) ---
    public static void DrawGunVisuals()
    {
        Transform rightHandTransform = FindChildRecursive(GorillaTagger.Instance.offlineVRRig.transform, "RightHand"); // VERIFY "RightHand" Name!

        if (rightHandTransform != null)
        {
            GameObject gunSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gunSphere.name = "ModGunSphere";
            gunSphere.transform.SetParent(rightHandTransform);
            gunSphere.transform.localPosition = new Vector3(0.05f, -0.05f, 0.1f);
            gunSphere.transform.localScale = Vector3.one * 0.1f;

            Renderer sphereRenderer = gunSphere.GetComponent<Renderer>();
            sphereRenderer.material = new Material(Shader.Find("Unlit/Color"));
            sphereRenderer.material.color = Color.cyan;
            sphereRenderer.material.shader = Shader.Find("GUI/Text Shader");

            UnityEngine.Object.Destroy(gunSphere.GetComponent<Collider>());
            UnityEngine.Object.Destroy(gunSphere, Time.deltaTime);

            DrawLine(rightHandTransform.position, rightHandTransform.position + rightHandTransform.forward * 5f, Color.white, 0.01f);
        }
    }

    // --- Local Cube Spawning Function ---
    public static void SpawnCubeLocal(Vector3 position, Vector3 forward, Color color)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "NetworkedModCube";
        cube.transform.position = position;
        cube.transform.forward = forward;
        cube.transform.localScale = Vector3.one * 0.15f;

        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material = new Material(Shader.Find("Unlit/Color"));
        cubeRenderer.material.color = color;
        cubeRenderer.material.shader = Shader.Find("GUI/Text Shader");

        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(forward * 20f, ForceMode.VelocityChange);

        UnityEngine.Object.Destroy(cube.GetComponent<Collider>());

        UnityEngine.Object.Destroy(cube, 20f);
    }
}