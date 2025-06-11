using System;
using System.Collections.Generic;
using UnityEngine;
using BepInEx;

namespace MonkeyMansBasicGUITemp
{
    [BepInPlugin("MonkeyMansBasicGUITemp", "Made By MonkeyMan love and thc <3", "1.0.0")]
    public class Gui : BaseUnityPlugin
    {
        // --- GUI State Variables ---
        private Rect windowRect = new Rect(50, 50, 400, 520);
        private bool guiOpen = true; // Controls whether the GUI is drawn
        // The scale and animationSpeed variables are not currently used for any animation/scaling
        // functionality in this code. You can remove them if not planning to use them,
        // or implement animation logic.
        // private float scale = 1f;
        // private float animationSpeed = 10f;

        // Dragging is handled by GUI.DragWindow, so these are not needed for window movement
        // private Vector2 dragStart;
        // private bool dragging;

        private int currentPage = 0; // Pagination isn't used with only 3 mods, but kept for future expansion
        private const int buttonsPerPage = 5;

        private float buttonWidth = 350f;
        private float buttonHeight = 40f;
        private float spacing = 8f;

        private Texture2D gradientTexture; // Background gradient for the window
        private GUIStyle windowStyle; // Custom style for the window
        private GUIStyle buttonStyle; // Custom style for buttons/toggles
        private GUIStyle boxStyle;    // Custom style for the enabled mods box

        private List<ModInfo> allMods; // List to store all mod information

        // --- Nested class for Mod Information ---
        private class ModInfo
        {
            public string buttonText;
            public Action OnEnable;
            public Action OnDisable;
            public bool toggleState;
            // public bool onGui; // This field isn't explicitly used as all mods are on GUI
        }

        // --- Awake: Called when the plugin is loaded ---
        private void Awake()
        {
            // Initialize the gradient background texture
            gradientTexture = new Texture2D(1, 2);
            gradientTexture.SetPixels(new Color[] { new Color(0f, 0f, 0.3f, 0.8f), new Color(0f, 0f, 0f, 0.8f) }); // Added alpha for transparency
            gradientTexture.Apply();

            // Initialize GUI styles (important for custom look and feel)
            windowStyle = new GUIStyle(GUI.skin.window);
            windowStyle.normal.background = gradientTexture; // Set the gradient as background
            windowStyle.normal.textColor = Color.white;
            windowStyle.fontStyle = FontStyle.Bold;
            windowStyle.alignment = TextAnchor.UpperCenter;
            windowStyle.padding = new RectOffset(5, 5, 25, 5); // Adjust padding for title

            buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = Color.white;
            buttonStyle.hover.textColor = Color.cyan;
            buttonStyle.active.textColor = Color.green;
            buttonStyle.normal.background = MakeTex(2, 2, new Color(0.1f, 0.1f, 0.2f, 0.7f)); // Darker, semi-transparent background
            buttonStyle.hover.background = MakeTex(2, 2, new Color(0.2f, 0.2f, 0.4f, 0.8f));
            buttonStyle.active.background = MakeTex(2, 2, new Color(0.0f, 0.3f, 0.0f, 0.9f)); // Green when pressed
            buttonStyle.alignment = TextAnchor.MiddleCenter;
            buttonStyle.fontStyle = FontStyle.Bold;

            boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.background = MakeTex(2, 2, new Color(0.05f, 0.05f, 0.1f, 0.7f)); // Slightly darker than buttons
            boxStyle.normal.textColor = Color.white;
            boxStyle.alignment = TextAnchor.UpperLeft;
            boxStyle.padding = new RectOffset(10, 10, 10, 10);


            // Define your mods here
            allMods = new List<ModInfo>
            {
                new ModInfo
                {
                    buttonText = "WASD Fly",
                    OnEnable = () => Mods.EnableWASDFly(),
                    OnDisable = () => Mods.DisableWASDFly(),
                    toggleState = false // Initial state
                },
                new ModInfo
                {
                    buttonText = "Bug Gun",
                    OnEnable = () => Mods.EnableBugGun(),
                    OnDisable = () => Mods.DisableBugGun(),
                    toggleState = false
                },
                new ModInfo
                {
                    buttonText = "Bat Gun",
                    OnEnable = () => Mods.GunMod.EnableGunTemplate(),
                    OnDisable = () => Mods.GunMod.DisableGunTemplate(),
                    toggleState = false
                }
            };
        }

        // Helper to create a solid color texture for GUIStyles
        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        // --- Update: Handle input for toggling GUI visibility ---
        private void Update()
        {
            // Toggle GUI visibility with 'R' key
            if (Input.GetKeyDown(KeyCode.R))
            {
                guiOpen = !guiOpen;
            }
        }

        // --- OnGUI: The main drawing function for the GUI ---
        private void OnGUI()
        {
            // Only draw the window if guiOpen is true
            if (!guiOpen)
            {
                return;
            }

            // Apply global scaling if you intend to use it, otherwise remove this section
            // GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * scale);

            // Draw the GUI window using the custom windowStyle
            windowRect = GUI.Window(123456, windowRect, WindowFunction, "MONKEYMAN GUI - By MonkeyMan <3", windowStyle);

            // Reset matrix if scaling was applied
            // GUI.matrix = Matrix4x4.identity;
        }

        // --- WindowFunction: Defines the contents of the GUI window ---
        private void WindowFunction(int windowID)
        {
            // The gradientTexture is now set in the windowStyle, so you don't need GUI.DrawTexture here
            // GUI.DrawTexture(new Rect(0, 0, windowRect.width, windowRect.height), gradientTexture);

            // Draw window outline based on mouse hover
            Color outlineColor = new Rect(0, 0, windowRect.width, windowRect.height).Contains(Event.current.mousePosition) ? Color.red : Color.blue;
            DrawOutline(new Rect(0, 0, windowRect.width, windowRect.height), 3, outlineColor);

            // --- Window Dragging: Use GUI.DragWindow for proper dragging ---
            // This line tells Unity that dragging within the top bar of the window should move the window.
            // Remove your manual dragging logic: dragStart, dragging variables and the if (dragging) block.
            GUI.DragWindow(new Rect(0, 0, windowRect.width, 30)); // Make the top 30 pixels draggable

            float y = 30f; // Starting Y position for buttons, below the title bar

            // Discord Button
            Rect discordRect = new Rect((windowRect.width - buttonWidth) / 2, y, buttonWidth, buttonHeight);
            if (GUI.Button(discordRect, "TragicX Discord", buttonStyle))
            {
                Application.OpenURL("https://discord.gg/YQBMARtT");
            }
            y += buttonHeight + spacing;

            // Mod Toggles (using custom buttonStyle for toggles too)
            // No pagination implemented here as there are only 3 mods; if you add more, consider adding page navigation.
            foreach (var mod in allMods)
            {
                Rect toggleRect = new Rect((windowRect.width - buttonWidth) / 2, y, buttonWidth, buttonHeight);
                // Use GUI.Toggle with the custom buttonStyle
                bool newToggleState = GUI.Toggle(toggleRect, mod.toggleState, mod.buttonText, buttonStyle);

                // Check if the toggle state has changed and update the mod's state
                if (newToggleState != mod.toggleState)
                {
                    mod.toggleState = newToggleState; // Update the internal state
                    if (mod.toggleState) // If new state is true (enabled)
                    {
                        mod.OnEnable?.Invoke(); // Call enable action if it exists
                    }
                    else // If new state is false (disabled)
                    {
                        mod.OnDisable?.Invoke(); // Call disable action if it exists
                    }
                }
                y += buttonHeight + spacing;
            }

            // Display Enabled Mods Box
            string enabledModsText = "Enabled Mods:\n";
            foreach (var mod in allMods)
            {
                if (mod.toggleState)
                {
                    enabledModsText += "- " + mod.buttonText + "\n"; // Added a bullet point for clarity
                }
            }

            float enabledModsHeight = windowRect.height - y - 10f; // Adjusted height for bottom margin
            GUI.Box(new Rect(10, y, windowRect.width - 20, enabledModsHeight), enabledModsText, boxStyle);
        }

        // --- DrawOutline: Helper to draw a border around the window ---
        private void DrawOutline(Rect rect, int thickness, Color color)
        {
            Color oldColor = GUI.color;
            GUI.color = color;

            GUI.DrawTexture(new Rect(rect.x, rect.y, rect.width, thickness), Texture2D.whiteTexture); // Top
            GUI.DrawTexture(new Rect(rect.x, rect.y + rect.height - thickness, rect.width, thickness), Texture2D.whiteTexture); // Bottom
            GUI.DrawTexture(new Rect(rect.x, rect.y + thickness, thickness, rect.height - 2 * thickness), Texture2D.whiteTexture); // Left
            GUI.DrawTexture(new Rect(rect.x + rect.width - thickness, rect.y + thickness, thickness, rect.height - 2 * thickness), Texture2D.whiteTexture); // Right

            GUI.color = oldColor;
        }
    }

    // --- Mods Class: Contains your actual mod logic ---
    // (No changes needed here, assuming your actual mod logic works as intended)
    public static class Mods
    {
        private static bool wasdFlyEnabled = false;
        public static void EnableWASDFly()
        {
            if (wasdFlyEnabled) return;
            wasdFlyEnabled = true;
            Debug.Log("WASD Fly Enabled");
            // Your actual WASD fly logic here
        }
        public static void DisableWASDFly()
        {
            if (!wasdFlyEnabled) return;
            wasdFlyEnabled = false;
            Debug.Log("WASD Fly Disabled");
            // Your logic to disable WASD fly here
        }

        private static bool bugGunEnabled = false;
        public static void EnableBugGun()
        {
            if (bugGunEnabled) return;
            bugGunEnabled = true;
            Debug.Log("Bug Gun Enabled");
            // Your bug gun enable code here
        }
        public static void DisableBugGun()
        {
            if (!bugGunEnabled) return;
            bugGunEnabled = false;
            Debug.Log("Bug Gun Disabled");
            // Your bug gun disable code here
        }

        public static class GunMod
        {
            private static bool gunTemplateEnabled = false;
            public static void EnableGunTemplate()
            {
                if (gunTemplateEnabled) return;
                gunTemplateEnabled = true;
                Debug.Log("Gun Template Enabled");
                // Your gun template enable code here
            }
            public static void DisableGunTemplate()
            {
                if (!gunTemplateEnabled) return;
                gunTemplateEnabled = false;
                Debug.Log("Gun Template Disabled");
                // Your gun template disable code here
            }
        }
    }
}