
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace StopSoundsWhenAltTabbed
{
    public class ModConfig
    {
        // Config option.
        public bool OnlyWhenPaused { get; set; } = true;
    }
    public class ModEntry : Mod
    {
        // Store current settings to restore later.
        private float soundVol;
        private float ambientVol;
        private float footstepVol;
        private bool volumeSaved = false;
        private ModConfig Config = null!;
        private ModConfig LoadConfig()
        {
            var Config = this.Helper.ReadConfig<ModConfig>();
            return Config;
        }
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicking += this.OnUpdateTicking;
            this.Config = this.LoadConfig();
        }
        void OnUpdateTicking(object? sender, UpdateTickingEventArgs e)
        {
            if (!Context.IsGameLaunched || Game1.game1 is null|| Game1.currentSong is null){
                return;
            }
            
            
           //if (Game1.activeClickableMenu != null){
            if (Game1.activeClickableMenu is GameMenu){
                if (!volumeSaved){
                    soundVol = Game1.options.soundVolumeLevel;
                    ambientVol = Game1.options.ambientVolumeLevel;
                    footstepVol = Game1.options.footstepVolumeLevel;
                    Game1.currentSong.Pause();
                    Game1.soundCategory.SetVolume(0.0f);
                    Game1.ambientCategory.SetVolume(0.0f);
                    Game1.footstepCategory.SetVolume(0.0f);
                    volumeSaved = true;
                }

            }else{

                if (volumeSaved){
                    Game1.currentSong.Resume();
                    Game1.soundCategory.SetVolume(soundVol);
                    Game1.ambientCategory.SetVolume(ambientVol);
                    Game1.footstepCategory.SetVolume(footstepVol);
                    volumeSaved = false;
                }
            }
        }
    }
}
