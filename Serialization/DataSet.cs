﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Serialization
{
    [Serializable()]
    public class DataSet
    {
        #region Serialization Interface
        [System.Xml.Serialization.XmlAttribute]
        public string Name;
        public Serialization.Query Query = new Query();
        public List<Field> Fields = new List<Field>();
        #endregion

        #region Helper Methods
        /// <summary>
        /// copy the query parameters values (probably from the QueryString) into this report's parameters (in this object)
        /// </summary>
        /// <param name="webParameters"></param>
        public void AssignParameters(System.Collections.Hashtable webParameters)
        {
            foreach (Serialization.QueryParameter param in this.Query.QueryParameters)
            {
                string paramName = param.Name.Replace("@", "");
                //if this report param was passed as an arg to the report, then populate it
                if (webParameters[paramName] != null)
                {
                    //param.Value =  webParameters[paramName].ToString();
                    param.Value = webParameters[paramName].ToString();
                }
            }
        }

        /// <summary>
        /// Fills a DataTable from this report's query
        /// </summary>
        /// <param name="DBConnectionString">a working DB connection string</param>
        /// <returns>one DataTable</returns>
        public System.Data.DataTable GetDataTable(string DBConnectionString)
        {
            System.Data.DataTable re = new System.Data.DataTable();
            //OLEDB parameters
            if (DBConnectionString.Contains("Provider="))
                using (System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(this.Query.CommandText, DBConnectionString))
                {
                    if (this.Query.QueryParameters.Count > 0)
                    {
                        foreach (Serialization.QueryParameter param in this.Query.QueryParameters)
                        {
                            string paramName = param.Name.Replace("@", "");
                            //OLEDB chokes on the @symbol, it prefers ? marks
                            da.SelectCommand.CommandText = da.SelectCommand.CommandText.Replace(param.Name, "?");

                            switch (param.DataType)
                            {
                                case "Text":
                                    da.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(paramName, System.Data.OleDb.OleDbType.VarWChar) { Value = param.Value });
                                    break;
                                case "Boolean":
                                    da.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(paramName, System.Data.OleDb.OleDbType.Boolean) { Value = param.Value });
                                    break;
                                case "DateTime":
                                    da.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(paramName, System.Data.OleDb.OleDbType.Date) { Value = param.Value });
                                    break;
                                case "Integer":
                                    da.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(paramName, System.Data.OleDb.OleDbType.Integer) { Value = param.Value });
                                    break;
                                case "Float":
                                    da.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(paramName, System.Data.OleDb.OleDbType.Decimal) { Value = param.Value });
                                    break;
                                default:
                                    da.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(paramName, param.Value));
                                    break;
                            }
                        }
                    }
                    da.Fill(re);
                }
            else //Sql Client parameters
                using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(this.Query.CommandText, DBConnectionString))
                {
                    foreach (Serialization.QueryParameter param in this.Query.QueryParameters)
                    {
                        string paramName = param.Name.Replace("@", "");

                        switch (param.DataType)
                        {
                            case "Text":
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(paramName, System.Data.SqlDbType.VarChar) { Value = param.Value });
                                break;
                            case "Boolean":
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(paramName, System.Data.SqlDbType.Bit) { Value = param.Value });
                                break;
                            case "DateTime":
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(paramName, System.Data.SqlDbType.DateTime) { Value = param.Value });
                                break;
                            case "Integer":
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(paramName, System.Data.SqlDbType.Int) { Value = param.Value });
                                break;
                            case "Float":
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(paramName, System.Data.SqlDbType.Decimal) { Value = param.Value });
                                break;
                            default:
                                da.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(param.Name, param.Value));
                                break;
                        }
                    }
                    da.Fill(re);
                }

            re.TableName = this.Name;
            return re;
        }
        #endregion

    }
}