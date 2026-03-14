# Starstruck!
Repository for the game Starstruck. Contains the C# scripts and assets, along with documentation and design notes for its production in Unity.

The current project goal is to have a working demo, complete with:
- Introduction UI
- Intro cutscene
- Tutorial on gameplay (Scene 1, 2, and 3)
- Outro cutscene

Even a demo like this has a lot of parts:

## Gameplay Features
The gameplay is inspired by a mixture of [Final Fantasy Tactics: Advance](https://en.wikipedia.org/wiki/Final_Fantasy_Tactics_Advance#Gameplay), Dungeons and Dragons 5e, and [Fire Emblem: 3 Houses](https://en.wikipedia.org/wiki/Fire_Emblem:_Three_Houses), in that it is a **TRPG** (turn-based role-playing game). 

### Isometric Grids
We adopt a **2D isometric (Z as Y)** grid for battles. We keep the entire engine in 2D for simplicity, and save each tile position in a grid. We also adopt a notion of height using the (Z as Y) grid, which will be useful for more complex terrain. The grid is painted on using [Unity's tilemap feature](https://docs.unity3d.com/es/2019.4/Manual/Tilemap-Isometric-CreateIso.html). For the demo, the tile palette requires these tiles:

- Spacehship Floor (>3)
- Spaceship Wall (>10)
- Fire (animated): Units may move through, but if they start their turn inside the tile, they take heavy damage. It can also be extinguished with certain **Items** 
- Crates (>2): Can provide **Cover**, which makes hitting the unit harder. 

### Turn-Based Combat
Similar to the games mentioned, characters have **Speed**, which determines their position in turn order relative to other units. Unlike D&D, characters with a high enough speed may be able to take two turns in one round. Once the turn order is determined, each unit takes turns doing **Movement**, **Action**, and **Object Interaction**. The player controls their **Party units**, while an AI controls the **Enemy Units**. For the tutorial, the AI is very light.

### Movement
Each turn, the unit may choose to move to a space within its **Movement Range**. The unit cannot move through certain terrain and heights. To move, the player selects their unit by clicking on it. It then shows the possible spaces that the player can move to. By moving the cursor, it draws the corresponding path on the grid. The player can then click on the final space to take that path, which shows the animation to get there. The player can still move so long as they still have **Movement Range** left, even after taking an **Action** (similar to D&D). As in D&D, there are **Opportunity Attacks**. If a unit moves out of an opposing unit's **Melee Range**, the unit may make one **Melee Attack**. Once the movement is completed, it cannot be undone. At the end of the movement, the unit chooses an **Orientation**, i.e., which grid axis it decides to face. 

### Actions
On each unit's turn, they can take one **Action**. The options for an **Action** are:
- **Attack**: The unit makes one attack with one of two possible **Equipped Weapons**. If there is no weapon equipped, the unit instead does a **Shove**. The unit selects a target in the **Weapon's Range** and attacks. Each weapon has its own **Base Damage**, which can be modified by the unit. The unit then has a probability of hitting. The probability of hitting is a function of the attacking units' **Proficiency Bonus**, which corresponds to their **Level** and whether the **Equipped Weapon** is favored, and the opposing units' **Speed**, **Distance From Attacker**, and **Orientation Relative to Attacker**. The higher the **Speed** and **Distance From Attacker**, the lower the probability. The more the **Orientation Relative to Attacker** is backward facing, the higher the probability is. There is also a small chance of a **Crit**, depending on the weapon and the unit.
- **Dash**: The unit doubles its **Movement Range** for one turn.
- **Dodge**: The unit makes it harder to hit until the start of its next turn.
- **Shove**: The unit pushes an adjacent unit one space.

### Object Interactions
The unit interacts with an object in its **Inventory**, or interacts with something in the environment, such as picking up an **Item** or doing a **Hack**. For the tutorial, you can find and pick up a **Fire Extinguisher**, as well as do a **Hack**. 

### Hacking

On **Consoles**, an adjacent unit can perform a **Hack** to do certain things, like open doors. Each **Hack** is a timed puzzle. For the demo, the time is long and is a simple puzzle of finding which wire is broken and reattaching it. 

## Story

The demo story is just the introduction to a larger story. You are a criminal (unspecified as to what kind) in a far-off galaxy that is in the slow process of colliding with another galaxy. The galaxy is currently a mess of lawlessness, as an intergalactic war sprawls between the **ALLIANCE** and the **THUL**. The **ALLIANCE** is a majority human-like-based civilization, which is technically the "good guy", but suffers greatly from racism against other alien species and classism. The **THUL** are bug-like alien hive-minds with a thirst for conquering. They pilot their galaxy into others (via **DARK MATTER ENGINES**, see [dark electromagnetism](https://en.wikipedia.org/wiki/Dark_radiation)) to cannibalize it for resources. 

You start as a prisoner of the **ALLIANCE**, who is then given the choice to either rot in prison or volunteer for a mission to retrieve a data stick from the **THUL**. You learn that the **THUL** recently raided a research lab of the company **TACHYON**. The company was hired by the **ALLIANCE** government and thus holds important data for the **ALLIANCE**, which it wants back. But if they can't get it back, they will destroy the **THUL BRIG** that holds it. The **ALLIANCE** does not want to risk its own soldiers, so it uses prisoners. You choose to accept, meeting your fellow party members **KEYS** (a daredevil humanoid alien woman pilot, known for her reckless piloting and illegal black hole slingshotting), **MARCY 11** (a malfunctioning battlebot Mark 11), and **JEFF BOMBLE** (a dwarf asteroid demolitionist). You are sent to the **THUL BRIG** in a giant metal ball that crashes through and automatically forms a seal. Inside, you have limited time before the **ALLIANCE** blows up the ship. Inside is fire and alarms. You make your way through the ship to get to the computer room, grabbing the data stick. You have one final fight before escaping in an escape pod. You get an incoming transmission from the **ALLIANCE CRUISER**, asking you to identify yourselves. You then have the choice to head back to the **ALLIANCE** or to try to lose the **CRUISER** in the escape pod, where the demo ends.

## VFX
The style is pixel art, similar to that of Final Fantasy Tactics Advance, but with a sci-fi feel. 

## SFX

## Demo Features

### Main Menu
In the main menu, we have two buttons embedded in a title screen: **Play Game** and **Settings**. In the **Settings** menu, you can adjust the volume (master, music, and SFX) and mouse sensitivity. 

### Scene 1 (Movement and Hacking Tutorial)
In **Scene 1**, you have just crashed into the ship. You are instructed to move to a **Console** and **Hack** to get a door open.

### Scene 2 (Enemies, Cover, Dodge, Shove, Orientation, and Item Tutorial)
Once inside the next room, you encounter two **Thul** Guards, who immediately point their **Laser Pistols** at you. You are instructed to take **Cover** behind some crates, use the **Dodge** action, and change **Orientation** as they shoot. You are then instructed to push the guards into the fire to kill them. To then loot the bodies, you use **Object Interaction** to grab a **Fire Extinguisher** and put out the fires (which were also blocking the door). You obtain 4 **Weapons**, which you learn to equip to your 4 characters, 2 **Laser Pistols** and 2 **Laser Cutters**.

### Scene 3 (Final Boss and Opportunity Attacks)
You enter the next room, which has a **Thul Captain** and 2 **Thul Guards**, trying to escape with an **Escape Pod**. You use all the knowledge from the previous two scenes and learn about **Opportunity Attacks**, to defeat the **Thul Captain**, and obtain the **Data**. You then use the **Escape Pod** and eject before the **Thul Brig** blows up.
