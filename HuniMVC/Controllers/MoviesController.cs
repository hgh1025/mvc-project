using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HuniMVC.Data;
using HuniMVC.Models;
using System.ComponentModel.DataAnnotations;
using Azure.Messaging;
using HuniMVC.ViewModel;
using DnsClient;
using Humanizer.Localisation;

namespace HuniMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly HuniMVCContext _context;

        public MoviesController(HuniMVCContext context)
        {
            _context = context;
        }
        //[HttpPost]
        //public JsonResult CommentAdd(int? messageId, string comment)
        //{
        //    //var movie = _context.Movies.Where(x => x.Id == id);
        //    //var message = _context.Messages.Select(x =>  new { messageId = new Guid() });
        //    var message = _context.Messages.Where(x => x.Id == messageId); ;
        //    var newmessage = new MessageComment
        //    {
        //        //Message = message,
        //        Body = comment,
        //        CreateDate = DateTime.Now


        //    };
        //    if (message != null)
        //    {
        //        // message.Comments.Add(messageComment);

        //        _context.Add(message);
        //        _context.SaveChanges();
        //    }
        //    return Json(newmessage);
        //}
  
        // GET: Movies
        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            //IQueryable<string> genreQuery = from m in _context.Movie
            //                                orderby m.Genre
            //                                select m.Genre;
            var genreData = _context.Movies.Where(m => m.Genre == movieGenre);
            //var movies = from m in _context.Movie
            //             select m;
            //var movieData = _context.Movie;
            var movies = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            if (movies.Any(x => x.MessageId == null))
            {
            }
                var movieGenreVM = new MovieGenreViewModel
                {
                   MovieGenre = movieGenre,
                    Genres = new SelectList(await genreData.Distinct().ToListAsync()),
                    Movies = await movies.ToListAsync(),

                };
           
            return View(movieGenreVM);
        }
        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

   
        // GET: Movies/Details/5
        [HttpGet]
        public async Task<IActionResult> MovieDetails(int? id, Guid? messageId, string comment)
        {
            var movie = _context.Movies.Find(id);
            //var comments = _context.Messages.Where(c => c.MovieId == id && c.Body == comment).ToList();
            var comments = _context.Messages.Where(c => c.MovieId == id ).ToList();
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }
            var viewModel = new MovieDetailsViewModel
            {
                Id = movie.MovieId,
                Title = movie.Title,
                Price = movie.Price,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                //Movie = movie,
                Messages = comments
            };
            ViewBag.MessageId = movie.MessageId;
            ViewBag.MovieId = movie.MovieId;
            //var movie = await _context.Movies .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // GET: Movies/Create
        public IActionResult MovieCreate()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MovieCreate([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/MovieEdit/5
        public async Task<IActionResult> MovieEdit(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/MovieEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] // 요청 위조 방지를 위해 사용(XSRF/CSRF)
        public async Task<IActionResult> MovieEdit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie) //[Bind] 속성은 사용자 입력을 특정 모델 속성에 제한하는 역할을 합니다. 이를 통해 보안을 강화할 수 있습니다. Over-Posting 공격을 방지
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid) //양식에서 제출된 데이터를 사용하여 Movie 개체를 수정(편집 또는 업데이트)할 수 있는지를 확인합니다. 데이터가 유효하면 저장됩니다
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> MovieDelete(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MovieDelete(int id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'HuniMVCContext.Movie'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_context.Movies?.Any(e => e.MovieId == id)).GetValueOrDefault();
        }

  
    }

  
}
