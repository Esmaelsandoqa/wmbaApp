using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wmbaApp.Data;
using wmbaApp.Models;
using wmbaApp.ViewModels;

namespace wmbaApp.Controllers
{
    public class PlayersLineUpController : Controller
    {
        private readonly WmbaContext _context;

        public PlayersLineUpController(WmbaContext context)
        {
            _context = context;
        }

        public async Task <IActionResult> Index(string SearchString, int? ID,
             int? page, int? pageSizeID, string actionButton, string sortDirection = "asc", string sortField = "")
        {

            var data =  _context.PlayersLineUp.Include(x => x.Game).Include(x => x.Team).Include(x => x.Players).AsNoTracking();


            ViewData["Filtering"] = "btn-outline-secondary";
            int numberFilters = 0;

            string[] sortOptions = new[] { "Players LineUp Name", "Game Name", "Team Name", "Player Name" };
            ViewData["ID"] = new SelectList(_context.PlayersLineUp.ToList(), "Id", "Name");
            //Add as many filters as needed
            if (ID.HasValue)
            {
                data = data.Where(t => t.Id == ID);
                numberFilters++;
            }

            if (!System.String.IsNullOrEmpty(SearchString))
            {
                data = data.Where(p => p.Name.ToUpper().Contains(SearchString.ToUpper())

                                       );

                numberFilters++;
            }
            //Give feedback about the state of the filters
            if (numberFilters != 0)
            {
                //Toggle the Open/Closed state of the collapse depending on if we are filtering
                ViewData["Filtering"] = " btn-danger";
                //Show how many filters have been applied
                ViewData["numberFilters"] = "(" + numberFilters.ToString()
                    + " Filter" + (numberFilters > 1 ? "s" : "") + " Applied)";
                //Keep the Bootstrap collapse open
                //@ViewData["ShowFilter"] = " show";
            }

            //Before we sort, see if we have called for a change of filtering or sorting
            if (!System.String.IsNullOrEmpty(actionButton)) //Form Submitted!
            {
                page = 1;//Reset page to start

                if (sortOptions.Contains(actionButton))//Change of sort is requested
                {
                    if (actionButton == sortField) //Reverse order on same field
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;//Sort by the button clicked
                }
            }
            //Now we know which field and direction to sort by
            if (sortField == "Players LineUp Name")
            {

                if (sortDirection == "asc")
                {
                    data = data
                        .OrderBy(p => p.
                        Name);
                }
                else
                {
                    data = data
                        .OrderByDescending(p => p.Name);
                }
            }
            else if (sortField == "Game Name")
            {
                if (sortDirection == "asc")
                {
                    data = data
                      .OrderBy(p => p.Game.GameLocation);
                }
                else
                {
                    data = data
                       .OrderByDescending(p => p.Game.GameLocation);
                }
            }
            else if (sortField == "Team Name")
            {
                if (sortDirection == "asc")
                {
                    data = data
                       .OrderBy(p => p.Team.TmName);

                }
                else
                {
                    data = data
                       .OrderByDescending(p => p.Team.TmName);

                }
            }
           
            else if (sortField == "Player Name")
            {
                if (sortDirection == "asc")
                {
                    data = data
                       .OrderBy(p => p.Name);

                }
                else
                {
                    data = data
                       .OrderByDescending(p => p.Name);

                }
            }
           
            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            //int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            //ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            //var pagedData = await PaginatedList<Game>.CreateAsync(data.AsNoTracking(), page ?? 1, pageSize);




            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.games = new SelectList(_context.Games.ToList(), "ID", "GameLocation");
            ViewBag.Teams = new SelectList(_context.Teams.ToList(), "ID", "TmName");
            ViewBag.players = new SelectList(_context.Players.ToList(), "ID", "PlyrFirstName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,GameId,TeamId,PlayersIds")] PlayerLinUpVM playerLinUpVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingPlayers = await _context.Players
                    .Where(p => playerLinUpVM.PlayersIds.Contains(p.ID))
                    .ToListAsync();
                    var playerLineUp = new PlayersLineUp
                    {
                        Name = playerLinUpVM.Name,
                        GameId = playerLinUpVM.GameId,
                        TeamId = playerLinUpVM.TeamId,
                        // Other properties as needed
                        //Players = playerLinUpVM.PlayersIds
                        //.Select(playerId => new Player { ID = playerId })
                        //.ToList()
                        Players= existingPlayers
                    };
                    await _context.PlayersLineUp.AddAsync(playerLineUp);
                    await _context.SaveChangesAsync();

                    //await _context.PlayersLineUp.AddAsync(playerLinUpVM);
                    //await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }


            }
            catch(Exception ex)
            {


                ViewBag.games = new SelectList(_context.Games.ToList(), "ID", "GameLocation");
                ViewBag.Teams = new SelectList(_context.Teams.ToList(), "ID", "TmName");
                ViewBag.players = new SelectList(_context.Games.ToList(), "ID", "PlyrFirstName");
                return View();
            }
            

            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Fetch the PlayersLineUp entity by id
            var playersLineUp = _context.PlayersLineUp
                .Include(pl => pl.Game)
                .Include(pl => pl.Team)
                .Include(pl => pl.Players)
                .FirstOrDefault(pl => pl.Id == id);

            if (playersLineUp == null)
            {
                return NotFound();
            }

            // Populate dropdowns for Games, Teams, and Players
            ViewBag.Games = new SelectList(_context.Games.ToList(), "ID", "GameLocation", playersLineUp.GameId);
            ViewBag.Teams = new SelectList(_context.Teams.ToList(), "ID", "TmName", playersLineUp.TeamId);
            ViewBag.Players = new MultiSelectList(_context.Players.ToList(), "ID", "PlyrFirstName", playersLineUp.Players.Select(p => p.ID));

            // Map the entity to the view model
            var viewModel = new PlayerLinUpVM
            {
                Id = playersLineUp.Id,
                Name = playersLineUp.Name,
                GameId = playersLineUp.GameId,
                TeamId = playersLineUp.TeamId,
                PlayersIds = playersLineUp.Players.Select(p => p.ID).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GameId,TeamId,PlayersIds")] PlayerLinUpVM playerLinUpVM)
        {
            if (id != playerLinUpVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch the existing PlayersLineUp entity
                    var playersLineUp = _context.PlayersLineUp
                        .Include(pl => pl.Players)
                        .FirstOrDefault(pl => pl.Id == id);

                    if (playersLineUp == null)
                    {
                        return NotFound();
                    }

                    // Update properties based on the view model
                    playersLineUp.Name = playerLinUpVM.Name;
                    playersLineUp.GameId = playerLinUpVM.GameId;
                    playersLineUp.TeamId = playerLinUpVM.TeamId;

                    // Update associated players
                    playersLineUp.Players = _context.Players.Where(p => playerLinUpVM.PlayersIds.Contains(p.ID)).ToList();

                    // Save changes
                    _context.Update(playersLineUp);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayersLineUpExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // If the model is not valid, repopulate dropdowns and return to the view
            ViewBag.Games = new SelectList(_context.Games.ToList(), "ID", "GameLocation", playerLinUpVM.GameId);
            ViewBag.Teams = new SelectList(_context.Teams.ToList(), "ID", "TmName", playerLinUpVM.TeamId);
            ViewBag.Players = new MultiSelectList(_context.Players.ToList(), "ID", "PlyrFirstName", playerLinUpVM.PlayersIds);

            return View(playerLinUpVM);
        }


        public IActionResult Delete(int id)
        {
            var playersLineUp = _context.PlayersLineUp
                .Include(pl => pl.Game)
                .Include(pl => pl.Team)
                .Include(pl => pl.Players)
                .FirstOrDefault(pl => pl.Id == id);

            if (playersLineUp == null)
            {
                return NotFound();
            }

            return View(playersLineUp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playersLineUp = await _context.PlayersLineUp
                .Include(pl => pl.Players)
                .FirstOrDefaultAsync(pl => pl.Id == id);

            if (playersLineUp == null)
            {
                return NotFound();
            }

            // Remove associated players
            _context.Players.RemoveRange(playersLineUp.Players);

            // Remove the PlayersLineUp entity
            _context.PlayersLineUp.Remove(playersLineUp);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var playersLineUp = _context.PlayersLineUp
                .Include(pl => pl.Game)
                .Include(pl => pl.Team)
                .Include(pl => pl.Players)
                .FirstOrDefault(pl => pl.Id == id);

            if (playersLineUp == null)
            {
                return NotFound();
            }

            return View(playersLineUp);
        }
        private bool PlayersLineUpExists(int id)
        {
            return _context.PlayersLineUp.Any(e => e.Id == id);
        }
        [HttpPost]
        public JsonResult GetPlayerByTeamId(int teamId)
        {
            var data = _context.Players.Where(x => x.TeamID == teamId).Select(x => new { x.ID, x.FullName }).ToList();
            return Json(data);
        }
    }
}
