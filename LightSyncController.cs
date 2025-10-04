using System;
using LabApi.Features.Wrappers;
using UnityEngine;

namespace CustomLightBlackout
{
    public class LightBlackoutController : MonoBehaviour
    {
        public LightSourceToy lightSourceToy { get; private set; }
        public Room Room { get; private set; }
        private float FallbackIntensity { get; set; }
        private bool WasBlackout { get; set; }
        
        void Awake()
        {
            lightSourceToy = GetComponent<LightSourceToy>();
            FallbackIntensity = lightSourceToy.Intensity;
            WasBlackout = false;
            if (Room.TryGetRoomAtPosition(lightSourceToy.Transform.position, out var room))
            {
                Room = room;
            }
        }

        private void FixedUpdate()
        {
            if (Room== null || lightSourceToy == null) return;
            if (!Room.LightController.LightsEnabled)
            {
                // Turn off the light
                if (lightSourceToy.Intensity > 0f)
                {
                    lightSourceToy.Intensity = 0f;
                }
                WasBlackout = true;
            }
            else
            {
                // Turn on the light if not already
                if (WasBlackout)
                {
                    lightSourceToy.Intensity = FallbackIntensity;
                    WasBlackout = false;
                }
                else
                {
                    // Otherwise track its intensity
                    if(!Mathf.Approximately(lightSourceToy.Intensity, FallbackIntensity))
                    {
                        FallbackIntensity = lightSourceToy.Intensity;
                    }
                }
            }
        }
    }
}