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

## ⚖️ Copyright and License

**© 2026 Ukiyo Studios. All Rights Reserved.**

This software, including all original code, scripts, 3D models, textures, UI elements, and artwork, is copyrighted material. 

While the source code and assets in this repository are made available under the **MIT License** (see below), this is **not an open-source community project**. The repository serves as a public portfolio/record of the project. We are not currently accepting unsolicited pull requests, external contributions, or community development features.

### MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
