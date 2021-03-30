using Common.Infrastructure.Responses;
using Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using Ubiety.Dns.Core;

namespace ReadData
{
    public class ReadData:IReadData
    {
        public string ConvertToJsonFromTextFile(string fileName)
        {
            DataTable data = new DataTable();
            try
            {
                this.createDirectory(Constants.FilePath);
                FileStream fileStream = new FileStream(Constants.FilePath + fileName, FileMode.Open);
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] items = line.Trim().Split('|');
                        if (data.Columns.Count == 0)
                        {
                            for (int i = 0; i < items.Length; i++)
                                data.Columns.Add(new DataColumn(getColumnNames(i) , typeof(string)));
                        }
                        data.Rows.Add(items);

                    }
                }
            }
            catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return string.Empty;
            }
            return this.convertDataTableToJson(data);
        }
        private string getColumnNames(int index)
        {
            string columnName = string.Empty;
            switch (index)
            {
                case 0:
                    columnName = "ID";
                    break;
                case 1:
                    columnName = "Name";
                    break;
                case 2:
                    columnName = "Surname";
                    break;
                case 3:
                    columnName = "Email";
                    break;
                case 4:
                    columnName = "Gender";
                    break;
                case 5:
                    columnName = "IPAddress";
                    break;
                default:
                    columnName = "";
                    break;
            }
            return columnName;
        }

        public string ReadFromJsonFile(string fileName)
        {
            try
            {
                FileStream fileStream = new FileStream(Constants.FilePath + fileName, FileMode.Open);
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return string.Empty;
            }
        }

        private string convertDataTableToJson(DataTable dataTable)
        {
            try
            {
                string JSONresult = JsonConvert.SerializeObject(dataTable);
                this.createDirectory(Constants.FilePath);
                File.WriteAllText(Constants.FilePath+ Constants.ConvertedJsonFileName, JSONresult);
                return JSONresult;
            }
            catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return string.Empty;
            }
        }
        private void createDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                this.grantAccess(path);
            }
        }
        private void grantAccess(string fullPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(fullPath);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
        }
    }
}
