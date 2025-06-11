using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

public class TracersMod
{
    private bool isTracersEnabledLocally = false;
    private static TracersMod _instance;

    public TracersMod()
    {
        InitializeMod();
        _instance = this;
    }

    public void InitializeMod()
    {
        isTracersEnabledLocally = false;
        Debug.Log("Tracers Mod Initialized!");
    }

    public void OnGameUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            ToggleTracersEsp();
        }

        if (isTracersEnabledLocally)
        {
            TracersEspHelpers.DrawTracers();
        }
    }

    public static void ToggleTracersMod()
    {
        if (_instance != null)
        {
            _instance.ToggleTracersEsp();
        }
        else
        {
            Debug.LogError("TracersMod instance is not initialized! Ensure your mod is properly loaded.");
        }
    }

    private void ToggleTracersEsp()
    {
        isTracersEnabledLocally = !isTracersEnabledLocally;
        Debug.Log($"Tracers ESP State: {(isTracersEnabledLocally ? "ENABLED" : "DISABLED")}");
    }
}

public static class TracersEspHelpers
{
    private static void DrawLine(Vector3 start, Vector3 end, UnityEngine.Color color, float thickness)
    {
        GameObject line = new GameObject("TracerLine");
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

    public static void DrawTracers()
    {
        if (GorillaTagger.Instance == null || GorillaParent.instance == null || GorillaTagger.Instance.offlineVRRig == null) return;

        Transform localHeadTransform = null;
        if (GorillaTagger.Instance.offlineVRRig.head != null)
        {
            localHeadTransform = GorillaTagger.Instance.offlineVRRig.head.rigTarget;
        }
        if (localHeadTransform == null)
        {
            localHeadTransform = FindChildRecursive(GorillaTagger.Instance.offlineVRRig.transform, "Head");
        }

        if (localHeadTransform == null)
        {
            return;
        }

        Vector3 localPlayerHeadPos = localHeadTransform.position;

        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
        {
            if (vrrig == GorillaTagger.Instance.offlineVRRig)
            {
                continue;
            }

            Transform otherPlayerTargetTransform = null;
            if (vrrig.head != null)
            {
                otherPlayerTargetTransform = vrrig.head.rigTarget;
            }
            if (otherPlayerTargetTransform == null)
            {
                otherPlayerTargetTransform = FindChildRecursive(vrrig.transform, "Head");
            }

            if (otherPlayerTargetTransform != null)
            {
                Vector3 otherPlayerTargetPos = otherPlayerTargetTransform.position;
                UnityEngine.Color playerColor = vrrig.playerColor;

                DrawLine(localPlayerHeadPos, otherPlayerTargetPos, playerColor, 0.015f);
            }
        }
    }
}