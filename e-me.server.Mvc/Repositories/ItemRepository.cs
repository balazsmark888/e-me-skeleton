using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_me.Model.Models;
using e_me.server.Mvc.Data;
using e_me.server.Mvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_me.server.Mvc.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;

        public ItemRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
