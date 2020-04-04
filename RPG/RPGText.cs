namespace Linns.RPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using IEnumerator = System.Collections.IEnumerator;
    using UnityEvent = UnityEngine.Events.UnityEvent;
    using Text = UnityEngine.UI.Text;
    using Convert = Convert;

    [AddComponentMenu("Linns/RPG/RPG Text")]
    public class RPGText : SecurityCoroutine
    {
        [Space]
        [SerializeField]
        protected int m_nextIndex;
        public string textSymbol = "\n";
        public DisplayMode displayMode;
        [SerializeField]
        protected TextAsset m_textAsset;
        public Text textUI;
        public List<TextEvent> textEvents = new List<TextEvent>();
        public List<string> textList = new List<string>();

        public int nextIndex
        {
            get { return Mathf.Clamp(m_nextIndex, 0, length - 1); }
            set { m_nextIndex = Mathf.Clamp(value, 0, length - 1); }
        }
        public virtual TextAsset textAsset
        {
            get { return m_textAsset; }
            set
            {
                m_textAsset = value;
                SetTextList(textSymbol);
                textEvents = new List<TextEvent>();
                ShowNextString(true);
            }
        }
        public int length
        {
            get { return textList.Count; }
        }
        public string textString
        {
            get;
            protected set;
        }

        protected new void OnDestroy()
        {
            base.OnDestroy();
            textEvents.Clear();
            textList.Clear();
    }
        public int GetIndex()
        {
            return textList.IndexOf(textString);
        }
        public string GetString()
        {
            return textString;
        }
        public bool IsBoundsIn(int i, bool isIn = true)
        {
            if (i > -1 && i < length) { return true && isIn; }
            else { return false == isIn; }
        }
        [ContextMenu("Set Text List")]
        public void SetTextList()
        {
            SetTextList(textSymbol);
        }
        public void SetTextList(char symbol)
        {
            m_nextIndex = 0;
            Convert.ArrayTo(Convert.TextToStringArray(m_textAsset, symbol), textList);
        }
        public void SetTextList(string symbol)
        {
            m_nextIndex = 0;
            Convert.ArrayTo(Convert.TextToStringArray(m_textAsset, symbol), textList);
        }
        [ContextMenu("Start Text")]
        public virtual void StartText()
        {
            m_nextIndex = 0;
            textUI.gameObject.SetActive(true);
            textUI.enabled = true;
        }
        [ContextMenu("End Text")]
        public virtual void EndText()
        {
            textUI.enabled = false;
        }

        public virtual void ShowNextString(bool isNext = true)
        {
            textString = textList[nextIndex];
            Run();
            if (isNext) { ++nextIndex; }
        }
        protected override IEnumerator RunCoroutine()
        {
            InvokeTextEvent(GetIndex() - 1);
            switch (displayMode)
            {
                case DisplayMode.NormalDisplay: NormalDisplay(); break;
                case DisplayMode.DisplayOneByOne: yield return StartCoroutine(DisplayOneByOne(textUI, waitForSeconds, textString)); break;
            }
            InvokeTextEvent(GetIndex());
        }
        protected void InvokeTextEvent(int index)
        {
            foreach (TextEvent obj in textEvents)
            {
                if (obj.Invoke(index) && obj.isZero)
                {
                    textEvents.Remove(obj);
                    break;
                }
            }
        }
        protected virtual void NormalDisplay()
        {
            textUI.text = textString;
        }
        protected virtual IEnumerator DisplayOneByOne(Text text, WaitForSeconds wait, string str, bool isRun = true)
        {
            text.text = null;
            foreach (char obj in str)
            {
                text.text += obj;
                if (isRun) { InvokeRunEvent(); }
                yield return wait;
            }
        }
    }

    [Serializable]
    public class TextEvent : BaseEvent
    {
        [SerializeField]
        protected List<int> m_indexes = new List<int>();

        public int count
        {
            get { return m_indexes.Count; }
        }
        public bool isZero
        {
            get { return count == 0; }
        }

        public TextEvent()
        {
        }
        public TextEvent(bool isEnable, UnityEvent unityEvent, int index) : base(isEnable, unityEvent)
        {
            m_indexes.Add(index);
        }
        public TextEvent(bool isEnable, UnityEvent unityEvent, List<int> indexes) : base(isEnable, unityEvent)
        {
            m_indexes = indexes;
        }
        public void Add(int index = -1)
        {
            m_indexes.Add(index);
        }
        public void Remove(int index = -1)
        {
            m_indexes.Remove(index);
        }
        public void Clear()
        {
            m_indexes.Clear();
        }
        public override void Dispose()
        {
            base.Dispose();
            m_indexes.Clear();
            m_indexes = null;
        }

        public override bool Invoke()
        {
            return false;
        }
        public override bool Invoke(bool value)
        {
            return false;
        }
        public bool Invoke(int index)
        {
            return Invoke(isEnable, index);
        }
        public bool Invoke(bool value, int index)
        {
            value = !isNull && value && m_indexes.Contains(index);
            if (value)
            {
                m_unityEvent.Invoke();
                m_indexes.Remove(index);
            }
            else if (index < 0) { m_indexes.Remove(index); }
            return value;
        }
    }

    /// <summary>
    /// 显示的方式
    /// </summary>
    public enum DisplayMode
    {
        /// <summary>
        /// 正常显示
        /// </summary>
        NormalDisplay,
        /// <summary>
        /// 逐个字显示
        /// </summary>
        DisplayOneByOne
    }
}