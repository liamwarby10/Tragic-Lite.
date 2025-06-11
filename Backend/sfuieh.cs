using UnityEngine;
using System.Collections.Generic; // For List
using Photon.Pun; // For PhotonView

// Ensure this static class is defined in your project.
// It can be in the same file as your MelonMod class or a separate .cs file.
public static class GorillaTagEsp
{
    // --- Helper Methods ---

    // Draws a line between two points. Useful for wireframes, but not directly for filled boxes.
    private static void DrawLine(Vector3 start, Vector3 end, UnityEngine.Color color, float thickness)
    {
        GameObject line = new GameObject("EspLine");
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("GUI/Text Shader")); // See-through shader
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        UnityEngine.Object.Destroy(line, Time.deltaTime); // Destroy after one frame
    }

    // Helper to find a child Transform by name recursively.
    // This is crucial for finding specific body part GameObjects in VRRig's hierarchy.
    private static Transform FindChildRecursive(Transform parent, string name)
    {
        if (parent == null) return null;
        foreach (Transform child in parent)
        {
            // Using 'Contains' for flexibility, but '==' is more precise if you have exact names.
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

    // --- Head ESP Filled 3D with Colliders Box ---
    public static void HeadBoxFilledEsp()
    {
        // Safety check: Ensure core game instances are initialized
        if (GorillaParent.instance == null || GorillaTagger.Instance == null)
        {
            // Debug.LogWarning("GorillaTagEsp: Core game instances not ready."); // Uncomment for debugging
            return;
        }

        // Define the dimensions of the head box. You will need to adjust these values
        // based on how the monkey heads look in Gorilla Tag to get an accurate fit.
        float headWidth = 0.2f;  // X-axis (side to side)
        float headHeight = 0.25f; // Y-axis (top to bottom)
        float headDepth = 0.2f;  // Z-axis (front to back)

        foreach (VRRig vrrig in GorillaParent.instance.vrrigs) // Iterate through all player rigs in the lobby
        {
            if (vrrig == GorillaTagger.Instance.offlineVRRig)
            {
                continue; // Skip your own rig to avoid self-ESP
            }

            UnityEngine.Color playerColor = vrrig.playerColor; // Get the player's assigned color

            // --- Finding the Head Transform: This is the most reliable way given previous errors. ---
            // You MUST use a Unity Explorer tool to find the exact name of the GameObject/Transform
            // that represents the head in the VRRig's hierarchy.
            // Common names to try: "Head", "GorillaHead", "CustomHead"
            Transform headTransform = FindChildRecursive(vrrig.transform, "Head");

            // If "Head" isn't accurate, try other names you found in Unity Explorer, e.g.:
            // Transform headTransform = FindChildRecursive(vrrig.transform, "PlayerHead");
            // Or if it's deeply nested:
            // Transform headTransform = vrrig.transform.Find("Armature/Body/Head"); // Direct path if known

            if (headTransform != null) // Proceed only if we successfully found the head Transform
            {
                GameObject headBox = GameObject.CreatePrimitive(PrimitiveType.Cube); // Create a standard Unity cube

                // --- Getting Player Name: Using PhotonView (most robust method) ---
                string playerName = "UnknownPlayer"; // Default name if we can't find it
                Photon.Pun.PhotonView photonView = vrrig.GetComponent<Photon.Pun.PhotonView>(); // Get the PhotonView component

                if (photonView != null && photonView.Owner != null) // Check if PhotonView and its Owner exist
                {
                    playerName = photonView.Owner.NickName; // Get the player's nickname from the PhotonPlayer object
                }
                headBox.name = "HeadBoxESP_Filled_" + playerName; // Name the GameObject for easier debugging

                // Position the box at the head's location
                headBox.transform.position = headTransform.position;

                // Set the size of the box
                headBox.transform.localScale = new Vector3(headWidth, headHeight, headDepth);

                // Set the material and color for the filled box
                Renderer boxRenderer = headBox.GetComponent<Renderer>();

                // For a truly "filled" look, "Unlit/Color" is good, but NOT visible through walls by default.
                // If you need it filled *and* visible through walls, that usually requires a custom shader
                // that disables Z-testing but renders solid.
                boxRenderer.material = new Material(Shader.Find("Unlit/Color"));
                boxRenderer.material.color = playerColor;

                // Ensure it has a BoxCollider (CreatePrimitive(Cube) adds one by default)
                // We keep it as requested ("with colliders").
                BoxCollider boxCollider = headBox.GetComponent<BoxCollider>();
                if (boxCollider != null)
                {
                    boxCollider.isTrigger = false; // 'false' for solid physical collision; 'true' for trigger events only
                }

                // Destroy the box after a very short delay (one frame) to prevent accumulation.
                // This makes it appear to update its position constantly with the player.
                UnityEngine.Object.Destroy(headBox, Time.deltaTime);
            }
            // Optional: Uncomment for debugging if you can't find the head
            // else
            // {
            //     Debug.LogWarning($"HeadBoxESP: Could not find 'Head' Transform for VRRig: {vrrig.name}");
            // }
        }
    }

    // You would place your other ESP functions (BoxEsp, BeaconEsp, BoneEsp) here as well,
    // in the same static class, if you want them all in one place.
    // Example:
    // public static void BoxEsp() { /* ... existing BoxEsp code ... */ }
    // public static void BeaconEsp() { /* ... existing BeaconEsp code ... */ }
    // public static void BoneEsp() { /* ... existing BoneEsp code ... */ }
}