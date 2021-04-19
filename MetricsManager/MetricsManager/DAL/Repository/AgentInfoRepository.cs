using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.Requests.ControllerRequests;
using MetricsTool.SQLiteConnectionSettings;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.DAL.Repository
{
    public class AgentInfoRepository : IAgentInfoRepository
    {
        private ISqlSettingsProvider _sqliteConnection;

        public AgentInfoRepository (ISqlSettingsProvider sqliteConnection)
        {
            _sqliteConnection = sqliteConnection;
        }

        //попытка зарегистрировать агента
        public bool RegisterAgent(AgentInfoRequest agentInfoRequest)
        {
            var agent = CheckAgentInBase(agentInfoRequest);
            if (agent == null)
            {
                using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
                {
                    connection.Execute("INSERT INTO agentsbase(agentAddress,agentId) VALUES(@agentAddress,@agentId)",
                                       // анонимный объект с параметрами запроса
                                       new
                                       {
                                           agentId = agentInfoRequest.AgentId,
                                           agentAddress = agentInfoRequest.AgentAddress
                                       });
                }
                return true;
            }
            else { return false; }
        }

        public IList<AgentInfo> GetAllAgents()
        {

            using (var conncetion = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                return conncetion.Query<AgentInfo>("SELECT * FROM agentsbase").ToList();
            }
        }

        public AgentInfo CheckAgentInBase(AgentInfoRequest agentCheck)
        {
            //Если такого агента еще нет в базе, вернется null
            using (var connection = new SQLiteConnection(_sqliteConnection.GetConnectionSQLite()))
            {
                return connection.QueryFirstOrDefault<AgentInfo>("SELECT * FROM agentsbase WHERE agentAddress=@agentAddress",
                 new
                 {
                     agentAddress = agentCheck.AgentAddress
                 });
            }
        }
    }
}
