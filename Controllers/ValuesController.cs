using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace simple_api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        ObjectCache cache = MemoryCache.Default;
        CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30) };
        public ValuesController() {
            if (!cache.Contains("People")) {
                // Simple List of People for CRUD Example
                List<string> people = new List<string>();

                // Add some generic values
                people.Add("Patrict Stewart");
                people.Add("Brent Spiner");
                people.Add("Jonathon Frakes");
                people.Add("Marina Sirtus");
                people.Add("Gates McFadden");
                people.Add("Michael Dorn");
                people.Add("LeVar Burton");
                people.Add("Wil Wheaton");
                people.Add("Denise Crosby");
                people.Add("Majel Barrett");
                people.Add("Colm Meaney");
                people.Add("Whoopi Goldberg");
                people.Add("John Di Lancie");
                people.Add("Diana Muldaur");
                people.Add(DateTime.Now.ToLongTimeString());

                // Add a new Cache!
                cache.Add("People", people, policy);

            } // end of if
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() {
            return (List<string>)cache.Get("People");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) {
            // Get the List of Entities from the Cache
            List<string> people = (List<string>)cache.Get("People");

            // Don't Process if ID is out of Range 0-entity,count
            if (id >= people.Count || id < 0) {
                // Make a bad response and throw it
                throw new ArgumentException();
            } else {
                return people[id];
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value) {
            // Get the List of Entities from the Cache
            List<string> people = (List<string>)cache.Get("People");

            // Add the entity
            people.Add(value);

            // Update the Cache
            //cache["People"] = people;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
            // Get the List of Entities from the Cache
            List<string> people = (List<string>)cache.Get("People");

            // Don't Process if ID is out of Range 0-entity,count
            if (id >= people.Count || id < 0) {
                // Make a bad response and throw it
                throw new ArgumentException();
            }

            //Update the Entity
            people[id] = value;

            // Update the Cache
            //cache["People"] = people;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
            // Get the List of Entities from the Cache
            List<string> people = (List<string>)cache.Get("People");

            // Don't Process if ID is out of Range 0-entity,count
            if (id >= people.Count || id < 0) {
                // Make a bad response and throw it
                throw new ArgumentException();
            }

            // Delete the Entity
            people.RemoveAt(id);

            // Update the Cache
            //cache["People"] = people;
        }
    }
}
