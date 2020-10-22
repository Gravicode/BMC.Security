using BMC.Security.Web.Blazor.Data;
using BMC.Security.Web.Blazor.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BMC.Security.Web.Blazor.Helpers
{
    public class AzureTableHelper
    {
        CloudTable cloudTable;

        public AzureTableHelper(string TableName)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(AppConstants.BlobConString);
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            Console.WriteLine(TableName);
            //string tableName = Console.ReadLine();
            cloudTable = tableClient.GetTableReference(TableName);
            CreateNewTable(cloudTable);

        }

        //internal object InsertData(DataAudiensi data)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<bool> InsertData(CCTVData data)
        {
            try
            {
                if (data != null)
                {
                    TableOperation tableOperation = TableOperation.Insert(data);
                    await cloudTable.ExecuteAsync(tableOperation);
                    Console.WriteLine("Record inserted");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> InsertDataAbsen(AbsenData data)
        {
            try
            {
                if (data != null)
                {
                    TableOperation tableOperation = TableOperation.Insert(data);
                    await cloudTable.ExecuteAsync(tableOperation);
                    Console.WriteLine("Record inserted");
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }


        public async void CreateNewTable(CloudTable table)
        {
            if (!await table.CreateIfNotExistsAsync())
            {
                Console.WriteLine("Table {0} already exists", table.Name);
                return;
            }
            Console.WriteLine("Table {0} created", table.Name);
        }

        public async Task<List<CCTVData>> GetCCTVData()
        {
            
            // Create the table query.
            var datas = new List<CCTVData>();

            TableQuery<CCTVData> query = new TableQuery<CCTVData>().Where(TableQuery.GenerateFilterCondition("CCTVName", QueryComparisons.NotEqual, ""));

            // Print the fields for each customer.
            TableContinuationToken token = null;
            int counter = 0;
            do
            {
             
                TableQuerySegment<CCTVData> resultSegment = await cloudTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;

                foreach (CCTVData entity in resultSegment.Results)
                {
                    entity.Tanggal = entity.Tanggal.AddHours(7);
                    datas.Add(entity);
                    counter++;
                }
                if (counter > 100) break;
            } while (token != null);

            return datas;
        }

        public async Task<List<AbsenData>> GetAbsenData()
        {

            // Create the table query.
            var datas = new List<AbsenData>();

            TableQuery<AbsenData> query = new TableQuery<AbsenData>().Where(TableQuery.GenerateFilterCondition("IDS", QueryComparisons.NotEqual, ""));

            // Print the fields for each customer.
            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<AbsenData> resultSegment = await cloudTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;

                foreach (AbsenData entity in resultSegment.Results)
                {
                    entity.Tanggal = entity.Tanggal.AddHours(7);
                    datas.Add(entity);
                }
            } while (token != null);

            return datas;
        }

    }
}