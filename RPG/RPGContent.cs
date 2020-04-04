namespace Linns.RPG
{
    using System.Collections.Generic;
    using UnityEngine;
    using Text = UnityEngine.UI.Text;

    [AddComponentMenu("Linns/RPG/RPG Content")]
    public class RPGContent : RPGText
    {
        public float nameIntervalTime;
        public string nameSymbol = ":";
        public DisplayMode nameDisplayMode;
        public Text nameUI;
        public List<string> nameList = new List<string>();
        protected List<string> m_list = new List<string>();

        public override TextAsset textAsset
        {
            get { return textAsset; }
            set
            {
                textAsset = value;
                SetContentList(textSymbol, nameSymbol);
                textEvents = new List<TextEvent>();
                ShowNextString(true);
            }
        }
        public string nameString
        {
            get;
            private set;
        }

        protected new void OnDestroy()
        {
            base.OnDestroy();
            nameList.Clear();
            m_list.Clear();
        }
        [ContextMenu("Set Content List")]
        public void SetContentList()
        {
            SetContentList(textSymbol, nameSymbol);
        }
        public void SetContentList(char textSymbol, char nameSymbol)
        {
            SetContentList(textSymbol.ToString(), nameSymbol.ToString());
        }
        public void SetContentList(char textSymbol, string nameSymbol)
        {
            SetContentList(textSymbol.ToString(), nameSymbol);
        }
        public void SetContentList(string textSymbol, char nameSymbol)
        {
            SetContentList(textSymbol, nameSymbol.ToString());
        }
        public void SetContentList(string textSymbol, string nameSymbol)
        {
            m_nextIndex = 0;
            nameList.Clear();
            textList.Clear();
            Convert.ArrayTo(Convert.TextToStringArray(m_textAsset, textSymbol), m_list);
            foreach (string obj in m_list)
            {
                string[] str = Convert.TextToStringArray(obj, nameSymbol);
                try
                {
                    nameList.Add(str[0]);
                    textList.Add(str[1]);
                }
                catch { print("格式有误！"); }
            }
        }
        public override void StartText()
        {
            base.StartText();
            nameUI.gameObject.SetActive(true);
            nameUI.enabled = true;
        }
        public override void EndText()
        {
            base.EndText();
            nameUI.enabled = false;
        }

        public override void ShowNextString(bool isNext = true)
        {
            nameString = nameList[nextIndex];
            base.ShowNextString(isNext);
        }
        protected override void NormalDisplay()
        {
            nameUI.text = nameString;
            base.NormalDisplay();
        }
        protected override System.Collections.IEnumerator DisplayOneByOne(Text text, WaitForSeconds wait, string str, bool isRun = true)
        {
            Coroutine coro = null;

            switch (nameDisplayMode)
            {
                case DisplayMode.NormalDisplay:
                    nameUI.text = nameString;
                    break;
                case DisplayMode.DisplayOneByOne:
                    WaitForSeconds wait0 = nameIntervalTime > 0f ? new WaitForSeconds(nameIntervalTime) : null;
                    coro = StartCoroutine(base.DisplayOneByOne(nameUI, wait0, nameString, false));
                    break;
            }
            yield return StartCoroutine(base.DisplayOneByOne(text, wait, str));
            yield return coro;
        }
    }
}