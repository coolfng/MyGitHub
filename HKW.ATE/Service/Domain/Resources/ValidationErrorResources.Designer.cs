﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HKW.ATE.Domain.Resources {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ValidationErrorResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationErrorResources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HKW.ATE.Domain.Resources.ValidationErrorResources", typeof(ValidationErrorResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 {0}超过所能容纳的数量 的本地化字符串。
        /// </summary>
        public static string CompareValueWithTargeErrMsg {
            get {
                return ResourceManager.GetString("CompareValueWithTargeErrMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该项值范围是1-100 的本地化字符串。
        /// </summary>
        public static string LocationLevelDataRangeErrMsg {
            get {
                return ResourceManager.GetString("LocationLevelDataRangeErrMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 密码不匹配 的本地化字符串。
        /// </summary>
        public static string PasswordConfirmationMismatchErrMsg {
            get {
                return ResourceManager.GetString("PasswordConfirmationMismatchErrMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 密码最小长度为6位 的本地化字符串。
        /// </summary>
        public static string PasswordLenghtErrMsg {
            get {
                return ResourceManager.GetString("PasswordLenghtErrMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 必填选项 的本地化字符串。
        /// </summary>
        public static string RequiredErrMasg {
            get {
                return ResourceManager.GetString("RequiredErrMasg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 该项值范围是0-9 的本地化字符串。
        /// </summary>
        public static string RoomTypeDataRangeErrMsg {
            get {
                return ResourceManager.GetString("RoomTypeDataRangeErrMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 {1}“{0}”已被使用 的本地化字符串。
        /// </summary>
        public static string UniqueNameErrMsg {
            get {
                return ResourceManager.GetString("UniqueNameErrMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 登录超时，请重新登录或刷新 的本地化字符串。
        /// </summary>
        public static string UnloginErrMsg {
            get {
                return ResourceManager.GetString("UnloginErrMsg", resourceCulture);
            }
        }
    }
}