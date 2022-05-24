# Game Jam Aotearoa 2022

[Link to Jam](https://itch.io/jam/game-jam-aotearoa-2022)

_Original README from [Unity 2D Game Jam Template](https://github.com/peabnuts123/Unity-2D-Jam-Template) as follows:_

This is a project template for getting up-and-running quickly for game jams. It is configured for creating 2D games in Unity. Included are a bunch of common plugins, code and prefabs I use, a main menu and pause functionality, and some UI SFX. This is to save on overhead that would otherwise occur during the jam, setting up a new project and copying over any specific library code from other games.

The code included is all focused on automation and making code easier to write, it does not "pre-code" any features or mechanics (beyond menus).

## Contents

The project currently includes:

**Plugins**
  - [Zenject](https://github.com/modesttree/Zenject)
    - A dependency injection framework, for implementing a [composition over inheritance](https://en.wikipedia.org/wiki/Composition_over_inheritance) pattern in developing.
  - [NotNullAttribute](https://github.com/redbluegames/unity-notnullattribute)
    - A C# attribute for marking Serialized properties in the Unity inspector as required.
    - A console error is logged, and the game is prevented from running at all (not just when your GameObject enters the scene) whenever any "Not Null" fields are null.
    - Very useful for reducing errors in the Unity Editor, especially for non-programmer contributors.

**Scripts**
  - A small Easing library
    - Written by me, for adding juice to your game.
  - A bunch of utility extension methods
    - Vector extensions for fluently manipulating vectors
    - Texture extensions for performing common operations on textures
    - Rigidbody extensions for performing common physics operations on Rigidbodies and their attached colliders
  - A small Debug Drawing library
    - For drawing dots, lines etc. on the screen
  - PathStack
    - Class for easily working with FileSystem paths
    - Removes need for string operations, references to "Path separator" constants, etc.
  - PointService
    - A service for easily generating points in games
  - ThreadedCoroutine singleton
    - A singleton for easily offloading work to REAL background threads
    - Reminder: You cannot do any operations on Unity objects from a background thread
  - MasterInstaller
    - An "Installer" instance for Zenject that wires up a few simple concepts
    - Sets up Singleton reference to ThreadedCoroutine
    - Has some quick dependency injection hooks for injecting sibling components, instead of calling `GetComponent<T>()` all the time

**Prefabs**
  - _SceneContext
    - An object to add to your scene to pre-configure Zenject stuff

**Audio**
  - [Kenney.nl](https://kenney.nl/)'s superb [UI Audio](https://www.kenney.nl/assets/ui-audio) SFX pack, licensed under [Creative Commons Zero](http://creativecommons.org/publicdomain/zero/1.0/)

**Scenes**
  - Game.unity
    - Basic scene set up and ready to go for development, contains a _SceneContext instance and things.
  - Main Menu.unity
    - A sample Menu for playing your game. Has a screen for displaying a simple list of credits, and an "Exit" option.

## Folder Structure

The project has the following structure:
```sh
  Assets/
  ├─── Audio/
  │    # files and music
  │
  ├─── Fonts/
  │    # Custom fonts
  │
  ├─── Materials/
  │    # Physics or Visual materials
  │
  ├─── Images/
  │    # Image assets, textures, etc.
  │
  ├─── Plugins/
  │    # Any kind of addon to Unity
  │
  ├─── Prefabs/
  │    # Prefabs
  │
  ├─── Scenes/
  │    # Scenes
  │
  └─┬─ Scripts/
    │  # Your code
    │
    ├─── Components/
    │    # Scripts for extending the functionality of GameObjects
    │    #   e.g. "PlayerHoldable" etc.
    │
    ├─── Config
    │    # Configuration scripts e.g. Zenject Installers, Constants
    │
    ├─── Entities
    │    # Scripts specific to certain objects in your game
    │    #   e.g. "PlayerController", etc.
    │
    ├─── Singletons
    │    # Singleton objects
    │
    └─── Util
         # All sorts of raw coding logic that exists as
         #   a utility for other code
```


## Backlog / @TODO

  - Goal item
    - God rays
    - Don't immediately pause the game
    - Go to next level
  - Time mechanic
    - UI to show current time
    - Swap out tiles at night
  - Shark
    - Use new animations
    - Different animations at night
    - Attack move
    - Have health / die
  - SFX
    - Shark move
    - Shark jump
    - Shark take damage
    - Ratboy attack
    - Ghostcat attack
    - Ghostcat aggro
    - Collect coin
  - Music
    - @TODO
  - RatBoy
    - Fix bugs
    - Get hit from below, don't yeet
    - Drop coins on die
  - GhostCat
    - Enemies drop coins when killed
    - Touching an enemy takes damage, blinks
  - Coin item
    - Actually collect coin into a total
    - Display coin total on screen
    - Use new animation
  - Key item
    - Shark hold key item
  - Locked door block
    - Touching locked door while holding key unlocks
    - Drop 2 halves of door as physics items for a second
  - Main menu
    - Instructions / art / how to play
    - Credits screen
    - Level select / unlocking
  - UI
    - Num coins
    - Hearts / Health
    - Time of day
  - Power loops
    - Spend coins on main menu
    - Vanity items