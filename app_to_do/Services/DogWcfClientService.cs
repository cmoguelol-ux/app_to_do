//Hecho por Carlos Arturo Moguel Olvera

using app_to_do.Models;

namespace app_to_do.Services
{
    public class DogWcfClientService
    {
        public async Task<DogViewModel> ObtenerPerritoDelDia()
        {
            var model = new DogViewModel();

            try
            {
                var client = new DogServiceReference.DogServiceClient();

                string imagenUrl =
                    await client.ObtenerPerritoDelDiaAsync();
                if (string.IsNullOrWhiteSpace(imagenUrl))
                {
                    model.Mensaje = "No se pudo obtener el perrito del dia.";
                    return model;
                }

                model.ImagenUrl = imagenUrl;
                model.Mensaje = "Perrito del dia recuperado correctamente.";
            }
            catch (Exception ex)
            {
                model.Mensaje =
                    "Ocurrio un error al consumir el servicio WCF: "
                    + ex.Message;
            }

            return model;
        }
    }
}