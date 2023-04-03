using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using WAD_MVC_12252.Models;

namespace WAD_MVC_12252.Controllers
{
    public class GenreController : Controller
    {
        public const string baseUrl = "http://localhost:9796/";
        private readonly Uri clntBaseAddress = new Uri(baseUrl);
        private readonly HttpClient clnt;

        public GenreController()
        {
            clnt = new HttpClient();
            clnt.BaseAddress = clntBaseAddress;
        }
        private void HeaderClearing()
        {
            // Clearing default headers
            clnt.DefaultRequestHeaders.Clear();

            // Define the request type of the data
            clnt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Genre
        public async Task<ActionResult> Index()
        {
            List<Genre> genres = new List<Genre>();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await clnt.GetAsync("api/Genre");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                genres = JsonConvert.DeserializeObject<List<Genre>>(responseMessage);
            }
            return View(genres);
        }

        // GET: Genre/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Genre genre = new Genre();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await clnt.GetAsync($"api/Genre/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                genre = JsonConvert.DeserializeObject<Genre>(responseMessage);
            }
            return View(genre);
        }

        // GET: Genre/Create
        public async Task<ActionResult> CreateAsync()
        {
            Genre genre = new Genre();
            HeaderClearing();
            return View(genre);
        }

        // POST: Genre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                string createGenreInfo = JsonConvert.SerializeObject(genre);
                StringContent stringContentInfo = new StringContent(createGenreInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage createHttpResponseMessage = clnt.PostAsync(clnt.BaseAddress + "api/Genre", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(genre);
        }

        // GET: Genre/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Genre genre = new Genre();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await clnt.GetAsync($"api/Genre/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                genre = JsonConvert.DeserializeObject<Genre>(responseMessage);
            }

            return View(genre);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Genre genre)
        {
            if (ModelState.IsValid)
            {
                string createSubjectInfo = JsonConvert.SerializeObject(genre);
                StringContent stringContentInfo = new StringContent(createSubjectInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage editHttpResponseMessage = clnt.PutAsync(clnt.BaseAddress + $"api/Genre/{id}", stringContentInfo).Result;
                if (editHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(genre);
        }

        // GET: Subject/Delete/5
        public ActionResult Delete(int id)
        {
            Genre genreInfo = new Genre();
            HttpResponseMessage getSubjectHttpResponseMessage = clnt.GetAsync(clnt.BaseAddress + $"api/Genre/{id}").Result;
            if (getSubjectHttpResponseMessage.IsSuccessStatusCode)
            {
                genreInfo = getSubjectHttpResponseMessage.Content.ReadAsAsync<Genre>().Result;
            }
            return View(genreInfo);
        }

        // POST: Subject/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Genre subject)
        {
            HttpResponseMessage deleteSubjectHttpResponseMessage = clnt.DeleteAsync(clnt.BaseAddress + $"api/Genre/{id}").Result;
            if (deleteSubjectHttpResponseMessage.IsSuccessStatusCode)
            {
                //subjectInfo = getSubjectHttpResponseMessage.Content.ReadAsAsync<Subject>().Result;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

    }
}
