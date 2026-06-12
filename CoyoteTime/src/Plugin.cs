using BepInEx;
using UnityEngine.InputSystem;
using HarmonyLib;

[BepInPlugin("kiki.coyote", "KIKI Coyote Time", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    private static bool jumpPressed;

    private void Update()
    {
        jumpPressed = Keyboard.current != null &&
                      Keyboard.current.spaceKey.wasPressedThisFrame;
    }

    public static bool ConsumeJump()
    {
        bool value = jumpPressed;
        jumpPressed = false;
        return value;
    }

    private void Awake()
    {
        new Harmony("kiki.coyote").PatchAll();
        Logger.LogInfo("Coyote mod loaded");
    }
}