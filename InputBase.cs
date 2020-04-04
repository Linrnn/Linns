namespace Linns
{
    using UnityEngine;
    using Serializable = System.SerializableAttribute;

    [Serializable]
    public struct InputKey
    {
        public KeyCode code;
        public KeyState state;

        public bool keyDown
        {
            get { return GetKey(KeyState.Down); }
        }
        public bool keyStay
        {
            get { return GetKey(KeyState.Stay); }
        }
        public bool keyUp
        {
            get { return GetKey(KeyState.Up); }
        }

        public InputKey(KeyCode code, KeyState state = KeyState.Down)
        {
            this.code = code;
            this.state = state;
        }
        public bool GetKey(KeyState state)
        {
            return new InputKey(code, state);
        }

        public static implicit operator bool(InputKey input)
        {
            switch (input.state)
            {
                case KeyState.Down: return Input.GetKeyDown(input.code);
                case KeyState.Stay: return Input.GetKey(input.code);
                case KeyState.Up: return Input.GetKeyUp(input.code);
                default: return false;
            }
        }
    }

    [Serializable]
    public struct InputKeys
    {
        public KeyCode[] codes;
        public KeyState state;
        public KeyMode mode;

        public InputKeys(KeyCode[] codes, KeyState state = KeyState.Down, KeyMode mode = KeyMode.Or)
        {
            this.codes = codes;
            this.state = state;
            this.mode = mode;
        }
        public static implicit operator bool(InputKeys input)
        {
            switch (input.mode)
            {
                case KeyMode.Add:
                    {
                        switch (input.state)
                        {
                            case KeyState.Down:
                                foreach (KeyCode code in input.codes)
                                {
                                    if (!Input.GetKeyDown(code)) { return false; }
                                }
                                return true;
                            case KeyState.Stay:
                                foreach (KeyCode code in input.codes)
                                {
                                    if (!Input.GetKey(code)) { return false; }
                                }
                                return true;
                            case KeyState.Up:
                                foreach (KeyCode code in input.codes)
                                {
                                    if (!Input.GetKeyUp(code)) { return false; }
                                }
                                return true;
                            default: return false;
                        }
                    }
                case KeyMode.Or:
                    {
                        switch (input.state)
                        {
                            case KeyState.Down:
                                foreach (KeyCode code in input.codes)
                                {
                                    if (Input.GetKeyDown(code)) { return true; }
                                }
                                return false;
                            case KeyState.Stay:
                                foreach (KeyCode code in input.codes)
                                {
                                    if (Input.GetKey(code)) { return true; }
                                }
                                return false;
                            case KeyState.Up:
                                foreach (KeyCode code in input.codes)
                                {
                                    if (Input.GetKeyUp(code)) { return true; }
                                }
                                return false;
                            default: return false;
                        }
                    }
                default: return false;
            }
        }
    }

    public enum KeyState
    {
        Down,
        Stay,
        Up
    }

    public enum KeyMode
    {
        Add,
        Or
    }
}