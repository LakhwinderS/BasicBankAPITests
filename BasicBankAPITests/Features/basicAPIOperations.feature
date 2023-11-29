Feature: basiAPIOperations

As a User I want to do some basic banking operation using API

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
Given an Account with accountid '<accountID>'
When Delete endpoint triggered to delete an account with above details
Then Verify the response code is 'statusCode' and message “Account '<accountID>' deleted successfully”
Examples: 
| accountID	    | statusCode |
| 10000         | 200        |

@deposit
Scenario Outline: A user can deposit to an account
Given Account with 'accountID' and 'amount'
When Put endpoint triggered to deposit an account with above details
Then Verify the response code is 'statusCode' and message 'depositMessage'
Examples: 
| accountID | statusCode | amount | messgae              |
| 10000     | 200        | 1000   | deposit Successfully |

@withdraw
Scenario Outline: A user can withdraw from an account.
Given Account with '<accountID>' and '<amount>'
When Put endpoint triggered to withdraw an account with above details
Then Verify the response code is 'statusCode' and message “<amount> withdrawn from Account <accounId> successfully"
Examples: 
| accountID | statusCode | amount |
| 10000     | 200        | 1000   |

@Negative
Scenario Outline: Verify 400 bad request while creating Account
Given Account with 'initialBalance' , 'accountName','address' 
When POST endpoint triggered to create new account with above details
Then Verify the response code is 'statusCode' 
Examples: 
| initialBalance | accountName   | address            | statusCode |
| 10000          | ^^^%^%^^ `	 | Ahmedabad, Gujarat | 400        |

@Negative
Scenario Outline: A user tryinh to  delete account deleted Account
Given an Account with 'accountID'
When Delete endpoint triggered to delete an account with above details
Then Verify the response code is 'statusCode' 
Examples: 
| accountID	    | statusCode |
| 10000         | 400        |

@withdraw
Scenario Outline: A user can withdraw from an account with invalid amount
Given Account with 'accountID' and 'amount'
When Put endpoint triggered to withdraw an account with above details
Then Verify the response code is 'statusCode'
Examples: 
| accountID | statusCode | amount |
| 10000     | 400       | 1000   |