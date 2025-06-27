using CapuchinCosmetics;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using System;

[assembly: MelonInfo(typeof(Init), ModInfo.Name, ModInfo.Version, ModInfo.Author)]
[assembly: MelonGame("Duttbust", "Capuchin")]
public class Init : MelonMod
{
    // I wouldn't recommend modifying this Init class as it can be hard to understand for new modders.
    // If you're experienced then probably ignore these comments as they're mostly here to guide new modders.
    public static Init instance;

    // as we dont need to patch harmony as this is melon loader not hitlers soul pancakes --sonakrie
    //public Harmony harmonyInstance;

    [Obsolete]
    //gosh the init instance fuckin sucks --sonakrie
    public override void OnApplicationLateStart()
    {
        //harmonyInstance = HarmonyPatcher.Patch(ModInfo.GUID);
        instance = this;

        // If and only IF you're making custom MonoBehaviour's do this:
        // ClassInjector.RegisterTypeInIl2Cpp<CustomMonoBehaviour>();
        // If you don't do this, your MonoBehaviour will not be recognized by the game.

        ClassInjector.RegisterTypeInIl2Cpp<CosmeticLoader>();
        ClassInjector.RegisterTypeInIl2Cpp<WardrobeHandler>();
        ClassInjector.RegisterTypeInIl2Cpp<ButtonHandler>();
        ClassInjector.RegisterTypeInIl2Cpp<CustomEnableDisable>();
        ClassInjector.RegisterTypeInIl2Cpp<CustomPageSwitch>();
        ClassInjector.RegisterTypeInIl2Cpp<ShopWardrobeHandler>();
        ClassInjector.RegisterTypeInIl2Cpp<ShopButtonHandler>();
        ClassInjector.RegisterTypeInIl2Cpp<CustomPageSwitchShop>();
        ClassInjector.RegisterTypeInIl2Cpp<Plugin>();
    }

    public override void OnApplicationQuit()
    {
        // just saying but i deleted the harmony stuff as above --sonakrie
        MelonLogger.Msg("Good bye !");
    }
}