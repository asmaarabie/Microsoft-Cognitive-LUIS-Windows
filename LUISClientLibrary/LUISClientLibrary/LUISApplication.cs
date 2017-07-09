//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license.
//
// Microsoft Cognitive Services (formerly Project Oxford): https://www.microsoft.com/cognitive-services

//
// Microsoft Cognitive Services (formerly Project Oxford) GitHub:
// https://github.com/Microsoft/ProjectOxford-ClientSDK

//
// Copyright (c) Microsoft Corporation
// All rights reserved.
//
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LUISClientLibrary
{
    /// <summary>
    /// Represent LUIS App structure
    /// </summary>
    public class LUISApplication
    {
        /// <summary>
        /// base url for any request
        /// </summary>
        private const string _baseUrl = "https://westus.api.cognitive.microsoft.com/luis/api/v2.0/apps/";
        /// <summary>
        /// http request is done with this object
        /// </summary>
        private readonly HttpClient _client = new HttpClient();
        /// <summary>
        /// Application ID 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Application name 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Application discription
        /// </summary>
        public string Discription { get; set; }
        /// <summary>
        /// Application Culture e.g. "en-us","en-uk","fr-fr"
        /// </summary>
        public string Culture { get; set; }
        /// <summary>
        /// Application usage scenario e.g. "IoT"
        /// </summary>
        public string UsageScenario { get; set; }
        /// <summary>
        /// Application domain e.g. "Comics"
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// Versions Counter 
        /// </summary>
        public int VersionsCount { get; set; }
        /// <summary>
        /// Date and time the application was created e.g. "2017-01-31T16:15:54Z"
        /// </summary>
        public DateTime CreatedDateTime { get; set; }
        /// <summary>
        /// End point hits counter "how many time the end point was hit"
        /// </summary>
        public int EndPointHitsCount { get; set; }
        /// <summary>
        /// Application current active version e.g "1.0"
        /// </summary>
        public string ActiveVersion { get; set; }
        /// <summary>
        /// LIst of application end points
        /// </summary>
        public List<EndPoint> EndPoints { get; set; }
        /// <summary>
        /// check if we can us the add appilcation function
        /// if this object already correspond to an app then we decline AddApplication fuction
        /// </summary>
        private bool _isUsed { get; set; }
        /// <summary>
        /// Enum to spcify user HttpRequest type
        /// </summary>
        private enum RequstType { Get,Post,Delete,Put };
        /// <summary>
        /// Defualt Constructor , intialize every string to empty , date time to min value and set counters to 0
        /// </summary>
        public LUISApplication()
        {
            ID = Name = Discription = Culture = UsageScenario = Domain = ActiveVersion = "";
            VersionsCount = EndPointHitsCount = 0;
            CreatedDateTime = DateTime.MinValue;
            EndPoints = new List<EndPoint>();
        }
        /// <summary>
        /// Get applcation info and save it to the object fields 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subKey"></param>
        public async void GetApplicationInfo(string AppId,string subKey)
        {
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();// headers to be sent to httprequest
            headers.Add(new KeyValuePair<string, string>("Ocp-Apim-Subscription-Key", subKey));
            HttpContent cont = new StringContent(string.Empty);
            Task<JObject> httpResp = HttpRequest(_baseUrl, headers, cont, RequstType.Get);
            FillFeilds(httpResp.Result);
        }
        /// <summary>
        /// fill object fields with data priveded by JObject 
        /// </summary>
        /// <param name="json"></param>
        private void FillFeilds(JObject json)
        {
            this.ID = json["id"].ToString();
            this.Name = json["name"].ToString();
            this.Discription = json["description"].ToString();
            this.Culture = json["culture"].ToString();
            this.UsageScenario = json["usageScenario"].ToString();
            this.Domain = json["domain"].ToString();
            this.VersionsCount = int.Parse(json["versionsCount"].ToString());
            this.CreatedDateTime = DateTime.Parse( json["createdDateTime"].ToString());
            this.EndPointHitsCount = int.Parse(json["endpointHitsCount"].ToString());
            this.ActiveVersion = json["activeVersion"].ToString();
        }
        private async Task<JObject> HttpRequest(string url,List<KeyValuePair<string,string>> headers,HttpContent content,RequstType option)
        {
            _client.DefaultRequestHeaders.Clear();
            foreach(KeyValuePair<string,string> head in headers)
            {
                _client.DefaultRequestHeaders.Add(head.Key, head.Value);
            }
            if (option==RequstType.Get)
            {
                HttpResponseMessage resp = await _client.GetAsync(url);
                return new JObject(resp.ToString());
            }
            return new JObject();
        }
    }
}
