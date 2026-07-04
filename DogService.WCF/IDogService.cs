using System.ServiceModel;

namespace DogService.WCF
{
    [ServiceContract]
    public interface IDogService
    {
        [OperationContract]
        string ObtenerPerritoDelDia();
    }
}