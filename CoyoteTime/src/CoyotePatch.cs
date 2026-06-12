using HarmonyLib;
using UnityEngine;
using UnityEngine.Events;

namespace KIKI_Coyote_Time
{
    [HarmonyPatch(typeof(PlayerController), "UpdatePlayerMovement")]
    public class CoyotePatch
    {
        private static float coyoteTimer;
        private static float coyoteDuration = 0.15f;

        [HarmonyPostfix]
        static void Postfix(PlayerController __instance)
        {
            var traverse = Traverse.Create(__instance);

            CharacterController controller =
                traverse.Field("_controller").GetValue<CharacterController>();

            if (controller == null)
                return;

            // reset timer
            if (controller.isGrounded)
                coyoteTimer = coyoteDuration;
            else
                coyoteTimer -= Time.deltaTime;

            // jump input (safe + synced)
            if (Plugin.ConsumeJump() && coyoteTimer > 0f)
            {
                float jumpForce =
                    traverse.Field("_jumpForce").GetValue<float>();

                Vector3 move =
                    traverse.Field("_moveDirection").GetValue<Vector3>();

                move.y = jumpForce;

                traverse.Field("_moveDirection").SetValue(move);

                UnityEvent onJump =
                    traverse.Field("OnJump").GetValue<UnityEvent>();

                onJump?.Invoke();

                coyoteTimer = 0f;
            }
        }
    }
}