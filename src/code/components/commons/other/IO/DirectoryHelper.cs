﻿using Godot;
using System.Collections.Generic;
using System.IO;
using System;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.commons.errors.exceptions;

namespace SpaceMiner.src.code.components.commons.other.IO
{
    public class DirectoryHelper
    {
        public DirectoryHelper() { }
        
        /// <summary>
        /// Gets all directory paths leading to a specific file/directory.<br></br> NOTE: Does not skip the last directory.
        /// </summary>
        /// <returns>Paths to directories</returns>
        public static string[] GetParentDirectories(string path)
        {
            string[] paths = path.Split(new char[] { '/',  '\\' });
            List<string> directoriesList = new List<string>();
            string continuousPath = "";
            foreach (string stringPath in paths)
            {
                continuousPath = Path.Join(continuousPath, stringPath);
                if (File.Exists(continuousPath) || Directory.Exists(continuousPath))
                {
                    FileAttributes attributes = File.GetAttributes(continuousPath);
                    if (attributes == FileAttributes.Directory)
                    {
                        directoriesList.Add(continuousPath);
                    }
                }
                else
                {
                    directoriesList.Add(continuousPath);
                }
            }
            return directoriesList.ToArray();
        }

        public static void CopyDirectory(string dirPath, string destinationDirPath)
        {
            try
            {
                destinationDirPath = Path.Join(destinationDirPath, new DirectoryInfo(dirPath).Name);
                ValidateUserDirectories(dirPath);
                ValidateUserDirectories(destinationDirPath);

                string[] filePaths = Directory.GetFiles(dirPath);
                foreach (string file in filePaths)
                {
                    File.Copy(file, destinationDirPath);
                }
            }
            catch (Exception ex)
            {
                GD.Print($"Something went wrong: {ex}");
            }
        }

        /// <summary>
        /// Checks directories in a path and creates missing.<br></br>
        /// </summary>
        public static void ValidateUserDirectories(string path)
        {
            foreach (var dirPath in GetParentDirectories(path))
            {
                ValidateDirectory(dirPath);
            }
        }

        /// <summary>
        /// Checks if directory in a path exists and creates the directory if missing.
        /// </summary>
        /// <param name="path">Directory path</param>
        public static void ValidateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}