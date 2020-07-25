using System.IO;
using System.Reflection;
using ModSettings;

namespace FoxCompanion
{
    internal class SnowFoxSettingsMain : JsonModSettings
    {   
        [Section("Speed")]

        [Name("Running speed")]
        [Description("Adjust the speed when running (Default: 1.8)")]
        [Slider(1f, 3f, 1)]
        public float foxRunningSpeedSlider = 1.8f;

        [Name("Trotting speed")]
        [Description("Adjust the speed when trotting (Default: 1.5)")]
        [Slider(1f, 3f, 1)]
        public float foxTrottingSpeedSlider = 1.5f;

        [Name("Walking speed")]
        [Description("Adjust the speed when walking (Default: 1.3)")]
        [Slider(1f, 3f, 1)]
        public float foxWalkingSpeedSlider = 1.3f;

        [Section("Distance")]

        [Name("Stop distance")]
        [Description("Distance to target/player at which the fox stops (Default: 3.0)")]
        [Slider(0.1f, 4.9f, 1)]
        public float foxStopDistanceSlider = 3f;

        [Section("Control buttons")]

        [Name("Teleport")]
        [Description("Teleports fox to player, useful if fox gets stuck or lost")]
        [Choice("B", "C", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "R", "T", "U", "V", "X", "Y", "Z", "Insert", "Home", "End", "PageUp", "PageDown", "Pause", "Clear")]
        public int buttonTeleport = 12;

        [Name("Follow player / stop (toggle)")]
        [Description("Toggles following the player and stoping (stopping works for follow/goto target too")]
        [Choice("B", "C", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "R", "T", "U", "V", "X", "Y", "Z", "Insert", "Home", "End", "PageUp", "PageDown", "Pause", "Clear")]
        public int buttonFollowPlayer = 9;

        [Name("Follow target / Goto target")]
        [Description("Order to follow/goto target. Hold key down to display crosshair, release to issue command to fox")]
        [Choice("B", "C", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "R", "T", "U", "V", "X", "Y", "Z", "Insert", "Home", "End", "PageUp", "PageDown", "Pause", "Clear")]
        public int buttonFollowTarget = 10;

        protected override void OnChange(FieldInfo field, object oldValue, object newValue)
        {
            /*if (field.Name == nameof(advDecay) || field.Name == nameof(advFoodDecay) || field.Name == nameof(advOnUseDecay))
            {
                RefreshFields();
            }*/
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
