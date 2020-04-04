namespace Linns
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    public abstract class TagBase<TTag, TEnum> : MonoBehaviour
        where TTag : TagBase<TTag, TEnum>
        where TEnum : Enum
    {
        public TEnum tagValue;

        public int intValue
        {
            get { return (int)(ValueType)tagValue; }
        }

        public static TTag GetTag(GameObject gameObject)
        {
            return gameObject.GetComponentInParent<TTag>();
        }
        public static TTag GetTag(Component component)
        {
            return component.GetComponentInParent<TTag>();
        }
        public static TTag GetTag(ControllerColliderHit controllerColliderHit)
        {
            return controllerColliderHit.gameObject.GetComponentInParent<TTag>();
        }
        public static TTag GetTag(Collision collision)
        {
            return collision.gameObject.GetComponentInParent<TTag>();
        }
        public static TTag GetTag(Collision2D collision2D)
        {
            return collision2D.gameObject.GetComponentInParent<TTag>();
        }
        public static TTag GetTag(RaycastHit raycastHit)
        {
            return raycastHit.transform.GetComponentInParent<TTag>();
        }
        public static TTag GetTag(RaycastHit2D raycastHit2D)
        {
            return raycastHit2D.transform.GetComponentInParent<TTag>();
        }

        public static TTag GetTag(GameObject gameObject, out TTag output)
        {
            return output = gameObject.GetComponentInParent<TTag>();
        }
        public static TTag GetTag(Component component, out TTag output)
        {
            return output = component.GetComponentInParent<TTag>();
        }
        public static TTag GetTag(ControllerColliderHit controllerColliderHit, out TTag output)
        {
            return output = controllerColliderHit.gameObject.GetComponentInParent<TTag>();
        }
        public static TTag GetTag(Collision collision, out TTag output)
        {
            return output = collision.gameObject.GetComponentInParent<TTag>();
        }
        public static TTag GetTag(Collision2D collision2D, out TTag output)
        {
            return output = collision2D.gameObject.GetComponentInParent<TTag>();
        }
        public static TTag GetTag(RaycastHit raycastHit, out TTag output)
        {
            return output = raycastHit.transform.GetComponentInParent<TTag>();
        }
        public static TTag GetTag(RaycastHit2D raycastHit2D, out TTag output)
        {
            return output = raycastHit2D.transform.GetComponentInParent<TTag>();
        }

        public static TEnum GetTagValue(int tag)
        {
            return (TEnum)(ValueType)tag;
        }
        public static int GetTagValue(TEnum tag)
        {
            return (int)(ValueType)tag;
        }

        public bool Contains(TEnum tag)
        {
            return (intValue & GetTagValue(tag)) != 0;
        }
        public void AddTag(TEnum tag)
        {
            tagValue = GetTagValue(intValue | GetTagValue(tag));
        }
        public void AddAllTags()
        {
            tagValue = GetTagValue(-1);
        }
        public void RemoveTag(TEnum tag)
        {
            int value = intValue;
            tagValue = GetTagValue(value & GetTagValue(tag) ^ value);
        }
        public void RemoveAllTags()
        {
            tagValue = GetTagValue(0);
        }
    }
}