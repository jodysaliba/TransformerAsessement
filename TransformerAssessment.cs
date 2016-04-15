﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using TransformerAssessment.Core.Helpers;

namespace TransformerAssessment {
    static class TransformerAssessment {
        public static string normDir;
        public static string exportsDir;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            string PROGRAM_PATH = Application.StartupPath;
            
            // Get list of files in Sub-folders
            NormLoader.initializeNorms();
            EquipmentLoader.initializeEquipment();
            TestDataLoader.initializeTestData();

            normDir = NormLoader.getNormsDir();
            string[] normList = NormLoader.getNormsPathList();
            exportsDir = EquipmentLoader.getEquipmentDir();
            string[] toaExportsList = EquipmentLoader.getEquipmentPathList();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormHome());
        }
    }
}
