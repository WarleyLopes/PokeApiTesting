using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections;

namespace Tests
{
    public class Tests
    {
        /// <summary>
        /// RestClient object to have the base common URL to PokeAPI resources and be shared throughout this test class
        /// </summary>
        public static RestClient restClient = new RestClient("https://pokeapi.co/api/v2");

        /// <summary>
        /// Test case data generator to health check all the API resources firstly
        /// </summary>
        public static IEnumerable GetAllResources
        {
            get
            {
                var response = restClient.Execute(new RestRequest());
                if (response.IsSuccessful)
                {
                    JToken jToken = JToken.Parse(response.Content);
                    foreach (JProperty property in jToken)
                    {
                        yield return new TestCaseData(property.Name, property.Value);
                    }
                }
                else throw new Exception("Something went wrong trying to list all PokeApi resources");
            }
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test,TestCaseSource("GetAllResources"),Parallelizable]
        public void Health_Check(string resourceName, string resourceUrl)
        {
            Assert.Pass();
        }
    }
}