using Microsoft.AspNetCore.Mvc;
using ProductList.Models;
using ProductList.Data.Contexts;
using ProductList.Data.Entities;
using ProductList.Data.Interfaces;
using ProductList.Utils;
using Rotativa.AspNetCore;

namespace ProductList.Controllers
{
    public class SalesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SalesController> _logger;
        private readonly ISaleRepository _saleRepository;

        public SalesController(AppDbContext context, ILogger<SalesController> logger, ISaleRepository saleRepository)
        {
            _logger = logger;
            _context = context;
            _saleRepository = saleRepository;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 5)
        {
            try
            {
                var items =  _saleRepository.GetAllAsync(searchString, page, pageSize);
                var count = _saleRepository.GetCountAsync(searchString);
                await Task.WhenAll(items,  count);

                var data = new SalesView().ToList(items.Result);
                var totalPages = count.Result;

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.PageSize = pageSize;
                ViewBag.SearchString = searchString ?? "";

                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieve data");
            }
            return BadRequest();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SalesView Sales)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _saleRepository.AddAndSaveAsync(Sales.ToEntity(new Sale()));
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Error when add data");
                }
                
            }
            return BadRequest();
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var item = await _saleRepository.GetByIdAsync(id);
                if (item == null)
                    return NotFound();
                var data = new SalesView(item);
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when get data by id {id}");
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SalesView param)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _saleRepository.GetByIdAsync(param.Id);
                    if(data == null)
                    {
                        _logger.LogWarning($"Data with id {param.Id} not found");
                        return NotFound();
                    }
                    await _saleRepository.UpdateAndSaveAsync(param.ToEntity(data));
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, $"Error when update data");
                }
            }
            return BadRequest();
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var sale = await _saleRepository.GetByIdAsync(id);
                if (sale == null)
                    return NotFound();
                SalesView result = new SalesView(sale);
                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when get item with id {id}");
            }
            return BadRequest();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var sale = await _saleRepository.GetByIdAsync(id);
                if (sale != null)
                {
                    await _saleRepository.DeleteAndSaveAsync(id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when remove item with id {id}");
            }
            return BadRequest();
        }

        public async Task<IActionResult> ExportExcel(string searchString, int page = 1, int pageSize = 5)
        {
            var items = await _saleRepository.GetAllAsync(searchString, page, pageSize);

            var data = new SalesView().ToList(items);
            var workbook = new ProductReportUtil().Generate(data);

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Laporan-{DateTime.Now}.xlsx");
        }

        public async Task<IActionResult> GeneratePdf(string searchString, int page = 1, int pageSize = 5)
        {
            var items = await _saleRepository.GetAllAsync(searchString, page, pageSize);

            var data = new SalesView().ToList(items);

            return new ViewAsPdf("Report", data)
            {
                FileName = $"Laporan-{DateTime.Now}.pdf"
            };
        }
    }
}