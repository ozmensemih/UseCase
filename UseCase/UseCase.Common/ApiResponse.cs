using UseCase.Common.Enums;
using UseCase.Common.Extensions;
using System;
using System.Runtime.Serialization;

namespace UseCase.Common
{
    [DataContract]
    public class ApiResponse<T>
    {

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public Guid TraceId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ResponseMessageEnum Type { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Message { get; set; }

        [DataMember]
        public bool IsError { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public T Result { get; set; }

        //[JsonConstructor]
        public ApiResponse(string message, T result, int statusCode = 200)
        {
            this.StatusCode = statusCode;
            this.Message = message;
            this.Result = result;
            this.IsError = false;
        }

        public ApiResponse()
        {
            this.StatusCode = 200;
            this.IsError = false;
            this.TraceId = Guid.NewGuid();
            this.Type = ResponseMessageEnum.Success;
            this.Message = ResponseMessageEnum.Success.GetDescription();
        }

        public ApiResponse<T> SetResult(T result)
        {
            this.Result = result;
            return this;
        }

        public ApiResponse<T> ErrorResult(T result, ResponseMessageEnum responseMessage, int status = 500, string message = null)
        {
            this.StatusCode = status;
            this.IsError = true;
            this.Type = responseMessage;
            if (message == null)
            {
                this.Message = responseMessage.GetDescription();
            }
            else
            {
                this.Message = message;
            }

            this.Result = result;
            return this;

        }
    }
}
