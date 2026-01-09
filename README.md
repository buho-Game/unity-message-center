# Unity Message Center

A comprehensive messaging system for Unity with support for local and network messaging, state machines, singletons, and utility tools.

## Installation

### Via Unity Package Manager (Git URL)

1. Open your Unity project
2. Go to **Window > Package Manager**
3. Click the **+** button and select **Add package from git URL**
4. Enter the repository URL:
   ```
   https://github.com/yourusername/Unity-Message-Center.git
   ```
   Or use a specific version/tag:
   ```
   https://github.com/yourusername/Unity-Message-Center.git#v1.0.0
   ```

### Via Unity Package Manager (Local Path)

1. Open your Unity project
2. Go to **Window > Package Manager**
3. Click the **+** button and select **Add package from disk**
4. Navigate to the `package.json` file in this repository

### Via Manifest.json

Add the following to your `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.unity.messagecenter": "https://github.com/yourusername/Unity-Message-Center.git"
  }
}
```

## Features

- **Message System**: Type-safe messaging with support for messages with and without data
- **State Machine**: Flexible state machine implementation
- **Singletons**: Both MonoBehaviour and non-MonoBehaviour singleton patterns
- **Utility Tools**: Coroutine management, debugging tools, and math utilities
- **Network Support**: Network message types for distributed systems

## Quick Start

### Message System

#### Simple Messages (No Data)

```csharp
using UnityMessageCenter.Basic;

// Define a message
public class PlayerDiedMessage : IMessage { }

// Register a listener
MessageCenter.RegisterMessage<PlayerDiedMessage>(OnPlayerDied);

// Post a message
MessageCenter.PostMessage<PlayerDiedMessage>();

// Unregister when done
MessageCenter.UnregisterMessage<PlayerDiedMessage>(OnPlayerDied);

void OnPlayerDied()
{
    Debug.Log("Player died!");
}
```

#### Messages with Data

```csharp
using UnityMessageCenter.Basic;

// Define a message with data
public class ScoreUpdatedMessage : IMessageWithData
{
    public int NewScore { get; set; }
}

// Register a listener
MessageCenter.RegisterMessage<ScoreUpdatedMessage>(OnScoreUpdated);

// Post a message with data
var message = new ScoreUpdatedMessage { NewScore = 100 };
MessageCenter.PostMessage(message);

// Unregister when done
MessageCenter.UnregisterMessage<ScoreUpdatedMessage>(OnScoreUpdated);

void OnScoreUpdated(ScoreUpdatedMessage message)
{
    Debug.Log($"Score updated to: {message.NewScore}");
}
```

### State Machine

```csharp
using UnityMessageCenter.Basic;

public enum GameState
{
    Menu,
    Playing,
    Paused
}

public class GameStateMachine : MonoBehaviour
{
    private StateMachine<GameState> stateMachine;

    void Start()
    {
        stateMachine = new StateMachine<GameState>();
        stateMachine.AddState(GameState.Menu, new MenuState(stateMachine));
        stateMachine.AddState(GameState.Playing, new PlayingState(stateMachine));
        stateMachine.AddState(GameState.Paused, new PausedState(stateMachine));

        stateMachine.TryChangeState(GameState.Menu);
    }

    void Update()
    {
        stateMachine.OnUpdate();
    }
}

public class MenuState : State<GameState>
{
    public MenuState(StateMachine<GameState> stateMachine) : base(stateMachine) { }

    public override void OnEnter()
    {
        Debug.Log("Entered Menu");
    }

    public override bool CanChangeState(GameState state)
    {
        return state == GameState.Playing;
    }
}
```

### Singleton

#### MonoBehaviour Singleton

```csharp
using UnityMessageCenter.Basic;

public class GameManager : MonoSingleton<GameManager>
{
    protected override void Initialize()
    {
        // Initialization code
    }
}

// Usage
GameManager.Instance.SomeMethod();
```

#### Non-MonoBehaviour Singleton

```csharp
using UnityMessageCenter.Basic;

public class DataManager : Singleton<DataManager>
{
    protected override void Initialize()
    {
        // Initialization code
    }
}

// Usage
DataManager.Instance.SomeMethod();
```

### Coroutine Tool

```csharp
using UnityMessageCenter.Basic;

// Call a function after N frames
CoroutineTool.Instance.CallInFrames(5, () => {
    Debug.Log("Called after 5 frames");
});

// Call a function after N seconds
CoroutineTool.Instance.CallInSeconds(2.0f, () => {
    Debug.Log("Called after 2 seconds");
});

// Call a function at end of frame
CoroutineTool.Instance.CallInEndOfFrame(() => {
    Debug.Log("Called at end of frame");
});
```

### Debug Tool

```csharp
using UnityMessageCenter.Basic;

DebugTool.Info("This is an info message");
DebugTool.Error("This is an error message");
DebugTool.Debug("This is a debug message");
```

### Math Tools

```csharp
using UnityMessageCenter.Utility;

// Float utilities
float clamped = MathTools.FloatClamp(value, 0, 100);
float remapped = MathTools.Remap(value, new Vector2(0, 100), new Vector2(0, 1), true);

// Vector utilities
Vector2 direction = MathTools.AngleToDirection(angle);
float angle = MathTools.DirectionToAngle(direction);
Quaternion rotation = MathTools.DirectionToRotation(direction);

// List utilities
List<int> shuffled = originalList.RandomOrderList();
```

## Namespaces

- `UnityMessageCenter.Basic` - Core messaging, state machine, and singleton classes
- `UnityMessageCenter.Basic.Network` - Network message types
- `UnityMessageCenter.Utility` - Utility classes and extensions

## Requirements

- Unity 2020.3 or later

## License

[Specify your license here]

## Contributing

[Add contribution guidelines if applicable]
