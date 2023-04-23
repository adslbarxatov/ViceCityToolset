# Save files editor: user guide
> **ƒ** &nbsp;RD AAOW FDL; 23.04.2023; 16:12

---

- [General information](#general-information)
- [1. Starting the app and loading files](#starting-the-app-and-loading-files)
- [2. Parameters changing](#parameters-changing)
- 2.1. Basic parameters
- [2.2. Player’s parameters](#players-parameters)
- [2.3. Garages](#garages)
- [2.4. Pickups](#pickups)
- [2.5. Gangsters](#gangsters)
- [2.6. Car parks](#car-parks)
- [2.7. Stats](#stats)
- [3. Resetting of potentially hazardous parameters](#resetting-of-potentially-hazardous-parameters)
- [4. Editor library / API](#editor-library--api)

- [Download links](https://adslbarxatov.github.io/DPArray#vicecitytoolset)
- [Main page](https://adslbarxatov.github.io/ViceCityToolset)
- [Русская версия](https://adslbarxatov.github.io/ViceCityToolset/saves_ru)

---

## General information

**Save files editor** – a set of API descriptions and a tool that supports basic operations of reading and editing
of save settings of GTA Vice City.

> The application is under development now. Therefore, some parameters during usage of this application
> are edited by user at own risk. It is recommended that you back up your save files before using this tool.

---

## 1. Starting the app and loading files

When the program starts, the `File` tab is displayed. By pressing the `Open file...` button, user can select
the save file for editing. Upon successful opening, the name of save (corresponding to name of the last
completed mission) will be displayed in the middle of the tab.

At the end of all changes, you must save the file using the appropriate button (`Save to file...`). All changes
that will not be “confirmed” with this button will be lost when the program is closed. The corresponding warning
is displayed when you click the `Exit` button.

The `Reset potentially hazardous parameters` button will be described later. Before reading its description,
it is not recommended to touch it.

---

## 2. Parameters changing

### 2.1. Basic parameters

Here user can change (from top to bottom):
- Date and time recorded in the save (if necessary); valid values are from 01/01/1980 to 12/31/2080);
- Duration of the minute in the game in milliseconds (it can be convenient if the days and nights seem too long
or short; valid values are from 100 to 60’000);
- Game speed multiplier (unlike the previous parameter, this one affects the entire game, not just the clock;
valid values are from 10 to 0.1);
- Current weather (the environment in the game will change to this weather during the next gaming hour from
the start; the same weather will remain until the completion of any mission);
- Location of the camera relative to the car (there is a similar parameter for the player, but it doesn’t work
in the game);
- State of the radio wave of taxi drivers (this wave is switched on by game after completing all missions
in the taxi park. But it can be turned on earlier, although it is hardly necessary.

---

### 2.2. Player’s parameters

Here you can change (from top to bottom):
- Maximum level of health (increases by the game when passing the “Pizza deliverer” mission and when completing
all the missions; valid values are from 100 to 200);
- Maximum level of armor (increases by the game when completing the “Vigilante” mission and when completing
all missions); valid values are from 100 to 200);
- Current level of armor (manually set in the range from 0 to 900; maybe more, but have not yet been tested –
usually enough);
- Maximum level of police interest (values are from 0 to 6; rises by the game as it progresses; if the value
is zero, the player can perform any action with impunity – maniac mode);
- Account status (values are from $0 to $99’000’000; for honest players it’s suitable if you want to leave your
cash, but start the game from beginning);
- Suit / skin of the player (values from the list);
- The mode of perpetual running (player stops getting tired of running when completing an “Ambulance” mission;
you can turn it on earlier).

Flags:
- Fireproof mode (player ceases to receive damage from burning when passing the “Firefighter” mission;
you can turn it on earlier);
- Fast reload mode (set by the game when completing “The shootist” mission; you can turn it on earlier);
- Infinite ammo (ammo ceases to decrease when player passes all the missions; you can turn it on earlier,
but it’s better to turn it off to restore realism).

Three fields below:
- Type of weapon and ammo. Here you must select the weapon’s slot that should be changed, and then set its type
and ammo. Please note that the game will ignore attempts to put the weapon not in its slot (for example, a baseball
bat can be put only in weapon slot 2). The assigned slots can be viewed in the `weapon.dat` file in the last column.

The ammo is limited to 12’000 (anything greater than 10’000 is displayed as a void under the weapon’s icon).
For slots 1 (fist), 2 (cold steel / striking weapon) and 10 (special equipment), the value is ignored.

---

### 2.3. Garages

In this tab you can configure cars stored in garages purchased by the player along with the property. At the same
time, the application orientates them in all garages while saving the file so that their use is as convenient
as possible. Settings can be entered or changed even before buying a property: when it is purchased, cars will
automatically appear in the specified garages.

Before setting up a garage, you need to select it in the list at the top of the tab. Then you can specify:
- Auto model (the value `no` indicates an empty space in the garage; technically it allows putting boats, helicopters
and airplane into the garage);
- Primary and secondary color of the car (tool takes colors from `carcols.dat` file, so it is important to use the same
file that is in the folder where game is installed);
- Radio wave (allows you to turn the radio on or off in the car; in some cases, you can turn on the police wave);
- Set fire resistance (it is impossible to set fire or catch fire), protection against bullets (shots will not cause
damage), protection against damage (there will be no dents and broken glasses), protection against explosions (the car
will not catch fire from an explosion nearby); from coup, unfortunately, there is no protection in principle.
- Bomb type (only time bomb for 10 seconds and acceleration bomb are available).

Parameters can be saved to a file and transferred to another save file if necessary. In addition, current colors
of the car are displayed on the right side of the tab.

---

### 2.4. Pickups

For now this tab only allows you to change the limits of cash accumulators that appear at completion of missions related
to purchased property. Accumulators themselves become available as you progress through these missions; at the beginning
of the game, none of them can be changed.

To find out which accumulator is currently selected, you can use `Where is it` button. The maximum amount is $50’000.

---

### 2.5. Gangsters

Having previously selected the necessary gang, on this tab you can change:
- Model of car that the gang will use (or forbid them to use “their” cars);
- Skins (appearance) of gangsters (a pair of values from which the game randomly selects a skin for each generated
gangster when meeting him);
- Weapons (also a pair of values; it allows, among other things, to disarm the gang or make it “self-destructive”
by handing, for example, Molotov cocktails).

---

### 2.6. Car parks

This tab allows you to configure cars, boats, helicopters and airplanes placed around the city. These parameters can be
replaced with recommended ones (see Crazy cheater’s mod) or chosen independently using the `Download parameters` and
`Save parameters` buttons.

It is recommended to uncheck the `Cancel sorting by type` checkbox only when the program has finished adding vehicles
after missions. If the flag is unchecked, cars in the parking list will be sorted by model when saving the file. However,
technically nothing prevents you from unchecking it with every save.

After choosing the number of parking place, you can change:
- Car model (including the option `random`);
- Coordinates and rotation of the parking space (you can specify coordinates and rotation on the city map using the
`Specify coordinates` button);
- Primary and secondary colors (the value `–1` corresponds to random color from list of allowed colors for this model
in `carcols.dat`);
- Probability of an alarm (from 0 to 100%);
- Probability of blocking the car (when the doors will not open even after the explosion; value from 0 to 100%);
- Accessibility of the place (when unchecked, car will not be generated);
- The “constancy” of the place (when unchecked, car will be generated intermittently).

---

### 2.7. Stats

On this tab, you can download achievements from another save, as well as upload and see your achievements in text form.
Also in the list below are available all actions that must be completed in order to complete the game 100%.

---

## 3. Resetting of potentially hazardous parameters

This function allows you to remove all elements that may lead to crashes when further modding the game on this save
or long permanent use of this save. For now, we have found out that these elements include port cranes and objects
that replace other objects after some missions (for example, an exploded cafe in the north mall instead of a normal one).

This operation is recommended to be performed only after the full passage of the game. Otherwise, the consequences
are difficult to predict. In addition, this manipulation guarantees an unhindered (probably) change of number and / or
type of objects on the maps. Other types of modding should be checked in practice.

---

## 4. Editor library / API

Here you can also find a set of header files that represent the structure and parameters of GTA Vice City save file
(as it is known today) and file loading/saving functions that represent the order and processing
features of data blocks. It can be useful for developers that want to create an UI for loading,
editing and saving GTAVCsf.

> WARNING! It is strongly recommended to use EXACTLY THESE header files and EXACTLY THESE functions TOGETHER.
> Header definitions use direct handling with memory (unsafe mode) (implicitly defined pointers/arrays,
> structures and unions) and using them without recommended functions may cause unexpected application behaviour
> (of course, if you don’t know what you’re doing).

Information source: [gtamodding.com](http://gtamodding.com/wiki/Saves_(GTA_VC))
