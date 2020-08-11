using System.IO;
using System.Reflection;
using UnityEngine;
using ModSettings;

namespace FoxCompanion
{
    internal class SnowFoxSettingsMain : JsonModSettings
    {
        [Section("General settings")]

        [Name("Rabbit chance to catch")]
        [Description("Chance to actually catch a rabbit when hunting (Default: 10%)")]
        [Slider(0, 100)]
        public int foxCatchChance = 10;

        [Name("Auto follow")]
        [Description("Fox automatically follows player after transition / spawning")]
        public bool settingAutoFollow = true;

        [Name("Aurora effects")]
        [Description("Enable it to see how the fox is affected during the aurora")]
        public bool settingAuroraFox = true;

        [Section("Customization")]

        [Name("Fox texture")]
        [Description("Change the look of your fox")]
        [Choice("Snow", "Black", "Orange", "Mane", "Zerda", "Custom 1", "Custom 2", "Custom 3")]
        public int settingTexture = 3;

        [Section("Controls")]

        [Name("Teleport")]
        [Description("Teleports fox to player, useful if fox gets stuck or lost")]
        [Choice("B", "C", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "R", "T", "U", "V", "X", "Y", "Z", "Insert", "Home", "End", "PageUp", "PageDown", "Pause", "Clear")]
        public int buttonTeleport = 12;

        [Name("Follow player / stop (toggle)")]
        [Description("Toggles following the player and stopping (stopping works for follow/goto target too")]
        [Choice("B", "C", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "R", "T", "U", "V", "X", "Y", "Z", "Insert", "Home", "End", "PageUp", "PageDown", "Pause", "Clear")]
        public int buttonFollowPlayer = 9;

        [Name("Follow target / Goto target")]
        [Description("Order to follow/goto target. Hold key down to display crosshair, release to issue command to fox")]
        [Choice("B", "C", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "R", "T", "U", "V", "X", "Y", "Z", "Insert", "Home", "End", "PageUp", "PageDown", "Pause", "Clear")]
        public int buttonFollowTarget = 10;

        [Name("Hunt rabbit")]
        [Description("Order to hunt targeted rabbit. Hold key down to display crosshair, release to issue command to fox")]
        [Choice("B", "C", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "R", "T", "U", "V", "X", "Y", "Z", "Insert", "Home", "End", "PageUp", "PageDown", "Pause", "Clear")]
        public int buttonCatchRabbit = 0;

        protected override void OnConfirm()
        {
            byte[] img;
            // Apply texture
            switch (Settings.options.settingTexture)
            {
                case 0:
                    img = System.IO.File.ReadAllBytes("Mods\\foxtures\\snow.png");
                    break;
                case 1:
                    img = System.IO.File.ReadAllBytes("Mods\\foxtures\\black.png");
                    break;
                case 2:
                    img = System.IO.File.ReadAllBytes("Mods\\foxtures\\orange.png");
                    break;
                case 3:
                    img = System.IO.File.ReadAllBytes("Mods\\foxtures\\mane.png");
                    break;
                case 4:
                    img = System.IO.File.ReadAllBytes("Mods\\foxtures\\zerda.png");
                    break;
                case 5:
                    img = System.IO.File.ReadAllBytes("Mods\\foxtures\\custom1.png");
                    break;
                case 6:
                    img = System.IO.File.ReadAllBytes("Mods\\foxtures\\custom2.png");
                    break;
                case 7:
                    img = System.IO.File.ReadAllBytes("Mods\\foxtures\\custom3.png");
                    break;
                default:
                    img = System.IO.File.ReadAllBytes("Mods\\foxtures\\snow.png");
                    break;
            }

            FoxVars.foxTexture = new Texture2D(128, 64);
            //FoxVars.foxTexture.LoadImage(FoxVars.foxTexture, img);
            ImageConversion.LoadImage(FoxVars.foxTexture, img);
            FoxVars.foxTexture.Apply();

            FoxVars.foxRenderer.material.mainTexture = FoxVars.foxTexture;
        }
        
    }

    internal static class Settings
    {
        public static SnowFoxSettingsMain options;
        public static string returnKeyValue;

        public static void OnLoad()
        {
            options = new SnowFoxSettingsMain();
            ///options.RefreshFields();
            options.AddToModSettings("Fox Settings");
        }

        public static string GetInputKeyFromString(int keyStringInt)
        {
            switch(keyStringInt)
            {
                case 0:
                    returnKeyValue = "b";
                    break;
                case 1:
                    returnKeyValue = "c";
                    break;
                case 2:
                    returnKeyValue = "f";
                    break;
                case 3:
                    returnKeyValue = "g";
                    break;
                case 4:
                    returnKeyValue = "h";
                    break;
                case 5:
                    returnKeyValue = "i";
                    break;
                case 6:
                    returnKeyValue = "j";
                    break;
                case 7:
                    returnKeyValue = "k";
                    break;
                case 8:
                    returnKeyValue = "l";
                    break;
                case 9:
                    returnKeyValue = "m";
                    break;
                case 10:
                    returnKeyValue = "n";
                    break;
                case 11:
                    returnKeyValue = "o";
                    break;
                case 12:
                    returnKeyValue = "p";
                    break;
                case 13:
                    returnKeyValue = "r";
                    break;
                case 14:
                    returnKeyValue = "t";
                    break;
                case 15:
                    returnKeyValue = "u";
                    break;
                case 16:
                    returnKeyValue = "v";
                    break;
                case 17:
                    returnKeyValue = "x";
                    break;
                case 18:
                    returnKeyValue = "y";
                    break;
                case 19:
                    returnKeyValue = "z";
                    break;
                case 20:
                    returnKeyValue = "insert";
                    break;
                case 21:
                    returnKeyValue = "home";
                    break;
                case 22:
                    returnKeyValue = "end";
                    break;
                case 23:
                    returnKeyValue = "pageup";
                    break;
                case 24:
                    returnKeyValue = "pagedown";
                    break;
                case 25:
                    returnKeyValue = "pause";
                    break;
                case 26:
                    returnKeyValue = "clear";
                    break;
                default:
                    returnKeyValue = "";
                    break;
            }

            return returnKeyValue;
        }

    }
}
