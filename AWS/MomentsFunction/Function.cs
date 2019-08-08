using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using LambdaSharp;
using LambdaSharp.ApiGateway;
using Moments;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Moments.Api.MomentsFunction {

    public class Function : ALambdaApiGatewayFunction {

        //--- Fields ---
        private IAmazonDynamoDB _dynamoDbClient;
        private Table _table;

        //--- Methods ---
        public override async Task InitializeAsync(LambdaConfig config) {
            _dynamoDbClient = new AmazonDynamoDBClient();
            _table = Table.LoadTable(_dynamoDbClient, config.ReadDynamoDBTableName("Table"));
        }

        public async Task<AccountRegistrationResponse> RegistrationAsync(AccountRegistrationRequest request) {

            // TODO: if account is already registered

            // TODO: insert new account record
            var document = Document.FromJson(SerializeJson(new {
                PK = request.Account.Username,
                SK = "account"
            }));
            await _table.PutItemAsync(document);

            return new AccountRegistrationResponse {

                // TODO: set UserId
            };
        }
    }
}
