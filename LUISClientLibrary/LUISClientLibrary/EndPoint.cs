using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUISClientLibrary
{
    /// <summary>
    /// End point object 
    /// contains end point data
    /// </summary>
    class EndPoint
    {
        /// <summary>
        /// Endpoint version Id
        /// </summary>
        public string VersionID { get; set; }
        /// <summary>
        /// Is this endpoint staging one ?
        /// </summary>
        public bool IsStaging { get; set; }
        /// <summary>
        /// end point url 
        /// </summary>
        public string Url { set; get; }
        /// <summary>
        /// Key assigned to end point 
        /// </summary>
        public string AssignedKey { get; set; }
        /// <summary>
        /// end point region e.g "westus"
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// Datetime the end point was published 
        /// </summary>
        public DateTime PublishedDateTime { get; set; }

    }
}
