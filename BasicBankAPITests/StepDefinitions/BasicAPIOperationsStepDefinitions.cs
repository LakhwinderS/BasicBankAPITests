using BasicBankAPITests.Models;
using BasicBankAPITests.Utils;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace BasicBankAPITests.StepDefinitions
{
    [Binding]
    public class BasicAPIOperationsStepDefinitions
    {
        Object createPayload = null;
        Object depositPayload = null;
        ScenarioContext _scenarioContext;
        public BasicAPIOperationsStepDefinitions(ScenarioContext context)
        {
            _scenarioContext = context;
        }

        [Given(@"Account with '([^']*)' , '([^']*)','([^']*)'")]
        public void GivenAccountWith(double initialBalance, string accountName, string address)
        {
            createPayload = new createAccountRequest
            {
                InitialBalance = initialBalance,
                AccountName = accountName,
                Address = address,
            };

            _scenarioContext.Add("createPayload", createPayload);
        }

        [When(@"POST endpoint triggered to create new account with above details")]
        public async void WhenPOSTEndpointTriggeredToCreateNewAccountWithAboveDetails()
        {
            apiOperations api = new apiOperations(constansts.BaseURI);
            if (_scenarioContext != null)
            {
                RestResponse response = await api.PostData(constansts.create, _scenarioContext.Get<createAccountRequest>("createPayload"));
                if (response != null)
                {
                    _scenarioContext["responseStatusCode"] = (int)response.StatusCode;
                    dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
                    _scenarioContext["jsonResponse"] = jsonResponse;
                }
            }
        }

        [Then(@"Verify the response code is '([^']*)' and no error is returned")]
        public void ThenVerifyTheResponseCodeIsAndNoErrorIsReturned(string statusCode)
        {
            Assert.AreEqual(statusCode, _scenarioContext["responseStatusCode"]);
            
        }

        [Then(@"Verify the success message '([^']*)'")]
        public void ThenVerifyTheSuccessMessageAccountCreatedSuccessfully(string  message)
        {
            dynamic jsonObject = JObject.Parse(_scenarioContext["jsonResponse"].ToString());
            
            Assert.AreEqual(message, jsonObject.Message);
        }

        [Then(@"Verify the account details are correctly returned in the JSON response")]
        public void ThenVerifyTheAccountDetailsAreCorrectlyReturnedInTheJSONResponse()
        {
            try
            {
                if (_scenarioContext != null)
                {
                    commonMethods.ValidateJSON(_scenarioContext["jsonResponse"].ToString());
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Assert.Fail(ex.ToString());
            }
        
        }

        [Given(@"an Account with '([^']*)'")]
        public void GivenAnAccountWith(string accountID)
        {
           

            _scenarioContext["accountID"]= accountID;

        }


        [When(@"Delete endpoint triggered to delete an account with above details")]
        public async void WhenDeleteEndpointTriggeredToDeleteAnAccountWithAboveDetails()
        {

            apiOperations api = new apiOperations(constansts.BaseURI);
            string rourcePath = "delete" + _scenarioContext["accountID"].ToString();
            {
                RestResponse response = await api.DeleteData(rourcePath);
                if (response != null)
                {
                    _scenarioContext["responseStatusCode"] = (int)response.StatusCode;
                    dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
                    _scenarioContext["jsonResponse"] = jsonResponse;
                }
            }

        }

        [Then(@"Verify the response code is '([^']*)' and message “Account '([^']*)' deleted successfully”")]
        public void ThenVerifyTheResponseCodeIsAndMessageAccountDeletedSuccessfully(string statusCode, string message)
        {
            Assert.AreEqual(statusCode, _scenarioContext["responseStatusCode"]);

            dynamic jsonObject = JObject.Parse(_scenarioContext["jsonResponse"].ToString());

            Assert.AreEqual(message, jsonObject.Message);

        }

        [Given(@"Account with '([^']*)' and '([^']*)'")]
        public void GivenAccountWithAnd(string accountID, decimal amount)
        {
            depositPayload = new depositAccountRequest
            {
               
                amount = amount,
                accountNumber= accountID


            };

            _scenarioContext["depositPayload"]= depositPayload;
        }

        [When(@"Put endpoint triggered to deposit an account with above details")]
        public async Task WhenPutEndpointTriggeredToDepositAnAccountWithAboveDetailsAsync()
        {
            apiOperations api = new apiOperations(constansts.BaseURI);
            if (_scenarioContext != null)
            {
                RestResponse response = await api.PutData(constansts.deposit, _scenarioContext.Get<depositAccountRequest>("depositPayload"));
                if (response != null)
                {
                    _scenarioContext["responseStatusCode"] = (int)response.StatusCode;
                    dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
                    _scenarioContext["jsonResponse"] = jsonResponse;
                }
            }
        }

        [Then(@"Verify the response code is '([^']*)' and message “(.*) deposited to Account <accounId> successfully""")]
        public void ThenVerifyTheResponseCodeIsAndMessageDepositedToAccountAccounIdSuccessfully(int statusCode, string msg)
        {
            Assert.AreEqual(statusCode, _scenarioContext["responseStatusCode"]);

            dynamic jsonObject = JObject.Parse(_scenarioContext["jsonResponse"].ToString());

            Assert.Contains(msg, jsonObject.Message);

        }

        [When(@"Put endpoint triggered to withdraw an account with above details")]
        public async void WhenPutEndpointTriggeredToWithdrawAnAccountWithAboveDetails()
        {
            apiOperations api = new apiOperations(constansts.BaseURI);
            if (_scenarioContext != null)
            {
                RestResponse response = await api.PutData(constansts.withdraw, _scenarioContext.Get<depositAccountRequest>("depositPayload"));
                if (response != null)
                {
                    _scenarioContext["responseStatusCode"] = (int)response.StatusCode;
                    dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
                    _scenarioContext["jsonResponse"] = jsonResponse;
                }
            }
        }

        [Then(@"Verify the response code is '([^']*)' and message “(.*) withdrawn from Account <accounId> successfully""")]
        public void ThenVerifyTheResponseCodeIsAndMessageWithdrawnFromAccountAccounIdSuccessfully(int statusCode, string msg)
        {
            Assert.AreEqual(statusCode, (int)_scenarioContext["responseStatusCode"]);

            dynamic jsonObject = JObject.Parse(_scenarioContext["jsonResponse"].ToString());

            Assert.Contains(msg, jsonObject.Message);
        }
        [Then(@"Verify the response code is '([^']*)'")]
        public void ThenVerifyTheResponseCodeIs(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)_scenarioContext["responseStatusCode"]);
        }


    }
}
