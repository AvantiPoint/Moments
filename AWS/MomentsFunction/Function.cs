using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using LambdaSharp;
using LambdaSharp.ApiGateway;
using Moments;
using Amazon.DynamoDBv2.Model;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Moments.Api.MomentsFunction {

    public abstract class ARecord {

        //--- Properties ---
        public string PK { get; set; }
    }

    public class AccountRecord : ARecord {

        //--- Properties ---
        public string SK { get; } = "account";
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }
    }

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
            if(request.Account.Email == null) {
                throw AbortBadRequest("missing email address");
            }
            if(request.Account.Username.Length < 2) {
                throw AbortBadRequest("username too short");
            }
            try {
                var document = Document.FromJson(SerializeJson(new AccountRecord {
                    PK = request.Account.Username,
                    Password = HashText(request.Account.Password),
                    Email = request.Account.Email,
                    Name = request.User?.Name,
                    ProfileImage = request.User?.ProfileImage
                }));
                await _table.PutItemAsync(document, new PutItemOperationConfig {
                    ConditionalExpression = new Expression {
                        ExpressionStatement = "attribute_not_exists(PK)"
                    }
                });
                return new AccountRegistrationResponse { };
            } catch(ConditionalCheckFailedException) {
                throw AbortBadRequest("account is already registered");
            }
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request) {
            var document = await _table.GetItemAsync(Document.FromJson(SerializeJson(new AccountRecord {
                PK = request.Account.Username
            })));
            if(document == null) {
                throw AbortNotFound("invalid username or password");
            }
            var record = DeserializeJson<AccountRecord>(document.ToJson());
            if(record.Password != HashText(request.Account.Password)) {
                throw AbortNotFound("invalid username or password");
            }
            return new LoginResponse {
                SessionToken = await EncryptSecretAsync(request.Account.Username)
            };
        }

        private string HashText(string text) {
            using(var sha = SHA256.Create()) {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
                return string.Join("", bytes.Select(x => x.ToString("X2")));
            }
        }
    }
}
