using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NASADataApi;

namespace ShowNasaPictures.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly INASAApi _nasaApi;

        public SampleDataController(INASAApi nasaApi)
        {
            _nasaApi = nasaApi ?? throw new ArgumentNullException(nameof(nasaApi));
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<NASADM>> NasaData()
        {
            var result = new List<NASADM>();

            FileStream fileStream = new FileStream("dates.txt", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();

                    DateTime? date = DateParser.ParseDate(line);
                    if (date != null)
                    {
                        // Use await to asynchronously wait for the task to complete
                        NASADM data = await _nasaApi.GetImagesByDateAsync((DateTime)date);

                        if (!string.IsNullOrEmpty(data.url))
                        {
                            result.Add(data);
                        }
                    }
                }
            }

            return result;
        }
    }
}
