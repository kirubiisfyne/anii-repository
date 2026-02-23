# Anii - Educational Farming Simulation Game 🌾

Anii is a farming and gardening simulation game built using the Unity Engine. The game features grid-based garden building, dynamic crop growth mechanics, tool management, and an integrated quiz system. 

## 🌟 Key Features

* **Grid-Based Building System:** Place soil beds, open fields, and hanging plant structures using a customizable grid system (`BuildSystem`, `GridData`, `CellIndicator`).
* **Crop Management:** Plant, water, and harvest a variety of crops including:
  * Tomatoes
  * Eggplants
  * Squash
  * Bottle Gourds
  * Jicama (Singkamas)
  * String Beans (Sitaw)
* **Dynamic Tool System:** Switch between different farming tools like a hoe, watering can, sickle, and gardening trowel to interact with the environment (`ToolManager`, `ToolFunction`).
* **Interactive Environments:** Soil states change based on watering and crop growth stages.
* **Cosmetics & Customization:** Decorate your farm with items like scarecrows and flower boxes.
* **Educational Quiz Mini-Game:** Test your knowledge with built-in quizzes that integrate a Points and EXP system (`QuizSystem`, `PointsEXPSystem`).
* **Custom Visuals:** Utilizes the Universal Render Pipeline (URP) with custom Toon Shaders and Grid Shaders for a stylized look.

## 📁 Project Structure

The core assets of the game are organized within the `Assets/Master/` directory:

* **`/Scripts`**: Contains all the C# logic for the game.
  * `/Build Scripts`: Logic for grid placement, previews, and object data.
  * `/Crop Scripts`: Handles crop growth, planting, harvesting, and soil moisture.
  * `/Quiz Scripts`: Manages the trivia UI, quiz logic, and scoring.
* **`/Prefabs`**: Ready-to-use game objects, including mesh variants for different crop growth stages, UI text, and particle effects.
* **`/Shaders`**: Custom URP Shader Graph files used for the stylized rendering of crops and the placement grid.
* **`/Audio`**: Background music (e.g., `bgm_cuddleClouds`) and sound effects for harvesting, planting, and UI interactions.
* **`/Scenes`**: The main levels of the game, including the `TitleScreen`, `LoadingScreen`, and `SampleScene`.

## 🚀 Getting Started

### Prerequisites
* Unity Hub
* Unity Editor (Version compatible with Universal Render Pipeline / URP)

### Running the Game
1. Clone the repository to your local machine.
2. Open **Unity Hub** and click on **Add project**.
3. Navigate to the cloned folder and select it to open the project in Unity.
4. In the Unity Editor, navigate to `Assets/Scenes/`.
5. Double-click on `TitleScreen.unity` to open the main menu scene.
6. Press the **Play** button at the top of the editor to start the game.

## 🛠️ Built With

* **Unity** - Game Engine
* **Universal Render Pipeline (URP)** - Graphics and Rendering
* **C#** - Programming Language
* **TextMeshPro** - UI Typography
