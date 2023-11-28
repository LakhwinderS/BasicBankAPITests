Feature: basiAPIOperations

A short summary of the feature

@create
Scenario Outline: A user can create account
Given Account with 'initialBalance' , 'accountName','address' 
When POST endpoint triggered to create new account with above details
Then Verify the response code is 'statusCode' and no error is returned
And Verify the success message “Account created successfully”
And Verify the account details are correctly returned in the JSON response
Examples: 
| initialBalance | accountName   | address            | statusCode |
| 10000          | Rajesh Mittal | Ahmedabad, Gujarat | 200        |

@delete
Scenario Outline: A user can delete account
Given an Account with 'accountID'
When Delete endpoint triggered to delete an account with above details
Then Verify the response code is 'statusCode' and message “Account '<accountID>' deleted successfully”
Examples: 
| accountID	    | statusCode |
| 10000         | 200        |

@deposit
Scenario Outline: A user can deposit to an account
Given Account with 'accountID' and 'amount'
When Put endpoint triggered to deposit an account with above details
Then Verify the response code is 'statusCode' and message “<amount> deposited to Account <accounId> successfully"
Examples: 
| accountID | statusCode | amount |
| 10000     | 200        | 1000   |	

@withdraw
Scenario Outline: A user can withdraw from an account.
Given Account with 'accountID' and 'amount'
When Put endpoint triggered to withdraw an account with above details
Then Verify the response code is 'statusCode' and message “<amount> withdrawn from Account <accounId> successfully"
Examples: 
| accountID | statusCode | amount |
| 10000     | 200        | 1000   |