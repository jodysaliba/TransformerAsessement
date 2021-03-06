﻿using System;
using System.Collections.Generic;
using System.IO;

namespace TransformerAssessment.Core.Objects {
    class Norm {
        public string name;
        public string[] conditions;    // high, low, etc
        public string[] variables;     // h2, co, c2h2, etc
        public double[][] limits;        // values (if blank, enter -1, if non-number, enter -2)
        public List<string[]> rawNorm = new List<string[]>();   // raw norm matrix

        public Norm(string path) {
            char[] delimiters = new char[] { ',' };
            using (StreamReader reader = new StreamReader(path)) {
                bool recordData = false;
                while (true) {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;
                    if (!string.IsNullOrWhiteSpace(line)) {
                        string[] splitRow = line.Split(delimiters); // split csv row into array
                        if (splitRow[0] == "Name:")
                            name = splitRow[1];
                        if (splitRow[0] == "variable")
                            recordData = true;  // start adding data to variables
                        if (recordData && splitRow.Length > 0)
                            rawNorm.Add(splitRow);  // add row array into rawNorm
                    }
                }
                parseRawNorm();
            }
        }

        static string ConvertStringArrayToString(string[] array) { return string.Join(".", array); }

        static string ConvertStringArrayToString(double[] array) { return string.Join(".", Convert.ToString(array)); }

        private void parseRawNorm() {
            // create conditions[], variables, & limits[][]
            conditions = new string[rawNorm[0].Length]; // length = #cols
            variables = new string[rawNorm.Count];      // length = #rows
            limits = new double[variables.Length][];
            for (int i=0; i<variables.Length;i++)
                limits[i] = new double[conditions.Length]; // limits[row, col]

            for (int row = 0; row < limits.Length; row++) {
                for (int col = 0; col < limits[row].Length; col++) {
                    if (col == 0)
                        variables[row] = rawNorm[row][col];
                    if (row == 0)
                        conditions[col] = rawNorm[row][col];
                    if (row != 0 && col != 0) {  // if empty, then -1 ---- if "False", then -2
                        if (rawNorm[row][col].Length == 0)
                            limits[row][col] = -1;
                        else if (rawNorm[row][col] == "False")
                            limits[row][col] = -2;
                        else
                            limits[row][col] = Convert.ToDouble(rawNorm[row][col]);
                    }
                }
            }
        }
    }   
}
