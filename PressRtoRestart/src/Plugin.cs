using BepInEx;
using UnityEngine.InputSystem;

[BepInPlugin("kiki.pressr", "KIKI Press R to Restart", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            Logger.LogInfo("R PRESSED -> RESTART LEVEL");

            Game game = FindObjectOfType<Game>();
            if (game != null)
                game.RespawnPlayer();
            else
                Logger.LogWarning("Game introuvable dans la scène !");
        }
    }
}