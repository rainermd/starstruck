# Starstruck!
Repository for the game Starstruck. Contains the C# scripts and assets, along with documentation and design notes for its production in Unity.

The current project goal is to have a working demo, complete with:
- Introduction UI
- Intro cutscene
- Tutorial on gameplay (Scene 1)

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
Each turn, the unit may choose to move to a space within its **Movement Range**. The unit cannot move through certain terrain and heights. To move, the player selects their unit by clicking on it. It then shows the possible spaces that the player can move to. By moving the cursor, it draws the corresponding path on the grid. The player can then click on the final space to take that path, which shows the animation to get there. The player can still move so long as they still have **Movement Range** left, even after taking an **Action** (similar to D&D). Also similar to D&D, there exist **Opportunity Attacks**. If a unit moves out of an opposing unit's **Melee Range**, the unit may make one **Melee Attack**. Once the movement is completed, it cannot be undone. At the end of the movement, the unit chooses an **Orientation**, i.e., which grid axis it decides to face. 

### Actions
On each unit's turn, they can take one **Action**. The options for an **Action** are:
- **Attack**: The unit makes one attack with one of two possible **Equipped Weapons**. If there is no weapon equipped, the unit instead does a **Shove**. The unit selects a target in the **Weapon's Range** and attacks. Each weapon has its own **Base Damage**, which can be modified by the unit. The unit then has a probability of hitting. The probability of hitting is a function of the attacking units' **Proficiency Bonus**, which corresponds to their **Level** and whether the **Equipped Weapon** is favored, and the opposing units' **Speed**, **Distance From Attacker**, and **Orientation Relative to Attacker**. The higher the **Speed** and **Distance From Attacker**, the lower the probability. The more the **Orientation Relative to Attacker** is backward facing, the higher the probability is. There is also a small chance of a **Crit**, depending on the weapon and the unit.
- **Dash**: The unit doubles its **Movement Range** for one turn.
- **Dodge**: The unit makes it harder to hit until the start of its next turn.
- **Shove**: The unit pushes an adjacent unit one space.

### Object Interactions
The unit interacts with an object in its **Inventory**, or interacts with something in the environment, such as picking up an **Item** or doing a **Hack**. For the tutorial, you can find and pick up a **Fire Extinguisher**, as well as do a **Hack**. 

## Story

## VFX

## SFX
