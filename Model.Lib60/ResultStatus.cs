using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Lib60
{
    public enum ResultStatus
    {
        [Description("请求成功")]
        Success = 1,
        [Description("请求失败")]
        Fail = 0,
        [Description("请求异常")]
        Error = -1
    }
}
