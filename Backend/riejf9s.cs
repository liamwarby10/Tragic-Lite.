using UnityEngine;
using System.Collections.Generic;

public class GorillaTagESP
{
    // --- Shared Drawing Material ---
    private static Material lineMaterial;

    private static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity's built-in shader for simple colored drawing
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            if (shader == null)
            {
                Debug.LogError("GorillaTagESP: Could not find 'Hidden/Internal-Colored' shader.");
                return;
            }
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Set up blending and culling for transparency and correct drawing
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off); // Don't cull backfaces
            lineMaterial.SetInt("_ZWrite", 0); // Don't write to depth buffer (draws over other objects)
        }
    }

    // --- Core DrawLine Method ---
    // This is used by both Box ESP and Tracers
    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        if (lineMaterial == null)
        {
            CreateLineMaterial();
            if (lineMaterial == null) return; // Exit if material creation failed
        }

        lineMaterial.SetPass(0); // Activate the first pass of the material

        GL.PushMatrix();    // Save current GL matrix
        GL.Begin(GL.LINES); // Tell GL we are drawing lines
        GL.Color(color);    // Set the color for the lines

        GL.Vertex(start);   // Define the start point of the line
        GL.Vertex(end);     // Define the end point of the line

        GL.End();           // Stop drawing lines
        GL.PopMatrix();     // Restore GL matrix
    }

    // --- Tracer Specific Data ---
    private static Dictionary<VRRig, List<Vector3>> playerTracerPositions = new Dictionary<VRRig, List<Vector3>>();
    private static Dictionary<VRRig, Color> playerTracerColors = new Dictionary<VRRig, Color>(); // Store initial player color
    private static int maxTracerPoints = 30; // Max points for tracer (adjust for length)

    // --- Main ESP Rendering Method ---
    public static void RenderAllPlayerESP()
    {
        // Ensure GorillaParent instance and vrrigs are available
        if (GorillaParent.instance == null || GorillaParent.instance.vrrigs == null)
        {
            return;
        }

        foreach (VRRig vrrig in GorillaParent.instance.vrrigs) // Gets all the players in the lobby
        {
            // Skip your own rig and null rigs
            if (vrrig == null || vrrig == GorillaTagger.Instance.offlineVRRig)
            {
                continue;
            }

            // --- 1. Draw Box ESP ---
            DrawBoxESP(vrrig);

            // --- 2. Draw Tracers ---
            DrawPlayerTracer(vrrig);
        }
    }

    // --- Box ESP Logic (Refactored into its own method) ---
    private static void DrawBoxESP(VRRig vrrig)
    {
        // Define the dimensions of the 3D box. You can adjust these values.
        float boxWidth = 0.4f;
        float boxHeight = 1.0f;
        float boxDepth = 0.4f;

        // Calculate the half dimensions for easier corner calculation
        float halfWidth = boxWidth / 2f;
        float halfHeight = boxHeight / 2f;
        float halfDepth = boxDepth / 2f;

        // Get the player's color
        // Using a slightly more opaque color for the box for visibility
        Color playerColor = vrrig.playerColor;
        playerColor.a = 0.8f; // Make box lines more visible

        // Define the 8 corners of the 3D box relative to the player's position
        Vector3[] corners = new Vector3[8];
        corners[0] = vrrig.transform.position + new Vector3(-halfWidth, -halfHeight, -halfDepth);
        corners[1] = vrrig.transform.position + new Vector3(halfWidth, -halfHeight, -halfDepth);
        corners[2] = vrrig.transform.position + new Vector3(halfWidth, -halfHeight, halfDepth);
        corners[3] = vrrig.transform.position + new Vector3(-halfWidth, -halfHeight, halfDepth);
        corners[4] = vrrig.transform.position + new Vector3(-halfWidth, halfHeight, -halfDepth);
        corners[5] = vrrig.transform.position + new Vector3(halfWidth, halfHeight, -halfDepth);
        corners[6] = vrrig.transform.position + new Vector3(halfWidth, halfHeight, halfDepth);
        corners[7] = vrrig.transform.position + new Vector3(-halfWidth, halfHeight, halfDepth);

        // Draw the 12 lines of the wireframe box
        DrawLine(corners[0], corners[1], playerColor);
        DrawLine(corners[1], corners[2], playerColor);
        DrawLine(corners[2], corners[3], playerColor);
        DrawLine(corners[3], corners[0], playerColor); // Bottom rectangle

        DrawLine(corners[4], corners[5], playerColor);
        DrawLine(corners[5], corners[6], playerColor);
        DrawLine(corners[6], corners[7], playerColor);
        DrawLine(corners[7], corners[4], playerColor); // Top rectangle

        DrawLine(corners[0], corners[4], playerColor);
        DrawLine(corners[1], corners[5], playerColor);
        DrawLine(corners[2], corners[6], playerColor);
        DrawLine(corners[3], corners[7], playerColor); // Vertical lines
    }

    // --- Tracer Logic (Refactored into its own method) ---
    private static void DrawPlayerTracer(VRRig vrrig)
    {
        // Initialize tracer data for new players
        if (!playerTracerPositions.ContainsKey(vrrig))
        {
            playerTracerPositions[vrrig] = new List<Vector3>();
            playerTracerColors[vrrig] = vrrig.playerColor; // Store initial player color
        }

        // Add the current position to the tracer list
        playerTracerPositions[vrrig].Add(vrrig.transform.position);

        // Limit the number of stored positions to maintain tracer length
        if (playerTracerPositions[vrrig].Count > maxTracerPoints)
        {
            playerTracerPositions[vrrig].RemoveAt(0); // Remove the oldest position
        }

        // Get the base color for the tracer
        Color baseTracerColor = playerTracerColors[vrrig];

        // Draw the tracer lines, fading out over time
        for (int i = 0; i < playerTracerPositions[vrrig].Count - 1; i++)
        {
            // Calculate alpha for fading effect (oldest points are more transparent)
            float alpha = Mathf.Lerp(0.0f, 1.0f, (float)i / playerTracerPositions[vrrig].Count);
            Color fadedColor = new Color(baseTracerColor.r, baseTracerColor.g, baseTracerColor.b, alpha * 0.7f); // Adjust 0.7f for overall transparency

            // Draw the line segment
            DrawLine(playerTracerPositions[vrrig][i], playerTracerPositions[vrrig][i + 1], fadedColor);
        }
    }
}