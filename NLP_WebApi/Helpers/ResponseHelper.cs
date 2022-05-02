using NLP_WebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLP_WebApi.Helpers
{
    public class ResponseHelper
    {

        //In this section, we are preparing the response information that we will return to the request.
        public static BaseResponseModel<UserResponseModel> ConvertorResponse(string userText, string message, int statusCode)
        {
            UserResponseModel userResponse = new UserResponseModel();
            var baseResponse = new BaseResponseModel<UserResponseModel>();

            userResponse.convertedText = userText;
            baseResponse.Data = userResponse;
            baseResponse.Message = message;

            switch (statusCode)
            {
                case 1:
                    baseResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    break;
                case 2:
                    baseResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    break;
                case 3:
                    baseResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    break;

            };

            return baseResponse;

        }


    }
}
