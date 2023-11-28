using BasicBankAPITests.Models;
using BasicBankAPITests.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using TechTalk.SpecFlow;

namespace BasicBankAPITests.StepDefinitions
{
    [Binding]
    public class BasicAPIOperationsStepDefinitions
    {
        Object createPayload = null;
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
            apiOperations.ValidateJSON(_scenarioContext["jsonResponse"].ToString());
        }

        [Given(@"an Account with '([^']*)'")]
        public void GivenAnAccountWith(string accountID)
        {
            throw new PendingStepException();
        }

        [When(@"Delete endpoint triggered to delete an account with above details")]
        public void WhenDeleteEndpointTriggeredToDeleteAnAccountWithAboveDetails()
        {
            throw new PendingStepException();
        }

        [Then(@"Verify the response code is '([^']*)' and message “Account '([^']*)' deleted successfully”")]
        public void ThenVerifyTheResponseCodeIsAndMessageAccountDeletedSuccessfully(string statusCode, string p1)
        {
            throw new PendingStepException();
        }

        [Given(@"Account with '([^']*)' and '([^']*)'")]
        public void GivenAccountWithAnd(string accountID, string amount)
        {
            throw new PendingStepException();
        }

        [When(@"Put endpoint triggered to deposit an account with above details")]
        public void WhenPutEndpointTriggeredToDepositAnAccountWithAboveDetails()
        {
            throw new PendingStepException();
        }

        [Then(@"Verify the response code is '([^']*)' and message “(.*) deposited to Account <accounId> successfully""")]
        public void ThenVerifyTheResponseCodeIsAndMessageDepositedToAccountAccounIdSuccessfully(string statusCode, int p1)
        {
            throw new PendingStepException();
        }

        [When(@"Put endpoint triggered to withdraw an account with above details")]
        public void WhenPutEndpointTriggeredToWithdrawAnAccountWithAboveDetails()
        {
            throw new PendingStepException();
        }

        [Then(@"Verify the response code is '([^']*)' and message “(.*) withdrawn from Account <accounId> successfully""")]
        public void ThenVerifyTheResponseCodeIsAndMessageWithdrawnFromAccountAccounIdSuccessfully(string statusCode, int p1)
        {
            throw new PendingStepException();
        }
    }
}
