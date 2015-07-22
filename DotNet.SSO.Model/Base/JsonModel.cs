using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DotNet.SSO.Model
{
    [DataContract]
    public class JsonModel<T>
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        [DataMember]
        public bool Success { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        [DataMember]
        public string ErrCode { get; set; }
        /// <summary>
        /// 错误的消息
        /// </summary>
        [DataMember]
        public string ErrMsg { get; set; }
        /// <summary>
        /// 执行成功的消息提示
        /// </summary>
        [DataMember]
        public string SuccessMsg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        [DataMember]
        public T Data { get; set; }
        /// <summary>
        /// 扩展
        /// </summary>
        [DataMember]
        public string Extension { get; set; }
    }
}
