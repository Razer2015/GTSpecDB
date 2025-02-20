﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Core;
using Syroot.BinaryData.Memory;

using GTSpecDB.Core;
namespace GTSpecDB.Mapping.Tables
{
    public class Course : TableMetadata
    {
        public Course(SpecDBFolder folderType)
        {
            Columns.Add(new ColumnMetadata("ModelName", DBColumnType.String, "UnistrDB.sdb"));
            Columns.Add(new ColumnMetadata("NameJpn", DBColumnType.String, "UnistrDB.sdb"));
            Columns.Add(new ColumnMetadata("NameEng", DBColumnType.String, "UnistrDB.sdb"));

            if (folderType >= SpecDBFolder.GT5_JP3003)
                Columns.Add(new ColumnMetadata("PitCrew", DBColumnType.Int));
            else
            {
                Columns.Add(new ColumnMetadata("UnkStr1", DBColumnType.String, "UnistrDB.sdb"));
                if (folderType >= SpecDBFolder.GT5_TRIAL_EU2704 && folderType <= SpecDBFolder.GT5_TRIAL_JP2704)
                {
                    Columns.Add(new ColumnMetadata("UnkStr2", DBColumnType.String, "UnistrDB.sdb"));
                    Columns.Add(new ColumnMetadata("UnkStr3", DBColumnType.String, "UnistrDB.sdb"));
                }
                else if (folderType >= SpecDBFolder.GT5_PROLOGUE2813)
                {
                    Columns.Add(new ColumnMetadata("UnkStr2", DBColumnType.String, "UnistrDB.sdb"));
                }
            }
        

            Columns.Add(new ColumnMetadata("Condition", DBColumnType.Byte));
            Columns.Add(new ColumnMetadata("EntryMax", DBColumnType.Byte));
            Columns.Add(new ColumnMetadata("CourseTopology", DBColumnType.Byte));
            Columns.Add(new ColumnMetadata("NumberOfLanes", DBColumnType.Byte));
            Columns.Add(new ColumnMetadata("HasPitLane", DBColumnType.Bool));
            Columns.Add(new ColumnMetadata("GarageSide", DBColumnType.Byte));

            if (folderType >= SpecDBFolder.GT5_JP3010)
            {
                Columns.Add(new ColumnMetadata("StartingGridCount", DBColumnType.Byte));
                Columns.Add(new ColumnMetadata("PitStopCount", DBColumnType.Byte));
            }
        }
    }
}
