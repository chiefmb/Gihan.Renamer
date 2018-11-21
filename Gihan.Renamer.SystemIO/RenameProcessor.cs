﻿using Gihan.Helpers.String;
using Gihan.Renamer.Models;
using Gihan.Renamer.Models.Enums;
using Gihan.Storage.Core.Base;
using Gihan.Storage.SystemIO;
using System.Collections.Generic;
using System.Linq;

namespace Gihan.Renamer.SystemIO
{
    public class RenameProcessor : Gihan.Renamer.RenameProcessor
    {
        protected override StorageHelper StorageHelper { get; }
        public RenameProcessor()
        {
            Storage.SystemIO.Base.StorageHelper.Init();
            StorageHelper = StorageHelper.Creat(); //to init as SysIO
        }

        public IEnumerable<RenameOrder> ProcessReplace
            (IEnumerable<string> itemsPath, IEnumerable<ReplacePattern> patterns, RenameFlags renameFlags)
        {
            return ProcessReplace(itemsPath.Select(ip => StorageHelper.GetItem(ip)), patterns, renameFlags);
        }

        public IEnumerable<RenameOrder> ProcessReplace
            (string rootFolderPath, IEnumerable<ReplacePattern> patterns, RenameFlags renameFlags)
        {
            return ProcessReplace(new Folder(rootFolderPath), patterns, renameFlags);
        }
    }
}