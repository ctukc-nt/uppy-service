using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using UPPY.Logic.Classes;

namespace UPPY.ServerService
{
    [ServiceContract]
    public interface IUppyService
    {
        [OperationContract]
        List<FileDrawingsOrders> GetAllFileDrawingsOrders();
    }
}