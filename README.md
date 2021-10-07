The Long Dark - Snowfox companion mod - v0.9.9.5 "Fury, Then Silence" by Digitalzombie
===============================================================

How to install:
===============
1. Download and install "Melon Loader" by HerpDerpinstine
https://github.com/HerpDerpinstine/MelonLoader/
This creates a new folder named "Mods" in your TLD folder.

2. Extract everything from the ZIP file to your new TLD\Mods folder (overwrite old files)

3. Delete any existing "FoxCompanion.json" file in your TLD\Mods folder!!!

4. Download and install "ModSettings"-Mod by zeobviouslyfakeacc:
https://github.com/zeobviouslyfakeacc/ModSettings/releases/latest

5. Start the game! 

=============================================
=============================================

How to use:
===========
1. Fox spawns automatically 2 seconds after you get in the game or transition to a new scene 
(eg. getting indoors/outdoors)

2. You can customize options (controls etc) in the ModSettings menu found under: 
Main Menu -> Options -> Mod Settings -> Fox settings

3. Toggle follow-mode by pressing the "Follow Player / Stop (Toggle)" key on your keyboard

4. Teleport fox directly to you by pressing the "Teleport" key on your keyboard 
Useful if fox gets stuck or you lost it somewhere.

5. Enter target & command mode by pressing the "Enable command mode" button. 
Aim in the general direction of a target. A red circle appears at targeted rabbits, a green one at items to fetch, blue at food for the fox. 
Press the left mouse button to order the fox to hunt the rabbit, fetch the item or eat the food.
Press the right mouse button to cancel the command mode.


How to customize your own textures:
===========
1. Go to your TLD folder and locate the Mods\foxtures folder.

2. You can edit any texture file located there and save it as a png file with the same name, but
I recommend leaving the default textures and using the "customX.png" files instead.
You can use any picture editing program.

3. Change and apply the texture in the settings menu.


Hunger / hunting system
===============
The fox can still hunt. It never gets hungry anymore though. You can still feed it for immersion.


=============================================
=============================================

BUGS ARE TO BE EXPECTED!!!
===========================

Keep yourself up to date on the progress on:
https://www.youtube.com/channel/UCYRu_uDOzozbXIXKGrznHxQ

Or on the TLD reddit:
https://www.reddit.com/r/thelongdark/

Or on Discord:
https://discord.gg/AqpW9TjUfr

=============================================
=============================================

Changelog:
==========
0.9.9.5 "Fury, Then Silence"
- Fixed compatibility with the newest game update 1.95 "Fury, Then Silence"
- Took out fox snow pawprints until I can fix the "player has wolf tracks"-bugfixes
- Took out the hunger system of the fox. It can still hunt and eat, but you don't *need* to feed it anymore.
- Changed the input settings menu. You can now rebind to about every button, with way more comfort
- Changed the onscreen messages around. Everything should be a bit quicker and snappier now. You can still deactivate the messages in the options
- Added the a "naming" feature. You can edit the "FoxCompanionName.txt" file and rename your fox. The name will be displayed in the onscreen messages and in target mode
- Fox should now be able to eat way more food. Cooked meat/fish, some snacks and out of some cans (if they are open). May have missed a few items, will add them later


0.9.9
- Hunger / eating system
- pawprints in the snow
- integrated a fluffy option :)
- tilting on slopes
- new ground detection / fox shouldn't fall through floors anymore
- emission color on aurora effect
- option to change glow intensity
- illuminate surroundings with aurora glow
- option for intensity of light
- killed the "ghost" sphere appearing sometimes on scene load
- made the options menu background gets transparent, kills the smudge/vignette/depth of field, on setting change. so you can actually see what you are changing
- made the customization settings update in realtime
- changed target sphere to sparkly target circle. still trying to think of a way to make it more visible
- onscreen messages for different things like activating command mode
- feeding. when raw meat/fish is targetted (blue target circle) the fox will eat it
- fox keeps held items through scene transitions (eg. indoor/outdoor)
- updated assetbundle to new unity version
- made the targetting area smaller. target circle will stay at target if nothing else is targetted


0.9.8
- Working on newest update
- New targeting system for hunting / fetching
- Hunting now working!
- Fetching of items now working! May look a bit weird, depending on the item
- Smoothed the animations by quite a bit
- New customization options: fur color and aurora effect color
- Added option to disable mod. Will remove the fox on scene transition
- Added option for aurora effects: always enabled, never enabled, only during aurora
- Other small bugfixes 


0.9.5
- Refined the animation speeds a bit and removed the settings from the menu
- Smoothed the edges of the fox model just a tiny bit
- Added setting to disable the "Aurora fox""
- Rabbit hunting now working! Button configuration in the setting menu
- Setting to choose different textures for the fox
- Added setting to enable/disable automatic following after scene transition or spawn
- Changed deprecated methods for upcoming Melon Loader releases

0.9.0
- Fixed jerky animation while transitioning to different walking speeds (run / trot / walk)
- Increased the default walking speed
- Turning when following player now more lifelike
- Fixed script errors spamming the console
- Cleaned assetbundle
- Order fox to follow target / walk to target
- Customziable controls
- Customizable running / trotting / walking speeds
- Github code release
- Little surprise feature ... you need to spend some time with your fox to notice it. But you WILL notice it ;)

Known issues:
- Fox can't always find the right target when selecting terrain or far away object
- Jittery running animation when walking besides fox


0.8.0 - First release
