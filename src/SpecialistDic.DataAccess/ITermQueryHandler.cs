using System.Threading.Tasks;

namespace SpecialistDic.DataAccess
{
    public interface ITermQueryHandler
    {
        Task<TermQueryResult> ExecuteQueryAsync(TermQuery query);
    }
}