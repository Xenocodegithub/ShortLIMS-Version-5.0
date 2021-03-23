using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Serialization

{
    /// <summary>
    /// child of DataSet
    /// </summary>
    [Serializable()]
    public class Query
    {
        #region Serialization Interface
        public string DataSourceName;
        public List<Serialization.QueryParameter> QueryParameters = new List<Serialization.QueryParameter>();
        public string CommandText;
        #endregion
    }
}