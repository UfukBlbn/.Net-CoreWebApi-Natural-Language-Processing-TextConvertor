using Microsoft.AspNetCore.Mvc;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.Number;
using NLP_WebApi.Extensions;
using NLP_WebApi.Helpers;
using NLP_WebApi.Models.ControllerModels;
using NLP_WebApi.Models.RequestModels;
using NLP_WebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLP_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpRequestController : Controller
    {
        [HttpPost("[action]")]
        public BaseResponseModel<UserResponseModel> CharToNumberConvertor([FromBody] UserRequestModel userRequestModel)
        {
            ConvertorModel convertorModel = new ConvertorModel();

            if (!string.IsNullOrEmpty(userRequestModel.userText))
            {
                //Uppercase format adapts
                var userTextFromModel = ConvertorHelper.Capitalization(userRequestModel.userText);

                //If the user has only entered a number value, this method returns true.
                var isDigit = ConvertorHelper.IsDigitsOnly(userTextFromModel);

                if (isDigit == false)
                {
                    var suffixedText = WordSuffixCheck.ContainsAnySuffix(userTextFromModel, SuffixModel.suffixArr);
                    if (suffixedText != userTextFromModel)
                    {
                        userTextFromModel = suffixedText;
                    }

                    //Here, with the numbersplitter extension method, we take the numbers written with digit from the user input and add them to the numberList.
                    convertorModel.numberList = RegexNumberSplitter.NumberSplitter(userTextFromModel.ToString());

                    var convertedTextWithNumbers = "";

                    if (convertorModel.numberList.Count() != 0)
                    {
                        foreach (var number in convertorModel.numberList)
                        {
                            //We convert the numbers in the list into text with the ConvertNumberToString method
                            var numberToText = NumberConvertor.ConvertNumberToString(Convert.ToInt16(number)).Replace("-", "").Replace(";", "");

                            //We replace the numbers in the text with the variable numberToText and assign it to the new variable named convertedTextWithNumbers.
                            //And as a result we get a text just written
                            convertedTextWithNumbers = (string.IsNullOrEmpty(convertedTextWithNumbers) ? userTextFromModel : convertedTextWithNumbers).Replace(number, numberToText);
                        }
                    }

                    //Here we first check whether there is a number written with a digit in the user input.If this condition is not met, we continue with the user input from the API
                    //We determine the numbers that need to be converted with the RecognizeNumber method.
                    var userText = NumberRecognizer.RecognizeNumber(convertorModel.numberList.Count == 0 ? userTextFromModel : convertedTextWithNumbers, Culture.Turkish);


                    //We determine the target word and target number in the text
                    var targetNumber = Convert.ToString(double.Parse(userText.First().Resolution["value"].ToString()));
                    var targetWord = userText.First().Text;

                    //If there are no repeating numbers in the text, we create the variable convertedTextOnlyOneNumber.
                    var convertedTextOnlyOneNumber = ConvertorHelper.UserTextFormatter(userTextFromModel, targetWord, targetNumber);


                    var userTextNumbers = new List<string>();
                    string convertedUserText = "";
                    var i = 0;
                    //If there are separate number expressions in the input, this condition is met and the relevant result is obtained.
                    if (userText.Count() > 1)
                    {
                        foreach (var item in userText)
                        {
                            i++;
                            var targetNumbers = double.Parse(item.Resolution["value"].ToString());
                            userTextNumbers.Add(targetNumbers.ToString());
                            if (convertedTextWithNumbers != "" || convertedUserText != "")
                            {
                                //In this field, we use the convertedTextWithNumbers variable that we have obtained in the upper section as priority.
                                convertedUserText = (string.IsNullOrEmpty(convertedUserText) == true ? convertedTextWithNumbers : convertedUserText).Replace(userText[i - 1].Text.ToString(), userTextNumbers[i - 1]);
                            }
                            else
                            {
                                //if no a variable has been created before, the input value from the api is taken directly
                                convertedUserText = (userTextFromModel).Replace(userText[i - 1].Text.ToString(), userTextNumbers[i - 1]); ;
                            }
                        }
                    }
                    string formattedUserText = "";
                    formattedUserText = ConvertorHelper.UserTextFormatter((convertorModel.numberList.Count != 0 && userText.Count() <= 1 ? ConvertorHelper.Capitalization(convertedTextWithNumbers) : convertedUserText), targetWord, targetNumber);

                    return ResponseHelper.ConvertorResponse(string.IsNullOrEmpty(formattedUserText) == true ? WordSuffixCheck.AddSuffixBack(ConvertorHelper.Capitalization(convertedTextOnlyOneNumber), SuffixModel.suffixArr) : WordSuffixCheck.AddSuffixBack(ConvertorHelper.Capitalization(formattedUserText), SuffixModel.suffixArr), "Text converted successfully", 1);
                }

                else
                {
                    return ResponseHelper.ConvertorResponse(userTextFromModel, "Text converted successfully", 1);
                }
            }

            else
            {

                return ResponseHelper.ConvertorResponse(userRequestModel.userText, "Input must not be empty", 3);
            }

        }
    }
}
