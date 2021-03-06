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
        public void GetTheList()
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

       // [TestMethod]
        public void AddEmployee_returnAddedEmployee()
        {
            RestRequest request = new RestRequest("/Employee", Method.POST);
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
       // [TestMethod]
        public void AddMutipleEmployees_returnAddedEmployees()
        {
            List<EmployeeDetails> employee = new List<EmployeeDetails>();
            employee.Add(new EmployeeDetails("karan", "50000"));
            employee.Add(new EmployeeDetails("saurabh", "60000"));

            foreach (var emp in employee)
            {



                RestRequest request = new RestRequest("/Employee", Method.POST);

                JObject jObject = new JObject();
                jObject.Add("name", emp.name);
                jObject.Add("salary", emp.salary);

                request.AddParameter("application/json", jObject, ParameterType.RequestBody);


                IRestResponse response = client.Execute(request);
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);

            }

            IRestResponse newResponse = getEmployeeList();

            Assert.AreEqual(newResponse.StatusCode, HttpStatusCode.OK);

            List<EmployeeDetails> dataresponse = JsonConvert.DeserializeObject<List<EmployeeDetails>>(newResponse.Content);

            Assert.AreEqual(21, dataresponse.Count);

            Console.WriteLine(newResponse.Content);


        }
      //    [TestMethod]
        public void TakeEmployeeId_updateEmployeeName()
        {
            RestRequest request = new RestRequest("/Employee/10", Method.PUT);
            JObject jObject = new JObject();

            jObject.Add("name", "Ajay");
            jObject.Add("salary", "80000");


            request.AddParameter("application/json", jObject, ParameterType.RequestBody);


            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            EmployeeDetails dataresponse = JsonConvert.DeserializeObject<EmployeeDetails>(response.Content);

            Assert.AreEqual("Ajay", dataresponse.name);
            Assert.AreEqual("80000", dataresponse.salary);

        }
           [TestMethod]
        public void TakeEmployeeId_deleteEmployee()
        {
            RestRequest request = new RestRequest("/employee/14", Method.DELETE);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            Console.WriteLine(response.Content);
        }


    }
}
