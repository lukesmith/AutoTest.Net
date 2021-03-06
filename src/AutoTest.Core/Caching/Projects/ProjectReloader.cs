﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoTest.Core.Caching.Projects
{
    class ProjectReloader : IReload<Project>
    {
        private ICache _cache;

        public ProjectReloader(ICache cache)
        {
            _cache = cache;
        }

        #region IReload<Project> Members

        public void MarkAsDirty(Project record)
        {
            removeRemoteReferences(record);
            var referencedBys = record.Value.ReferencedBy;
            record.Reload();
            record.Value.AddReferencedBy(referencedBys);
            _cache.Get<Project>(record.Key);
        }

        #endregion

        private void removeRemoteReferences(Project record)
        {
            foreach (var reference in record.Value.References)
            {
                var referencedProject = _cache.Get<Project>(reference);
                referencedProject.Value.RemoveReferencedBy(record.Key);
            }
        }
    }
}
