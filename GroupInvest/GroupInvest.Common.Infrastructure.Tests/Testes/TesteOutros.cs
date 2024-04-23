using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace GroupInvest.Common.Infrastructure.Tests.Testes
{
    [TestClass]
    public class TesteOutros
    {
        [TestMethod]
        public void ValidarClaims()
        {
            string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6InlER2lvSVZPN1dYeGx0QUJPZ0Z1RVEiLCJ0eXAiOiJhdCtqd3QifQ.eyJuYmYiOjE1OTk5NzEyMzIsImV4cCI6MTU5OTk3NDgzMiwiaXNzIjoiaHR0cHM6Ly93b3JsZHN0b3JlLWRhbmllbHRzLWlhbW1pY3Jvc2VydmljZS1pZGVudGl0eS5henVyZXdlYnNpdGVzLm5ldCIsImF1ZCI6WyJBZG1pbk1vZHVsZV9hcGkiLCJHcm91cGludmVzdC1BcGxpY2F0aXZvLUFQSSIsIkdyb3VwaW52ZXN0LU1lbnNhbGlkYWRlcy1BUEkiLCJHcm91cGludmVzdC1QYXJ0aWNpcGFudGVzLUFQSSJdLCJjbGllbnRfaWQiOiJQb3N0bWFuIiwic3ViIjoiYjc0ZmFhZWMtZmZmOC00OGE5LWFhMWEtM2QyYjkyNzc3YjQ0IiwiYXV0aF90aW1lIjoxNTk5OTcxMjMxLCJpZHAiOiJsb2NhbCIsIm5hbWUiOiJhZG1pbiIsInJvbGUiOlsiQXBwVXNlciIsIkFkbWluIl0sInNjb3BlIjpbIkFkbWluTW9kdWxlX2FwaSIsIkdyb3VwaW52ZXN0LUFwbGljYXRpdm8tQVBJIiwiR3JvdXBpbnZlc3QtTWVuc2FsaWRhZGVzLUFQSSIsIkdyb3VwaW52ZXN0LVBhcnRpY2lwYW50ZXMtQVBJIl0sImFtciI6WyJwd2QiXX0.Qd4cFMzybquBCkiRuPiGd7q0JJwnaH0CFWS-1cvmWzgIPm4nMLmRKRef36SdlfjwdMyvUtMTOOQ0MRWiYlwCVwqOyb3DJNsqsLRogc3dpkVDXvZ0hguN-ko2DhSNzoLMgLcnHZ4SrAg1SH9L_FTjoPT2yO_4X8x5gRCIu4KuT3u64avxBcCoHK1ELLRkM6muBorxL5HYkDLe4RTay88Owc3NA4Lg100_g4kEo0D5ZD-3Jkfg8Z_j21Mn5RiBTCX2rKsW5cHYwUKg2Fj4zbIXYCQstssAlLjPKRVHzq26ZZAzRURa9mSzCiznvR87EwgCDilKquw-vRdk4yzgVG-gNg";
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenStream = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var roleClaims = tokenStream.Claims.Where(c => c.Type == "role").Select(c => c.Value);

            Assert.IsTrue(roleClaims.Contains("AppUser"));
        }

        [TestMethod]
        public void MongoDbProducao()
        {
            string connectionString = "";
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase db = client.GetDatabase("groupinvest01");

            db.GetCollection<Participante>("Teste").InsertOne(new Participante { Id = 1, Nome = "Daniel Cesar Tostes Santana" });

            Assert.IsNotNull(db);
        }
    }
}
