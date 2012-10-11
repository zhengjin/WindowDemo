﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace ListView
{
    #region 上下文
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    public partial class TestDataEntities : ObjectContext
    {
        #region 构造函数
    
        /// <summary>
        /// 请使用应用程序配置文件的“TestDataEntities”部分中的连接字符串初始化新 TestDataEntities 对象。
        /// </summary>
        public TestDataEntities() : base("name=TestDataEntities", "TestDataEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 TestDataEntities 对象。
        /// </summary>
        public TestDataEntities(string connectionString) : base(connectionString, "TestDataEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 TestDataEntities 对象。
        /// </summary>
        public TestDataEntities(EntityConnection connection) : base(connection, "TestDataEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region 分部方法
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet 属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public ObjectSet<tblImage> tblImage
        {
            get
            {
                if ((_tblImage == null))
                {
                    _tblImage = base.CreateObjectSet<tblImage>("tblImage");
                }
                return _tblImage;
            }
        }
        private ObjectSet<tblImage> _tblImage;

        #endregion
        #region AddTo 方法
    
        /// <summary>
        /// 用于向 tblImage EntitySet 添加新对象的方法，已弃用。请考虑改用关联的 ObjectSet&lt;T&gt; 属性的 .Add 方法。
        /// </summary>
        public void AddTotblImage(tblImage tblImage)
        {
            base.AddObject("tblImage", tblImage);
        }

        #endregion
    }
    

    #endregion
    
    #region 实体
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="TestDataModel", Name="tblImage")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class tblImage : EntityObject
    {
        #region 工厂方法
    
        /// <summary>
        /// 创建新的 tblImage 对象。
        /// </summary>
        /// <param name="imgId">ImgId 属性的初始值。</param>
        public static tblImage CreatetblImage(global::System.Guid imgId)
        {
            tblImage tblImage = new tblImage();
            tblImage.ImgId = imgId;
            return tblImage;
        }

        #endregion
        #region 基元属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid ImgId
        {
            get
            {
                return _ImgId;
            }
            set
            {
                if (_ImgId != value)
                {
                    OnImgIdChanging(value);
                    ReportPropertyChanging("ImgId");
                    _ImgId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ImgId");
                    OnImgIdChanged();
                }
            }
        }
        private global::System.Guid _ImgId;
        partial void OnImgIdChanging(global::System.Guid value);
        partial void OnImgIdChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ImgURL
        {
            get
            {
                return _ImgURL;
            }
            set
            {
                OnImgURLChanging(value);
                ReportPropertyChanging("ImgURL");
                _ImgURL = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ImgURL");
                OnImgURLChanged();
            }
        }
        private global::System.String _ImgURL;
        partial void OnImgURLChanging(global::System.String value);
        partial void OnImgURLChanged();

        #endregion
    
    }

    #endregion
    
}
