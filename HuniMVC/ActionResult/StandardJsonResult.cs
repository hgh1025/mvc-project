using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

// 에러 메시지 처리를 포함한 표준 응답을 생성하기 위한 로직

namespace HuniMVC.ActionResults
{
    public class StandardJsonResult : JsonResult
    {
        public IList<string> ErrorMessages { get; private set; }
        public StandardJsonResult() : base(null) // : base(null) -> sonResult가 인수를 0 개를 사용하는 생성자가 포함되어 있지않기 때문에 null 를 상속받는다.
        {
            ErrorMessages = new List<string>();
        }

        //에러 메시지를 리스트에 추가하기 위한 메서드
        public void AddError(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }

        //요청 메서드가 GET이면 예외를 발생시키고, 그렇지 않은 경우 응답의 ContentType을 설정하며, 에러가 있는 경우 이를 직렬화합니다.
        public override void ExecuteResult(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            if (string.Equals(request.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("GET access is not allowed.");
            }

            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            SerializeData(response);
        }

        //에러 메시지가 있으면 Value 속성에 에러 정보와 함께 새 객체를 할당하고, 응답 상태 코드를 400으로 설정합니다.
        protected virtual async Task SerializeData(HttpResponse response)
        {
            if (ErrorMessages.Any())
            {
                var originalData = Value;
                Value = new
                {
                    Success = false,
                    OriginalData = originalData,
                    ErrorMessage = string.Join("\n", ErrorMessages),
                    ErrorMessages = ErrorMessages.ToArray()
                };

                response.StatusCode = 400;
            }

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new JsonConverter[]
                {
                    new StringEnumConverter(),
                },
            };
            //응답 데이터를 JSON으로 직렬화하고 쓰는 부분이 비동기로 처리되어야 하므로, WriteAsync 메서드를 사용해야 합니다.
            await response.WriteAsync(JsonConvert.SerializeObject(Value, settings));
        }
    }

    public class StandardJsonResult<T> : StandardJsonResult
    {
        public new T Value
        {
            get { return (T)base.Value; } // value는 object로 되어있는데 T의 값이 int, string같이 다른 타입이 들어올 경우 이를 형변환 하기 위해서 필요하다. -> 파생 클래스에서 특정 형식의 값을 좀 더 편리하게 다룰 수 있습니다.
            set { base.Value = value; }
        }
    }
}
