namespace Linns.UICommonEvents
{
    using System;
    using UnityEngine;
    using IEnumerator = System.Collections.IEnumerator;
    using ListTransform = System.Collections.Generic.List<UnityEngine.Transform>;

    [Serializable]
    public struct UIRotate : IDisposable
    {
        private Coroutine m_coroutine;
        private Operation m_operation;

        public void ForOnDrag(UseUIRotate useUIRotate)
        {
            if (!useUIRotate.mouseButton.mouseButtonStay) { return; }
            if (useUIRotate.dirType == VectorType.Other && !useUIRotate.other) { return; }

            Vector2 dir = Mouse.direction;
            if (useUIRotate.isLimitedDir) { dir = Vector3.Project(dir, useUIRotate.direction); }

            Vector3 point = useUIRotate.transform.position;
            switch (useUIRotate.rotateType)
            {
                case RotateType.Point: point = useUIRotate.point; break;
                case RotateType.Other:
                    if (useUIRotate.pos) { point = useUIRotate.pos.position; }
                    else { return; }
                    break;
            }

            switch (useUIRotate.uiTarget)
            {
                case UITarget.Self: ForOnDrag(useUIRotate.transform, useUIRotate.other, useUIRotate.rotateType, useUIRotate.dirType, useUIRotate.movingRatio, dir, point); break;
                case UITarget.Other: ForOnDrag(useUIRotate.target, useUIRotate.other, useUIRotate.rotateType, useUIRotate.dirType, useUIRotate.movingRatio, dir, point); break;
                case UITarget.Others: ForOnDrag(useUIRotate.targets, useUIRotate.other, useUIRotate.rotateType, useUIRotate.dirType, useUIRotate.movingRatio, dir, point); break;
            }
        }
        public void ForOnDrag(Transform target, Transform other, RotateType rotateType, VectorType dirType, Vector2 movingRatio, Vector2 dir, Vector3 point)
        {
            if (!target) { return; }

            Vector3 dir3D = new Vector3(dir.y * movingRatio.x, -dir.x * movingRatio.y);
            switch (dirType)
            {
                case VectorType.Self: dir3D = target.TransformDirection(dir3D); break;
                case VectorType.Parent: if (target.parent) dir3D = target.parent.TransformDirection(dir3D); break;
                case VectorType.Other: dir3D = other.TransformDirection(dir3D); break;
            }

            if (rotateType == RotateType.Self) { point = target.position; }
            target.RotateAround(point, dir3D, dir3D.magnitude);
        }
        public void ForOnDrag(ListTransform targets, Transform other, RotateType rotateType, VectorType dirType, Vector2 movingRatio, Vector2 dir, Vector3 point)
        {
            foreach (Transform obj in targets) { ForOnDrag(obj, other, rotateType, dirType, movingRatio, dir, point); }
        }

        public void ForOnEndDrag(UseUIRotate useUIRotate)
        {
            if (!useUIRotate.mouseButton.mouseButtonUp) { return; }
            if (useUIRotate.dirType == VectorType.Other && !useUIRotate.other) { return; }
            if (m_coroutine != null) { useUIRotate.StopCoroutine(m_coroutine); }

            Vector2 dir = Mouse.direction;
            if (useUIRotate.isLimitedDir) { dir = Vector3.Project(dir, useUIRotate.direction); }

            Vector3 point = useUIRotate.transform.position;
            switch (useUIRotate.rotateType)
            {
                case RotateType.Point: point = useUIRotate.point; break;
                case RotateType.Other:
                    if (useUIRotate.pos) { point = useUIRotate.pos.position; }
                    else { return; }
                    break;
            }

            switch (useUIRotate.uiTarget)
            {
                case UITarget.Self:
                    m_coroutine = useUIRotate.StartCoroutine(
                        ForOnEndDrag(useUIRotate.transform, useUIRotate.other, useUIRotate.rotateType, useUIRotate.dirType, useUIRotate.movingRatio, dir, point,
                        useUIRotate.decelerationType, useUIRotate.percentage, useUIRotate.deltaTime, useUIRotate.start, useUIRotate.end)); break;
                case UITarget.Other:
                    m_coroutine = useUIRotate.StartCoroutine(
                        ForOnEndDrag(useUIRotate.target, useUIRotate.other, useUIRotate.rotateType, useUIRotate.dirType, useUIRotate.movingRatio, dir, point,
                        useUIRotate.decelerationType, useUIRotate.percentage, useUIRotate.deltaTime, useUIRotate.start, useUIRotate.end)); break;
                case UITarget.Others:
                    int count = useUIRotate.targets.Count;
                    Vector3[] poss = new Vector3[count];
                    for (int i = 0; i < count; ++i)
                    {
                        if (!useUIRotate.targets[i]) { continue; }

                        poss[i] = useUIRotate.targets[i].position;
                        switch (useUIRotate.rotateType)
                        {
                            case RotateType.Point: poss[i] = useUIRotate.point; break;
                            case RotateType.Other:
                                if (useUIRotate.pos) { poss[i] = useUIRotate.pos.position; }
                                else { return; }
                                break;
                        }
                    }
                    m_coroutine = useUIRotate.StartCoroutine(
                        ForOnEndDrag(useUIRotate.targets, useUIRotate.other, useUIRotate.rotateType, useUIRotate.dirType, useUIRotate.movingRatio, dir, poss,
                        useUIRotate.decelerationType, useUIRotate.percentage, useUIRotate.deltaTime, useUIRotate.start, useUIRotate.end)); break;
            }
        }
        public IEnumerator ForOnEndDrag(
            Transform target, Transform other, RotateType rotateType, VectorType dirType, Vector2 movingRatio, Vector2 dir, Vector3 point,
            DecelerationType decelerationType, float percentage = 0.1f, float time = 0.01f, float start = 1f, float end = 0.1f)
        {
            if (!target) { yield break; }

            Vector3 dir3D = new Vector3(dir.y * movingRatio.x, -dir.x * movingRatio.y);
            switch (dirType)
            {
                case VectorType.Self: dir3D = target.TransformDirection(dir3D); break;
                case VectorType.Parent: if (target.parent) dir3D = target.parent.TransformDirection(dir3D); break;
                case VectorType.Other: dir3D = other.TransformDirection(dir3D); break;
            }

            if (rotateType == RotateType.Self) { point = target.position; }

            switch (decelerationType)
            {
                case DecelerationType.Linear: m_operation += Linear; break;
                case DecelerationType.Lerp: m_operation += Lerp; break;
                default: yield break;
            }

            float length = dir3D.magnitude;
            WaitForSeconds wait = new WaitForSeconds(time);
            while ((start > end))
            {
                m_operation(ref start, in percentage);
                target.RotateAround(point, dir3D, length * start);
                yield return wait;
            }
            wait = null;
            Clear();
        }
        public IEnumerator ForOnEndDrag(
                ListTransform targets, Transform other, RotateType rotateType, VectorType dirType, Vector2 movingRatio, Vector2 dir, Vector3[] points,
                DecelerationType decelerationType, float percentage = 0.1f, float time = 0.01f, float start = 1f, float end = 0.1f)
        {
            int count = targets.Count;
            Vector3[] dir3Ds = new Vector3[count];
            dir3Ds[0].Set(dir.y * movingRatio.x, -dir.x * movingRatio.y, 0f);
            for (int i = 1; i != count; ++i) { dir3Ds[i].Set(dir3Ds[0].x, dir3Ds[0].y, dir3Ds[0].z); }

            for (int i = 0; i != count; ++i)
                switch (dirType)
                {
                    case VectorType.Self: dir3Ds[i] = targets[i].TransformDirection(dir3Ds[i]); break;
                    case VectorType.Parent: if (targets[i] && targets[i].parent) { dir3Ds[i] = targets[i].parent.TransformDirection(dir3Ds[i]); } break;
                    case VectorType.Other: dir3Ds[i] = other.TransformDirection(dir3Ds[i]); break;
                }

            if (rotateType == RotateType.Self)
            {
                for (int i = 0; i != count; ++i) { points[i] = targets[i].position; }
            }
            switch (decelerationType)
            {
                case DecelerationType.Linear: m_operation += Linear; break;
                case DecelerationType.Lerp: m_operation += Lerp; break;
                default: yield break;
            }

            float[] lengths = new float[count];
            for (int i = 0; i != count; ++i) { lengths[i] = dir3Ds[i].magnitude; }

            WaitForSeconds wait = new WaitForSeconds(time);
            while (start > end)
            {
                m_operation(ref start, in percentage);
                for (int i = 0; i != count; ++i)
                {
                    if (targets[i]) { targets[i].RotateAround(points[i], dir3Ds[i], lengths[i] * start); }
                }
                yield return wait;
            }
            wait = null;
            Clear();
        }

        private delegate void Operation(ref float left, in float right);
        private static void Linear(ref float left, in float right)
        {
            left -= right;
        }
        private static void Lerp(ref float left, in float right)
        {
            left *= 1f - right;
        }
        public void Clear()
        {
            m_operation -= Linear;
            m_operation -= Lerp;
            m_operation = null;
        }
        public void Dispose()
        {
            m_coroutine = null;
            Clear();
        }
    }
}