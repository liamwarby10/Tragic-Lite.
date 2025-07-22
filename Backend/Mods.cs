using dark.efijiPOIWikjek;
using GTAG_NotificationLib;
using Photon.Pun;
using Photon.Realtime;
using MalachiTemp.UI;
using MalachiTemp.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Text = UnityEngine.UI.Text;
using TMPro;
using BepInEx;
using GorillaLocomotion;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Technie.PhysicsCreator;
using GorillaNetworking;
using UnityEngine.Animations.Rigging;
using UnityEngine;                // For GameObject, LineRenderer, Vector3, Time, etc.
using UnityEngine.InputSystem;   // For new Input System (Mouse.current, etc.)
using UnityEngine.XR;
using UnityEngine;
using UnityEngine.InputSystem;  // For Mouse.current
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using GorillaTagScripts;// If you're using XR input features (optional)
using UnityEngine.Networking;
using System.Collections;
using OVR;
using POpusCodec.Enums;
using static Photon.Voice.OpusCodec;
using Photon.Voice.Unity;
using System.Threading.Tasks;




namespace MalachiTemp.Backend
{
    /*
       PROTECTION NOTE: THIS TEMPLATE IS PROTECTED MATERIAL FROM "Project Malachi". 
       IF ANY MATERIAL FROM "MalachiTemp" FOUND IN ANY OTHER PROJECT/THING WITHOUT 
       CREDIT OR PERMISSION MUST AND WILL BE REMOVED IMMEDIATELY
    */
    internal class Mods : MonoBehaviour
    {
        // Double click a grey square to open it, click the - in the box to the left of "#region" to close it
        #region Shit
        public static void DisableButton(string name)
        {
            GetButton(name).enabled = new bool?(false);
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
        }
        public static void PLACEHOLDER()
        {
            // DONT PUT ANYTHING IN HERE
        }
        public static void DrawHandOrbs()
        {
            orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            orb2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Destroy(orb.GetComponent<Rigidbody>());
            Destroy(orb.GetComponent<SphereCollider>());
            Destroy(orb2.GetComponent<Rigidbody>());
            Destroy(orb2.GetComponent<SphereCollider>());
            orb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            orb2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            orb.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            orb2.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            orb.GetComponent<Renderer>().material.color = CurrentGunColor;
            orb2.GetComponent<Renderer>().material.color = CurrentGunColor;
            Destroy(orb, Time.deltaTime);
            Destroy(orb2, Time.deltaTime);
        }
        #endregion
        #region Movement
        public static void FlyMeth(float speed)
        {
            if (WristMenu.abuttonDown)
            {
                GorillaLocomotion.GTPlayer.Instance.transform.position += GorillaLocomotion.GTPlayer.Instance.headCollider.transform.forward * Time.deltaTime * speed;
                GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        public static void Platforms()
        {
            PlatformsThing(invisplat, stickyplatforms);
        }
        public static void TagFreezeDisabler()
        {
            GorillaLocomotion.GTPlayer.Instance.disableMovement = false;
        }
        public static void noarmlengthlimit()
        {
            GorillaLocomotion.GTPlayer.Instance.maxArmLength = 74658;
        }
        public static void megalongarms()
        {
            GTPlayer.Instance.transform.localScale = new Vector3(6f, 6f);
        }

        public static void Invisableplatforms()
        {
            PlatformsThing(true, false);
        }
        // Declare this outside the method in your class
        private static Vector3 flyVelocity = Vector3.zero;

        public static void superflyy()
        {
            Vector3 direction = Vector3.zero;

            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                // Use head forward direction for flying
                direction = GorillaLocomotion.GTPlayer.Instance.headCollider.transform.forward;
            }

            // Set target velocity
            Vector3 targetVelocity = direction * 50f; // Max speed = 50

            // Smoothly accelerate or decelerate
            float accelerationRate = 5f; // Adjust for responsiveness
            flyVelocity = Vector3.Lerp(flyVelocity, targetVelocity, Time.deltaTime * accelerationRate);

            // Apply movement
            GorillaLocomotion.GTPlayer.Instance.transform.position += flyVelocity * Time.deltaTime;

            // Cancel default Rigidbody motion
            GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        public static void bigbat()
        {
            GameObject.Find("Cave Bat Holdable").transform.localScale = new Vector3(6.5f, 6.5f, 6.5f);
        }
        public static void littlebat()
        {
            GameObject.Find("Cave Bat Holdable").transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        public static void Ficbat()
        {
            GameObject.Find("Cave Bat Holdable").transform.localScale = Vector3.one;

        }
        public static void bigbug()
        {
            GameObject.Find("Floating Bug Holdable").transform.localScale = new Vector3(6.5f, 6.5f, 6.5f);
        }
        public static void littlebug()
        {
            GameObject.Find("Floating Bug Holdable").transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        public static void Ficbug()
        {
            GameObject.Find("Floating Bug Holdable").transform.localScale = Vector3.one;

        }
        public static void StumpText()
        {
            GameObject StumpObj = new GameObject("STUMPOBJ");
            TextMeshPro textobj = StumpObj.AddComponent<TextMeshPro>();
            textobj.text = "<color=yellow>Tragic Lite</color> <color=grey>||</color> Version: 3.0.0 <color=grey>||</color> <color=green>Undetected</color>";
            textobj.fontSize = 2f;
            textobj.alignment = TextAlignmentOptions.Center;
            textobj.color = Color.blue;
            textobj.font = GameObject.Find("motdtext").GetComponent<TextMeshPro>().font;
            UnityEngine.Object.Destroy(StumpObj, Time.deltaTime);
            Transform shit = StumpObj.transform;
            shit.GetComponent<TextMeshPro>().renderer.material.shader = Shader.Find("TextMeshPro/Distance Field");
            shit.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            shit.position = new Vector3(-63.5511f, 12.2094f, -82.6264f);
            shit.LookAt(Camera.main.transform.position);
            shit.Rotate(0f, 180f, 0f);
        }

        public static void rigdrone()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = false;

            float moveSpeed = 5f;
            float rotateSpeed = 90f; // degrees per second
            float delta = Time.deltaTime;
            DrawHandOrbs();

            // Target rig transform
            Transform rig = GorillaTagger.Instance.offlineVRRig.transform;

            // === Up and Down ===
            if (WristMenu.triggerDownR || UnityInput.Current.GetKey(KeyCode.Q))
            {
                rig.position += Vector3.up * moveSpeed * delta;
            }
            if (WristMenu.triggerDownL)
            {
                rig.position -= Vector3.up * moveSpeed * delta;
            }

            // === Rotation (Left Stick X) ===
            Vector2 leftStick = ControllerInputPoller.instance.leftControllerPrimary2DAxis;
            if (Mathf.Abs(leftStick.x) > 0.1f)
            {
                float yaw = leftStick.x * rotateSpeed * delta;
                rig.Rotate(0f, yaw, 0f, Space.World);
            }

            // === Movement (Right Stick) ===
            Vector2 rightStick = ControllerInputPoller.instance.rightControllerPrimary2DAxis;
            rig.position += rig.forward * rightStick.y * moveSpeed * delta;
            rig.position += rig.right * rightStick.x * moveSpeed * delta;

            // === Optional: Rotate bodyCollider too to align visuals/physics ===
            GorillaTagger.Instance.bodyCollider.transform.rotation = rig.rotation;
        }
        public static void handorbs()
        {
            DrawHandOrbs();
        }



        public static void joystickflyy()
        {
            Vector2 rightStick = ControllerInputPoller.instance.rightControllerPrimary2DAxis;

            Transform head = GorillaLocomotion.GTPlayer.Instance.headCollider.transform;
            Transform body = GorillaTagger.Instance.headCollider.transform;

            // Head-forward movement (true 3D direction)
            Vector3 headForward = head.forward.normalized;

            // Body strafe (flat, no vertical)
            Vector3 bodyRight = new Vector3(body.right.x, 0, body.right.z).normalized;

            // Combine input
            Vector3 moveDirection = headForward * rightStick.y + bodyRight * rightStick.x;

            // Normalize to avoid fast diagonals
            moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

            float maxSpeed = 25f;
            float acceleration = 6f;

            Vector3 targetVelocity = moveDirection * maxSpeed;
            flyVelocity = Vector3.Lerp(flyVelocity, targetVelocity, Time.deltaTime * acceleration);

            // Move the player
            GorillaLocomotion.GTPlayer.Instance.transform.position += flyVelocity * Time.deltaTime;

            // Cancel Rigidbody movement
            Rigidbody rb = GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }
        public static void freecam()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            Vector2 rightStick = ControllerInputPoller.instance.rightControllerPrimary2DAxis;

            Transform head = GorillaLocomotion.GTPlayer.Instance.headCollider.transform;
            Transform body = GorillaTagger.Instance.headCollider.transform;

            // Head-forward movement (true 3D direction)
            Vector3 headForward = head.forward.normalized;
            DrawHandOrbs();

            // Body strafe (flat, no vertical)
            Vector3 bodyRight = new Vector3(body.right.x, 0, body.right.z).normalized;

            // Combine input
            Vector3 moveDirection = headForward * rightStick.y + bodyRight * rightStick.x;

            // Normalize to avoid fast diagonals
            moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

            float maxSpeed = 25f;
            float acceleration = 6f;

            Vector3 targetVelocity = moveDirection * maxSpeed;
            flyVelocity = Vector3.Lerp(flyVelocity, targetVelocity, Time.deltaTime * acceleration);

            // Move the player
            GorillaLocomotion.GTPlayer.Instance.transform.position += flyVelocity * Time.deltaTime;

            // Cancel Rigidbody movement
            Rigidbody rb = GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }
        public static void fixrig()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }
        public static void breaknek()
        {
            VRRig.LocalRig.head.trackingRotationOffset.y = 116;
        }
        public static void backwardshead()
        {
            VRRig.LocalRig.head.trackingRotationOffset.y = 200;
        }
        public static void fix()
        {
            VRRig.LocalRig.head.trackingRotationOffset.y = 0;
        }

        public static void FlingAllGuardian()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                bool flag = ControllerInputPoller.instance.rightControllerIndexFloat > 0.4f;
                if (flag)
                {
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                        GorillaTagger.Instance.offlineVRRig.transform.position = new UnityEngine.Vector3(0f, 9999f, 0f);
                        GorillaTagger.Instance.myVRRig.SendRPC("GrabbedByPlayer", 0, new object[]
                        {
             true,
             false,
             false
                        });
                    }
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }
        













public static void superflyyyy()
        {
            Vector3 direction = Vector3.zero;

            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                // Use head forward direction for flying
                direction = GorillaLocomotion.GTPlayer.Instance.headCollider.transform.forward;
            }

            // Set target velocity
            Vector3 targetVelocity = direction * 5000f;


            float accelerationRate = 14f;
            flyVelocity = Vector3.Lerp(flyVelocity, targetVelocity, Time.deltaTime * accelerationRate);

            // Apply movement
            GorillaLocomotion.GTPlayer.Instance.transform.position += flyVelocity * Time.deltaTime;


            GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        public static GameObject PlatR = null;
        public static GameObject PlatL = null;

        public class GunMod : MonoBehaviour
        {
            private static GameObject BGunSphere;
            private static LineRenderer lineRenderer;
            private static Vector3[] linePositions;

            private static Vector3 previousControllerPosition;
            private static float num = 25f; // lightning smoothing speed

            void Update()
            {
                GunTemplate();
            }

            public static void GunTemplate()
            {
                bool gripActive = ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed;

                if (gripActive)
                {
                    RaycastHit hitInfo;
                    Vector3 rayOrigin = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.position;
                    Vector3 rayDirection = -GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.up;

                    // If aiming with mouse right click, override ray origin/direction with camera ray
                    if (Mouse.current.rightButton.isPressed)
                    {
                        Camera cam = Camera.main;
                        if (cam != null)
                        {
                            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
                            Physics.Raycast(ray, out hitInfo, 100f);
                            rayOrigin = ray.origin;
                            rayDirection = ray.direction;
                        }
                    }

                    if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
                    {
                        // Create sphere + line renderer if not created
                        if (BGunSphere == null)
                        {
                            BGunSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                            BGunSphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                            var mat = new Material(Shader.Find("GorillaTag/UberShader"));
                            mat.color = GorillaTagger.Instance.offlineVRRig.playerColor;
                            BGunSphere.GetComponent<Renderer>().material = mat;

                            Object.Destroy(BGunSphere.GetComponent<Collider>());
                            Object.Destroy(BGunSphere.GetComponent<Rigidbody>());

                            lineRenderer = BGunSphere.AddComponent<LineRenderer>();
                            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                            lineRenderer.widthCurve = AnimationCurve.Linear(0, 0.01f, 1, 0.01f);
                            lineRenderer.positionCount = 50;
                            lineRenderer.startColor = mat.color;
                            lineRenderer.endColor = mat.color;

                            linePositions = new Vector3[50];
                            for (int i = 0; i < linePositions.Length; i++)
                            {
                                linePositions[i] = rayOrigin;
                            }
                        }

                        BGunSphere.transform.position = hitInfo.point;

                        Vector3 controllerPos = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.position;

                        // Lightning flicker effect
                        float jitterAmount = 0.01f;
                        for (int i = 0; i < linePositions.Length; i++)
                        {
                            float t = i / (float)(linePositions.Length - 1);
                            Vector3 targetPos = Vector3.Lerp(controllerPos, hitInfo.point, t);
                            Vector3 jitter = Random.insideUnitSphere * jitterAmount;
                            linePositions[i] = Vector3.Lerp(linePositions[i], targetPos + jitter, Time.deltaTime * num);
                        }

                        lineRenderer.SetPositions(linePositions);

                        Color currentColor = GorillaTagger.Instance.offlineVRRig.playerColor;
                        BGunSphere.GetComponent<Renderer>().material.color = currentColor;
                        lineRenderer.startColor = currentColor;
                        lineRenderer.endColor = currentColor;

                        // Debug inputs
                        if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f)
                        {
                            Debug.Log("Right trigger pressed");
                        }
                        if (Mouse.current.leftButton.isPressed)
                        {
                            Debug.Log("Left mouse button pressed");
                        }

                        // Move the bat to the hit point while holding left trigger or left mouse button
                        if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f || Mouse.current.leftButton.isPressed)
                        {
                            GameObject bat = GameObject.Find("Cave Bat Holdable");
                            if (bat != null)
                            {
                                Debug.Log("Moving bat to: " + hitInfo.point);
                                // Teleport the bat directly to the point for immediate effect
                                bat.transform.position = hitInfo.point;
                            }
                            else
                            {
                                Debug.LogWarning("Bat object not found!");
                            }
                        }

                        previousControllerPosition = controllerPos;
                    }
                }
                else
                {
                    if (BGunSphere != null)
                    {
                        Object.Destroy(BGunSphere);
                        BGunSphere = null;

                        if (lineRenderer != null)
                        {
                            Object.Destroy(lineRenderer);
                            lineRenderer = null;
                        }

                        linePositions = null;
                    }
                }
            }

            public static void InstantTagAll()
            {
                var freezeTagManager = GameObject.FindObjectOfType<GorillaFreezeTagManager>();
                var AmbushTagManager = GameObject.FindObjectOfType<GorillaAmbushManager>();
                var InfectionTagManager = GameObject.FindObjectOfType<GorillaTagManager>();

                if (!freezeTagManager.currentInfected.Contains(VRRig.LocalRig.Creator))
                {
                    freezeTagManager.currentIt = VRRig.LocalRig.Creator;

                }
                else
                {

                    if (!AmbushTagManager.currentInfected.Contains(VRRig.LocalRig.Creator))
                    {
                        AmbushTagManager.currentIt = VRRig.LocalRig.Creator;

                    }

                }
            }
        }
        public static void boxesesp()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {

                    float boxWidth = 0.4f;
                    float boxHeight = 1.0f;
                    float boxDepth = 0.4f;


                    float halfWidth = boxWidth / 2f;
                    float halfHeight = boxHeight / 2f;
                    float halfDepth = boxDepth / 2f;


                    UnityEngine.Color playerColor = vrrig.playerColor;


                    Vector3[] corners = new Vector3[8];
                    corners[0] = vrrig.transform.position + new Vector3(-halfWidth, -halfHeight, -halfDepth);
                    corners[1] = vrrig.transform.position + new Vector3(halfWidth, -halfHeight, -halfDepth);
                    corners[2] = vrrig.transform.position + new Vector3(halfWidth, -halfHeight, halfDepth);
                    corners[3] = vrrig.transform.position + new Vector3(-halfWidth, -halfHeight, halfDepth);
                    corners[4] = vrrig.transform.position + new Vector3(-halfWidth, halfHeight, -halfDepth);
                    corners[5] = vrrig.transform.position + new Vector3(halfWidth, halfHeight, -halfDepth);
                    corners[6] = vrrig.transform.position + new Vector3(halfWidth, halfHeight, halfDepth);
                    corners[7] = vrrig.transform.position + new Vector3(-halfWidth, halfHeight, halfDepth);


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
            }
        }

        public static void BeaconEsp()
        {

            float beaconHeight = 50f;

            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    UnityEngine.Color playerColor = vrrig.playerColor;


                    Vector3 beaconStart = vrrig.transform.position + new Vector3(0f, 0.5f, 0f);
                    Vector3 beaconEnd = beaconStart + new Vector3(0f, beaconHeight, 0f);

                    DrawLine(beaconStart, beaconEnd, playerColor, 0.05f);
                }
            }
        }
        public static void GrabRigMod()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
            else
            {
                if (ControllerInputPoller.instance.leftGrab)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                }
                else GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void GrabBug()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GameObject.Find("Floating Bug Holdable").transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
        }
        public static void SlideControl()
        {
            GorillaLocomotion.GTPlayer.Instance.slideControl = 1f;
        }
        public static void NoName1()
        {
            PhotonNetwork.LocalPlayer.NickName = "格菲魯伊赫吉赫";
            GorillaComputer.instance.currentName = "格菲魯伊赫吉赫";
            GorillaComputer.instance.savedName = "格菲魯伊赫吉赫";
            PlayerPrefs.SetString("GorillaLocomotion.PlayerName", "格菲魯伊赫吉赫");
            PhotonNetwork.ReconnectAndRejoin();

        }
        public static void nameToExternal()
        {
            PhotonNetwork.LocalPlayer.NickName = "Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top ";
            GorillaComputer.instance.currentName = "Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top ";
            GorillaComputer.instance.savedName = "Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top ";
            PlayerPrefs.SetString("GorillaLocomotion.PlayerName", "Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top Tragic On Top ");
            PhotonNetwork.ReconnectAndRejoin();

        }
        private bool canCreateMenu = false;

        void Update()
        {
            if (NetworkSystem.Instance.InRoom && NetworkSystem.Instance.GameModeString.Contains("MODDED"))
            {
                canCreateMenu = true;
            }
            else
            {
                canCreateMenu = false;
            }

            // Example: trying to create or recreate the menu
            if (canCreateMenu)
            {
                // Safe to create menu here
                // CreateMenu(); or RecreateMenu();
            }
        }

        public static void NameToDiscord()
        {
            PhotonNetwork.LocalPlayer.NickName = "https://discord.gg/vr9FKDfG9y";
            GorillaComputer.instance.currentName = "https://discord.gg/vr9FKDfG9y";
            GorillaComputer.instance.savedName = "https://discord.gg/vr9FKDfG9y";
            PlayerPrefs.SetString("GorillaLocomotion.PlayerName", "https://discord.gg/vr9FKDfG9y");
            PhotonNetwork.ReconnectAndRejoin();

        }
        public static void TagAll()
        {
            foreach (var vrrig in GorillaParent.instance.vrrigs)
                if (!vrrig.mainSkin.material.name.Contains("fected") && GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.transform.position;
                    GorillaTagger.Instance.leftHandTransform.position = vrrig.transform.position;
                }

        }
        public static void DisableNetworkTriggers()
        {
            GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(false);
        }
        public static void EnableNetworkTriggers()
        {
            GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(true);
        }


        public static void breadcrumbs()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                bool flag = !vrrig.isOfflineVRRig && !vrrig.isMyPlayer;
                bool flag2 = flag;
                if (flag2)
                {
                    // Use a proper primitive type (e.g., Cube or Sphere)
                    GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere); // Fixed primitive type
                    gameObject.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    gameObject.transform.position = vrrig.transform.position;

                    // Correctly destroy components using UnityEngine.Object.Destroy
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Collider>());

                    // Destroy the gameObject after 1 second
                    UnityEngine.Object.Destroy(gameObject, 1f);

                    gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    UnityEngine.Color playerColor = vrrig.playerColor;
                }
            }
        }
        public static void OculusMic()
        {
            Recorder recorder = GorillaTagger.Instance.offlineVRRig.GetComponent<Recorder>();
            recorder.ReactOnSystemChanges = false;
            recorder.SkipDeviceChangeChecks = true;
            recorder.StopRecording();
            recorder.SamplingRate = SamplingRate.Sampling08000;
            recorder.Bitrate = 8000;
            recorder.FrameDuration = FrameDuration.Frame20ms;
            recorder.VoiceDetection = false;
            recorder.StartRecording();
        }
        public static void LowQualityMicrophone()
        {
            Recorder myRecorder = GorillaTagger.Instance.myRecorder;
            if (myRecorder != null)
            {
                myRecorder.SamplingRate = SamplingRate.Sampling08000;
                myRecorder.Bitrate = 5000;
                myRecorder.RestartRecording(true);
            }
        }
        public static void VeryLowQualityMicrophone()
        {
            Recorder myRecorder = GorillaTagger.Instance.myRecorder;
            if (myRecorder != null)
            {
                myRecorder.SamplingRate = SamplingRate.Sampling08000;
                myRecorder.Bitrate = 2000;
                myRecorder.RestartRecording(true);
            }
        }
        public static void TracersMod()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                bool flag = vrrig != null && !vrrig.isOfflineVRRig && !vrrig.isMyPlayer;
                bool flag2 = flag;
                if (flag2)
                {
                    GameObject gameObject = new GameObject("Line");
                    LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
                    lineRenderer.startWidth = 0.025f;
                    lineRenderer.endWidth = 0.025f;
                    lineRenderer.positionCount = 2;
                    lineRenderer.useWorldSpace = true;
                    lineRenderer.SetPosition(0, GTPlayer.Instance.rightControllerTransform.position);
                    lineRenderer.SetPosition(1, vrrig.transform.position);
                    lineRenderer.material.shader = Shader.Find("GUI/Text Shader");
                    bool flag3 = vrrig.mainSkin.material.name.Contains("fected");
                    if (flag3)
                    {
                        lineRenderer.startColor = Color.red;
                        lineRenderer.endColor = Color.red;
                    }
                    else
                    {
                        lineRenderer.startColor = Color.green;
                        lineRenderer.endColor = Color.green;
                    }
                    UnityEngine.Object.Destroy(gameObject, Time.deltaTime);
                }
            }
        }
        public static Vector2 lerpygerpy = Vector2.zero;
        public static void CarMonke()
        {
            lerpygerpy = Vector2.Lerp(lerpygerpy, ControllerInputPoller.instance.rightControllerPrimary2DAxis, 0.05f);
            RaycastHit raycastHit;
            Physics.Raycast(GorillaTagger.Instance.bodyCollider.transform.position - new Vector3(0f, 0.2f, 0f), Vector3.down, out raycastHit, 512f);

            if (raycastHit.distance < 0.2f && (Mathf.Abs(lerpygerpy.x) > 0.05f || Mathf.Abs(lerpygerpy.y) > 0.05f))
            {
                float speed = 10f; // Set the desired speed here
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity =
                    (GorillaTagger.Instance.bodyCollider.transform.forward * lerpygerpy.y * speed) +
                    (GorillaTagger.Instance.bodyCollider.transform.right * lerpygerpy.x * speed);
            }
        }

        public static GameObject KickDistance;
        public static void Antireport()
        {
            {
                try
                {
                    foreach (GorillaPlayerScoreboardLine gorillaPlayerScoreboardLine in GorillaScoreboardTotalUpdater.allScoreboardLines)
                    {
                        if (gorillaPlayerScoreboardLine.linePlayer == NetworkSystem.Instance.LocalPlayer)
                        {
                            Transform transform = gorillaPlayerScoreboardLine.reportButton.gameObject.transform;
                            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                            {
                                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                                {
                                    float num = Vector3.Distance(vrrig.rightHandTransform.position, transform.position);
                                    float num2 = Vector3.Distance(vrrig.leftHandTransform.position, transform.position);
                                    float num3 = 0.35f;
                                    if (num < num3 || num2 < num3)
                                    {

                                        PhotonNetwork.Disconnect();

                                        NotifiLib.SendNotification("<color=grey>[</color><color=red>Antireport</color>");
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
            }
        }
        public static void GrabBat()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GameObject.Find("Cave Bat Holdable").transform.position = GorillaTagger.Instance.leftHandTransform.position;
            }
        }
        // Helper method to draw a line between two points
        // Added a 'thickness' parameter to allow for different line widths
        private static void DrawLine(Vector3 start, Vector3 end, UnityEngine.Color color, float thickness)
        {
            GameObject line = new GameObject("BeaconLine"); // Give it a distinct name
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();

            // Set the material and shader for the line to be visible through walls
            lineRenderer.material = new Material(Shader.Find("GUI/Text Shader"));
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            lineRenderer.startWidth = thickness;
            lineRenderer.endWidth = thickness;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            // Destroy the line after a short delay to prevent accumulation
            UnityEngine.Object.Destroy(line, Time.deltaTime);
        }


        // Helper method to draw a line between two points
        private static void DrawLine(Vector3 start, Vector3 end, UnityEngine.Color color)
        {
            GameObject line = new GameObject("Line");
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();

            // Set the material and shader for the line to be visible through walls
            lineRenderer.material = new Material(Shader.Find("GUI/Text Shader"));
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            lineRenderer.startWidth = 0.02f; // Adjust line thickness
            lineRenderer.endWidth = 0.02f;   // Adjust line thickness

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            // Destroy the line after a short delay to prevent accumulation
            // Time.deltaTime ensures it's destroyed very quickly, making it appear as a single frame render.
            UnityEngine.Object.Destroy(line, Time.deltaTime);
        }
        public class floatingbugholdable : MonoBehaviour
        {
            private static GameObject BGunSphere;
            private static LineRenderer lineRenderer;
            private static Vector3[] linePositions;

            private static Vector3 previousControllerPosition;
            private static float num = 25f; // lightning smoothing speed

            void Update()
            {
                GunTemplate();
            }

            public static void GunTemplate()
            {
                bool gripActive = ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed;

                if (gripActive)
                {
                    RaycastHit hitInfo;
                    Vector3 rayOrigin = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.position;
                    Vector3 rayDirection = -GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.up;

                    // If aiming with mouse right click, override ray origin/direction with camera ray
                    if (Mouse.current.rightButton.isPressed)
                    {
                        Camera cam = Camera.main;
                        if (cam != null)
                        {
                            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
                            Physics.Raycast(ray, out hitInfo, 100f);
                            rayOrigin = ray.origin;
                            rayDirection = ray.direction;
                        }
                    }

                    if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
                    {
                        // Create sphere + line renderer if not created
                        if (BGunSphere == null)
                        {
                            BGunSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                            BGunSphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                            var mat = new Material(Shader.Find("GorillaTag/UberShader"));
                            mat.color = GorillaTagger.Instance.offlineVRRig.playerColor;
                            BGunSphere.GetComponent<Renderer>().material = mat;

                            Object.Destroy(BGunSphere.GetComponent<Collider>());
                            Object.Destroy(BGunSphere.GetComponent<Rigidbody>());

                            lineRenderer = BGunSphere.AddComponent<LineRenderer>();
                            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                            lineRenderer.widthCurve = AnimationCurve.Linear(0, 0.01f, 1, 0.01f);
                            lineRenderer.positionCount = 50;
                            lineRenderer.startColor = mat.color;
                            lineRenderer.endColor = mat.color;

                            linePositions = new Vector3[50];
                            for (int i = 0; i < linePositions.Length; i++)
                            {
                                linePositions[i] = rayOrigin;
                            }
                        }

                        BGunSphere.transform.position = hitInfo.point;

                        Vector3 controllerPos = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.position;

                        // Lightning flicker effect
                        float jitterAmount = 0.01f;
                        for (int i = 0; i < linePositions.Length; i++)
                        {
                            float t = i / (float)(linePositions.Length - 1);
                            Vector3 targetPos = Vector3.Lerp(controllerPos, hitInfo.point, t);
                            Vector3 jitter = Random.insideUnitSphere * jitterAmount;
                            linePositions[i] = Vector3.Lerp(linePositions[i], targetPos + jitter, Time.deltaTime * num);
                        }

                        lineRenderer.SetPositions(linePositions);

                        Color currentColor = GorillaTagger.Instance.offlineVRRig.playerColor;
                        BGunSphere.GetComponent<Renderer>().material.color = currentColor;
                        lineRenderer.startColor = currentColor;
                        lineRenderer.endColor = currentColor;

                        // Debug inputs
                        if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f)
                        {
                            Debug.Log("Right trigger pressed");
                        }
                        if (Mouse.current.leftButton.isPressed)
                        {
                            Debug.Log("Left mouse button pressed");
                        }

                        // Move the bat to the hit point while holding left trigger or left mouse button
                        if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f || Mouse.current.leftButton.isPressed)
                        {
                            GameObject bat = GameObject.Find("Floating Bug Holdable");
                            if (bat != null)
                            {
                                Debug.Log("Moving bat to: " + hitInfo.point);
                                // Teleport the bat directly to the point for immediate effect
                                bat.transform.position = hitInfo.point;
                            }
                            else
                            {
                                Debug.LogWarning("bug object not found!");
                            }
                        }

                        previousControllerPosition = controllerPos;
                    }
                }
                else
                {
                    if (BGunSphere != null)
                    {
                        Object.Destroy(BGunSphere);
                        BGunSphere = null;

                        if (lineRenderer != null)
                        {
                            Object.Destroy(lineRenderer);
                            lineRenderer = null;
                        }

                        linePositions = null;
                    }
                }
            }
        }






        private static int startY;
        private static float subThingyZ;
        private static float flySpeed;
        private static float startX;
        private static float subThingy;

        private static Vector3 currentFlySpeed = Vector3.zero;
        public static void WASDFly()
        {

            // Reset default Rigidbody motion (prevents interference)
            GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0.067f, 0f);

            // Key inputs
            bool W = UnityInput.Current.GetKey(KeyCode.W);
            bool A = UnityInput.Current.GetKey(KeyCode.A);
            bool S = UnityInput.Current.GetKey(KeyCode.S);
            bool D = UnityInput.Current.GetKey(KeyCode.D);
            bool Space = UnityInput.Current.GetKey(KeyCode.Space);
            bool Ctrl = UnityInput.Current.GetKey(KeyCode.LeftControl);

            // Mouse-based camera rotation
            if (Mouse.current.rightButton.isPressed)
            {
                Vector3 Euler = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.parent.rotation.eulerAngles;

                if (startX < 0)
                {
                    startX = Euler.y;
                    subThingy = Mouse.current.position.value.x / UnityEngine.Screen.width;
                }
                if (startY < 0)
                {
                    startY = (int)Euler.x;
                    subThingyZ = Mouse.current.position.value.y / UnityEngine.Screen.height;
                }

                Euler = new Vector3(
                    startY - (((Mouse.current.position.value.y / UnityEngine.Screen.height - subThingyZ) * 360f) * 1.33f),
                    startX + (((Mouse.current.position.value.x / UnityEngine.Screen.width - subThingy) * 360f) * 1.33f),
                    Euler.z
                );

                GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.parent.rotation = Quaternion.Euler(Euler);
            }
            else
            {
                startX = -1;
                startY = -1;
            }

            // Determine target fly direction
            Vector3 targetVelocity = Vector3.zero;
            float speed = 15f;
            Transform reference = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.parent;

            if (W) targetVelocity += reference.forward;
            if (S) targetVelocity -= reference.forward;
            if (A) targetVelocity -= reference.right;
            if (D) targetVelocity += reference.right;
            if (Space) targetVelocity += Vector3.up;
            if (Ctrl) targetVelocity -= Vector3.up;

            targetVelocity = targetVelocity.normalized * speed;

            // Smooth velocity with Lerp
            float acceleration = 5f;
            currentFlySpeed = Vector3.Lerp(currentFlySpeed, targetVelocity, Time.deltaTime * acceleration);

            // Apply movement
            GorillaTagger.Instance.rigidbody.transform.position += currentFlySpeed * Time.deltaTime;
        }

        public static void superfly()
        {
            var player = GorillaLocomotion.GTPlayer.Instance;
            var head = player.headCollider.transform;

            // Fallback Unity input axes (make sure they exist in Input Manager)
            float leftX = Input.GetAxis("Horizontal");   // Left stick X
            float leftY = Input.GetAxis("Vertical");     // Left stick Y
            float rightY = Input.GetAxis("FlyUpDown");   // Right stick vertical for fly up/down

            Vector3 horizontal = head.forward * leftY + head.right * leftX;
            horizontal.y = 0f;

            Vector3 vertical = Vector3.up * rightY;

            Vector3 moveDirection = horizontal + vertical;
            float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);
            moveDirection = moveDirection.normalized;

            float maxSpeed = 20f;
            Vector3 targetVelocity = moveDirection * (inputMagnitude * maxSpeed);

            float acceleration = 8f;
            currentFlySpeed = Vector3.Lerp(currentFlySpeed, targetVelocity, Time.deltaTime * acceleration);

            player.transform.position += currentFlySpeed * Time.deltaTime;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        public static void carmonkeyy()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f)
            {
                GorillaLocomotion.GTPlayer.Instance.transform.position += GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.forward * Time.deltaTime * 7f;

                if (ControllerInputPoller.instance.rightGrab)
                {
                    GorillaLocomotion.GTPlayer.Instance.transform.position -= GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.forward * Time.deltaTime * 15f;
                }
            }
        }

        public static float colorChangerDelay = 0f;

        public static void RainbowColor()
        {
            if (Time.time > colorChangerDelay)
            {
                colorChangerDelay = Time.time + 0.1f;

                float hue = (float)Time.frameCount / 180f % 1f;
                Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);

                // Save values to PlayerPrefs so GorillaTag updates them internally
                PlayerPrefs.SetFloat("redValue", rainbowColor.r);
                PlayerPrefs.SetFloat("greenValue", rainbowColor.g);
                PlayerPrefs.SetFloat("blueValue", rainbowColor.b);
                PlayerPrefs.Save();

                // Apply it through the color sync system
                if (GorillaTagger.Instance != null && GorillaTagger.Instance.offlineVRRig != null)
                {
                    GorillaTagger.Instance.offlineVRRig.InitializeNoobMaterialLocal(rainbowColor.r, rainbowColor.g, rainbowColor.b);
                }

                // Sync the color in multiplayer
                if (GorillaComputer.instance != null)
                {
                    GorillaComputer.instance.UpdateColor(rainbowColor.r, rainbowColor.g, rainbowColor.b);
                }
            }
        }
        private static GameObject gunspere;
        public static void BugGun()
        {
            if (ControllerInputPoller.instance.rightGrab || UnityInput.Current.GetMouseButton(1))
            {

                Physics.Raycast(GTPlayer.Instance.rightControllerTransform.position, -GTPlayer.Instance.rightControllerTransform.up, out var hitinfo);
                if (Mouse.current.rightButton.isPressed)

                {

                    Camera cam = GameObject.Find("Shoulder Camera").GetComponent<Camera>();
                    Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
                    Physics.Raycast(ray, out hitinfo, 100);
                }
                BGunSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                BGunSphere.transform.position = hitinfo.point;
                BGunSphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                BGunSphere.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                float h = (Time.frameCount / 180f) % 1f;
                BGunSphere.GetComponent<Renderer>().material.color = GorillaTagger.Instance.offlineVRRig.playerColor;
                GameObject.Destroy(BGunSphere.GetComponent<BoxCollider>());
                GameObject.Destroy(BGunSphere.GetComponent<Rigidbody>());
                GameObject.Destroy(BGunSphere.GetComponent<Collider>());
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f || UnityInput.Current.GetMouseButton(0))
                {
                    GameObject.Destroy(BGunSphere, Time.deltaTime);
                    BGunSphere.GetComponent<Renderer>().material.color = GorillaTagger.Instance.offlineVRRig.playerColor;
                    GameObject.Find("Floating Bug Holdable").transform.position = BGunSphere.transform.position;

                }
            }
            if (BGunSphere != null)
            {

                GameObject.Destroy(BGunSphere, Time.deltaTime);
            }
        }
        private static GameObject BGunSphere;
        public static void bees()
        {
            if (ControllerInputPoller.instance.rightGrab || UnityInput.Current.GetMouseButton(1))
            {

                Physics.Raycast(GTPlayer.Instance.rightControllerTransform.position, -GTPlayer.Instance.rightControllerTransform.up, out var hitinfo);
                if (Mouse.current.rightButton.isPressed)

                {

                    Camera cam = GameObject.Find("Shoulder Camera").GetComponent<Camera>();
                    Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
                    Physics.Raycast(ray, out hitinfo, 100);
                }
                BGunSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                BGunSphere.transform.position = hitinfo.point;
                BGunSphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                BGunSphere.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                float h = (Time.frameCount / 180f) % 1f;
                BGunSphere.GetComponent<Renderer>().material.color = GorillaTagger.Instance.offlineVRRig.playerColor;
                GameObject.Destroy(BGunSphere.GetComponent<BoxCollider>());
                GameObject.Destroy(BGunSphere.GetComponent<Rigidbody>());
                GameObject.Destroy(BGunSphere.GetComponent<Collider>());
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f || UnityInput.Current.GetMouseButton(0))
                {
                    GameObject.Destroy(BGunSphere, Time.deltaTime);
                    BGunSphere.GetComponent<Renderer>().material.color = GorillaTagger.Instance.offlineVRRig.playerColor;
                    GameObject.Find("Floating Bee Holdable").transform.position = BGunSphere.transform.position;

                }
            }
        }


        public static void rideBat()
        {
            GorillaLocomotion.GTPlayer.Instance.transform.position = GameObject.Find("Cave Bat Holdable").transform.position;
        }
        private static Vector3 currentVelocity = Vector3.zero;

        public static void UpAndDown()
        {
            Vector3 direction = Vector3.zero;

            if (ControllerInputPoller.instance.leftGrab)
            {
                direction = GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.up;
            }
            else if (ControllerInputPoller.instance.rightGrab)
            {
                direction = -GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.up;
            }

            if (direction != Vector3.zero)
            {
                Vector3 targetVelocity = direction * 15f;
                currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * 5f);
            }
            else
            {
                currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, Time.deltaTime * 5f);
            }

            GorillaLocomotion.GTPlayer.Instance.transform.position += currentVelocity * Time.deltaTime;
            GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        public static void Noclip()
        {
            foreach (MeshCollider m in Resources.FindObjectsOfTypeAll<MeshCollider>())
            {
                if (WristMenu.triggerDownR)
                {
                    m.enabled = false;
                }
                else
                {
                    m.enabled = true;
                }
            }
        }
        public static void LongArms()
        {
            GTPlayer.Instance.transform.localScale = new Vector3(1.10f, 1.10f, 1.10f);
        }
        public static void normalarms()
        {
            GTPlayer.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        public static void triggerfly()
        {
            Vector3 direction = Vector3.zero;


            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
            {

                direction = GorillaLocomotion.GTPlayer.Instance.headCollider.transform.forward;
            }


            Vector3 targetVelocity = direction * 50f;


            float accelerationRate = 5f; // Adjust for responsiveness
            flyVelocity = Vector3.Lerp(flyVelocity, targetVelocity, Time.deltaTime * accelerationRate);


            GorillaLocomotion.GTPlayer.Instance.transform.position += flyVelocity * Time.deltaTime;


            GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        public static void BoxEsp()
        {
            //This is a 2d box esp!
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)//Gets all the players in the lobby
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig)//Basicly skips your own rig
                {
                    UnityEngine.Color PlayerColor = vrrig.playerColor;//Makes a color that is the same color as the player
                    GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);//Makes the cube
                    box.transform.position = vrrig.transform.position;//Puts it on the player
                    UnityEngine.Object.Destroy(box.GetComponent<BoxCollider>());//Gets rid of the box collider
                    box.transform.localScale = new Vector3(0.5f, 0.5f, 0f);//Size of the bos Depth, Width, Hieght
                    box.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);//Makes it face your own rig so its always visabel
                    box.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");//Makes it the text shader so you can see it through walls
                    box.GetComponent<Renderer>().material.color = PlayerColor;//Calls the players color from earlier 
                    UnityEngine.Object.Destroy(box, Time.deltaTime);//idk how to explain but if its not on Time.Delta it makes it look like a trail
                }
            }
        }
        public static void seccounddisconnect()
        {
            if (ControllerInputPoller.instance.rightControllerSecondaryButton)
            {
                PhotonNetwork.Disconnect();
            }
        }



        #endregion
        #region Rig Mods
        public static void Ghostmonke()
        {
            if (right)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = !ghostMonke;
                if (ghostMonke)
                {
                    DrawHandOrbs();
                }
                if (WristMenu.ybuttonDown && !lastHit)
                {
                    ghostMonke = !ghostMonke;
                }
                lastHit = WristMenu.ybuttonDown;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = !ghostMonke;
                if (ghostMonke)
                {
                    DrawHandOrbs();
                }
                if (WristMenu.bbuttonDown && !lastHit)
                {
                    ghostMonke = !ghostMonke;
                }
                lastHit = WristMenu.bbuttonDown;
            }
        }
        public static void Invis()
        {
            if (right)
            {
                if (invisMonke)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(9999f, 9999f, 9999f);
                    DrawHandOrbs();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
                if (WristMenu.ybuttonDown && !lastHit2)
                {
                    invisMonke = !invisMonke;
                }
                lastHit2 = WristMenu.ybuttonDown;
            }
            else
            {
                if (invisMonke)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(9999f, 9999f, 9999f);
                    DrawHandOrbs();
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
                if (WristMenu.bbuttonDown && !lastHit2)
                {
                    invisMonke = !invisMonke;
                }
                lastHit2 = WristMenu.bbuttonDown;
            }
        }
        #endregion
        #region Visual
        public static void Tracers()
        {
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                VRRig rig = RigShit.GetVRRigFromPlayer(p);
                GameObject g = new GameObject("Line");
                LineRenderer l = g.AddComponent<LineRenderer>();
                l.startWidth = 0.01f;
                l.endWidth = 0.01f;
                l.positionCount = 2;
                l.useWorldSpace = true;
                l.SetPosition(0, GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.position);
                l.SetPosition(1, rig.transform.position);
                l.material.shader = Shader.Find("GUI/Text Shader");
                l.startColor = CurrentESPColor;
                l.endColor = CurrentESPColor;
                Destroy(l, Time.deltaTime);
            }
        }
        [Obsolete]
        public static void FPSboost()
        {
            fps = true;
            if (fps)
            {
                QualitySettings.masterTextureLimit = 999999999;
                QualitySettings.masterTextureLimit = 999999999;
                QualitySettings.globalTextureMipmapLimit = 999999999;
                QualitySettings.maxQueuedFrames = 60;
            }
        }

        [Obsolete]
        public static void fixFPS()
        {
            if (fps)
            {
                QualitySettings.masterTextureLimit = default;
                QualitySettings.masterTextureLimit = default;
                QualitySettings.globalTextureMipmapLimit = default;
                QualitySettings.maxQueuedFrames = default;
                fps = false;
            }
        }
        #endregion
        #region Save-Load Buttons & Settings
        public static void save()
        {
            List<string> list = new List<string>();
            foreach (ButtonInfo buttonInfo in WristMenu.CatButtons1)
            {
                bool? enabled = buttonInfo.enabled;
                bool flag = true;
                if (enabled.GetValueOrDefault() == flag & enabled != null)
                {
                    list.Add(buttonInfo.buttonText);
                }
            }
            foreach (ButtonInfo buttonInfo in WristMenu.CatButtons2)
            {
                bool? enabled = buttonInfo.enabled;
                bool flag = true;
                if (enabled.GetValueOrDefault() == flag & enabled != null)
                {
                    list.Add(buttonInfo.buttonText);
                }
            }
            foreach (ButtonInfo buttonInfo in WristMenu.CatButtons3)
            {
                bool? enabled = buttonInfo.enabled;
                bool flag = true;
                if (enabled.GetValueOrDefault() == flag & enabled != null)
                {
                    list.Add(buttonInfo.buttonText);
                }
            }
            foreach (ButtonInfo buttonInfo in WristMenu.CatButtons4)
            {
                bool? enabled = buttonInfo.enabled;
                bool flag = true;
                if (enabled.GetValueOrDefault() == flag & enabled != null)
                {
                    list.Add(buttonInfo.buttonText);
                }
            }
            foreach (ButtonInfo buttonInfo in WristMenu.CatButtons5)
            {
                bool? enabled = buttonInfo.enabled;
                bool flag = true;
                if (enabled.GetValueOrDefault() == flag & enabled != null)
                {
                    list.Add(buttonInfo.buttonText);
                }
            }
            foreach (ButtonInfo buttonInfo in WristMenu.CatButtons6)
            {
                bool? enabled = buttonInfo.enabled;
                bool flag = true;
                if (enabled.GetValueOrDefault() == flag & enabled != null)
                {
                    list.Add(buttonInfo.buttonText);
                }
            }
            foreach (ButtonInfo buttonInfo in WristMenu.CatButtons7)
            {
                bool? enabled = buttonInfo.enabled;
                bool flag = true;
                if (enabled.GetValueOrDefault() == flag & enabled != null)
                {
                    list.Add(buttonInfo.buttonText);
                }
            }
            foreach (ButtonInfo buttonInfo in WristMenu.CatButtons8)
            {
                bool? enabled = buttonInfo.enabled;
                bool flag = true;
                if (enabled.GetValueOrDefault() == flag & enabled != null)
                {
                    list.Add(buttonInfo.buttonText);
                }
            }
            foreach (ButtonInfo buttonInfo in WristMenu.CatButtons9)
            {
                bool? enabled = buttonInfo.enabled;
                bool flag = true;
                if (enabled.GetValueOrDefault() == flag & enabled != null)
                {
                    list.Add(buttonInfo.buttonText);
                }
            }
            foreach (ButtonInfo buttonInfo in WristMenu.CatButtons10)
            {
                bool? enabled = buttonInfo.enabled;
                bool flag = true;
                if (enabled.GetValueOrDefault() == flag & enabled != null)
                {
                    list.Add(buttonInfo.buttonText);
                }
            }
            File.WriteAllLines(WristMenu.FolderName + "\\Saved_Buttons.txt", list);
            NotifiLib.SendNotification("<color=white>[</color><color=blue>SAVE</color><color=white>]</color> <color=white>Saved Buttons Successfully!</color>");
        }
        public static void Load1()
        {
            string[] array = File.ReadAllLines(WristMenu.FolderName + "\\Saved_Buttons.txt");
            foreach (string b in array)
            {
                foreach (ButtonInfo buttonInfo in WristMenu.CatButtons1)
                {
                    if (buttonInfo.buttonText == b)
                    {
                        buttonInfo.enabled = new bool?(true);
                    }
                }
                foreach (ButtonInfo buttonInfo in WristMenu.CatButtons2)
                {
                    if (buttonInfo.buttonText == b)
                    {
                        buttonInfo.enabled = new bool?(true);
                    }
                }
                foreach (ButtonInfo buttonInfo in WristMenu.CatButtons3)
                {
                    if (buttonInfo.buttonText == b)
                    {
                        buttonInfo.enabled = new bool?(true);
                    }
                }
                foreach (ButtonInfo buttonInfo in WristMenu.CatButtons4)
                {
                    if (buttonInfo.buttonText == b)
                    {
                        buttonInfo.enabled = new bool?(true);
                    }
                }
                foreach (ButtonInfo buttonInfo in WristMenu.CatButtons5)
                {
                    if (buttonInfo.buttonText == b)
                    {
                        buttonInfo.enabled = new bool?(true);
                    }
                }
                foreach (ButtonInfo buttonInfo in WristMenu.CatButtons6)
                {
                    if (buttonInfo.buttonText == b)
                    {
                        buttonInfo.enabled = new bool?(true);
                    }
                }
                foreach (ButtonInfo buttonInfo in WristMenu.CatButtons7)
                {
                    if (buttonInfo.buttonText == b)
                    {
                        buttonInfo.enabled = new bool?(true);
                    }
                }
                foreach (ButtonInfo buttonInfo in WristMenu.CatButtons8)
                {
                    if (buttonInfo.buttonText == b)
                    {
                        buttonInfo.enabled = new bool?(true);
                    }
                }
                foreach (ButtonInfo buttonInfo in WristMenu.CatButtons9)
                {
                    if (buttonInfo.buttonText == b)
                    {
                        buttonInfo.enabled = new bool?(true);
                    }
                }
                foreach (ButtonInfo buttonInfo in WristMenu.CatButtons10)
                {
                    if (buttonInfo.buttonText == b)
                    {
                        buttonInfo.enabled = new bool?(true);
                    }
                }
            }
            NotifiLib.SendNotification("<color=white>[</color><color=blue>LOAD</color><color=white>]</color> <color=white>Loaded Buttons Successfully!</color>");
        }
        public static void Save()
        {
            List<string> list = new List<string>();
            foreach (ButtonInfo buttonInfo in WristMenu.settingsbuttons)
            {
                bool? enabled = buttonInfo.enabled;
                bool flag = true;
                if (enabled.GetValueOrDefault() == flag & enabled != null && buttonInfo.buttonText != "Save Settings")
                {
                    list.Add(buttonInfo.buttonText);
                }
            }
            File.WriteAllLines(WristMenu.FolderName + "\\Saved_Settings.txt", list);
            string text4 = string.Concat(new string[]
            {
               change1.ToString(),
               "\n",
               change2.ToString(),
               "\n",
               change3.ToString(),
               "\n",
               change4.ToString(),
               "\n",
               change6.ToString(),
               "\n",
               change7.ToString(),
               "\n",
               change8.ToString(),
               "\n",
               change9.ToString(),
               "\n",
               change10.ToString(),
               "\n",
               change11.ToString(),
               "\n",
               change12.ToString(),
               "\n",
               change13.ToString(),
               "\n",
               change14.ToString(),
               "\n",
               change15.ToString(),
               "\n",
               change16.ToString()
            });
            File.WriteAllText(WristMenu.FolderName + "/Saved_Settings2.txt", text4.ToString());
            NotifiLib.SendNotification("<color=white>[</color><color=blue>SAVE</color><color=white>]</color> <color=white>Saved Settings Successfully!</color>");
        }
        public static void Load()
        {
            string[] array = File.ReadAllLines(WristMenu.FolderName + "\\Saved_Settings.txt");
            foreach (string b in array)
            {
                foreach (ButtonInfo buttonInfo in WristMenu.settingsbuttons)
                {
                    if (buttonInfo.buttonText == b)
                    {
                        buttonInfo.enabled = new bool?(true);
                    }
                }
            }
            try
            {
                string text3 = File.ReadAllText(WristMenu.FolderName + "/Saved_Settings2.txt");
                string[] array4 = text3.Split(new string[] { "\n" }, StringSplitOptions.None);
                change1 = int.Parse(array4[0]) - 1;
                Changeplat();
                change2 = int.Parse(array4[1]) - 1;
                Changenoti();
                change3 = int.Parse(array4[2]) - 1;
                ChangeFPS();
                change4 = int.Parse(array4[3]) - 1;
                Changedisconnect();
                change6 = int.Parse(array4[4]) - 1;
                Changemenu();
                change7 = int.Parse(array4[5]) - 1;
                Changepagebutton();
                change8 = int.Parse(array4[6]) - 1;
                ChangeOrbColor();
                change9 = int.Parse(array4[7]) - 1;
                ChangeVisualColor();
                change10 = int.Parse(array4[8]) - 1;
                ThemeChangerV1();
                change11 = int.Parse(array4[9]) - 1;
                ThemeChangerV2();
                change12 = int.Parse(array4[10]) - 1;
                ThemeChangerV3();
                change13 = int.Parse(array4[11]) - 1;
                ThemeChangerV4();
                change14 = int.Parse(array4[12]) - 1;
                ThemeChangerV5();
                change15 = int.Parse(array4[13]) - 1;
                ThemeChangerV6();
                change16 = int.Parse(array4[14]) - 1;
                ThemeChangerV7();
            }
            catch
            {
            }
            NotifiLib.SendNotification("<color=white>[</color><color=blue>LOAD</color><color=white>]</color> <color=white>Loaded settings successfully!</color>");
        }
        #endregion
        #region Platform Shit
        // sticky plats r broke still srry
        private static void PlatformsThing(bool invis, bool sticky)
        {
            if (TriggerPlats)
            {
                RPlat = WristMenu.triggerDownR;
                LPlat = WristMenu.triggerDownL;
            }
            else
            {
                RPlat = WristMenu.gripDownR;
                LPlat = WristMenu.gripDownL;
            }
            if (RPlat)
            {
                if (!once_right && jump_right_local == null)
                {
                    if (sticky)
                    {
                        jump_right_local = GameObject.CreatePrimitive(0);
                    }
                    else
                    {
                        jump_right_local = GameObject.CreatePrimitive((PrimitiveType)3);
                    }
                    if (invis)
                    {
                        Destroy(jump_right_local.GetComponent<Renderer>());
                    }
                    jump_right_local.transform.localScale = scale;
                    jump_right_local.transform.position = new Vector3(0f, -0.01f, 0f) + GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.position;
                    jump_right_local.transform.rotation = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.rotation;
                    jump_right_local.AddComponent<GorillaSurfaceOverride>().overrideIndex = jump_right_local.GetComponent<GorillaSurfaceOverride>().overrideIndex;
                    once_right = true;
                    once_right_false = false;
                    ColorChanger colorChanger1 = jump_right_local.AddComponent<ColorChanger>();
                    colorChanger1.colors = new Gradient
                    {
                        colorKeys = colorKeysPlatformMonke
                    };
                    colorChanger1.Start();
                }
            }
            else
            {
                if (!once_right_false && jump_right_local != null)
                {
                    Destroy(jump_right_local.GetComponent<Collider>());
                    Rigidbody platr = jump_right_local.AddComponent(typeof(Rigidbody)) as Rigidbody;
                    platr.velocity = GorillaLocomotion.GTPlayer.Instance.rightHandCenterVelocityTracker.GetAverageVelocity(true, 5);
                    Destroy(jump_right_local, 2.0f);
                    jump_right_local = null;
                    once_right = false;
                    once_right_false = true;
                }
            }
            if (LPlat)
            {
                if (!once_left && jump_left_local == null)
                {
                    if (sticky)
                    {
                        jump_left_local = GameObject.CreatePrimitive(0);
                    }
                    else
                    {
                        jump_left_local = GameObject.CreatePrimitive((PrimitiveType)3);
                    }
                    if (invis)
                    {
                        Destroy(jump_left_local.GetComponent<Renderer>());
                    }
                    jump_left_local.transform.localScale = scale;
                    jump_left_local.transform.position = new Vector3(0f, -0.01f, 0f) + GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.position;
                    jump_left_local.transform.rotation = GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.rotation;
                    jump_left_local.AddComponent<GorillaSurfaceOverride>().overrideIndex = jump_left_local.GetComponent<GorillaSurfaceOverride>().overrideIndex;
                    once_left = true;
                    once_left_false = false;
                    ColorChanger colorChanger2 = jump_left_local.AddComponent<ColorChanger>();
                    colorChanger2.colors = new Gradient
                    {
                        colorKeys = colorKeysPlatformMonke
                    };
                    colorChanger2.Start();
                }
            }
            else
            {
                if (!once_left_false && jump_left_local != null)
                {
                    Destroy(jump_left_local.GetComponent<Collider>());
                    Rigidbody comp = jump_left_local.AddComponent(typeof(Rigidbody)) as Rigidbody;
                    comp.velocity = GorillaLocomotion.GTPlayer.Instance.leftHandCenterVelocityTracker.GetAverageVelocity(true, 5);
                    Destroy(jump_left_local, 2.0f);
                    jump_left_local = null;
                    once_left = false;
                    once_left_false = true;
                }
            }
            if (!PhotonNetwork.InRoom)
            {
                for (int i = 0; i < jump_right_network.Length; i++)
                {
                    Destroy(jump_right_network[i]);
                }
                for (int j = 0; j < jump_left_network.Length; j++)
                {
                    Destroy(jump_left_network[j]);
                }
            }
        }
        #endregion
        #region GetButton
        public static ButtonInfo GetButton(string name)
        {
            foreach (ButtonInfo b in WristMenu.buttons)
            {
                if (b.buttonText == name)
                {
                    return b;
                }
            }
            foreach (ButtonInfo b in WristMenu.settingsbuttons)
            {
                if (b.buttonText == name)
                {
                    return b;
                }
            }
            foreach (ButtonInfo b in WristMenu.CatButtons1)
            {
                if (b.buttonText == name)
                {
                    return b;
                }
            }
            foreach (ButtonInfo b in WristMenu.CatButtons2)
            {
                if (b.buttonText == name)
                {
                    return b;
                }
            }
            foreach (ButtonInfo b in WristMenu.CatButtons3)
            {
                if (b.buttonText == name)
                {
                    return b;
                }
            }
            foreach (ButtonInfo b in WristMenu.CatButtons4)
            {
                if (b.buttonText == name)
                {
                    return b;
                }
            }
            foreach (ButtonInfo b in WristMenu.CatButtons5)
            {
                if (b.buttonText == name)
                {
                    return b;
                }
            }
            foreach (ButtonInfo b in WristMenu.CatButtons6)
            {
                if (b.buttonText == name)
                {
                    return b;
                }
            }
            foreach (ButtonInfo b in WristMenu.CatButtons7)
            {
                if (b.buttonText == name)
                {
                    return b;
                }
            }
            foreach (ButtonInfo b in WristMenu.CatButtons8)
            {
                if (b.buttonText == name)
                {
                    return b;
                }
            }
            foreach (ButtonInfo b in WristMenu.CatButtons9)
            {
                if (b.buttonText == name)
                {
                    return b;
                }
            }
            foreach (ButtonInfo b in WristMenu.CatButtons10)
            {
                if (b.buttonText == name)
                {
                    return b;
                }
            }
            return null;
        }
        #endregion
        #region Category shit
        public static void Settings()
        {
            WristMenu.settingsbuttons[0].enabled = new bool?(false);
            WristMenu.buttons[2].enabled = new bool?(false);
            inSettings = !inSettings;
            if (inSettings)
            {
                WristMenu.pageNumber = 0;
            }
            if (!inSettings)
            {
                WristMenu.pageNumber = 0;
            }
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
        }
        public static void Cat1()
        {
            WristMenu.CatButtons1[0].enabled = new bool?(false);
            WristMenu.buttons[3].enabled = new bool?(false);
            inCat1 = !inCat1;
            if (inCat1)
            {
                WristMenu.pageNumber = 0;
            }
            if (change7 == 1)
            {
                if (!inCat1)
                {
                    WristMenu.pageNumber = 1;
                }
            }
            if (change7 == 2 | change7 == 3 | change7 == 4 | change7 == 5)
            {
                if (!inCat1)
                {
                    WristMenu.pageNumber = 0;
                }
            }
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
        }
        public static void Cat2()
        {
            WristMenu.CatButtons2[0].enabled = new bool?(false);
            WristMenu.buttons[4].enabled = new bool?(false);
            inCat2 = !inCat2;
            if (inCat2)
            {
                WristMenu.pageNumber = 0;
            }
            if (change7 == 1)
            {
                if (!inCat2)
                {
                    WristMenu.pageNumber = 1;
                }
            }
            if (change7 == 2 | change7 == 3 | change7 == 4 | change7 == 5)
            {
                if (!inCat2)
                {
                    WristMenu.pageNumber = 0;
                }
            }
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
        }
        public static void Cat3()
        {
            WristMenu.CatButtons3[0].enabled = new bool?(false);
            WristMenu.buttons[5].enabled = new bool?(false);
            inCat3 = !inCat3;
            if (inCat3)
            {
                WristMenu.pageNumber = 0;
            }
            if (change7 == 1)
            {
                if (!inCat3)
                {
                    WristMenu.pageNumber = 1;
                }
            }
            if (change7 == 2 | change7 == 3 | change7 == 4 | change7 == 5)
            {
                if (!inCat3)
                {
                    WristMenu.pageNumber = 1;
                }
            }
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
        }
        public static void Cat4()
        {
            WristMenu.CatButtons4[0].enabled = new bool?(false);
            WristMenu.buttons[6].enabled = new bool?(false);
            inCat4 = !inCat4;
            if (inCat4)
            {
                WristMenu.pageNumber = 0;
            }
            if (change7 == 1)
            {
                if (!inCat4)
                {
                    WristMenu.pageNumber = 1;
                }
            }
            if (change7 == 2 | change7 == 3 | change7 == 4 | change7 == 5)
            {
                if (!inCat4)
                {
                    WristMenu.pageNumber = 1;
                }
            }
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
        }
        public static void Cat5()
        {
            WristMenu.CatButtons5[0].enabled = new bool?(false);
            WristMenu.buttons[7].enabled = new bool?(false);
            inCat5 = !inCat5;
            if (inCat5)
            {
                WristMenu.pageNumber = 0;
            }
            if (change7 == 1)
            {
                if (!inCat5)
                {
                    WristMenu.pageNumber = 2;
                }
            }
            if (change7 == 2 | change7 == 3 | change7 == 4 | change7 == 5)
            {
                if (!inCat5)
                {
                    WristMenu.pageNumber = 1;
                }
            }
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
        }
        public static void Cat6()
        {
            WristMenu.CatButtons6[0].enabled = new bool?(false);
            WristMenu.buttons[8].enabled = new bool?(false);
            inCat6 = !inCat6;
            if (inCat6)
            {
                WristMenu.pageNumber = 0;
            }
            if (change7 == 1)
            {
                if (!inCat6)
                {
                    WristMenu.pageNumber = 2;
                }
            }
            if (change7 == 2 | change7 == 3 | change7 == 4 | change7 == 5)
            {
                if (!inCat6)
                {
                    WristMenu.pageNumber = 1;
                }
            }
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
        }
        public static void Cat7()
        {
            WristMenu.CatButtons7[0].enabled = new bool?(false);
            WristMenu.buttons[9].enabled = new bool?(false);
            inCat7 = !inCat7;
            if (inCat7)
            {
                WristMenu.pageNumber = 0;
            }
            if (change7 == 1)
            {
                if (!inCat7)
                {
                    WristMenu.pageNumber = 2;
                }
            }
            if (change7 == 2 | change7 == 3 | change7 == 4 | change7 == 5)
            {
                if (!inCat7)
                {
                    WristMenu.pageNumber = 1;
                }
            }
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
        }
        public static void Cat8()
        {
            WristMenu.CatButtons8[0].enabled = new bool?(false);
            WristMenu.buttons[10].enabled = new bool?(false);
            inCat8 = !inCat8;
            if (inCat8)
            {
                WristMenu.pageNumber = 0;
            }
            if (change7 == 1)
            {
                if (!inCat8)
                {
                    WristMenu.pageNumber = 2;
                }
            }
            if (change7 == 2 | change7 == 3 | change7 == 4 | change7 == 5)
            {
                if (!inCat8)
                {
                    WristMenu.pageNumber = 1;
                }
            }
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
        }
        public static void Cat9()
        {
            WristMenu.CatButtons9[0].enabled = new bool?(false);
            WristMenu.buttons[11].enabled = new bool?(false);
            inCat9 = !inCat9;
            if (inCat9)
            {
                WristMenu.pageNumber = 0;
            }
            if (change7 == 1)
            {
                if (!inCat9)
                {
                    WristMenu.pageNumber = 2;
                }
            }
            if (change7 == 2 | change7 == 3 | change7 == 4 | change7 == 5)
            {
                if (!inCat9)
                {
                    WristMenu.pageNumber = 2;
                }
            }
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
        }
        public static void Cat10()
        {
            WristMenu.CatButtons10[0].enabled = new bool?(false);
            WristMenu.buttons[12].enabled = new bool?(false);
            inCat10 = !inCat10;
            if (inCat10)
            {
                WristMenu.pageNumber = 0;
            }
            if (change7 == 1)
            {
                if (!inCat10)
                {
                    WristMenu.pageNumber = 3;
                }
            }
            if (change7 == 2 | change7 == 3 | change7 == 4 | change7 == 5)
            {
                if (!inCat10)
                {
                    WristMenu.pageNumber = 2;
                }
            }
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
        }
        #endregion
        #region Changers
        // DO NOT MESS WITH ANY OF THE THEME CHANGERS OR CHANGERS
        public static void Changeplat()
        {
            change1++;
            if (change1 > 2)
            {
                change1 = 1;
            }
            if (change1 == 1)
            {
                TriggerPlats = false;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>PLATFORMS</color><color=white>] Enable Platforms: Grips</color>");
            }
            if (change1 == 2)
            {
                TriggerPlats = true;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>PLATFORMS</color><color=white>] Enable Platforms: Triggers</color>");
            }
        }
        public static void Changenoti()
        {
            change2++;
            if (change2 > 2)
            {
                change2 = 1;
            }
            if (change2 == 1)
            {
                NotifiLib.IsEnabled = true;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>NOTIS</color><color=white>] Notis Enabled: Yes</color>");
            }
            if (change2 == 2)
            {
                NotifiLib.SendNotification("<color=white>[</color><color=blue>NOTIS</color><color=white>] Notis Enabled: No</color>");
                NotifiLib.IsEnabled = false;
            }
        }
        public static void ChangeFPS()
        {
            change3++;
            if (change3 > 2)
            {
                change3 = 1;
            }
            if (change3 == 1)
            {
                FPSPage = false;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>FPS & PAGE COUNTER</color><color=white>] Is Enabled: No</color>");
            }
            if (change3 == 2)
            {
                FPSPage = true;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>FPS & PAGE COUNTER</color><color=white>] Is Enabled: Yes</color>");
            }
        }
        public static void Changedisconnect()
        {
            change4++;
            if (change4 > 4)
            {
                change4 = 1;
            }
            if (change4 == 1)
            {
                NotifiLib.SendNotification("<color=white>[</color><color=blue>DISCONNECT BUTTON</color><color=white>] Disconnect Location: Right Side</color>");
            }
            if (change4 == 2)
            {
                NotifiLib.SendNotification("<color=white>[</color><color=blue>DISCONNECT BUTTON</color><color=white>] Disconnect Location: Left Side</color>");
            }
            if (change4 == 3)
            {
                NotifiLib.SendNotification("<color=white>[</color><color=blue>DISCONNECT BUTTON</color><color=white>] Disconnect Location: Top</color>");
            }
            if (change4 == 4)
            {
                NotifiLib.SendNotification("<color=white>[</color><color=blue>DISCONNECT BUTTON</color><color=white>] Disconnect Location: Bottom</color>");
            }
        }
        public static void Changemenu()
        {
            change6++;
            if (change6 > 2)
            {
                change6 = 1;
            }
            if (change6 == 1)
            {
                right = false;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>MENU LOCATION</color><color=white>] Current Location: Left Hand</color>");
            }
            if (change6 == 2)
            {
                right = true;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>MENU LOCATION</color><color=white>] Current Location: Right Hand</color>");
            }
        }
        public static void Changepagebutton()
        {
            change7++;
            if (change7 > 5)
            {
                change7 = 1;
            }
            if (change7 == 1)
            {
                NotifiLib.SendNotification("<color=white>[</color><color=blue>NEXT & PREV</color><color=white>] Page Change Button Location: On Menu</color>");
            }
            if (change7 == 2)
            {
                NotifiLib.SendNotification("<color=white>[</color><color=blue>NEXT & PREV</color><color=white>] Page Change Button Location: Top</color>");
            }
            if (change7 == 3)
            {
                NotifiLib.SendNotification("<color=white>[</color><color=blue>NEXT & PREV</color><color=white>] Page Change Button Location: Sides</color>");
            }
            if (change7 == 4)
            {
                NotifiLib.SendNotification("<color=white>[</color><color=blue>NEXT & PREV</color><color=white>] Page Change Button Location: Bottom</color>");
            }
            if (change7 == 5)
            {
                NotifiLib.SendNotification("<color=white>[</color><color=blue>NEXT & PREV</color><color=white>] Page Change Button Location: Triggers</color>");
            }
        }
        public static void ChangeOrbColor()
        {
            change8++;
            if (change8 > 9)
            {
                change8 = 1;
            }
            if (change8 == 1)
            {
                CurrentGunColor = Color.blue;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Blue</color>");
            }
            if (change8 == 2)
            {
                CurrentGunColor = Color.red;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Red</color>");
            }
            if (change8 == 3)
            {
                CurrentGunColor = Color.white;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: White</color>");
            }
            if (change8 == 4)
            {
                CurrentGunColor = Color.green;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Green</color>");
            }
            if (change8 == 5)
            {
                CurrentGunColor = Color.magenta;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Magenta</color>");
            }
            if (change8 == 6)
            {
                CurrentGunColor = Color.cyan;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Cyan</color>");
            }
            if (change8 == 7)
            {
                CurrentGunColor = Color.yellow;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Yellow</color>");
            }
            if (change8 == 8)
            {
                CurrentGunColor = Color.black;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Black</color>");
            }
            if (change8 == 9)
            {
                CurrentGunColor = Color.grey;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>GUN & HAND ORB COLOR</color><color=white>] Current Color: Grey</color>");
            }
        }
        public static void ChangeVisualColor()
        {
            change9++;
            if (change9 > 9)
            {
                change9 = 1;
            }
            if (change9 == 1)
            {
                CurrentESPColor = Color.blue;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Blue</color>");
            }
            if (change9 == 2)
            {
                CurrentESPColor = Color.red;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Red</color>");
            }
            if (change9 == 3)
            {
                CurrentESPColor = Color.white;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: White</color>");
            }
            if (change9 == 4)
            {
                CurrentESPColor = Color.green;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Green</color>");
            }
            if (change9 == 5)
            {
                CurrentESPColor = Color.magenta;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Magenta</color>");
            }
            if (change9 == 6)
            {
                CurrentESPColor = Color.cyan;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Cyan</color>");
            }
            if (change9 == 7)
            {
                CurrentESPColor = Color.yellow;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Yellow</color>");
            }
            if (change9 == 8)
            {
                CurrentESPColor = Color.black;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Black</color>");
            }
            if (change9 == 9)
            {
                CurrentESPColor = Color.grey;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>ESP COLOR</color><color=white>] Current Color: Grey</color>");
            }
        }
        public static void ThemeChangerV1()
        {
            change10++;
            if (change10 > 11)
            {
                change10 = 1;
            }
            if (change10 == 1)
            {
                if (WristMenu.ChangingColors)
                {
                    RGBMenu = false;
                    WristMenu.FirstColor = Color.blue;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Blue</color>");
                }
                else
                {
                    RGBMenu = false;
                    WristMenu.NormalColor = Color.blue;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Blue</color>");
                }
            }
            if (change10 == 2)
            {
                if (WristMenu.ChangingColors)
                {
                    WristMenu.FirstColor = Color.red;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Red</color>");
                }
                else
                {
                    WristMenu.NormalColor = Color.red;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Red</color>");
                }
            }
            if (change10 == 3)
            {
                if (WristMenu.ChangingColors)
                {
                    WristMenu.FirstColor = Color.white;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: White</color>");
                }
                else
                {
                    WristMenu.NormalColor = Color.white;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: White</color>");
                }
            }
            if (change10 == 4)
            {
                if (WristMenu.ChangingColors)
                {
                    WristMenu.FirstColor = Color.green;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Green</color>");
                }
                else
                {
                    WristMenu.NormalColor = Color.green;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Green</color>");
                }
            }
            if (change10 == 5)
            {
                if (WristMenu.ChangingColors)
                {
                    WristMenu.FirstColor = Color.magenta;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Magenta</color>");
                }
                else
                {
                    WristMenu.NormalColor = Color.magenta;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Magenta</color>");
                }
            }
            if (change10 == 6)
            {
                if (WristMenu.ChangingColors)
                {
                    WristMenu.FirstColor = Color.cyan;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Cyan</color>");
                }
                else
                {
                    WristMenu.NormalColor = Color.cyan;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Cyan</color>");
                }
            }
            if (change10 == 7)
            {
                if (WristMenu.ChangingColors)
                {
                    WristMenu.FirstColor = Color.yellow;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Yellow</color>");
                }
                else
                {
                    WristMenu.NormalColor = Color.yellow;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Yellow</color>");
                }
            }
            if (change10 == 8)
            {
                if (WristMenu.ChangingColors)
                {
                    WristMenu.FirstColor = Color.black;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Black</color>");
                }
                else
                {
                    WristMenu.NormalColor = Color.black;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Black</color>");
                }
            }
            if (change10 == 9)
            {
                if (WristMenu.ChangingColors)
                {
                    WristMenu.FirstColor = Color.grey;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] First Color: Grey</color>");
                }
                else
                {
                    WristMenu.NormalColor = Color.grey;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Grey</color>");
                }
            }
            if (change10 == 10)
            {
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: Clear</color>");
            }
            if (change10 == 11)
            {
                if (WristMenu.ChangingColors)
                {
                    RGBMenu = true;
                    NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Menu Color: RGB</color>");
                }
                else
                {
                    NotifiLib.SendNotification("<color=white>[</color><color=red>ERROR</color><color=white>] Cannot Change The Menu To RGB Due To WristMenu.ChangingColors Being false</color>");
                }
            }
        }
        public static void ThemeChangerV2()
        {
            change11++;
            if (change11 > 9)
            {
                change11 = 1;
            }
            if (change11 == 1)
            {
                WristMenu.SecondColor = Color.black;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Black</color>");
            }
            if (change11 == 2)
            {
                WristMenu.SecondColor = Color.red;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Red</color>");
            }
            if (change11 == 3)
            {
                WristMenu.SecondColor = Color.white;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: White</color>");
            }
            if (change11 == 4)
            {
                WristMenu.SecondColor = Color.green;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Green</color>");
            }
            if (change11 == 5)
            {
                WristMenu.SecondColor = Color.magenta;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Magenta</color>");
            }
            if (change11 == 6)
            {
                WristMenu.SecondColor = Color.cyan;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Cyan</color>");
            }
            if (change11 == 7)
            {
                WristMenu.SecondColor = Color.yellow;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Yellow</color>");
            }
            if (change11 == 8)
            {
                WristMenu.SecondColor = Color.blue;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Blue</color>");
            }
            if (change11 == 9)
            {
                WristMenu.SecondColor = Color.grey;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Second Color: Grey</color>");
            }
        }
        public static void ThemeChangerV3()
        {
            change12++;
            if (change12 > 10)
            {
                change12 = 1;
            }
            if (change12 == 1)
            {
                WristMenu.ButtonColorDisable = Color.yellow;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Yellow</color>");
            }
            if (change12 == 2)
            {
                WristMenu.ButtonColorDisable = Color.red;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Red</color>");
            }
            if (change12 == 3)
            {
                WristMenu.ButtonColorDisable = Color.white;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: White</color>");
            }
            if (change12 == 4)
            {
                WristMenu.ButtonColorDisable = Color.green;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Green</color>");
            }
            if (change12 == 5)
            {
                WristMenu.ButtonColorDisable = Color.magenta;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Magenta</color>");
            }
            if (change12 == 6)
            {
                WristMenu.ButtonColorDisable = Color.cyan;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Cyan</color>");
            }
            if (change12 == 7)
            {
                WristMenu.ButtonColorDisable = Color.black;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Black</color>");
            }
            if (change12 == 8)
            {
                WristMenu.ButtonColorDisable = Color.blue;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Blue</color>");
            }
            if (change12 == 9)
            {
                WristMenu.ButtonColorDisable = Color.grey;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Grey</color>");
            }
            if (change12 == 10)
            {
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disable Button Color: Clear</color>");
            }
        }
        public static void ThemeChangerV4()
        {
            change13++;
            if (change13 > 10)
            {
                change13 = 1;
            }
            if (change13 == 1)
            {
                WristMenu.ButtonColorEnabled = Color.magenta;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Magenta</color>");
            }
            if (change13 == 2)
            {
                WristMenu.ButtonColorEnabled = Color.red;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Red</color>");
            }
            if (change13 == 3)
            {
                WristMenu.ButtonColorEnabled = Color.white;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: White</color>");
            }
            if (change13 == 4)
            {
                WristMenu.ButtonColorEnabled = Color.green;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Green</color>");
            }
            if (change13 == 5)
            {
                WristMenu.ButtonColorEnabled = Color.yellow;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Yellow</color>");
            }
            if (change13 == 6)
            {
                WristMenu.ButtonColorEnabled = Color.cyan;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Cyan</color>");
            }
            if (change13 == 7)
            {
                WristMenu.ButtonColorEnabled = Color.black;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Black</color>");
            }
            if (change13 == 8)
            {
                WristMenu.ButtonColorEnabled = Color.blue;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Blue</color>");
            }
            if (change13 == 9)
            {
                WristMenu.ButtonColorEnabled = Color.grey;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Grey</color>");
            }
            if (change13 == 10)
            {
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enable Button Color: Clear</color>");
            }
        }
        public static void ThemeChangerV5()
        {
            change14++;
            if (change14 > 9)
            {
                change14 = 1;
            }
            if (change14 == 1)
            {
                WristMenu.EnableTextColor = Color.black;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Black</color>");
            }
            if (change14 == 2)
            {
                WristMenu.EnableTextColor = Color.red;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Red</color>");
            }
            if (change14 == 3)
            {
                WristMenu.EnableTextColor = Color.white;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: White</color>");
            }
            if (change14 == 4)
            {
                WristMenu.EnableTextColor = Color.green;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Green</color>");
            }
            if (change14 == 5)
            {
                WristMenu.EnableTextColor = Color.yellow;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Yellow</color>");
            }
            if (change14 == 6)
            {
                WristMenu.EnableTextColor = Color.cyan;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Cyan</color>");
            }
            if (change14 == 7)
            {
                WristMenu.EnableTextColor = Color.magenta;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Magenta</color>");
            }
            if (change14 == 8)
            {
                WristMenu.EnableTextColor = Color.blue;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Blue</color>");
            }
            if (change14 == 9)
            {
                WristMenu.EnableTextColor = Color.grey;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Enabled Text Color: Grey</color>");
            }
        }
        public static void ThemeChangerV6()
        {
            change15++;
            if (change15 > 9)
            {
                change15 = 1;
            }
            if (change15 == 1)
            {
                WristMenu.DIsableTextColor = Color.black;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Black</color>");
            }
            if (change15 == 2)
            {
                WristMenu.DIsableTextColor = Color.red;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Red</color>");
            }
            if (change15 == 3)
            {
                WristMenu.DIsableTextColor = Color.white;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: White</color>");
            }
            if (change15 == 4)
            {
                WristMenu.DIsableTextColor = Color.green;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Green</color>");
            }
            if (change15 == 5)
            {
                WristMenu.DIsableTextColor = Color.yellow;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Yellow</color>");
            }
            if (change15 == 6)
            {
                WristMenu.DIsableTextColor = Color.cyan;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Cyan</color>");
            }
            if (change15 == 7)
            {
                WristMenu.DIsableTextColor = Color.magenta;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Magenta</color>");
            }
            if (change15 == 8)
            {
                WristMenu.DIsableTextColor = Color.blue;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Blue</color>");
            }
            if (change15 == 9)
            {
                WristMenu.DIsableTextColor = Color.grey;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Disabled Text Color: Grey</color>");
            }
        }
        public static void ThemeChangerV7()
        {
            change16++;
            if (change16 > 6)
            {
                change16 = 1;
            }
            if (change16 == 1)
            {
                ButtonSound = 67;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Button Sound: Normal</color>");
            }
            if (change16 == 2)
            {
                ButtonSound = 8;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Button Sound: Stump</color>");
            }
            if (change16 == 3)
            {
                ButtonSound = 203;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Button Sound: AK47</color>");
            }
            if (change16 == 4)
            {
                ButtonSound = 50;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Button Sound: Glass</color>");
            }
            if (change16 == 5)
            {
                ButtonSound = 66;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Button Sound: KeyBoard</color>");
            }
            if (change16 == 6)
            {
                ButtonSound = 114;
                NotifiLib.SendNotification("<color=white>[</color><color=blue>THEME CHANGER</color><color=white>] Button Sound: Cayon Bridge</color>"); // this sounds the best tbh
            }
        }
        #endregion
        #region GunLib
        public static void MakeGun(Color color, Vector3 pointersize, float linesize, PrimitiveType pointershape, Transform arm, bool liner, Action shit, Action shit1)
        {
            if (arm == GorillaLocomotion.GTPlayer.Instance.rightControllerTransform)
            {
                hand = WristMenu.gripDownR;
                hand1 = WristMenu.triggerDownR;
            }
            else if (arm == GorillaLocomotion.GTPlayer.Instance.leftControllerTransform)
            {
                hand = WristMenu.gripDownL;
                hand1 = WristMenu.triggerDownL;
            }
            if (hand)
            {
                Physics.Raycast(arm.position, -arm.up, out raycastHit);
                if (pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(pointershape);
                }
                pointer.transform.localScale = pointersize;
                pointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                pointer.transform.position = raycastHit.point;
                pointer.GetComponent<Renderer>().material.color = color;
                if (liner)
                {
                    GameObject g = new GameObject("Line");
                    Line = g.AddComponent<LineRenderer>();
                    Line.material.shader = Shader.Find("GUI/Text Shader");
                    Line.startWidth = linesize;
                    Line.endWidth = linesize;
                    Line.startColor = color;
                    Line.endColor = color;
                    Line.positionCount = 2;
                    Line.useWorldSpace = true;
                    Line.SetPosition(0, arm.position);
                    Line.SetPosition(1, pointer.transform.position);
                    Destroy(g, Time.deltaTime);
                }
                Destroy(pointer.GetComponent<BoxCollider>());
                Destroy(pointer.GetComponent<Rigidbody>());
                Destroy(pointer.GetComponent<Collider>());
                if (hand1)
                {
                    shit.Invoke();
                }
                else
                {
                    shit1.Invoke();
                }
            }
            else
            {
                if (pointer != null)
                {
                    Destroy(pointer, Time.deltaTime);
                }
            }
        }
        // here are some examples of how to use the gunlib
        // this example is for when u only want to execute code when holding ur trigger
        public static void ExampleOnHowToUseGunLib() // this mod is a bug gun
        {
            MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, PrimitiveType.Sphere, GorillaLocomotion.GTPlayer.Instance.rightControllerTransform, true, delegate
            {
                GameObject.Find("Floating Bug Holdable").transform.position = pointer.transform.position + new Vector3(0f, 0.25f, 0f);
            }, delegate { });
        }
        // this example is for when u want to execute code when holding ur trigger and when ur not holding trigger
        public static void ExampleOnHowToUseGunLibV2() // this mod is a rig gun
        {
            MakeGun(CurrentGunColor, new Vector3(0.15f, 0.15f, 0.15f), 0.025f, PrimitiveType.Sphere, GorillaLocomotion.GTPlayer.Instance.rightControllerTransform, true, delegate
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = pointer.transform.position + new Vector3(0f, 0.3f, 0f);
            }, delegate { GorillaTagger.Instance.offlineVRRig.enabled = true; }); // this code makes ur rig go back to normal if ur not holding ur trigger
        }
        #endregion
        #region Vars
        // category vars
        public static bool inSettings = false;
        public static bool inCat1 = false;
        public static bool inCat2 = false;
        public static bool inCat3 = false;
        public static bool inCat4 = false;
        public static bool inCat5 = false;
        public static bool inCat6 = false;
        public static bool inCat7 = false;
        public static bool inCat8 = false;
        public static bool inCat9 = false;
        public static bool inCat10 = false;
        // color vars
        public static Color CurrentGunColor = Color.blue;
        public static Color CurrentESPColor = Color.blue;
        // changers
        public static int change1 = 1;
        public static int change2 = 1;
        public static int change3 = 1;
        public static int change4 = 1;
        public static int change6 = 1;
        public static int change7 = 1;
        public static int change8 = 1;
        public static int change9 = 1;
        public static int change10 = 1;
        public static int change11 = 1;
        public static int change12 = 1;
        public static int change13 = 1;
        public static int change14 = 1;
        public static int change15 = 1;
        public static int change16 = 1;
        // rig vars
        public static bool ghostMonke = false;
        public static bool rightHand = false;
        public static bool lastHit;
        public static bool lastHit2;
        public static GameObject orb;
        public static GameObject orb2;
        // random vars
        public static bool FPSPage;
        public static bool RGBMenu;
        public static bool right;
        public static bool fps;
        public static int ButtonSound = 67;
        public static float balll435342111;
        // gun vars
        public static GameObject pointer = null;
        public static LineRenderer Line;
        public static RaycastHit raycastHit;
        public static bool hand = false;
        public static bool hand1 = false;
        // platform vars
        public static bool invisplat = false;
        public static bool invisMonke = false;
        public static bool stickyplatforms = false;
        private static Vector3 scale = new Vector3(0.0125f, 0.28f, 0.3825f);
        private static bool once_left;
        private static bool once_right;
        private static bool once_left_false;
        private static bool once_right_false;
        private static GameObject[] jump_left_network = new GameObject[9999];
        private static GameObject[] jump_right_network = new GameObject[9999];
        private static GameObject jump_left_local = null;
        private static GameObject jump_right_local = null;
        private static GradientColorKey[] colorKeysPlatformMonke = new GradientColorKey[4];
        public static bool TriggerPlats;
        public static bool RPlat;
        public static bool LPlat;
        #endregion
    }
}

