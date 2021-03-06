﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gihan.Renamer.Models;
using Gihan.Renamer.Models.Enums;
using Gihan.Storage.Core;
using Gihan.Storage.Core.Base;

namespace Gihan.Renamer
{
    public abstract class Renamer
    {
        protected abstract StorageHelper StorageHelper { get; }

        public IEnumerable<bool> Rename(IEnumerable<MoveOrder> renameOrders)
        {
            var renames = new List<MoveOrder>();
            foreach (var renameOrder in renameOrders)
            {
                try
                {
                    var item = StorageHelper.GetItem(renameOrder.Path);
                    item.Move(renameOrder.DestPath);
                }
                catch (Exception err)
                {
                    renameOrder.Message = $"{err.GetType().FullName}: {err.Message}";
                }
                renames.Add(renameOrder);
            }
            using (var db = new RenameDbContext())
            {
                var gp = new RenameGroup()
                {
                    DateTime = DateTime.Now,
                    Renames = renames
                };
                db.RenameGroups.Insert(gp);
            }
            return renames.Select(r => string.IsNullOrWhiteSpace(r.Message));
        }
    }
}
