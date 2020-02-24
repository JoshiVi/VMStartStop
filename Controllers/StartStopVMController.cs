using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;


using System.Net;

namespace StartStopVMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartStopVMController : ControllerBase
    {
        // GET api/StartStopVM
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/StartStopVM/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscriptionId">01cb0aba-23e0-457f-b099-98168a9f75aa</param>
        /// <param name="resourceGroupName">Hi2VM</param>
        /// <param name="vmName">StartStopVM</param>
        /// <returns></returns>
        [Route("/StartVM/{subscriptionId}/{resourceGroupName}/{vmName}")]
        public ActionResult<string> StartVM(string subscriptionId, string resourceGroupName, string vmName)
        {

            string azureapi = "https://management.azure.com/subscriptions/" + subscriptionId + "/resourceGroups/" + resourceGroupName + "/providers/Microsoft.Compute/virtualMachines/" + vmName + "/start?api-version=2019-07-01";
            Uri uri = new Uri(String.Format(azureapi));

            // Create the request
            var httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
            httpWebRequest.Headers.Add(System.Net.HttpRequestHeader.Authorization, "Bearer " + GetAccessToken());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = 0;

            HttpWebResponse httpResponse = null;
            httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            return vmName + " Started" ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscriptionId">01cb0aba-23e0-457f-b099-98168a9f75aa</param>
        /// <param name="resourceGroupName">Hi2VM</param>
        /// <param name="vmName">StartStopVM</param>
        /// <returns></returns>
        [Route("/DeallocateVM/{subscriptionId}/{resourceGroupName}/{vmName}")]
        public ActionResult<string> DeallocateVM(string subscriptionId, string resourceGroupName, string vmName)
        {
            string azureapi = "https://management.azure.com/subscriptions/"+ subscriptionId + "/resourceGroups/" + resourceGroupName +"/providers/Microsoft.Compute/virtualMachines/"+vmName+ "/deallocate?api-version=2019-07-01";
            Uri uri = new Uri(String.Format(azureapi));

            // Create the request
            var httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
            httpWebRequest.Headers.Add(System.Net.HttpRequestHeader.Authorization, "Bearer " + GetAccessToken());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            httpWebRequest.ContentLength = 0;

            HttpWebResponse httpResponse = null;
            //httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            var response = httpWebRequest.GetResponse();

            return vmName + " Deallocated";
        }

        private string GetAccessToken()
        {
            string tenantId = "]...................e1";
            string clientId = "cac94293]..................."; // Application id
            string subscriptionId = "0]...................a";
            string clientSecret = "d]...................";

            string authContextURL = "https://login.windows.net/" + tenantId;
            var authenticationContext = new AuthenticationContext(authContextURL);
            var credential = new ClientCredential(clientId: clientId, clientSecret: clientSecret);
            var result = authenticationContext.AcquireTokenAsync("https://management.azure.com/", credential);
            return result.Result.AccessToken; 
        }

    }
}
