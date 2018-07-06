using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SpecialistDic.DataAccess;

namespace SpecialistDic.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITermQueryHandler _queryHandler;
        private readonly string _mySearchPath;

        public HomeController(ITermQueryHandler queryHandler, IOptions<ApplicationSettings> myPath) 
        {
            _queryHandler = queryHandler;
            _mySearchPath = myPath.Value.SearchPath;
        }
        
        
        /// <summary>
        /// Get users input
        /// </summary>
        /// <returns>
        /// input form
        /// </returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get users input
        /// Get one List of Termgroups containing Term = filter. See Model -> DataModel.cs -> TermGroup
        /// Get one List with all TermGroups if filter is null or empty
        /// </summary>
        /// <param name="q"></param>
        /// <returns>
        /// input form
        /// List result with Termgroups
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Index(string q)
        {
            if (String.IsNullOrEmpty(q))
            {
                return View();
            }

            var query = new TermQuery
            {
                SearchPath = _mySearchPath,
                SearchText = q,
                MaxResults = q.Length != 1 ? 20 : short.MaxValue
            };

            var result = await _queryHandler.ExecuteQueryAsync(query);
            
            return View("Result", result);
        }
        
        public IActionResult Error()
        {
            return View();
        }
    }
}
