using System;
using Exiled.Loader.Models;

namespace CustomLightBlackout
{
    public class Plugin : Exiled.API.Features.Plugin<Config>
    {
            public static Plugin Instance { get; private set; }
            public LightEventHandlers LightEventHandlers { get; private set; }
            
            
            public override void OnEnabled()
            {
                Instance = this;
                LightEventHandlers = new LightEventHandlers();
                LightEventHandlers.RegisterEvents();
            }

            public override void OnDisabled()
            {
                if (LightEventHandlers != null)
                {
                    LightEventHandlers.UnregisterEvents();
                    LightEventHandlers = null;
                }
                Instance = null;
            }

            public override string Name { get; } = "CustomLightBlackout";
            public override string Author { get; } = "TayTay";
            public override Version Version { get; } = new Version(1, 0, 0);
            public override Version RequiredExiledVersion { get; } = new Version(9, 9, 2);
    }
}