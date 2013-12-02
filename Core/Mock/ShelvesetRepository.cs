using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Core.Mock
{
    /// <summary>
    /// An implementation of <see cref="IShelvesetRepository"/> with fake data.
    /// </summary>
    [Export(typeof(IShelvesetRepository))]
    public class ShelvesetRepository : IShelvesetRepository
    {
        private readonly DataTable _shelvesets = new DataTable
        {
            Columns =
            {
                new DataColumn("ID", typeof(string))
                {
                    Caption = "ID",
                    ExtendedProperties =
                    {
                        {"Visible", false},
                        {"Searchable", false},
                    },
                },
                {"Owner", typeof(string)},
                new DataColumn("Name", typeof(string)) { Caption = "Shelveset Name" },
                new DataColumn("LastActivity", typeof(DateTime)) { Caption = "Last Activity" },
                {"Message", typeof(string)},
            },
            Rows =
            {
                {"ArgumentValidation with expression trees;odagenais", "Olivier Dagenais", "ArgumentValidation with expression trees", new DateTime(2012, 01, 04, 13, 47, 38), "Je suis un gros légume." },
                {"ca marche presque;odagenais", "Olivier Dagenais", "ca marche presque", new DateTime(2011, 12, 22, 17, 36, 55), "Je ne peux plus tomber en bas de ma chaise parce que je travaille debout."},
                {"proximity;enadeau", "Eric Nadeau", "proximity", new DateTime(2012, 01, 16, 15, 28, 56), "Je veux ravoir ma chaise!"},
            }
        };

        string IRepository.Name { get { return "Mock shelveset repository"; } }

        /// <summary>
        /// The <see cref="ILog"/> to send messages to.
        /// </summary>
        public ILog Log { get; set; }

        DataTable IShelvesetRepository.LoadShelvesets()
        {
            Info("Downloading...", 0, 0);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            const int items = 100;
            for (var i = 0; i < items; i++)
            {
                Info("Loading shelvesets", i, items - 1);
                Thread.Sleep(TimeSpan.FromMilliseconds(10));
            }
            return _shelvesets;
        }

        private void Info(string message, int progressValue, int progressMaximum)
        {
            if (Log != null)
            {
                Log.Info(message, progressValue, progressMaximum);
            }
        }

        IList<MenuAction> IShelvesetRepository.GetShelvesetActions()
        {
            return new[]
            {
                new MenuAction("create", "Create shelveset", true, () => Debug.WriteLine("Creating a new shelveset")),
            };
        }

        IList<MenuAction> IShelvesetRepository.GetShelvesetActions(object shelvesetId)
        {
            return new[]
            {
                new MenuAction("unshelve", "&Unshelve", true, () => 
                    Debug.WriteLine("Unshelving shelveset '{0}'", new []{shelvesetId})),
            };
        }

        DataTable IShelvesetRepository.GetShelvesetChanges(object shelvesetId)
        {
            Debug.WriteLine("Scanning for changes in {0}...", shelvesetId);
            switch (shelvesetId.ToString())
            {
                case "ca marche presque;odagenais":
                    return Data.RefactorInternetPendingChanges;
                default:
                    return Data.EmptyPendingChanges;
            }
        }

        string IShelvesetRepository.GetShelvesetMessage(object shelvesetId)
        {
            var id = (string) shelvesetId;
            Debug.WriteLine("Obtaining the message for revision {0}...", new[] { shelvesetId });
            var escapedId = id.Replace("'", "''");
            var rows = _shelvesets.Select("[ID] = '{0}'".FormatInvariant(escapedId));
            var row = rows[0];
            var value = row["Message"];
            return (string) value;
        }

        string IShelvesetRepository.ComputeShelvesetDifferences(object shelvesetId, IEnumerable<object> changeIds)
        {
            var numberOfPendingChanges = changeIds.Count();
            var suffix = ( numberOfPendingChanges == 1 ? "" : "s" );
            Debug.WriteLine("Computing revision differences for {0} change{1}...", numberOfPendingChanges, suffix);
            return Data.HardcodedDifferences;
        }

        IList<MenuAction> IShelvesetRepository.GetActionsForShelvesetChanges(object shelvesetId, IEnumerable<object> changeIds)
        {
            var numberOfShelvesetChangeIds = changeIds.Count();
            if (numberOfShelvesetChangeIds == 0)
            {
                return MenuAction.EmptyList;
            }
            var suffix = ( ( numberOfShelvesetChangeIds == 1 ) ? "" : "s" );
            return new[]
            {
                new MenuAction("diff", "&Diff", true,
                    () => Debug.WriteLine("Diffing {0} change{1}", numberOfShelvesetChangeIds, suffix)),
                new MenuAction("blame", "&Blame", true,
                    () => Debug.WriteLine("Launching blame for {0} change{1}", numberOfShelvesetChangeIds, suffix)),
            };
        }
    }
}
