﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_SpecDB_Editor.Utils
{
    public class AlphanumericComparer : IComparer<string>
    {
        public int Compare(string value1, string value2)
        {
            string v1 = value1;
            string v2 = value2;

            if (v1.Length < v2.Length)
                return -1;
            else if (v1.Length > v2.Length)
                return 1;

            int min = v1.Length > v2.Length ? v2.Length : v1.Length;
            for (int i = 0; i < min; i++)
            {
                if (v1[i] < v2[i])
                    return -1;
                else if (v1[i] > v2[i])
                    return 1;
            }

            return 0;
        }
    }
}
