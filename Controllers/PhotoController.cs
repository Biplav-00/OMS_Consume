using ConsumeOMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace ConsumeOMS.Controllers
{
    public class PhotoController : Controller
    {
        private string baseUrl = "https://localhost:7068/api/services/";
        
        private  string photoUrl = "";
        public async Task<IActionResult> UploadPhoto()
        {
            return View();
        }

        public async Task<IActionResult> CallApi(ImageModel model)
        {
            //if (ModelState.IsValid)
            //{
                ImageModel imageModel = new ImageModel
                {
                    ServiceName = model.ServiceName,
                    serviceDescription = model.serviceDescription,
                    image = model.image

                };
                if (imageModel != null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        using (var formData = new MultipartFormDataContent())
                        {
                            formData.Add(new StringContent(imageModel.ServiceName!), "serviceName");
                            formData.Add(new StringContent(imageModel.serviceDescription!), "serviceDescription");
                            if (imageModel.image != null)
                            {
                                var imageContent = new StreamContent(imageModel.image.OpenReadStream());
                                formData.Add(imageContent, "image", imageModel.image.FileName);
                            }
                            HttpResponseMessage responseMessage = await client.PostAsync(baseUrl, formData);
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                                var responseObject = JObject.Parse(responseContent);
                                var imagePath = responseObject?["value"]?["imagePath"]?.ToString();                          
                                string base64Image = ConvertImageToBase64(imagePath!);
                            //ViewData["Base64Image"] = base64Image;
                            //return RedirectToAction("DisplayPhoto","Photo", new { imageUrl = base64Image });
                                DisplayPhoto(base64Image);
                                return View("DisplayPhoto");
                        }
                            else
                            {
                                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<object>>(responseContent);
                                var msg = apiResponse?.message;
                                ViewData["Message"] += msg;
                            }
                        }
                    }
                }

                return View("UploadPhoto");
            //}
            //else
            //{
            //    return View("UploadPhoto", model);
            //}
        }

        [HttpGet]
        public  async Task<IActionResult> DisplayPhoto(string imageUrl)
        {           
            ViewBag.photoUrl = imageUrl;
            return View();           
        }
        private string ConvertImageToBase64(string imagePath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}
