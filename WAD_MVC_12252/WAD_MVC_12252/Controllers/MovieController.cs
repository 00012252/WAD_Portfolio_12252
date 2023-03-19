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


            //// Creating the list of new Products list
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
        public ActionResult Details(int id)
        {
            //Creating a Get Request to get single Product
            Movie movieDetails = new Movie();

            HeaderClearing();

            // Creating a get request after preparation of get URL and assignin the results

            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Movie/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                movieDetails = JsonConvert.DeserializeObject<Movie>(detailsInfo);
            }
            return View(movieDetails);
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            movie.Genre = new Genre { Id = movie.MovieGenreId };
            if (ModelState.IsValid)
            {
                // serializing movir object into json format to send
                /*string jsonObject = "{"+movie."}"*/
                ;
                string createMovieInfo = JsonConvert.SerializeObject(movie);

                // creating string content to pass as Http content later
                StringContent stringContentInfo = new StringContent(createMovieInfo, Encoding.UTF8, "application/json");

                // Making a Post request
                HttpResponseMessage createHttpResponseMessage = clnt.PostAsync(clnt.BaseAddress + "api/Movie/", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(movie);

        }

        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
