using System;
using Exiled.API.Features;
using LabApi.Features.Wrappers;
using MEC;
using Mirror;
using UnityEngine;
using Light = Exiled.API.Features.Toys.Light;
using Room = Exiled.API.Features.Room;

namespace CustomLightBlackout
{
    public class LightSyncController : MonoBehaviour
    {
        public Light Light { get; private set; }
        public Room Room => Room.Get(Light.Transform.position);
        private float FallbackIntensity { get; set; } = 1f;
        private bool WasBlackout { get; set; } = false;

        public void Init(Light lightSourceToy)
        {
            this.Light = lightSourceToy;
            FallbackIntensity = lightSourceToy.Intensity;
        }

        private void FixedUpdate()
        {
            if (!Room || Light == null) return;
            if (Room.RoomLightController && !Room.RoomLightController.LightsEnabled)
            {
                // Turn off the light
                if (WasBlackout) return; // Already off
                FallbackIntensity = Light.Intensity;
                Light.Intensity = 0f;
                WasBlackout = true;
            }
            else
            {
                // Turn on the light if not already
                if (WasBlackout)
                {
                    Light.UnSpawn();
                    Timing.CallDelayed(Timing.WaitForOneFrame, () =>
                    {
                        Light.Intensity = FallbackIntensity;
                        Light.Spawn();
                        Destroy(this);
                    });
                    WasBlackout = false;
                    
                }
            }
        }
    }
}