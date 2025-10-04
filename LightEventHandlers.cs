using CommandSystem.Commands.RemoteAdmin;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp079;
using LabApi.Events.Arguments.Scp079Events;
using LabApi.Features.Wrappers;
using Room = Exiled.API.Features.Room;

namespace CustomLightBlackout
{
    public class LightEventHandlers
    {
        public void OnRoomBlackout(RoomBlackoutEventArgs ev)
        {
            foreach(LightSourceToy light in LightSourceToy.List)
            {
                Room room = Room.Get(light.Transform.position);
                if(room == null || room != ev.Room) continue;
                if (!light.GameObject.TryGetComponent(out LightSyncController controller))
                {
                    light.GameObject.AddComponent<LightSyncController>().Init(Light.Get(light.Base));
                }
            }
        }
        
        public void OnZoneBlackout(ZoneBlackoutEventArgs ev)
        {
            foreach(LightSourceToy light in LightSourceToy.List)
            {
                Room room = Room.Get(light.Transform.position);
                if(room == null || room.Zone != ev.Zone) continue;
                if (!light.GameObject.TryGetComponent(out LightSyncController controller))
                {
                    light.GameObject.AddComponent<LightSyncController>().Init(Light.Get(light.Base));
                }
            }
        }

        public void OnTurningOffLights(TurningOffLightsEventArgs ev)
        {
            foreach(LightSourceToy light in LightSourceToy.List)
            {
                Room room = Room.Get(light.Transform.position);
                if(room == null || room != Room.Get(ev.RoomLightController)) continue;
                if (!light.GameObject.TryGetComponent(out LightSyncController controller))
                {
                    light.GameObject.AddComponent<LightSyncController>().Init(Light.Get(light.Base));
                }
            }
        }
        public void RegisterEvents()
        {
            Exiled.Events.Handlers.Map.TurningOffLights += OnTurningOffLights;
            Exiled.Events.Handlers.Scp079.RoomBlackout += OnRoomBlackout;
            Exiled.Events.Handlers.Scp079.ZoneBlackout += OnZoneBlackout;
        }
        
        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Map.TurningOffLights -= OnTurningOffLights;
            Exiled.Events.Handlers.Scp079.RoomBlackout -= OnRoomBlackout;
            Exiled.Events.Handlers.Scp079.ZoneBlackout -= OnZoneBlackout;
        }
        
        
        
    }
}