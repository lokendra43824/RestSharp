using JsNodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {


        RestClient client = new RestClient(" http://localhost:3000");

       // [TestMethod]
        public void GetTheRecords()
        {

            IRestResponse response = getEmployeeList();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            List<EmployeeDetails> dataresponse = JsonConvert.DeserializeObject<List<EmployeeDetails>>(response.Content);

            Assert.AreEqual(6, dataresponse.Count);

            Console.WriteLine(response.Content);
        }

        private IRestResponse getEmployeeList()
        {
            RestRequest request = new RestRequest("/Employee", Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        [TestMethod]
        public void givenEmployeeOnPost_returnAddedEmployee()
        {
            RestRequest request = new RestRequest("/employee", Method.POST);
            JObject jObject = new JObject();

            jObject.Add("name", "lokendra");
            jObject.Add("salary", "40000");


            request.AddParameter("application/json", jObject, ParameterType.RequestBody);


            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);



            EmployeeDetails dataresponse = JsonConvert.DeserializeObject<EmployeeDetails>(response.Content);

            Assert.AreEqual("lokendra", dataresponse.name);
            Assert.AreEqual("40000", dataresponse.salary);

        }



    }
}
