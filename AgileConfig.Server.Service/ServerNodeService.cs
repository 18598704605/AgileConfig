﻿using AgileConfig.Server.Data.Entity;
using AgileConfig.Server.IService;
using AgileConfig.Server.Data.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgileConfig.Server.Common;
using Microsoft.EntityFrameworkCore;

namespace AgileConfig.Server.Service
{
    public class ServerNodeService : IServerNodeService
    {
        private AgileConfigDbContext _dbContext;

        public ServerNodeService(ISqlContext context)
        {
            _dbContext = context as AgileConfigDbContext;
        }

        public async Task<bool> AddAsync(ServerNode node)
        {
            await _dbContext.ServerNodes.AddAsync(node);
            int x = await _dbContext.SaveChangesAsync();
            return x > 0;
        }

        public async Task<bool> DeleteAsync(ServerNode node)
        {
            node = _dbContext.ServerNodes.Find(node.Address);
            if (node != null)
            {
                _dbContext.ServerNodes.Remove(node);
            }
            int x = await _dbContext.SaveChangesAsync();
            return x > 0;
        }

        public async Task<bool> DeleteAsync(string address)
        {
            var node = _dbContext.ServerNodes.Find(address);
            if (node != null)
            {
                _dbContext.ServerNodes.Remove(node);
            }
            int x = await _dbContext.SaveChangesAsync();
            return x > 0;
        }

        public Task<List<ServerNode>> GetAllNodesAsync()
        {
            return _dbContext.ServerNodes.ToListAsync();
        }

        public Task<ServerNode> GetAsync(string address)
        {
            return _dbContext.ServerNodes.FindAsync(address).AsTask();
        }

        public async Task<bool> UpdateAsync(ServerNode node)
        {
            _dbContext.Update(node);
            var x = await _dbContext.SaveChangesAsync();

            return x > 0;
        }
    }
}