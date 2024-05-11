using CtrlRes.Areas.Admin.Controllers;
using CtrlRes.Data;
using CtrlRes.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;

namespace CtrlRes.BackgroundTasks
{
    public class AppointmentNotificationService : IHostedService, IDisposable
    {

        private Timer _timer;
        private string? propOrArr_IdArr;
        private string? propOrArr_IdProp;
        private readonly ApplicationDbContext _context;

        public AppointmentNotificationService(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 15, 00, 0, DateTimeKind.Utc);
            if (now > scheduledTime)
                scheduledTime = scheduledTime.AddDays(1);

            var timeUntilScheduledTime = scheduledTime - now;
            _timer = new Timer(DoWork, null, timeUntilScheduledTime, TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            //var appointments = await _context.Asambleas.Where(a => DateTimeOffset.ParseExact(a.FechaProgramada, "yyyy-MM-dd'T'HH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture).ToOffset(DateTimeOffset.UtcNow.Offset) > DateTimeOffset.UtcNow).ToListAsync();

            var asambleas = _context.Asambleas.ToList();

            var appointments = asambleas.Where(d => DateTimeOffset.ParseExact(d.FechaProgramada, "yyyy-MM-ddTHH:mm:ss.fffffffK", CultureInfo.InvariantCulture) > DateTimeOffset.UtcNow).ToList();

            foreach (var appointment in appointments)
            {
                var dateScheduled = DateTimeOffset.ParseExact(appointment.FechaProgramada, "yyyy-MM-ddTHH:mm:ss.fffffffK", CultureInfo.InvariantCulture);

                //DateTime dateScheduled = DateTime.ParseExact(appointment.FechaProgramada, "yyyy-MM-ddTHH:mm:ss.fffffffffK", CultureInfo.InvariantCulture);

                if (dateScheduled.Date == DateTimeOffset.UtcNow.Date.AddDays(1))
                {
                    String authorizationToken = "AAAAGS5H130:APA91bGdSkid_j_qcn2czZik1XS5hTknjcdU6aGukorgZ1bPuubb-6VRmmbVI9u9mKq4AHRXxakJIsdj26Cp619Bd8EzXira7dLh2fCnk6cZTpVD02tjALUudVtLFtlBP_FXPfX9tHr5";
                    string[] valores = appointment.Notificacion.Split(',');

                    foreach (string valor in valores)
                    {

                        propOrArr_IdArr = _context.Arrendatarios.FirstOrDefault(u => u.Vivienda_Id == valor)?.Id;
                        propOrArr_IdProp = _context.Propietarios.FirstOrDefault(u => u.Vivienda_Id == valor)?.Id;

                        if (!(propOrArr_IdArr == null || propOrArr_IdProp == null))
                        {                    
                            var PushToken = _context.ApplicationUser.FirstOrDefault(u => u.UsuarioTipo == "Arrendatario" && u.PropOrArr_Id == propOrArr_IdArr)?.PushToken;
                            var PushToken2 = _context.ApplicationUser.FirstOrDefault(u => u.UsuarioTipo == "Propietario" && u.PropOrArr_Id == propOrArr_IdProp)?.PushToken;


                            if (PushToken != null)
                            {
                                var payload = new
                                {
                                    to = PushToken,
                                    notification = new
                                    {
                                        title = appointment.Nombre,
                                        body = "Esta asamblea tendrá lugar el día de mañana, revisa tus notificaciones para más detalles",
                                        android_channel_id = "siaumex"
                                    }
                                };

                                var httpClient = new HttpClient();
                                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
                                var json = JsonConvert.SerializeObject(payload);
                                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                                var response = await httpClient.PostAsync("https://fcm.googleapis.com/fcm/send", content);
                                response.EnsureSuccessStatusCode();
                                var responseContent = await response.Content.ReadAsStringAsync();
                                Console.WriteLine(responseContent);
                            }

                            if (PushToken2 != null)
                            {
                                var payload2 = new
                                {
                                    to = PushToken2,
                                    notification = new
                                    {
                                        title = "Recordatorio: " + appointment.Nombre,
                                        body = "Esta asamblea tendrá lugar el día de mañana, revisa tus notificaciones para más detalles",
                                        android_channel_id = "siaumex"
                                    }
                                };

                                var httpClient2 = new HttpClient();
                                httpClient2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
                                var json2 = JsonConvert.SerializeObject(payload2);
                                var content2 = new StringContent(json2, System.Text.Encoding.UTF8, "application/json");
                                var response2 = await httpClient2.PostAsync("https://fcm.googleapis.com/fcm/send", content2);
                                response2.EnsureSuccessStatusCode();
                                var responseContent2 = await response2.Content.ReadAsStringAsync();
                                Console.WriteLine(responseContent2);
                            }
                            
                        }

                        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    }


                }
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

}
