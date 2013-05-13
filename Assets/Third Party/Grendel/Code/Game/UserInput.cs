using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserInput<T> : Singleton<T> where T  : MonoBehaviour
{
//    [System.Serializable]
//    public class GrendelKeyBinding
//    {
//        public string BindingName = "New Binding";
//        public KeyCode Key = KeyCode.A;
//        public KeyCode AltKey = KeyCode.B;
//        public bool Enabled = true;
//        public GrendelKeyBinding.MouseButtons MouseButton = UserInput.GrendelKeyBinding.MouseButtons.None;
//        public GrendelKeyBinding.MouseButtons AltMouseButton = UserInput.GrendelKeyBinding.MouseButtons.None;
//        public List<UserInput<T>.GrendelKeyBinding> Conflicts = new List<UserInput<T>.GrendelKeyBinding>(); //TODO: Figure out the most efficient way to update keybind conflicts
//
//        private bool mIsDown = false;
//
//        public GrendelKeyBinding(string bindingName, KeyCode key, KeyCode altKey, GrendelKeyBinding.MouseButtons mouseButton, GrendelKeyBinding.MouseButtons altMouseButton)
//        {
//            BindingName = bindingName;
//            Key = key;
//            AltKey = altKey;
//            MouseButton = mouseButton;
//            AltMouseButton = altMouseButton;
//        }
//
//        public GrendelKeyBinding(string bindingName, KeyCode key, KeyCode altKey)
//        {
//            BindingName = bindingName;
//            Key = key;
//            AltKey = altKey;
//            MouseButton = GrendelKeyBinding.MouseButtons.None;
//            AltMouseButton = GrendelKeyBinding.MouseButtons.None;
//        }
//
//        public bool IsDown
//        {
//            get
//            {
//                return mIsDown;
//            }
//            set
//            {
//                mIsDown = value;
//            }
//        }
//    }

//    public enum GrendelKeyBinding.MouseButtons
//    {
//        None = 0,
//        One = 1,
//        Two,
//        Three,
//        Four,
//        Five,
//        Six,
//    }

    public float MouseSensitivityVertical = 1f;
    public float MouseSensitivityHorizontal = 1f;

    [HideInInspector]public GrendelKeyBinding MoveUp = new GrendelKeyBinding("Move Up", KeyCode.W, KeyCode.UpArrow, GrendelKeyBinding.MouseButtons.None, GrendelKeyBinding.MouseButtons.None);
    [HideInInspector]public GrendelKeyBinding MoveDown = new GrendelKeyBinding("Move Down", KeyCode.S, KeyCode.DownArrow, GrendelKeyBinding.MouseButtons.None, GrendelKeyBinding.MouseButtons.None);
    [HideInInspector]public GrendelKeyBinding MoveLeft = new GrendelKeyBinding("Move Left", KeyCode.A, KeyCode.LeftArrow, GrendelKeyBinding.MouseButtons.None, GrendelKeyBinding.MouseButtons.None);
    [HideInInspector]public GrendelKeyBinding MoveRight = new GrendelKeyBinding("Move Right", KeyCode.D, KeyCode.RightArrow, GrendelKeyBinding.MouseButtons.None, GrendelKeyBinding.MouseButtons.None);
    [HideInInspector]public GrendelKeyBinding Jump = new GrendelKeyBinding("Jump", KeyCode.Space, KeyCode.Return, GrendelKeyBinding.MouseButtons.None, GrendelKeyBinding.MouseButtons.None);
    [HideInInspector]public GrendelKeyBinding Run = new GrendelKeyBinding("Run", KeyCode.LeftShift, KeyCode.RightShift, GrendelKeyBinding.MouseButtons.None, GrendelKeyBinding.MouseButtons.None);
    [HideInInspector]public GrendelKeyBinding PrimaryFire = new GrendelKeyBinding("Primary Fire", KeyCode.None, KeyCode.None, GrendelKeyBinding.MouseButtons.One, GrendelKeyBinding.MouseButtons.None);
    [HideInInspector]public GrendelKeyBinding SecondaryFire = new GrendelKeyBinding("Secondary Fire", KeyCode.None, KeyCode.None, GrendelKeyBinding.MouseButtons.Two, GrendelKeyBinding.MouseButtons.None);

    [HideInInspector]public List<GrendelKeyBinding> KeyBindings = new List<GrendelKeyBinding>();

    private Dictionary<KeyCode, List<GrendelKeyBinding>> mGrendelKeyBindingsDictionary = new Dictionary<KeyCode, List<GrendelKeyBinding>>();
    private Dictionary<GrendelKeyBinding.MouseButtons, List<GrendelKeyBinding>> mMouseBindingsDictionary = new Dictionary<GrendelKeyBinding.MouseButtons, List<GrendelKeyBinding>>();

    private List<GrendelKeyBinding> mKeysDown = new List<GrendelKeyBinding>();
    
    // Use this for initialization
    private void Start ()
    {
        GatherKeyBindings(this.GetType());
        StoreGrendelKeyBindings();
        mKeysDown.Clear();
    }

    //Find all the GrendelKeyBindings on UserInput
    public void GatherKeyBindings(System.Type t)
    {
        KeyBindings.Clear();

        System.Type myType = t;

        System.Reflection.FieldInfo[] myField = myType.GetFields();

        for(int i = 0; i < myField.Length; i++)
        {
            if(myField[i].FieldType == typeof(GrendelKeyBinding))
            {
                GrendelKeyBinding binding = (GrendelKeyBinding)myField[i].GetValue(this);
                if (!KeyBindings.Contains(binding))
                {
                    KeyBindings.Add(binding);
                }
            }
        }
    }

    //Store all the GrendelKeyBindings for easy referencing
    private void StoreGrendelKeyBindings()
    {
        foreach(GrendelKeyBinding binding in KeyBindings)
        {
            if (binding.Key != KeyCode.None)
            {
                if (!mGrendelKeyBindingsDictionary.ContainsKey(binding.Key))
                {
                    mGrendelKeyBindingsDictionary.Add(binding.Key, new List<GrendelKeyBinding>(){ binding } );
                }
                else
                {
                    mGrendelKeyBindingsDictionary[binding.Key].Add(binding);
                }
            }

            if (binding.AltKey != KeyCode.None)
            {
                if (!mGrendelKeyBindingsDictionary.ContainsKey(binding.AltKey))
                {
                    mGrendelKeyBindingsDictionary.Add(binding.AltKey, new List<GrendelKeyBinding>(){ binding });
                }
                else
                {
                    mGrendelKeyBindingsDictionary[binding.AltKey].Add(binding);
                }
            }

            if (binding.MouseButton != GrendelKeyBinding.MouseButtons.None)
            {
                if (!mMouseBindingsDictionary.ContainsKey(binding.MouseButton))
                {
                    mMouseBindingsDictionary.Add(binding.MouseButton, new List<GrendelKeyBinding>(){ binding });
                }
                else
                {
                    mMouseBindingsDictionary[binding.MouseButton].Add(binding);
                }
            }

            if (binding.AltMouseButton != GrendelKeyBinding.MouseButtons.None)
            {
                if (!mMouseBindingsDictionary.ContainsKey(binding.AltMouseButton))
                {
                    mMouseBindingsDictionary.Add(binding.AltMouseButton, new List<GrendelKeyBinding>(){ binding });
                }
                else
                {
                    mMouseBindingsDictionary[binding.AltMouseButton].Add(binding);
                }
            }
        }
    }
     
    // Update is called once per frame
    private void Update ()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
    
        }
    
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {

        }

        if(Input.GetKeyDown(KeyCode.Equals))
        {//
          //  AudioManager.Instance.VolumeUp();
        }

        if(Input.GetKeyDown(KeyCode.Minus))
        {
           // AudioManager.Instance.VolumeDown();
        }

        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            if(GameOptions.Instance.DebugMode){ Console.Instance.ToggleConsole(); }
        }

        foreach(GrendelKeyBinding binding in mKeysDown)
        {
            EventManager.Instance.Post(new UserInputKeyEvent(UserInputKeyEvent.TYPE.KEYHELD, binding, Vector3.zero, this));
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;

        if (e.isKey && e.keyCode != KeyCode.None)
        {
            if(e.type == EventType.KeyDown)
            {
                ProcessKeycode(e.keyCode, UserInputKeyEvent.TYPE.KEYDOWN);
            }

            if(e.type == EventType.KeyUp)
            {
                ProcessKeycode(e.keyCode, UserInputKeyEvent.TYPE.KEYUP);
            }
        }
    }

    private void ProcessKeycode(KeyCode code, UserInputKeyEvent.TYPE inputType)
    {
        if (!mGrendelKeyBindingsDictionary.ContainsKey(code))
        {
            return;
        }

        foreach(GrendelKeyBinding binding in mGrendelKeyBindingsDictionary[code])
        {
            if (binding.Enabled)
            {
                EventManager.Instance.Post(new UserInputKeyEvent(inputType, binding, Vector3.zero, this));

                if (inputType == UserInputKeyEvent.TYPE.KEYDOWN)
                {
                    binding.IsDown = true;

                    if (!mKeysDown.Contains(binding))
                    {
                        mKeysDown.Add(binding);
                    }
                }
                else if (inputType == UserInputKeyEvent.TYPE.KEYUP)
                {
                    binding.IsDown = false;

                    if (mKeysDown.Contains(binding))
                    {
                        mKeysDown.Remove(binding);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Enables or disables a binding.
    /// </summary>
    /// <param name='binding'>
    /// Binding.
    /// </param>
    /// <param name='enable'>
    /// Enable (true) / Disable (false).
    /// </param>
    public void EnableBinding(GrendelKeyBinding binding, bool enable)
    {
        if(KeyBindings.Contains(binding))
        {
                binding.Enabled = enable;
        }
    }

    /// <summary>
    /// Enables or disables several bindings.
    /// </summary>
    /// <param name='bindings'>
    /// Array of bindings.
    /// </param>
    /// <param name='enable'>
    /// Enable (true) / Disable (false).
    /// </param>
    public void EnableBindings(GrendelKeyBinding[] bindings, bool enable)
    {
        foreach(GrendelKeyBinding binding in bindings)
        {
            EnableBinding(binding, enable);
        }
    }
}
