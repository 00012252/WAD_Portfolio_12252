using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WAD_MVC_12252.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WAD_MVC_12252.Controllers
{
    public class MovieController : Controller
    {
        // The Definition of Base URL
        public const string baseUrl = "http://localhost:9796/";
        Uri ClientBaseAddress = new Uri(baseUrl);
        HttpClient clnt;

        // Constructor for initiating request to the given base URL publicly
        public MovieController()
        {
            clnt = new HttpClient();
            clnt.BaseAddress = ClientBaseAddress;

        }

        public void HeaderClearing()
        {
            // Clearing default headers
            clnt.DefaultRequestHeaders.Clear();

            // Define the request type of the data
            clnt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Movie
        public async Task<ActionResult> Index()
        {


            //// Creating the list of new Movies list
            List<Movie> MovieInfo = new List<Movie>();


            HeaderClearing();

            // Sending Request to the find web api Rest service resources using HTTPClient
            HttpResponseMessage httpResponseMessage = await clnt.GetAsync("api/Movie");

            // If the request is success
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // storing the web api data into model that was predefined prior
                var responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;

                MovieInfo = JsonConvert.DeserializeObject<List<Movie>>(responseMessage);
            }
            return View(MovieInfo);
        }

        // GET: MovieController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Movie movie = new Movie();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await clnt.GetAsync($"api/Movie/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                movie = JsonConvert.DeserializeObject<Movie>(responseMessage);
            }
            return View(movie);

        }

        // GET: MovieController/Create
        public async Task<ActionResult> CreateAsync()
        {
            List<Genre> genres = new List<Genre>();
            HeaderClearing();
            HttpResponseMessage httpResponseMessage = await clnt.GetAsync("api/Genre");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                genres = JsonConvert.DeserializeObject<List<Genre>>(responseMessage);
            }

            var viewModel = new MovieGenreViewModel
            {
                Movie = new Movie(),
                Genres = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(genres, "Id", "Name")
            };
            return View(viewModel);
        }


        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            movie.Genre = new Genre { Id = movie.MovieGenreId };
            if (ModelState.IsValid)
            {
                string createMovieInfo = JsonConvert.SerializeObject(movie);

                // creating string content to pass as Http content later
                StringContent stringContentInfo = new StringContent(createMovieInfo, Encoding.UTF8, "application/json");

                // Making a Post request
                HttpResponseMessage createHttpResponseMessage = clnt.PostAsync(clnt.BaseAddress + "api/Movie", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(movie);

        }

        // GET: MovieController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Movie movie = new Movie();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await clnt.GetAsync($"api/Movie/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                movie = JsonConvert.DeserializeObject<Movie>(responseMessage);
            }

            List<Genre> genres = new List<Genre>();
            httpResponseMessage = await clnt.GetAsync("api/Genre");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                genres = JsonConvert.DeserializeObject<List<Genre>>(responseMessage);
            }
            var viewModel = new MovieGenreViewModel
            {
                Movie = movie,
                Genres = new SelectList(genres, "Id", "Name", movie.MovieGenreId)
            };
            return View(viewModel);
        }


        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieGenreViewModel movieGenreModel)
        {
            movieGenreModel.Movie.Genre = new Genre { Id = movieGenreModel.Movie.MovieGenreId };
            if (ModelState.IsValid)
            {
                string createMovieInfo = JsonConvert.SerializeObject(movieGenreModel.Movie);
                StringContent stringContentInfo = new StringContent(createMovieInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage editHttpResponseMessage = clnt.PutAsync(clnt.BaseAddress + $"api/Movie/{id}", stringContentInfo).Result;
                if (editHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(movieGenreModel);
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int id)
        {
            Movie movieInfo = new Movie();
            HttpResponseMessage getMovieHttpResponseMessage = clnt.GetAsync(clnt.BaseAddress + $"api/Movie/{id}").Result;
            if (getMovieHttpResponseMessage.IsSuccessStatusCode)
            {
                movieInfo = getMovieHttpResponseMessage.Content.ReadAsAsync<Movie>().Result;
            }
            return View(movieInfo);
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Movie movie)
        {
            HttpResponseMessage deleteMovieHttpResponseMessage = clnt.DeleteAsync(clnt.BaseAddress + $"api/Movie/{id}").Result;
            if (deleteMovieHttpResponseMessage.IsSuccessStatusCode)
            {
                //productInfo = getProductHttpResponseMessage.Content.ReadAsAsync<Product>().Result;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
