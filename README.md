# LoanComplianceApi

## Running the project
1. [Install the .NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
2. Clone the repository
3. Open the solution in Visual Studio and click Run or navigate to the project directory with the .csproj filefor the API via command line and type dotnet run

## Project structure
Used the "Screaming Architecture" concept to structure the source code. Rather than relying on the architecture to define how the project is structured, it is focused around the domain. In this case, that domain is "Loan Compliance". This is probably overkill if this were to be a microservice, but if you were developing this in a monolith, I'd generally prefer structure the project by domains, so that you can look at a domain such as "Loan Compliance" and all the code related to that is right there.

## Approach
There's a "ComplianceEngine" that gets an IEnumerable<IValidator> injected. Each state has its own folder so that all the validations / compliance checks for that state are located together. Each validation inherits from a base class for the type of validation / compliance being performed (ex. Fee Validations, APR Validations, etc.). Each validation  type inherits from a BaseValidator that implements IValidator, and each state specific validation is added to the IoC container with:

```C#
services.AddScoped<IValidator, MarylandFeeValidation>();
```

Microsoft's DI container will then take every concrete class that implements IValidator and put it in an IEnumerable<IValidator>. The Compliance Engine then selects the validators relevant to the loan based on the State that the loan is in (Virginia, Maryland, etc.). It will then run each validation and save a list of the results in a LoanComplianceResult object, check if all of the validations succeeded, and return that to the client.

Each concrete class will need to override the public State property and set the State enum properly. It will then need to override the Validate() method. Once a concrete implementation of a state validation is created and added to the IoC container, it will then work with the compliance engine.

## Examples

#### Request where all validations will pass
POST /compliancecheck
```json
 {
   "State": "va",
   "loanAmount": 100000.00,
   "apr": ".04",
   "fees": [
 		{ "feeType": "floodCertification",
 		 "amount": 7000.00 
 		},
 		{ "feeType": "application",
 		 "amount": 0.00 
 		}
 	],
   "feePercentage": 10.0,
   "loanType": "conventional",
   "primaryOccupancy": false
 }
```

#### Response
200 OK
```json
{
  "isCompliant": true,
  "complianceChecks": [
    {
      "complianceType": "FeeValidation",
      "passed": true
    },
    {
      "complianceType": "AprValidation",
      "passed": true
    }
  ]
}
```

#### Request where a validation will fail
POST /compliancecheck
```json
 {
   "State": "va",
   "loanAmount": 100000.00,
   "apr": ".04",
   "fees": [
 		{ "feeType": "floodCertification",
 		 "amount": 8000.00 
 		},
 		{ "feeType": "application",
 		 "amount": 0.00 
 		}
 	],
   "feePercentage": 10.0,
   "loanType": "conventional",
   "primaryOccupancy": false
 }
```


#### Response
200 OK
```json
{
  "isCompliant": false,
  "complianceChecks": [
    {
      "complianceType": "FeeValidation",
      "passed": false
    },
    {
      "complianceType": "AprValidation",
      "passed": true
    }
  ]
}
```
