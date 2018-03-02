# Caminho.NET #

Caminho is a flexible, lightweight dialogue engine for games. 
Written in Lua, it embeds easily into your game engine of choice,
and allows you to focus on creating great content, not boilerplate code.

### Features ###

* Decouple dialogue content and logic from your game code.
* Iterate dialogue content without recompiling.
* Write dialogues with a simple syntax.
* Easily script complicated behaviors in your dialogues with Lua.
* Test and debug dialogues outside your game!

### Platforms ###

These .NET Bindings are especially designed for the Unity Engine. 
For other .NET environments, please see the Caminho.NET project instead.

To run Caminho’s Lua code, the MoonSharp interpreter (included) is used.
It strikes a good balance between interoperability and performance.


### Requirements ###

* Unity 2017.2 or Later (older versions might work)
* .NET 3.5 runtime (4.6 Experimental has not been tested yet)

### Installation ###

Download the Caminho.unitypackage file from this repository and open it in Unity.
Import all folders into your project.

### Quickstart ###

Let’s write a sample dialogue:

    “Hello, World!” 
    -> “Welcome to Caminho!” 
      -> “Have a nice day!”

#### hello.lua ####

    require('dialogue')

    local d = Dialogue:new()`

    d:sequence{
       start=true,
       name="test",
       {text="Hello, World!"},
       {text="Welcome to Caminho!"},
       {text="Have a nice day!"}
     }

    return d

Now let's run our dialogue:

    using Caminho;

    public class CaminhoDialogueRunner : MonoBehaviour
    {
        private CaminhoEngine _engine;

        // Use this for initialization
        void Start()
        {
            _engine = new CaminhoEngine();
            _engine.Initialize();

            _engine.Start("test");
            Debug.Log(_engine.Current.Text); //"Welcome to Caminho-Unity!"

            _engine.Continue();
            Debug.Log(_engine.Current.Text); //"If you're reading this then the dialogue loaded successfully."

            _engine.Continue();
            Debug.Log(_engine.Current.Text); //"Good bye!"
        }
		
		...
	}

For more information about how Caminho works, take a look at the wiki.

### License ###

Caminho and its related projects are available under the MIT license.