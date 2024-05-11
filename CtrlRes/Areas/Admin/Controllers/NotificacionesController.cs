using CtrlRes.Data;
using CtrlRes.Data.Migrations;
using CtrlRes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CtrlRes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NotificacionesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private string? propOrArr_Id;
        private string? propOrArr_IdArr;
        private string? propOrArr_IdProp;

        public NotificacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Obtener el listado de privadas para mostrar en la lista desplegable
            var privadas = _context.Privadas.ToList();

            ViewBag.Privadas = privadas;

            return View();
        }

        [HttpGet]
        public IActionResult GetViviendas(string foreignKey)
        {

            // Realiza una consulta a la base de datos para obtener los valores que coinciden con la clave foránea
            var data = _context.Viviendas.Where(t => t.Privada_Id == foreignKey).Select(t => new {
                Value = t.Id,
                Name = t.Numero
            }).ToList();

            // Devuelve los datos en formato JSON
            return Json(data);
        }

        [HttpGet]
        public IActionResult GetPropArre(string foreignKey) //Obtener propietario y arrendatario
        {

            // Realiza una consulta a la base de datos para obtener los nombres de los propietarios y arrendatarios correspondientes
            var propietarioNombre = _context.Propietarios.FirstOrDefault(p => p.Vivienda_Id == foreignKey)?.Nombre;
            var arrendatarioNombre = _context.Arrendatarios.FirstOrDefault(a => a.Vivienda_Id == foreignKey)?.Nombre;


            // Devuelve los nombres en formato JSON
            return Json(new { PropietarioNombre = propietarioNombre, ArrendatarioNombre = arrendatarioNombre });
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(Notificaciones notificaciones)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("America/Mexico_City");
            DateTimeOffset fechaRegistroMx = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tz);
            notificaciones.FechaRegistro = fechaRegistroMx.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");

            if (ModelState.IsValid)
            {

                _context.Notificaciones.Add(notificaciones);
                _context.SaveChanges();

                String authorizationToken = "AAAAGS5H130:APA91bGdSkid_j_qcn2czZik1XS5hTknjcdU6aGukorgZ1bPuubb-6VRmmbVI9u9mKq4AHRXxakJIsdj26Cp619Bd8EzXira7dLh2fCnk6cZTpVD02tjALUudVtLFtlBP_FXPfX9tHr5";

                if (notificaciones.Para == "Arrendatario")
                {
                    propOrArr_Id = _context.Arrendatarios.FirstOrDefault(u => u.Vivienda_Id == notificaciones.Vivienda_Id)?.Id;

                    if (propOrArr_Id == null)
                    {
                        return NotFound();
                    }
                }

                if (notificaciones.Para == "Propietario")
                {
                    propOrArr_Id = _context.Propietarios.FirstOrDefault(u => u.Vivienda_Id == notificaciones.Vivienda_Id)?.Id;

                    if (propOrArr_Id == null)
                    {
                        return NotFound();
                    }
                }

                if (propOrArr_Id != null)
                {
                    var PushToken = _context.ApplicationUser.FirstOrDefault(u => u.UsuarioTipo == notificaciones.Para && u.PropOrArr_Id == propOrArr_Id)?.PushToken;

                    if (PushToken == null)
                    { return RedirectToAction(nameof(Index)); }

                    var payload = new
                    {
                        to = PushToken,
                        notification = new
                        {
                            title = notificaciones.Titulo,
                            body = notificaciones.Mensaje,
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

                    return RedirectToAction(nameof(Index));
                }

                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                if (notificaciones.Para == "Ambos")
                {

                    propOrArr_IdArr = _context.Arrendatarios.FirstOrDefault(u => u.Vivienda_Id == notificaciones.Vivienda_Id)?.Id;
                    propOrArr_IdProp = _context.Propietarios.FirstOrDefault(u => u.Vivienda_Id == notificaciones.Vivienda_Id)?.Id;

                    if (propOrArr_IdArr == null || propOrArr_IdProp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var PushToken = _context.ApplicationUser.FirstOrDefault(u => u.UsuarioTipo == "Arrendatario" && u.PropOrArr_Id == propOrArr_IdArr)?.PushToken;
                        var PushToken2 = _context.ApplicationUser.FirstOrDefault(u => u.UsuarioTipo == "Propietario" && u.PropOrArr_Id == propOrArr_IdProp)?.PushToken;

                        var payload = new
                        {
                            to = PushToken,
                            notification = new
                            {
                                title = notificaciones.Titulo,
                                body = notificaciones.Mensaje,
                                android_channel_id = "siaumex"
                            }
                        };

                        if (PushToken != null)
                        {
                            var httpClient = new HttpClient();
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
                            var json = JsonConvert.SerializeObject(payload);
                            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                            var response = await httpClient.PostAsync("https://fcm.googleapis.com/fcm/send", content);
                            response.EnsureSuccessStatusCode();
                            var responseContent = await response.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);
                        }

                        if ( PushToken2 != null) { 
                        var payload2 = new
                        {
                            to = PushToken2,
                            notification = new
                            {
                                title = notificaciones.Titulo,
                                body = notificaciones.Mensaje,
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

                        return View();

                    }

                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                }

            }
            return View();
        }



        //*************************************************************************************************************************************************

        [HttpPost]
        public async Task<IActionResult> ForeingPostAsync(Notificaciones notificaciones, String callingController)
        {


            if (ModelState.IsValid)
            {

                _context.Notificaciones.Add(notificaciones);
                _context.SaveChanges();

                String authorizationToken = "AAAAGS5H130:APA91bGdSkid_j_qcn2czZik1XS5hTknjcdU6aGukorgZ1bPuubb-6VRmmbVI9u9mKq4AHRXxakJIsdj26Cp619Bd8EzXira7dLh2fCnk6cZTpVD02tjALUudVtLFtlBP_FXPfX9tHr5";


                if (notificaciones.Para == "Arrendatario" || notificaciones.Para == "Propietario")

                {

                    if (notificaciones.Para == "Arrendatario")
                    {
                        propOrArr_Id = _context.Arrendatarios.FirstOrDefault(u => u.Vivienda_Id == notificaciones.Vivienda_Id)?.Id;

                        if (propOrArr_Id == null)
                        {
                            return NotFound();
                        }
                    }

                    if (notificaciones.Para == "Propietario")
                    {
                        propOrArr_Id = _context.Propietarios.FirstOrDefault(u => u.Vivienda_Id == notificaciones.Vivienda_Id)?.Id;

                        if (propOrArr_Id == null)
                        {
                            return NotFound();
                        }
                    }

                    if (propOrArr_Id != null)
                    {
                        var PushToken = _context.ApplicationUser.FirstOrDefault(u => u.UsuarioTipo == notificaciones.Para && u.PropOrArr_Id == propOrArr_Id)?.PushToken;

                        if (PushToken != null)
                        {

                            var payload = new
                            {
                                to = PushToken,
                                notification = new
                                {
                                    title = notificaciones.Titulo,
                                    body = notificaciones.Mensaje,
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
                        return RedirectToAction("Index", callingController);
                    }

                }

                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                if (notificaciones.Para == "Ambos")
                {

                    propOrArr_IdArr = _context.Arrendatarios.FirstOrDefault(u => u.Vivienda_Id == notificaciones.Vivienda_Id)?.Id;
                    propOrArr_IdProp = _context.Propietarios.FirstOrDefault(u => u.Vivienda_Id == notificaciones.Vivienda_Id)?.Id;

                    if (propOrArr_IdArr == null || propOrArr_IdProp == null)
                    {
                        return NotFound();
                    }
                    else
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
                                    title = notificaciones.Titulo,
                                    body = notificaciones.Mensaje,
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
                                    title = notificaciones.Titulo,
                                    body = notificaciones.Mensaje,
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
                        return Ok();

                    }

                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                }

            }
            return View();
        }


    }
    }

