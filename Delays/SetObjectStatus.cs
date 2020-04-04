namespace Linns.Delays
{
    using System.Collections.Generic;
    using UnityEngine;
    using PropertyInfo = System.Reflection.PropertyInfo;

    /// <summary>
    /// 设置对象状态类
    /// </summary>
    [AddComponentMenu("Linns/Delays/Set Object Status")]
    public class SetObjectStatus : Delay
    {
        public ObjectType objectType;
        public List<Component> enables;
        public List<Component> noEnables;
        public List<Component> counterEnables;
        public List<GameObject> actives;
        public List<GameObject> disables;
        public List<GameObject> counters;
        public List<Object> destroys;

        protected override void RunMethod()
        {
            switch (objectType)
            {
                case ObjectType.Component: ComponentEnable(); break;
                case ObjectType.GameObject: GameObjectActive(); break;
                case ObjectType.Object: ObjectDestroy(); break;
                case ObjectType.NotIncludComponent:
                    GameObjectActive();
                    ObjectDestroy();
                    break;
                case ObjectType.NotIncludGameObject:
                    ComponentEnable();
                    ObjectDestroy();
                    break;
                case ObjectType.NotIncludObject:
                    ComponentEnable();
                    GameObjectActive();
                    break;
                case ObjectType.AllInclud:
                    ComponentEnable();
                    GameObjectActive();
                    ObjectDestroy();
                    break;
            }
        }

        protected void ComponentEnable()
        {
            RunForeach(enables, true);
            RunForeach(noEnables, false);
            RunForeach();
        }
        protected void GameObjectActive()
        {
            foreach (GameObject obj in actives)
            {
                if (obj) { obj.SetActive(true); }
            }

            foreach (GameObject obj in disables)
            {
                if (obj) { obj.SetActive(true); }
            }

            foreach (GameObject obj in counters)
            {
                if (obj) { obj.SetActive(!obj.activeSelf); }
            }

        }
        protected void ObjectDestroy()
        {
            foreach (Object obj in destroys)
            {
                if (obj) { Destroy(obj); }
            }
            Loop.Empty(destroys);
        }

        protected void RunForeach()
        {
            foreach (Component obj in counterEnables)
            {
                SetComponentEnable(obj);
            }
        }
        protected static void RunForeach(List<Component> list, bool value)
        {
            foreach (Component obj in list)
            {
                SetComponentEnable(obj, value);
            }
        }
        public static bool SetComponentEnable(Component component)
        {
            try
            {
                PropertyInfo property = component.GetType().GetProperty("enabled");
                property.SetValue(component, !(bool)property.GetValue(component));
                return true;
            }
            catch { return false; }
        }
        public static bool SetComponentEnable(Component component, bool value)
        {
            try
            {
                component.GetType().GetProperty("enabled").SetValue(component, value);
                return true;
            }
            catch { return false; }
        }
    }

    /// <summary>
    /// 对象的类型
    /// </summary>
    public enum ObjectType
    {
        /// <summary>
        /// 组件
        /// </summary>
        Component,
        /// <summary>
        /// 游戏对象
        /// </summary>
        GameObject,
        /// <summary>
        /// 对象
        /// </summary>
        Object,
        /// <summary>
        /// 游戏对象+对象
        /// </summary>
        NotIncludComponent,
        /// <summary>
        /// 组件+对象
        /// </summary>
        NotIncludGameObject,
        /// <summary>
        /// 组件+游戏对象
        /// </summary>
        NotIncludObject,
        /// <summary>
        /// 组件+游戏对象+对象
        /// </summary>
        AllInclud
    }
}