﻿using Microsoft.WindowsAzure.Storage.Table;
using System;

public class CCTVData : TableEntity
{
    public CCTVData()
    {

    }

    public void AssignKey()
    {
        this.RowKey = shortid.ShortId.Generate(true, false, 10).ToUpper();
        this.PartitionKey = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
    }
    public DateTime Tanggal { get; set; }
    public string CCTVName { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public string Tags { get; set; }

}