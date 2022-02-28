using Identity.Domain;
using Identity.Entities.Entities.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core
{
    public class TrackChangesManager
    {

        private List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> _addedEntities;
        private List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> _updateEntities;
        private List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> _deletedEntities;
        private ApplicationDbContext _context;

       private ContextAccessor _contextAccessor { get; set; }

        public TrackChangesManager(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context )
        {
            _contextAccessor = new ContextAccessor( httpContextAccessor);
            _context = context;
        }
        private void Track(List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> changeEntityList)
        {
            changeEntityList.ForEach(item =>
            {
                IBaseEntity entity = item.Entity as IBaseEntity;

                // update Operation
                if (entity.Id > 0)
                {
                    entity.ModifyDate = DateTime.Now;
                    entity.ModifyUser = _contextAccessor.GetUser();
                }
                // insert operation
                else
                {
                    entity.CreateUser = _contextAccessor.GetUser();
                    entity.CreateDate = DateTime.Now;
                }
            });
        }

        public void TrackChanges()
        {
            Track(_addedEntities);
            Track(_updateEntities);
            Track(_deletedEntities);
        }

        public void DetectChanges()
        {
            _updateEntities = _context.ChangeTracker.Entries()
                  .Where(t => t.State == Microsoft.EntityFrameworkCore.EntityState.Modified).ToList();
            _addedEntities = _context.ChangeTracker.Entries()
                 .Where(t => t.State == Microsoft.EntityFrameworkCore.EntityState.Added).ToList();
            _deletedEntities = _context.ChangeTracker.Entries()
                 .Where(t => t.State == Microsoft.EntityFrameworkCore.EntityState.Deleted).ToList();

            // change state SOFT DELETE
            _deletedEntities.ForEach(d =>
            {
                d.State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                IBaseEntity entity = d.Entity as IBaseEntity;
                entity.Deleted = true;
            });
        }

        public int GetLastIdInsert()
        {
            int ret = -1;
            var item = _addedEntities.LastOrDefault();
            if (item != null)
                ret = ((IBaseEntity)item.Entity).Id;

            return ret;
        }
    }
}
