# Sync Dash - Unity Hyper-Casual Game

## Game Concept
Sync Dash is a hyper-casual game developed in Unity 2021, where the player controls a glowing cube that moves forward automatically on the right side of the screen. The player taps to jump, avoiding obstacles and collecting orbs to increase their score. The left side of the screen features a "ghost" cube that mirrors the player's actions in real-time, simulating network synchronization locally with a configurable slight lag to mimic multiplayer behavior. The game emphasizes smooth visuals, real-time state synchronization, and performance optimization.

https://github.com/user-attachments/assets/1daae294-61dd-4ed8-a3bb-1ee7d0757b8b

## Mechanics
- **Core Gameplay**: 
  - The player cube moves forward automatically.
  - Tap to jump over obstacles and collect glowing orbs.
  - The left side ghost cube syncs with the player's actions using a local data structure (ring buffer) for real-time mirroring.
  - Configurable lag on the ghost cube for a realistic network sync feel, with smooth interpolation to prevent jittery movement.
- **Visual Effects**:
  - Player and ghost cubes feature a glowing shader for a vibrant look.
  - Obstacles use a dissolve shader when hit for a polished destruction effect.
  - Collecting orbs triggers a particle burst effect.
  - Crashing triggers a combination of chromatic aberration, screen shake, and lens distortion effects for a dramatic game-over sequence.
  - Motion blur effect activates as the cube's speed increases, enhancing the sense of motion.
- **UI & Game Flow**:
  - Main menu with "Start" and "Exit" buttons.
  - Game over screen with "Restart" and "Main Menu" options.
  - Current score displayed at the top of the screen during gameplay.
- **Performance Optimization**:
  - Object pooling implemented for obstacles and collectibles to reduce instantiation overhead.
  - Optimized syncing mechanism to minimize frame drops.
  - Build size kept under 50MB for efficient mobile performance.

## Implemented Features
- **Core Gameplay**: Fully functional player controls with tap-to-jump mechanics and real-time ghost cube synchronization.
- **Visual Effects**:
  - Glowing shader applied to both player and ghost cubes.
  - Dissolve shader for obstacles when hit.
  - Particle burst effect when collecting orbs.
  - Motion blur effect tied to increasing speed.
  - Chromatic aberration, screen shake, and lens distortion effects on crash.
- **Performance**: Object pooling and optimized sync logic ensure smooth performance on mobile devices.
- **UI**: Clean and intuitive main menu, game over screen, and score display.

## How to Play
1. Launch the game and click "Start" from the main menu.
2. Tap the screen to make the player cube jump, avoiding obstacles and collecting orbs.
3. Watch the ghost cube on the left side mirror your actions with a slight, configurable delay.
4. Aim for a high score by collecting orbs while avoiding crashes.
5. Upon crashing, view the game over screen with options to restart or return to the main menu.

## Setup Instructions
1. Clone the repository from [GitHub link](https://github.com/virendra531/Sync-Dash.git).
2. Open the project in Unity 6.1 or later.
3. Build the project for your desired platform (e.g., Android for APK).
4. Alternatively, download the provided APK build from [build link](https://github.com/virendra531/Sync-Dash/releases) to test on a mobile device.



