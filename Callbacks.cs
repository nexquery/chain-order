using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CallbacksData
{
    public string CallbackName;
    public string Parameter;
    public int RetValue;

    public CallbacksData(string callbackName, string parameter, int retValue)
    {
        CallbackName = callbackName;
        Parameter = parameter;
        RetValue = retValue;
    }
}

public class Callbacks
{
    public static List<CallbacksData> calldata = new List<CallbacksData>();

    public static void Callbacks_Init()
    {
        calldata.Add(new CallbacksData("OnGameModeInit", "", 1));
        calldata.Add(new CallbacksData("OnGameModeExit", "", 1));
        calldata.Add(new CallbacksData("OnPlayerConnect", "playerid", 1));
        calldata.Add(new CallbacksData("OnPlayerDisconnect", "playerid, reason", 1));
        calldata.Add(new CallbacksData("OnPlayerStateChange", "playerid, PLAYER_STATE:newstate, PLAYER_STATE:oldstate", 1));
        calldata.Add(new CallbacksData("OnPlayerKeyStateChange", "playerid, KEY:newkeys, KEY:oldkeys", 1));
        calldata.Add(new CallbacksData("OnPlayerEnterCheckpoint", "playerid", 1));
        calldata.Add(new CallbacksData("OnPlayerLeaveCheckpoint", "playerid", 1));
        calldata.Add(new CallbacksData("OnPlayerClickTextDraw", "playerid, Text:clickedid", 0));
        calldata.Add(new CallbacksData("OnPlayerClickPlayerTextDraw", "playerid, PlayerText:playertextid", 0));
        calldata.Add(new CallbacksData("OnClickDynamicTextDraw", "playerid, Text:textid", 0));
        calldata.Add(new CallbacksData("OnClickDynamicPlayerTextDraw", "playerid, PlayerText:textid", 0));
        calldata.Add(new CallbacksData("OnCancelDynamicTextDraw", "playerid", 0));
        calldata.Add(new CallbacksData("OnVehicleSpawn", "vehicleid", 1));
        calldata.Add(new CallbacksData("OnVehicleDeath", "vehicleid, killerid", 1));
        calldata.Add(new CallbacksData("OnVehicleMod", "playerid, vehicleid, componentid", 1));
        calldata.Add(new CallbacksData("OnEnterExitModShop", "playerid, enterexit, interiorid", 1));
        calldata.Add(new CallbacksData("OnVehicleRespray", "playerid, vehicleid, color1, color2", 1));
        calldata.Add(new CallbacksData("OnVehiclePaintjob", "playerid, vehicleid, paintjobid", 1));
        calldata.Add(new CallbacksData("OnPlayerEnterVehicle", "playerid, vehicleid, ispassenger", 1));
        calldata.Add(new CallbacksData("OnPlayerExitVehicle", "playerid, vehicleid", 1));
        calldata.Add(new CallbacksData("OnVehicleSirenStateChange", "playerid, vehicleid, newstate", 1));
        calldata.Add(new CallbacksData("OnPlayerUpdate", "playerid", 1));
        calldata.Add(new CallbacksData("OnPlayerText", "playerid, text[]", 0));
        calldata.Add(new CallbacksData("OnPlayerSpawn", "playerid", 1));
        calldata.Add(new CallbacksData("OnPlayerDeath", "playerid, killerid, WEAPON:reason", 1));
        calldata.Add(new CallbacksData("OnPlayerStreamIn", "playerid, forplayerid", 1));
        calldata.Add(new CallbacksData("OnPlayerStreamOut", "playerid, forplayerid", 1));
        calldata.Add(new CallbacksData("OnPlayerInteriorChange", "playerid, newinteriorid, oldinteriorid", 1));
        calldata.Add(new CallbacksData("OnPlayerClickMap", "playerid, Float:fX, Float:fY, Float:fZ", 1));
        calldata.Add(new CallbacksData("OnPlayerClickPlayer", "playerid, clickedplayerid, source", 1));
        calldata.Add(new CallbacksData("OnPlayerWeaponShot", "playerid, WEAPON:weaponid, BULLET_HIT_TYPE:hittype, hitid, Float:fX, Float:fY, Float:fZ", 1));
        calldata.Add(new CallbacksData("OnRconCommand", "cmd[]", 0));
        calldata.Add(new CallbacksData("OnPlayerFinishedDownloading", "playerid, virtualworld", 1));

        // Streamer
        calldata.Add(new CallbacksData("OnDynamicObjectMoved", "playerid", 1));
        calldata.Add(new CallbacksData("OnPlayerEditDynamicObject", "playerid, objectid, response, Float:x, Float:y, Float:z, Float:rx, Float:ry, Float:rz", 1));
        calldata.Add(new CallbacksData("OnPlayerSelectDynamicObject", "playerid, objectid, modelid, Float:x, Float:y, Float:z", 1));
        calldata.Add(new CallbacksData("OnPlayerShootDynamicObject", "playerid, weaponid, objectid, Float:x, Float:y, Float:z", 1));
        calldata.Add(new CallbacksData("OnPlayerPickUpDynamicPickup", "playerid, pickupid", 1));
        calldata.Add(new CallbacksData("OnPlayerEnterDynamicCP", "playerid, checkpointid", 1));
        calldata.Add(new CallbacksData("OnPlayerLeaveDynamicCP", "playerid, checkpointid", 1));
        calldata.Add(new CallbacksData("OnPlayerEnterDynamicRaceCP", "playerid, checkpointid", 1));
        calldata.Add(new CallbacksData("OnPlayerLeaveDynamicRaceCP", "playerid, checkpointid", 1));
        calldata.Add(new CallbacksData("OnPlayerEnterDynamicArea", "playerid, areaid", 1));
        calldata.Add(new CallbacksData("OnPlayerLeaveDynamicArea", "playerid, areaid", 1));
        calldata.Add(new CallbacksData("OnPlayerGiveDamageDynamicActor", "playerid, actorid, Float:amount, weaponid, bodypart", 1));
        calldata.Add(new CallbacksData("OnDynamicActorStreamIn", "actorid, forplayerid", 1));
        calldata.Add(new CallbacksData("OnDynamicActorStreamOut", "actorid, forplayerid", 1));
    }
}