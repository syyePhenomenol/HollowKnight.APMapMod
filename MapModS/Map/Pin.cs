﻿using GlobalEnums;
using System;
using UnityEngine;
using MapModS.Data;
using MapModS.Settings;

namespace MapModS.Map
{
    internal class Pin : MonoBehaviour
    {
        public PinDef PinData { get; private set; } = null;
        private SpriteRenderer _SR => gameObject.GetComponent<SpriteRenderer>();

        public void SetPinData(PinDef pd)
        {
            PinData = pd;
        }

        public void UpdatePin(MapZone mapZone)
        {
            if (PinData == null)
            {
                throw new Exception("Cannot enable pin with null pindata. Ensure game object is disabled before adding as component, then call SetPinData(<pd>) before enabling.");
            }

            try
            {
                ShowBasedOnMap(mapZone);
                //HideIfNotBought();
                HideIfFound();
            }
            catch (Exception e)
            {
                MapModS.Instance.LogError(message: $"Failed to update pin! ID: {PinData.name}\n{e}");
            }
        }

        // Hides or shows the pin depending on the state of the map (NONE is World Map)
        private void ShowBasedOnMap(MapZone mapZone)
        {
            if (mapZone == PinData.mapZone || mapZone == MapZone.NONE)
            {
                gameObject.SetActive(true);

                //// Show everything if full map was revealed
                //// Or, if it's a Map, always show the pin
                //if (MapModS.LS.RevealFullMap || PinData.vanillaPool == Pool.Map)
                //{
                //    gameObject.SetActive(true);
                //    return;
                //}

                //// Show these pins if the corresponding map item has been picked up
                //if (SettingsUtil.GetMMSMapSetting(PinData.mapZone))
                //{
                //    if (PinData.vanillaPool == Pool.Skill
                //    || PinData.vanillaPool == Pool.Charm
                //    || PinData.vanillaPool == Pool.Key
                //    || PinData.vanillaPool == Pool.Notch
                //    || PinData.vanillaPool == Pool.Mask
                //    || PinData.vanillaPool == Pool.Vessel
                //    || PinData.vanillaPool == Pool.Ore
                //    || PinData.vanillaPool == Pool.EssenceBoss)
                //    {
                //        gameObject.SetActive(true);
                //        return;
                //    }

                //    // Only show the rest if the corresponding scene/room has been mapped
                //    if (PinData.pinScene != null)
                //    {
                //        if (PlayerData.instance.scenesMapped.Contains(PinData.pinScene))
                //        {
                //            gameObject.SetActive(true);
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        if (PlayerData.instance.scenesMapped.Contains(PinData.sceneName))
                //        {
                //            gameObject.SetActive(true);
                //            return;
                //        }
                //    }
                //}
            }

            gameObject.SetActive(false);
        }

        //private void HideIfNotBought()
        //{
        //    if (!MapModS.LS.GetHasFromGroup(PinData.vanillaPool) && !MapModS.LS.RevealFullMap)
        //    {
        //        gameObject.SetActive(false);
        //    }
        //}

        private void HideIfFound()
        {
            //if (PinData.vanillaObjectName == null) return;

            // Don't hide pin if something isn't in the obtained items dictionary
            if (!MapModS.LS.ObtainedVanillaItems.ContainsKey(PinData.objectName + PinData.sceneName)) return;

            gameObject.SetActive(false);
        }
    }
}