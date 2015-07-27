using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using UnitTestingWithNBuilderAndFakeDbSet.Models;
using UnitTestingWithNBuilderAndFakeDbSet.ViewModels;

namespace UnitTestingWithNBuilderAndFakeDbSet.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IApplicationDbContext _dbContext;

        public ProductsController() : this(new ApplicationDbContext())
        {
            
        }

        public ProductsController(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index(bool includeDiscontinued = false)
        {
            // Set base product query
            var productsQuery = _dbContext.Products.AsQueryable();

            // Filter out discontinued products
            if (!includeDiscontinued)
                productsQuery = productsQuery.Where(p => p.IsDiscontinued == false);

            // Create view model
            var viewModel = new ProductIndexViewModel
            {
                IncludeDiscontinued = includeDiscontinued,
                Products = productsQuery.ToList()
            };

            // Display index view
            return View(viewModel);
        }

        public ActionResult Details(int? id)
        {
            // Ensure an Id is passed in
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Get the product from the database
            Product product = _dbContext.Products.Find(id);

            // Ensure the product exists
            if (product == null)
                return HttpNotFound();

            // Display the product details view
            return View(product);
        }
    }
}
