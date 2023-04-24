using Model.Lib60.helper;

namespace Model.Lib60
{
    public class Response<T>
    {
        /// <summary>
        /// 状态结果
        /// </summary>
        public ResultStatus? code { get; set; } = ResultStatus.Success;

        private string? _msg;

        /// <summary>
        /// 消息描述
        /// </summary>
        public string? message
        {
            get
            {
                return !string.IsNullOrEmpty(_msg) ? _msg : new EnumHelper().GetDescription(code);
            }
            set
            {
                _msg = value;
            }
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        public T? data { get; set; }

        /// <summary>
        /// 成功状态返回结果
        /// </summary>
        /// <param name="result">返回的数据</param>
        /// <returns></returns>
        public Response<T> SuccessResult(T data)
        {
            return new Response<T> { code = ResultStatus.Success, data = data };
        }

        /// <summary>
        /// 失败状态返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">失败信息</param>
        /// <returns></returns>
        public Response<T> FailResult(string? msg = null)
        {
            return new Response<T> { code = ResultStatus.Fail, message = msg };
        }

        /// <summary>
        /// 异常状态返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">异常信息</param>
        /// <returns></returns>
        public Response<T> ErrorResult(string? msg = null)
        {
            return new Response<T> { code = ResultStatus.Error, message = msg };
        }

        /// <summary>
        /// 自定义状态返回结果
        /// </summary>
        /// <param name="status"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public Response<T> Result(ResultStatus status, T data, string? msg = null)
        {
            return new Response<T> { code = status, data = data, message = msg };
        }
    }
}