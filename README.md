# Local Multiplayer Game

## Overview
This Unity project is a test assignment implementing a local multiplayer game with a "Host-Client" architecture. The game features a simple scene where enemies spawn and attack all players present. Multiple game instances can run on a single PC, with one acting as the host (server + player) and others connecting as clients.

The project demonstrates proficiency in Unity, C#, and Netcode for GameObjects, focusing on networked gameplay, enemy AI, and synchronized interactions. All calculations are executed on the server side and synchronized with other players.

## Features
- **Local Multiplayer**: Supports multiple players on a single PC, each running in a separate game window.
- **Host-Client Architecture**: One game instance acts as the host (server + player), while others connect as clients.
- **Player Mechanics**:
  - Basic movement using WASD keys.
  - Top-down third-person camera following the player.
  - Health system (100 HP by default).
- **Enemy AI**:
  - Enemies spawn periodically at designated spawn points. When an enemy leaves a spawn point, a new enemy spawns after 5 seconds.
  - Enemies shoot projectiles every 3 seconds, dealing damage to players on hit.
  - All AI logic runs on the host, with states synchronized to clients.
- **Projectiles**:
  - Deal 10 damage to players on hit.
  - Destroyed upon colliding with another collider.
- **Networked Objects**: Players, enemies, enemy spawners, and projectiles are NetworkObjects, ensuring proper synchronization across clients.
- **Simple UI**: Buttons for "Host," "Client," and "Shutdown" to manage network sessions.
- **Scene Setup**:
  - A plane as the ground.
  - Cube obstacles for navigation.
  - Baked NavMesh for enemy pathfinding.

## Technology Stack
- **Unity**: 6000.0.42f1 LTS
- **Netcode for GameObjects**: Unity's networking library for multiplayer functionality
- **C#**: Core programming language for gameplay and network logic
- **Unity Editor**: For scene creation, prefab setup, and NavMesh baking
- **Cinemachine**: For camera setup and object following
- **New Input System**: Provides input for player WASD movement
- **AI Navigation**: For baking navigation surfaces
- **Unity Multiplayer Play Mode**: For testing multiplayer in the editor, speeding up development
- **Unity Physics**: For handling collision and overlap events

## Setup Instructions
1. **Clone the Repository**:
   ```bash
   git clone <repository-url>
   ```
2. **Open in Unity**:
   - Open Unity Hub and add the project folder.
   - Ensure the Unity version is 6000.0.42f1 or compatible.
3. **Configure Build Settings**:
   - Set the platform to **Windows Standalone** in Build Settings.
4. **Run the Game**:
   - Build and run one instance as the **Host** using the "Host" button in the UI.
   - Build and run additional instances as **Clients** using the "Client" button.
   - Use the "Shutdown" button to stop the network session.
5. **Play**:
   - Control players with WASD keys.
   - Avoid enemy projectiles and navigate around obstacles.

## Implemented Mechanics
- **Networking**:
  - Host-Client model using Netcode for GameObjects.
  - NetworkObject and NetworkBehaviour for synchronized entities.
  - Server RPCs for authoritative enemy and projectile logic.
  - NetworkVariable for synchronizing player health and enemy states.
- **Player**:
  - Smooth WASD movement with a following camera.
  - Health system with damage taken from enemy projectiles.
- **Enemies**:
  - Periodic spawning on the host.
  - Detection range for spotting players.
  - Chasing the target player and switching to the closest player.
  - Timed projectile attacks.
- **Projectiles**:
  - Spawned on the host and synchronized to clients.
  - Deal damage to players on collision.
  - Destroyed upon hitting another collider.
- **Scene**:
  - Simple environment with a plane and cube obstacles.
  - NavMesh for enemy pathfinding.
  - Enemy spawners.

## Gameplay Video
<a href="https://youtu.be/087zlQdqj7o">
<image scr="https://github.com/Just0Simon/LocalMultiplayer/blob/main/Preview.jpg" wight=100% alt="Local Multiplayer Demo Preview">
</a>

## Notes
- The project uses basic geometric shapes (capsules, cubes, cylinders), focusing on functionality over visuals.
- The .gitignore file is configured to exclude Unity-specific temporary files, builds, and the .idea folder (Rider IDE cache).
- The code follows C# and Unity conventions with a clear structure and minimal comments where the logic is self-explanatory.
- All critical logic is server-authoritative to ensure consistency across clients.

## Future Improvements
- Add visual feedback for player damage (e.g., a UI health bar).
- Enhance the UI with connection status indicators.
- Add basic sound effects for shooting and damage.
- Destroy projectiles after a set time if they do not collide with anything.

## License
This project is for demonstration purposes only and is not licensed for commercial use.
