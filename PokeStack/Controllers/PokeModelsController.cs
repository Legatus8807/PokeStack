#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokeStack.Data;
using PokeStack.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace PokeStack.Controllers
{
    public class PokeModelsController : Controller
    {
        private readonly PokeStackContext _context;

        public PokeModelsController(PokeStackContext context)
        {
            _context = context;
        }

        // GET: PokeModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PokeModel.ToListAsync());
        }

        // GET: PokeModels/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokeModel = await _context.PokeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokeModel == null)
            {
                return NotFound();
            }

            return View(pokeModel);
        }

        // GET: PokeModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PokeModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,pokeName,type1,type2,hp,atk,def,spA,spD,spe,imageUrl")] PokeModel pokeModel, PokeModelVM pokeModelVM)
        {
            string pokeNameVM = pokeModelVM.pokeNameVM.ToLower();
            
            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                {
                    string url = String.Format("https://pokeapi.co/api/v2/pokemon/" + pokeNameVM);
                    var response = client.GetAsync(url).Result;

                    string responseAsString = await response.Content.ReadAsStringAsync();

                    //PokeGetModel result = JsonConvert.DeserializeObject<PokeGetModel>(responseAsString);
                    PokeGetModel result = JsonConvert.DeserializeObject<PokeGetModel>(responseAsString);

                    //pokeModel.pokeName = pokeNameVM;
                    pokeModel.pokeName = char.ToUpper(pokeNameVM.First()) + pokeNameVM.Substring(1).ToLower();
                    string type1Get = result.types[0].type.name;
                    pokeModel.type1 = char.ToUpper(type1Get.First()) + type1Get.Substring(1).ToLower();
                    if (result.types.Count == 1)
                    {
                        pokeModel.type2 = "N/A";
                    }
                    else
                    {
                        string type2Get = result.types[1].type.name;
                        pokeModel.type2 = char.ToUpper(type2Get.First()) + type2Get.Substring(1).ToLower();
                    }
                    pokeModel.hp = result.stats[0].base_stat;
                    pokeModel.atk = result.stats[1].base_stat;
                    pokeModel.def = result.stats[2].base_stat;
                    pokeModel.spA = result.stats[3].base_stat;
                    pokeModel.spD = result.stats[4].base_stat;
                    pokeModel.spe = result.stats[5].base_stat;
                    pokeModel.imageUrl = result.sprites.other.home.front_default;

                    Debug.WriteLine(message: result.types[0].type.name);
                    if (result.types.Count == 1)
                    {
                        Debug.WriteLine("N/A");
                    }
                    else
                    {
                        Debug.WriteLine(result.types[1].type.name);
                    }
                }

                _context.Add(pokeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pokeModel);
        }

        // GET: PokeModels/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokeModel = await _context.PokeModel.FindAsync(id);
            if (pokeModel == null)
            {
                return NotFound();
            }
            return View(pokeModel);
        }

        // POST: PokeModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,pokeName,type1,type2,hp,atk,def,spA,spD,spe,imageUrl")] PokeModel pokeModel)
        {
            if (id != pokeModel.Id)
            {
                return NotFound();
            }

            string pokeName = pokeModel.pokeName.ToLower();

            if (ModelState.IsValid)
            {
                try
                {

                    using (var client = new HttpClient())
                    {
                        string url = String.Format("https://pokeapi.co/api/v2/pokemon/" + pokeName);
                        var response = client.GetAsync(url).Result;

                        string responseAsString = await response.Content.ReadAsStringAsync();

                        //PokeGetModel result = JsonConvert.DeserializeObject<PokeGetModel>(responseAsString);
                        PokeGetModel result = JsonConvert.DeserializeObject<PokeGetModel>(responseAsString);

                        pokeModel.pokeName = char.ToUpper(pokeName.First()) + pokeName.Substring(1).ToLower();
                        string type1Get = result.types[0].type.name;
                        pokeModel.type1 = char.ToUpper(type1Get.First()) + type1Get.Substring(1).ToLower();
                        if (result.types.Count == 1)
                        {
                            pokeModel.type2 = "N/A";
                        }
                        else
                        {
                            string type2Get = result.types[1].type.name;
                            pokeModel.type2 = char.ToUpper(type2Get.First()) + type2Get.Substring(1).ToLower();
                        }
                        pokeModel.hp = result.stats[0].base_stat;
                        pokeModel.atk = result.stats[1].base_stat;
                        pokeModel.def = result.stats[2].base_stat;
                        pokeModel.spA = result.stats[3].base_stat;
                        pokeModel.spD = result.stats[4].base_stat;
                        pokeModel.spe = result.stats[5].base_stat;
                        pokeModel.imageUrl = result.sprites.other.home.front_default;

                        Debug.WriteLine(message: result.types[0].type.name);
                        if (result.types.Count == 1)
                        {
                            Debug.WriteLine("N/A");
                        }
                        else
                        {
                            Debug.WriteLine(result.types[1].type.name);
                        }
                    }

                    _context.Update(pokeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokeModelExists(pokeModel.Id))
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
            return View(pokeModel);
        }

        // GET: PokeModels/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokeModel = await _context.PokeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokeModel == null)
            {
                return NotFound();
            }

            return View(pokeModel);
        }

        // POST: PokeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var pokeModel = await _context.PokeModel.FindAsync(id);
            _context.PokeModel.Remove(pokeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokeModelExists(long id)
        {
            return _context.PokeModel.Any(e => e.Id == id);
        }
    }
}
