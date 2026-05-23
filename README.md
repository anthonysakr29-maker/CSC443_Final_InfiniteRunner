# CSC 443 Infinite Runner Final Project

**Student:** Anthony Sakr  
**ID:** 20231456

## Project Overview

This project is a 3D infinite runner game made in Unity. The player runs through an endless lane-based environment while avoiding obstacles, collecting coins, and trying to survive as long as possible. The game uses procedural chunk spawning so the world keeps generating ahead of the player while old chunks are recycled behind them.

The project was extended beyond the starter version with menus, scoring, high score saving, collectible coins, animations, particle effects, audio, and extra player mechanics.

---

## Controls

| Action | Input |
|---|---|
| Move Left | A / Left Arrow |
| Move Right | D / Right Arrow |
| Jump | W / Up Arrow |
| Slide | S / Down Arrow |
| Pause / Resume | ESC |
| Restart | R |

---

## Main Features

### Core Infinite Runner Gameplay
- Three-lane player movement
- Jumping
- Sliding/crouching
- Endless chunk spawning
- Obstacle collision
- Game over state
- Restart support

### Score System
- Distance-based score
- Score displayed during gameplay
- Final score displayed after death

### High Score Saving
- High score is saved between runs using `PlayerPrefs`
- Main menu displays the saved high score
- Game over screen displays the current high score

### Coin System
- Collectible coins
- Coin counter during gameplay
- Total coins displayed on game over screen
- Rotating coin animation
- Coin pickup particle effect
- Coin pickup sound effect
- Randomized coin spawn patterns
- Randomized coin count per pattern
- Obstacle-aware coin spawning to reduce coins appearing inside hazards

### Menus and UI
- Main menu scene
- Animated runner background on the main menu
- Play button
- Quit button
- Pause menu
- On-screen pause button
- Game over screen
- Restart button
- Main menu button
- How To Play screen with controls display
- Options menu with music and SFX volume sliders
- Fade transitions between menu and gameplay

### Animation and Polish
- Death animation
- Jump animation
- Slide animation
- Slide animation adjusted using 3ds Max to remove unwanted forward movement
- Slide dust particle effect
- Death particle effect
- Background music
- UI button click sounds
- Death sound effect
- Jump and slide actions can interrupt each other for more responsive movement
- Mid-air slide input makes the player fall faster into a slide

### Difficulty Ramping
- The runner speed increases over time
- Maximum speed and speed increase rate are controlled through the `GameConfig` ScriptableObject

---

## Custom Additions / Extensions

The main extensions added to the base infinite runner are:

1. Pause menu with ESC and on-screen pause button (C)
2. High score saving between runs (D)
3. Main menu with animated runner background (B)
4. Coin collection system with random coin spawning 
5. Particle effects for coin pickup, death, and sliding (E)
6. Jump, slide, and death animations
7. Slide/crouch mechanic with custom obstacle support (A)
8. Audio polish for buttons, coins, music, and death
9. Multiple custom chunk prefabs created (F)
10. Speed-based difficulty tuning (G)

---


## How to Play

1. Start from the main menu.
2. Press Play.
3. Move between lanes to avoid obstacles.
4. Jump over low obstacles or onto platforms.
5. Slide under crouch obstacles.
6. Collect coins.
7. Survive as long as possible to increase your score.
8. Try to beat the saved high score.

---

## Known Bugs/Unfinished things
## Known Notes

- Some animation transitions could be further polished with more blend tuning.
- The background/environment could be expanded with more decorative scenery in future versions.
- Coin spawning not perfected, some cases of coins spawning in obstacles appear during gameplay.
---


## Links

GitHub Repository: https://github.com/anthonysakr29-maker/CSC443_Final_InfiniteRunner

Gameplay Video: https://youtu.be/ug5RZr6oHGs
