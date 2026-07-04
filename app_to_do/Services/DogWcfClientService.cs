// Hecho por Carlos Arturo Moguel Olvera s

using app_to_do.Models;
using System.ServiceModel;

namespace app_to_do.Services
{
    public class DogWcfClientService
    {
        private const string ServiceUrl = "http://localhost:58751/DogService.svc";

        public async Task<DogViewModel> ObtenerPerritoDelDia()
        {
            var model = new DogViewModel();

            try
            {
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(ServiceUrl);

                var factory = new ChannelFactory<IDogService>(binding, endpoint);
                IDogService client = factory.CreateChannel();

                string imagenUrl = await Task.Run(() =>
                    client.ObtenerPerritoDelDia()
                );

                try
                {
                    ((IClientChannel)client).Close();
                    factory.Close();
                }
                catch
                {
                    ((IClientChannel)client).Abort();
                    factory.Abort();
                }

                if (string.IsNullOrWhiteSpace(imagenUrl))
                {
                    model.Mensaje = "No se pudo obtener el perrito del día.";
                    return model;
                }

                model.ImagenUrl = imagenUrl;
                model.Mensaje = "Perrito del día recuperado correctamente.";
            }
            catch (Exception ex)
            {
                model.Mensaje = "Ocurrió un error al consumir el servicio WCF: " + ex.Message;
            }

            return model;
        }
    }

    [ServiceContract]
    public interface IDogService
    {
        [OperationContract]
        string ObtenerPerritoDelDia();
    }
}