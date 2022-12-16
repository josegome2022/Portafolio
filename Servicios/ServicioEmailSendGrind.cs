using ProyectoPortafolio.Servicios;
using ProyectoPortafolio.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ProyectoPortafolio.Servicios
{
    //Salida de la llave

    public interface IservicioEmail
    {
        Task Enviar(Contacto contacto);
    }
    public class ServicioEmailSendGrind : IservicioEmail
    {
        private readonly IConfiguration configuration;
        public ServicioEmailSendGrind(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    

    public async Task Enviar(Contacto contacto)
        {
            //variables para la llave, email y nombre

            var apikey = configuration.GetValue<string>("SENDGRID_APIKEY");
            var email = configuration.GetValue<string>("SENDGRIDFROM");
            var nombre = configuration.GetValue<string>("SENDGRID_NOMBRE");

            //Cuerpo del correo

            var cliente = new SendGridClient(apikey);
            var from = new EmailAddress(email, nombre);
            var subjet = $"El Cliente {contacto.Email} quiere contactarse";
            var to = new EmailAddress(email, nombre);
            var mensajeTextoPlando = contacto.Mensaje;

            //Mensaje de HTML paea nosotros
            var contenidoHTML = $@"De:{contacto.Nombre} -
                                   Email: {contacto.Email}
                                   Mensaje: {contacto.Mensaje}";

            var singleEmail = MailHelper.CreateSingleEmail(from, to, subjet, mensajeTextoPlando, contenidoHTML);

            var respuesta = await cliente.SendEmailAsync(singleEmail);
        }
    }
}

