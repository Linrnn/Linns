namespace Linns.UICommonEvents
{
    /// <summary>
    /// UI对象
    /// </summary>
    public enum UITarget
    {
        /// <summary>
        /// 不选中
        /// </summary>
        None,
        /// <summary>
        /// 自身
        /// </summary>
        Self,
        /// <summary>
        /// 某个对象
        /// </summary>
        Other,
        /// <summary>
        /// 某些对象
        /// </summary>
        Others
    }

    /// <summary>
    /// 向量的类型
    /// </summary>
    public enum VectorType
    {
        /// <summary>
        /// 自身为标准
        /// </summary>
        Self,
        /// <summary>
        /// 父对象为标准
        /// </summary>
        Parent,
        /// <summary>
        /// 世界为标准
        /// </summary>
        World,
        /// <summary>
        /// 某个对象为标准
        /// </summary>
        Other
    }

    /// <summary>
    /// 方向的构成方式
    /// </summary>
    public enum DirectionStructure
    {
        /// <summary>
        /// 方向由两个点构成
        /// </summary>
        TwoPoints = 1,
        /// <summary>
        /// 方向是一个二维向量
        /// </summary>
        Vector2,
        /// <summary>
        /// 方向是一个三维向量
        /// </summary>
        Vector3,
        /// <summary>
        /// 方向是一个四维向量
        /// </summary>
        Vector4
    }

    /// <summary>
    /// 几对几
    /// </summary>
    public enum SeveralToSeveral
    {
        /// <summary>
        /// 一对一
        /// </summary>
        OneToOne,
        /// <summary>
        /// 多对一
        /// </summary>
        ManyToOne,
        /// <summary>
        /// 多对多
        /// </summary>
        ManyToMany,
    }

    /// <summary>
    /// Transform的类型
    /// </summary>
    public enum TransformType
    {
        /// <summary>
        /// Transform
        /// </summary>
        Transform,
        /// <summary>
        /// Rect Transform
        /// </summary>
        RectTransform
    }

    /// <summary>
    /// 运算
    /// </summary>
    public enum Operation
    {
        /// <summary>
        /// 加法
        /// </summary>
        Addition,
        /// <summary>
        /// 减法
        /// </summary>
        Subtraction,
        /// <summary>
        /// 乘法
        /// </summary>
        Multiplication,
        /// <summary>
        /// 除法
        /// </summary>
        Division
    }

    /// <summary>
    /// 隐形的方式
    /// </summary>
    public enum StealthMethod
    {
        /// <summary>
        /// 不改变
        /// </summary>
        NoChange,
        /// <summary>
        /// 一直显示
        /// </summary>
        Stay,
        /// <summary>
        /// 可视组件
        /// </summary>
        Enable,
        /// <summary>
        /// 激活状态
        /// </summary>
        Active
    }

    /// <summary>
    /// 减速的方式
    /// </summary>
    public enum DecelerationType
    {
        /// <summary>
        /// 不运行
        /// </summary>
        None,
        /// <summary>
        /// 线性
        /// </summary>
        Linear,
        /// <summary>
        /// 插值
        /// </summary>
        Lerp
    }

    /// <summary>
    /// 旋转的中心点
    /// </summary>
    public enum RotateType
    {
        /// <summary>
        /// 自身
        /// </summary>
        Self,
        /// <summary>
        /// 一个点
        /// </summary>
        Point,
        /// <summary>
        /// 一个对象
        /// </summary>
        Other
    }
}