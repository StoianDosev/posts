using System.Threading.Tasks;
using WebApi.Responses;

namespace WebApi.Clients
{
    public interface IApiClient
    {
        Task<ResponseModel> GetAll(string name);
    }
}