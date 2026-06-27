<div align="center">

# 🌾 Anii! Grow To Know

**An educational farming simulation game built with Unity.**  
Plant, grow, and harvest crops on a grid-based farm while testing your agricultural knowledge through interactive quizzes.

[![Unity](https://img.shields.io/badge/Unity-2021%2B-black?logo=unity&logoColor=white)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-10.0-239120?logo=csharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![URP](https://img.shields.io/badge/Rendering-URP-orange?logo=unity&logoColor=white)](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest)
[![TextMeshPro](https://img.shields.io/badge/UI-TextMeshPro-blue)](https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest)

</div>

---

## ✨ Features

- 🏗️ **Grid-Based Building System** — place soil beds, open fields, and hanging plant structures using a customizable grid system (`BuildSystem`, `GridData`, `CellIndicator`)
- 🌱 **Crop Management** — plant, water, and harvest a variety of crops across multiple growth stages
- 🔧 **Dynamic Tool System** — switch between farming tools like a hoe, watering can, sickle, and gardening trowel to interact with the environment (`ToolManager`, `ToolFunction`)
- 💧 **Interactive Environments** — soil states change dynamically based on watering and crop growth stages
- 🎨 **Cosmetics & Customization** — decorate your farm with items like scarecrows and flower boxes
- 🧠 **Educational Quiz Mini-Game** — test your knowledge with built-in quizzes integrated with a Points and EXP system (`QuizSystem`, `PointsEXPSystem`)
- ✨ **Custom Visuals** — stylized look powered by URP with custom Toon Shaders and Grid Shaders

---

## 🚀 Quick Start

### Prerequisites

- **Unity Hub**
- **Unity Editor** (version compatible with [Universal Render Pipeline / URP](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest))

### Running the Game

```bash
# 1. Clone the repository
git clone https://github.com/your-username/anii-repository.git

# 2. Open Unity Hub → click "Add Project" → select the cloned folder

# 3. In the Unity Editor, navigate to Assets/Scenes/

# 4. Double-click TitleScreen.unity to open the main menu scene

# 5. Press the Play ▶️ button at the top of the editor
```

---

## 📁 Project Structure

The core assets are organized within the `Assets/Master/` directory:

```
Assets/Master/
├── Scripts/
│   ├── Build Scripts/       ← Grid placement, previews, and object data
│   ├── Crop Scripts/        ← Crop growth, planting, harvesting, and soil moisture
│   └── Quiz Scripts/        ← Trivia UI, quiz logic, and scoring
├── Prefabs/                 ← Mesh variants for growth stages, UI text, particles
├── Shaders/                 ← Custom URP Shader Graph files (Toon, Grid)
├── Audio/                   ← BGM (e.g. bgm_cuddleClouds) and SFX
└── Scenes/                  ← TitleScreen, LoadingScreen, SampleScene
```

---

## 🌱 Crops

| Crop | Local Name | Description |
|---|---|---|
| 🍅 Tomato | Kamatis | Classic garden staple with multiple growth stages |
| 🍆 Eggplant | Talong | Versatile vegetable commonly grown in local farms |
| 🎃 Squash | Kalabasa | Hearty, sprawling crop |
| 🍶 Bottle Gourd | Upo | Traditional gourd used in Filipino cuisine |
| 🥔 Jicama | Singkamas | Crunchy root vegetable |
| 🫘 String Beans | Sitaw | Climbing legume, a kitchen staple |

---

## 🔧 Tools

| Tool | Function |
|---|---|
| 🪓 Hoe | Prepare and till soil for planting |
| 💧 Watering Can | Water crops to promote growth through stages |
| 🔪 Sickle | Harvest mature crops ready for picking |
| 🌿 Gardening Trowel | Fine garden work and detailed interaction |

---

## 🛠️ Built With

| Technology | Purpose |
|---|---|
| [Unity](https://unity.com/) | Game Engine |
| [Universal Render Pipeline (URP)](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest) | Graphics & Rendering |
| [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) | Programming Language |
| [TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest) | UI Typography |
